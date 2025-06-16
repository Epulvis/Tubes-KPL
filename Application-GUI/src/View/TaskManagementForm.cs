using Application.Models;
using Application.Helpers;
using Application_GUI.src.View;
using Application.Controller;
using Application.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Application.View;

public partial class TaskManagementForm : Form, ITaskView
{
    public event Action AddTaskRequested;
    public event Action ViewTasksRequested;
    public event Action ViewTaskDetailsRequested;
    public event Action UpdateTaskStatusRequested;
    public event Action DeleteTaskRequested;
    public event Action FilterTasksByDateRequested;
    public event Action ExportTasksRequested;
    public event Action FormLoaded;

    public HttpClient httpClient;
    public InputValidator inputValidator;
    public StatusStateMachine statusStateMachine;
    public TaskService taskService;
    public ITaskView view => this;
    public TaskPresenter taskPresenter;

    private string _new_task_id;
    private string _new_task_title;
    private string _new_task_description;
    private DateTime _new_task_dueDate;
    private int _new_task_kategoriIndex;
    private int _new_task_StatusIndex;

    // Constructor initializes the form and sets up the task service and presenter
    public TaskManagementForm()
    {
        httpClient = new HttpClient();
        inputValidator = new InputValidator();
        statusStateMachine = new StatusStateMachine();
        InitializeComponent();

        taskService = new TaskService(httpClient, inputValidator, statusStateMachine);
        taskPresenter = new TaskPresenter(view, taskService, inputValidator);

        // Hubungkan event handler sidebar & hamburger
        buttonHamburger.Click += ButtonHamburger_Click;
        buttonCloseSidebar.Click += ButtonCloseSidebar_Click;
        btnShowAddTaskForm.Click += BtnShowAddTaskForm_Click;
        btnShowUpdateForm.Click += BtnShowUpdateForm_Click;
        btnPageFilterTaskByDate.Click += BtnPageFilterTaskByDate_Click;
        buttonHome.Click += ButtonHome_Click;

        // Double click pada DataGridView untuk detail
        dgvTasks.CellDoubleClick += DgvTasks_CellDoubleClick;

        // Atur posisi hamburger saat resize
        this.Resize += (s, e) => AdjustHamburgerPosition();

        taskPresenter.OnViewTasksRequested();
    }

    private void AdjustHamburgerPosition()
    {
        buttonHamburger.Location = new Point(panelSidebar.Visible ? panelSidebar.Width + 8 : 8, 8);
    }

    // === SIDEBAR & HAMBURGER EVENT HANDLERS ===

    private void ButtonHamburger_Click(object sender, EventArgs e)
    {
        panelSidebar.Visible = !panelSidebar.Visible;
        AdjustHamburgerPosition();
    }

    private void ButtonCloseSidebar_Click(object sender, EventArgs e)
    {
        panelSidebar.Visible = false;
        AdjustHamburgerPosition();
    }

    // === BUTTON EVENT HANDLERS ===
    private void BtnShowAddTaskForm_Click(object sender, EventArgs e)
    {
        using var addTaskForm = new CreateTaskForm();
        if (addTaskForm.ShowDialog(this) == DialogResult.OK)
        {
            _new_task_title = addTaskForm.JudulTugas;
            _new_task_dueDate = addTaskForm.Deadline;
            _new_task_kategoriIndex = addTaskForm.KategoriIndex;
            AddTaskRequested?.Invoke();
            taskPresenter.OnViewTasksRequested();
        }
    }

    // === BUTTON EVENT HANDLERS FOR UPDATE AND FILTER ===
    private void BtnShowUpdateForm_Click(object sender, EventArgs e)
    {
        using var updateTaskStatusForm = new UpdateTaskStatusForm();
        if (updateTaskStatusForm.ShowDialog(this) == DialogResult.OK)
        {
            _new_task_id = updateTaskStatusForm.IdTask;
            _new_task_StatusIndex = updateTaskStatusForm.StatusIndex;
            UpdateTaskStatusRequested?.Invoke();
        }
    }

    // === BUTTON EVENT HANDLER FOR FILTER TASKS BY DATE ===
    private void BtnPageFilterTaskByDate_Click(object sender, EventArgs e)
    {
        FilteredTaskByDateForm form = new FilteredTaskByDateForm(this);
        form.Show();
        this.Hide();
    }

    private void ButtonHome_Click(object sender, EventArgs e)
    {
        var deleteTaskForm = new DeleteTask(this);
        deleteTaskForm.Show();
        this.Hide();
    }

    // === DATAGRIDVIEW EVENT HANDLER ===

    private void DgvTasks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && dgvTasks.Rows[e.RowIndex].Cells["Id"].Value != null)
        {
            int id = Convert.ToInt32(dgvTasks.Rows[e.RowIndex].Cells["Id"].Value);
            taskPresenter.OnViewTaskDetailsRequested(id);
        }
    }

    // === ITaskView IMPLEMENTATION ===
    
    public string GetTaskTitleInput() => _new_task_title;
    public string GetTaskDescriptionInput() => _new_task_description;
    public DateTime GetTaskDueDateInput() => _new_task_dueDate;
    public int GetKategoriIndexInput() => _new_task_kategoriIndex;

    public int GetSelectedTaskId()
    {
        if (dgvTasks.SelectedRows.Count > 0)
        {
            var row = dgvTasks.SelectedRows[0];
            if (row.Cells["Id"].Value != null && int.TryParse(row.Cells["Id"].Value.ToString(), out int id))
                return id;
        }
        return -1;
    }

    // Method to get the new task status input from the user
    public StatusTugas GetNewTaskStatusInput()
    {
        var dialog = new Form();
        dialog.Text = "Update Status Tugas";
        var comboBoxStatus = new ComboBox { Dock = DockStyle.Top };
        comboBoxStatus.Items.AddRange(Enum.GetNames(typeof(StatusTugas)));
        comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        if (comboBoxStatus.Items.Count > 0) comboBoxStatus.SelectedIndex = 0;

        var btnOk = new Button { Text = "OK", Dock = DockStyle.Bottom };
        StatusTugas selectedStatus = StatusTugas.BelumMulai;
        btnOk.Click += (s, e) =>
        {
            if (comboBoxStatus.SelectedItem != null)
            {
                Enum.TryParse(comboBoxStatus.SelectedItem.ToString(), out selectedStatus);
            }
            dialog.DialogResult = DialogResult.OK;
            dialog.Close();
        };
        dialog.Controls.Add(comboBoxStatus);
        dialog.Controls.Add(btnOk);
        dialog.StartPosition = FormStartPosition.CenterParent;

        return dialog.ShowDialog() == DialogResult.OK ? selectedStatus : StatusTugas.BelumMulai;
    }
    
    // Method to reload tasks after an operation
    public void ReloadTask()
    {
        taskPresenter.OnViewTasksRequested();
    }

    // Methods to get filter inputs from the user
    public DateTime GetFilterStartDateInput()
    {
        using (var dateDialog = new Form { Text = "Pilih Tanggal Awal", StartPosition = FormStartPosition.CenterParent, Size = new Size(250, 150) })
        using (var picker = new DateTimePicker { Dock = DockStyle.Top })
        using (var okButton = new Button { Text = "OK", Dock = DockStyle.Bottom })
        {
            DateTime result = DateTime.MinValue;
            okButton.Click += (s, e) => { result = picker.Value; dateDialog.DialogResult = DialogResult.OK; dateDialog.Close(); };
            dateDialog.Controls.Add(picker);
            dateDialog.Controls.Add(okButton);
            if (dateDialog.ShowDialog() == DialogResult.OK) return result;
            throw new OperationCanceledException("Pemilihan tanggal awal dibatalkan.");
        }
    }

    // Method to get filter end date input from the user
    public DateTime GetFilterEndDateInput()
    {
        using (var dateDialog = new Form { Text = "Pilih Tanggal Akhir", StartPosition = FormStartPosition.CenterParent, Size = new Size(250, 150) })
        using (var picker = new DateTimePicker { Dock = DockStyle.Top })
        using (var okButton = new Button { Text = "OK", Dock = DockStyle.Bottom })
        {
            DateTime result = DateTime.MaxValue;
            okButton.Click += (s, e) => { result = picker.Value; dateDialog.DialogResult = DialogResult.OK; dateDialog.Close(); };
            dateDialog.Controls.Add(picker);
            dateDialog.Controls.Add(okButton);
            if (dateDialog.ShowDialog() == DialogResult.OK) return result;
            throw new OperationCanceledException("Pemilihan tanggal akhir dibatalkan.");
        }
    }

    // Method to get export format input from the user
    public string GetExportFormatInput()
    {
        using (var formatDialog = new Form { Text = "Pilih Format Ekspor", StartPosition = FormStartPosition.CenterParent, Size = new Size(250, 150) })
        using (var cmbFormat = new ComboBox { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList })
        using (var okButton = new Button { Text = "OK", Dock = DockStyle.Bottom })
        {
            cmbFormat.Items.AddRange(new string[] { "json", "txt" });
            cmbFormat.SelectedIndex = 0;
            string result = "json";
            okButton.Click += (s, e) => { result = cmbFormat.SelectedItem.ToString(); formatDialog.DialogResult = DialogResult.OK; formatDialog.Close(); };
            formatDialog.Controls.Add(cmbFormat);
            formatDialog.Controls.Add(okButton);
            if (formatDialog.ShowDialog() == DialogResult.OK) return result;
            throw new OperationCanceledException("Pemilihan format ekspor dibatalkan.");
        }
    }

    // Method to get export file path input from the user
    public string GetExportFilePathInput(string defaultFileName, string filter)
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = filter;
            saveFileDialog.Title = "Simpan File Tugas";
            saveFileDialog.FileName = defaultFileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
        }
        throw new OperationCanceledException("Ekspor dibatalkan oleh pengguna.");
    }

    // Method to display tasks in the DataGridView
    public void DisplayTasks(List<Tugas> tasks)
    {
        dgvTasks.Columns.Clear();
        dgvTasks.Rows.Clear();

        dgvTasks.Columns.Add("Id", "ID");
        dgvTasks.Columns.Add("Judul", "Judul");
        dgvTasks.Columns.Add("Deadline", "Deadline");
        dgvTasks.Columns.Add("Status", "Status");

        if (tasks != null && tasks.Count > 0)
        {
            foreach (var tugas in tasks)
            {
                dgvTasks.Rows.Add(
                    tugas.Id,
                    tugas.Judul,
                    tugas.Deadline.ToString("dd/MM/yyyy"),
                    tugas.Status.ToString()
                );
            }
        }
    }

    // Method to display task details in a message box
    public void DisplayTaskDetails(string task)
    {
        if (task != null)
        {
            DisplayMessage(task, "Detail Tugas", MessageBoxIcon.Information);
        }
        else
        {
            DisplayMessage("Tugas tidak ditemukan.", "Error", MessageBoxIcon.Error);
        }
    }

    // Method to display a message box with a specific message, caption, and icon
    public void DisplayMessage(string message, string caption, MessageBoxIcon icon)
    {
        MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
    }

    // Method to clear input fields in the form
    public void ClearInputs()
    {
    }
}

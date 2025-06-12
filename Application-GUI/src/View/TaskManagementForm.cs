using Application.Models;
using Application.Helpers;
using Application_GUI.src.View;
using System.Text.Json;
using Application.Controller;
using Application.Services;
using System.Windows.Forms;

namespace Application.View;

public partial class TaskManagementForm : Form, ITaskView
{
    //private List<Tugas> _listTugas;
    private ListBox lstTasks;
    private TextBox txtTitle;
    private TextBox txtDescription;
    private DateTimePicker dtpDueDate;
    //private Button btnViewDetails;
    //private Button btnUpdateStatus;
    //private Button btnDeleteTask;
    //private Button btnFilterByDate;
    //private Button btnExportTasks;
    //private Button btnRefreshTasks;
    //private Label lblStatusMessage;

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

    private string _new_task_title;
    private string _new_task_description;
    private DateTime _new_task_dueDate;
    private int _new_task_kategoriIndex;

    public TaskManagementForm()
    {
        httpClient = new HttpClient();
        inputValidator = new InputValidator();
        statusStateMachine = new StatusStateMachine();
        InitializeComponent();


        // init controller
        taskService = new TaskService(httpClient, inputValidator, statusStateMachine);
        taskPresenter = new TaskPresenter(view, taskService, inputValidator);

        taskPresenter.OnViewTasksRequested();
        this.btnShowAddTaskForm.Click += new System.EventHandler(this.btnShowAddTaskForm_Click);
    }

    public string GetTaskTitleInput() => _new_task_title;
    public string GetTaskDescriptionInput() => _new_task_description;
    public DateTime GetTaskDueDateInput() => _new_task_dueDate;
    public int GetKategoriIndexInput() => _new_task_kategoriIndex;

    private void btnShowAddTaskForm_Click(object sender, EventArgs e)
    {
        using (var addTaskForm = new CreateTaskForm())
        {
            if (addTaskForm.ShowDialog(this) == DialogResult.OK)
            {
                _new_task_title = addTaskForm.JudulTugas;
                _new_task_dueDate = addTaskForm.Deadline;
                _new_task_kategoriIndex = addTaskForm.KategoriIndex;
                AddTaskRequested?.Invoke();
                taskPresenter.OnViewTasksRequested();
            }
        }
    }

    public int GetSelectedTaskId()
    {
        if (lstTasks.SelectedItem is Tugas selectedTask)
        {
            return selectedTask.Id;
        }

        if (lstTasks.SelectedIndex != -1 && int.TryParse(lstTasks.SelectedItem?.ToString().Split(':')[0].Trim(), out int id))
        {
            return id;
        }
        return -1;
    }

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


    public DateTime GetFilterStartDateInput()
    {
        using (var dateDialog = new Form { Text = "Pilih Tanggal Awal", StartPosition = FormStartPosition.CenterParent, Size = new System.Drawing.Size(250, 150) })
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

    public DateTime GetFilterEndDateInput()
    {
        using (var dateDialog = new Form { Text = "Pilih Tanggal Akhir", StartPosition = FormStartPosition.CenterParent, Size = new System.Drawing.Size(250, 150) })
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

    public string GetExportFormatInput()
    {
        using (var formatDialog = new Form { Text = "Pilih Format Ekspor", StartPosition = FormStartPosition.CenterParent, Size = new System.Drawing.Size(250, 150) })
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

    public string GetExportFilePathInput(string defaultFileName, string filter)
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Filter = filter;
            saveFileDialog.Title = "Simpan File Tugas";
            saveFileDialog.FileName = defaultFileName;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Secure Coding: Path yang dipilih pengguna aman untuk digunakan
                return saveFileDialog.FileName;
            }
        }
        // Secure Coding: Handle pembatalan oleh pengguna
        throw new OperationCanceledException("Ekspor dibatalkan oleh pengguna.");
    }

    // display all task
    public void DisplayTasks(List<Tugas> tasks)
    {
        // if task not null and count > 0, then display in datagridview
        if (tasks != null && tasks.Count > 0)
        {
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = tasks;
        }
        else
        {
            dataGridView1.DataSource = "Tidak ada tugas";
            //lstTasks.Items.Add("Tidak ada tugas.");
        }
    }

    // display task detait
    public void DisplayTaskDetails(string task)
    {
        // if task is not null, then display in display message
        if (task != null)
        {
            DisplayMessage(task, "Detail Tugas", MessageBoxIcon.Information);
        }
        else
        {
            DisplayMessage("Tugas tidak ditemukan.", "Error", MessageBoxIcon.Error);
        }
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        // Cek jika kolom "Detail" yang diklik
        if (dataGridView1.Columns[e.ColumnIndex].Name == "detailButton" && e.RowIndex >= 0)
        {
            taskPresenter.OnViewTaskDetailsRequested(e.RowIndex+1);
        }
    }

    public void DisplayMessage(string message, string caption, MessageBoxIcon icon)
    {
        MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
    }

    public void ClearInputs()
    {
    }

    private void ButtonHamburger_Click(object sender, EventArgs e)
    {
        // Toggle panel visibility
        panelSidebar.Visible = !panelSidebar.Visible;
        flowLayoutPanel1.Location = new Point(141, 31);
        flowLayoutPanel1.Size = new Size(417, 257);
        dataGridView1.Location = new Point(3, 3);
        dataGridView1.Size = new Size(491, 252);
    }

    private void ButtonCloseSidebar_Click(object sender, EventArgs e)
    {
        panelSidebar.Visible = false;
        flowLayoutPanel1.Location = new Point(0, 31);
        flowLayoutPanel1.Size = new Size(558, 257);
        dataGridView1.Location = new Point(3, 3);
        dataGridView1.Size = new Size(555, 254);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        FilteredTaskByDateForm form = new FilteredTaskByDateForm(this);
        form.Show();
        this.Hide();
    }


    // button delete task
    private void buttonHome_Click(object sender, EventArgs e)
    {
        var deleteTaskForm = new DeleteTask(this); // Kirim referensi dashboard
        deleteTaskForm.Show();
        this.Hide();
    }
    
    // button delete task
    // private void btnDeleteTask_Click(object sender, EventArgs e)
    // {
    //     var deleteTaskForm = new DeleteTask(this); // Kirim referensi dashboard
    //     deleteTaskForm.Show();
    //     this.Hide();
    // }
}

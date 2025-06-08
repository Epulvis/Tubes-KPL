using Application.Models;
using Application.Helpers;
using Application_GUI.src.View;

namespace Application.View
{
    public partial class TaskManagementForm : Form, ITaskView
    {
        private ListBox lstTasks;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private DateTimePicker dtpDueDate;
        private Button btnAddTask;
        private Button btnViewDetails;
        private Button btnUpdateStatus;
        private Button btnDeleteTask;
        private Button btnFilterByDate;
        private Button btnExportTasks;
        private Button btnRefreshTasks;
        private Label lblStatusMessage;

        public event Action AddTaskRequested;
        public event Action ViewTasksRequested;
        public event Action ViewTaskDetailsRequested;
        public event Action UpdateTaskStatusRequested;
        public event Action DeleteTaskRequested;
        public event Action FilterTasksByDateRequested;
        public event Action ExportTasksRequested;
        public event Action FormLoaded;
        public TaskManagementForm()
        {
            InitializeComponent();
        }

        public string GetTaskTitleInput() => txtTitle.Text.Trim();
        public string GetTaskDescriptionInput() => txtDescription.Text.Trim();
        public DateTime GetTaskDueDateInput() => dtpDueDate.Value;

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


        public void DisplayTasks(List<Tugas> tasks)
        {
            lstTasks.Items.Clear();
            if (tasks != null && tasks.Count > 0)
            {
                foreach (var task in tasks)
                {
                    lstTasks.Items.Add($"{task.Id}: {task.Judul} (Due: {task.Deadline.ToShortDateString()}) - Status: {task.Status}");
                }
            }
            else
            {
                lstTasks.Items.Add("Tidak ada tugas.");
            }
        }

        public void DisplayTaskDetails(Tugas task)
        {
            if (task != null)
            {
                // Secure Coding: Hindari menampilkan informasi sensitif jika ada.
                // Untuk aplikasi ini, detail tugas umumnya aman.
                //string details = $"ID: {task.Id}\n" +
                //                 $"Judul: {task.Judul}\n" +
                //                 $"Deskripsi: {task.Deskripsi}\n" +
                //                 $"Jatuh Tempo: {task.deadline.ToString("dd-MM-yyyy HH:mm")}\n" +
                //                 $"Status: {task.Status}\n" +
                //                 $"Dibuat: {task.CreatedAt.ToString("dd-MM-yyyy HH:mm")}\n" +
                //                 $"Diperbarui: {task.UpdatedAt.ToString("dd-MM-yyyy HH:mm")}";
                //DisplayMessage(details, "Detail Tugas", MessageBoxIcon.Information);
            }
            else
            {
                DisplayMessage("Tugas tidak ditemukan.", "Error", MessageBoxIcon.Error);
            }
        }

        public void DisplayMessage(string message, string caption, MessageBoxIcon icon)
        {
            MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
        }

        public void ClearInputs()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            dtpDueDate.Value = DateTime.Now;
            txtTitle.Focus();
        }

        private void ButtonHamburger_Click(object sender, EventArgs e)
        {
            // Toggle panel visibility
            panelSidebar.Visible = !panelSidebar.Visible;
        }

        private void ButtonCloseSidebar_Click(object sender, EventArgs e)
        {
            panelSidebar.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilteredTaskByDateForm form = new FilteredTaskByDateForm(this);
            form.Show();
            this.Hide();
        }
    }
}

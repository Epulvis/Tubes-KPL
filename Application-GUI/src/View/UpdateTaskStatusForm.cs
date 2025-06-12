using Application.Controller;
using Application.Models;
using System.ComponentModel;
using System.Text.Json;
using Application.View;

namespace Application_GUI.src.View
{
    public partial class UpdateTaskStatusForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TaskPresenter Presenter { get; internal set; }

        public UpdateTaskStatusForm()
        {
            InitializeComponent();
            this.Load += UpdateTaskStatusForm_Load;
        }

        private void UpdateTaskStatusForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(Enum.GetNames(typeof(StatusTugas)));
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        public static bool UpdateTaskStatusInJson(int id, StatusTugas newStatus, string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            var json = File.ReadAllText(filePath);
            var tasks = JsonSerializer.Deserialize<List<Tugas>>(json) ?? new List<Tugas>();

            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return false;

            task.Status = newStatus;

            var newJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, newJson);
            return true;
        }

        private void buttonUpdateOnClick(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text.Trim(), out int taskId))
            {
                MessageBox.Show("ID tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Pilih status!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var statusStr = comboBox1.SelectedItem.ToString();
            if (!Enum.TryParse<StatusTugas>(statusStr, out var newStatus))
            {
                MessageBox.Show("Status tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\API\Storage\Tugas.json");
            if (!File.Exists(filePath))
            {
                MessageBox.Show($"File tidak ditemukan:\n{filePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool success = UpdateTaskStatusInJson(taskId, newStatus, filePath);

            if (success)
                MessageBox.Show("Status tugas berhasil diperbarui di file JSON.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Gagal memperbarui status tugas di file JSON.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            var mainForm = new TaskManagementForm();
            mainForm.Show();
            this.Close();
        }


    }
}

//private async void button1_Click(object sender, EventArgs e)
//{
//    // Ambil ID dari textbox
//    if (!int.TryParse(textBox1.Text.Trim(), out int taskId))
//    {
//        MessageBox.Show("ID tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        return;
//    }

//    // Ambil status dari combobox
//    if (comboBox1.SelectedItem == null)
//    {
//        MessageBox.Show("Pilih status!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        return;
//    }
//    var statusStr = comboBox1.SelectedItem.ToString();
//    if (!Enum.TryParse<StatusTugas>(statusStr, out var newStatus))
//    {
//        MessageBox.Show("Status tidak valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        return;
//    }

//    // Langsung update lewat presenter
//    if (Presenter != null)
//    {
//        await Presenter.UpdateTaskStatusDirect(taskId, newStatus);
//    }

//    // Tutup form setelah update
//    this.Close();
//}

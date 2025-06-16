using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Application.View; 

namespace Application_GUI.src.View
{
    public partial class DeleteTask : Form
    {
        // Base URL for API
        private readonly string apiBaseUrl = "http://localhost:4000/api/tugas"; // Ganti dengan URL API yang sesuai
        private TaskManagementForm _dashboard;

        // Load tasks when form is shown
        public DeleteTask(TaskManagementForm taskManagementForm)
        {
            InitializeComponent();
            _dashboard = taskManagementForm; // Perbaiki penamaan parameter
        }

        // Fetch tasks from API and display in DataGridView
        private async void DeleteTask_Load(object sender, EventArgs e)
        {
            await LoadTasksAsync();
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(apiBaseUrl);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<List<TaskModel>>(json); 
                    dataGridViewTasks.DataSource = tasks;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load tasks: " + ex.Message);
            }
        }

        // Handle delete button click
        private async void btnContinue_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtTaskId.Text, out int taskId))
            {
                var confirm = MessageBox.Show($"Are you sure to delete task with ID {taskId}?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    await DeleteTaskByIdAsync(taskId);
                    await LoadTasksAsync();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Task ID.");
            }
        }

        // Delete task by ID via API
        private async Task DeleteTaskByIdAsync(int taskId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync($"{apiBaseUrl}/{taskId}");
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Task deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete task. Task may not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting task: " + ex.Message);
            }
        }

        // Go back to dashboard
        private void btnBack_Click(object sender, EventArgs e)
        {
            _dashboard.Show();
            this.Close();
        }
    }

    // Model for task data
    public class TaskModel
    {
        public int Id { get; set; }
        public string Judul { get; set; }
        public DateTime Deadline { get; set; }
        public string Status { get; set; }
        public string Kategori { get; set; }
       
    }
}
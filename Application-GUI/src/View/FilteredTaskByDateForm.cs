using Application.Models;
using Application.View;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application.Services;
using Application.Helpers;

namespace Application_GUI.src.View
{
    public partial class FilteredTaskByDateForm : Form
    {
        private readonly TaskManagementForm _taskManagementForm;
        private List<Tugas> _tasks = new();
        private readonly TaskService _taskService;

        public FilteredTaskByDateForm(TaskManagementForm form)
        {
            InitializeComponent();
            _taskManagementForm = form ?? throw new ArgumentNullException(nameof(form));

            var httpClient = new HttpClient();
            var validator = new InputValidator();
            var statusStateMachine = new StatusStateMachine();
            _taskService = new TaskService(httpClient, validator, statusStateMachine);

            Load += FilteredTaskByDateForm_Load;
        }

        private async void FilteredTaskByDateForm_Load(object? sender, EventArgs e)
        {
            await LoadTasksFromApiAsync();
        }

        private async Task LoadTasksFromApiAsync()
        {
            try
            {
                var result = await _taskService.GetAllTasksAsync();
                if (result.IsSuccess)
                {
                    _tasks = result.Value ?? new List<Tugas>();
                    dataGridViewTasks.AutoGenerateColumns = true;
                    dataGridViewTasks.DataSource = _tasks;
                }
                else
                {
                    ShowError("Gagal mendapatkan data.");
                    _tasks = new List<Tugas>();
                    dataGridViewTasks.DataSource = _tasks;
                }
            }
            catch (Exception)
            {
                ShowError("Terjadi kesalahan tak terduga saat memuat tugas.");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            _taskManagementForm.Show();
            Close();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            // Ambil tanggal dari DateTimePicker
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (!IsValidDateRange(startDate, endDate))
            {
                MessageBox.Show(
                    "Tanggal mulai tidak boleh melewati tanggal selesai.",
                    "Validasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var filteredTasks = _tasks
                .Where(t => t.Deadline.Date >= startDate && t.Deadline.Date <= endDate)
                .ToList();

            // Tampilkan hasil filter ke DataGridView
            dataGridViewTasks.DataSource = null;
            dataGridViewTasks.DataSource = filteredTasks;

            if (filteredTasks.Count == 0)
            {
                MessageBox.Show(
                    "Tidak ada tugas pada rentang waktu tersebut.",
                    "Informasi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private static bool IsValidDateRange(DateTime start, DateTime end)
        {
            return start <= end;
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}


using Application.Models;
using Application.View;
using Application.Services;
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
using Application.Helpers;

namespace Application_GUI.src.View
{
    public partial class FilteredTaskByDateForm : Form
    {
        private TaskManagementForm _taskManagementForm;
        private List<Tugas> _listTugas = new List<Tugas>();
        //private List<Tugas> _listTugas;
        private TaskService _taskService;

        public FilteredTaskByDateForm(TaskManagementForm form)
        {
            InitializeComponent();
            this._taskManagementForm = form;

            // Inisialisasi dependency TaskService
            var httpClient = new HttpClient();
            var validator = new InputValidator();
            var statusStateMachine = new StatusStateMachine();
            _taskService = new TaskService(httpClient, validator, statusStateMachine);

            // Load data dari API saat form di-load
            this.Load += FilteredTaskByDateForm_Load;

            //// Path relatif ke file JSON (pastikan path ini benar sesuai struktur project Anda)
            //string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\API\Storage\Tugas.json");

            //// Baca dan deserialisasi file JSON
            //try
            //{
            //    string json = File.ReadAllText(filePath);
            //    _listTugas = JsonSerializer.Deserialize<List<Tugas>>(json, new JsonSerializerOptions
            //    {
            //        PropertyNameCaseInsensitive = true
            //    }) ?? new List<Tugas>();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Gagal membaca data tugas: {ex.Message}");
            //    _listTugas = new List<Tugas>();
            //}

            //// Tampilkan ke DataGridView
            //dataGridViewTasks.AutoGenerateColumns = true;
            //dataGridViewTasks.DataSource = _listTugas;

            //MessageBox.Show($"Jumlah tugas: {_listTugas.Count}");
        }

        private async void FilteredTaskByDateForm_Load(object? sender, EventArgs e)
        {
            await LoadTugasFromApiAsync();
        }

        private async Task LoadTugasFromApiAsync()
        {
            var result = await _taskService.GetAllTasksAsync();
            if (result.IsSuccess)
            {
                _listTugas = result.Value ?? new List<Tugas>();
                dataGridViewTasks.AutoGenerateColumns = true;
                dataGridViewTasks.DataSource = _listTugas;
                MessageBox.Show($"Jumlah tugas: {_listTugas.Count}");
            }
            else
            {
                MessageBox.Show($"Gagal mengambil data tugas dari API: {result.Error}");
                _listTugas = new List<Tugas>();
                dataGridViewTasks.DataSource = _listTugas;
            }
        }

        private void btnBack_Click1(object sender, EventArgs e)
        {
            _taskManagementForm.Show();
            this.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            // Ambil tanggal dari DateTimePicker
            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (startDate > endDate)
            {
                MessageBox.Show("Tanggal mulai tidak boleh lebih besar dari tanggal akhir.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Filter tugas berdasarkan deadline
            var filtered = _listTugas
                .Where(t => t.Deadline.Date >= startDate && t.Deadline.Date <= endDate)
                .ToList();

            // Tampilkan hasil filter ke DataGridView
            dataGridViewTasks.DataSource = null;
            dataGridViewTasks.DataSource = filtered;

            // Opsional: tampilkan pesan jika tidak ada data
            if (filtered.Count == 0)
            {
                MessageBox.Show("Tidak ada tugas pada rentang tanggal tersebut.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}


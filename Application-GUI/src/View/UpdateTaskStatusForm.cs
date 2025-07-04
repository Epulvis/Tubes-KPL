﻿using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Application_GUI.src.View
{
    public partial class UpdateTaskStatusForm : Form
    {
        public string IdTask => txtIdTask.Text.Trim();
        public int StatusIndex => cmbStatus.SelectedIndex;

        // Constructor initializes the form and hides status controls
        public UpdateTaskStatusForm()
        {
            InitializeComponent();
            HideStatusControls();
            InitializeStatusComboBox();
        }

        // Method to show status controls based on the selected task
        private void HideStatusControls()
        {
            cmbStatus.Visible = false;
            lblStatus.Visible = false;
        }

        // Method to initialize the status ComboBox with predefined statuses
        private void InitializeStatusComboBox()
        {
            cmbStatus.Items.AddRange(new[] { "BelumMulai", "SedangDikerjakan", "Selesai", "Terlewat" });
            cmbStatus.SelectedIndex = 0;
        }

        // Method to validate the task ID input
        private bool IsTaskIdValid(string id)
        {
            // Validasi: tidak kosong, hanya angka, panjang maksimal 6 digit, dan > 0
            if (string.IsNullOrWhiteSpace(id))
                return false;
            if (!Regex.IsMatch(id, @"^\d{1,6}$"))
                return false;
            if (!int.TryParse(id, out int idNum) || idNum <= 0)
                return false;
            return true;
        }

        // Event handler for form load to set focus on the task ID input
        private void BtnKembali_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Event handler for the save button to validate input and save changes
        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (!IsTaskIdValid(txtIdTask.Text))
            {
                MessageBox.Show("Id tugas tidak boleh kosong dan harus berupa angka positif maksimal 6 digit.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtIdTask.Focus();
                return;
            }

            try
            {
                // Jika ada proses simpan ke file/database, lakukan di sini dan tangani exception
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception)
            {
                // Jangan tampilkan detail exception ke user
                MessageBox.Show("Terjadi kesalahan sistem. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application_GUI.src.View
{
    public partial class CreateTaskForm: Form
    {
        // Properties to access the input values from the form
        public string JudulTugas => txtJudul.Text.Trim();
        
        // Deadline is a DateTime property that returns the value from the DateTimePicker
        public DateTime Deadline => dtpDeadline.Value;
        
        // KategoriIndex is an integer property that returns the selected index of the ComboBox
        public int KategoriIndex => cmbKategori.SelectedIndex;

        // Constructor initializes the form and populates the category ComboBox
        public CreateTaskForm()
        {
            InitializeComponent();
            InisialisasiKategori();
        }
        
        // Method to initialize the category ComboBox with predefined categories
        private void InisialisasiKategori()
        {
            cmbKategori.Items.Add("Akademik");
            cmbKategori.Items.Add("NonAkademik");
            cmbKategori.SelectedIndex = 0;
        }

        // Event handler for the form load event
        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtJudul.Text))
            {
                MessageBox.Show("Judul tugas tidak boleh kosong.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtJudul.Focus();
                return;
            }

            if (dtpDeadline.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("Tanggal deadline tidak boleh kurang dari hari ini.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDeadline.Focus();
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Event handler for the "Bersihkan" button to clear the input fields
        private void btnBersihkan_Click(object sender, EventArgs e)
        {
            txtJudul.Clear();
            dtpDeadline.Value = DateTime.Now;
            cmbKategori.SelectedIndex = 0;
            txtJudul.Focus();
        }

        // Event handler for the "Kembali" button to close the form without saving
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

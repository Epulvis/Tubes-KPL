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
        public string JudulTugas => txtJudul.Text.Trim();
        public DateTime Deadline => dtpDeadline.Value;
        public int KategoriIndex => cmbKategori.SelectedIndex;

        public CreateTaskForm()
        {
            InitializeComponent();
            InisialisasiKategori();
        }

        private void InisialisasiKategori()
        {
            cmbKategori.Items.Add("Akademik");
            cmbKategori.Items.Add("NonAkademik");
            cmbKategori.SelectedIndex = 0;
        }

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

        private void btnBersihkan_Click(object sender, EventArgs e)
        {
            txtJudul.Clear();
            dtpDeadline.Value = DateTime.Now;
            cmbKategori.SelectedIndex = 0;
            txtJudul.Focus();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

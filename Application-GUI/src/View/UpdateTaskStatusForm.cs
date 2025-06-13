namespace Application_GUI.src.View
{
    public partial class UpdateTaskStatusForm : Form
    {
            public string IdTask => txtIdTask.Text.Trim();
            public int StatusIndex => cmbStatus.SelectedIndex;

            public UpdateTaskStatusForm()
            {
                InitializeComponent();
                InisialisasiStatus();
            }

            private void InisialisasiStatus()
            {
                cmbStatus.Items.Add("BelumMulai");
                cmbStatus.Items.Add("SedangDikerjakan");
                cmbStatus.Items.Add("Selesai");
                cmbStatus.Items.Add("Terlewat");
                cmbStatus.SelectedIndex = 0;
            }

            private void btnSimpan_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrWhiteSpace(txtIdTask.Text))
                {
                    MessageBox.Show("Id tugas tidak boleh kosong.", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtIdTask.Focus();
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            private void btnKembali_Click(object sender, EventArgs e)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }

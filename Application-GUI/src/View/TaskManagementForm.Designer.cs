namespace Application.View;

partial class TaskManagementForm
{

    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private Panel panelSidebar;
    private Button buttonHamburger;
    private Button buttonHome;
    private Button buttonCloseSidebar;
    private Button buttonSettings;
    private DataGridViewButtonColumn detailButton;
    private Button buttonAdd;

//    partial class TaskManagementForm
//{

//    /// <summary>
//    ///  Required designer variable.
//    /// </summary>
//    private System.ComponentModel.IContainer components = null;
//    private Panel panelSidebar;
//    private Button buttonHamburger;
//    private Button buttonHome;
//    private Button buttonCloseSidebar;
//    private Button buttonSettings;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        buttonHome = new Button();
        buttonSettings = new Button();
        btnPageFilterTaskByDate = new Button();
        buttonCloseSidebar = new Button();
        buttonAdd = new Button();
        detailButton = new DataGridViewButtonColumn();
        buttonHamburger = new Button();
        flowLayoutPanel1 = new FlowLayoutPanel();
        dataGridView1 = new DataGridView();
        label1 = new Label();
        panelSidebar.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.GhostWhite;
        panelSidebar.Controls.Add(buttonHome);
        panelSidebar.Controls.Add(buttonSettings);
        panelSidebar.Controls.Add(btnPageFilterTaskByDate);
        panelSidebar.Controls.Add(buttonCloseSidebar);
        panelSidebar.Controls.Add(buttonAdd);
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Margin = new Padding(2, 3, 2, 3);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(160, 384);
        panelSidebar.TabIndex = 0;
        panelSidebar.Visible = false;
        // 
        // buttonHome
        // 
        buttonHome.BackColor = Color.LightCyan;
        buttonHome.ForeColor = SystemColors.ControlText;
        buttonHome.Location = new Point(8, 100);
        buttonHome.Margin = new Padding(2, 3, 2, 3);
        buttonHome.Name = "buttonHome";
        buttonHome.Size = new Size(147, 32);
        buttonHome.TabIndex = 0;
        buttonHome.Text = "Delete Task";
        buttonHome.UseVisualStyleBackColor = false;
        buttonHome.Click += buttonHome_Click;
        // 
        // buttonSettingsUpdate!!!
        // 
        buttonSettings.BackColor = Color.LightCyan;
        buttonSettings.Location = new Point(8, 145);
        buttonSettings.Margin = new Padding(2, 3, 2, 3);
        buttonSettings.Name = "buttonSettings";
        buttonSettings.Size = new Size(147, 32);
        buttonSettings.TabIndex = 0;
        buttonSettings.Text = "Update Task";
        buttonSettings.UseVisualStyleBackColor = false;
        buttonSettings.Click += buttonSettings_Click;
        // 
        // btnPageFilterTaskByDate
        // 
        btnPageFilterTaskByDate.BackColor = Color.LightCyan;
        btnPageFilterTaskByDate.Location = new Point(8, 192);
        btnPageFilterTaskByDate.Margin = new Padding(2, 3, 2, 3);
        btnPageFilterTaskByDate.Name = "btnPageFilterTaskByDate";
        btnPageFilterTaskByDate.Size = new Size(147, 32);
        btnPageFilterTaskByDate.TabIndex = 0;
        btnPageFilterTaskByDate.Text = "Filter Tanggal Tugas ";
        btnPageFilterTaskByDate.UseVisualStyleBackColor = false;
        btnPageFilterTaskByDate.Click += button1_Click;
        // 
        // buttonCloseSidebar
        // 
        buttonCloseSidebar.BackColor = Color.Transparent;
        buttonCloseSidebar.FlatAppearance.BorderSize = 0;
        buttonCloseSidebar.FlatStyle = FlatStyle.Flat;
        buttonCloseSidebar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        buttonCloseSidebar.ForeColor = Color.Black;
        buttonCloseSidebar.Location = new Point(123, 3);
        buttonCloseSidebar.Margin = new Padding(2, 3, 2, 3);
        buttonCloseSidebar.Name = "buttonCloseSidebar";
        buttonCloseSidebar.Size = new Size(32, 32);
        buttonCloseSidebar.TabIndex = 2;
        buttonCloseSidebar.Text = "X";
        buttonCloseSidebar.UseVisualStyleBackColor = false;
        buttonCloseSidebar.Click += ButtonCloseSidebar_Click;
        // 
        // buttonAdd
        // 
        buttonAdd.BackColor = Color.LightCyan;
        buttonAdd.Location = new Point(8, 53);
        buttonAdd.Margin = new Padding(2, 3, 2, 3);
        buttonAdd.Name = "buttonAdd";
        buttonAdd.Size = new Size(147, 32);
        buttonAdd.TabIndex = 0;
        buttonAdd.Text = "Add Task";
        buttonAdd.UseVisualStyleBackColor = false;
        // 
        // detailButton
        // 
        detailButton.HeaderText = "Detail";
        detailButton.MinimumWidth = 6;
        detailButton.Name = "detailButton";
        detailButton.Text = "🔍";
        detailButton.UseColumnTextForButtonValue = true;
        detailButton.Width = 125;
        // 
        // buttonHamburger
        // 
        buttonHamburger.Font = new Font("Segoe UI", 14F);
        buttonHamburger.Location = new Point(8, 8);
        buttonHamburger.Margin = new Padding(2, 3, 2, 3);
        buttonHamburger.Name = "buttonHamburger";
        buttonHamburger.Size = new Size(32, 32);
        buttonHamburger.TabIndex = 1;
        buttonHamburger.Text = "☰";
        buttonHamburger.Click += ButtonHamburger_Click;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(dataGridView1);
        flowLayoutPanel1.Location = new Point(0, 41);
        flowLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(638, 343);
        flowLayoutPanel1.TabIndex = 2;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { detailButton });
        dataGridView1.Location = new Point(3, 4);
        dataGridView1.Margin = new Padding(3, 4, 3, 4);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersWidth = 51;
        dataGridView1.RowTemplate.Height = 24;
        dataGridView1.Size = new Size(634, 339);
        dataGridView1.TabIndex = 0;
        dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 12F);
        label1.Location = new Point(197, 5);
        label1.Name = "label1";
        label1.Size = new Size(269, 28);
        label1.TabIndex = 3;
        label1.Text = "Manajemen Tugas Mahasiswa";
        // 
        // TaskManagementForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(640, 383);
        Controls.Add(label1);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(panelSidebar);
        Controls.Add(buttonHamburger);
        Margin = new Padding(2, 3, 2, 3);
        Name = "TaskManagementForm";
        Text = "Aplikasi Manajemen Tugas";
        panelSidebar.ResumeLayout(false);
        flowLayoutPanel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button btnPageFilterTaskByDate;
    private FlowLayoutPanel flowLayoutPanel1;
    private DataGridView dataGridView1;
    private Label label1;
}

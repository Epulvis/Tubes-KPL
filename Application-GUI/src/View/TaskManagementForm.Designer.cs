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
        panelSidebar.Margin = new Padding(2);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(140, 288);
        panelSidebar.TabIndex = 0;
        panelSidebar.Visible = false;
        // 
        // buttonHome
        // 
        buttonHome.BackColor = Color.LightCyan;
        buttonHome.ForeColor = SystemColors.ControlText;
        buttonHome.Location = new Point(7, 75);
        buttonHome.Margin = new Padding(2);
        buttonHome.Name = "buttonHome";
        buttonHome.Size = new Size(129, 24);
        buttonHome.TabIndex = 0;
        buttonHome.Text = "Delete Task";
        buttonHome.UseVisualStyleBackColor = false;
        buttonHome.Click += buttonHome_Click;
        // 
        // buttonSettings
        // 
        buttonSettings.BackColor = Color.LightCyan;
        buttonSettings.Location = new Point(7, 109);
        buttonSettings.Margin = new Padding(2);
        buttonSettings.Name = "buttonSettings";
        buttonSettings.Size = new Size(129, 24);
        buttonSettings.TabIndex = 0;
        buttonSettings.Text = "Update Task";
        buttonSettings.UseVisualStyleBackColor = false;
        // 
        // btnPageFilterTaskByDate
        // 
        btnPageFilterTaskByDate.BackColor = Color.LightCyan;
        btnPageFilterTaskByDate.Location = new Point(7, 144);
        btnPageFilterTaskByDate.Margin = new Padding(2);
        btnPageFilterTaskByDate.Name = "btnPageFilterTaskByDate";
        btnPageFilterTaskByDate.Size = new Size(129, 24);
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
        buttonCloseSidebar.Location = new Point(108, 2);
        buttonCloseSidebar.Margin = new Padding(2);
        buttonCloseSidebar.Name = "buttonCloseSidebar";
        buttonCloseSidebar.Size = new Size(28, 24);
        buttonCloseSidebar.TabIndex = 2;
        buttonCloseSidebar.Text = "X";
        buttonCloseSidebar.UseVisualStyleBackColor = false;
        buttonCloseSidebar.Click += ButtonCloseSidebar_Click;
        // 
        // buttonAdd
        // 
        buttonAdd.BackColor = Color.LightCyan;
        buttonAdd.Location = new Point(7, 40);
        buttonAdd.Margin = new Padding(2);
        buttonAdd.Name = "buttonAdd";
        buttonAdd.Size = new Size(129, 24);
        buttonAdd.TabIndex = 0;
        buttonAdd.Text = "Add Task";
        buttonAdd.UseVisualStyleBackColor = false;
        // 
        // detailButton
        // 
        detailButton.HeaderText = "Detail";
        detailButton.Name = "detailButton";
        detailButton.Text = "🔍";
        detailButton.UseColumnTextForButtonValue = true;
        // 
        // buttonHamburger
        // 
        buttonHamburger.Font = new Font("Segoe UI", 14F);
        buttonHamburger.Location = new Point(7, 6);
        buttonHamburger.Margin = new Padding(2);
        buttonHamburger.Name = "buttonHamburger";
        buttonHamburger.Size = new Size(28, 24);
        buttonHamburger.TabIndex = 1;
        buttonHamburger.Text = "☰";
        buttonHamburger.Click += ButtonHamburger_Click;
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.Controls.Add(dataGridView1);
        flowLayoutPanel1.Location = new Point(0, 31);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new Size(558, 257);
        flowLayoutPanel1.TabIndex = 2;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(new DataGridViewColumn[] { detailButton });
        dataGridView1.Location = new Point(3, 3);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowTemplate.Height = 24;
        dataGridView1.Size = new Size(555, 254);
        dataGridView1.TabIndex = 0;
        dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Font = new Font("Segoe UI", 12F);
        label1.Location = new Point(172, 4);
        label1.Name = "label1";
        label1.Size = new Size(218, 21);
        label1.TabIndex = 3;
        label1.Text = "Manajemen Tugas Mahasiswa";
        // 
        // TaskManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(560, 287);
        Controls.Add(panelSidebar);
        Controls.Add(label1);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(buttonHamburger);
        Margin = new Padding(2);
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

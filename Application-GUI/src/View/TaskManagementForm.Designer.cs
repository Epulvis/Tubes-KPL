using System.Drawing;
using System.Windows.Forms;

namespace Application.View;

partial class TaskManagementForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel panelSidebar;
    private Button buttonHamburger;
    private Button buttonHome;
    private Button buttonCloseSidebar;
    private Button btnShowUpdateForm;
    private Button btnPageFilterTaskByDate;
    private Button btnShowAddTaskForm;
    private Label label1;
    private TableLayoutPanel mainLayout;
    private DataGridView dgvTasks;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        btnShowAddTaskForm = new Button();
        btnShowUpdateForm = new Button();
        btnPageFilterTaskByDate = new Button();
        buttonHome = new Button();
        buttonCloseSidebar = new Button();
        buttonHamburger = new Button();
        mainLayout = new TableLayoutPanel();
        label1 = new Label();
        dgvTasks = new DataGridView();
        panelSidebar.SuspendLayout();
        mainLayout.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTasks).BeginInit();
        SuspendLayout();
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.GhostWhite;
        panelSidebar.Controls.Add(btnShowAddTaskForm);
        panelSidebar.Controls.Add(btnShowUpdateForm);
        panelSidebar.Controls.Add(btnPageFilterTaskByDate);
        panelSidebar.Controls.Add(buttonHome);
        panelSidebar.Controls.Add(buttonCloseSidebar);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Margin = new Padding(3, 2, 3, 2);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(140, 338);
        panelSidebar.TabIndex = 0;
        panelSidebar.Visible = false;
        // 
        // btnShowAddTaskForm
        // 
        btnShowAddTaskForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnShowAddTaskForm.BackColor = Color.LightCyan;
        btnShowAddTaskForm.Location = new Point(7, 40);
        btnShowAddTaskForm.Margin = new Padding(3, 2, 3, 2);
        btnShowAddTaskForm.Name = "btnShowAddTaskForm";
        btnShowAddTaskForm.Size = new Size(126, 24);
        btnShowAddTaskForm.TabIndex = 0;
        btnShowAddTaskForm.Text = "Add Task";
        btnShowAddTaskForm.UseVisualStyleBackColor = false;
        // 
        // btnShowUpdateForm
        // 
        btnShowUpdateForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnShowUpdateForm.BackColor = Color.LightCyan;
        btnShowUpdateForm.Location = new Point(7, 75);
        btnShowUpdateForm.Margin = new Padding(3, 2, 3, 2);
        btnShowUpdateForm.Name = "btnShowUpdateForm";
        btnShowUpdateForm.Size = new Size(126, 24);
        btnShowUpdateForm.TabIndex = 1;
        btnShowUpdateForm.Text = "Update Task";
        btnShowUpdateForm.UseVisualStyleBackColor = false;
        // 
        // btnPageFilterTaskByDate
        // 
        btnPageFilterTaskByDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnPageFilterTaskByDate.BackColor = Color.LightCyan;
        btnPageFilterTaskByDate.Location = new Point(7, 110);
        btnPageFilterTaskByDate.Margin = new Padding(3, 2, 3, 2);
        btnPageFilterTaskByDate.Name = "btnPageFilterTaskByDate";
        btnPageFilterTaskByDate.Size = new Size(126, 24);
        btnPageFilterTaskByDate.TabIndex = 2;
        btnPageFilterTaskByDate.Text = "Filter Tanggal Tugas";
        btnPageFilterTaskByDate.UseVisualStyleBackColor = false;
        // 
        // buttonHome
        // 
        buttonHome.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        buttonHome.BackColor = Color.LightCyan;
        buttonHome.Location = new Point(7, 146);
        buttonHome.Margin = new Padding(3, 2, 3, 2);
        buttonHome.Name = "buttonHome";
        buttonHome.Size = new Size(126, 24);
        buttonHome.TabIndex = 3;
        buttonHome.Text = "Delete Task";
        buttonHome.UseVisualStyleBackColor = false;
        // 
        // buttonCloseSidebar
        // 
        buttonCloseSidebar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        buttonCloseSidebar.BackColor = Color.Transparent;
        buttonCloseSidebar.FlatAppearance.BorderSize = 0;
        buttonCloseSidebar.FlatStyle = FlatStyle.Flat;
        buttonCloseSidebar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        buttonCloseSidebar.ForeColor = Color.Black;
        buttonCloseSidebar.Location = new Point(105, 2);
        buttonCloseSidebar.Margin = new Padding(3, 2, 3, 2);
        buttonCloseSidebar.Name = "buttonCloseSidebar";
        buttonCloseSidebar.Size = new Size(28, 24);
        buttonCloseSidebar.TabIndex = 4;
        buttonCloseSidebar.Text = "X";
        buttonCloseSidebar.UseVisualStyleBackColor = false;
        // 
        // buttonHamburger
        // 
        buttonHamburger.Font = new Font("Segoe UI", 14F);
        buttonHamburger.Location = new Point(7, 6);
        buttonHamburger.Margin = new Padding(3, 2, 3, 2);
        buttonHamburger.Name = "buttonHamburger";
        buttonHamburger.Size = new Size(26, 22);
        buttonHamburger.TabIndex = 3;
        buttonHamburger.Text = "☰";
        // 
        // mainLayout
        // 
        mainLayout.ColumnCount = 1;
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        mainLayout.Controls.Add(label1, 0, 0);
        mainLayout.Controls.Add(dgvTasks, 0, 1);
        mainLayout.Dock = DockStyle.Fill;
        mainLayout.Location = new Point(0, 0);
        mainLayout.Margin = new Padding(3, 2, 3, 2);
        mainLayout.Name = "mainLayout";
        mainLayout.Padding = new Padding(9, 8, 9, 8);
        mainLayout.RowCount = 2;
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainLayout.Size = new Size(700, 338);
        mainLayout.TabIndex = 2;
        // 
        // label1
        // 
        label1.Dock = DockStyle.Fill;
        label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        label1.Location = new Point(12, 8);
        label1.Name = "label1";
        label1.Size = new Size(676, 36);
        label1.TabIndex = 3;
        label1.Text = "Manajemen Tugas Mahasiswa";
        label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dgvTasks
        // 
        dgvTasks.AllowUserToAddRows = false;
        dgvTasks.AllowUserToDeleteRows = false;
        dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvTasks.Dock = DockStyle.Fill;
        dgvTasks.Font = new Font("Segoe UI", 10F);
        dgvTasks.Location = new Point(12, 46);
        dgvTasks.Margin = new Padding(3, 2, 3, 2);
        dgvTasks.Name = "dgvTasks";
        dgvTasks.ReadOnly = true;
        dgvTasks.RowHeadersVisible = false;
        dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTasks.Size = new Size(676, 282);
        dgvTasks.TabIndex = 4;
        // 
        // TaskManagementForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(700, 338);
        Controls.Add(panelSidebar);
        Controls.Add(mainLayout);
        Controls.Add(buttonHamburger);
        Margin = new Padding(3, 2, 3, 2);
        Name = "TaskManagementForm";
        Text = "Aplikasi Manajemen Tugas";
        panelSidebar.ResumeLayout(false);
        mainLayout.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvTasks).EndInit();
        buttonHamburger.Location = new Point(5, 5);
        buttonHamburger.BringToFront();
        ResumeLayout(false);
    }
}

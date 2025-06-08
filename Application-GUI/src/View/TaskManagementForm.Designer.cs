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
        private void InitializeComponent()
        {
            btnPageFilterTaskByDate = new Button();
            SuspendLayout();
            // 
            // btnPageFilterTaskByDate
            // 
            btnPageFilterTaskByDate.Location = new Point(46, 91);
            btnPageFilterTaskByDate.Name = "btnPageFilterTaskByDate";
            btnPageFilterTaskByDate.Size = new Size(181, 34);
            btnPageFilterTaskByDate.TabIndex = 0;
            btnPageFilterTaskByDate.Text = "Filter Tanggal Tugas ";
            btnPageFilterTaskByDate.UseVisualStyleBackColor = true;
            btnPageFilterTaskByDate.Click += button1_Click;
            // 
            // TaskManagementForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPageFilterTaskByDate);
            Name = "TaskManagementForm";
            Text = "Aplikasi Manajemen Tugas";
            ResumeLayout(false);
        }
    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        buttonHome = new Button();
        buttonSettings = new Button();
        buttonCloseSidebar = new Button();
        buttonHamburger = new Button();
        panelSidebar.SuspendLayout();
        SuspendLayout();
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.Bisque;
        panelSidebar.Controls.Add(buttonHome);
        panelSidebar.Controls.Add(buttonSettings);
        panelSidebar.Controls.Add(buttonCloseSidebar);
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(200, 563);
        panelSidebar.TabIndex = 0;
        panelSidebar.Visible = false;
        // 
        // buttonHome
        // 
        buttonHome.BackColor = Color.FromArgb(60, 60, 60);
        buttonHome.FlatStyle = FlatStyle.Flat;
        buttonHome.FlatAppearance.BorderSize = 0;
        buttonHome.ForeColor = Color.White;
        buttonHome.Location = new Point(10, 60);
        buttonHome.Name = "buttonHome";
        buttonHome.Size = new Size(180, 40);
        buttonHome.TabIndex = 3;
        buttonHome.Text = "Home";
        buttonHome.UseVisualStyleBackColor = false;
        // 
        // buttonSettings
        // 
        buttonSettings.BackColor = Color.FromArgb(60, 60, 60);
        buttonHome.FlatStyle = FlatStyle.Flat;
        buttonHome.FlatAppearance.BorderSize = 0;
        buttonSettings.ForeColor = Color.White;
        buttonSettings.Location = new Point(10, 110);
        buttonSettings.Name = "buttonSettings";
        buttonSettings.Size = new Size(180, 40);
        buttonSettings.TabIndex = 3;
        buttonSettings.Text = "Settings";
        buttonSettings.UseVisualStyleBackColor = false;
        // 
        // buttonCloseSidebar
        // 
        buttonCloseSidebar.BackColor = Color.Transparent;
        buttonCloseSidebar.FlatAppearance.BorderSize = 0;
        buttonCloseSidebar.FlatStyle = FlatStyle.Flat;
        buttonCloseSidebar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        buttonCloseSidebar.ForeColor = Color.Black;
        buttonCloseSidebar.Location = new Point(155, 5);
        buttonCloseSidebar.Name = "buttonCloseSidebar";
        buttonCloseSidebar.Size = new Size(40, 40);
        buttonCloseSidebar.TabIndex = 2;
        buttonCloseSidebar.Text = "X";
        buttonCloseSidebar.UseVisualStyleBackColor = false;
        buttonCloseSidebar.Click += ButtonCloseSidebar_Click;
        // 
        // buttonHamburger
        // 
        buttonHamburger.Font = new Font("Segoe UI", 14F);
        buttonHamburger.Location = new Point(10, 10);
        buttonHamburger.Name = "buttonHamburger";
        buttonHamburger.Size = new Size(40, 40);
        buttonHamburger.TabIndex = 1;
        buttonHamburger.Text = "☰";
        buttonHamburger.Click += ButtonHamburger_Click;
        // 
        // TaskManagementForm
        // 
        ClientSize = new Size(784, 561);
        Controls.Add(panelSidebar);
        Controls.Add(buttonHamburger);
        Name = "TaskManagementForm";
        Text = "Hamburger Menu Demo";
        panelSidebar.ResumeLayout(false);
        ResumeLayout(false);
    }

        #endregion

        private Button btnPageFilterTaskByDate;
    }
}

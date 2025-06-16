namespace Application_GUI.src.View
{
    partial class DeleteTask
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewTasks;
        private Label lblTitle;
        private Label lblInput;
        private TextBox txtTaskId;
        private Button btnContinue;
        private Button btnBack;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewTasks = new DataGridView();
            this.lblTitle = new Label();
            this.lblInput = new Label();
            this.txtTaskId = new TextBox();
            this.btnContinue = new Button();
            this.btnBack = new Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Text = "Delete Task";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Size = new System.Drawing.Size(776, 40);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTasks
            // 
            this.dataGridViewTasks.Location = new System.Drawing.Point(12, 60);
            this.dataGridViewTasks.Size = new System.Drawing.Size(776, 250);
            this.dataGridViewTasks.ReadOnly = true;
            this.dataGridViewTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTasks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.dataGridViewTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTasks.BorderStyle = BorderStyle.FixedSingle;
            this.dataGridViewTasks.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            this.dataGridViewTasks.EnableHeadersVisualStyles = false;
            // 
            // lblInput
            // 
            this.lblInput.Text = "Enter Task ID to delete:";
            this.lblInput.Location = new System.Drawing.Point(12, 330);
            this.lblInput.Size = new System.Drawing.Size(200, 30);
            // 
            // txtTaskId
            // 
            this.txtTaskId.Location = new System.Drawing.Point(220, 330);
            this.txtTaskId.Size = new System.Drawing.Size(150, 31);
            // 
            // btnContinue
            // 
            this.btnContinue.Text = "Continue";
            this.btnContinue.Location = new System.Drawing.Point(400, 330);
            this.btnContinue.Size = new System.Drawing.Size(100, 31);
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // btnBack
            // 
            this.btnBack.Text = "Back";
            this.btnBack.Location = new System.Drawing.Point(520, 330);
            this.btnBack.Size = new System.Drawing.Size(100, 31);
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // DeleteTask
            // 
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dataGridViewTasks);
            this.Controls.Add(this.lblInput);
            this.Controls.Add(this.txtTaskId);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.btnBack);
            this.Name = "DeleteTask";
            this.Text = "Delete Task";
            this.Load += new System.EventHandler(this.DeleteTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
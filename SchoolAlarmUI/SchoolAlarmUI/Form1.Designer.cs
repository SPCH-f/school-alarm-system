namespace SchoolAlarmUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblClock = new Label();
            dgvSchedule = new DataGridView();
            notifyIcon1 = new NotifyIcon(components);
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            บนทกขอมลToolStripMenuItem = new ToolStripMenuItem();
            alarmToolStripMenuItem = new ToolStripMenuItem();
            vToolStripMenuItem = new ToolStripMenuItem();
            ลบกจกรรมToolStripMenuItem = new ToolStripMenuItem();
            เพมกจกรรมToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            เกยวกบโปรแกรมToolStripMenuItem = new ToolStripMenuItem();
            เปดโปรแกรมอตโนมตToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblClock
            // 
            lblClock.AutoSize = true;
            lblClock.Font = new Font("Leelawadee UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblClock.ForeColor = SystemColors.ActiveCaptionText;
            lblClock.Location = new Point(449, 46);
            lblClock.Margin = new Padding(4, 0, 4, 0);
            lblClock.Name = "lblClock";
            lblClock.Size = new Size(106, 38);
            lblClock.TabIndex = 0;
            lblClock.Text = "นาฬิกา";
            // 
            // dgvSchedule
            // 
            dgvSchedule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedule.BackgroundColor = SystemColors.ButtonFace;
            dgvSchedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSchedule.GridColor = SystemColors.GrayText;
            dgvSchedule.Location = new Point(36, 104);
            dgvSchedule.Margin = new Padding(4);
            dgvSchedule.Name = "dgvSchedule";
            dgvSchedule.RowHeadersWidth = 51;
            dgvSchedule.Size = new Size(953, 461);
            dgvSchedule.TabIndex = 1;
            dgvSchedule.CellFormatting += dgvSchedule_CellFormatting;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.ActiveBorder;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, alarmToolStripMenuItem, optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1030, 28);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { บนทกขอมลToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // บนทกขอมลToolStripMenuItem
            // 
            บนทกขอมลToolStripMenuItem.Name = "บนทกขอมลToolStripMenuItem";
            บนทกขอมลToolStripMenuItem.Size = new Size(161, 26);
            บนทกขอมลToolStripMenuItem.Text = "บันทึกข้อมูล";
            บนทกขอมลToolStripMenuItem.Click += บนทกขอมลToolStripMenuItem_Click;
            // 
            // alarmToolStripMenuItem
            // 
            alarmToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { vToolStripMenuItem, ลบกจกรรมToolStripMenuItem, เพมกจกรรมToolStripMenuItem });
            alarmToolStripMenuItem.Name = "alarmToolStripMenuItem";
            alarmToolStripMenuItem.Size = new Size(63, 24);
            alarmToolStripMenuItem.Text = "Alarm";
            // 
            // vToolStripMenuItem
            // 
            vToolStripMenuItem.Name = "vToolStripMenuItem";
            vToolStripMenuItem.Size = new Size(224, 26);
            vToolStripMenuItem.Text = "เพิ่มกิจกรรม";
            vToolStripMenuItem.Click += vToolStripMenuItem_Click;
            // 
            // ลบกจกรรมToolStripMenuItem
            // 
            ลบกจกรรมToolStripMenuItem.Name = "ลบกจกรรมToolStripMenuItem";
            ลบกจกรรมToolStripMenuItem.Size = new Size(224, 26);
            ลบกจกรรมToolStripMenuItem.Text = "ลบกิจกรรม";
            ลบกจกรรมToolStripMenuItem.Click += ลบกจกรรมToolStripMenuItem_Click;
            // 
            // เพมกจกรรมToolStripMenuItem
            // 
            เพมกจกรรมToolStripMenuItem.Name = "เพมกจกรรมToolStripMenuItem";
            เพมกจกรรมToolStripMenuItem.Size = new Size(224, 26);
            เพมกจกรรมToolStripMenuItem.Text = "ทดสอบเสียง";
            เพมกจกรรมToolStripMenuItem.Click += ทดสอบเสยงToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { เกยวกบโปรแกรมToolStripMenuItem, เปดโปรแกรมอตโนมตToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(75, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // เกยวกบโปรแกรมToolStripMenuItem
            // 
            เกยวกบโปรแกรมToolStripMenuItem.Name = "เกยวกบโปรแกรมToolStripMenuItem";
            เกยวกบโปรแกรมToolStripMenuItem.Size = new Size(214, 26);
            เกยวกบโปรแกรมToolStripMenuItem.Text = "เกี่ยวกับโปรแกรม";
            เกยวกบโปรแกรมToolStripMenuItem.Click += เกยวกบโปรแกรมToolStripMenuItem_Click;
            // 
            // เปดโปรแกรมอตโนมตToolStripMenuItem
            // 
            เปดโปรแกรมอตโนมตToolStripMenuItem.CheckOnClick = true;
            เปดโปรแกรมอตโนมตToolStripMenuItem.Name = "เปดโปรแกรมอตโนมตToolStripMenuItem";
            เปดโปรแกรมอตโนมตToolStripMenuItem.Size = new Size(214, 26);
            เปดโปรแกรมอตโนมตToolStripMenuItem.Text = "เปิดโปรแกรมอัตโนมัติ";
            เปดโปรแกรมอตโนมตToolStripMenuItem.Click += เปดโปรแกรมอตโนมตToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1030, 609);
            Controls.Add(dgvSchedule);
            Controls.Add(lblClock);
            Controls.Add(menuStrip1);
            Cursor = Cursors.Hand;
            Font = new Font("Leelawadee UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "ระบบแจ้งเตือน";
            FormClosing += Form1_FormClosing;
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)dgvSchedule).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblClock;
        private DataGridView dgvSchedule;
        private NotifyIcon notifyIcon1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem บนทกขอมลToolStripMenuItem;
        private ToolStripMenuItem alarmToolStripMenuItem;
        private ToolStripMenuItem vToolStripMenuItem;
        private ToolStripMenuItem ลบกจกรรมToolStripMenuItem;
        private ToolStripMenuItem เพมกจกรรมToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem เกยวกบโปรแกรมToolStripMenuItem;
        private ToolStripMenuItem เปดโปรแกรมอตโนมตToolStripMenuItem;
    }
}
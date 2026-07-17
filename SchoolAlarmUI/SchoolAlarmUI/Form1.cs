using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Media;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Win32;

namespace SchoolAlarmUI
{

    public partial class Form1 : Form
    {
        List<ScheduleItem> scheduleList = new List<ScheduleItem>();
        string jsonFilePath = "schedule.json";

        public Form1()
        {
            InitializeComponent();
            // ใช้ไอคอนพื้นฐานของระบบเพื่อไม่ให้ Error
            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.Text = "ระบบแจ้งเตือนโรงเรียน (กำลังทำงาน)";

            // ตั้งค่า DataGridView
            dgvSchedule.ColumnCount = 3;
            dgvSchedule.Columns[0].Name = "เวลา";
            dgvSchedule.Columns[1].Name = "กิจกรรม";
            dgvSchedule.Columns[2].Name = "ไฟล์เสียง";
            dgvSchedule.Columns[2].ReadOnly = true; // ให้เลือกผ่าน DoubleClick เท่านั้น

            LoadSchedule();

            // ตั้งค่า Timer
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();

            ApplyOfficialTheme();
        }

        private void LoadSchedule()
        {
            try
            {
                if (File.Exists(jsonFilePath))
                {
                    string jsonString = File.ReadAllText(jsonFilePath);
                    // ถ้าไฟล์ว่างหรือมีปัญหา ให้ใช้ List ว่างเปล่าแทนการหยุดทำงาน
                    scheduleList = JsonSerializer.Deserialize<List<ScheduleItem>>(jsonString) ?? new List<ScheduleItem>();

                    dgvSchedule.Rows.Clear();
                    foreach (var item in scheduleList)
                    {
                        dgvSchedule.Rows.Add(item.Time, item.Activity, item.SoundPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไฟล์ข้อมูลเสียหาย ขออนุญาตเริ่มใหม่นะครับ: " + ex.Message);
                scheduleList = new List<ScheduleItem>(); // รีเซ็ตให้เป็นลิสต์ว่าง
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            lblClock.Text = currentTime;

            foreach (DataGridViewRow row in dgvSchedule.Rows)
            {
                if (row.IsNewRow) continue; // ข้ามแถวว่างท้ายตาราง

                if (row.Cells[0].Value?.ToString() == currentTime)
                {
                    string activity = row.Cells[1].Value?.ToString() ?? "แจ้งเตือน";
                    string audioPath = row.Cells[2].Value?.ToString() ?? "";

                    if (File.Exists(audioPath))
                    {
                        try
                        {
                            SoundPlayer player = new SoundPlayer(audioPath);
                            player.Play();
                        }
                        catch { }
                    }
                    MessageBox.Show(activity, "🔔 ถึงเวลาแล้ว!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            scheduleList.Clear();
            foreach (DataGridViewRow row in dgvSchedule.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value != null)
                {
                    scheduleList.Add(new ScheduleItem
                    {
                        Time = row.Cells[0].Value.ToString()!,
                        Activity = row.Cells[1].Value?.ToString() ?? "",
                        SoundPath = row.Cells[2].Value?.ToString() ?? ""
                    });
                }
            }
            string jsonString = JsonSerializer.Serialize(scheduleList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonFilePath, jsonString);
            MessageBox.Show("บันทึกตารางสอนสำเร็จ!", "Success");
        }

        private void dgvSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Wave files (*.wav)|*.wav";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        dgvSchedule.Rows[e.RowIndex].Cells[2].Value = ofd.FileName;
                    }
                }
            }
        }

        public class ScheduleItem
        {
            public string Time { get; set; } = "";
            public string Activity { get; set; } = "";
            public string SoundPath { get; set; } = "";
        }

        private void btnSelectSound_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Wave files (*.wav)|*.wav";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // ถ้าเลือกผ่านปุ่มนี้ ให้เอาไฟล์ไปใส่ในแถวที่เลือกอยู่ (CurrentRow)
                    if (dgvSchedule.CurrentRow != null)
                    {
                        dgvSchedule.CurrentRow.Cells[2].Value = ofd.FileName;
                    }
                }
            }
        }

        private void ApplyOfficialTheme()
        {
            // เช็คว่าเคยตั้งค่าให้เปิดอัตโนมัติไว้ไหม ถ้าเคย ให้ติ๊กถูกที่ CheckBox รอก่อนเลย
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            if (rk.GetValue("SchoolAlarmApp") != null)
            {
            }
            // 1. สีพื้นหลัง Form และฟอนต์หลัก
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Tahoma", 10F, FontStyle.Regular);

            // 2. แต่งนาฬิกาให้เด่นและเป็นทางการ
            lblClock.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            lblClock.ForeColor = Color.Navy;

            // 3. แต่งปุ่มกดทั้งหมดให้เป็นแบบ Flat Design
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = Color.Navy;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    btn.Height = 35; // ปรับให้ปุ่มหนาขึ้นนิดหน่อย
                }
            }

            // 4. แต่งตาราง DataGridView ให้ดูสะอาดตา
            dgvSchedule.BackgroundColor = Color.White;
            dgvSchedule.BorderStyle = BorderStyle.FixedSingle;
            dgvSchedule.GridColor = Color.LightGray;

            // แต่งหัวตาราง (Header)
            dgvSchedule.EnableHeadersVisualStyles = false;
            dgvSchedule.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dgvSchedule.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvSchedule.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 10F, FontStyle.Bold);

            // ให้แถวที่ถูกเลือกเป็นสีกรมท่า
            dgvSchedule.DefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvSchedule.DefaultCellStyle.SelectionForeColor = Color.White;

            // 1. ให้ตารางขยายตัวติดขอบทั้ง 4 ด้าน (ซ้าย, ขวา, บน, ล่าง)
            dgvSchedule.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 3. ให้นาฬิกายึดติดกับ "ขอบบน" อย่างเดียว
            lblClock.Anchor = AnchorStyles.Top;
        }

        private void btnTestSound_Click(object sender, EventArgs e)
        {
            // 1. เช็คว่าผู้ใช้ได้คลิกเลือกแถวในตารางหรือยัง
            if (dgvSchedule.CurrentRow != null && !dgvSchedule.CurrentRow.IsNewRow)
            {
                // 2. ดึง Path ไฟล์เสียงจากคอลัมน์ที่ 2 (คอลัมน์ "ไฟล์เสียง") ของแถวที่เลือกอยู่
                string audioPath = dgvSchedule.CurrentRow.Cells[2].Value?.ToString() ?? "";

                // 3. เช็คว่ามีไฟล์อยู่จริงในเครื่องไหม
                if (File.Exists(audioPath))
                {
                    try
                    {
                        // สั่งเล่นเสียง
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(audioPath);
                        player.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ไม่สามารถเล่นไฟล์เสียงนี้ได้ครับ: " + ex.Message, "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("ยังไม่ได้ระบุไฟล์เสียง หรือหาไฟล์ในเครื่องไม่พบครับ!", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // ถ้าผู้ใช้กดปุ่มโดยที่ยังไม่ได้เลือกแถวไหนเลย
                MessageBox.Show("กรุณาคลิกเลือกกิจกรรมในตารางที่ต้องการทดสอบเสียงก่อนครับ", "คำแนะนำ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvSchedule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // เช็คว่ากำลังแสดงผลที่คอลัมน์ "ไฟล์เสียง" (คอลัมน์ที่ 2) ใช่หรือไม่
            if (e.ColumnIndex == 2 && e.Value != null)
            {
                string fullPath = e.Value.ToString();

                // ถ้าค่าไม่ใช่ช่องว่าง ให้ตัดเอามาเฉพาะชื่อไฟล์
                if (!string.IsNullOrEmpty(fullPath))
                {
                    // Path.GetFileName จะทำการตัด C:\... ออก เหลือแค่ชื่อไฟล์.wav
                    e.Value = System.IO.Path.GetFileName(fullPath);
                    e.FormattingApplied = true; // บอกตารางว่า "เราจัดการหน้าตาให้แล้วนะ ไม่ต้องโชว์ Path เต็ม"
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // ถ้าผู้ใช้กดย่อหน้าต่าง (-)
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide(); // ซ่อนหน้าต่างหลัก
                notifyIcon1.Visible = true; // โชว์ไอคอนมุมขวาล่าง
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show(); // โชว์หน้าต่างหลัก
            this.WindowState = FormWindowState.Normal; // คืนขนาดหน้าต่าง
            notifyIcon1.Visible = false; // ซ่อนไอคอน
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. ตรวจสอบว่าผู้ใช้คลิกเลือกแถวในตารางหรือยัง และไม่ใช่แถวว่างล่างสุด
            if (dgvSchedule.CurrentRow != null && !dgvSchedule.CurrentRow.IsNewRow)
            {
                // 2. ดึงชื่อกิจกรรมมาโชว์เพื่อถามให้แน่ใจก่อนลบ (กันคุณครูมือลั่นไปโดน)
                string activityName = dgvSchedule.CurrentRow.Cells[1].Value?.ToString() ?? "กิจกรรมนี้";

                DialogResult result = MessageBox.Show($"คุณครูแน่ใจใช่ไหมครับว่าจะลบกิจกรรม \"{activityName}\" ออกจากตาราง?",
                    "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                // 3. ถ้าคุณครูกด "ใช่" (Yes) ให้ลบแถวนั้นทิ้งทันที
                if (result == DialogResult.Yes)
                {
                    dgvSchedule.Rows.Remove(dgvSchedule.CurrentRow);
                }
            }
            else
            {
                MessageBox.Show("กรุณาคลิกเลือกแถวกิจกรรมที่ต้องการลบก่อนครับ", "คำแนะนำ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void บนทกขอมลToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // เปลี่ยนจากการกดปุ่ม เป็นการเรียกใช้ฟังก์ชันเซฟตัวเก่าแทนครับ
            btnSave_Click(sender, e);
        }

        private void ลบกจกรรมToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // เรียกใช้ฟังก์ชันลบตัวเก่า
            btnDelete_Click(sender, e);
        }

        private void เพมกจกรรมToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // เอ๊ะ! ตรงนี้คุณน่าจะก๊อปมาผิดครับ ไปเรียก TestSound เฉยเลย ฮ่าๆ
            // เอาโค้ดนี้ไปใส่แทนครับ มันคือการสั่งให้ตารางกระโดดไปบรรทัดล่างสุดเพื่อรอพิมพ์กิจกรรมใหม่ครับ
            int lastRowIndex = dgvSchedule.Rows.Count - 1;
            dgvSchedule.ClearSelection();
            dgvSchedule.CurrentCell = dgvSchedule.Rows[lastRowIndex].Cells[0];
            dgvSchedule.BeginEdit(true);
        }

        private void ทดสอบเสยงToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnTestSound_Click(sender, e);
        }

        private void เกยวกบโปรแกรมToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ระบบแจ้งเตือนตารางเรียนอัตโนมัติ\n\nพัฒนาโดย: SPCH-f\nเวอร์ชัน: 1.0 (2026)\n\nสงวนลิขสิทธิ์สำหรับการใช้งานในสถานศึกษา",
        "About School Alarm", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void vToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. หาตำแหน่งของแถวว่างล่างสุด
            int lastRowIndex = dgvSchedule.Rows.Count - 1;

            // 2. เคลียร์การเลือกแถวเดิม และสั่งให้โฟกัสไปที่แถวใหม่ช่องแรก (ช่องเวลา)
            dgvSchedule.ClearSelection();
            dgvSchedule.CurrentCell = dgvSchedule.Rows[lastRowIndex].Cells[0];

            // 3. สั่งให้ช่องนั้นอยู่ในโหมดพร้อมพิมพ์ (กระพริบรอเลย)
            dgvSchedule.BeginEdit(true);
        }

        private void เปดโปรแกรมอตโนมตToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string appName = "SchoolAlarmApp";

            // เช็คว่าเมนูนี้มีเครื่องหมายถูก (✓) อยู่หรือไม่
            // (เปลี่ยนชื่อ ToolStripMenuItem ให้ตรงกับที่คุณตั้งใน Properties นะครับ)
            if (เปดโปรแกรมอตโนมตToolStripMenuItem.Checked)
            {
                rk.SetValue(appName, Application.ExecutablePath);
            }
            else
            {
                rk.DeleteValue(appName, false);
            }
        }

        private void เลอกไฟลเสยงToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSelectSound_Click(sender, e);
        }
    }
}
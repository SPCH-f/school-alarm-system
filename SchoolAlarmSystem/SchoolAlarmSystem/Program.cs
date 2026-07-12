using System;
using System.Collections.Generic;
using System.IO; // 🌟 เพิ่ม: สำหรับจัดการการอ่าน/เขียนไฟล์
using System.Text.Json; // 🌟 เพิ่ม: สำหรับจัดการ JSON
using System.Media;
using System.Threading;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("🚀 ระบบแจ้งเตือนตารางสอน (เวอร์ชัน JSON) เริ่มทำงาน...");
Console.WriteLine("==========================================================");

string soundFilePath = @"C:\Windows\Media\Alarm01.wav";
string jsonFilePath = "schedule.json"; // กำหนดชื่อไฟล์ที่เราจะใช้เก็บตารางสอน

// 1. สร้างตัวแปร Dictionary เตรียมไว้รับข้อมูล
Dictionary<string, string> schoolSchedule = new Dictionary<string, string>();

// 2. เช็คว่ามีไฟล์ schedule.json อยู่แล้วหรือยัง?
if (File.Exists(jsonFilePath))
{
    // ถ้ามีไฟล์แล้ว: ให้อ่านข้อมูลจากไฟล์นั้นมาใช้งาน
    string jsonString = File.ReadAllText(jsonFilePath);
    schoolSchedule = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
    Console.WriteLine("✅ โหลดตารางสอนจากไฟล์ schedule.json สำเร็จ!");
}
else
{
    // ถ้ายังไม่มีไฟล์: ให้สร้างข้อมูลเริ่มต้น แล้วบันทึกเป็นไฟล์ใหม่
    Console.WriteLine("⚠️ ไม่พบไฟล์ schedule.json กำลังสร้างไฟล์ใหม่...");

    schoolSchedule = new Dictionary<string, string>()
    {
        { "08:00:00", "ถึงเวลาเข้าแถวเคารพธงชาติ" },
        { "08:30:00", "เริ่มเรียนคาบที่ 1 (คณิตศาสตร์)" },
        { "12:00:00", "พักรับประทานอาหารกลางวัน" },
        // 🌟 แก้ไขเวลาบรรทัดนี้ให้ล่วงหน้าจากเวลาของคุณสัก 1-2 นาทีเพื่อทดสอบ
        { "21:30:00", "ทดสอบระบบอ่าน JSON!" }
    };

    // แปลง Dictionary เป็นข้อความ JSON (WriteIndented = true ช่วยจัดบรรทัดให้อ่านง่าย)
    string jsonString = JsonSerializer.Serialize(schoolSchedule, new JsonSerializerOptions { WriteIndented = true });

    // เขียนไฟล์ลงเครื่อง
    File.WriteAllText(jsonFilePath, jsonString);
    Console.WriteLine("✅ สร้างไฟล์ schedule.json สำเร็จ!");
}

Console.WriteLine("----------------------------------------------------------\n");

// 3. เริ่มลูปเช็คเวลา (โค้ดส่วนแจ้งเตือนทำงานเหมือนเดิมครับ)
while (true)
{
    DateTime now = DateTime.Now;
    string currentTime = now.ToString("HH:mm:ss");

    Console.Write($"\rเวลาปัจจุบัน: {currentTime}   ");

    if (schoolSchedule.ContainsKey(currentTime))
    {
        string alertMessage = schoolSchedule[currentTime];
        Console.WriteLine($"\n\n[แจ้งเตือน] 🔔 {alertMessage}\n");

        try
        {
            SoundPlayer player = new SoundPlayer(soundFilePath);
            player.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[Error] ไม่สามารถเล่นเสียงได้: {ex.Message}");
        }
    }

    Thread.Sleep(1000);
}
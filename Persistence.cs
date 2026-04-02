using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using BambooEnterprise.Models;
using ClosedXML.Excel;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
//using iText.Layout.Element.Paragraph;
using iText.Layout.Properties;

namespace BambooEnterprise.Data
{
    public static class PersistenceManager
    {
        private static readonly string Root = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string DataDir = Path.Combine(Root, "Data");
        private static readonly string BackupDir = Path.Combine(DataDir, "Backups");
        private static readonly string PdfDir = Path.Combine(Root, "Reports", "PDF");
        private static readonly string ExcelDir = Path.Combine(Root, "Reports", "Excel");
        private static readonly string SummaryDir = Path.Combine(Root, "Reports", "Summary");

        static PersistenceManager()
        {
            Directory.CreateDirectory(DataDir);
            Directory.CreateDirectory(BackupDir);
            Directory.CreateDirectory(PdfDir);
            Directory.CreateDirectory(ExcelDir);
            Directory.CreateDirectory(SummaryDir);
        }

        private static void CreateBackup(string fileName)
        {
            try
            {
                string source = Path.Combine(DataDir, fileName);
                if (File.Exists(source))
                {
                    string bName = $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                    string bPath = Path.Combine(BackupDir, bName);
                    File.Copy(source, bPath, true);
                    Console.WriteLine($"[BACKUP] Created: {bPath}");
                    var old = new DirectoryInfo(BackupDir).GetFiles("*.json").OrderByDescending(f => f.CreationTime).Skip(10);
                    foreach (var f in old) f.Delete();
                }
            }
            catch { }
        }

        public static void SaveToJson<T>(List<T> data, string fileName)
        {
            try
            {
                CreateBackup(fileName);
                string path = Path.Combine(DataDir, fileName);
                File.WriteAllText(path, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
                Console.WriteLine($"[SAVE] Successful: {path}");
            }
            catch (Exception ex) { throw new Exception($"Save Failed: {ex.Message}"); }
        }

        public static List<T> LoadFromJson<T>(string fileName)
        {
            string path = Path.Combine(DataDir, fileName);
            if (!File.Exists(path)) return new List<T>();
            try
            {
                Console.WriteLine($"[LOAD] Reading: {path}");
                return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(path)) ?? new List<T>();
            }
            catch { return new List<T>(); }
        }

        public static List<FileInfo> GetAvailableBackups() => new DirectoryInfo(BackupDir).GetFiles("*.json").OrderByDescending(f => f.CreationTime).ToList();

        public static void RestoreBackup(string source, string target)
        {
            string targetPath = Path.Combine(DataDir, target);
            CreateBackup(target);
            File.Copy(source, targetPath, true);
            Console.WriteLine($"[RESTORE] Overwritten: {targetPath}");
        }

        public static void ExportSummary(string content, string name)
        {
            string path = Path.Combine(SummaryDir, $"{name}_{DateTime.Now:yyyyMMdd}.txt");
            File.WriteAllText(path, content);
            Console.WriteLine($"[EXPORT] Saved: {path}");
        }

        public static void ExportToExcel<T>(List<T> data, string name)
        {
            string path = Path.Combine(ExcelDir, $"{name}.xlsx");
            using (var wb = new XLWorkbook()) { wb.Worksheets.Add("Report").Cell(1, 1).InsertTable(data); wb.SaveAs(path); }
            Console.WriteLine($"[EXPORT] Saved: {path}");
        }

        public static void GeneratePLReport(List<InventoryBatch> batches, List<PerformanceLog> logs, List<OperationalExpense> expenses)
        {
            string path = Path.Combine(PdfDir, "Bamboo_PL_Report.pdf");
            using (var writer = new PdfWriter(path))
            using (var pdf = new PdfDocument(writer))
            using (var doc = new Document(pdf))
            {
                doc.Add(new Paragraph("Financial Audit & P&L Report").SetFontSize(18).SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)));
                doc.Add(new Paragraph($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}"));

                // Expanded to 7 columns to include Original Cost and Currency
                Table table = new Table(7).SetWidth(UnitValue.CreatePercentValue(100));
                table.AddHeaderCell("Batch ID");
                table.AddHeaderCell("Orig. Cost");
                table.AddHeaderCell("Curr");
                table.AddHeaderCell("Total SEK");
                table.AddHeaderCell("OpEx SEK");
                table.AddHeaderCell("Net SEK");
                table.AddHeaderCell("Audit Trail (Reason)");

                foreach (var b in batches)
                {
                    double rev = logs.Where(l => l.BatchID == b.BatchID).Sum(l => l.CulmRevenueSek + l.ShootRevenueSek);
                    double opex = expenses.Where(e => e.BatchID == b.BatchID || e.BatchID == "Global").Sum(e => e.MaintenanceCost);
                    double totalCost = b.TotalCostSek + opex;
                    double net = rev - totalCost;

                    table.AddCell(b.BatchID);
                    table.AddCell(b.UnitCost.ToString("N2"));
                    table.AddCell(b.Curr.ToString());
                    table.AddCell(b.TotalCostSek.ToString("N2"));
                    table.AddCell(opex.ToString("N2"));
                    table.AddCell(net.ToString("N2"));
                    table.AddCell($"{b.UpdateReason} ({b.LastUpdatedDate:yyyy-MM-dd})");
                }
                doc.Add(table);
            }
            Console.WriteLine($"[EXPORT] Saved to PDF: {path}");
        }
    }
}

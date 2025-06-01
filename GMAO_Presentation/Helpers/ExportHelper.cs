using DevExpress.XtraCharts;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting.Export.Pdf;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Pdf;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System.IO;
using System.IO.Compression;
using DevExpress.XtraGrid.Columns;
using System.Reflection;
using System.Diagnostics;
using DevExpress.XtraTreeMap;

namespace GMAO_Presentation.Helpers
{
    public static class ExportHelper
    {
        public static Image CaptureControl(UserControl ctrl)
        {
            if (!ctrl.Visible)
                throw new InvalidOperationException("Le contrôle doit être visible à l'écran.");

            Point screenPoint = ctrl.PointToScreen(Point.Empty);
            Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(screenPoint, Point.Empty, ctrl.Size);
            }
            return bmp;
        }


        public static Bitmap CaptureControlWithScreenShot(Control ctrl)
        {
            if (!ctrl.Visible || ctrl.Width <= 0 || ctrl.Height <= 0)
                return null;

            Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Point absolu du contrôle à l'écran
                Point screenPoint = ctrl.PointToScreen(Point.Empty);
                g.CopyFromScreen(screenPoint.X, screenPoint.Y, 0, 0, ctrl.Size);
            }
            return bmp;
        }



        public static void ExportControlToPdfOrExcel(Control ctrl, string filePath, string title = "")
        {
            // DevExpress avec support natif
            if (ctrl is GridControl grid)
            {
                string excelPath = Path.ChangeExtension(filePath, ".xlsx");

                var ps = new DevExpress.XtraPrinting.PrintingSystem();
                var link = new DevExpress.XtraPrinting.PrintableComponentLink(ps)
                {
                    Component = (DevExpress.XtraPrinting.IBasePrintable)ctrl
                };

                link.CreateDetailArea += (s, e) =>
                {
                    e.Graph.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    e.Graph.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);
                    e.Graph.DrawString(title, Color.Black, new RectangleF(0, 0, 1000, 30), DevExpress.XtraPrinting.BorderSide.None);
                };

                link.CreateDocument();
                link.ExportToXlsx(excelPath);
            }
            else if (ctrl is ChartControl || ctrl.GetType().Name.Contains("TreeMap"))
            {
                var ps = new DevExpress.XtraPrinting.PrintingSystem();
                var link = new DevExpress.XtraPrinting.PrintableComponentLink(ps)
                {
                    Component = (DevExpress.XtraPrinting.IBasePrintable)ctrl
                };

                link.CreateDetailArea += (s, e) =>
                {
                    e.Graph.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                    e.Graph.StringFormat = new DevExpress.XtraPrinting.BrickStringFormat(StringAlignment.Center);
                    e.Graph.DrawString(title, Color.Black, new RectangleF(0, 0, 1000, 30), DevExpress.XtraPrinting.BorderSide.None);
                };

                link.CreateDocument();
                link.ExportToPdf(filePath);
            }
            else if (ctrl is BiCard biCard)
            {
                //ExportAsImageAndText(ctrl, filePath, biCard.Title, biCard.ValueText);
            }
            else if (ctrl is PowerBIGauge gauge)
            {
                //string dataText = $"Title: {gauge.Title}\nValue: {gauge.Value}\nRange: [{gauge.Minimum} - {gauge.Maximum}]";
                //ExportAsImageAndText(ctrl, filePath, gauge.Title, dataText);
            }
            else
            {
                // Image seule
                var bmp = CaptureControl(ctrl as UserControl);
                var imagePath = Path.ChangeExtension(filePath, ".png");
                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        /*private static void ExportAsImageAndText(Control ctrl, string pdfPath, string title, string dataText)
        {
            var bmp = CaptureControl(ctrl as UserControl);
            if (bmp == null)
                throw new Exception("Impossible de capturer le contrôle (invisible ou non prêt)");
            var imagePath = Path.ChangeExtension(pdfPath, ".png");
            bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose(); // important pour libérer aussi ce bitmap

            using (var doc = new DevExpress.XtraPrinting.PrintingSystem())
            {
                var link = new DevExpress.XtraPrinting.Link(doc);
                link.CreateDetailArea += (s, e) =>
                {
                    using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    using (var img = Image.FromStream(fs))
                    {
                        e.Graph.DrawImage(img, new RectangleF(0, 0, img.Width, img.Height), BorderSide.None, Color.Transparent);
                    }

                    var textBrick = new DevExpress.XtraPrinting.TextBrick
                    {
                        Text = dataText,
                        Rect = new RectangleF(0, bmp.Height + 10, 500, 100),
                        Font = new Font("Segoe UI", 10),
                        ForeColor = Color.Black,
                        Sides = BorderSide.None
                    };
                    e.Graph.DrawBrick(textBrick);
                };
                link.CreateDocument();
                link.ExportToPdf(pdfPath);
            }

            File.Delete(imagePath);

        }
        */
        public static void ExportControlToJson(Control ctrl, string filePath)
        {
            object exportData = null;

            switch (ctrl)
            {
                case GridControl grid:
                    exportData = ExtractGridControlData(grid);
                    break;
                case ChartControl chart:
                    exportData = ExtractChartControlData(chart);
                    break;
                case TreeMapControl treeMap:
                    exportData = ExtractTreeMapDataFromSource(treeMap);
                    break;

                case BiCard biCard:
                    exportData = new { Type = "BiCard", Title = biCard.Title, Value = biCard.ValueText };
                    break;
                case PowerBIGauge gauge:
                    exportData = new { Type = "PowerBIGauge", Title = gauge.Title, Value = gauge.Value, Minimum = gauge.Minimum, Maximum = gauge.Maximum };
                    break;
                default:
                    exportData = new { Type = ctrl.GetType().Name, Message = "Export non supporté pour ce contrôle" };
                    break;
            }

            var json = JsonConvert.SerializeObject(exportData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private static object ExtractTreeMapDataFromSource(TreeMapControl treeMap)
        {
            if (treeMap.DataAdapter is TreeMapFlatDataAdapter adapter &&
                adapter.DataSource is IEnumerable<object> source)
            {
                var labelField = adapter.LabelDataMember;
                var valueField = adapter.ValueDataMember;
               

                var fullData = source
                    .Select(item => new
                    {
                        Label = GetProperty(item, labelField),
                        Value = GetProperty(item, valueField)
                        
                    })
                    .ToList();

                return new
                {
                    Type = "TreeMapData",
                    Count = fullData.Count,
                    Nodes = fullData
                };
            }

            return new { Type = "TreeMap", Error = "DataAdapter non supporté ou vide" };
        }

        private static object GetProperty(object obj, string propertyName)
        {
            if (obj == null || string.IsNullOrEmpty(propertyName)) return null;
            var prop = obj.GetType().GetProperty(propertyName);
            return prop?.GetValue(obj);
        }




        private static object ExtractGridControlData(GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view == null) return null;

            var dataList = new List<Dictionary<string, object>>();

            for (int i = 0; i < view.RowCount; i++)
            {
                var rowData = new Dictionary<string, object>();
                foreach (GridColumn column in view.VisibleColumns)
                {
                    var value = view.GetRowCellValue(i, column);
                    rowData[column.Caption] = value;
                }
                dataList.Add(rowData);
            }

            return new
            {
                Type = "GridControl",
                RowCount = view.RowCount,
                Data = dataList
            };
        }

        private static object ExtractChartControlData(ChartControl chart)
        {
            var seriesList = new List<object>();

            foreach (Series series in chart.Series)
            {
                var points = series.Points.Select(p => new
                {
                    Argument = p.Argument,
                    Value = p.NumericalValue
                }).ToList();

                seriesList.Add(new
                {
                    SeriesName = series.Name,
                    Points = points
                });
            }

            return new
            {
                Type = "ChartControl",
                Series = seriesList
            };
        }

        public static void ExportControlToZip(Control ctrl, string zipPath, string baseName)
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDir);

            string fileExportPath = Path.Combine(tempDir, baseName); // Sans extension

            ExportControlToPdfOrExcel(ctrl, fileExportPath + ".pdf", baseName);
            ExportControlToJson(ctrl, fileExportPath + ".json");

            ZipFile.CreateFromDirectory(tempDir, zipPath);
            Directory.Delete(tempDir, true);
        }

        public static void ExportVisuelsDeLaPage(XtraTabPage tabPage, string dossierExport)
        {
            if (tabPage == null || string.IsNullOrWhiteSpace(dossierExport)) return;

            Directory.CreateDirectory(dossierExport);
            int compteur = 1;

            // Forcer le TabPage actif pour que ses contrôles soient visibles
            if (tabPage.Parent is XtraTabControl tabControl)
                tabControl.SelectedTabPage = tabPage;

            Application.DoEvents(); // s'assurer que le rendu se met à jour

            foreach (Control ctrl in tabPage.Controls)
            {
                string nomControle = string.IsNullOrWhiteSpace(ctrl.Name) ? ctrl.GetType().Name : ctrl.Name;
                string baseName = $"Visuel_{compteur}_{nomControle}";
                string zipPath = Path.Combine(dossierExport, baseName + ".zip");

                try
                {
                    ExportControlToZip(ctrl, zipPath, baseName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de l'export de {nomControle} : {ex.Message}", "Erreur export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                compteur++;
            }

            MessageBox.Show("Tous les visuels de la page ont été exportés.", "Export terminé", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }

}

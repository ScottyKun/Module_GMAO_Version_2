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



namespace GMAO_Presentation.Helpers
{
    public static class ExportHelper
    {
        public static Bitmap CaptureControl(Control ctrl)
        {
            Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height);
            ctrl.DrawToBitmap(bmp, new Rectangle(Point.Empty, ctrl.Size));
            return bmp;
        }

        public static void ExportControlToPdf(Control ctrl, string filePath)
        {
            // Cas des composants DevExpress avec support natif de l'impression
            if (ctrl is GridControl || ctrl is ChartControl || ctrl.GetType().Name.Contains("TreeMap"))
            {
                var ps = new DevExpress.XtraPrinting.PrintingSystem();
                var link = new DevExpress.XtraPrinting.PrintableComponentLink(ps);

                // Affectation du composant imprimable
                link.Component = (DevExpress.XtraPrinting.IBasePrintable)ctrl;
                link.CreateDocument();
                link.ExportToPdf(filePath);
            }
            // Cas des composants personnalisés (image + données)
            else if (ctrl is BiCard biCard)
            {
                ExportAsImageAndText(ctrl, filePath, biCard.Title, biCard.ValueText);
            }
            else if (ctrl is PowerBIGauge gauge)
            {
                string dataText = $"Title: {gauge.Title}\nValue: {gauge.Value}\nRange: [{gauge.Minimum} - {gauge.Maximum}]";
                ExportAsImageAndText(ctrl, filePath, gauge.Title, dataText);
            }
            else
            {
                // Cas par défaut : capture en image uniquement
                var bmp = CaptureControl(ctrl);
                var imagePath = Path.ChangeExtension(filePath, ".png");
                bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private static void ExportAsImageAndText(Control ctrl, string pdfPath, string title, string dataText)
        {
            var bmp = CaptureControl(ctrl);
            var imagePath = Path.ChangeExtension(pdfPath, ".png");
            bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

            using (var doc = new DevExpress.XtraPrinting.PrintingSystem())
            {
                var link = new DevExpress.XtraPrinting.Link(doc);
                link.CreateDetailArea += (s, e) =>
                {
                    // Ajout de l'image capturée
                    var img = System.Drawing.Image.FromFile(imagePath);
                    e.Graph.DrawImage(img, new RectangleF(0, 0, img.Width, img.Height), DevExpress.XtraPrinting.BorderSide.None, Color.Transparent);

                    var textBrick = new DevExpress.XtraPrinting.TextBrick
                    {
                        Text = dataText,
                        Rect = new RectangleF(0, img.Height + 10, 500, 100),
                        Font = new Font("Segoe UI", 10),
                        ForeColor = Color.Black,
                        Sides = BorderSide.None
                    };
                    e.Graph.DrawBrick(textBrick);


                };
                link.CreateDocument();
                link.ExportToPdf(pdfPath);
            }

            // Optionnel : supprimer l'image temporaire
            File.Delete(imagePath);
        }





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

                case Control c when c.GetType().Name.Contains("TreeMap"):
                    exportData = new { Type = "TreeMap", Message = "TreeMap export not implemented yet." };
                    break;

                case BiCard biCard:
                    exportData = new
                    {
                        Type = "BiCard",
                        Title = biCard.Title,
                        Value = biCard.ValueText
                    };
                    break;

                case PowerBIGauge gauge:
                    exportData = new
                    {
                        Type = "PowerBIGauge",
                        Title = gauge.Title,
                        Value = gauge.Value,
                        Minimum = gauge.Minimum,
                        Maximum = gauge.Maximum
                    };
                    break;

                default:
                    exportData = new
                    {
                        Type = ctrl.GetType().Name,
                        Message = "Export non supporté pour ce contrôle"
                    };
                    break;
            }

            var json = JsonConvert.SerializeObject(exportData, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private static object ExtractGridControlData(GridControl grid)
        {
            var view = grid.MainView as GridView;
            if (view == null) return null;

            var dataList = new List<Dictionary<string, object>>();

            for (int i = 0; i < view.RowCount; i++)
            {
                var rowData = new Dictionary<string, object>();
                foreach (var column in view.VisibleColumns)
                {
                    var fieldName = column.ToString();
                    var value = view.GetRowCellValue(i, (string)column);
                    rowData[fieldName] = value;
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

            string pdfPath = Path.Combine(tempDir, baseName + ".pdf");
            string jsonPath = Path.Combine(tempDir, baseName + ".json");

            ExportControlToPdf(ctrl, pdfPath);
            ExportControlToJson(ctrl, jsonPath);

            ZipFile.CreateFromDirectory(tempDir, zipPath);

            Directory.Delete(tempDir, true);
        }


        public static void ExportVisuelsDeLaPage(XtraTabPage tabPage, string dossierExport)
        {
            if (tabPage == null || string.IsNullOrWhiteSpace(dossierExport)) return;

            Directory.CreateDirectory(dossierExport);
            int compteur = 1;

            foreach (Control ctrl in tabPage.Controls)
            {
                // Nettoyage du nom
                string nomControle = string.IsNullOrWhiteSpace(ctrl.Name) ? ctrl.GetType().Name : ctrl.Name;
                string baseName = $"Visuel_{compteur}_{nomControle}";
                string zipPath = Path.Combine(dossierExport, baseName + ".zip");

                try
                {
                    ExportHelper.ExportControlToZip(ctrl, zipPath, baseName);
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

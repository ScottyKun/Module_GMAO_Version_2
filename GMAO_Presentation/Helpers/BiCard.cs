using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAO_Presentation.Helpers
{
    [ToolboxItem(true)] // Garantit l'apparition dans la boîte à outils
    [Category("Custom Controls")] // Groupe les contrôles dans la boîte à outils
    [Description("Affiche une carte de style Power BI")]
    public class BiCard : Control
    {
        private string _valueText = "0";
        private string _title = "Title";
        private Color _titleColor = Color.DarkGray;

        public BiCard()
        {
            this.Size = new Size(200, 100);
            this.DoubleBuffered = true;
        }

        public string ValueText
        {
            get => _valueText;
            set { _valueText = value; Invalidate(); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; Invalidate(); }
        }

        public Color TitleColor
        {
            get => _titleColor;
            set { _titleColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var backgroundBrush = new SolidBrush(this.BackColor))
            using (var titleBrush = new SolidBrush(_titleColor))
            using (var valueBrush = new SolidBrush(this.ForeColor))
            using (var borderPen = new Pen(Color.FromArgb(200, 200, 200), 1))
            {
                // Fond arrondi
                var rect = new Rectangle(0, 0, Width - 1, Height - 1);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillRoundedRectangle(backgroundBrush, rect, 10);
                e.Graphics.DrawRoundedRectangle(borderPen, rect, 10);

                // Titre
                var titleFont = new Font(this.Font.FontFamily, 9, FontStyle.Regular);
                var titleSize = e.Graphics.MeasureString(_title, titleFont);
                e.Graphics.DrawString(_title, titleFont, titleBrush,
                    new PointF(10, 10));

                // Valeur
                var valueFont = new Font(this.Font.FontFamily, 20, FontStyle.Bold);
                var valueSize = e.Graphics.MeasureString(_valueText, valueFont);
                var yPos = (Height - valueSize.Height) / 2 + 10;
                e.Graphics.DrawString(_valueText, valueFont, valueBrush,
                    new PointF(10, yPos));
            }
        }


       

    }

    // Extension pour les rectangles arrondis
    public static class GraphicsExtensions
    {
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen,
            Rectangle bounds, int cornerRadius)
        {
            var path = CreateRoundedRectanglePath(bounds, cornerRadius);
            graphics.DrawPath(pen, path);
        }

        public static void FillRoundedRectangle(this Graphics graphics, Brush brush,
            Rectangle bounds, int cornerRadius)
        {
            var path = CreateRoundedRectanglePath(bounds, cornerRadius);
            graphics.FillPath(brush, path);
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius,
                       radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }



}

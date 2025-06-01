using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GMAO_Presentation.Helpers
{
    [ToolboxItem(true)]
    [Category("Custom Controls")]
    [Description("Affiche une jauge de style Power BI")]
    public class PowerBIGauge : UserControl
    {
        private double _minimum = 0;
        private double _maximum = 100;
        private double _value = 50;
        private string _title = "Gauge";
        private Color _gaugeColor = Color.SteelBlue;
        private Color _textColor = Color.Black;
        private Font _titleFont = new Font("Segoe UI", 10);
        private Font _valueFont = new Font("Segoe UI", 12);

        [Category("Appearance")]
        public string Title
        {
            get { return _title; }
            set { _title = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color GaugeColor
        {
            get { return _gaugeColor; }
            set { _gaugeColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Font TitleFont
        {
            get { return _titleFont; }
            set { _titleFont = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Font ValueFont
        {
            get { return _valueFont; }
            set { _valueFont = value; Invalidate(); }
        }

        [Category("Data")]
        public double Minimum
        {
            get { return _minimum; }
            set { _minimum = value; Invalidate(); }
        }

        [Category("Data")]
        public double Maximum
        {
            get { return _maximum; }
            set { _maximum = value; Invalidate(); }
        }

        [Category("Data")]
        public double Value
        {
            get { return _value; }
            set
            {
                if (value < _minimum) _value = _minimum;
                else if (value > _maximum) _value = _maximum;
                else _value = value;
                Invalidate();
            }
        }

        public PowerBIGauge()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Padding = new Padding(10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = ClientRectangle;
            bounds.Inflate(-Padding.Left, -Padding.Top);

            // Draw background
            using (var brush = new SolidBrush(BackColor))
            {
                g.FillRectangle(brush, ClientRectangle);
            }

            // Draw title
            SizeF titleSize = g.MeasureString(_title, _titleFont);
            float titleX = bounds.X + (bounds.Width - titleSize.Width) / 2;
            float titleY = bounds.Y;
            using (var brush = new SolidBrush(_textColor))
            {
                g.DrawString(_title, _titleFont, brush, titleX, titleY);
            }

            // Gauge drawing parameters
            float centerX = bounds.X + bounds.Width / 2;
            float centerY = bounds.Y + bounds.Height * 0.75f;
            float radius = Math.Min(bounds.Width, bounds.Height * 1.5f) / 2 - 10;
            float startAngle = 180;
            float sweepAngle = 180;

            // Calculate current value angle
            double percentage = (_value - _minimum) / (_maximum - _minimum);
            float currentValueAngle = (float)(sweepAngle * percentage);

            try
            {
                using (var pen = new Pen(Color.LightGray, 10))
                {
                    g.DrawArc(pen, centerX - radius, centerY - radius, 2 * radius, 2 * radius, startAngle, sweepAngle);
                }

                using (var pen = new Pen(_gaugeColor, 10))
                {
                    g.DrawArc(pen, centerX - radius, centerY - radius, 2 * radius, 2 * radius, startAngle, currentValueAngle);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erreur DrawArc : " + ex.Message);
            }

            // Draw value text
            string valueText = _value.ToString();
            SizeF valueTextSize = g.MeasureString(valueText, _valueFont);
            float valueTextX = bounds.X + (bounds.Width - valueTextSize.Width) / 2;
            float valueTextY = bounds.Y + bounds.Height * 0.75f - valueTextSize.Height / 2;
            using (var brush = new SolidBrush(_textColor))
            {
                g.DrawString(valueText, _valueFont, brush, valueTextX, valueTextY);
            }

            // Draw min/max labels
            using (var font = new Font("Segoe UI", 8))
            using (var brush = new SolidBrush(_textColor))
            {
                SizeF minSize = g.MeasureString(_minimum.ToString(), font);
                g.DrawString(_minimum.ToString(), font, brush, bounds.X, centerY + radius - minSize.Height);

                SizeF maxSize = g.MeasureString(_maximum.ToString(), font);
                g.DrawString(_maximum.ToString(), font, brush, bounds.Right - maxSize.Width, centerY + radius - maxSize.Height);
            }

            // Draw border (optional, for visual separation)
            using (var pen = new Pen(Color.LightGray))
            {
                g.DrawRectangle(pen, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            }
        }

        

    }
}

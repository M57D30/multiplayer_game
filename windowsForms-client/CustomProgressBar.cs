using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace windowsForms_client
{
    public class CustomProgressBar : ProgressBar
    {
        public Color BarColor { get; set; } = Color.Green;

        public CustomProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;
            e.Graphics.FillRectangle(new SolidBrush(BackColor), rect);
            rect.Width = (int)(rect.Width * ((double)Value / Maximum));
            e.Graphics.FillRectangle(new SolidBrush(BarColor), rect);
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}

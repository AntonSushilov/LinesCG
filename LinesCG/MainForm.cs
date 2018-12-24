using System;
using System.Drawing;
using System.Windows.Forms;

namespace LinesCG
{
    public partial class MainForm : Form
    {
        Point A, B, C, D;
        Line AB, DC;
        public MainForm()
        {
            InitializeComponent();
            PaintLogic.bitmap = new Bitmap(DrawZone.ClientRectangle.Width, DrawZone.ClientRectangle.Height);
            PaintLogic.c = new Converter(DrawZone.Width, DrawZone.Height);
            MouseWheel += new MouseEventHandler(MainForm_MouseWheel);

            KeyPreview = true;
        }
        bool IsDraw = false;



        void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                PaintLogic.c.Scale(0.5);
            }
            else
            {
                PaintLogic.c.Scale(2);

            }
            DrawZone.Invalidate();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            A = new Point((int)numericUpDown1.Value, (int)numericUpDown2.Value);
            B = new Point((int)numericUpDown3.Value, (int)numericUpDown4.Value);
            C = new Point((int)numericUpDown5.Value, (int)numericUpDown6.Value);
            D = new Point((int)numericUpDown7.Value, (int)numericUpDown8.Value);
            AB = new Line(A, B);
            DC = new Line(D, C);
            IsDraw = true;
            DrawZone.Invalidate();
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            IsDraw = false;
            DrawZone.Invalidate();
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                PaintLogic.c.Change(3);
            }
            if (e.KeyCode == Keys.S)
            {
                PaintLogic.c.Change(2);
            }
            if (e.KeyCode == Keys.A)
            {
                PaintLogic.c.Change(1);
            }
            if (e.KeyCode == Keys.D)
            {
                PaintLogic.c.Change(0);
            }
            DrawZone.Invalidate();
        }

        private void DrawZone_Paint(object sender, PaintEventArgs e)
        {
            if (IsDraw)
            {
                PaintLogic.Draw(DrawZone.ClientRectangle, AB, DC);
                e.Graphics.DrawImage(PaintLogic.bitmap, DrawZone.ClientRectangle);
            }
        }

    }
}

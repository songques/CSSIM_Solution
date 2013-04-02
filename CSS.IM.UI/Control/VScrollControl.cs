using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Util;

namespace CSS.IM.UI.Control
{
    public partial class VScrollControl : System.Windows.Forms.Control
    {
        private Bitmap Bmp, bar_Bmp;
        private System.Drawing.Graphics g;
        private PictureBox arrowdown;
        private PictureBox bar;
        private PictureBox arrowup;

        private enum ArrowState
        {
            Up, 
            Down, 
            None
        }
        private ArrowState currentArrow = ArrowState.None;
        private Timer timer = null;
        private bool barMove;
        private Point oldPoint;

        public VScrollControl()
        {
            this.Width = 14;
            this.Height = 60;
            bar_Bmp = ResClass.GetImgRes("scrollbar_bar_normal");
            InitControl();
        }

        private void InitControl()
        {
            arrowdown = new PictureBox();
            arrowdown.Name = "arrowdown";
            arrowdown.BackColor = Color.Transparent;
            arrowdown.SizeMode = PictureBoxSizeMode.AutoSize;
            arrowdown.Image = ResClass.GetImgRes("scrollbar_arrowdown_normal");
            arrowdown.Location = new Point(0, Height - 14);
            arrowdown.MouseEnter += new EventHandler(arrow_MouseEnter);
            arrowdown.MouseLeave += new EventHandler(arrow_MouseLeave);
            arrowdown.MouseDown += new MouseEventHandler(arrow_MouseDown);
            arrowdown.MouseUp += new MouseEventHandler(arrow_MouseUp);
            arrowdown.MouseClick += new MouseEventHandler(arrow_MouseClick);
            Controls.Add(arrowdown);

            bar = new PictureBox();
            bar.BackColor = Color.Transparent;
            bar.Paint += new PaintEventHandler(bar_Paint);
            bar.MouseEnter += new EventHandler(bar_MouseEnter);
            bar.MouseLeave += new EventHandler(bar_MouseLeave);
            bar.MouseDown += new MouseEventHandler(bar_MouseDown);
            bar.MouseUp += new MouseEventHandler(bar_MouseUp);
            bar.MouseClick += new MouseEventHandler(bar_MouseClick);
            bar.MouseMove += new MouseEventHandler(bar_MouseMove);
            bar.Location = new Point(0, 14);
            bar.Size = new Size(14, 14);
            Controls.Add(bar);

            arrowup = new PictureBox();
            arrowup.Name = "arrowup";
            arrowup.BackColor = Color.Transparent;
            arrowup.SizeMode = PictureBoxSizeMode.AutoSize;
            arrowup.Image = ResClass.GetImgRes("scrollbar_arrowup_normal");
            arrowup.Location = new Point(0, 0);
            arrowup.MouseEnter += new EventHandler(arrow_MouseEnter);
            arrowup.MouseLeave += new EventHandler(arrow_MouseLeave);
            arrowup.MouseDown += new MouseEventHandler(arrow_MouseDown);
            arrowup.MouseUp += new MouseEventHandler(arrow_MouseUp);
            arrowup.MouseClick += new MouseEventHandler(arrow_MouseClick);
            Controls.Add(arrowup);

            timer = new Timer();
            timer.Enabled = false;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 100;
        }

        private void bar_MouseMove(object sender, MouseEventArgs e)
        {
            if (barMove) 
            {
                //e.x
                System.Diagnostics.Debug.WriteLine(bar.Top);
                //int c=0;
                //if (currentArrow == ArrowState.Down)
                //{
                //    c = MousePosition.Y - oldPoint.Y;
                //}
                //else if (currentArrow == ArrowState.Up)
                //{
                //    c = MousePosition.Y + oldPoint.Y;
                //}
                //bar.Top = bar.Top + c;
                bar.Top = e.Y;
            }
            System.GC.Collect();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (currentArrow == ArrowState.Down)
            {
                if (bar.Top < Height - 14 - bar.Height) 
                {
                    bar.Top += 1;
                }
            }
            else if (currentArrow == ArrowState.Up)
            {
                if (bar.Top > 14)
                {
                    bar.Top -= 1;
                }
            }
            else 
            {
                timer.Enabled = false;
            }
        }

        private void bar_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox p = sender as PictureBox;
                if (p.Name == "arrowup")
                {

                }
                else
                {

                }
            }
        }

        private void arrow_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox p = sender as PictureBox;
                if (p.Name == "arrowup")
                {
                    if (bar.Top > 14)
                    {
                        bar.Top -= 1;
                    }
                }
                else
                {
                    if (bar.Top < Height - 14 - bar.Height)
                    {
                        bar.Top += 1;
                    }
                }
            }
        }

        private void bar_MouseUp(object sender, MouseEventArgs e)
        {
            barMove = false;
            bar_MouseLeave(sender, null);
        }

        private void bar_MouseDown(object sender, MouseEventArgs e)
        {
            barMove = true;
            oldPoint = MousePosition;
            bar_Bmp = ResClass.GetImgRes("scrollbar_bar_down");
            this.Invalidate();
        }

        private void bar_MouseLeave(object sender, EventArgs e)
        {
            bar_Bmp = ResClass.GetImgRes("scrollbar_bar_normal");
            this.Invalidate();
        }

        private void bar_MouseEnter(object sender, EventArgs e)
        {
            bar_Bmp = ResClass.GetImgRes("scrollbar_bar_highlight");
            this.Invalidate();
        }

        private void arrow_MouseUp(object sender, MouseEventArgs e)
        {
            currentArrow = ArrowState.None;
            arrow_MouseLeave(sender, null);
        }

        private void arrow_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (p.Name == "arrowup")
            {
                p.Image = ResClass.GetImgRes("scrollbar_arrowup_down");
                currentArrow = ArrowState.Up;
            }
            else
            {
                p.Image = ResClass.GetImgRes("scrollbar_arrowdown_down");
                currentArrow = ArrowState.Down;
            }
            timer.Enabled = true;
        }

        private void arrow_MouseLeave(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (p.Name == "arrowup")
                p.Image = ResClass.GetImgRes("scrollbar_arrowup_normal");
            else
                p.Image = ResClass.GetImgRes("scrollbar_arrowdown_normal");
        }

        private void arrow_MouseEnter(object sender, EventArgs e)
        {
            PictureBox p = sender as PictureBox;
            if (p.Name == "arrowup")
                p.Image = ResClass.GetImgRes("scrollbar_arrowup_highlight");
            else
                p.Image = ResClass.GetImgRes("scrollbar_arrowdown_highlight");
        }

        private void bar_Paint(object sender, PaintEventArgs e)
        {
            if (bar_Bmp != null)
            {
                g = e.Graphics;
                g.DrawImage(bar_Bmp, new Rectangle(0, 0, bar.Width, 2), 0, 0, Bmp.Width, 2, GraphicsUnit.Pixel);
                g.DrawImage(bar_Bmp, new Rectangle(0, 2, bar.Width, bar.Height - 4), 0, 2, Bmp.Width, Bmp.Height - 4, GraphicsUnit.Pixel);
                g.DrawImage(bar_Bmp, new Rectangle(0, bar.Height - 2, bar.Width, 2), 0, Bmp.Height - 2, Bmp.Width, 2, GraphicsUnit.Pixel);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            g = e.Graphics;
            Bmp = ResClass.GetImgRes("scrollbar_bkg");
            g.DrawImage(Bmp, new Rectangle(0, 0, Width, Height), 0, 0, Bmp.Width, Bmp.Height - 1, GraphicsUnit.Pixel);  
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Width = 14;
            if (arrowdown != null)
                arrowdown.Location = new Point(0, Height - 14);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }
    }
}

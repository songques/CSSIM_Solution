using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CSS.IM.Library.Controls
{
	/// <summary>
	/// CaptureForm 的摘要说明。
	/// </summary>
	public class FormCapture : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		Point op;
		Rectangle area=Rectangle.Empty;
		private Image img;
		public FormCapture()
		{
			this.SetStyle(ControlStyles.DoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.UserPaint,true);
			InitializeComponent();
			this.Bounds=System.Windows.Forms.Screen.PrimaryScreen.Bounds;

			this.BackgroundImage=this.GetDestopImage();
		}

		public Image Image
		{
			get{return this.img;}
		}


		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private Image GetDestopImage()
		{
			Rectangle area=System.Windows.Forms.Screen.PrimaryScreen.Bounds;
			Bitmap bm=new Bitmap(area.Width,area.Height);
			Graphics g=Graphics.FromImage(bm);

			System.IntPtr p=g.GetHdc();
			IntPtr c=GetDesktopWindow();
			System.IntPtr ddc=GetDC(c);
			API.BitBlt(p,0,0,this.Width,this.Height,ddc,0,0, API.SRCCOPY);
			API.ReleaseDC(c,ddc);
			g.ReleaseHdc(p);
			return bm;
		}
		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// CaptureForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "CaptureForm";
			this.Text = "CaptureForm";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			//this.Click += new System.EventHandler(this.CaptureForm_Click);
			this.DoubleClick += new System.EventHandler(this.CaptureForm_DoubleClick);

		}
		#endregion
		
		[DllImport("user32.dll",EntryPoint="GetDC")]
		public static extern IntPtr GetDC(IntPtr ptr);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern IntPtr GetDesktopWindow();

		private void CaptureForm_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.area.Width<=0 ||this.area.Height<=0)
			{
				this.DialogResult=DialogResult.Cancel;return;
			}
			Bitmap bm=new Bitmap(this.area.Width,this.area.Height);
			Graphics g=Graphics.FromImage(bm);
			g.DrawImage(this.BackgroundImage,0,0,this.area,GraphicsUnit.Pixel);
			this.img=bm;
		//	Bitmap bm=new Bitmap(this.BackgroundImage);
		//	this.img=bm.Clone(this.area,System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
			this.DialogResult=DialogResult.OK;
		}

		int index=-1;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			if(this.area==Rectangle.Empty && e.Button==MouseButtons.Left)
			{
				this.area.Location=new Point(e.X,e.Y);
			}
			this.op=new Point(e.X,e.Y);
			this.index=this.GetSelectedHandle(new Point(e.X,e.Y));
			this.SetCursor();
		}

		private int GetSelectedHandle(Point p)
		{
			int index=-1;
			for(int i=1;i<9;i++)
			{
				if(GetHandleRect(i).Contains(p))
				{
					index=i;
					break;
				}
			}
			if(this.area.Contains(p))index=0;
			System.Diagnostics.Trace.WriteLine(area.ToString());
			System.Diagnostics.Trace.WriteLine(p.ToString());
			System.Diagnostics.Trace.WriteLine(index.ToString());
			return index;
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			if(this.Capture)
			{
				this.MoveHandleTo(new Point(e.X,e.Y));
				this.Invalidate();
			}
			else
			{
				this.index=this.GetSelectedHandle(new Point(e.X,e.Y));
				this.SetCursor();
			}
		}
		private  void MoveHandleTo(Point point)
		{
			int left = area.Left;
			int top = area.Top;
			int right = area.Right;
			int bottom = area.Bottom;

			switch ( index )
			{
				case 0:
					area.X+=point.X-op.X;
					area.Y+=point.Y-op.Y;
					this.op=point;
					return;
				case 1:
					left = point.X;
					top = point.Y;
					break;
				case 2:
					top = point.Y;
					break;
				case 3:
					right = point.X;
					top = point.Y;
					break;
				case 4:
					right = point.X;
					break;
				case 5:
					right = point.X;
					bottom = point.Y;
					break;
				case 6:
					bottom = point.Y;
					break;
				case 7:
					left = point.X;
					bottom = point.Y;
					break;
				case 8:
					left = point.X;
					break;
			}
			this.op=point;
			area.X=left;
			area.Y=top;
			area.Width=right-left;
			area.Height=bottom-top;
		}
		private void SetCursor()
		{
			Cursor cr=Cursors.Default;
			if(index==1 ||index==5)
			{
				cr=Cursors.SizeNWSE;
			}
			else if(index==2 ||index==6)
			{
				cr=Cursors.SizeNS;
			}
			else if(index==3 ||index==7)
			{
				cr=Cursors.SizeNESW;
			}
			else if(index==4 ||index==8)
			{
				cr=Cursors.SizeWE;
			}
			else if(index==0)
			{
				cr=Cursors.SizeAll;
			}
			Cursor.Current=cr;
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			int left = area.Left;
			int top = area.Top;
			int right = area.Right;
			int bottom = area.Bottom;
			area.X=Math.Min(left,right);
			area.Y=Math.Min(top,bottom);
			area.Width=Math.Abs(left-right);
			area.Height=Math.Abs(top-bottom);
			if(e.Button==MouseButtons.Right)
			{
				if(this.area==Rectangle.Empty)
				{
					this.DialogResult=DialogResult.Cancel;
				}
				else
				{
					this.area=Rectangle.Empty;
					this.Invalidate();
				}
			}
			this.index=this.GetSelectedHandle(new Point(e.X,e.Y));
			this.SetCursor();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//		base.OnPaint (e);
			base.OnPaint(e);
			e.Graphics.DrawRectangle(new Pen(this.ForeColor),this.area);
			
			for(int i=1;i<9;i++)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.Red),this.GetHandleRect(i));
			}
		}
		private Rectangle GetHandleRect(int index)
		{
			Point point = GetHandle(index);
			return new Rectangle(point.X - 3, point.Y - 3, 7, 7);
		}
		private  Point GetHandle(int index)
		{
			int x, y, xCenter, yCenter;

			xCenter = area.X + area.Width/2;
			yCenter = area.Y + area.Height/2;
			x = area.X;
			y = area.Y;

			switch ( index )
			{
				case 1:
					x = area.X;
					y = area.Y;
					break;
				case 2:
					x = xCenter;
					y = area.Y;
					break;
				case 3:
					x = area.Right;
					y = area.Y;
					break;
				case 4:
					x = area.Right;
					y = yCenter;
					break;
				case 5:
					x = area.Right;
					y = area.Bottom;
					break;
				case 6:
					x = xCenter;
					y = area.Bottom;
					break;
				case 7:
					x = area.X;
					y = area.Bottom;
					break;
				case 8:
					x = area.X;
					y = yCenter;
					break;
			}

			return new Point(x, y);

		}
	 

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;
using System.Threading;
using CSS.IM.UI.Control;
using CSS.IM.UI.Entity;
using CSS.IM.UI.Util;
using CSS.IM.UI.Form;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.iq.disco;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.App.Settings;
using CSS.IM.XMPP.protocol.iq.roster;
using CSS.IM.XMPP.protocol.x.data;
using CSS.IM.XMPP.protocol.iq.vcard;
using CSS.IM.App.Properties;
using CSS.IM.Library.Data;
using System.Data.OleDb;
using System.Net;

namespace CSS.IM.App
{
    public partial class QQMainForm : System.Windows.Forms.Form
    {
        String filename = "";

        public XmppClientConnection XmppCon = new XmppClientConnection();
        public DiscoManager discoManager;

        private LoginWaiting waiting = null;
        private LoginFrom login = null;

        private List<Point> TrayIconPoints = new List<Point>();
        public System.Windows.Forms.Timer OverTrayTimer = new System.Windows.Forms.Timer();

        private Dictionary<String, List<CSS.IM.XMPP.protocol.client.Message>> msgBox = new Dictionary<string, List<XMPP.protocol.client.Message>>();
        //private List<CSS.IM.XMPP.protocol.client.Message> msgBox = new List<XMPP.protocol.client.Message>();

        private TreeNode Selectnode;//用于保存选取后的treenode

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="pres"></param>
        delegate void OnPresenceDelegate(object sender, Presence pres);
        /// <summary>
        /// 打开消息窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        delegate void OnMessageDelegate(object sender, CSS.IM.XMPP.protocol.client.Message msg);

        /// <summary>
        /// 添加聊天室的代理
        /// </summary>
        /// <param name="jid"></param>
        delegate void OnChatGroupAddDelegate(Jid jid);

        /// <summary>
        /// 注销事件
        /// </summary>
        delegate void LogoutDelegate(bool ISAutoLogin, bool isLogin);

        /// <summary>
        /// Socket打开事件
        /// </summary>
        delegate void XmppSocketOpenDelegate();

        #region 参数
        private Graphics g = null;
        private Bitmap Bmp = null;
        private Bitmap closeBmp = null;
        private Bitmap minBmp = null;
        private Bitmap maxBmp = null;
        private Brush titleColor = Brushes.Black;

        private int xwidth;
        private bool isOneLoad = true;
        private bool miniMode = false;
        private Size oldSize;
        private List<string> shadlist = null;
        private Thread skinThread = null;
        private string currentSkin;
        private Font f = null;

        [Description("所有按钮的鼠标单击事件"), Category("Action")]
        public delegate void QzoneMouseClickEventHandler(object sender, MouseEventArgs e);
        public delegate void MailMouseClickEventHandler(object sender, MouseEventArgs e);
        public delegate void SearchMouseClickEventHandler(object sender, MouseEventArgs e);
        public delegate void MenuMouseClickEventHandler(object sender, MouseEventArgs e);
        public delegate void ToolsMouseClickEventHandler(object sender, MouseEventArgs e);
        public delegate void MessageMouseClickEventHandler(object sender, MouseEventArgs e);
        public delegate void FindMouseClickEventHandler(object sender, MouseEventArgs e);
        public event QzoneMouseClickEventHandler QzoneMouseClick;
        public event MailMouseClickEventHandler MailMouseClick;
        public event SearchMouseClickEventHandler SearchMouseClick;
        public event MenuMouseClickEventHandler MenuMouseClick;
        public event ToolsMouseClickEventHandler ToolsMouseClick;
        public event FindMouseClickEventHandler FindMouseClick;
        private string SelectTab = "";
        private string _NikeName = "翱翔的雄鹰";
        #endregion

        #region 初始化

        public QQMainForm()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                this.Font = new Font("微软雅黑", 9F, FontStyle.Regular);
            }
            else
            {
                this.Font = new Font("宋体", 9F, FontStyle.Regular);
            }
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            f = new Font("Tahoma", 9F, FontStyle.Bold);
            InitializeComponent();
            InitControl();
        }

        private void InitControl()
        {
            MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            closeBmp = ResClass.GetImgRes("btn_close_normal");
            minBmp = ResClass.GetImgRes("btn_mini_normal");
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
            SelectTab = fd_btn.Name;
            fd_btn.BackgroundImage = ResClass.GetImgRes("main_tab_check");
            gp_btn.BackgroundImage = nt_btn.BackgroundImage = lt_btn.BackgroundImage = ResClass.GetImgRes("main_tab_bkg");
            fd_btn.Image = ResClass.GetImgRes("MainPanel_ContactMainTabButton_texture");
            gp_btn.Image = ResClass.GetImgRes("MainPanel_GroupMainTabButton_texture1");
            nt_btn.Image = ResClass.GetImgRes("role_tabBtn_Normal");
            lt_btn.Image = ResClass.GetImgRes("MainPanel_RecentMainTabButton_texture1");

            userImg.BackgroundImage = ResClass.GetImgRes("Padding4Normal");
            userImg.Image = ResClass.GetHead("big194");

            //qzone16_btn.Image = ResClass.GetImgRes("qzone16");
            qzone16_btn.Image = CSS.IM.UI.Properties.Resources.Wireless;
            mail_btn.Image = ResClass.GetImgRes("mail");
            color_Btn.Image = ResClass.GetImgRes("colour");
            tools_Btn.Image = ResClass.GetImgRes("Tools");
            message_Btn.Image = ResClass.GetImgRes("message");
            find_Btn.Image = ResClass.GetImgRes("find");
            menu_Btn.Image = ResClass.GetImgRes("menu_btn_normal");

            skinPanel.BackgroundImage = ResClass.GetImgRes("MainBkg");
            select_shad.Image = ResClass.GetImgRes("TbShadingNormal");
            select_color.Image = ResClass.GetImgRes("TbAdjustColorNormal");
            skinPanel.BackgroundImage = ResClass.GetImgRes("MainBkg");
            colorPanel.BackgroundImage = ResClass.GetImgRes("RecentColorBkg");
            shadPanel.BackgroundImage = ResClass.GetImgRes("ShadingBkg");
        }

        #endregion

        #region 系统事件

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.FormBorderStyle != FormBorderStyle.None)
                TaskMenu.InitSYSMENU(this);
            else
                TaskMenu.ShowSYSMENU(this);
            if (this.ControlBox)
            {
                this.ButtonClose.Visible = true;
                if (!this.MinimizeBox)
                    this.ButtonMin.Visible = false;
                else
                    this.ButtonMin.Visible = true;
                if (!this.MaximizeBox)
                    this.ButtonMax.Visible = false;
                else
                    this.ButtonMax.Visible = true;
            }
            else
            {
                this.ButtonClose.Visible = false;
                this.ButtonMin.Visible = false;
                this.ButtonMax.Visible = false;
            }
            ResizeControl();
            int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 5, 5);
            Win32.SetWindowRgn(this.Handle, Rgn, true);
            isOneLoad = false;

            shadlist = new List<string>(14);
            shadlist.Add("pic_defaultcolor_normal");
            //if (Directory.Exists(Application.StartupPath + "\\skins"))
            //{
            //    string[] sd = Directory.GetDirectories(Application.StartupPath + "\\skins");
            //    for (int i = 0; i < sd.Length; i++)
            //    {
            //        string[] sf = Directory.GetFiles(sd[i], "main*");
            //        if (sf.Length > 0)
            //        {
            //            shadlist.Add(sd[i]);
            //        }
            //    } 
            //}
            LoadShadList();
            LoadColorList();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            titleColor = Brushes.Black;
            this.Invalidate(new Rectangle(26, 7, xwidth - 26, 31));
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            titleColor = Brushes.Black;
            this.Invalidate(new Rectangle(26, 7, xwidth - 26, 31));
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.Invalidate(new Rectangle(26, 7, xwidth - 26, 31));
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            //if (miniMode && WindowState == FormWindowState.Normal)
            //miniTimer.Enabled = true;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //if (this.Top == 0 && WindowState == FormWindowState.Normal)
            //{
            //    Point p = Control.MousePosition;
            //    if (p.X < this.Left || p.X > this.Left + this.Width || p.Y > this.Top + this.Height)
            //        miniTimer.Enabled = true;
            //}
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //搜索框

            g = e.Graphics;
            Bmp = ResClass.GetImgRes("main_png_bkg");
            g.DrawImage(Bmp, new Rectangle(0, 0, 5, 121), 5, 5, 5, 121, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, 0, this.Width - 10, 121), 10, 5, Bmp.Width - 20, 121, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, 0, 5, 121), Bmp.Width - 10, 5, 5, 121, GraphicsUnit.Pixel);

            g.DrawImage(Bmp, new Rectangle(0, 121, 2, this.Height - 182), 5, 126, 2, Bmp.Height - 192, GraphicsUnit.Pixel);
            //g.FillRectangle(Brushes.White, 2, 121, this.Width - 4, this.Height - 182);
            //g.DrawImage(Bmp, new Rectangle(2, 121, this.Width - 4, this.Height - 182), 7, 126, Bmp.Width - 14, Bmp.Height - 192, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 2, 121, 2, this.Height - 182), Bmp.Width - 7, 126, 2, Bmp.Height - 192, GraphicsUnit.Pixel);

            g.DrawImage(Bmp, new Rectangle(0, this.Height - 61, 5, 61), 5, Bmp.Height - 66, 5, 61, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(5, this.Height - 61, this.Width - 10, 61), 10, Bmp.Height - 66, Bmp.Width - 20, 61, GraphicsUnit.Pixel);
            g.DrawImage(Bmp, new Rectangle(this.Width - 5, this.Height - 61, 5, 61), Bmp.Width - 10, Bmp.Height - 66, 5, 61, GraphicsUnit.Pixel);

            g.DrawString(this.Text, f, titleColor, 10, 3);
            g.DrawString(NikeName, new Font(Font.FontFamily, 10F, FontStyle.Bold), titleColor, 90, 34);

            //Bmp = ResClass.GetImgRes("main_search_bkg");
            //g.DrawImage(Bmp, new Rectangle(2, 99, 9, Bmp.Height), 0, 0, 9, Bmp.Height, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(11, 99, this.Width - 22, Bmp.Height), 9, 0, Bmp.Width - 18, Bmp.Height, GraphicsUnit.Pixel);
            //g.DrawImage(Bmp, new Rectangle(this.Width - 11, 99, 9, Bmp.Height), Bmp.Width - 9, 0, 9, Bmp.Height, GraphicsUnit.Pixel);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point Point);
        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        private void timer1_Tick(object sender, EventArgs e)
        {

            //if (GetTrayIconRectangle().Contains(MousePosition))
            //{
            //    return;
            //}

            IntPtr vHandle = WindowFromPoint(Control.MousePosition);
            while (vHandle != IntPtr.Zero && vHandle != Handle)
            {
                vHandle = GetParent(vHandle);
            }
            if (vHandle == Handle)
            {
                if (TopMost == true)
                {
                    TopMost = false;
                }
                if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
                {
                    Left = 0;
                }
                if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
                {
                    Top = 0;
                }
                if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
                {
                    Left = Screen.PrimaryScreen.Bounds.Width - Width;
                }
                if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                {
                    Top = Screen.PrimaryScreen.Bounds.Height - Height;
                }
            }
            else
            {
                if (TopMost==false)
                {
                    TopMost = true;
                }
                if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
                {
                    Left = -Width + OFFSET;

                }
                if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
                {
                    Top = -Height + OFFSET;
                }
                if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
                {
                    Left = Screen.PrimaryScreen.Bounds.Width - OFFSET;
                }
                //if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
                //{
                //    Top = Screen.PrimaryScreen.Bounds.Height - OFFSET;
                //}
            }
        }

        AnchorStyles anchors;
        const int OFFSET = 6;
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TOPMOST = 8;
                base.CreateParams.Parent = GetDesktopWindow();
                base.CreateParams.ExStyle |= WS_EX_TOPMOST;
                return base.CreateParams;
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            const int WM_MOVING = 534;
            switch (m.Msg)
            {
                case WM_MOVING:
                        int left = Marshal.ReadInt32(m.LParam, 0);
                        int top = Marshal.ReadInt32(m.LParam, 4);
                        int right = Marshal.ReadInt32(m.LParam, 8);
                        int bottom = Marshal.ReadInt32(m.LParam, 12);
                        left = Math.Min(Math.Max(0, left), Screen.PrimaryScreen.Bounds.Width - Width);
                        top = Math.Min(Math.Max(0, top), Screen.PrimaryScreen.Bounds.Height - Height);
                        right = Math.Min(Math.Max(Width, right), Screen.PrimaryScreen.Bounds.Width);
                        bottom = Math.Min(Math.Max(Height, bottom), Screen.PrimaryScreen.Bounds.Height);
                        Marshal.WriteInt32(m.LParam, 0, left);
                        Marshal.WriteInt32(m.LParam, 4, top);
                        Marshal.WriteInt32(m.LParam, 8, right);
                        Marshal.WriteInt32(m.LParam, 12, bottom);
                        anchors = AnchorStyles.None;
                        if (left <= OFFSET)
                        {
                            anchors |= AnchorStyles.Left;
                        }
                        if (top <= OFFSET)
                        {
                            anchors |= AnchorStyles.Top;
                        }
                        if (bottom >= Screen.PrimaryScreen.Bounds.Height - OFFSET)
                        {
                            anchors |= AnchorStyles.Bottom;
                        }
                        if (right >= Screen.PrimaryScreen.Bounds.Width - OFFSET)
                        {
                            anchors |= AnchorStyles.Right;
                        }
                        timer1.Enabled = anchors != AnchorStyles.None;

                    break;
                case Win32.WM_NCPAINT:
                    break;
                case Win32.WM_NCACTIVATE:
                    if (m.WParam == (IntPtr)0)
                    {
                        m.Result = (IntPtr)1;
                    }
                    if (m.WParam == (IntPtr)2097152)
                    {
                        m.Result = (IntPtr)1;
                    }
                    break;
                case Win32.WM_NCCALCSIZE:
                    break;
                case Win32.WM_NCLBUTTONDOWN:
                    description.Focus();
                    skinPanel_Leave(null, null);
                    base.WndProc(ref m);
                    break;
                case Win32.WM_LBUTTONDOWN:
                    description.Focus();
                    skinPanel_Leave(null, null);
                    base.WndProc(ref m);
                    break;
                case Win32.WM_SYSCOMMAND:
                    if (m.WParam.ToInt32() == Win32.SC_MAXIMIZE || m.WParam.ToInt32() == Win32.SC_MAXIMIZE + 2)
                    {
                        if (!isOneLoad)
                        {
                            oldSize = this.Size;
                        }
                    }
                    else if (m.WParam == (IntPtr)Win32.SC_RESTORE || m.WParam.ToInt32() == Win32.SC_RESTORE + 2)
                    {
                        if (!isOneLoad)
                        {
                            this.Size = oldSize;
                        }
                    }
                    else if (m.WParam == (IntPtr)Win32.SC_MINIMIZE || m.WParam.ToInt32() == Win32.SC_MINIMIZE + 2)
                    {
                        if (oldSize.Width == 0)
                            oldSize = this.Size;
                    }
                    else if (m.WParam == (IntPtr)Win32.SC_CLOSE || m.WParam.ToInt32() == Win32.SC_CLOSE + 2)
                    {
                        Application.Exit();
                    }
                    base.WndProc(ref m);
                    break;
                case Win32.WM_NCHITTEST:
                    if (!miniMode)
                    {
                        Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        vPoint = PointToClient(vPoint);
                        if (Top != 0)
                        {
                            if (vPoint.X <= 5)
                            {
                                if (vPoint.Y <= 5)
                                    m.Result = (IntPtr)Win32.HTTOPLEFT;
                                else if (vPoint.Y >= Height - 5)
                                    m.Result = (IntPtr)Win32.HTBOTTOMLEFT;
                                else m.Result = (IntPtr)Win32.HTLEFT;
                            }
                            else if (vPoint.X >= Width - 5)
                            {
                                if (vPoint.Y <= 5)
                                    m.Result = (IntPtr)Win32.HTTOPRIGHT;
                                else if (vPoint.Y >= Height - 5)
                                    m.Result = (IntPtr)Win32.HTBOTTOMRIGHT;
                                else m.Result = (IntPtr)Win32.HTRIGHT;
                            }
                            else if (vPoint.Y <= 5)
                            {
                                m.Result = (IntPtr)Win32.HTTOP;
                            }
                            else if (vPoint.Y >= Height - 5)
                                m.Result = (IntPtr)Win32.HTBOTTOM;
                        }
                        if (((vPoint.Y < 121 && vPoint.Y > 5) || (vPoint.Y > Height - 61 && vPoint.Y < Height - 5)) && (vPoint.X > 5 && vPoint.X < Width - 5))
                            m.Result = (IntPtr)Win32.HTCAPTION;
                    }
                    OnMouseEnter(null);
                    break;
                default:
                    try
                    {
                        base.WndProc(ref m);
                    }
                    catch (Exception ex)
                    {
                        //base.WndProc(ref m);
                        System.Diagnostics.Trace.WriteLine(ex.Message);
                    }

                    break;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResizeControl();
            if (!isOneLoad)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 5, 5);
                    Win32.SetWindowRgn(this.Handle, Rgn, true);
                }
                else if (this.WindowState == FormWindowState.Maximized)
                {
                    int Rgn = Win32.CreateRoundRectRgn(0, 0, this.Width + 1, this.Height + 1, 0, 0);
                    Win32.SetWindowRgn(this.Handle, Rgn, true);
                }
            }
            Size size = Screen.PrimaryScreen.WorkingArea.Size;
            MaximizedBounds = new Rectangle(0, 0, size.Width, size.Height - 1);
            //this.Invalidate();
            GC.Collect();
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            base.OnResizeEnd(e);
            oldSize = this.Size;
        }

        #endregion

        #region 其他操作

        private void ResizeControl()
        {
            if (this.ControlBox)
            {
                this.ButtonClose.Left = this.Width - ButtonClose.Width;
            }

            if (this.MinimizeBox)
            {
                if (this.MaximizeBox)
                {
                    this.ButtonMax.Left = ButtonClose.Left - ButtonMax.Width;
                    this.ButtonMin.Left = ButtonMax.Left - ButtonMin.Width;
                    xwidth = ButtonMin.Left;
                }
                else
                {
                    this.ButtonMin.Left = ButtonClose.Left - ButtonMin.Width;
                    xwidth = ButtonMin.Left;
                }
            }
            else
            {
                xwidth = ButtonClose.Left;
            }
            color_Btn.Left = Width - color_Btn.Width - 3;

            int tw = (Width - 4) / 4;
            fd_btn.Width = tw;
            gp_btn.Left = fd_btn.Left + fd_btn.Width;
            gp_btn.Width = tw;
            nt_btn.Left = gp_btn.Left + gp_btn.Width;
            nt_btn.Width = tw;
            lt_btn.Left = nt_btn.Left + nt_btn.Width;
            lt_btn.Width = Width - lt_btn.Left - 2;

            //search_Btn.Left = Width - search_Btn.Width - 3;
            friendListView.Size = groupListView.Size = pal_tree.Size = lastListView.Size = new Size(this.Width - 4, this.Height - 211 + 50);
            //friendListView.Size = groupListView.Size = pal_tree.Size = lastListView.Size = new Size(this.Width - 4, this.Height - 211);//设置 好友列表区的大小

            menu_Btn.Top = Height - menu_Btn.Height - 10;

            tools_Btn.Top = Height - tools_Btn.Height - 10;
            message_Btn.Top = Height - message_Btn.Height - 10;
            find_Btn.Top = Height - find_Btn.Height - 10;
        }

        private void LoadShadList()
        {
            for (int i = 0; i < shadlist.Count; i++)
            {
                //QQPictureBox pic = new QQPictureBox();
                //pic.Texts = shadlist[i];
                //pic.SizeMode = PictureBoxSizeMode.AutoSize;
                //if (Directory.Exists(shadlist[i]))
                //    pic.Image = Image.FromFile(shadlist[i] + "\\preview.png");
                //else
                //    pic.Image = ResClass.GetImgRes(shadlist[i]);
                //if (i < 7)
                //{
                //    pic.Left = i * 30 + 1 + i;
                //    pic.Top = 1;
                //}
                //else
                //{
                //    pic.Left = (i - 7) * 30 + 1 + (i - 7);
                //    pic.Top = 43;
                //}
                //pic.MouseEnter += new EventHandler(pic_MouseEnter);
                //pic.MouseLeave += new EventHandler(pic_MouseLeave);
                //pic.MouseClick += new MouseEventHandler(pic_MouseClick);
                //shadPanel.Controls.Add(pic);
            }
        }

        private void LoadColorList()
        {
            //for (int i = 0; i < shadlist.Count; i++)
            //{
            //    QQPictureBox pic = new QQPictureBox();
            //    pic.Texts = shadlist[i];
            //    pic.SizeMode = PictureBoxSizeMode.AutoSize;
            //    if (Directory.Exists(shadlist[i]))
            //        pic.Image = Image.FromFile(shadlist[i] + "\\preview.png");
            //    else
            //        pic.Image = ResClass.GetResObj(shadlist[i]);
            //    if (i < 7)
            //    {
            //        pic.Left = i * 30 + 1 + i;
            //        pic.Top = 1;
            //    }
            //    else
            //    {
            //        pic.Left = (i - 7) * 30 + 1 + (i - 7);
            //        pic.Top = 43;
            //    }
            //    pic.MouseEnter += new EventHandler(pic_MouseEnter);
            //    pic.MouseLeave += new EventHandler(pic_MouseLeave);
            //    pic.MouseClick += new MouseEventHandler(pic_MouseClick);
            //    shadPanel.Controls.Add(pic);
            //}
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            //QQPictureBox pic = sender as QQPictureBox;
            //if (pic.Texts != currentSkin)
            //{
            //    if (Directory.Exists(pic.Texts))
            //    {
            //        friendListView.BgColorMode = false;
            //        this.BackgroundImage = Image.FromFile(GetRealFile(pic.Texts + "\\main"));
            //    }
            //    else
            //    {
            //        friendListView.BgColorMode = true;
            //        this.BackgroundImage = null;
            //    }
            //    currentSkin = pic.Texts;
            //    GC.Collect();
            //    skinThread = new Thread(new ParameterizedThreadStart(ChangeSkin));
            //    skinThread.Start(this.BackgroundImage);
            //}

        }

        private void ChangeSkin(object obj)
        {
            FormCollection fc = Application.OpenForms;
            foreach (System.Windows.Forms.Form f in fc)
            {
                if (f.Name != this.Name)
                    f.BackgroundImage = (Image)obj;
            }
            GC.Collect();
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).Invalidate();
            //toolTip1.Hide((sender as QQPictureBox));
        }

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            PictureBox qp = sender as PictureBox;
            Bmp = ResClass.GetImgRes("shading_highlight");
            g = qp.CreateGraphics();
            g.DrawImage(Bmp, new Rectangle(0, 0, 30, 41), 0, 0, 30, 41, GraphicsUnit.Pixel);
            Bmp.Dispose();
            g.Dispose();
            //toolTip1.Show(qp.Texts, qp);
        }

        private void miniTimer_Tick(object sender, EventArgs e)
        {
            if (miniMode)
            {
                if (this.Top != 0)
                    this.Top = 0;
                else
                {
                    miniMode = false;
                    miniTimer.Enabled = false;
                }
            }
            else
            {
                if (this.Top == -(this.Height - 3))
                {
                    miniMode = true;
                    miniTimer.Enabled = false;
                }
                else
                    this.Top = -(this.Height - 3);
            }
        }

        private void tab_MouseEnter(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.BackgroundImage = ResClass.GetImgRes("main_tab_highlight");
        }

        private void tab_MouseLeave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (SelectTab == p.Name)
                p.BackgroundImage = ResClass.GetImgRes("main_tab_check");
            else
                p.BackgroundImage = ResClass.GetImgRes("main_tab_bkg");
        }

        private void tab_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Name != SelectTab)
            {
                p.BackgroundImage = ResClass.GetImgRes("main_tab_check");
                (Controls[SelectTab] as PictureBox).BackgroundImage = ResClass.GetImgRes("main_tab_bkg");
                ChangeTabImg(p.Name, true);
                ChangeTabImg(SelectTab, false);
                ShowTab(p.Name);
                GC.Collect();
            }
        }

        private void ChangeTabImg(string name, bool select)
        {
            switch (name)
            {
                case "fd_btn":
                    if (select)
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("MainPanel_ContactMainTabButton_texture");
                    else
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("MainPanel_ContactMainTabButton_texture1");
                    break;
                case "gp_btn":
                    if (select)
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("MainPanel_GroupMainTabButton_texture");
                    else
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("MainPanel_GroupMainTabButton_texture1");
                    break;
                case "nt_btn":
                    if (select)
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("role_tabBtn_Focus");
                    else
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("role_tabBtn_Normal");
                    break;
                case "lt_btn":
                    if (select)
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("MainPanel_RecentMainTabButton_texture");
                    else
                        (Controls[name] as PictureBox).Image = ResClass.GetImgRes("MainPanel_RecentMainTabButton_texture1");
                    break;
            }
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = ResClass.GetImgRes("allbtn_highlight");
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = null;
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((PictureBox)sender).BackgroundImage = ResClass.GetImgRes("allbtn_down");
            }
        }

        private void btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ((PictureBox)sender).Invalidate();
            }
        }

        private void color_Btn_Click(object sender, EventArgs e)
        {
            if (skinPanel.Width == 0)
            {
                skinPanel.Left = this.Width - 238;
                skinPanel.Height = 140;
                skinPanel.Width = 236;
                skinPanel.BringToFront();
                skinPanel.Focus();
            }
            else
            {
                skinPanel.Width = 0;
            }
        }

        private void color_Btn_MouseEnter(object sender, EventArgs e)
        {
            if (skinPanel.Width == 0)
            {
                Bmp = ResClass.GetImgRes("allbtn_highlight");
                g = color_Btn.CreateGraphics();
                g.DrawImage(Bmp, new Rectangle(0, 0, 22, 22), 0, 0, 22, 22, GraphicsUnit.Pixel);
                Bmp.Dispose();
                g.Dispose();
            }
        }

        private void color_Btn_MouseLeave(object sender, EventArgs e)
        {
            if (skinPanel.Width == 0)
            {
                color_Btn.Invalidate();
            }
        }

        private void skinPanel_Leave(object sender, EventArgs e)
        {
            if (skinPanel.Width != 0)
            {
                skinPanel.Width = 0;
                color_Btn_MouseLeave(null, null);
            }
        }

        private void color_Btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                g = color_Btn.CreateGraphics();
                g.Clear(Color.Transparent);
                Bmp = ResClass.GetImgRes("allbtn_down");
                g.DrawImage(Bmp, new Rectangle(0, 0, 22, 22), 0, 0, 22, 22, GraphicsUnit.Pixel);
                g.DrawImage(color_Btn.Image, new Rectangle(2, 3, 18, 18), 0, 0, 18, 18, GraphicsUnit.Pixel);
                Bmp.Dispose();
                g.Dispose();
            }
        }

        private void color_Btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (skinPanel.Width == 0)
                {
                    color_Btn.Invalidate();
                }
            }
        }
        #endregion

        #region 面板切换
        private void ShowTab(string selectTab)
        {
            switch (SelectTab)
            {
                case "fd_btn":
                    friendListView.Hide();
                    switch (selectTab)
                    {
                        case "gp_btn":
                            TabChatGroup();//聊天室
                            break;
                        case "nt_btn":
                            TabTree();//组织结构
                            break;
                        case "lt_btn":
                            TabHistory();//历史聊天人
                            break;
                    }
                    break;
                case "gp_btn":
                    groupListView.Hide();
                    switch (selectTab)
                    {
                        case "fd_btn":
                            friendListView.Show();
                            break;
                        case "nt_btn"://树图
                            TabTree();
                            break;
                        case "lt_btn":
                            TabHistory();
                            break;
                    }
                    break;
                case "nt_btn":
                    pal_tree.Hide();
                    switch (selectTab)
                    {
                        case "fd_btn":
                            friendListView.Show();
                            break;
                        case "gp_btn":
                            TabChatGroup();
                            break;
                        case "lt_btn":
                            TabHistory();
                            break;
                    }
                    break;
                case "lt_btn":
                    lastListView.Hide();
                    switch (selectTab)
                    {
                        case "fd_btn":
                            friendListView.Show();
                            break;
                        case "nt_btn"://树图
                            TabTree();
                            break;
                        case "gp_btn":
                            TabChatGroup();
                            break;
                    }
                    break;
            }
            SelectTab = selectTab;
        }

        /// <summary>
        /// 好友标签
        /// </summary>
        private void TabFriend()
        {

        }

        /// <summary>
        /// 聊天室
        /// </summary>
        private void TabChatGroup()
        {
            groupListView.Show();
            pal_chatGroupRef.Controls.Clear();
            Util.Services.Search.Clear();
            discoManager.DiscoverItems(new Jid(XmppCon.Server), new IqCB(OnDiscoServerResult), null);
        }

        /// <summary>
        /// 组件结构
        /// </summary>
        private void TabTree()
        {
            pal_tree.Show();

            treeView1.Nodes.Clear();
            treeView1.BackColor = Color.White;
            treeView1.Width = pal_tree.Width - 2;
            treeView1.Height = pal_tree.Height - 2;
            treeView1.Scrollable = true;
            if (filename == null || filename.Trim().Length == 0)
                filename = "new";
            //filename = "1340003997218.xml";
            IQ tree_iq = new IQ(IqType.get);
            tree_iq.Id = CSS.IM.XMPP.Id.GetNextId();
            tree_iq.Namespace = null;
            CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
            query.Attributes.Add("filename", filename);
            query.Namespace = "xmlns:org:tree";
            tree_iq.AddChild(query);
            XmppCon.IqGrabber.SendIq(tree_iq, new IqCB(TreeResulit), null);
        }

        /// <summary>
        /// 历史联系人
        /// </summary>
        private void TabHistory()
        {
            OleDbDataReader dr = null;
            try
            {
                dr = OleDb.ExSQLReDr("select Jid from ChatMessageLog where Belong='" + XmppCon.MyJID.Bare.ToString() + "' group by Jid");
                chat_history_listview.RemoveFriendAll();
                while (dr.Read())
                {

                    string jid = dr.GetString(0);

                    Friend flfriend = friendListView.Rosters[new Jid(jid).User];

                    chat_history_listview.XmppConn = XmppCon;
                    chat_history_listview.AddFriend(flfriend);
                }
            }
            catch (Exception)
            {
                dr = null;
            }
            lastListView.Show();
        }


        public void TreeResulit(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                this.Invoke(new IqCB(TreeResulit), new object[] { sender, iq, data });
            }

            try
            {

                if (treeView1.Nodes.Count != 0)
                {
                    return;
                }

                Element orgs = iq.Query.FirstChild;
                if (orgs != null)
                {
                    TreeNode root_treenode = new TreeNode(orgs.GetAttribute("orgName").ToString(), 0, 0);
                    root_treenode.Tag = iq.Query.GetAttribute("filename").ToString();

                    treeView1.Nodes.Add(root_treenode);
                    ElementList org_list = orgs.SelectElements("org");
                    MarkOrgTOList(org_list, root_treenode);
                }
            }
            catch (Exception)
            {

            }

            try
            {

                List<int> selectNodeIndex = new List<int>();
                if (Selectnode != null)
                {
                    for (TreeNode i = Selectnode; i.Parent != null; i = i.Parent)
                    {
                        selectNodeIndex.Insert(0, i.Index);
                    }

                    TreeNode selecNodeout = treeView1.Nodes[0];
                    selecNodeout.Expand();
                    for (int i = 0; i < selectNodeIndex.Count; i++)
                    {
                        selecNodeout = selecNodeout.Nodes[selectNodeIndex[i]];
                        selecNodeout.Expand();

                    }
                }
            }
            catch (Exception)
            {

            }

        }

        public void MarkOrgTOList(ElementList OrgList, TreeNode treeindex)
        {
            String loginName = "";
            String userName = "";
            for (int i = 0; i < OrgList.Count; i++)
            {

                ElementList user_list = OrgList.Item(i).SelectElements("user");
                for (int j = 0; j < user_list.Count; j++)
                {
                    if (user_list.Item(j).Attributes.Count > 0)
                    {
                        loginName = user_list.Item(j).GetAttribute("loginName");
                        userName = user_list.Item(j).GetAttribute("username");
                        TreeNode node = new TreeNode(userName, 2, 2);
                        node.Tag = loginName;
                        treeindex.Nodes.Add(node);

                    }
                }

                ElementList org_list = OrgList.Item(i).SelectElements("org");
                for (int j = 0; j < org_list.Count; j++)
                {
                    if (org_list.Item(j).Attributes.Count > 0)
                    {
                        userName = org_list.Item(j).GetAttribute("orgName");
                        TreeNode node = new TreeNode(userName, 1, 1);
                        int ctreeindex = treeindex.Nodes.Add(node);
                        MarkOrgTOList(org_list, node);
                    }
                }
            }
        }

        #endregion

        #region 控制按钮实现
        private void ButtonClose_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //    Application.Exit();
            if (e.Button == MouseButtons.Left)
            {
                if (!this.ShowInTaskbar)
                {
                    this.Hide();
                }
                else
                {
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
                }
            }
        }

        private void ButtonClose_MouseDown(object sender, MouseEventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_down");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ButtonClose.IsDisposed)
            {
                closeBmp = ResClass.GetImgRes("btn_close_normal");
                ButtonClose.Invalidate();
            }
        }

        private void ButtonClose_MouseLeave(object sender, EventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_normal");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_MouseEnter(object sender, EventArgs e)
        {
            closeBmp = ResClass.GetImgRes("btn_close_highlight");
            toolTip1.SetToolTip(ButtonClose, "关闭");
            ButtonClose.Invalidate();
        }

        private void ButtonClose_Paint(object sender, PaintEventArgs e)
        {
            if (this.ControlBox)
            {
                g = e.Graphics;
                g.DrawImage(closeBmp, new Rectangle(0, 0, closeBmp.Width, closeBmp.Height), 0, 0, closeBmp.Width, closeBmp.Height, GraphicsUnit.Pixel);
            }
        }

        private void ButtonMin_MouseEnter(object sender, EventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_highlight");
            toolTip1.SetToolTip(ButtonMin, "最小化");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseDown(object sender, MouseEventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_down");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseUp(object sender, MouseEventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_down");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseLeave(object sender, EventArgs e)
        {
            minBmp = ResClass.GetImgRes("btn_mini_normal");
            ButtonMin.Invalidate();
        }

        private void ButtonMin_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Hide();
                if (!this.ShowInTaskbar)
                {
                    this.Hide();
                }
                else
                {
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MINIMIZE, 0);
                }
            }
        }

        private void ButtonMin_Paint(object sender, PaintEventArgs e)
        {
            if (this.MinimizeBox)
            {
                g = e.Graphics;
                g.DrawImage(minBmp, new Rectangle(0, 0, minBmp.Width, minBmp.Height), 0, 0, minBmp.Width, minBmp.Height, GraphicsUnit.Pixel);
            }
        }

        private void ButtonMax_MouseDown(object sender, MouseEventArgs e)
        {
            g = ButtonMax.CreateGraphics();
            maxBmp = ResClass.GetImgRes("btn_max_down");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_down");
            }
            g.DrawImage(maxBmp, new Rectangle(0, 0, maxBmp.Width, maxBmp.Height), 0, 0, maxBmp.Width, maxBmp.Height, GraphicsUnit.Pixel);
        }

        private void ButtonMax_MouseEnter(object sender, EventArgs e)
        {
            g = ButtonMax.CreateGraphics();
            maxBmp = ResClass.GetImgRes("btn_max_highlight");
            toolTip1.SetToolTip(ButtonMax, "最大化");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_highlight");
                toolTip1.SetToolTip(ButtonMax, "还原");
            }
            g.DrawImage(maxBmp, new Rectangle(0, 0, maxBmp.Width, maxBmp.Height), 0, 0, maxBmp.Width, maxBmp.Height, GraphicsUnit.Pixel);
        }

        private void ButtonMax_MouseLeave(object sender, EventArgs e)
        {
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
            ButtonMax.Invalidate();
        }

        private void ButtonMax_MouseUp(object sender, MouseEventArgs e)
        {
            maxBmp = ResClass.GetImgRes("btn_max_normal");
            if (this.WindowState == FormWindowState.Maximized)
            {
                maxBmp = ResClass.GetImgRes("btn_restore_normal");
            }
            ButtonMax.Invalidate();
        }

        private void ButtonMax_Paint(object sender, PaintEventArgs e)
        {
            if (this.MaximizeBox)
            {
                g = e.Graphics;
                maxBmp = ResClass.GetImgRes("btn_max_normal");
                if (this.WindowState == FormWindowState.Maximized)
                {
                    maxBmp = ResClass.GetImgRes("btn_restore_normal");
                }
                g.DrawImage(maxBmp, new Rectangle(0, 0, maxBmp.Width, maxBmp.Height), 0, 0, maxBmp.Width, maxBmp.Height, GraphicsUnit.Pixel);
            }
        }

        private void ButtonMax_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Normal)
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_MAXIMIZE, 0);
                else
                    Win32.PostMessage(this.Handle, Win32.WM_SYSCOMMAND, Win32.SC_RESTORE, 0);
                this.Invalidate();
            }
        }
        #endregion

        #region 辅助函数

        private Bitmap GetImg(string ImgName)
        {
            Image srcImg = ResClass.GetImgRes(ImgName);
            return GetImg(ImgName, srcImg.Width, srcImg.Height, 0, 0, srcImg.Width, srcImg.Height);
        }

        private Bitmap GetImg(string ImgName, int i)
        {
            Image srcImg = ResClass.GetImgRes(ImgName);
            return GetImg(ImgName, srcImg.Width / 4, srcImg.Height, srcImg.Width / 4 * i, 0, srcImg.Width / 4, srcImg.Height);
        }

        private Bitmap GetImg(string ImgName, int width, int height, int sleft, int stop, int sWidth, int sHeight)
        {
            Image srcImg = ResClass.GetImgRes(ImgName);

            Bitmap newImg = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(newImg);

            graphics.DrawImage(srcImg, new Rectangle(0, 0, width, height), (sleft < 0 ? srcImg.Width + sleft : sleft), (stop < 0 ? srcImg.Height + stop : stop), (sWidth < 0 ? srcImg.Width + sWidth : sWidth), (sHeight < 0 ? srcImg.Height + sHeight : sHeight), GraphicsUnit.Pixel);
            graphics.Dispose();
            srcImg.Dispose();
            return newImg;
        }

        private string GetRealFile(string pimg)
        {
            string s1 = pimg + ".PNG";
            if (!File.Exists(s1))
            {
                s1 = pimg + ".BMP";
                if (!File.Exists(s1))
                {
                    s1 = pimg + ".jpg";
                    if (!File.Exists(s1))
                    {
                        s1 = pimg + ".jpeg";
                    }
                }
            }
            return s1;
        }

        #endregion

        #region QQ窗体事件
        private void qzone16_btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (QzoneMouseClick != null)
                QzoneMouseClick(sender, e);
            else
            {
                System.Diagnostics.Process.Start(@"http://www.6tianxia.com.cn");
            }
        }

        private void mail_btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (MailMouseClick != null)
                MailMouseClick(sender, e);
            else
            {
                System.Diagnostics.Process.Start(@"http://mail.css.com.cn");
            }
        }

        /// <summary>
        /// 搜索好友事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void search_Btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (SearchMouseClick != null)
                SearchMouseClick(sender, e);
            else
            {
            }
        }


        private void menu_Btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (MenuMouseClick != null)
                MenuMouseClick(sender, e);
            else
            {
            }
        }


        SetingForm sf = null;
        private void tools_Btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (ToolsMouseClick != null)
                ToolsMouseClick(sender, e);
            else
            {
                if (sf == null)
                {
                    sf = new SetingForm();
                    try
                    {
                        sf.Show();
                        sf.Activate();
                    }
                    catch (Exception)
                    {

                    }
                    
                }
                else
                {
                    if (sf.IsDisposed)
                    {
                        sf = new SetingForm();
                        try
                        {
                            sf.Show();
                            sf.Activate();
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        try
                        {
                            sf.Show();
                            sf.Activate();
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

            }
        }


        MessageLogForm bf = null;
        /// <summary>
        /// 消息记录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void message_Btn_MouseClick(object sender, MouseEventArgs e)
        {
            //ChatMessageBox.GetInstance(this).Height = ChatMessageBox.GetInstance(this).Height + 10;

            //if (MessageMouseClick != null)
            //    MessageMouseClick(sender, e);
            //else
            //{


            //}

            if (bf == null)
            {
                bf = new MessageLogForm();
                bf.XmppConn = XmppCon;
                bf.BackgroundImage = this.BackgroundImage;
                bf.Text = "消息管理器";
                bf.ShowIcon = false;
                bf.AllowMove = false;
                bf.ShowInTaskbar = false;
                //bf.Size = new Size(500, 300);
                try
                {
                    bf.Show();
                }
                catch (Exception)
                {

                }
                
            }
            else
            {
                if (bf.IsDisposed)
                {
                    bf = new MessageLogForm();
                    bf.XmppConn = XmppCon;
                    bf.BackgroundImage = this.BackgroundImage;
                    bf.Text = "消息管理器";
                    bf.ShowIcon = false;
                    bf.AllowMove = false;
                    bf.ShowInTaskbar = false;
                    //bf.Size = new Size(500, 300);
                    try
                    {
                        bf.Show();
                    }
                    catch (Exception)
                    {

                    }
                    
                }
                else
                {
                    try
                    {
                        bf.Show();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        FindFriendForm _FindFriendForm = null;
        /// <summary>
        /// 查找好友事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void find_Btn_MouseClick(object sender, MouseEventArgs e)
        {
            //for (int i = 0; i < 50; i++)
            //{
            //    new TChatForm().Show();
            //}
            if (FindMouseClick != null)
                FindMouseClick(sender, e);
            else
            {
                if (_FindFriendForm == null)
                {
                    _FindFriendForm = new FindFriendForm(XmppCon);
                    try
                    {
                        _FindFriendForm.Show();
                    }
                    catch (Exception)
                    {


                    }
                }
                else if (_FindFriendForm.IsDisposed)
                {
                    _FindFriendForm = new FindFriendForm(XmppCon);
                        try
                        {
                            _FindFriendForm.Show();
                        }
                        catch (Exception)
                        {


                        }
                }
                else
                {
                    try
                    {
                        _FindFriendForm.Show();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 头像事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userImg_MouseEnter(object sender, EventArgs e)
        {
            userImg.BackgroundImage = ResClass.GetImgRes("Padding4Select");

        }

        /// <summary>
        /// 头像事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userImg_MouseLeave(object sender, EventArgs e)
        {
            userImg.BackgroundImage = ResClass.GetImgRes("Padding4Normal");
        }

        #region 个人心心情标签 预留
        /// <summary>
        /// 个人心情单击事件预留
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void description_MouseClick(object sender, MouseEventArgs e)
        {
            QQtextBox tb = new QQtextBox();
            tb.MaxLength = 100;
            tb.BorderStyle = BorderStyle.Fixed3D;
            tb.Text = description.Text;
            tb.Location = new Point(description.Left, description.Top - 2);
            tb.Size = new Size(description.Width, description.Height + 4);
            tb.Leave += new EventHandler(tb_Leave);
            tb.KeyDown += new KeyEventHandler(tb_KeyDown);
            Controls.Add(tb);
            tb.BringToFront();
            tb.Focus();
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                description.Focus();
        }

        private void tb_Leave(object sender, EventArgs e)
        {
            bool change = true;

            QQtextBox tb = sender as QQtextBox;
            if (description.Text==tb.Text)
            {
                change = false;
            }
            else
            {
                change = true;
                description.Text = tb.Text;
            }
            tb.Dispose();
            Controls.Remove(tb);
            
            if (change)
            {
                Util.vcard.Description = description.Text;
                VcardIq viq = new VcardIq(IqType.set, null, new Jid(XmppCon.MyJID.User), Util.vcard);
                XmppCon.IqGrabber.SendIq(viq, new IqCB(SaveVcardResult), null); 
            }
        }

        private void SaveVcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(SaveVcardResult), new object[] { sender, iq, data });
                return;
            }

            if (iq.Type == IqType.result)
            {
                Util.VcardChangeEventMethod();//通知名片更新事件
                MsgBox.Show(this, "CSS&IM", "备注保存成功！", MessageBoxButtons.OK);
            }
            else
            {
                MsgBox.Show(this, "CSS&IM", "备注保存失败", MessageBoxButtons.OK);
            }
        }
        #endregion

        public string NikeName
        {
            get
            {
                return _NikeName;
            }
            set
            {
                _NikeName = value;
                this.Invalidate(new Rectangle(90, 34, 60, 20));
            }
        }

        private void select_shad_Click(object sender, EventArgs e)
        {
            if (!shadPanel.Visible)
            {
                colorPanel.Hide();
                shadPanel.Show();
            }
        }

        private void select_color_Click(object sender, EventArgs e)
        {
            if (!colorPanel.Visible)
            {
                shadPanel.Hide();
                colorPanel.Show();
            }
        }
        #endregion


        private void QQMainForm_Load(object sender, EventArgs e)
        {

            Util.VcardChangeEvent += new Util.ObjectHandler(Util_VcardChangeEvent);

            OverTrayTimer.Interval = 250;
            OverTrayTimer.Enabled = true;
            OverTrayTimer.Tick += new EventHandler(OverTrayTimer_Tick);

            stateButton1.State = 1;
            //this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.ShowInTaskbar = false;

            XmppCon.SocketConnectionType = CSS.IM.XMPP.net.SocketConnectionType.Direct;//使用TCP/IP的方式进行连接
            //XmppCon.ClientSocket.ConnectTimeout = 1000;
            XmppCon.OnReadXml += new XmlHandler(XmppCon_OnReadXml);//发送数据的XML
            XmppCon.OnWriteXml += new XmlHandler(XmppCon_OnWriteXml);//接收数据的XML


            XmppCon.OnRosterStart += new ObjectHandler(XmppCon_OnRosterStart);//获取联系人列表
            XmppCon.OnRosterEnd += new ObjectHandler(XmppCon_OnRosterEnd);//获取列表结束
            XmppCon.OnRosterItem += new CSS.IM.XMPP.XmppClientConnection.RosterHandler(XmppCon_OnRosterItem);//得到列表项目

            //XmppCon.OnAgentStart += new ObjectHandler(XmppCon_OnAgentStart);//代理
            //XmppCon.OnAgentEnd += new ObjectHandler(XmppCon_OnAgentEnd);
            //XmppCon.OnAgentItem += new agsXMPP.XmppClientConnection.AgentHandler(XmppCon_OnAgentItem);

            XmppCon.OnLogin += new ObjectHandler(XmppCon_OnLogin);//登录
            XmppCon.OnClose += new ObjectHandler(XmppCon_OnClose);
            XmppCon.OnError += new ErrorHandler(XmppCon_OnError);//错误
            XmppCon.OnPresence += new PresenceHandler(XmppCon_OnPresence);//状态
            XmppCon.OnMessage += new MessageHandler(XmppCon_OnMessage);//消息
            XmppCon.OnIq += new IqHandler(XmppCon_OnIq);//ID

            //XmppCon.OnAuthError += new XmppElementHandler(XmppCon_OnAuthError);//登录错误

            XmppCon.OnSocketError += new ErrorHandler(XmppCon_OnSocketError);
            //XmppCon.OnStreamError += new XmppElementHandler(XmppCon_OnStreamError);


            XmppCon.OnReadSocketData += new CSS.IM.XMPP.net.BaseSocket.OnSocketDataHandler(ClientSocket_OnReceive);//查看socket接收数据
            XmppCon.OnWriteSocketData += new CSS.IM.XMPP.net.BaseSocket.OnSocketDataHandler(ClientSocket_OnSend);//查看socket发送数据

            //XmppCon.ClientSocket.OnValidateCertificate += new System.Net.Security.RemoteCertificateValidationCallback(ClientSocket_OnValidateCertificate);//SSL验证


            //XmppCon.OnXmppConnectionStateChanged += new XmppConnectionStateHandler(XmppCon_OnXmppConnectionStateChanged);
            //XmppCon.OnSaslStart += new SaslEventHandler(XmppCon_OnSaslStart);

            //discoManager = new DiscoManager(XmppCon);

            XmppCon.OnAuthError += new XmppElementHandler(XmppCon_OnAuthError);
            discoManager = new DiscoManager(XmppCon);

            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("UserRemark", null, typeof(Settings.TUserRemark));//用户备注表

            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("Login", null, typeof(Settings.Login));//注册login类
            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("Paths", null, typeof(Settings.Paths));//注册Paths类
            CSS.IM.XMPP.Factory.ElementFactory.AddElementType("ServerInfo", null, typeof(Settings.ServerInfo));//注册ServerInfo类


            //XmppCon.Server = "DUBIN-PC";
            //XmppCon.Server = "192.168.0.44";
            //XmppCon.Server = "192.168.0.102";


            //XmppCon.Server = System.Net.Dns.GetHostByAddress("6.136.8.14").HostName.ToString();
            //XmppCon.Server = "192.168.0.36";


            //XmppCon.Username = "ccbbaa";
            //XmppCon.Password = "1";
            XmppCon.Resource = "CSS.IM.App";
            XmppCon.Priority = 10;
            //XmppCon.UseSSL=false;
            XmppCon.AutoResolveConnectServer = true;
            XmppCon.UseCompression = false;
            //XmppCon.RegisterAccount = true;  //是否注册.
            XmppCon.EnableCapabilities = true;
            XmppCon.ClientVersion = "1.0";
            XmppCon.Capabilities.Node = "http://www.css.com.cn/";



            friendListView.friend_qcm_MouseClickEvent += new QQListViewEx.friend_qcm_MouseClick_Delegate(friendListView_friend_qcm_MouseClickEvent);//好友右单击事件
            friendListView.OpenChatEvent += new QQListViewEx.delegate_openChat(friendListView_OpenChatEvent);

            //System.IO.MemoryStream memory = new MemoryStream();
            //this.Icon.Save(memory);

            //FileStream fileStream = new FileStream("C:\\a.jpg", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            //fileStream.Write(memory.ToArray(), 0, memory.ToArray().Length);
            //fileStream.Flush();
            //fileStream.Close();

            timer1.Enabled = false;
            timer1.Interval = 200;
            ShowInTaskbar = false;

            ChatMessageBox.GetInstance(this).OpenChatEvent += new ChatMessageBox.delegate_openChat(QQMainForm_OpenChatEvent);

            try
            {
                login = new LoginFrom(this, false);
                login.ISAutoLogin = true;
                login.Login_Event += new LoginFrom.LoginDelegate(login_Login_Event);
                login.Show();
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 名片改变事件
        /// </summary>
        /// <param name="sender"></param>
        void Util_VcardChangeEvent(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(Util_VcardChangeEvent), new object[] { sender });
                return;
            }

            XmppCon.SendMyPresence();

            try
            {
                description.Text = Util.vcard.Description;
                userImg.Image = Util.vcard.Photo.Image;
            }
            catch (Exception)
            {

            }
            

        }

        /// <summary>
        /// 通过消息盒子打开消息窗体
        /// </summary>
        /// <param name="friend"></param>
        /// <param name="sender"></param>
        /// <param name="CName"></param>
        void QQMainForm_OpenChatEvent(Friend friend, Jid sender, string CName)
        {
            RosterItem ritem = new RosterItem(sender);
            friend.Ritem = ritem;
            //throw new NotImplementedException();
            //friendListView_OpenChatEvent(friend);
            if (!Util.ChatForms.ContainsKey(friend.Ritem.Jid.Bare.ToString()))
            {
                friendListView.UpdateFriendFlicker(friend.NikeName);
                ChatForm chat = new ChatForm(friend.Ritem.Jid, XmppCon, friend.NikeName);

                Friend flfriend = friendListView.Rosters[friend.NikeName];
                if (flfriend!=null)
                {
                    chat.UpdateFriendOnline(flfriend.IsOnline);
                }

                if (msgBox.ContainsKey(friend.Ritem.Jid.Bare.ToString()))
                {
                    chat.FristMessage(msgBox[friend.Ritem.Jid.Bare.ToString()]);
                    msgBox.Remove(friend.Ritem.Jid.Bare.ToString());
                }
                chat.Show();
            }
            else
            {
                try
                {
                    ChatForm chatform = Util.ChatForms[friend.Ritem.Jid.ToString()] as ChatForm;
                    chatform.WindowState = FormWindowState.Normal;
                    chatform.Activate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (ChatMessageBox.GetInstance(this).FrienMessageCount() > 0)
                Debug.WriteLine("?");
            else
            {
                notifyIcon1.Icon = CSS.IM.UI.Properties.Resources.Iico;
                timer_notifyIco.Enabled = false;
            }

        }

        /// <summary>
        /// 聊天窗口打开事件
        /// </summary>
        /// <param name="sender"></param>
        public void friendListView_OpenChatEvent(Friend sender)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new QQListViewEx.delegate_openChat(friendListView_OpenChatEvent), new object[] { sender });
                return;
            }

            if (!Util.ChatForms.ContainsKey(sender.Ritem.Jid.ToString()))
            {
                try
                {

                    ChatForm chat = new ChatForm(sender.Ritem.Jid, XmppCon, sender.NikeName);
                    chat.UpdateFriendOnline(sender.IsOnline);//设置好友在线状态

                    ChatMessageBox.GetInstance(this).RemoveFriend(sender.Ritem.Jid);
                    if (msgBox.ContainsKey(sender.Ritem.Jid.ToString()))
                    {
                        chat.FristMessage(msgBox[sender.Ritem.Jid.ToString()]);
                        msgBox.Remove(sender.Ritem.Jid.ToString());
                    }
                    chat.Show();

                    if (ChatMessageBox.GetInstance(this).FrienMessageCount() > 0)
                        Debug.WriteLine("?");
                    else
                    {
                        notifyIcon1.Icon = CSS.IM.UI.Properties.Resources.Iico;
                        timer_notifyIco.Enabled = false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                try
                {
                    ChatForm chatform = Util.ChatForms[sender.Ritem.Jid.ToString()] as ChatForm;
                    chatform.WindowState = FormWindowState.Normal;
                    chatform.Activate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 查看联系人详细信息事件、删除、备注
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="item"></param>
        public void friendListView_friend_qcm_MouseClickEvent(object sender, Friend item, String type)
        {

            CSS.IM.XMPP.protocol.iq.roster.RosterItem ritem = friendListView.SelectedFriend.Ritem;

            switch (type)
            {
                case "vcar":
                    VcardForm vcardForm = new VcardForm(ritem.Jid, XmppCon);
                    try
                    {
                        vcardForm.Show();
                    }
                    catch (Exception)
                    {
   
                    }
                    
                    break;
                case "chat":
                    if (!Util.ChatForms.ContainsKey(item.Ritem.Jid.ToString()))
                    {
                        try
                        {
                            ChatForm chat = new ChatForm(item.Ritem.Jid, XmppCon, item.NikeName);
                            chat.UpdateFriendOnline(item.IsOnline);//设置好友在线状态
                            chat.Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        try
                        {
                            ChatForm chatform = Util.ChatForms[item.Ritem.Jid.ToString()] as ChatForm;
                            chatform.WindowState = FormWindowState.Normal;
                            chatform.Activate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;
                case "dele":
                    Jid jid = item.Ritem.Jid;
                    Friend friend = new Friend();
                    friend.NikeName = item.Ritem.Jid.User;
                    friend.Description = "";//心情
                    friend.HeadIMG = "big1";
                    friend.IsSysHead = true;
                    friend.State = 1;

                    if (item.Ritem.GetGroups().Count > 0)
                    {
                        CSS.IM.XMPP.protocol.Base.Group g = (CSS.IM.XMPP.protocol.Base.Group)item.Ritem.GetGroups().Item(0);
                        int groupID = 0;

                        foreach (var groups in friendListView.Groups)
                        {
                            if (groups.Value.Title == g.Name)
                            {
                                groupID = groups.Value.Id;
                            }
                        }
                        //if (groupID == 0)
                        //{
                        //    friendListView.AddGroup(g.Name);
                        //    groupID = friendListView.Groups[g.Name].Id;
                        //    friendListView.UpdateLayout(3, groupID);

                        //}
                        friend.GroupID = groupID;
                        friend.GroupName = g.Name;
                    }
                    else
                    {
                        //离线联系人
                        Group group = friendListView.Groups["我的联系人"];
                        friend.GroupID = group.Id;
                        friend.GroupName = group.Title;
                    }
                    friend.Ritem = item.Ritem;
                    friend.IsOnline = false;

                    DialogResult msgResult=MsgBox.Show(this, "CSS&IM", "确认要删除联系人么？", MessageBoxButtons.YesNo);

                    if (msgResult==DialogResult.Yes)
                    {
                        friendListView.RemoveFriend(friend);


                        try
                        {
                            RosterIq riq = new RosterIq();
                            riq.Type = IqType.set;
                            XmppCon.RosterManager.RemoveRosterItem(jid);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    break;
                case "move":
                    MoveFriendGroup _MoveFriendGroup = new MoveFriendGroup();
                    Dictionary<string, Group> group_args = friendListView.Groups;
                    string[] strvalue = new string[group_args.Count];
                    int index = 0;
                    foreach (String keystr in group_args.Keys)
                    {
                        Group groupargs = group_args[keystr];
                        strvalue[index] = groupargs.Title;
                        index++;

                    }

                    _MoveFriendGroup.basicComboBox1.Items = strvalue;
                    _MoveFriendGroup.basicComboBox1.SelectIndex = 0;
                    DialogResult reslut = _MoveFriendGroup.ShowDialog();

                    String groupupdate = _MoveFriendGroup.basicComboBox1.SelectItem.ToString();

                    String name_move = friendListView.SelectedFriend.Ritem.Jid.User;
                    if (reslut == DialogResult.Yes)
                    {

                        foreach (String user_key in friendListView.Rosters.Keys)
                        {
                            if (friendListView.Rosters[user_key].Ritem.Jid.User == name_move)
                            {


                                Friend friend_old = friendListView.Rosters[user_key];
                                friendListView.RemoveFriend(friendListView.Rosters[user_key].Ritem.Jid.User);

                                Group fgroup = friendListView.Groups[groupupdate];
                                friend_old.GroupID = fgroup.Id;
                                friend_old.GroupName = fgroup.Title;

                                friendListView.AddFriend(friend_old);

                                friendListView.UpdateLayout(3, 0);
                                break;

                            }
                        }


                        CSS.IM.XMPP.protocol.Base.Group group_move = new CSS.IM.XMPP.protocol.Base.Group(groupupdate);
                        CSS.IM.XMPP.protocol.Base.Item item_move = new CSS.IM.XMPP.protocol.Base.Item();
                        item_move.Namespace = null;
                        item_move.AddChild(group_move);
                        item_move.SetAttribute("jid", friendListView.SelectedFriend.Ritem.Jid);
                        item_move.SetAttribute("subscripton", "from");

                        CSS.IM.XMPP.protocol.Base.Query query_move = new CSS.IM.XMPP.protocol.Base.Query();
                        query_move.Namespace = CSS.IM.XMPP.Uri.IQ_ROSTER;
                        query_move.AddChild(item_move);

                        IQ iq = new IQ(IqType.set);
                        iq.GenerateId();
                        iq.Namespace = null;
                        iq.AddChild(query_move);

                        XmppCon.IqGrabber.SendIq(iq);
                    }
                    break;
                case "g_dele":

                    break;
                case "remark":

                    //RemarkFriendGroup _RemarkFriendGroup = new RemarkFriendGroup();
                    //Dictionary<string, Group> group_args = friendListView.Groups;
                    //string[] strvalue = new string[group_args.Count];
                    //int index = 0;
                    //foreach (String keystr in group_args.Keys)
                    //{
                    //    Group groupargs = group_args[keystr];
                    //    strvalue[index] = groupargs.Title;
                    //    index++;

                    //}

                    //_MoveFriendGroup.basicComboBox1.Items = strvalue;
                    //_MoveFriendGroup.basicComboBox1.SelectIndex = 0;
                    //DialogResult reslut = _MoveFriendGroup.ShowDialog();

                    //String groupupdate = _MoveFriendGroup.basicComboBox1.SelectItem.ToString();

                    //String name_move = friendListView.SelectedFriend.Ritem.Jid.User;
                    //if (reslut == DialogResult.Yes)
                    //{

                    //    foreach (String user_key in friendListView.Rosters.Keys)
                    //    {
                    //        if (friendListView.Rosters[user_key].Ritem.Jid.User == name_move)
                    //        {


                    //            Friend friend_old = friendListView.Rosters[user_key];
                    //            friendListView.RemoveFriend(friendListView.Rosters[user_key].Ritem.Jid.User);

                    //            Group fgroup = friendListView.Groups[groupupdate];
                    //            friend_old.GroupID = fgroup.Id;
                    //            friend_old.GroupName = fgroup.Title;

                    //            friendListView.AddFriend(friend_old);

                    //            friendListView.UpdateLayout(3, 0);
                    //            break;

                    //        }
                    //    }


                    //    CSS.IM.XMPP.protocol.Base.Group group_move = new CSS.IM.XMPP.protocol.Base.Group(groupupdate);
                    //    CSS.IM.XMPP.protocol.Base.Item item_move = new CSS.IM.XMPP.protocol.Base.Item();
                    //    item_move.Namespace = null;
                    //    item_move.AddChild(group_move);
                    //    item_move.SetAttribute("jid", friendListView.SelectedFriend.Ritem.Jid);
                    //    item_move.SetAttribute("subscripton", "from");

                    //    CSS.IM.XMPP.protocol.Base.Query query_move = new CSS.IM.XMPP.protocol.Base.Query();
                    //    query_move.Namespace = CSS.IM.XMPP.Uri.IQ_ROSTER;
                    //    query_move.AddChild(item_move);

                    //    IQ iq = new IQ(IqType.set);
                    //    iq.GenerateId();
                    //    iq.Namespace = null;
                    //    iq.AddChild(query_move);

                    //    XmppCon.IqGrabber.SendIq(iq);
                    break;
                default:
                    break;
            }


        }

        #region XMPP事件


        /// <summary>
        /// 登录窗体登录返回事件
        /// </summary>
        /// <param name="user"></param>
        private void login_Login_Event(User user)
        {

            XmppCon.Username = user.UserName;
            XmppCon.Password = user.PassWord;

            waiting = new LoginWaiting();
            try
            {
                waiting.Show();
            }
            catch (Exception)
            {

            }

            Document doc_login = new Document();
            Settings.Verify settings = new Settings.Verify();
            doc_login.LoadFile(CSS.IM.UI.Util.Path.SettingsFilename);
            Settings.ServerInfo serverInfo = serverInfo = doc_login.RootElement.SelectSingleElement(typeof(Settings.ServerInfo)) as Settings.ServerInfo;

            if (serverInfo.ServerPort == null || serverInfo.ServerIP == null)
            {
                MsgBox.Show(waiting, "CSS&IM", "服务器地址错误！", MessageBoxButtons.OK);
                waiting.Hide();
                LogOut(false, false);
                return;
            }

            XmppCon.Port = int.Parse(serverInfo.ServerPort.ToString());
            XmppCon.Server = serverInfo.ServerIP;
            this.Hide();
            this.ShowInTaskbar = false;
            new Thread(new ThreadStart(OpenSocket)).Start();
            
        }

        public void OpenSocket()
        {
            try
            {
                //XmppCon.Server = System.Net.Dns.GetHostByAddress(XmppCon.Server).HostName.ToString();
                XmppCon.Open();
                Thread.Sleep(1000);
            }
            catch (Exception)
            {
                //MessageBox.Show("b");
                this.Invoke(new XmppSocketOpenDelegate(XmppSocketOpenMedhot));
            }
            finally
            {
//MessageBox.Show("c");
            }
            
        }

        public void XmppSocketOpenMedhot()
        {

            if (InvokeRequired)
            {
                this.Invoke(new XmppSocketOpenDelegate(XmppSocketOpenMedhot));
            }

            if (XmppCon.XmppConnectionState != XmppConnectionState.Disconnected)
                XmppCon.Close();

            //Program.IsLogin = true;
            MsgBox.Show(waiting, "CSS&IM", "登录失败，服务器无法识别！", MessageBoxButtons.OK);

            waiting.Close();

            try
            {
                LogOut(false, false);
            }
            catch (Exception)
            {
                Application.Exit();
            }
        }

        public void OnSearchFieldsResult(object sender, IQ iq, object data)
        {
            Debug.WriteLine(iq.ToString());

            if (iq.Type == IqType.result)
            {
                //String command = Console.ReadLine();
            }
            else if (iq.Type == IqType.error)
            {
            }
        }

        private void VcardResult(object sender, IQ iq, object data)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(VcardResult), new object[] { sender, iq, data });
                return;
            }
            if (iq.Type == IqType.result)
            {
                Vcard vcard = iq.Vcard;

                Util.vcard = vcard;

                if (vcard != null)
                {
                    //Program.NikName = vcard.Nickname;
                    Photo photo = vcard.Photo;
                    if (photo != null)
                        userImg.Image = vcard.Photo.Image;
                }
                description.Text = vcard.Description;
            }
        }

        public void DiscoServer()
        {
            discoManager.DiscoverItems(new Jid(XmppCon.Server), new IqCB(OnDiscoServerResult), null);
        }

        /// <summary>
        /// Callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        public void OnDiscoServerResult(object sender, IQ iq, object data)
        {
            if (iq.Type == IqType.result)
            {
                Element query = iq.Query;
                if (query != null && query.GetType() == typeof(DiscoItems))
                {
                    DiscoItems items = query as DiscoItems;
                    DiscoItem[] itms = items.GetDiscoItems();

                    foreach (DiscoItem itm in itms)
                    {
                        if (itm.Jid != null)
                            discoManager.DiscoverInformation(itm.Jid, new IqCB(OnDiscoInfoResult), itm);
                    }
                }
            }
        }

        public void OnDiscoInfoResult(object sender, IQ iq, object data)
        {
            // <iq from='proxy.cachet.myjabber.net' to='gnauck@jabber.org/Exodus' type='result' id='jcl_19'>
            //  <query xmlns='http://jabber.org/protocol/disco#info'>
            //      <identity category='proxy' name='SOCKS5 Bytestreams Service' type='bytestreams'/>
            //      <feature var='http://jabber.org/protocol/bytestreams'/>
            //      <feature var='http://jabber.org/protocol/disco#info'/>
            //  </query>
            // </iq>

            if (iq.Type == IqType.result)
            {
                if (iq.Query is DiscoInfo)
                {
                    DiscoInfo di = iq.Query as DiscoInfo;
                    Jid jid = iq.From;
                    if (di.HasFeature(CSS.IM.XMPP.Uri.IQ_SEARCH))
                    {
                        if (!Util.Services.Search.Contains(jid))
                        {

                            if (!Util.Services.Rooms.Contains(jid))
                            {
                                if ("conference" == di.SelectElements("identity").Item(0).GetAttribute("category"))
                                {
                                    Util.Services.Rooms.Add(jid);

                                }
                            }


                            if (!Util.Services.Finds.Contains(jid))
                            {
                                if ("directory" == di.SelectElements("identity").Item(0).GetAttribute("category"))
                                {
                                    Util.Services.Finds.Add(jid);
                                }
                            }

                            Util.Services.Search.Add(jid);
                            this.Invoke(new OnChatGroupAddDelegate(OnChatGroupAdd), new object[] { jid });
                        }

                    }
                    else if (di.HasFeature(CSS.IM.XMPP.Uri.BYTESTREAMS))
                    {
                        if (!Util.Services.Proxy.Contains(jid))
                            Util.Services.Proxy.Add(jid);
                    }

                }
            }

        }

        /// <summary>
        /// 添加IM会议室事件
        /// </summary>
        /// <param name="jid"></param>
        public void OnChatGroupAdd(Jid jid)
        {
            if (Util.Services.Finds.Contains(jid))
            {
                return;
            }
            pal_chatGroupRef.Location = new Point(1, 3);
            pal_chatGroupRef.BackColor = Color.White;
            pal_chatGroupRef.Width = friendListView.Width - 2;
            pal_chatGroupRef.Height = friendListView.Height - 4;
            if (InvokeRequired)
            {
                BeginInvoke(new OnChatGroupAddDelegate(OnChatGroupAdd), new object[] { jid });
            }
            ChatGroupControl chatgroup = new ChatGroupControl();
            chatgroup.MJid = jid;
            chatgroup.TextName = jid.ToString();
            chatgroup.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right)));
            chatgroup.BackColor = this.BackColor;
            chatgroup.Location = new Point(1, 56 * pal_chatGroupRef.Controls.Count);
            chatgroup.Name = jid.ToString();
            chatgroup.Size = new Size(pal_chatGroupRef.Width - 2, 55);
            chatgroup.BackColor = Color.White;
            chatgroup.ChatGroupOpenEvent += new ChatGroupControl.ChatGroupOpenDelegate(chatgroup_ChatGroupOpenEvent);

            //friend.FriendInfo = item;
            //friend.Selecting += new FriendControl.SelectedEventHandler(friend_Selecting);
            //friend.ShowContextMenu += new FriendControl.ShowContextMenuEventHandler(friend_ShowContextMenu);
            //friend.OpenChat += new FriendControl.OpenChatEventHandler(friend_OpenChat);
            //friend.Conn = _connection;
            //friend.UpdateImage();//更新头像信息
            pal_chatGroupRef.Controls.Add(chatgroup);
            //pal_chatGroupRef.BackColor = Color.Aqua;
            ///pal_chatGroupRef.Height += 56;
            //UpdateLayout(panel_user);
        }

        private ChatGroupRoomsForm rooms = null;
        /// <summary>
        /// IM会议室打开事件
        /// </summary>
        /// <param name="jid"></param>
        void chatgroup_ChatGroupOpenEvent(Jid jid)
        {
            if (rooms == null || rooms.IsDisposed)
            {
                rooms = new ChatGroupRoomsForm(jid, XmppCon);
                //打开聊天室窗体事件
                rooms.OpenChatGroupWindowsEvent += new ChatGroupRoomsForm.OpenChatGroupWindowsDelegate(rooms_OpenChatGroupWindowsEvent);
            }
            try
            {
                rooms.Show();
            }
            catch (Exception)
            {
 
            }
            
        }

        /// <summary>
        /// 从房间列表的打开房间事件开发
        /// </summary>
        /// <param name="jid"></param>
        /// <param name="pswd"></param>
        private void rooms_OpenChatGroupWindowsEvent(Jid jid, string pswd)
        {

            if (InvokeRequired)
            {
                this.Invoke(new ChatGroupRoomsForm.OpenChatGroupWindowsDelegate(rooms_OpenChatGroupWindowsEvent), new object[] { jid, pswd });
            }

            if (!Util.GroupChatForms.ContainsKey(jid.Bare))
            {
                try
                {
                    ChatGroupForm chat = new ChatGroupForm(jid, XmppCon, XmppCon.MyJID.User);
                    chat.Initial(pswd);
                }
                catch (Exception)
                {
                    
                   
                }
                
            }
        }

        /// <summary>
        /// 获取临时组服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="iq"></param>
        /// <param name="data"></param>
        public void ChatGroupResulit(object sender, IQ iq, object data)
        {
            //Debug.WriteLine(iq.Query);
            if (iq.Type == IqType.result)
            {
                if (iq.Query is DiscoItems)
                {
                    DiscoItems items = iq.Query as DiscoItems;
                    DiscoItem[] itms = items.GetDiscoItems();

                    foreach (DiscoItem itm in itms)
                    {
                        if (itm.Jid != null)
                            //discoManager.DiscoverInformation(itm.Jid, new IqCB(OnDiscoInfoResult), itm);
                            Debug.WriteLine(itm);
                    }
                }
            }
        }

        /// <summary>
        /// 组织结构图右键的功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeContextMenu1_Paint(object sender, PaintEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag != null)
                {
                    treeContextMenu1.Enabled = true;
                }
                else
                {
                    treeContextMenu1.Enabled = false;
                }
            }
            else
            {
                treeContextMenu1.Enabled = false;
            }

        }

        /// <summary>
        ///  登录失败事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void XmppCon_OnAuthError(object sender, Element e)
        {

            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new XmppElementHandler(XmppCon_OnAuthError), new object[] { sender, e });
                return;
            }

            //if (XmppCon.XmppConnectionState != XmppConnectionState.Disconnected)
            //    XmppCon.Close();

            //Program.IsLogin = true;
            MsgBox.Show(waiting, "CSS&IM", "登录失败，用户名或密码错误！", MessageBoxButtons.OK);
            waiting.Close();
            LogOut(false, false);
            
        }

        public void ClientSocket_OnReceive(object sender, byte[] data, int count)
        {
            //Debug.WriteLine(Encoding.UTF8.GetString(data));
        }

        public void ClientSocket_OnSend(object sender, byte[] data, int count)
        {
            //Debug.WriteLine(Encoding.UTF8.GetString(data));
        }

        public void XmppCon_OnSocketError(object sender, Exception ex)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new ErrorHandler(XmppCon_OnSocketError), new object[] { sender, ex });
                return;
            }
            waiting.Close();
            string str = "Socket Error\r\n" + ex.Message + "\r\n" + ex.InnerException + "\n 请重检查网络并重新启动程序！";
            MsgBox.Show(this, "CSS&IM", str, MessageBoxButtons.OK);

            this.Invoke(new LogoutDelegate(LogOut), new object[] { false, true });
            //Application.Exit();
        }

        public void XmppCon_OnLogin(object sender)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new CSS.IM.XMPP.ObjectHandler(XmppCon_OnLogin), new object[] { sender });
                return;
            }

            Program.LocalHostIP =IPAddress.Parse(XmppCon.ClientSocket.LocalHostIP);//设置本地IP地址

            VcardIq viq = new VcardIq(IqType.get, null, new Jid(XmppCon.MyJID.User));
            XmppCon.IqGrabber.SendIq(viq, new IqCB(VcardResult), null);

            Program.UserName = XmppCon.MyJID.User;//保存登录的用户名

            notifyIcon1.Visible = true;
            waiting.Close();

            XmppCon.Show = ShowType.NONE;
            XmppCon.SendMyPresence();

            DiscoServer();//获取各种服务器

            this.NikeName = XmppCon.Username;

            this.ShowInTaskbar = false;
            this.Show();
            this.WindowState = FormWindowState.Normal;
            friendListView.Conn = XmppCon;

            friendListView.AddGroup("我的联系人");
            friendListView.UpdateLayout(3, 0);

            this.TopMost = true;
            this.Show();
            this.Activate();
            this.TopMost = false;



            //获取新的组织结构图
            IQ tree_iq = new IQ(IqType.get);
            tree_iq.Id = CSS.IM.XMPP.Id.GetNextId();
            tree_iq.Namespace = null;
            CSS.IM.XMPP.protocol.Base.Query query = new CSS.IM.XMPP.protocol.Base.Query();
            query.Attributes.Add("filename", "new");
            query.Namespace = "xmlns:org:tree";
            tree_iq.AddChild(query);
            XmppCon.IqGrabber.SendIq(tree_iq, new IqCB(TreeResulit), null);

        }

        public void XmppCon_OnIq(object sender, IQ iq)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqHandler(XmppCon_OnIq), new object[] { sender, iq });
                return;
            }


            if (iq != null)
            {
                // No Iq with query
                if (iq.HasTag(typeof(CSS.IM.XMPP.protocol.extensions.si.SI)))
                {
                    if (iq.Type == IqType.error)
                    {
                        CSS.IM.XMPP.protocol.extensions.si.SI si = iq.SelectSingleElement(typeof(CSS.IM.XMPP.protocol.extensions.si.SI)) as CSS.IM.XMPP.protocol.extensions.si.SI;

                        CSS.IM.XMPP.protocol.extensions.filetransfer.File file = si.File;
                        if (file != null)
                        {
                            // somebody wants to send a file to us
                            //if (!Util.ChatForms.ContainsKey(iq.From.Bare))//正常消息
                            //{
                            //    //RosterNode rn = rosterControl.GetRosterItem(msg.From);
                            //    //string nick = msg.From.Bare;
                            //    //if (rn != null)
                            //    //    nick = rn.Text;
                            //    ChatForm chatForm = new ChatForm(iq.From, XmppCon, iq.From.Bare);
                            //    chatForm.Show();
                            //    //chatForm.IncomingMessage(msg);
                            //    chatForm.FileTransfer(iq);
                            //}
                        }
                    }
                }
                else
                {

                }
            }
        }

        public void XmppCon_OnMessage(object sender, CSS.IM.XMPP.protocol.client.Message msg)
        {
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                Invoke(new OnMessageDelegate(XmppCon_OnMessage), new object[] { sender, msg });
                return;
            }

            // Dont handle GroupChat Messages here, they have their own callbacks in the
            // GroupChat Form
            if (msg.Type == MessageType.groupchat)
            {
                //Debug.WriteLine("创建聊天室发送了消息:"+msg.Body);
                return;
            }
            if (msg.Type == MessageType.error)
            {
                //Handle errors here
                // we dont handle them in this example
                return;
            }
            //if (msg.Type == MessageType.normal)
            //{
            //    filename = msg.Body;
            //    Debug.WriteLine(msg.ToString());
            //    return;
            //}

            // check for xData Message

            if (msg.HasTag(typeof(Data)))//如果是文件
            {
                Element e = msg.SelectSingleElement(typeof(Data));
                Data xdata = e as Data;
                if (xdata.Type == XDataFormType.form)
                {
                    //frmXData fXData = new frmXData(xdata);
                    //fXData.Text = "xData Form from " + msg.From.ToString();
                    //fXData.Show();
                }
            }
            else if (msg.HasTag(typeof(CSS.IM.XMPP.protocol.extensions.ibb.Data)))
            {
                // ignore IBB messages
                return;
            }
            else
            {
                if (msg.Body != null)
                {
                    if (msg.GetTag("subject") == "notify")//xu
                    {
                        filename = msg.Body;
                        return;
                    }
                    if (msg.From.ToString() == msg.From.Server)
                    {
                        if (CSS.IM.UI.Util.Path.ReveiveSystemNotification)//是否接收服务器消息
                        {
                            if (CSS.IM.UI.Util.Path.SystemSwitch)
                                SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.SystemPath);

                            MessageBoxForm sBox = new MessageBoxForm("系统通知", msg);
                            try
                            {
                                sBox.TopMost = true;
                                sBox.Show();
                            }
                            catch (Exception)
                            {

                            }
                            
                            string sqlstr = "insert into MessageLog (Belong,MessageType,MessageLog,[DateNow]) values ({0},{1},{2},{3})";
                            sqlstr = String.Format(sqlstr,
                                "'" + XmppCon.MyJID.Bare.ToString() + "'",
                                "'0'",
                                "'" + msg.ToString() + "'",
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                            CSS.IM.Library.Data.OleDb.ExSQL(sqlstr);
                        }
                    }
                    else
                    {
                        if (!Util.ChatForms.ContainsKey(msg.From.Bare))//查看聊天窗口是否已经打开了
                        {

                            //RosterNode rn = rosterControl.GetRosterItem(msg.From);
                            //string nick = msg.From.Bare;
                            //if (rn != null)
                            //    nick = rn.Text;
                            try
                            {
                                //string sqlstr = "insert into ChatMessageLog (Jid,[MessageLog],[DateNow])values ({0},{1},{2})";
                                //sqlstr = String.Format(sqlstr,
                                //    "'" + msg.From.Bare.ToString() + "'",
                                //    "'" + msg.ToString() + "'",
                                //     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                                //CSS.IM.Library.Data.OleDb.ExSQL(sqlstr);

                                if (msg.GetTagInt("m_type") == 0)//如果为0就是正常消息，就播放声音
                                {
                                    if (CSS.IM.UI.Util.Path.MsgSwitch)
                                        SoundPlayEx.MsgPlay(CSS.IM.UI.Util.Path.MsgPath);
                                }
                                else//代码进入到这里，说明，聊天的窗口已经关闭，接收到属于聊天窗口里面的业务不做处理
                                {
                                    //6发图片
                                    //1接收视频

                                    if (!(msg.GetTagInt("m_type")==6||msg.GetTagInt("m_type")==1))
                                    {
                                        return;    
                                    }
                                }


                                Friend flfriend = friendListView.Rosters[msg.From.User];

 
                                if (CSS.IM.UI.Util.Path.ChatOpen)
                                {
                                    //friendListView.flickerFriend(new Jid(msg.From.Bare));
                                    //ChatMessageBox.GetInstance(this).AddFriendMessage(msg.From, XmppCon, msg.From.Bare);
                                    //timer_notifyIco.Enabled = true;

                                    ChatForm chatForm = new ChatForm(msg.From, XmppCon, msg.From.Bare);
                                    try
                                    {
                                        chatForm.UpdateFriendOnline(flfriend.IsOnline);//设置好友在线状态
                                        chatForm.Show();
                                        chatForm.Activate();
                                        chatForm.IncomingMessage(msg);
                                    }
                                    catch (Exception)
                                    {
                                        
                                    }
                                    
                                }
                                else
                                {
                                    if (msgBox.ContainsKey(msg.From.Bare.ToString()))
                                    {
                                        List<CSS.IM.XMPP.protocol.client.Message> msgs = msgBox[msg.From.Bare.ToString()];
                                        msgs.Add(msg);
                                        msgBox.Remove(msg.From.Bare.ToString());
                                        msgBox.Add(msg.From.Bare.ToString(), msgs);
                                        friendListView.flickerFriend(new Jid(msg.From.Bare));

                                    }
                                    else
                                    {
                                        if (friendListView.Rosters.ContainsKey(msg.From.User))
                                        {
                                            List<CSS.IM.XMPP.protocol.client.Message> msgs = new List<XMPP.protocol.client.Message>();
                                            msgs.Add(msg);
                                            msgBox.Add(msg.From.Bare.ToString(), msgs);
                                            friendListView.flickerFriend(new Jid(msg.From.Bare));
                                            ChatMessageBox.GetInstance(this).AddFriendMessage(msg.From, XmppCon, msg.From.Bare);
                                            timer_notifyIco.Enabled = true;
                                        }
                                        else
                                        {
                                            ChatForm chatForm = new ChatForm(msg.From, XmppCon, msg.From.Bare);
                                            try
                                            {
                                                chatForm.UpdateFriendOnline(flfriend.IsOnline);//设置好友在线状态
                                                chatForm.Show();
                                                chatForm.Activate();
                                                chatForm.IncomingMessage(msg);
                                            }
                                            catch (Exception)
                                            {
                                                
                      
                                            }
                                            
                                        }

                                        //Friend item=friendListView.Rosters[msg.From.Bare.ToString()];
                                    }
                                }


                                //ChatForm chatForm = new ChatForm(msg.From, XmppCon, msg.From.Bare);
                                //chatForm.Show();
                                //chatForm.IncomingMessage(msg);
                            }
                            catch (Exception)
                            {
                                //MessageBox.Show(ex.Message);
                            }

                        }
                    }
                }
            }
        }

        public void XmppCon_OnRosterItem(object sender, CSS.IM.XMPP.protocol.iq.roster.RosterItem item)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new CSS.IM.XMPP.XmppClientConnection.RosterHandler(XmppCon_OnRosterItem), new object[] { this, item });
                return;
            }
            if (item.Jid.User == null)
            {
                return;
            }
            if (item.Subscription != SubscriptionType.remove)
            {


                Friend friend = new Friend();
                friend.NikeName = item.Jid.User;
                friend.Description = "";//心情
                friend.HeadIMG = "big194";
                friend.IsSysHead = true;
                friend.State = 1;
                friend.NameRmark = Util.getFriendReamrk(item.Jid.User);

                if (item.GetGroups().Count > 0)
                {
                    CSS.IM.XMPP.protocol.Base.Group g = (CSS.IM.XMPP.protocol.Base.Group)item.GetGroups().Item(0);
                    int groupID = 0;

                    foreach (var groups in friendListView.Groups)
                    {
                        if (groups.Value.Title == g.Name)
                        {
                            groupID = groups.Value.Id;
                        }
                    }
                    if (groupID == 0)
                    {
                        friendListView.AddGroup(g.Name);
                        groupID = friendListView.Groups[g.Name].Id;
                        friendListView.UpdateLayout(3, groupID);

                    }
                    friend.GroupID = groupID;
                    friend.GroupName = g.Name;
                }
                else
                {
                    //离线联系人
                    Group group = friendListView.Groups["我的联系人"];
                    friend.GroupID = group.Id;
                    friend.GroupName = group.Title;

                    try
                    {
                        if (friendListView.Rosters.ContainsKey(friend.NikeName))
                            return;
                    }
                    catch (Exception)
                    {

                        //throw ex;
                    }

                }
                friend.Ritem = item;
                friend.IsOnline = false;

                try
                {
                    friendListView.AddFriend(friend);
                }
                catch (Exception)
                {

                    //throw ex;
                }

            }
            else
            {
                friendListView.RemoveFriend(item.Jid.User);
            }
        }

        public void XmppCon_OnRosterEnd(object sender)
        {
            //friendListView.AddGroup();
        }

        public void XmppCon_OnRosterStart(object sender)
        {
            //friendListView.Update();
        }

        public void XmppCon_OnPresence(object sender, Presence pres)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new OnPresenceDelegate(XmppCon_OnPresence), new object[] { sender, pres });
                return;
            }

            if (pres.Type == PresenceType.subscribe)
            {
                RequestFriendForm f = new RequestFriendForm(XmppCon, pres.From);
                f.TopMost = true;
                try
                {
                    f.Show();
                }
                catch (Exception)
                {
                    
                }
                
            }
            else if (pres.Type == PresenceType.subscribed)
            {

            }
            else if (pres.Type == PresenceType.unsubscribe)
            {

            }
            else if (pres.Type == PresenceType.unsubscribed)
            {

            }
            else
            {
                try
                {
                    //friendListView.RefreshFriend(pres.From.User, pres.Type, pres.Show);
                    friendListView.RefreshFriend(pres.From.User, pres.Type, pres.Show);
                    if (pres.Type==XMPP.protocol.client.PresenceType.unavailable)
                    {
                        if (Util.ChatForms.ContainsKey(pres.From.Bare))
                        {
                            ((ChatForm)Util.ChatForms[pres.From.Bare]).UpdateFriendOnline(false);
                        }

                        chat_history_listview.UpdateFriendInfoOnline(pres.From, false);
                    }
                    else
                    {
                        if (Util.ChatForms.ContainsKey(pres.From.Bare))
                        {
                            ((ChatForm)Util.ChatForms[pres.From.Bare]).UpdateFriendOnline(true);
                        }
                        chat_history_listview.UpdateFriendInfoOnline(pres.From, true);
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public void XmppCon_OnReadXml(object sender, string xml)
        {
            //Debug.WriteLine("XmppCon_OnReadXml:" + xml + "\n");
        }

        public void XmppCon_OnWriteXml(object sender, string xml)
        {
            //Debug.WriteLine("XmppCon_OnWriteXml:" + xml + "\n");
        }

        public void XmppCon_OnClose(object sender)
        {
            this.Invoke(new LogoutDelegate(LogOut),new object[]{false,true});
        }

        public void XmppCon_OnError(object sender, Exception ex)
        {
            this.Invoke(new LogoutDelegate(LogOut), new object[] { false, true });
        }


        /// <summary>
        /// 重新登录功能
        /// </summary>
        /// <param name="ISAutoLogin">是否读取配置文件的自动登录</param>
        ///<param name="isLogin">是否显示错误登录功能</param>
        public void LogOut(bool ISAutoLogin,bool isLogin)
        {
            this.Hide();
            OverTrayTimer.Enabled = false;
            timer1.Enabled = false;
            try
            {
                XmppCon.OnClose -= new ObjectHandler(XmppCon_OnClose);
                XmppCon.OnError -= new ErrorHandler(XmppCon_OnError);//错误
                XmppCon.Close();
            }
            catch (Exception)
            {

            }
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            friendListView.Rosters.Clear();
            friendListView.Groups.Clear();
            friendListView.Controls.Clear();


            try
            {
                login = new LoginFrom(this, isLogin);
                login.ISAutoLogin = ISAutoLogin;//不从配置中读取设置
                login.Login_Event += new LoginFrom.LoginDelegate(login_Login_Event);
                login.Show();

                XmppCon.OnClose += new ObjectHandler(XmppCon_OnClose);
                XmppCon.OnError += new ErrorHandler(XmppCon_OnError);
            }
            catch (Exception)
            {

            }
        }

        SetInfoForm _SetInfoForm = null;
        private void userImg_Click(object sender, EventArgs e)
        {

            if (_SetInfoForm == null || _SetInfoForm.IsDisposed)
            {
                _SetInfoForm = new SetInfoForm(XmppCon);
                _SetInfoForm.update_top_image_event += new SetInfoForm.update_top_image(_SetInfoForm_update_top_image_event);
            }
            _SetInfoForm.Show();
            _SetInfoForm.Activate();

        }

        #endregion
        /// <summary>
        /// 更新头像事件
        /// </summary>
        void _SetInfoForm_update_top_image_event()
        {
            VcardIq viq = new VcardIq(IqType.get, null, new Jid(XmppCon.MyJID.User));
            XmppCon.IqGrabber.SendIq(viq, new IqCB(VcardResult), null);
            _SetInfoForm = null;
        }

        #region 状态操作
        /// <summary>
        /// 在线状态更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stateButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                iContextMenu1.Show(this, stateButton1.Left, stateButton1.Top + 20);
            }
        }

        private void 我在线上ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            stateButton1.State = 1;
            if (XmppCon != null && XmppCon.Authenticated)
            {
                //if (cboStatus.Text == "online")
                //{
                //    XmppCon.Show = ShowType.NONE;
                //}
                //else if (cboStatus.Text == "offline")
                //{
                //    XmppCon.Close();
                //}
                //else
                //{
                //    XmppCon.Show = (ShowType)Enum.Parse(typeof(ShowType), cboStatus.Text);
                //}
                //XmppCon.SendMyPresence();
                XmppCon.Show = ShowType.NONE;
                XmppCon.SendMyPresence();
            }
        }

        private void 忙碌ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            stateButton1.State = 4;
            XmppCon.Show = ShowType.dnd;
            XmppCon.SendMyPresence();
        }

        private void 隐身ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            stateButton1.State = 5;
            //XmppCon.Show = ShowType.xa;
            //XmppCon.SendMyPresence();

        }

        private void 离开ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            stateButton1.State = 2;
            XmppCon.Show = ShowType.away;
            XmppCon.SendMyPresence();
        }

        private void 离线ToolStripMenuItem_MouseDown(object sender, MouseEventArgs e)
        {
            stateButton1.State = 0;
        }
        #endregion

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (timer_notifyIco.Enabled)
            {

                ChatMessageBox.GetInstance(this).OpenChatAll();//打开所有聊天窗体

                notifyIcon1.Icon = CSS.IM.UI.Properties.Resources.Iico;
                timer_notifyIco.Enabled = false;
                ChatMessageBox.GetInstance(this).RemoveFriend();
                return;
            }

            this.Show();
            this.Activate();

            if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
            {
                Left = 0;
            }
            if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
            {
                Top = 0;
            }
            if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
            {
                Left = Screen.PrimaryScreen.Bounds.Width - Width;
            }
            if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
            {
                Top = Screen.PrimaryScreen.Bounds.Height - Height;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 更换用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();


            if ((anchors & AnchorStyles.Left) == AnchorStyles.Left)
            {
                Left = 0;
            }
            if ((anchors & AnchorStyles.Top) == AnchorStyles.Top)
            {
                Top = 0;
            }
            if ((anchors & AnchorStyles.Right) == AnchorStyles.Right)
            {
                Left = Screen.PrimaryScreen.Bounds.Width - Width;
            }
            if ((anchors & AnchorStyles.Bottom) == AnchorStyles.Bottom)
            {
                Top = Screen.PrimaryScreen.Bounds.Height - Height;
            }
        }


        int noyX = 0;
        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            // 显示提示窗体
            OverTrayTimer.Enabled = true;

            Point MouseAt = new Point(MousePosition.X, MousePosition.Y);
            if (!TrayIconPoints.Contains(MouseAt))
            {
                TrayIconPoints.Add(MouseAt);
            }

            // 获得屏幕的宽
            Screen screen = Screen.PrimaryScreen;
            int screenWidth = screen.Bounds.Width;

            // 获得工作区域的高
            int workAreaHeight = Screen.PrimaryScreen.WorkingArea.Height;

            // 获得提示窗体的宽和高
            int toolTipWidth = ChatMessageBox.GetInstance(this).Width;
            int toolTipHeight = ChatMessageBox.GetInstance(this).Height;
            if (noyX == 0)
            {
                noyX = MousePosition.X;
            }
            // 那么提示窗体的左上角坐标就是：屏幕的宽 - 提示窗体的宽， 工作区域的高 - 提示窗体的高
            ChatMessageBox.GetInstance(this).Location = new Point(noyX - toolTipWidth / 2, workAreaHeight - toolTipHeight);


        }

        public Rectangle GetTrayIconRectangle()
        {
            //Point curPoint;
            Point TopLeft = new Point(10000, 10000);
            Point BottomRight = new Point(-10000, -10000);
            foreach (Point curPoint in TrayIconPoints)
            {
                if (curPoint.X < TopLeft.X)
                {
                    TopLeft.X = curPoint.X;
                }

                if (curPoint.Y < TopLeft.Y)
                {
                    TopLeft.Y = curPoint.Y;
                }

                if (curPoint.X > BottomRight.X)
                {
                    BottomRight.X = curPoint.X;
                }

                if (curPoint.Y > BottomRight.Y)
                {
                    BottomRight.Y = curPoint.Y;
                }
            }

            return new Rectangle(TopLeft, new Size(BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y));
        }

        void OverTrayTimer_Tick(object sender, EventArgs e)
        {

            if (GetTrayIconRectangle().Contains(MousePosition))
            {
                if (ChatMessageBox.GetInstance(this).FrienMessageCount() > 0)
                {
                    ChatMessageBox.GetInstance(this).Show();
                }
            }
            else
            {
                noyX = 0;
                ChatMessageBox.GetInstance(this).Hide();
            }
            OverTrayTimer.Enabled = false;
        }

        bool isMessageNotifyIco = false;
        private void timer_notifyIco_Tick(object sender, EventArgs e)
        {
            if (isMessageNotifyIco)
            {
                notifyIcon1.Icon = CSS.IM.UI.Properties.Resources.Imessage;
                isMessageNotifyIco = false;
            }
            else
            {
                notifyIcon1.Icon = CSS.IM.UI.Properties.Resources.Iico;
                isMessageNotifyIco = true;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void tsmi添加好友_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            Jid j = new Jid(treeView1.SelectedNode.Tag.ToString(), XmppCon.Server, null);
            XmppCon.RosterManager.AddRosterItem(j);
            XmppCon.PresenceManager.Subscribe(j);
        }

        private void tsmi发送消息_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            if (treeView1.SelectedNode.Tag == null)
                return;
            Jid j = new Jid(treeView1.SelectedNode.Tag.ToString(), XmppCon.Server, null);
            if (!Util.ChatForms.ContainsKey(j.ToString()))
            {
                friendListView.UpdateFriendFlicker(j.User);
                ChatForm chat = new ChatForm(j, XmppCon, j.User);

                Friend friend = friendListView.Rosters[j.User];

                if (friend!=null)
                {
                    chat.UpdateFriendOnline(friendListView.Rosters[j.User].IsOnline);
                }

                if (msgBox.ContainsKey(j.Bare.ToString()))
                {
                    chat.FristMessage(msgBox[j.Bare.ToString()]);
                    msgBox.Remove(j.Bare.ToString());
                }
                try
                {
                    chat.Show();
                }
                catch (Exception)
                {

                }
                
            }
        }

        private void tsmi查看信息_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            VcardForm vcardForm = new VcardForm(new Jid(treeView1.SelectedNode.Tag.ToString(), XmppCon.Server, null), XmppCon);
            try
            {
                vcardForm.Show();
            }
            catch (Exception)
            {
                
            }
           
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Selectnode = treeView1.SelectedNode;

            if (treeView1.SelectedNode == null)
                return;
            if (treeView1.SelectedNode.Tag == null)
                return;
            if (treeView1.SelectedNode.Tag == treeView1.Nodes[0].Tag)
                return;
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag != null)
                {
                    tsmi发送消息_Click(null, null);
                }
            }
        }

        private void 关闭声音ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Document doc = new Document();
            doc.LoadFile(CSS.IM.UI.Util.Path.ConfigFilename);
            Settings.Settings config = new Settings.Settings();
            Settings.Paths path = doc.RootElement.SelectSingleElement(typeof(Settings.Paths)) as Settings.Paths;

            if (关闭声音ToolStripMenuItem.Text == "关闭所有声音")
            {
                关闭声音ToolStripMenuItem.Text = "开启所有声音";
                path.SelectSingleElement("MsgPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.MsgSwitch = false;
                path.SelectSingleElement("SystemPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.SystemSwitch = false;
                path.SelectSingleElement("CallPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.CallSwitch = false;
                path.SelectSingleElement("FolderPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.FolderSwitch = false;
                path.SelectSingleElement("GlobalPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.GlobalSwitch = false;
                path.SelectSingleElement("InputAlertPath").SetAttribute("Enable", false);
                CSS.IM.UI.Util.Path.InputAlertSwitch = false;
            }
            else
            {
                关闭声音ToolStripMenuItem.Text = "开启所有声音";
                path.SelectSingleElement("MsgPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.MsgSwitch = true;
                path.SelectSingleElement("SystemPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.SystemSwitch = true;
                path.SelectSingleElement("CallPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.CallSwitch = true;
                path.SelectSingleElement("FolderPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.FolderSwitch = true;
                path.SelectSingleElement("GlobalPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.GlobalSwitch = true;
                path.SelectSingleElement("InputAlertPath").SetAttribute("Enable", true);
                CSS.IM.UI.Util.Path.InputAlertSwitch = true;
            }
            config.Paths = path;
            doc.Clear();
            doc.ChildNodes.Add(config);
            doc.Save(CSS.IM.UI.Util.Path.ConfigFilename);

        }

        private void btn_MouseClick(object sender, MouseEventArgs e)
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.Library.Data;
using System.Data.OleDb;
using System.Diagnostics;
using CSS.IM.XMPP.Factory;
using CSS.IM.XMPP.Xml.Dom;
using CSS.IM.UI.Util;
using CSS.IM.UI.Control;
using CSS.IM.XMPP;
using System.Threading;
using CSS.IM.App.Settings;
using CSS.IM.App.Msg;

namespace CSS.IM.App
{
    public partial class MessageLogForm : BasicForm
    {
        public XmppClientConnection XmppConn { set; get; }

        private EmotionDropdown emotionDropdown;//表情管理器

        int PageSize = 20;
        int Pagecount = 0;
        TreeNode ActiveNode;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            XmppConn = null;

            RTBRecord.Dispose();
            RTBRecord = null;

            if (删除记录ToolStripMenuItem!=null)
            {
                删除记录ToolStripMenuItem.Dispose();
                删除记录ToolStripMenuItem = null;
            }

            if (qqContextMenu1!=null)
            {
                qqContextMenu1.Dispose();
                qqContextMenu1 = null;
            }

            if (emotionDropdown!=null)
            {
                emotionDropdown.Dispose();
                emotionDropdown = null;
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public MessageLogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 表单初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MessageLogForm_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            //select Jid from ChatMessageLog group by Jid 查找出所有有消息的好友
            treeView1.Nodes[0].Nodes.Clear();
            TreeNode tn_pre = treeView1.Nodes[0];
            OleDbDataReader dr = null;

            try
            {
                dr = OleDb.ExSQLReDr("select Jid from ChatMessageLog where Belong='" + XmppConn.MyJID.Bare.ToString()+ "'group by Jid");
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
                MsgBox.Show(this, "CSS&IM", "读取消息历史记录错误,程序存储文件错误，请重新安装程序！" , MessageBoxButtons.OK);
                this.Close();
            }

            string tn_name = "";
            while (dr.Read())
            {
                tn_name = dr.GetString(0);

                TreeNode tn_f = new TreeNode(new CSS.IM.XMPP.Jid(tn_name).User, 1, 1);
                tn_f.Tag = tn_name;
                tn_pre.Nodes.Add(tn_f);
            }

            treeView1.Nodes[1].Nodes.Clear();
            tn_pre = treeView1.Nodes[1];
            try
            {
                dr = OleDb.ExSQLReDr("select MessageType from MessageLog where Belong='" + XmppConn.MyJID.Bare.ToString() + "' group by MessageType");
            }
            catch (Exception ex)
            {
                MsgBox.Show(this, "CSS&IM", "读取消息历史记录错误," + ex.Message, MessageBoxButtons.OK);
                this.Close();
            }

            string tag = "";
            while (dr.Read())
            {
                tn_name = dr.GetString(0);
                tag = tn_name;

                switch (tn_name)
                {
                    case "0":
                        tn_name = "系统通知";
                        break;
                }

                TreeNode tn_f = new TreeNode(tn_name, 1, 1);
                tn_f.Tag = tag;
                tn_pre.Nodes.Add(tn_f);
            }

            groupBox1.Invalidate();
            groupBox2.Invalidate();


        }

        /// <summary>
        /// 显示分布消息聊天记录
        /// </summary>
        /// <param name="e"></param>
        private void LoadChatMessage(TreeNode e)
        {
            RTBRecord.Clear();
            RTBRecord.ClearUndo();
            //SELECT TOP 2 * FROM (SELECT TOP 2 * FROM ChatMessageLog  ORDER BY id DESC) ORDER BY id
            //SELECT * FROM (SELECT TOP " + PageSize + " * FROM (SELECT * FROM (SELECT TOP " + PageSize * int.Parse(txt_pageIndex.Text.ToString()) + " * FROM ChatMessageLog where Jid='" + e.Tag.ToString() + "') ORDER BY id desc) ) ORDER BY ID asc
            OleDbDataReader dr = OleDb.ExSQLReDr("SELECT * FROM (SELECT TOP " + PageSize + " * FROM (SELECT * FROM (SELECT TOP " + PageSize * int.Parse(txt_pageIndex.Text.ToString()) + " * FROM ChatMessageLog where Jid='" + e.Tag.ToString() + "' and Belong='" + XmppConn.MyJID.Bare.ToString() + "' ) ORDER BY id desc) ) ORDER BY ID asc");
            while (dr.Read())
            {
                Document document = new Document();
                document.LoadXml(dr.GetString(3));
                CSS.IM.XMPP.protocol.client.Message top_msg = (CSS.IM.XMPP.protocol.client.Message)document.RootElement;
                if (top_msg.GetTagInt("m_type") == 0)
                {
                    RTBRecord_Show(top_msg, false);
                }
            }
        }

        /// <summary>
        /// 显示分页后的系统消息
        /// </summary>
        /// <param name="e"></param>
        private void LoadMessage(TreeNode e)
        {
            RTBRecord.Clear();
            RTBRecord.ClearUndo();

            OleDbDataReader dr = OleDb.ExSQLReDr("SELECT * FROM (SELECT TOP " + PageSize + " * FROM (SELECT * FROM (SELECT TOP " + PageSize * int.Parse(txt_pageIndex.Text.ToString()) + " * FROM MessageLog where MessageType='" + e.Tag.ToString() + "' and Belong='" + XmppConn.MyJID.Bare.ToString() + "') ORDER BY id desc) ) ORDER BY ID asc");
            //OleDbDataReader dr = OleDb.ExSQLReDr("SELECT TOP " + PageSize + " * FROM (SELECT TOP " + PageSize * int.Parse(txt_pageIndex.Text.ToString()) + " * FROM MessageLog where MessageType='" + e.Tag.ToString() + "' ORDER BY id DESC) ORDER BY id");
            //OleDbDataReader dr = OleDb.ExSQLReDr("select * from MessageLog where MessageType='" + e.Tag.ToString() + "' order by [ID] asc");
            while (dr.Read())
            {
                Document document = new Document();
                string sytsem_msg = "<message xmlns=\"jabber:client\" from=\"songques@imserver\" to=\"songques@imserver\"><FName>宋体</FName><FSize>9</FSize><FBold>true</FBold><FItalic>false</FItalic><FStrikeout>false</FStrikeout><FUnderline>true</FUnderline><CA>207</CA><CR>119</CR><CG>33</CG><CB>255</CB><body>[系统通知]    " + dr.GetDateTime(4).ToString("yyyy-MM-dd HH:mm:ss") + "</body></message>";
                document.LoadXml(sytsem_msg);
                CSS.IM.XMPP.protocol.client.Message top_msg = (CSS.IM.XMPP.protocol.client.Message)document.RootElement;
                RTBRecord_Show(top_msg, false);


                document.Clear();
                document.LoadXml(dr.GetString(3));
                top_msg = (CSS.IM.XMPP.protocol.client.Message)document.RootElement;
                top_msg.Body = top_msg.Body + "\n";

                MqMessage mqMsg=MessageBoxForm.MarkMessage_Mq(top_msg);
                if (top_msg.GetTag("subject")!=null)
	            {
                    top_msg.Body = mqMsg.Herf + "?token=" + mqMsg.Token + "&url=" + mqMsg.Url + "&password=" + Base64.EncodeBase64(XmppConn.Password);
	            }
                RTBRecord_Show(top_msg, false);
            }
        }

        /// <summary>
        /// 初始化分布按钮
        /// </summary>
        /// <param name="pageCount"></param>
        public void InitButtons(int pageCount)
        {
            txt_pageIndex.Text = "1";
            lab_pageCount.Text = pageCount == 0 ? "1" : pageCount.ToString();

        }

        /// <summary>
        /// 更新消息显示
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="isSend"></param>
        public void RTBRecord_Show(CSS.IM.XMPP.protocol.client.Message msg, bool isSend)
        {
            System.Drawing.FontStyle fontStyle = new System.Drawing.FontStyle();
            System.Drawing.Font ft = null;

            #region 获取字体
            try
            {
                if (msg.GetTagBool("FBold"))
                {
                    fontStyle = System.Drawing.FontStyle.Bold;
                }
                if (msg.GetTagBool("FItalic"))
                {
                    fontStyle = fontStyle | System.Drawing.FontStyle.Italic;
                }
                if (msg.GetTagBool("FStrikeout"))
                {
                    fontStyle = fontStyle | System.Drawing.FontStyle.Strikeout;
                }
                if (msg.GetTagBool("FUnderline"))
                {
                    fontStyle = fontStyle | System.Drawing.FontStyle.Underline;
                }
                ft = new System.Drawing.Font(msg.GetTag("FName"), float.Parse(msg.GetTag("FSize")), fontStyle);
            }
            catch (Exception)
            {
                ft = RTBRecord.Font;
            }
            #endregion

            #region 获取颜色
            Color cl = RTBRecord.ForeColor;
            try
            {
                byte[] cby = new byte[4];
                cby[0] = Byte.Parse(msg.GetTag("CA"));
                cby[1] = Byte.Parse(msg.GetTag("CR"));
                cby[2] = Byte.Parse(msg.GetTag("CG"));
                cby[3] = Byte.Parse(msg.GetTag("CB"));
                cl = Color.FromArgb(BitConverter.ToInt32(cby, 0));

            }
            catch
            {
                cl = RTBRecord.ForeColor;
            }
            #endregion

            int iniPos = this.RTBRecord.TextLength;//获得当前记录richBox中最后的位置
            String msgtext = msg.Body;
            //RTBRecord.Focus();
            RTBRecord.Select(RTBRecord.TextLength, 0);
            RTBRecord.ScrollToCaret();

            string face = "";

            try
            {
                face = msg.GetTag("face").ToString();
            }
            catch (Exception)
            {
                face = "";
            }


            if (face != "")//如果消息中有图片，则添加图片
            {
                string[] imagePos = face.Split('|');
                int addPos = 0;//
                int currPos = 0;//当前正要添加的文本位置
                int textPos = 0;
                for (int i = 0; i < imagePos.Length - 1; i++)
                {
                    string[] imageContent = imagePos[i].Split(',');//获得图片所在的位置、图片名称 
                    currPos = Convert.ToInt32(imageContent[0]);//获得图片所在的位置

                    this.RTBRecord.AppendText(msgtext.Substring(textPos, currPos - addPos));
                    this.RTBRecord.SelectionStart = this.RTBRecord.TextLength;

                    textPos += currPos - addPos;
                    addPos += currPos - addPos;

                    Image image = null;

                    if (emotionDropdown == null)
                    {
                        emotionDropdown = new EmotionDropdown();
                    }

                    if (emotionDropdown.faces.ContainsKey(imageContent[1]))
                    {
                        if (this.RTBRecord.findPic(imageContent[1]) == null)
                            image = ResClass.GetImgRes("_" + int.Parse(imageContent[1].ToString()).ToString());
                        else
                            image = this.RTBRecord.findPic(imageContent[1]).Image;
                    }
                    else
                    {
                        String img_str = msg.GetTag(imageContent[1]);
                        try
                        {

                            if (msg.From.Bare == XmppConn.MyJID.Bare)
                            {
                                image = Image.FromFile(Util.sendImage + imageContent[1].ToString() + ".gif");
                            }
                            else
                            {
                                image = Image.FromFile(Util.receiveImage + imageContent[1].ToString() + ".gif");
                            }

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    this.RTBRecord.addGifControl(imageContent[1], image);
                    addPos++;
                }
                this.RTBRecord.AppendText(msgtext.Substring(textPos, msgtext.Length - textPos) + "  \n");
            }
            else
            {

                this.RTBRecord.AppendText(msgtext + "\n");
            }
            //RTBRecord.Focus();
            this.RTBRecord.Select(iniPos, this.RTBRecord.TextLength - iniPos);
            this.RTBRecord.SelectionFont = ft;
            this.RTBRecord.SelectionColor = cl;
            this.RTBRecord.Select(this.RTBRecord.TextLength, 0);
            this.RTBRecord.ScrollToCaret();
            //this.RTBRecord.Focus();
        }

        /// <summary>
        /// 弹出菜单删除功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode.Parent.Text == "我的好友")
                {
                    OleDb.ExSQLReDr("delete from ChatMessageLog where Jid='" + treeView1.SelectedNode.Tag.ToString() + "' and Belong='" + XmppConn.MyJID.Bare.ToString() + "'");    
                }

                if (treeView1.SelectedNode.Parent.Text == "通知消息")
                {
                    OleDb.ExSQLReDr("delete from MessageLog where MessageType='0' and Belong='" + XmppConn.MyJID.Bare.ToString() + "'");    
                }

                RTBRecord.Clear();
                RTBRecord.ClearUndo();
                pal_buttons.Visible = false;
            }
            catch (Exception)
            {
                
     
            }

            Thread.Sleep(1000);

            MessageLogForm_Load(null, null);

        }

        /// <summary>
        /// 弹出菜单更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void qqContextMenu1_Paint(object sender, PaintEventArgs e)
        {
            if (treeView1.SelectedNode.Tag != null)
            {
                if (treeView1.SelectedNode.Parent != null)
                {
                    if (treeView1.SelectedNode.Parent.Text == "我的好友" || treeView1.SelectedNode.Parent.Text=="通知消息")
                    {
                        qqContextMenu1.Enabled = true;
                        return;
                    }
                }
            }
            qqContextMenu1.Enabled = false;
        }

        /// <summary>
        /// 输入页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_pageIndex_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int pagenum = 0;
                try
                {
                    pagenum = int.Parse(txt_pageIndex.Text);
                }
                catch (Exception)
                {
                    pagenum = 0;
                    txt_pageIndex.Text = "0";
                }

                int pagenum_lab = int.Parse(lab_pageCount.Text);


                if (pagenum > pagenum_lab)
                {
                    txt_pageIndex.Text = pagenum_lab.ToString();
                }
                else if (pagenum == 0)
                {
                    txt_pageIndex.Text = "1";
                }
                if (ActiveNode.Parent.Text == "我的好友")
                {
                    LoadChatMessage(ActiveNode);
                }
                else if (ActiveNode.Parent.Text == "通知消息")
                {
                    LoadMessage(ActiveNode);
                }
            }
        }

        /// <summary>
        /// 首页功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_frist_Click(object sender, EventArgs e)
        {
            txt_pageIndex.Text = "1";
            if (ActiveNode.Parent.Text == "我的好友")
            {
                LoadChatMessage(ActiveNode);
            }
            else if (ActiveNode.Parent.Text == "通知消息")
            {
                LoadMessage(ActiveNode);
            }
        }

        /// <summary>
        /// 尾页功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_last_Click(object sender, EventArgs e)
        {
            txt_pageIndex.Text = lab_pageCount.Text;
            if (ActiveNode.Parent.Text == "我的好友")
            {
                LoadChatMessage(ActiveNode);
            }
            else if (ActiveNode.Parent.Text == "通知消息")
            {
                LoadMessage(ActiveNode);
            }
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_pre_Click(object sender, EventArgs e)
        {

            int pageindex = 1;
        A:
            try
            {

                pageindex = int.Parse(txt_pageIndex.Text);

                if (pageindex > 1)
                {
                    pageindex = pageindex - 1;
                    txt_pageIndex.Text = pageindex.ToString();
                }

            }
            catch (Exception)
            {
                txt_pageIndex.Text = "1";
                goto A;
            }

            if (ActiveNode.Parent.Text == "我的好友")
            {
                LoadChatMessage(ActiveNode);
            }
            else if (ActiveNode.Parent.Text == "通知消息")
            {
                LoadMessage(ActiveNode);
            }


        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_next_Click(object sender, EventArgs e)
        {
            int pageindex = 1;
        A:
            try
            {

                pageindex = int.Parse(txt_pageIndex.Text);
                if (pageindex < int.Parse(lab_pageCount.Text))
                {
                    pageindex = pageindex + 1;
                    txt_pageIndex.Text = pageindex.ToString();
                }
            }
            catch (Exception)
            {
                txt_pageIndex.Text = "1";
                goto A;
            }
            if (ActiveNode.Parent.Text == "我的好友")
            {
                LoadChatMessage(ActiveNode);
            }
            else if (ActiveNode.Parent.Text == "通知消息")
            {
                LoadMessage(ActiveNode);
            }

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            RTBRecord.Clear();
            RTBRecord.ClearUndo();
            
            //RTBRecord.panelGif.Dispose();
            RTBRecord.gifs.Dispose();

            if (e.Node.Parent != null)
            {
                ActiveNode = e.Node;
                pal_buttons.Visible = true;
                if (e.Node.Parent.Text == "我的好友")
                {

                    object PageCountObject = OleDb.ExSQLReField("PageCount", "select count(*) as PageCount from ChatMessageLog where Jid='" + e.Node.Tag.ToString() + "' and Belong='" + XmppConn.MyJID.Bare.ToString() + "'" );
                    Pagecount = PageCountObject == null ? 0 : int.Parse(PageCountObject.ToString());

                    int num2 = 1;
                    if (Pagecount > 0)
                    {
                        double num1 = (double)Pagecount / (double)PageSize;
                        num2 = Pagecount / PageSize;
                        num2 = ((num2 - (double)num1) > 0 ? 1 : num2);
                    }



                    InitButtons(num2);

                    LoadChatMessage(e.Node);
                }
                if (e.Node.Parent.Text == "通知消息")
                {
                    
                    //
                    object PageCountObject = OleDb.ExSQLReField("PageCount", "select count(*) as PageCount from MessageLog where MessageType='" + e.Node.Tag.ToString() + "' and Belong='" + XmppConn.MyJID.Bare.ToString() + "'");
                    Pagecount = PageCountObject == null ? 0 : int.Parse(PageCountObject.ToString());

                    int num2 = 1;
                    if (Pagecount > 0)
                    {
                        double num1 = (double)Pagecount / (double)PageSize;
                        num2 = Pagecount / PageSize;
                        num2 = ((num2 - (double)num1) > 0 ? 1 : num2);
                    }

                    InitButtons(num2);

                    InitButtons(Pagecount / PageSize);
                    LoadMessage(e.Node);
                }
            }
            else
            {
                pal_buttons.Visible = false;
            }
            treeView1.Focus();
        }

        private void RTBRecord_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //MessageBox.Show(e.LinkText);
            //file:\\C:\Users\Administrator\Desktop\system.wav
            string url = e.LinkText;
            string falg = url.Substring(0, 4);
            if (falg == "http")
            {
                Process.Start(url);
            }
            else
            {
                url = url.Substring(7, url.Length - 7);
                Process proc = new Process();
                proc.StartInfo.FileName = "explorer"; //打开资源管理器
                proc.StartInfo.Arguments = @"/select," + url;
                proc.Start();
            }
        }
        

    }
}

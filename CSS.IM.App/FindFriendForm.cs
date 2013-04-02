using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CSS.IM.UI.Form;
using CSS.IM.XMPP;
using CSS.IM.App.Settings;
using CSS.IM.XMPP.protocol.x.data;
using CSS.IM.XMPP.protocol.client;
using CSS.IM.XMPP.protocol.iq.search;
using CSS.IM.UI.Control;

namespace CSS.IM.App
{
    public partial class FindFriendForm : BasicForm
    {

        private XmppClientConnection m_XmppCon;
        private QQContextMenu friend_qcm;

        //选择的Jid用于选人组件
        public Jid SelectJid { set; get; }

        public bool ISSelect { set; get; }

        public FindFriendForm(XmppClientConnection con)
        {
            InitializeComponent();

            m_XmppCon = con;

            friend_qcm = new QQContextMenu();
            QQToolStripMenuItem item1 = new QQToolStripMenuItem();
            item1.Text = "加为联系人";
            item1.Click += new EventHandler(item1_Click);
            QQToolStripMenuItem item2 = new QQToolStripMenuItem();
            item2.Text = "名片";
            item2.Click += new EventHandler(item2_Click);
            friend_qcm.Items.AddRange(new ToolStripItem[] { item1, item2 });
            if (!ISSelect)
                this.ContextMenuStrip = friend_qcm;
            // Fill combo with search services
            //foreach (Jid jid in Util.Services.Search)
            //{
            //    //cboServices.Items.Add(jid.Bare);
            //}

        }

        void item2_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (!(listView1.SelectedIndices.Count > 0))
                return;
            ListViewItem item = listView1.Items[listView1.SelectedIndices[0]];
            String jid = item.SubItems[0].Text.ToString();
            Jid j = new Jid(jid);
            VcardInfoForm vcardForm = new VcardInfoForm(j, m_XmppCon);
            try
            {
                vcardForm.Show();
            }
            catch (Exception)
            {
                
            }
            
        }

        void item1_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < listView1.SelectedIndices.Count; i++)
                {
                    ListViewItem item = listView1.Items[listView1.SelectedIndices[i]];
                    String jid = item.SubItems[0].Text.ToString();
                    Jid j = new Jid(jid);
                    m_XmppCon.RosterManager.AddRosterItem(j);
                    m_XmppCon.PresenceManager.Subscribe(j);
                }
                MsgBox.Show(this, "CSS&IM", "添加好友成功，等待对方验证！", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MsgBox.Show(this, "CSS&IM", "添加好友失败，" + ex.Message, MessageBoxButtons.OK);
            }
        }

        private void FindFriendForm_Load(object sender, EventArgs e)
        {
            cmb_findtype.SelectIndex = 0;
            if (ISSelect)
            {
                this.ContextMenuStrip = null;
            }
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            //<field type="boolean" var="Email"><value>1</value></field>
            //<field type="boolean" var="Name"><value>1</value></field>
            //<field type="boolean" var="Username"><value>1</value></field>
            //<field type="text-single" var="search"><value>*</value></field>
            //<field type="hidden" var="FORM_TYPE"><value>jabber:iq:search</value></field>



            Data data = new Data(XDataFormType.submit);
            Field fEmail = new Field(FieldType.Boolean);
            fEmail.SetValueBool(true);
            fEmail.Var = "Email";
            data.AddField(fEmail);



            Field fName = new Field(FieldType.Boolean);
            fName.SetValueBool(true);
            fName.Var = "Name";
            data.AddField(fName);

            Field fUsername = new Field(FieldType.Boolean);
            fUsername.SetValueBool(true);
            fUsername.Var = "Username";
            data.AddField(fUsername);

            Field fWhare = new Field(FieldType.Text_Single);
            if (cmb_findtype.Items[cmb_findtype.SelectIndex].ToString() != "全部")
            {
                fWhare.AddValues(new String[] { txt_userName.Texts.Trim().ToString() });
            }
            else
            {
                fWhare.AddValues(new String[] { "*" });
            }

            fWhare.Var = "search";
            data.AddField(fWhare);


            Field fsearch = new Field(FieldType.Hidden);

            fsearch.AddValue("jabber:iq:search");
            fsearch.Var = "FORM_TYPE";

            data.AddField(fsearch);


            IQ siq = siq = new SearchIq();
            ((SearchIq)siq).Query.Data = data;

            //directory

            if (Util.Services.Finds.Count==0)
            {
                 MsgBox.Show(this, "CSS&IM", "服务器没有注册Findes请与管理员联系！", MessageBoxButtons.OK);
                 return;
            }

            if (Util.Services.Finds[0]!=null)
            {
                try
                {
                    siq.To = Util.Services.Finds[0];
                    siq.Type = IqType.set;
                    m_XmppCon.IqGrabber.SendIq(siq, new IqCB(OnSearchResult), null, true);
                }
                catch (Exception)
                {
                    
                }
                
            }
        }

        private void OnSearchResult(object sender, IQ iq, object data)
        {
            // We will upate the GUI from here, so invoke
            if (InvokeRequired)
            {
                // Windows Forms are not Thread Safe, we need to invoke this :(
                // We're not in the UI thread, so we need to call BeginInvoke				
                BeginInvoke(new IqCB(OnSearchResult), new object[] { sender, iq, data });
                return;
            }

            /*
             Example 9. Service Returns Search Results

            <iq type='result'
                from='characters.shakespeare.lit'
                to='juliet@capulet.com/balcony'
                id='search4'
                xml:lang='en'>
              <query xmlns='jabber:iq:search'>
                <x xmlns='jabber:x:data' type='result'>
                  <field type='hidden' var='FORM_TYPE'>
                    <value>jabber:iq:search</value>
                  </field>
                  <reported>
                    <field var='first' label='First Name'/>
                    <field var='last' label='Last Name'/>
                    <field var='jid' label='Jabber ID'/>
                    <field var='gender' label='Gender'/>
                  </reported>
                  <item>
                    <field var='first'><value>Benvolio</value></field>
                    <field var='last'><value>Montague</value></field>
                    <field var='jid'><value>benvolio@montague.net</value></field>
                    <field var='gender'><value>male</value></field>
                  </item>
                  <item>
                    <field var='first'><value>Romeo</value></field>
                    <field var='last'><value>Montague</value></field>
                    <field var='jid'><value>romeo@montague.net</value></field>
                    <field var='gender'><value>male</value></field>
                  </item>
                </x>
              </query>
            </iq>            
            */

            if (iq.Type == IqType.result)
            {
                if (iq.Query is Search)
                {
                    Data xdata = ((Search)iq.Query).Data;
                    if (xdata != null)
                    {
                        ShowData(xdata);
                    }
                    else
                    {
                        //ShowData(iq.Query as Search);
                    }
                }
            }
            else
            {
                //ClearGridAndDataTable();
                //toolStripStatusLabel1.Text = "an error occured in the search request";
            }
        }

        private void ShowData(Data xdata)
        {
            Reported reported = xdata.Reported;
            //if (reported != null)
            //{
            //    foreach (Field f in reported.GetFields())
            //    {
            //        // Create header
            //        //DataGridViewTextBoxColumn header = new DataGridViewTextBoxColumn();
            //        //header.DataPropertyName = f.Var;
            //        //header.HeaderText = f.Label;
            //        //header.Name = f.Var;

            //        //dataGridView1.Columns.Add(header);

            //        // Create dataTable Col
            //        //_dataTable.Columns.Add(f.Var, typeof(string));
            //    }
            //}

            Item[] items = xdata.GetItems();
            foreach (Item item in items)
            {
                //DataRow dataRow = _dataTable.Rows.Add();
                ListViewItem litem = null;
                int values_length = item.GetFields().Length;
                String[] item_values = new String[values_length];

                for (int i = 0; i < values_length; i++)
                {
                    item_values[3 - i] = item.GetFields()[i].GetValue();
                }
                litem = new ListViewItem(item_values);
                listView1.Items.Add(litem);
                listView1.Tag = item;


                //foreach (Field field in item.GetFields())
                //{
                //    //dataRow[field.Var] = field.GetValue();
                //}

                //ListViewItem item = new ListViewItem(new string[] { (listView1.Items.Count + 1).ToString(), infos[0], infos[1], infos[2] });
                //listView1.Items.Add(item01);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!ISSelect)
                item1_Click(null, null);
            else
            {
                ListViewItem item = listView1.Items[listView1.SelectedIndices[0]];
                String jid = item.SubItems[0].Text.ToString();
                SelectJid = new Jid(jid);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}

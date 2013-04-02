using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Net;

namespace CSS.IM.Library.Controls
{
    #region �ļ�����ؼ�
    public partial class p2pFileTransmitEX : Component
    {
        #region �����ʼ��
        public p2pFileTransmitEX()
        {
            InitializeComponent();
        }

        public p2pFileTransmitEX(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        #endregion

        #region �Զ������

        /// <summary>
        /// ��д�ļ��ľ���·��
        /// </summary>
        private string _fullFileName ="";//���ͻ�����ļ��������λ��
       
        /// <summary>
        /// ��ʾ���ļ�����������·����
        /// </summary>
        private  string _fileName="";
       

        /// <summary>
        /// ������ip��ַ 
        /// </summary>
        private System.Net.IPAddress _serverIp = System.Net.IPAddress.Parse("127.0.0.1");

        public System.Net.IPAddress ServerIp
        {
            get { return _serverIp; }
            set { _serverIp = value; }
        }

        /// <summary>
        /// ��תTCP������
        /// </summary>
        private int _serverUDPPort = 0;

        /// <summary>
        /// ��תUDP������
        /// </summary>
        //private int _serverTCPPort = 0;
         
        /// <summary>
        /// �ļ��ߴ�
        /// </summary>
        private int _FileLen = 0;//
       
        /// <summary>
        /// �ļ���չ��
        /// </summary>
        public string Extension = "";
       
        /// <summary>
        /// ����ļ��Ƿ��ڷ��͹�����
        /// </summary>
        private bool IsSendState = false;

        /// <summary>
        /// ��ʶ��ǰ�û��Ƿ����ļ����ǽ����ļ�
        /// </summary>
        private bool _IsSend = false;

        /// <summary>
        /// �Ƿ�ȡ���ļ�����
        /// </summary>
        private bool _isCancelTransmit = false ;

        /// <summary>
        /// �ļ��ߴ���������
        /// </summary>
        public  string  FileLenStr = "0";
       
        /// <summary>
        ///  һ�ζ�д�ļ��Ļ�������С������Ϊ10M
        /// </summary>
        private byte[] FileBlock;


        /// <summary>
        /// ��¼�ܹ�Ҫ��д�ļ�����
        /// </summary>
        private int readFileCount = 1;//10M

        /// <summary>
        /// ��ǰ���ļ�����
        /// </summary>
        private int currReadCount = 0;//��ǰ���ļ�����

        /// <summary>
        /// ��ǰ����ļ������ݳ���
        /// </summary>
        private int currGetPos = 0; 

        /// <summary>
        /// һ�ζ�д�ļ��Ĵ�С������Ϊ10M
        /// </summary>
        private int _maxReadWriteFileBlock = 5242880*2;//5M
        [Category("ȫ������")]
        [Description("һ�ζ�д�ļ��Ļ�������С��Ĭ��Ϊ10M")]
        public int maxReadWriteFileBlock
        {
            set { _maxReadWriteFileBlock = value; }
            get { return _maxReadWriteFileBlock; }
        }

        /// <summary>
        /// ÿ�η��͵����ݰ���������
        /// </summary>
        private UInt16 _mtu = 1250;//���һ�δ����ļ����ݿ�Ĵ�С�����ܳ�����������䵥Ԫ MTU 576-1492 ���ƣ��������������ϵ����ݷ��ͽ����ɹ�


        private byte  outTime = 0;
        [Category("ȫ������")]
        [Description("����UDPÿһ�δ������ݰ��ĳ�ʱ����")]
        [DefaultValue(2)]
        public byte OutTime
        {
            set { outTime = value; }
            get { return outTime; }
        }


        public string FileMD5Value;//�ļ�MD5ֵ
        //private CSS.IM.Library.Class.UserInfo _selfUserInfo;//�Լ���������Ϣ
        //private CSS.IM.Library.Class.UserInfo _OppositeUserInfo;//�Է���������Ϣ
        private int serverSelfID = -1;//�Լ�����ת����ID 
        private int serverOppositeID = -1;//�Է�����ת����ID
        private int OppositeUDPPort = -1;//�Է��ļ����䱾��UDP�˿�
        private IPAddress OppositeUDPIP = IPAddress.Parse("127.0.0.1");
        private int selfUDPPort = -1;//�Լ����ļ����䱾��UDP�˿�
        private CSS.IM.Library.Class.NetCommunicationClass netClass = CSS.IM.Library.Class.NetCommunicationClass.None;//�ļ����������õ�ͨ��Э��,��ʼΪͨѶû�гɹ�

        #endregion

        #region  �ļ������¼� 
        /// <summary>
        /// �ļ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmittedEventHandler(object sender, fileTransmitEvnetArgs e);//�ļ���������¼�
        public event fileTransmittedEventHandler fileTransmitted ;

        /// <summary>
        /// ȡ���ļ������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitCancelEventHandler(object sender, fileTransmitEvnetArgs e);//ȡ���ļ������¼�
        public event fileTransmitCancelEventHandler fileTransmitCancel ;

        /// <summary>
        /// �ļ����俪ʼ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitBeforeEventHandler(object sender, fileTransmitEvnetArgs e);//�����ļ������¼�
        public event fileTransmitBeforeEventHandler fileTransmitBefore;

        /// <summary>
        ///  �ļ����䳬ʱ�¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void fileTransmitOutTimeEventHandler(object sender, fileTransmitEvnetArgs e);//�����ļ����ͳ�ʱ
        public event fileTransmitOutTimeEventHandler fileTransmitOutTime ;

        /// <summary>
        /// �ļ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public delegate void fileTransmitErrorEventHandler(object sender, fileTransmitEvnetArgs e);//�ļ����ʹ��� 
        //public event fileTransmitErrorEventHandler fileTransmitError ;

        /// <summary>
        /// �¼������ͻ��յ��ļ�����
        /// </summary>
        /// <param name="sender">����</param>
        /// <param name="e"></param>
        public delegate void fileTransmittingEventHandler(object sender, fileTransmitEvnetArgs e);//���ͻ��յ��ļ����� 
        public event fileTransmittingEventHandler fileTransmitting;

        public delegate void getFileProxyIDEventHandler(object sender, int proxyID);//���ͻ��յ��ļ����� 
        public event getFileProxyIDEventHandler getFileProxyID ;

        public delegate void ConnectedEventHandler(object sender, CSS.IM.Library.Class.NetCommunicationClass NetCommunicationClass);//���ͻ��յ��ļ����� 
        public event ConnectedEventHandler fileTransmitConnected;

        public delegate void GetUDPPortEventHandler(object sender, int Port, bool udpHandshakeInfoClass);//��ñ���UDP�˿��¼� 
        public event GetUDPPortEventHandler fileTransmitGetUDPPort;

        #endregion

        #region ���ô����ļ�����
        /// <summary>
        /// ���ô����ļ�����
        /// </summary>
        /// <param name="isSend">��ʶ�ļ��Ƿ��ͻ��ǽ���</param>
        /// <param name="FullFileName">��������ļ���������Ҫ���ŵ��ļ�·��</param>
        /// <param name="FileName">�ļ���</param>
        /// <param name="FileLen">�ļ�����</param>
        /// <param name="fileExtension">�ļ���չ��</param>
        /// <param name="FileMD5Value">�ļ�MD5ֵ</param>
        /// <param name="ServerIP">������IP��ַ</param>
        /// <param name="ServerUDPPort">������UDP����˿�</param>
        /// <param name="ServerTCPPort">������TCP����˿�</param>
        /// <param name="selfUserInfo">�Լ��������û���Ϣ</param>
        /// <param name="OppositeUserInfo">�Է��������û���Ϣ</param>
        public void SetParameter(bool IsSend, string FullFileName, string FileName, int FileLen, string fileExtension, string FileMD5Value)
        {
            //�ļ�����ǰ����˫�����ӵĲ������ú���
            this._IsSend = IsSend;
            //this._serverIp = ServerIP;//��ȡ������IP��ַ
            //this._serverUDPPort = ServerUDPPort;//��ȡ�ļ�������UDP����˿�
            //this._serverTCPPort = ServerTCPPort;//��ȡ�ļ�������TCP����˿�
            this.Extension = fileExtension;//��ȡ�ļ���չ��
            this._FileLen = FileLen;//��ȡ�ļ�����
            this._fileName = FileName;//�ļ�����
            this.FileMD5Value = FileMD5Value;//��ȡ�ļ���MD5ֵ
            //this._selfUserInfo = selfUserInfo;//��ȡ�Լ��������û���Ϣ
            //this._OppositeUserInfo = OppositeUserInfo;//��ȡ�Է��������û���Ϣ

            this.FileLenStr = CSS.IM.Library.Class.Calculate.GetSizeStr(FileLen);//����ļ��ߴ��ַ���

            this.readFileCount = FileLen / this.maxReadWriteFileBlock;//����ļ���д����

            if (FileLen % this.maxReadWriteFileBlock != 0)
                this.readFileCount++;//�����д�ļ����࣬�����д������1

            if (_IsSend)//������ļ�������
                this._fullFileName  = FullFileName;//���ļ��ľ���·��
        }
        #endregion 

        #region �����ļ�
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="currTransmittedLen">��ǰ�Ѿ�������ɵ��ļ����ݳ���</param>
        private void sendFile(int currTransmittedLen)
        {

            //Console.WriteLine("sendFile_currTransmittedLen:" + currTransmittedLen);
            if (currTransmittedLen >= this._FileLen)
            {
                //Console.WriteLine("sendFile_currTransmittedLen:" + currTransmittedLen);
                onFileTransmitted();//�����ļ���������¼�
                return;//����Է�Ҫ���͵����ݿ���ʼλ�ô����ļ��ߴ�����Ϊ�ǷǷ������˳�
            }

            if (!IsSendState)
            {
                IsSendState = true;//���ô���

                if (this.fileTransmitBefore != null)//�����ļ���ʼ����ǰ�¼� 
                    this.fileTransmitBefore(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));
            }

            if (IsReadWriteFile(currTransmittedLen))//�����ǰ����Ҫ��д�ļ�
            {
                //���ļ����ڴ����
                //if (this.currReadCount + 1 == this.readFileCount)//��������һ�ζ�д�ļ����������ļ�β����ȫ�����뵽�ڴ�
                //    FileBlock = new byte[this._FileLen - this.currReadCount * this.maxReadWriteFileBlock];
                //else
                //    FileBlock = new byte[this.maxReadWriteFileBlock];

                FileBlock = new byte[this._FileLen];
                ////////////////////////�ļ�����
                FileStream fw = new FileStream(this._fullFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                fw.Seek(currTransmittedLen, SeekOrigin.Begin);//�ϴη��͵�λ��
                fw.Read(FileBlock, 0, FileBlock.Length);
                //�����д�����������첽��ʽ   
                //fw.BeginRead(myData.Buffer, 0, assignSize, new AsyncCallback(AsyncRead), myData);
                ///ʵ�ֶ��߳�ͬʱ��д�ļ�
                fw.Close();
                fw.Dispose();
                ///////////////////////////
                //FileStream file_s=new FileStream(this._fullFileName+"aa.jpg",FileMode.CreateNew,FileAccess.ReadWrite,FileShare.ReadWrite);
                //file_s.Write(FileBlock, 0, (int)FileBlock.Length);
                //file_s.Flush();
                //file_s.Close();
                //file_s.Dispose();

                //this.currReadCount++;//�ļ�����������1

                //FileBlock = new byte[this._FileLen];
            }

            //FileStream file_s = new FileStream(this._fullFileName + Guid.NewGuid().ToString()+".jpg", FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
            //file_s.Write(FileBlock, 0, (int)FileBlock.Length);
            //file_s.Flush();
            //file_s.Close();
            //file_s.Dispose();


            int offSet = currTransmittedLen % this.maxReadWriteFileBlock;// ���Ҫ���͵ľ���λ��

            byte[] buffer;
            Console.WriteLine("FileLen:" + this._FileLen);
            Console.WriteLine("FileBlock:" + this.FileBlock.Length);
            if (offSet + this._mtu > this.FileBlock.Length)
                buffer = new byte[this.FileBlock.Length - offSet];//Ҫ���͵Ļ�����
            else
                buffer = new byte[this._mtu];//Ҫ���͵Ļ�����

            Buffer.BlockCopy(this.FileBlock, offSet, buffer, 0, buffer.Length);//���䱣����Buffer�ֽ�����

            currTransmittedLen += buffer.Length;
            this.sendData(new CSS.IM.Library.Class.msgFile((byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileBlock, this.serverSelfID, this.serverOppositeID, (long)currTransmittedLen, buffer));//�����Ѷ�ȡ���ļ����ݸ��Է�

            if (this.fileTransmitting != null)//�����յ������ļ������¼� 
                this.fileTransmitting(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, currTransmittedLen, this.FileMD5Value));

        }
        #endregion 

        #region  ��ȡ�ļ����ݰ�����
        /// <summary>
        /// ���ͻ�ȡ�ļ����ݰ�����
        /// </summary>
        private void sendRequestGetFileData()
        {
            this.timerGetFileOut.Enabled = false;
            this.OutTime = 0;
            this.sendData(new CSS.IM.Library.Class.msgFile((byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTransmit, this.serverSelfID, this.serverOppositeID, (long)this.currGetPos, new byte[0])); //����Է������ļ����ݰ�
            this.LastPos = this.currGetPos;
            this.timerGetFileOut.Enabled = true;
        }
        #endregion

        #region �����յ����ļ����ݿ�
        /// <summary>
        /// ����Է������ļ����ݿ�
        /// </summary>
        private void ReceivedFileBlock(CSS.IM.Library.Class.msgFile msg)//���Է������ļ����ݿ����
        {
            if (msg.pSendPos > this.currGetPos)//������͹��������ݴ��ڵ�ǰ��õ�����
            {
                if (this.IsReadWriteFile(this.currGetPos))
                {
                    //�����Ƕ�һ���ļ����ڴ����
                    //FileBlock = new byte[this._FileLen];
                    if (this.currReadCount + 1 == this.readFileCount)//��������һ�ζ�д�ļ����������ļ�β����ȫ�����뵽�ڴ�
                        FileBlock = new byte[this._FileLen - this.currReadCount * this.maxReadWriteFileBlock];
                    else
                        FileBlock = new byte[this.maxReadWriteFileBlock];

                    this.currReadCount++;//�ļ�����������1
                }

                int offSet = this.currGetPos % this.maxReadWriteFileBlock;// ���Ҫ��д�ڴ�ľ���λ��
                Buffer.BlockCopy(msg.FileBlock, 0, this.FileBlock, offSet, msg.FileBlock.Length);//���䱣����Buffer�ֽ�����

                this.currGetPos = (int)msg.pSendPos;

                //if (this.fileTransmitting != null)//�����յ������ļ������¼� 
                //    this.fileTransmitting(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));

                if (this.IsReadWriteFile(this.currGetPos) || this.currGetPos == this._FileLen)
                {
                    ////////////////////////�ļ�����
                    FileStream fw = new FileStream(this._fullFileName, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                    //fw.Seek(fw.l, SeekOrigin.Begin);//�ϴη��͵�λ��
                    fw.Write(this.FileBlock, 0, this.FileBlock.Length);
                    //�����д�����������첽��ʽ   
                    //fw.BeginRead(myData.Buffer, 0, assignSize, new AsyncCallback(AsyncRead), myData);
                    ///ʵ�ֶ��߳�ͬʱ��д�ļ�
                    fw.Close();
                    fw.Dispose();

                    onFileTransmitted();//�����ļ���������¼�
                    ///////////////////////////
                }

                if (this.currGetPos == this._FileLen)//����ļ�������ɣ�������������¼�
                {
                    msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTranstmitOver;
                    msg.SendID = this.serverSelfID;
                    msg.RecID = this.serverOppositeID;
                    this.sendData(msg);//���߶Է��ļ��������
                    onFileTransmitted();//�����ļ���������¼�
                    //return;//�ļ�����
                }

                this.sendRequestGetFileData();//���۵�ǰ��ö������ݣ���Ҫ��Է�������һ���ݰ�
            }
        }
        #endregion

        #region �ļ�������� 
        /// <summary>
        /// �ļ��������������� 
        /// </summary>
        private void onFileTransmitted()
        {
            if (this.fileTransmitted  != null)
                this.fileTransmitted(this, new fileTransmitEvnetArgs(this._IsSend, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));
            try
            {
                if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.TCP)
                    this.asyncTCPClient1.Disconnect();
                else
                {
                    this.sockUDP1.CloseSock();
                    this.sockUDP1.Dispose();
                }
            }
            catch { }

            try
            {
                timerConnection.Enabled = false;
                timerGetFileOut.Enabled = false;
                timersUdpPenetrate.Enabled = false;
            }
            catch (Exception)
            {
                
            }
        }
        #endregion

        #region �жϵ�ǰ�Ƿ���Ҫ��д�ļ�
        /// <summary>
        /// �жϵ�ǰ�Ƿ���Ҫ��д�ļ�
        /// </summary>
        /// <param name="currTransmittedLen">��ǰ�ļ������λ��</param>
        /// <returns></returns>
        private bool IsReadWriteFile(int currTransmittedLen)
        {
            if (currTransmittedLen % this.maxReadWriteFileBlock == 0)
                return true;
            else
                return false;
        }

        #endregion
         
        #region winSock ��������
        /// <summary>
        /// sockUDP �����ļ�����
        /// </summary>
        /// <param name="msg">�ļ���Ϣ</param>
        private void sendData(CSS.IM.Library.Class.msgFile msg)
        {
            try
            {
                //if (this.netClass == CSS.IM.Library.Class.NetCommunicationClass.LanUDP)//����Ǿ�����ͨ��
                    this.sockUDP1.Send(this.OppositeUDPIP,this.OppositeUDPPort, msg.getBytes());//����UDP�������ݵ��Է�������IP��˿�
            }
            catch
            { }
        }

        /// <summary>
        /// sockUDP ��������
        /// </summary>
        /// <param name="Ip">������IP</param>
        /// <param name="Port">�����߶˿�</param>
        /// <param name="MsgContent">Ҫ���͵��ֽڿ�</param>
        public void sendData(System.Net.IPAddress Ip, int Port, byte[] MsgContent)
        {
            try
            {
                this.sockUDP1.Send(Ip, Port, MsgContent);
            }
            catch { }
        }
        #endregion
            
        #region �ļ����䳬ʱ��
        /// <summary>
        /// ���һ�λ���ļ����ݰ���λ��
        /// </summary>
        private int LastPos = 0;

        private void timerGetFileOut_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this._isCancelTransmit || this.currGetPos ==this._FileLen)
            {
                this.timerGetFileOut.Enabled = false;
                this.Dispose();
                return;
            }
            OutTime++;

            if (OutTime > 3)
            {
                if (this.LastPos == this.currGetPos)//��������Ӻ�δ�����һ���ݰ�����ʱ
                    this.sendRequestGetFileData();
                ///�����ļ����ճ�ʱ�¼�
                if (this.fileTransmitOutTime != null)
                    this.fileTransmitOutTime(this, new fileTransmitEvnetArgs( this._IsSend , this._fullFileName , this._fileName , "", this._FileLen , this.currGetPos,this.FileMD5Value ));
            }
        }
        #endregion

        #region ȡ���ļ�����
        /// <summary>
        /// ȡ���ļ�����
        /// </summary>
        public void CancelTransmit(bool isMe)
        {
            this._isCancelTransmit = true;//ȡ���ļ�����Ϊ��

            try
            {
                if (this.netClass != CSS.IM.Library.Class.NetCommunicationClass.TCP)
                {
                    this.sockUDP1.CloseSock();//�ر�sockUDP1�˿ڣ����ռ�õ���Դ 
                    this.sockUDP1 = null;
                }
                {
                    this.asyncTCPClient1.Disconnect();
                    this.asyncTCPClient1.Dispose();
                    this.asyncTCPClient1 = null;
                }
            }
            catch { }

            if (this.fileTransmitCancel != null)
                this.fileTransmitCancel(this, new fileTransmitEvnetArgs(isMe, this._fullFileName, this._fileName, "", this._FileLen, this.currGetPos, this.FileMD5Value));//�������ļ�ȡ�������¼���(�Լ�ȡ����)
        }
        #endregion

        #region ��ʼ���������ⲿ����Ϣ Listen()
        /// <summary>
        /// ��ʼ���������ⲿ����Ϣ
        /// </summary>
        private int UDPListen()//UDP��ʼ���������ⲿ����Ϣ.
        {
        xx:
            System.Random i = new Random();
            int j = i.Next(2000, 65530);
            try
            {
                this.sockUDP1.Listen(j);
                this.selfUDPPort = j;
                //Calculate.WirteLog(this._IsSend.ToString() + "����UDP�˿ںţ�" + j.ToString());
                return this.selfUDPPort;
            }
            catch
            { goto xx; }
        }
        #endregion

        #region TCP����

        private void asyncTCPClient1_OnConnected(object sender,  CSS.IM.Library.Net.SockEventArgs  e)
        {
            //CSS.IM.Library.Calculate.WirteLog("������");
            CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile((byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileTransmitProxyID, -1, -1, 0, new byte[1]);
            this.asyncTCPClient1.SendData(msg.getBytes());//�������������ת����ID��
        }

        private void asyncTCPClient1_OnDataArrival(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            //if (e.Data.Length < 10) return;
            CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile(e.Data);
            this.DataArrival(msg,CSS.IM.Library.Class.NatClass.Tcp ,null,0);
        }
         
        private void asyncTCPClient1_OnDisconnected(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            //CSS.IM.Library.Calculate.WirteLog("��������Ͽ�����");
        }

        private void asyncTCPClient1_OnError(object sender, CSS.IM.Library.Net.SockEventArgs e)
        {
            //CSS.IM.Library.Calculate.WirteLog("�ļ��������"+ e.ErrorCode + e.ErrorMessage );
        }
        #endregion
        
        #region UDP���ݵ����¼�
        /// <summary>
        /// UDP���ݵ����¼�
        /// </summary>
        /// <param name="e">UDP���ݲ���</param>
        private void sockUDP1_DataArrival(object sender,CSS.IM.Library.Net.SockEventArgs  e)
        {
            //if (e.Data.Length < 10) return;
            CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile(e.Data);
            this.DataArrival(msg,CSS.IM.Library.Class.NatClass.FullCone, e.IP,e.Port);
        }
        #endregion

        #region ���ݵ��� 
        private void DataArrival(CSS.IM.Library.Class.msgFile msg, CSS.IM.Library.Class.NatClass netClass, System.Net.IPAddress Ip, int Port)
        {
            switch (msg.InfoClass)
            {
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileTransmitProxyID://����Լ��ӷ������ϻ����ת����ID 
                    {
                        this.serverSelfID = msg.SendID;
                        if (this.serverOppositeID != -1)//����Ƿ��ͷ������ת����ID������߶Է���ʼ�ս��ļ�
                        {
                            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.TCP;//��ʶ��ǰͨ��Э��ΪTCP
                            //this._mtu = 1200;//�����������ļ�ʱ����MTUֵ����Ϊ1200ʹ·���������ǽת������
                            msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit;
                            msg.SendID = this.serverSelfID;
                            msg.RecID = this.serverOppositeID;
                            this.sendData(msg);
                        }
                        else if (this.getFileProxyID != null)//����ǽ��շ��򴥷������ת����ID��֮�Է�
                            this.getFileProxyID(this, this.serverSelfID);
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTransmit://����ļ���������
                    {
                        //Calculate.WirteLog("�����ļ����Է�");
                        this.sendFile((int)msg.pSendPos);//�����ļ����Է�
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetFileBlock :// ��öԷ�������ļ����ݰ�
                    {
                        //Calculate.WirteLog("�յ��ļ�����");
                        this.ReceivedFileBlock(msg);//�Է������ļ����ݹ���,�������ݵ��ļ�
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit://�����Ѿ��������Է�Ҫ��ʼ�����ļ�
                    {
                        this.serverOppositeID = msg.SendID;//��öԷ�ID
                        if (netClass == CSS.IM.Library.Class.NatClass.Tcp)//�����TCPͨ��
                        {
                            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.TCP;//����TCPЭ�鴫���ļ� 
                        }
                        else
                        {
                            this.netClass = CSS.IM.Library.Class.NetCommunicationClass.WanNoProxyUDP;//����UDPЭ�鴫��  
                        }

                        if (this.fileTransmitConnected != null)//����ͨ�ųɹ��¼������˳�ͨ�Ų���
                            this.fileTransmitConnected(this, this.netClass);

                        if (!this.IsSendState)//����ļ���û�п�ʼ���ͣ�����
                            this.sendRequestGetFileData();//�����ļ����Է�
                        //Calculate.WirteLog("�����Ѿ��������Է�Ҫ��ʼ�����ļ�");
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.FileTranstmitOver ://�ļ��������
                    {
                        this.onFileTransmitted();
                    }
                    break;


                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeLAN://�յ��Է�������UDP��������
                    {
                        this.OppositeUDPIP = Ip;//�������öԷ��ľ�����IP
                        this.OppositeUDPPort = Port;//�������öԷ��ľ�����UDP�˿�
                        msg.InfoClass =(byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfLanUDPData;//���߶Է��յ�����������Ϣ
                        this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                        //Calculate.WirteLog(this._IsSend.ToString()+ "�յ��Է�������UDP��������:"+ Ip.ToString() +":"+ Port.ToString());
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfLanUDPData://�Է��յ��Լ����͵ľ�����UDP��������
                    {
                        this.netClass = CSS.IM.Library.Class.NetCommunicationClass.LanUDP;//��ʶ��Է�����������ͨ�ųɹ�
                       if (this._IsSend)//����Ƿ����ļ���һ�����ҶԷ���֮�յ��Լ����������ݣ���ͨ��ͨ����ͨ
                       {
                           //this._mtu = 1400;//�����������ļ�ʱ����MTUֵ����Ϊ5120��5k������ٶ�
                           msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit;
                           msg.SendID = 0;
                           this.sendData(msg);//���߶Է���ʼ�����ļ�
                       } 
                       //Calculate.WirteLog(Ip.ToString() + ":" + Port.ToString() + "�Է��յ��Լ����͵ľ�����UDP��������" + this._IsSend.ToString());
                    }
                    break;

                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeWAN ://�յ��Է�������UDP��������
                    {
                        this.OppositeUDPIP = Ip;//�������öԷ��Ĺ�����IP
                        this.OppositeUDPPort = Port;//�������öԷ��Ĺ�����UDP�˿�
                        msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfWanUDPData ;//���߶Է��յ�����������Ϣ
                        this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                        //Calculate.WirteLog(this._IsSend.ToString()+ "�յ��Է�������UDP��������:"+ Ip.ToString() +":"+ Port.ToString());
                    }
                    break;
                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.IsOppositeRecSelfWanUDPData://�Է��յ��Լ����͵ľ�����UDP��������
                    {
                        this.netClass = CSS.IM.Library.Class.NetCommunicationClass.WanNoProxyUDP;//��ʶ��Է�����������ֱ��ͨ�ųɹ�
                        if (this._IsSend)//����Ƿ����ļ���һ�����ҶԷ���֮�յ��Լ����������ݣ���ͨ��ͨ����ͨ
                        {
                            //this._mtu = 1200;//�����������ļ�ʱ����MTUֵ����Ϊ1200��1k�ٶ�
                            msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.BeginTransmit;
                            msg.SendID = 0;
                            this.sendData(msg);//���߶Է���ʼ�����ļ�
                        }
                        //Calculate.WirteLog(Ip.ToString() + ":" + Port.ToString() + "�Է��յ��Լ����͵ľ�����UDP��������" + this._IsSend.ToString());
                    }
                    break;

                case (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetUDPWANInfo://��÷��������ص��ļ������׽��ֹ�����UDP�˿�
                    {
                        this.selfUDPPort=msg.SendID;//�������öԷ��Ĺ�����UDP�˿�
                        if (!this.IsGetWanUDP && this.fileTransmitGetUDPPort != null)
                        {
                            this.IsGetWanUDP = true;//��ʶ�Ѿ�������WAN UDP�˿ڻ�ȡ�¼� 
                            this.fileTransmitGetUDPPort(this, this.selfUDPPort, true);
                        }
                        //Calculate.WirteLog(this._IsSend.ToString() + "��÷��������ص��ļ������׽��ֹ�����UDP�˿�:" + this.selfUDPPort);
                    }
                    break;
            }
        }
        #endregion


        #region ���öԷ��ļ�����UDP���ض˿�
        /// <summary>
        /// ���öԷ��ļ�����UDP���ض˿� 
        /// </summary>
        /// <param name="Port">�ļ�����UDP���ض˿�</param>
        public void setFileTransmitGetUdpLocalPort(IPAddress ip,int Port, bool udpHandshakeInfoClass)
        {
            this.ServerIp = ip;
            this.OppositeUDPIP = ip;
            this.OppositeUDPPort = Port;//���öԷ�UDP�˿ں�
            this.UdpHandshakeInfoClass = udpHandshakeInfoClass;

            System.Threading.Thread.Sleep(100);

            if (!udpHandshakeInfoClass)//����Լ���δUDP����,���þ�������ʽͨ��
            {
                if (!this.sockUDP1.Listened)//���û������
                    this.UDPListen();//���UDP����
                if (!this.IsGetLanUDP && this.fileTransmitGetUDPPort != null)//��֮�Է����ض˿ڣ�������ö˿��¼�
                {
                    this.IsGetLanUDP = true;//��ʶ�Ѿ�������LAN UDP�¼�
                    this.fileTransmitGetUDPPort(this, this.selfUDPPort, false);
                }
                //Calculate.WirteLog(this._IsSend.ToString() + "����Լ���δUDP����,���þ�������ʽͨ��");
            }
            else if (udpHandshakeInfoClass)//���ù�������ʽͨ��
            {
                if (!this.sockUDP1.Listened)//���û������
                    this.UDPListen();//���UDP����
                CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile();
                msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.GetUDPWANInfo;//ͨ��Э��
                this.sockUDP1.Send(ServerIp, _serverUDPPort, msg.getBytes());//����ļ������׽��ֵĹ�����UDP�˿�
                //Calculate.WirteLog(this._IsSend.ToString() + "����ļ������׽��ֵĹ�����UDP�˿�");
            }

            if (!timersUdpPenetrate.Enabled)//���δ���֣���ʼ����
                timersUdpPenetrate.Enabled = true;//��ʼ��Է�UDP�˿�����(��),����ɹ�����ʾ���Խ���UDPͨ��
        }

        /// <summary>
        /// UDP���������ֿ��ܣ���һ��Ϊ����������Ϊfalse���ڶ���Ϊ����������Ϊtrue;
        /// </summary>
        private bool UdpHandshakeInfoClass = false ;//UDP���������ֿ��ܣ���һ��Ϊ����������Ϊfalse���ڶ���Ϊ����������Ϊtrue;

        #region timers������������
        int HandshakeCount = 0;//���ִ���
        private void timersUdpPenetrate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            HandshakeCount++;
            this.UdpHandshake();// ��Է�UDP�˿ڷ�����������
            if (HandshakeCount == 10)//���3���ӣ���ֹͣ��
            {
                timersUdpPenetrate.Enabled = false;//��ʼ��Է�UDP�˿ڴ�
                HandshakeCount = 0;
            }
        }

        /// <summary>
        /// ��Է�UDP�˿ڷ�����������
        /// </summary>
        private void UdpHandshake()
        {
            try
            {
                CSS.IM.Library.Class.msgFile msg = new CSS.IM.Library.Class.msgFile();
                if (!UdpHandshakeInfoClass)//���Ϊ����������
                {
                    msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeLAN;
                    this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                }
                else if (UdpHandshakeInfoClass)//���Ϊ����������
                {
                    msg.InfoClass = (byte)CSS.IM.Library.Class.ProtocolFileTransmit.HandshakeWAN;
                    this.sockUDP1.Send(this.OppositeUDPIP, this.OppositeUDPPort, msg.getBytes());
                }
            }
            catch { }
        }
        #endregion

        #endregion

        #region �����ļ�����
        /// <summary>
        /// ��ʼ�����ļ�
        /// </summary>
        /// <param name="savefullFileName">�ļ�����·��</param>
        public void startIncept(string savefullFileName)
        {
            this._fullFileName = savefullFileName;//��ǽ��յ��ļ�����·��
            this.timerConnection.Enabled = true;//��ʼ���˫������
        }

        /// <summary>
        /// ��ʶ�Ƿ񴥷���LanUDP�˿ڴ����¼�
        /// </summary>
        private bool IsGetLanUDP = false;
        /// <summary>
        ///  ��ʶ�Ƿ񴥷���WanUDP�˿ڴ����¼�
        /// </summary>
        private bool IsGetWanUDP = false;
        /// <summary>
        /// ��ʱ����
        /// </summary>
        private byte TimeOutCount = 0; 


        /// <summary>
        /// ���˫�����ӵĹ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerConnection_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.netClass != CSS.IM.Library.Class.NetCommunicationClass.None)//���UDPͨ�ųɹ�
            {
                this.timerConnection.Enabled = false;//ֹͣͨ��״̬���
                if (this.fileTransmitConnected != null)//����ͨ�ųɹ��¼������˳�ͨ�Ų���
                    this.fileTransmitConnected(this, this.netClass);
                return;
            }

            TimeOutCount++;

            if (TimeOutCount == 1) ///����˫������ͬһ�������ڣ������P2P UDP��ʽ�շ�����
            {
                this.UdpHandshakeInfoClass = false;//UDP���������ֿ��ܣ���һ��Ϊ����������Ϊfalse���ڶ���Ϊ����������Ϊtrue;��ʱ���Ϊ������false
                if (!this.sockUDP1.Listened)//���û��������������
                    this.UDPListen();
                if (!this.IsGetLanUDP && this.fileTransmitGetUDPPort != null)//����UDP�˿������ɹ��¼����Ա�Է���֪�Լ���UDP�˿�
                {
                    this.IsGetLanUDP = true;//��ʶ�Ѿ�������LanUDP�˿ڴ����¼�
                    this.fileTransmitGetUDPPort(this, this.selfUDPPort, this.UdpHandshakeInfoClass);
                }
            }
        }

        #endregion
    }
    #endregion

    #region  P2P�ļ����伯��
    /// <summary>
    /// �����û����ϡ�
    /// </summary>
    [Serializable]
    public class p2pFileTransmitCollectionsEX : System.Collections.CollectionBase,IDisposable
    {
        public p2pFileTransmitCollectionsEX()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        // Get UserInfo at the specified index
        public p2pFileTransmitEX this[int index]
        {
            get
            {
                return ((p2pFileTransmitEX)InnerList[index]);
            }
        }

 
        public void add(p2pFileTransmitEX _p2pFileTransmit)
        {
            base.InnerList.Add(_p2pFileTransmit);
        }
       
        public void Romove(p2pFileTransmitEX _p2pFileTransmit)
        {
            base.InnerList.Remove(_p2pFileTransmit);
        }

        public p2pFileTransmitEX find(string FileMD5)
        {
            foreach (p2pFileTransmitEX _p2pFileTransmit in this)
                if (_p2pFileTransmit.FileMD5Value == FileMD5)
                    return _p2pFileTransmit;
            return null;
        }


        public void Dispose()
        {
            foreach (p2pFileTransmitEX _p2pFileTransmit in this)
                _p2pFileTransmit.Dispose();
        }
    }
    #endregion     
}

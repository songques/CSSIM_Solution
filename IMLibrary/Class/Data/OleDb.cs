using System;
using System.Data.OleDb ;
using System.Data;
using System.ComponentModel;

namespace IMLibrary.Data 
{
	/// <summary>
	/// ClassOptionData ��ժҪ˵����
	/// </summary>
	public sealed class OleDb  
	{
        /// <summary>
        /// ���û��ȡ�����ַ���
        /// </summary>
        public static string ConStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Record.dll;Persist Security Info=False";//���ݿ������ַ��� 

        private OleDb()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
       
        /// <summary>
        /// ִ���κ�SQL��䣬������Ӱ�������
        /// </summary>
        /// <param name="SQLStr">SQL����</param>
        /// <returns>������Ӱ�������</returns>
		public static int ExSQL(string SQLStr) 
		{
			try
			{
				OleDbConnection cnn = new OleDbConnection(ConStr);
				OleDbCommand cmd =new OleDbCommand(SQLStr ,cnn);
				cnn.Open ();
				int i =0;
				i=cmd.ExecuteNonQuery();
				cmd.Dispose();
				cnn.Close();
				cnn.Dispose();   
				return i;
			}
			catch{  return 0;}

		}

        /// <summary>
        /// ִ���κ�SQL��䣬������Ӱ�������
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="par"></param>
        /// <param name="SQLStr">SQL����</param>
        /// <returns>������Ӱ�������</returns>
		public static  int ExSQLLengData(object Data,string par,string SQLStr)//
		{
			try
			{
				OleDbConnection cnn = new OleDbConnection(ConStr);
				OleDbCommand cmd =new OleDbCommand(SQLStr ,cnn);
				cnn.Open ();
				int i =0;
				cmd.Parameters.Add(par, System.Data.OleDb.OleDbType.Binary);
				i=cmd.ExecuteNonQuery();
				cmd.Dispose();
				cnn.Close();
				cnn.Dispose();   
				return i;
			}
			catch{return 0;}

		}

        /// <summary>
        /// ִ���κ�SQL��ѯ��䣬������Ӱ�������
        /// </summary>
        /// <param name="SQLStr">SQL����</param>
        /// <returns>������Ӱ�������</returns>
		public static  int ExSQLR(string SQLStr) 
		{
			try
			{
				OleDbConnection cnn = new OleDbConnection(ConStr);
				OleDbCommand cmd =new OleDbCommand(SQLStr ,cnn);
				cnn.Open ();
				OleDbDataReader dr;
				int i =0;
				dr=cmd.ExecuteReader(CommandBehavior.CloseConnection );
                if(dr!=null)
				while(dr.Read())
				{i++;}
				cmd.Dispose();
				cnn.Close();
				cnn.Dispose();
				return i;
			}
			catch{return 0;}

		}

        /// <summary>
        /// ִ���κ�SQL��ѯ��䣬����һ���ֶ�ֵ
        /// </summary>
        /// <param name="field">�ֶ���</param>
        /// <param name="SQLStr">SQL����</param>
        /// <returns>����һ���ֶ�ֵ</returns>
		public static object ExSQLReField(string field ,string SQLStr) 
		{
			try
			{
				OleDbConnection cnn = new OleDbConnection(ConStr);
				OleDbCommand cmd =new OleDbCommand(SQLStr ,cnn);
				cnn.Open ();
				OleDbDataReader dr;
				object fieldValue=null;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr != null && dr.Read())
				{fieldValue=dr[field];}
                dr.Close();
				cmd.Dispose();
				cnn.Close();
				cnn.Dispose();   
				return fieldValue;
			}
			catch{return null;}

		}

        /// <summary>
        /// ִ���κ�SQL��ѯ��䣬����һ��OleDbDataReader
        /// </summary>
        /// <param name="SQLStr">SQL����</param>
        /// <returns>����һ��OleDbDataReader</returns>
		public static  OleDbDataReader ExSQLReDr(string SQLStr) 
		{
			try
			{
				OleDbConnection cnn = new OleDbConnection(ConStr);
				OleDbCommand cmd =new OleDbCommand(SQLStr ,cnn);
				cnn.Open ();
				OleDbDataReader dr;
				dr=cmd.ExecuteReader(CommandBehavior.CloseConnection);
				return dr;
			}
			catch{return null;}
		}

	}
}

namespace IMLibrary.SqlData
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.InteropServices;
    using System.Xml;
	using System.Configuration;
 
    /// <summary>
    /// SQL���ݿ������
    /// </summary>
    public sealed class DataAccess
    {
        /// <summary>
        /// ���û��ȡ���ݿ������ַ���
        /// </summary>
        public static string ConnectionString = ConfigurationSettings.AppSettings["SqlConnectionString"];

        private DataAccess()
        {
        }

        
        private static void AssignParameterValues(SqlParameter[] commandParameters, object[] parameterValues, bool IncludeReturnVarParameter)
        {
            if ((commandParameters != null) && (parameterValues != null))
            {
                int num;
                if (IncludeReturnVarParameter)
                {
                    num = 1;
                    if (commandParameters.Length != (parameterValues.Length + 1))
                    {
                        throw new ArgumentException("Parameter count does not match Parameter Value count.");
                    }
                }
                else
                {
                    num = 0;
                    if (commandParameters.Length != parameterValues.Length)
                    {
                        throw new ArgumentException("Parameter count does not match Parameter Value count.");
                    }
                }
                int index = 0;
                int length = parameterValues.Length;
                while (index < length)
                {
                    commandParameters[index + num].Value = parameterValues[index];
                    index++;
                }
            }
        }

        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter parameter in commandParameters)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput) && (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="spName">�洢��������</param>
        /// <param name="parameterValues">�����б�</param>
        /// <returns></returns>
        public static int ExecProc(string spName, params object[] parameterValues)
        {
            return ExecProc(ConnectionString, spName, parameterValues);
        }

        /// <summary>
        /// ִ�д洢����
        /// </summary>
        /// <param name="spName">�洢��������</param>
        /// <param name="ReturnValue">����ֵ</param>
        /// <param name="parameterValues">�����б�</param>
        /// <returns></returns>
        public static int ExecProc(string spName, out object ReturnValue, params object[] parameterValues)
        {
            return ExecProc(ConnectionString, spName, out ReturnValue, parameterValues);
        }

        private static int ExecProc(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = DataAccessParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues, false);
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
        }

        private static int ExecProc(string connectionString, string spName, out object ReturnValue, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] commandParameters = DataAccessParameterCache.GetSpParameterSet(connectionString, spName, true);
                AssignParameterValues(commandParameters, parameterValues, true);
                return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, out ReturnValue, commandParameters);
            }
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, out ReturnValue);
        }

        /// <summary>
        /// ִ��SQL����
        /// </summary>
        /// <param name="commandText">�����ı�</param>
        /// <param name="commandParameters">�����б�</param>
        /// <returns></returns>
        public static int ExecSql(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecSql(ConnectionString, commandText, commandParameters);
        }

        private static int ExecSql(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connectionString, CommandType.Text, commandText, commandParameters);
        }

        private static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteDataSet(connectionString, commandType, commandText, null);
        }

        private static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connection, null, commandType, commandText, commandParameters);
        }

        private static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteDataSet(connection, commandType, commandText, commandParameters);
            }
        }

        private static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        private static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, (SqlParameter[]) null);
        }

        private static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connection, null, commandType, commandText, commandParameters);
        }

        private static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, out object ReturnValue)
        {
            return ExecuteNonQuery(connectionString, commandType, commandText, out ReturnValue, null);
        }

        private static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
            }
        }

        private static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, out object ReturnValue, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(connection, null, commandType, commandText, out ReturnValue, commandParameters);
        }

        private static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            return command.ExecuteNonQuery();
        }

        private static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, out object ReturnValue, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteNonQuery(connection, commandType, commandText, out ReturnValue, commandParameters);
            }
        }

        private static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, out object ReturnValue, params SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            int num = command.ExecuteNonQuery();
            ReturnValue = command.Parameters["@RETURN_VALUE"].Value;
            return num;
        }

        private static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteReader(connectionString, commandType, commandText, null);
        }

        private static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlDataReader reader;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                reader = ExecuteReader(connection, null, commandType, commandText, SqlConnectionOwnership.Internal, commandParameters);
            }
            catch
            {
                connection.Close();
                throw;
            }
            return reader;
        }

        private static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText, SqlConnectionOwnership connectionOwnership, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(connection, null, commandType, commandText, connectionOwnership, commandParameters);
        }

        private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlConnectionOwnership connectionOwnership, SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            if (connectionOwnership == SqlConnectionOwnership.External)
            {
                return command.ExecuteReader();
            }
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        private static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteScalar(connectionString, commandType, commandText, null);
        }

        private static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(connection, null, commandType, commandText, commandParameters);
        }

        private static object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        private static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            return command.ExecuteScalar();
        }

        private static XmlReader ExecuteXmlReader(string connectionString, CommandType commandType, string commandText)
        {
            return ExecuteXmlReader(connectionString, commandType, commandText, null);
        }

        private static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteXmlReader(connection, null, commandType, commandText, commandParameters);
        }

        private static XmlReader ExecuteXmlReader(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            XmlReader reader;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                reader = ExecuteXmlReader(connection, null, commandType, commandText, commandParameters);
            }
            catch
            {
                connection.Close();
                throw;
            }
            return reader;
        }

        private static XmlReader ExecuteXmlReader(SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            SqlCommand command = new SqlCommand();
            PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
            return command.ExecuteXmlReader();
        }

        /// <summary>
        /// ִ�д洢�����Ի�ȡDataSet
        /// </summary>
        /// <param name="spName">�洢������</param>
        /// <param name="parameterValues">�����б�</param>
        /// <returns>����DATASET</returns>
        public static DataSet GetDataSetByProc(string spName, params object[] parameterValues)
        {
            return GetDataSetByProc(ConnectionString, spName, parameterValues);
        }

        private static DataSet GetDataSetByProc(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = DataAccessParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues, false);
                return ExecuteDataSet(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// ִ��SQL�����Ի�ȡDataSet
        /// </summary>
        /// <param name="commandText">SQL����</param>
        /// <param name="commandParameters">�����б�</param>
        /// <returns>����DATASET</returns>
        public static DataSet GetDataSetBySql(string commandText, params SqlParameter[] commandParameters)
        {
            return GetDataSetBySql(ConnectionString, commandText, commandParameters);
        }

        /// <summary>
        /// ִ��SQL�����Ի�ȡDataSet
        /// </summary>
        /// <param name="connectionString">�����ַ���</param>
        /// <param name="commandText">�����ı�</param>
        /// <param name="commandParameters">�����б�</param>
        /// <returns>����DATASET</returns>
        public static DataSet GetDataSetBySql(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// ִ�д洢�����Ի�ȡReader
        /// </summary>
        /// <param name="spName">�洢������</param>
        /// <param name="parameterValues">�����б�</param>
        /// <returns>����Reader</returns>
        public static SqlDataReader GetReaderByProc(string spName, params object[] parameterValues)
        {
            return GetReaderByProc(ConnectionString, spName, parameterValues);
        }

        private static SqlDataReader GetReaderByProc(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = DataAccessParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues, false);
                return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// ִ��SQL�����Ի�ȡReader
        /// </summary>
        /// <param name="commandText">SQL����</param>
        /// <param name="commandParameters">�����б�</param>
        /// <returns>����Reader</returns>
        public static SqlDataReader GetReaderBySql(string commandText, params SqlParameter[] commandParameters)
        {
            return GetReaderBySql(ConnectionString, commandText, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SqlDataReader GetReaderBySql(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteReader(connectionString, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static object GetScalarByProc(string spName, params object[] parameterValues)
        {
            return GetScalarByProc(ConnectionString, spName, parameterValues);
        }

        private static object GetScalarByProc(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = DataAccessParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues, false);
                return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object GetScalarBySql(string commandText, params SqlParameter[] commandParameters)
        {
            return GetScalarBySql(ConnectionString, commandText, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object GetScalarBySql(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(connectionString, CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="spName"></param>
        /// <returns></returns>
        public static XmlReader GetXmlReaderByProc(string connectionString, string spName)
        {
            return ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="spName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static XmlReader GetXmlReaderByProc(string connectionString, string spName, params object[] parameterValues)
        {
            if ((parameterValues != null) && (parameterValues.Length > 0))
            {
                SqlParameter[] spParameterSet = DataAccessParameterCache.GetSpParameterSet(connectionString, spName);
                AssignParameterValues(spParameterSet, parameterValues, false);
                return ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName, spParameterSet);
            }
            return ExecuteXmlReader(connectionString, CommandType.StoredProcedure, spName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static XmlReader GetXmlReaderBySql(string connectionString, string commandText)
        {
            return ExecuteXmlReader(connectionString, CommandType.Text, commandText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static XmlReader GetXmlReaderBySql(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteXmlReader(connectionString, CommandType.Text, commandText, commandParameters);
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            command.Connection = connection;
            command.CommandText = commandText;
            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        private enum SqlConnectionOwnership
        {
            Internal,
            External
        }
    }
}


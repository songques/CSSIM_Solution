namespace IMLibrary.SqlData
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// 快速缓冲区参数类
    /// </summary>
    public sealed class DataAccessParameterCache
    {
        private static Hashtable paramCache = Hashtable.Synchronized(new Hashtable());
        private static Hashtable paramDirections = Hashtable.Synchronized(new Hashtable());
        private static Hashtable paramTypes = Hashtable.Synchronized(new Hashtable());

        static DataAccessParameterCache()
        {
            paramTypes.Add("bigint", SqlDbType.BigInt);
            paramTypes.Add("binary", SqlDbType.Binary);
            paramTypes.Add("bit", SqlDbType.Bit);
            paramTypes.Add("char", SqlDbType.Char);
            paramTypes.Add("datetime", SqlDbType.DateTime);
            paramTypes.Add("decimal", SqlDbType.Decimal);
            paramTypes.Add("float", SqlDbType.Float);
            paramTypes.Add("image", SqlDbType.Image);
            paramTypes.Add("int", SqlDbType.Int);
            paramTypes.Add("money", SqlDbType.Money);
            paramTypes.Add("nchar", SqlDbType.NChar);
            paramTypes.Add("ntext", SqlDbType.NText);
            paramTypes.Add("numeric", SqlDbType.Decimal);
            paramTypes.Add("nvarchar", SqlDbType.NVarChar);
            paramTypes.Add("real", SqlDbType.Real);
            paramTypes.Add("smalldatetime", SqlDbType.SmallDateTime);
            paramTypes.Add("smallint", SqlDbType.SmallInt);
            paramTypes.Add("smallmoney", SqlDbType.SmallMoney);
            paramTypes.Add("sql_variant", SqlDbType.Variant);
            paramTypes.Add("text", SqlDbType.Text);
            paramTypes.Add("timestamp", SqlDbType.Timestamp);
            paramTypes.Add("tinyint", SqlDbType.TinyInt);
            paramTypes.Add("uniqueidentifier", SqlDbType.UniqueIdentifier);
            paramTypes.Add("varbinary", SqlDbType.VarBinary);
            paramTypes.Add("varchar", SqlDbType.VarChar);
            paramDirections.Add((short) 1, ParameterDirection.Input);
            paramDirections.Add((short) 2, ParameterDirection.InputOutput);
            paramDirections.Add((short) 4, ParameterDirection.ReturnValue);
        }

        private DataAccessParameterCache()
        {
        }

        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="connectionString">联接字符串</param>
        /// <param name="commandText">命令文件</param>
        /// <param name="commandParameters">命令参数</param>
        public static void CacheParameterSet(string connectionString, string commandText, params SqlParameter[] commandParameters)
        {
            string text = connectionString + ":" + commandText;
            paramCache[text] = commandParameters;
        }

        private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
        {
            SqlParameter[] parameterArray = new SqlParameter[originalParameters.Length];
            int index = 0;
            int length = originalParameters.Length;
            while (index < length)
            {
                parameterArray[index] = (SqlParameter) ((ICloneable) originalParameters[index]).Clone();
                index++;
            }
            return parameterArray;
        }

        private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            SqlParameter[] parameterArray;
            int num;
            DataTable dataTable = new DataTable("paramDescriptions");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("sp_procedure_params_rowset", connection);
                selectCommand.CommandType = CommandType.StoredProcedure;
                selectCommand.Parameters.AddWithValue("@procedure_name", spName);
                new SqlDataAdapter(selectCommand).Fill(dataTable);
            }
            if (dataTable.Rows.Count <= 0)
            {
                throw new ArgumentException("Stored procedure '" + spName + "' not found", "spName");
            }
            if (includeReturnValueParameter)
            {
                parameterArray = new SqlParameter[dataTable.Rows.Count];
                num = 0;
            }
            else
            {
                parameterArray = new SqlParameter[dataTable.Rows.Count - 1];
                num = 1;
            }
            int index = 0;
            int length = parameterArray.Length;
            while (index < length)
            {
                DataRow row = dataTable.Rows[index + num];
                parameterArray[index] = new SqlParameter();
                parameterArray[index].ParameterName = (string) row["PARAMETER_NAME"];
                parameterArray[index].SqlDbType = (SqlDbType) paramTypes[(string) row["TYPE_NAME"]];
                parameterArray[index].Direction = (ParameterDirection) paramDirections[(short) row["PARAMETER_TYPE"]];
                parameterArray[index].Size = (row["CHARACTER_OCTET_LENGTH"] == DBNull.Value) ? 0 : ((int) row["CHARACTER_OCTET_LENGTH"]);
                parameterArray[index].Precision = (row["NUMERIC_PRECISION"] == DBNull.Value) ? ((byte) 0) : ((byte) ((short) row["NUMERIC_PRECISION"]));
                parameterArray[index].Scale = (row["NUMERIC_SCALE"] == DBNull.Value) ? ((byte) 0) : ((byte) ((short) row["NUMERIC_SCALE"]));
                index++;
            }
            return parameterArray;
        }

        /// <summary>
        /// 获取参数设置 
        /// </summary>
        /// <param name="connectionString">联接字符串</param>
        /// <param name="commandText">命令文本</param>
        /// <returns></returns>
        public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
        {
            string text = connectionString + ":" + commandText;
            SqlParameter[] originalParameters = (SqlParameter[]) paramCache[text];
            if (originalParameters == null)
            {
                return null;
            }
            return CloneParameters(originalParameters);
        }

        /// <summary>
        /// 获取存储过程参数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <returns></returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
        {
            return GetSpParameterSet(connectionString, spName, false);
        }

        /// <summary>
        /// 获取存储过程参数
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="spName">存储过程名称</param>
        /// <param name="includeReturnValueParameter">是否包括返回值参数</param>
        /// <returns></returns>
        public static SqlParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter)
        {
            string text = connectionString + ":" + spName + (includeReturnValueParameter ? ":include ReturnValue Parameter" : "");
            SqlParameter[] originalParameters = (SqlParameter[]) paramCache[text];
            if (originalParameters == null)
            {
                object obj2;
                paramCache[text] = obj2 = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter);
                originalParameters = (SqlParameter[]) obj2;
            }
            return CloneParameters(originalParameters);
        }
    }
}


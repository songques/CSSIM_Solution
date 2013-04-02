using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CSS.IM.Library.Controls.UdpSendFile
{

    public class BufferHelper
    {
        public static byte[] Serialize(object obj)
        {
            byte[] datas;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            try
            {
                bf.Serialize(stream, obj);
                datas = stream.ToArray();

                stream.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            return datas;
        }

        public static object Deserialize(byte[] datas, int index)
        {

            BinaryFormatter bf = new BinaryFormatter();
            object obj = null;
            MemoryStream stream=null;
            try
            {
                stream = new MemoryStream(datas, index, datas.Length - index);
                obj = bf.Deserialize(stream);
                return obj;
               
            }
            catch (Exception)
            {
                return obj;
            }
            finally
            { 
                stream.Dispose();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace CSS.IM.UI.Entity
{
    [Serializable]
    public class UserInfo
    {
        public int qq;
        public string passWD;
        public string Key;
        public string nikeName;
        public string description;
        public string headImg;
        public int state;
        public IPAddress ip;
        public int port;
        public int sex;
        public string BirsDay;
        public string contray;

        public UserInfo()
        {
        }

        public UserInfo(object[] ss)
        {
            setValues(ss);
        }

        public UserInfo(object[] ss,string key)
        {
            setValues(ss);
            this.Key = key;
        }

        public void update(UserInfo ss)
        {
            qq = ss.qq;
            Key = ss.Key;
            nikeName = ss.nikeName;
            description = ss.description;
            headImg = ss.headImg;
            sex = ss.sex;
            BirsDay = ss.BirsDay;
            contray = ss.contray;
        }

        public void setValues(object[] ss)
        {
            qq = int.Parse(ss[0].ToString());
            nikeName = ss[1].ToString();
            description = ss[2].ToString();
            headImg = ss[3].ToString();
            sex = int.Parse(ss[4].ToString().Equals("") ? "0" : ss[4].ToString());
            BirsDay = ss[5].ToString();
            contray = ss[6].ToString();
            ip = IPAddress.Parse(ss[7].ToString().Equals("") ? "0.0.0.0" : ss[7].ToString());
            port = int.Parse(ss[8].ToString().Equals("") ? "0" : ss[8].ToString());
            state = int.Parse(ss[9].ToString().Equals("") ? "0" : ss[9].ToString());
        }
    }
}

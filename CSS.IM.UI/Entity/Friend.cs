using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.UI.Entity
{
    public class Friend
    {

        public CSS.IM.XMPP.protocol.iq.roster.RosterItem Ritem { set; get; }
        public string GroupName { set; get; }
        public int Uin { get; set; }
        public string NikeName { set; get; }
        public string Description { set; get; }
        public string HeadIMG { set; get; }
        public bool IsSysHead { get; set; }
        public bool IsOnline { get; set; }
        public int State { get; set; }
        public int GroupID { get; set; }
        public bool isTreeSearch { get; set; }


    }
}

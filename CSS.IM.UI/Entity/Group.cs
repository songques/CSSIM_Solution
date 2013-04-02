using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CSS.IM.UI.Entity
{
    public class Group
    {

        public string Title { get; set; }
        public int Id { get; set; }
        public int OnlineCount { get; set; }
        public int Count { get; set; }
        public int DisplayOrder { get; set; }

        private ArrayList m_List = new ArrayList();
        public ArrayList List
        {
            get
            {
                return m_List;
            }
            set
            {
                m_List = value;
            }
        }

        


    }
}

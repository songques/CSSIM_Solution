using System;
using CSS.IM.XMPP.Xml.Dom;


namespace CSS.IM.App.Settings
{
    /*   
    This class shows how agsXMPP could also be used read and write custom xml files.
    Here we use it for the application settings which are stored in xml files    
    */
    public class Settings : Element
    {
        public Settings()
        {
            this.TagName = "configs";
        }        
        
        //public Login Login
        //{
        //    get { return (Login)SelectSingleElement(typeof(Login)); }
        //    set
        //    {
        //        RemoveTag(typeof(Login));
        //        if (value != null)
        //            AddChild(value);
        //    }
        //}

        public Paths Paths
        {
            get { return (Paths)SelectSingleElement(typeof(Paths)); }
            set
            {
                RemoveTag(typeof(Paths));
                if (value != null)
                    AddChild(value);
            }
        }


        public SFont Font
        {
            get { return (SFont)SelectSingleElement(typeof(SFont)); }
            set
            {
                RemoveTag(typeof(SFont));
                if (value != null)
                    AddChild(value);
            }
        }

        public SColor Color
        {
            get { return (SColor)SelectSingleElement(typeof(SColor)); }
            set
            {
                RemoveTag(typeof(SColor));
                if (value != null)
                    AddChild(value);
            }
        }
    }
}
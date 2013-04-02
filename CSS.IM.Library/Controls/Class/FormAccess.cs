using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library 
{
    sealed  class FormAccess
    {
        private  FormAccess()
        {
        }
        public static System.Resources.ResourceManager rm = new System.Resources.ResourceManager("CSS.IM.Library.Properties.Resource", System.Reflection.Assembly.GetExecutingAssembly());
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary 
{
    sealed  class FormAccess
    {
        private  FormAccess()
        {
        }
        public static System.Resources.ResourceManager rm = new System.Resources.ResourceManager("IMLibrary.Properties.Resource", System.Reflection.Assembly.GetExecutingAssembly());
    }
}

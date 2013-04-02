/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright (c) 2003-2008 by AG-Software 											 *
 * All Rights Reserved.																 *
 * Contact information for AG-Software is available at http://www.ag-software.de	 *
 *																					 *
 * Licence:																			 *
 * The CSS.IM.XMPP SDK is released under a dual licence									 *
 * CSS.IM.XMPP can be used under either of two licences									 *
 * 																					 *
 * A commercial licence which is probably the most appropriate for commercial 		 *
 * corporate use and closed source projects. 										 *
 *																					 *
 * The GNU Public License (GPL) is probably most appropriate for inclusion in		 *
 * other open source projects.														 *
 *																					 *
 * See README.html for details.														 *
 *																					 *
 * For general enquiries visit our website at:										 *
 * http://www.ag-software.de														 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */ 

using System;

using CSS.IM.XMPP.protocol.x.data;

using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.XMPP.protocol.extensions.pubsub
{
    public class Configure : PubSubAction
    {
        #region << Constructors >>
        public Configure() : base()
        {
            this.TagName = "configure";
        }

        public Configure(string node) : this()
        {
            this.Node = node;
        }

        public Configure(Type type) : this()
        {
            this.Type = type;            
        }

        public Configure(string node, Type type) : this(node)
        {            
            this.Type = type;
        }
        #endregion

        public Access Access
		{
			get 
			{
                return (Access)GetAttributeEnum("access", typeof(Access)); 
			}
			set 
			{
                if (value == Access.NONE)
                    RemoveAttribute("access");
                else
                    SetAttribute("access", value.ToString()); 
			}
		}

        /// <summary>
        /// The x-Data Element
        /// </summary>
        public Data Data
        {
            get
            {
                return SelectSingleElement(typeof(Data)) as Data;

            }
            set
            {
                if (HasTag(typeof(Data)))
                    RemoveTag(typeof(Data));

                if (value != null)
                    this.AddChild(value);
            }
        }
    }
}

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

using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.XMPP.protocol.extensions.pubsub
{
    /*
        <affiliation node='node1' jid='francisco@denmark.lit' affiliation='owner'/>
    */
    public class Affiliation : Element
    {
        #region << Constructors >>
        public Affiliation()
        {
            this.TagName = "affiliation";
            this.Namespace = Uri.PUBSUB;
        }

        public Affiliation(Jid jid, AffiliationType affiliation)
        {
            this.Jid                = jid;
            this.AffiliationType    = affiliation;
        }

        public Affiliation(string node, Jid jid, AffiliationType affiliation) : this(jid, affiliation)
        {
            this.Node = node;
        }
        #endregion

        public Jid Jid
		{
			get 
			{
                if (HasAttribute("jid"))
                    return new Jid(this.GetAttribute("jid"));
				else
					return null;
			}
			set 
			{ 
				if (value!=null)
                    this.SetAttribute("jid", value.ToString());
			}
		}
        
        public string Node
		{
            get { return GetAttribute("node"); }
			set	{ SetAttribute("node", value); }			
		}

        public AffiliationType AffiliationType
		{
			get 
			{
                return (AffiliationType)GetAttributeEnum("affiliation", typeof(AffiliationType)); 
			}
			set 
			{
                SetAttribute("affiliation", value.ToString()); 
			}
		}
    }
}

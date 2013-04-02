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

#region Using directives

using System;

#endregion

namespace CSS.IM.XMPP.protocol.component
{
    /// <summary>
    /// Summary description for Iq.
    /// </summary>
    public class IQ : CSS.IM.XMPP.protocol.client.IQ
    {
        #region << Constructors >>
        public IQ() : base()
        {
            this.Namespace = Uri.ACCEPT;
        }

        public IQ(CSS.IM.XMPP.protocol.client.IqType type) : base(type)
        {
            this.Namespace = Uri.ACCEPT;
        }

        public IQ(Jid from, Jid to) : base(from, to)
        {
            this.Namespace = Uri.ACCEPT;
        }

        public IQ(CSS.IM.XMPP.protocol.client.IqType type, Jid from, Jid to) : base(type, from, to)
        {
            this.Namespace = Uri.ACCEPT;
        }
        #endregion

        /// <summary>
        /// Error Child Element
        /// </summary>
        public new CSS.IM.XMPP.protocol.component.Error Error
        {
            get
            {
                return SelectSingleElement(typeof(CSS.IM.XMPP.protocol.component.Error)) as CSS.IM.XMPP.protocol.component.Error;

            }
            set
            {
                if (HasTag(typeof(CSS.IM.XMPP.protocol.component.Error)))
                    RemoveTag(typeof(CSS.IM.XMPP.protocol.component.Error));

                if (value != null)
                    this.AddChild(value);
            }
        }
    }
}

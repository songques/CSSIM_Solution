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

using CSS.IM.XMPP.protocol.client;


namespace CSS.IM.XMPP.protocol.iq.rpc
{
    /// <summary>
    /// RpcIq.
    /// </summary>
    public class RpcIq : IQ
    {
        private Rpc m_Rpc = new Rpc();

        public RpcIq()
        {
            base.Query = m_Rpc;
            this.GenerateId();
        }

        public RpcIq(IqType type) : this()
        {
            this.Type = type;
        }

        public RpcIq(IqType type, Jid to) : this(type)
        {
            this.To = to;
        }

        public RpcIq(IqType type, Jid to, Jid from) : this(type, to)
        {
            this.From = from;
        }

        public new Rpc Query
        {
            get
            {
                return m_Rpc;
            }
        }

    }
}

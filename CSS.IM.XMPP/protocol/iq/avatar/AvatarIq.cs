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

namespace CSS.IM.XMPP.protocol.iq.avatar
{
	/// <summary>
	/// Summary description for AvatarIq.
	/// </summary>
	public class AvatarIq : IQ
	{
		private Avatar m_Avatar = new Avatar();

		public AvatarIq()
		{			
			base.Query = m_Avatar;
			this.GenerateId();	
		}

		public AvatarIq(IqType type) : this()
		{			
			this.Type = type;		
		}

		public AvatarIq(IqType type, Jid to) : this(type)
		{
			this.To = to;
		}

		public AvatarIq(IqType type, Jid to, Jid from) : this(type, to)
		{
			this.From = from;
		}

		public new Avatar Query
		{
			get
			{
				return m_Avatar;
			}
		}

		
	}
}

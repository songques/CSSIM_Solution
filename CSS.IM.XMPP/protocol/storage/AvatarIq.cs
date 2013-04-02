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

namespace CSS.IM.XMPP.protocol.storage
{

	//	Once such data has been set, the avatar can be retrieved by any requesting client from the avatar-generating client's public XML storage:
	//
	//	Example 8.
	//
	//	<iq id='1' type='get' to='user@server'>
	//		<query xmlns='storage:client:avatar'/>
	//	</iq>  

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

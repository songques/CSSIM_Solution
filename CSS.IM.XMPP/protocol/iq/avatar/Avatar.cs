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

namespace CSS.IM.XMPP.protocol.iq.avatar
{
	//	<iq id='2' type='result' to='user@server/resource'>
	//		<query xmlns='jabber:iq:avatar'>
	//			<data mimetype='image/jpeg'>
	//			Base64-Encoded Data
	//			</data>
	//		</query>
	//	</iq>

	/// <summary>
	/// Summary description for Avatar.
	/// </summary>
	public class Avatar : CSS.IM.XMPP.protocol.Base.Avatar
	{
		public Avatar() : base()
		{
			this.Namespace	= Uri.IQ_AVATAR;			
		}	
	}
}

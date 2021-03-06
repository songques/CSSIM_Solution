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

namespace CSS.IM.XMPP.protocol.iq.vcard
{

	/// <summary>
	/// 
	/// </summary>
	public class Organization : Element
	{
		// <ORG>
		//	<ORGNAME>Jabber Software Foundation</ORGNAME>
		//	<ORGUNIT/>
		// </ORG>

		#region << Constructors >>
		public Organization()
		{
			this.TagName	= "ORG";
			this.Namespace	= Uri.VCARD;
		}
		
		public Organization(string name, string unit) : this()
		{			
			this.Name	= name;		
			this.Unit	= unit;
		}
		#endregion

		public string Name
		{
			get { return GetTag("ORGNAME"); }
			set { SetTag("ORGNAME", value); }
		}

		public string Unit
		{
			get { return GetTag("ORGUNIT"); }
			set { SetTag("ORGUNIT", value); }
		}
	}
}

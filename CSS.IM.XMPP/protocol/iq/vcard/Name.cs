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
	public class Name : Element
	{
		// <N>
		//	<FAMILY>Saint-Andre<FAMILY>
		//	<GIVEN>Peter</GIVEN>
		//	<MIDDLE/>
		// </N>
		#region << Constructors >>
		public Name()
		{
			this.TagName	= "N";
			this.Namespace	= Uri.VCARD;
		}
		public Name(string family, string given, string middle) : this()
		{
			this.Family	= family;
			this.Given	= given;
			this.Middle	= middle;
		}
		#endregion
		
		public string Family
		{
			get { return GetTag("FAMILY"); }
			set { SetTag("FAMILY", value); }
		}

		public string Given
		{
			get { return GetTag("GIVEN"); }
			set { SetTag("GIVEN", value); }

		}
		public string Middle
		{
			get { return GetTag("MIDDLE"); }
			set { SetTag("MIDDLE", value); }
		}
	}
}

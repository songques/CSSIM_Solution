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

using CSS.IM.XMPP.protocol.Base;

using CSS.IM.XMPP.Xml;
using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.XMPP.protocol.component
{
	public enum RouteType
	{
		NONE = -1,
		error,
		auth,
		session
	}

	/// <summary>
	/// 
	/// </summary>
	public class Route : Stanza
	{
		public Route()
		{
			this.TagName	= "route";
			this.Namespace	= Uri.ACCEPT;	
		}

		public Route(Element route) : this()
		{
			RouteElement = route;
		}

		public Route(Element route, Jid from, Jid to) : this()
		{
			RouteElement	= route;
			From			= from;
			To				= to;
		}

		public Route(Element route, Jid from, Jid to, RouteType type) : this()
		{
			RouteElement	= route;
			From			= from;
			To				= to;
			Type			= type;
		}

		/// <summary>
		/// Gets or Sets the logtype
		/// </summary>
		public RouteType Type
		{
			get 
			{ 
				return (RouteType) GetAttributeEnum("type", typeof(RouteType));
			}
			set 
			{ 
				if (value == RouteType.NONE)
					RemoveAttribute("type");
				else
					SetAttribute("type", value.ToString()); 
			}
		}

		/// <summary>
		/// sets or gets the element to route
		/// </summary>
		public Element RouteElement
		{
			get { return this.FirstChild as Element; }
			set 
			{
				if (this.HasChildElements)
					this.RemoveAllChildNodes();
                
                if (value != null)
				    this.AddChild(value);					
			}
		}
	}
}
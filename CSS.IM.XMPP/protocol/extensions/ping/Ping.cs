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

namespace CSS.IM.XMPP.protocol.extensions.ping
{
    /*
     * <iq from='capulet.com' to='juliet@capulet.com/balcony' id='ping123' type='get'>
     *   <ping xmlns='urn:xmpp:ping'/>
     * </iq>
     */
    public class Ping : Element
    {
        public Ping()
        {
            this.TagName    = "ping";
            this.Namespace  = Uri.PING;
        }
    }
}

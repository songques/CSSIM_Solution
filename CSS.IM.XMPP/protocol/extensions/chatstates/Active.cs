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
using System.Text;

using CSS.IM.XMPP.Xml.Dom;

namespace CSS.IM.XMPP.protocol.extensions.chatstates
{
    /// <summary>
    /// User is actively participating in the chat session.
    /// User accepts an initial content message, sends a content message, 
    /// gives focus to the chat interface, or is otherwise paying attention to the conversation.
    /// </summary>
    public class Active : Element
    {
        /// <summary>
        /// 
        /// </summary>
        public Active()
        {
            this.TagName    = Chatstate.active.ToString();
            this.Namespace  = Uri.CHATSTATES;
        }
    }
}

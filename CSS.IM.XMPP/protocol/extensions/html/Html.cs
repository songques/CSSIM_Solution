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

namespace CSS.IM.XMPP.protocol.extensions.html
{
    /*
     * <message>
     *      <body>hi!</body>
     *      <html xmlns='http://jabber.org/protocol/xhtml-im'>
     *          <body xmlns='http://www.w3.org/1999/xhtml'>
     *              <p style='font-weight:bold'>hi!</p>
     *          </body>
     *      </html>
     * </message>
     */

    public class Html : Element
    {
        public Html()
        {
            this.TagName    = "html";
            this.Namespace  = Uri.XHTML_IM;
        }
        
        /// <summary>
        /// The Body Element of the XHTML Message
        /// </summary>
        public Body Body
        {
            get
            {
                return SelectSingleElement(typeof(Body)) as Body;

            }
            set
            {
                if (HasTag(typeof(Body)))
                    RemoveTag(typeof(Body));
                
                if (value != null)
                    this.AddChild(value);
            }
        }
    }
}

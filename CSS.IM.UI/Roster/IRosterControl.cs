using System;
using System.Collections.Generic;
using System.Text;
using CSS.IM.UI.Entity;
using CSS.IM.XMPP;
using CSS.IM.XMPP.protocol.client;

namespace CSS.IM.UI.Roster
{
    interface IRosterControl
    {
        Friend AddRosterItem(Friend ritem);

        bool RemoveRosterItem(Jid jid);
        bool RemoveRosterItem(Friend ritem);

        void SetPresence(Presence pres);

        void Clear();
    }
}

using System;
using System.Collections.Generic;

namespace ApiTFG
{
    public partial class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int UserEmitter { get; set; }
        public int UserReceiver { get; set; }
        public int Chat { get; set; }

        public virtual Chat ChatNavigation { get; set; }
        public virtual Users UserEmitterNavigation { get; set; }
        public virtual Users UserReceiverNavigation { get; set; }
    }
}

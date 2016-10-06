using System;
using System.Collections.Generic;

namespace ApiTFG
{
    public partial class Chat
    {
        public Chat()
        {
            Message = new HashSet<Message>();
        }

        public int Id { get; set; }
        public DateTime InitDatetime { get; set; }
        public DateTime EndDatetime { get; set; }
        public int UserRequest { get; set; }
        public int UserRequested { get; set; }

        public virtual ICollection<Message> Message { get; set; }
        public virtual Users UserRequestNavigation { get; set; }
        public virtual Users UserRequestedNavigation { get; set; }
    }
}

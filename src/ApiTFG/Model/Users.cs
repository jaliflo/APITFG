using System;
using System.Collections.Generic;

namespace ApiTFG
{
    public partial class Users
    {
        public Users()
        {
            ChatUserRequestNavigation = new HashSet<Chat>();
            ChatUserRequestedNavigation = new HashSet<Chat>();
            MessageUserEmitterNavigation = new HashSet<Message>();
            MessageUserReceiverNavigation = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string CityAndCountry { get; set; }
        public string Job { get; set; }
        public string Hobbies { get; set; }
        public string MusicTastes { get; set; }
        public string FilmsTastes { get; set; }
        public string ReadingTastes { get; set; }
        public string BluetoothMac { get; set; }
        public int Age { get; set; }

        public virtual ICollection<Chat> ChatUserRequestNavigation { get; set; }
        public virtual ICollection<Chat> ChatUserRequestedNavigation { get; set; }
        public virtual ICollection<Message> MessageUserEmitterNavigation { get; set; }
        public virtual ICollection<Message> MessageUserReceiverNavigation { get; set; }
    }
}

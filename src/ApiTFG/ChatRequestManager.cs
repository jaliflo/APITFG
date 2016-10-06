using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace ApiTFG
{
    public class ChatRequestManager
    {
        private itsMeServerDBContext dbContext;

        public Dictionary<Users, WebSocket> userSocket { get; set; }

        public ChatRequestManager()
        {
            dbContext = new itsMeServerDBContext();
            userSocket = new Dictionary<Users, WebSocket>();
        }

        public Users FindUserById(int id)
        {
            Users user = null;
            List<Users> users = dbContext.Users.ToList();

            foreach(Users useri in users)
            {
                if(id == useri.Id)
                {
                    user = useri;
                    break;
                }
            }

            return user;
        }

        public Users FindUserByName(string name)
        {
            Users user = null;
            List<Users> users = dbContext.Users.ToList();

            foreach (Users useri in users)
            {
                if (name == useri.Name)
                {
                    user = useri;
                    break;
                }
            }

            return user;
        }
    }
}

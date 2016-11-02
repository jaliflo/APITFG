using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTFG.Model;
using System.Diagnostics;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTFG.Controllers
{
    [Route("api/chatsbackup")]
    public class ChatsBackupController : Controller
    {
        private itsMeServerDBContext dbContext;

        public ChatsBackupController()
        {
            dbContext = new itsMeServerDBContext();
        }
        
        [HttpPost("insertChats/{id}")]
        public IActionResult insertChats(int id, [FromBody] Chats chats)
        {
            List<Chat> chatsFromDB = new List<Chat>();

            foreach (ChatFromClient chat in chats.chats)
            {
                Chat chatToAdd = new Chat();
                Debug.WriteLine(chat.InitDatetime);
                chatToAdd.InitDatetime = chat.InitDatetime;
                chatToAdd.EndDatetime = chat.EndDatetime;
                chatToAdd.UserRequest = id;
                chatToAdd.UserRequested = findUser(chat.user).Id;
                dbContext.Chat.Add(chatToAdd);
                chatsFromDB.Add(chatToAdd);
            }

            dbContext.SaveChanges();

            for(int i = 0; i < chatsFromDB.Count; i++)
            {
                chats.chats.ElementAt(i).Id = chatsFromDB.ElementAt(i).Id;
            }

            return new ObjectResult(chats);
        }

        [HttpPost("insertMessages/{id}")]
        public IActionResult insertMessages(int id, [FromBody] Messages messages)
        {
            foreach (Message message in messages.messages)
            {
                message.Chat = id;
                dbContext.Message.Add(message);
            }

            dbContext.SaveChanges();

            return new ObjectResult(messages);
        }

        private Users findUser(Users userFromClient)
        {
            List<Users> users = dbContext.Users.ToList();
            Users user = null;

            foreach(Users useri in users)
            {
                if(useri.Name == userFromClient.Name)
                {
                    user = useri;
                }
            }

            return user;
        }
    }

    public class ChatFromClient
    {
        public int Id { get; set; }
        public DateTime InitDatetime { get; set; }
        public DateTime EndDatetime { get; set; }
        public Users user { get; set; }

        public ChatFromClient() { }
    }

    public class Chats
    {
        public List<ChatFromClient> chats;

        public Chats()
        {
            chats = new List<ChatFromClient>();
        }
    }

    public class Messages
    {
        public List<Message> messages;

        public Messages()
        {
            messages = new List<Message>();
        }
    }
}

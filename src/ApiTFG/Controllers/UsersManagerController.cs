using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTFG.Controllers
{
    [Route("api/usersmanager")]
    public class UsersManagerController : Controller
    {

        private itsMeServerDBContext dbContext;

        public UsersManagerController()
        {
            dbContext = new itsMeServerDBContext();
        }

        // GET: api/values
        public IEnumerable<Users> GetAll()
        {
            return dbContext.Users;
        }

        // POST: login
        [HttpPost]
        public IActionResult Login([FromBody] Users credentials)
        {
            Debug.WriteLine(credentials.Name);

            List<Users> users = dbContext.Users.ToList();
            Users user = null;
            
            foreach(Users useri in users)
            {
                if(useri.Name == credentials.Name)
                {
                    user = useri;
                }
            }

            if(user == null)
            {
                return NotFound();
            }
            else if(user.Password != credentials.Password)
            {
                return NotFound();
            }
            

            return new ObjectResult(user);
        }

        //POST: Create new user
        [HttpPost("PostCreateUser")]
        public IActionResult CreateUser([FromBody] Users user)
        {
            if(user == null || user.Name == null || user.Password == null)
            {
                return NoContent();
            }

            List<Users> users = dbContext.Users.ToList();
            foreach(Users useri in users)
            {
                if(useri.Name == user.Name)
                {
                    return BadRequest();
                }
            }

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return new ObjectResult(user);
        }

        //PUT: Update user
        [HttpPatch]
        public IActionResult UpdateUser([FromBody] Users user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            List<Users> users = dbContext.Users.ToList<Users>();
            Users userToUpdate = null;
            
            foreach(Users useri in users)
            {
                if(user.Id == useri.Id)
                {
                    userToUpdate = useri;
                }
            }

            if(userToUpdate == null)
            {
                return BadRequest();
            }

            userToUpdate.CityAndCountry = user.CityAndCountry;
            userToUpdate.Job = user.Job;
            userToUpdate.Hobbies = user.Hobbies;
            userToUpdate.MusicTastes = user.MusicTastes;
            userToUpdate.ReadingTastes = user.ReadingTastes;
            userToUpdate.FilmsTastes = user.FilmsTastes;

            dbContext.SaveChanges();

            return new NoContentResult();
        }
    }
}

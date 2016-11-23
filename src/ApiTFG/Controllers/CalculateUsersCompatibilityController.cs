using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTFG.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTFG.Controllers
{
    [Route("api/calculateuserscompatibility")]
    public class CalculateUsersCompatibilityController : Controller
    {
        private itsMeServerDBContext dbContext;
        public CalculateUsersCompatibilityController()
        {
            dbContext = new itsMeServerDBContext();
        }

        //POST: Get nearby users order by compatibility
        [HttpPost("{id}")]
        public IActionResult GetListOfNearbyUsers(int id, [FromBody] MacsList macsList)
        {
            NearbyUsers usersList = new NearbyUsers();
            List<Users> users = dbContext.Users.ToList();
            Users myUser = null;

            foreach(Users useri in users)
            {
                foreach(string mac in macsList.macList)
                {
                    if (useri.BluetoothMac == mac)
                    {
                        usersList.nearbyUsers.Add(useri, 0);
                    }
                }
            }

            foreach(Users useri in users)
            {
                if(id == useri.Id)
                {
                    myUser = useri;
                }
            }

            if(myUser == null)
            {
                return BadRequest();
            }

            for(int i = 0; i < usersList.nearbyUsers.Keys.ToArray().Length; i++)
            {
                usersList.CalculateCompatibility(i, myUser);
            }

            var orderedUsers = usersList.nearbyUsers.ToList();
            orderedUsers.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

            List<string> result = new List<string>();

            for(int i=orderedUsers.Count; i > 0; i--)
            {
                result.Add(orderedUsers.ElementAt(i-1).Key.Name + " " + orderedUsers.ElementAt(i-1).Value+","+orderedUsers.ElementAt(i-1).Key.BluetoothMac);
            }

            return new ObjectResult(result);
        }
  
    }

    public class NearbyUsers
    {
        public const int AGE_RANGE = 5;

        public Dictionary<Users, float> nearbyUsers;

        public NearbyUsers()
        {
            nearbyUsers = new Dictionary<Users, float>();
        }

        public void CalculateCompatibility(int pos, Users user)
        {
            Users calculateUser = nearbyUsers.ElementAt(pos).Key;
            float compatibility = nearbyUsers.ElementAt(pos).Value;

            if(Math.Abs(calculateUser.Age - user.Age) <= 10)
            {
                compatibility += 25;
            }

            if (calculateUser.CityAndCountry != "" && user.CityAndCountry != "")
            {
                string[] cityandcountry1 = calculateUser.CityAndCountry.Split('-');
                string[] cityandcountry2 = user.CityAndCountry.Split('-');

                if (cityandcountry1[0] == cityandcountry2[0])
                {
                    compatibility += 15;
                }
                else if (cityandcountry1[1] == cityandcountry2[1])
                {
                    compatibility += 5;
                }
            }

            if (calculateUser.Job != "" && user.Job != "")
            {
                if (calculateUser.Job == user.Job)
                {
                    compatibility += 12;
                }
            }

            string[] separator = new string[] { ", " };
            float unityValue = 0;

            if (calculateUser.Hobbies != "" && user.Hobbies != "")
            {
                string[] hobbies1 = calculateUser.Hobbies.Split(separator, StringSplitOptions.None);
                string[] hobbies2 = user.Hobbies.Split(separator, StringSplitOptions.None);

                if (hobbies1.Length > hobbies2.Length)
                {
                    unityValue = (float) 12 / hobbies1.Length;
                }
                else
                {
                    unityValue = (float) 12 / hobbies2.Length;
                }

                for (int i = 0; i < hobbies1.Length; i++)
                {
                    for (int j = 0; j < hobbies2.Length; j++)
                    {
                        if (hobbies1[i] == hobbies2[j])
                        {
                            compatibility += unityValue;
                        }
                    }
                }
            }

            if (calculateUser.MusicTastes != "" && user.MusicTastes != "")
            {
                string[] musicaltastes1 = calculateUser.MusicTastes.Split(separator, StringSplitOptions.None);
                string[] musicaltastes2 = user.MusicTastes.Split(separator, StringSplitOptions.None);

                if (musicaltastes1.Length > musicaltastes2.Length)
                {
                    unityValue = (float) 12 / musicaltastes1.Length;
                }
                else
                {
                    unityValue = (float) 12 / musicaltastes2.Length;
                }

                for (int i = 0; i < musicaltastes1.Length; i++)
                {
                    for (int j = 0; j < musicaltastes2.Length; j++)
                    {
                        if (musicaltastes1[i] == musicaltastes2[j])
                        {
                            compatibility += unityValue;
                        }
                    }
                }
            }

            if (calculateUser.ReadingTastes != "" && user.ReadingTastes != "")
            {
                string[] readingtastes1 = calculateUser.ReadingTastes.Split(separator, StringSplitOptions.None);
                string[] readingtastes2 = user.ReadingTastes.Split(separator, StringSplitOptions.None);

                if (readingtastes1.Length > readingtastes2.Length)
                {
                    unityValue = (float) 12 / readingtastes1.Length;
                }
                else
                {
                    unityValue = (float) 12 / readingtastes2.Length;
                }

                for (int i = 0; i < readingtastes1.Length; i++)
                {
                    for (int j = 0; j < readingtastes2.Length; j++)
                    {
                        if (readingtastes1[i] == readingtastes2[j])
                        {
                            compatibility += unityValue;
                        }
                    }
                }
            }

            if (calculateUser.FilmsTastes != "" && user.FilmsTastes != "")
            {
                string[] filmtastes1 = calculateUser.FilmsTastes.Split(separator, StringSplitOptions.None);
                string[] filmtastes2 = user.FilmsTastes.Split(separator, StringSplitOptions.None);

                if (filmtastes1.Length > filmtastes2.Length)
                {
                    unityValue = (float) 12 / filmtastes1.Length;
                }
                else
                {
                    unityValue = (float) 12 / filmtastes2.Length;
                }

                for (int i = 0; i < filmtastes1.Length; i++)
                {
                    for (int j = 0; j < filmtastes2.Length; j++)
                    {
                        if (filmtastes1[i] == filmtastes2[j])
                        {
                            compatibility += unityValue;
                        }
                    }
                }
            }

            nearbyUsers[calculateUser] = float.Parse(compatibility.ToString("0"));
        }
    }
}

using System;
using System.Threading.Tasks;
using NextGenSoftware.OASIS.API.Core;
using NextGenSoftware.OASIS.API.Core.Holons;

namespace NextGenSoftware.OASIS.API.Providers.Neo4jOASIS.Aura.TestHarness
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("NEXTGEN SOFTWARE Neo4JOASIS TEST HARNESS V1.O");
            Console.WriteLine("");

            Console.WriteLine("Connecting To Aura DB...");
            //Neo4jOASIS neo = new Neo4jOASIS("http://localhost:7474", "neo4j", "letthereb@light!");
            Neo4jOASIS neo = new Neo4jOASIS("<neo4j bolt URL>", "<username>", "<password>");
            
            
            neo.ActivateProvider();
            
            Console.WriteLine("Connected To Aura DB.");
            Console.WriteLine("Creating New Avatar...");

            Avatar avatar = new Avatar() 
            { 
                Title="harshal",
                FirstName = "Harshal edited", 
                LastName = "Patel edited", 
                Email = "harshalpatel@gmail.com", 
                Username= "harshalpatel@gmail.com",
                Password ="password",
            };
            var resAvatar=await neo.SaveAvatarAsync(avatar);
            if (resAvatar != null)
            {
                if (!string.IsNullOrEmpty(resAvatar.Title))
                {
                    Console.WriteLine("Avatar ID:{0}", resAvatar.Title);
                    Console.WriteLine("Avatar Created.");
                }
                else
                {
                    Console.WriteLine("Message:{0}", resAvatar.FirstName);                    
                }
            }

            Console.WriteLine("Loading Avatar by Email...");
            var retAvatar = await neo.LoadAvatarByEmailAsync("alpeshsharma@gmail.com");
            if (retAvatar != null)
            {
                if (!string.IsNullOrEmpty(retAvatar.FirstName))
                {
                    Console.WriteLine("Avatar Loaded.");
                    Console.WriteLine("Avatar First Name:{0}", retAvatar.FirstName);
                    Console.WriteLine("Avatar Last Name:{0}", retAvatar.LastName);
                }
                else
                {
                    Console.WriteLine("No record found.");
                }
            }

            Console.WriteLine("Loading Avatar by Username and Password...");
            var uAvatar = await neo.LoadAvatarAsync("devangpatel@gmail.com","password");
            if (uAvatar != null)
            {
                if (!string.IsNullOrEmpty(uAvatar.FirstName))
                {
                    Console.WriteLine("Avatar Loaded.");
                    Console.WriteLine("Avatar First Name:{0}", uAvatar.FirstName);
                    Console.WriteLine("Avatar Last Name:{0}", uAvatar.LastName);
                }
                else
                {
                    Console.WriteLine("No record found.");
                }
            }

            Console.WriteLine("Loading Avatar by Username...");
            var userAvatar = await neo.LoadAvatarByUsernameAsync("devangpatel@gmail.com");
            if (userAvatar != null)
            {
                if (!string.IsNullOrEmpty(userAvatar.FirstName))
                {
                    Console.WriteLine("Avatar Loaded.");
                    Console.WriteLine("Avatar First Name:{0}", userAvatar.FirstName);
                    Console.WriteLine("Avatar Last Name:{0}", userAvatar.LastName);
                }
                else
                {
                    Console.WriteLine("No record found.");
                }
            }

            Console.WriteLine("Loading all Avatar...");
            var AvatarList = await neo.LoadAllAvatarsAsync();
            if (AvatarList != null)
            {
                Console.WriteLine("Avatar List Started.");
                foreach (var item in AvatarList)
                {
                    Console.WriteLine("Avatar First Name:{0}", item.FirstName);
                    Console.WriteLine("Avatar Last Name:{0}", item.LastName);
                }
                Console.WriteLine("Avatar List Ended.");                
            }

            Console.WriteLine("Deleting Avatar...");
            var result = await neo.DeleteAvatarByEmailAsync("harshalpatel@gmail.com");
            if (result == true)
            {
                Console.WriteLine("Avatar Deleted.");
            }
            else
            {
                Console.WriteLine("Error in Avatar Deletion, Please try after some time");
            }
        }
    }
}

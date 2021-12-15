using System;
using NextGenSoftware.OASIS.API.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NextGenSoftware.OASIS.API.Core.Interfaces;
using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Holons;
using NextGenSoftware.OASIS.API.Core.Helpers;
using Neo4j.Driver;

namespace NextGenSoftware.OASIS.API.Providers.Neo4jOASIS.Aura
{
    public class Neo4jOASIS : OASISStorageBase, IOASISStorage, IOASISNET
    {        
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IDriver _driver;

        public Neo4jOASIS(string host, string username, string password)
        {
            this.ProviderName = "Neo4jOASIS";
            this.ProviderDescription = "Neo4j Provider";
            this.ProviderType = new EnumValue<ProviderType>(Core.Enums.ProviderType.Neo4jOASIS);
            this.ProviderCategory = new EnumValue<ProviderCategory>(Core.Enums.ProviderCategory.StorageAndNetwork);

            Host = host;
            Username = username;
            Password = password;          

        }

        private async Task<bool> Connect()
        {
            try
            {                
                _driver = GraphDatabase.Driver(Host, AuthTokens.Basic(Username, Password));
                
                await _driver.VerifyConnectivityAsync();
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return false;
            }

        }

        private async Task Disconnect()
        {
            //TODO: Find if there is a disconnect/shutdown function?
            await _driver.CloseAsync();
            _driver = null;
        }
        

        public override bool DeleteAvatar(Guid id, bool softDelete = true)
        {
            //GraphClient.Cypher.OptionalMatch("(avatar:Avatar)-[r]-()")
            //    .Where((Avatar avatar) => avatar.Id == id)
            //    .Delete("r,avatar")
            //    .ExecuteWithoutResultsAsync();

            return true;

            //public void DeletePerson(string personName)
            //{
            //    this.graphClient.Cypher.OptionalMatch("(person:Person)-[r]-()")
            //        .Where((Person person) => person.name == personName)
            //        .Delete("r,person")
            //        .ExecuteWithoutResults();
            //}
        }

        public override bool DeleteAvatarByEmail(string avatarEmail, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteAvatarByUsername(string avatarUsername, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> DeleteAvatarByUsernameAsync(string avatarUsername, bool softDelete = true)
        {
            try
            {
                var session = _driver.AsyncSession(WithDatabase);

                var avatarList = await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (p:Avatar {username: $username})
                        DELETE p",
                        new
                        {
                            username = avatarUsername,                            
                        }
                    );

                    return await cursor.ToListAsync(record => new Avatar
                    {
                        LastName = record["lastname"].As<string>()
                    });
                });

                if (avatarList.Count<=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch (Exception ex)
            {                
                return false;
            }
        }

        public override bool DeleteAvatar(string providerKey, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteAvatarAsync(Guid id, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> DeleteAvatarByEmailAsync(string avatarEmail, bool softDelete = true)
        {
            try
            {
                var session = _driver.AsyncSession(WithDatabase);

                var avatarList = await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (p:Avatar {EMail: $eMail})
                        DELETE p",
                        new
                        {
                            eMail = avatarEmail,
                        }
                    );

                    return await cursor.ToListAsync(record => new Avatar
                    {
                        LastName = record["lastname"].As<string>()
                    });
                });

                if (avatarList.Count <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public override Task<bool> DeleteAvatarAsync(string providerKey, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteHolon(Guid id, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteHolon(string providerKey, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteHolonAsync(Guid id, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteHolonAsync(string providerKey, bool softDelete = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IHolon> GetHolonsNearMe(HolonType Type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IPlayer> GetPlayersNearMe()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IAvatar> LoadAllAvatars()
        {
            throw new NotImplementedException();
        }

        public override IAvatar LoadAvatarByEmail(string avatarEmail)
        {
            throw new NotImplementedException();
        }

        public override IAvatar LoadAvatarByUsername(string avatarUsername)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<IAvatar>> LoadAllAvatarsAsync()
        {
            try
            {
                var session = _driver.AsyncSession(WithDatabase);

                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (av:Avatar)                        
                        RETURN av.FirstName AS firstname,av.LastName AS lastname"                        
                    );

                    return await cursor.ToListAsync(record => new Avatar
                    {
                        FirstName = record["firstname"].As<string>(),
                        LastName = record["lastname"].As<string>()
                    });
                    
                });
            }
            catch (Exception ex)
            {
                List<IAvatar> objAvatarList = new List<IAvatar>();
                IAvatar objAv = new Avatar { FirstName = ex.ToString() };
                objAvatarList.Add(objAv);
                return objAvatarList;
            }
        }

        public override async Task<IAvatar> LoadAvatarByUsernameAsync(string avatarUsername)
        {
            try
            {
                var session = _driver.AsyncSession(WithDatabase);

                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (av:Avatar)
                        WHERE TOLOWER(av.username) CONTAINS TOLOWER($UserName)
                        RETURN av.FirstName AS firstname,av.LastName AS lastname",
                        new { UserName = avatarUsername }
                    );

                    var avList = await cursor.ToListAsync(record => new Avatar
                    {
                        FirstName = record["firstname"].As<string>(),
                        LastName = record["lastname"].As<string>()
                    });
                    IAvatar objAv = new Avatar();
                    if (avList != null)
                    {
                        if (avList.Count > 0)
                        {
                            objAv = avList[0];
                        }
                    }
                    return objAv;
                });
            }
            catch (Exception ex)
            {
                //List<IAvatar> objAvatarList = new List<IAvatar>();
                IAvatar objAv = new Avatar { FirstName = ex.ToString() };
                //objAvatarList.Add(objAv);
                return objAv;
            }
        }

        public override IAvatar LoadAvatar(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override IAvatar LoadAvatar(string username, string password)
        {
            try
            {
                //var people = _graphClient.Cypher
                //  .Match("(p:Person)")
                //  .Return(p => p.As<Person>())
                //  .Results;


                //Avatar avatar =
                //    GraphClient.Cypher.Match("(p:Avatar {Username: {nameParam}})") //TODO: Need to add password to match query...
                //   .WithParam("nameParam", username)
                //  .Return(p => p.As<Avatar>())
                //    .ResultsAsync.Result.Single();

                return null;
            }
            catch (InvalidOperationException) //thrown when nothing found
            {
                return null;
            }
        }

        public override IAvatar LoadAvatar(string username)
        {
            //Avatar avatar =
            //       GraphClient.Cypher.Match("(p:Avatar {Username: {nameParam}})")
            //      .WithParam("nameParam", username)
            //     .Return(p => p.As<Avatar>())
            //       .ResultsAsync.Result.Single();

            return null;
        }

        public override Task<IAvatar> LoadAvatarAsync(string providerKey)
        {
            throw new NotImplementedException();
        }

        public override Task<IAvatar> LoadAvatarAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IAvatar> LoadAvatarByEmailAsync(string avatarEmail)
        {
            try
            {
                var session = _driver.AsyncSession(WithDatabase);

                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (av:Avatar)
                        WHERE TOLOWER(av.EMail) CONTAINS TOLOWER($email)
                        RETURN av.FirstName AS firstname,av.LastName AS lastname",
                        new { email = avatarEmail }
                    );

                    var avList= await cursor.ToListAsync(record => new Avatar
                    {
                        FirstName= record["firstname"].As<string>(),
                        LastName = record["lastname"].As<string>()
                    });
                    IAvatar objAv = new Avatar ();
                    if (avList != null)
                    {
                        if (avList.Count > 0)
                        {
                            objAv = avList[0];
                        }
                    }
                    return objAv;
                });                
            }
            catch (Exception ex)
            {
                //List<IAvatar> objAvatarList = new List<IAvatar>();
                IAvatar objAv = new Avatar { FirstName = ex.ToString() };
                //objAvatarList.Add(objAv);
                return objAv;
            }
        }

        public override async Task<IAvatar> LoadAvatarAsync(string username, string password)
        {
            try
            {
                var session = _driver.AsyncSession(WithDatabase);

                return await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (av:Avatar)
                        WHERE TOLOWER(av.EMail) CONTAINS TOLOWER($username)
                        RETURN av.FirstName AS firstname,av.LastName AS lastname",
                        new { username = username }
                    );

                    var avList = await cursor.ToListAsync(record => new Avatar
                    {
                        FirstName = record["firstname"].As<string>(),
                        LastName = record["lastname"].As<string>()
                    });
                    IAvatar objAv = new Avatar();
                    if (avList != null)
                    {
                        if (avList.Count > 0)
                        {
                            objAv = avList[0];
                        }
                    }
                    return objAv;
                });
            }
            catch (Exception ex)
            {
                //List<IAvatar> objAvatarList = new List<IAvatar>();
                IAvatar objAv = new Avatar { FirstName = ex.ToString() };
                //objAvatarList.Add(objAv);
                return objAv;
            }
        } 

        public override IAvatar LoadAvatarForProviderKey(string providerKey)
        {
            throw new NotImplementedException();
        }

        public override Task<IAvatar> LoadAvatarForProviderKeyAsync(string providerKey)
        {
            throw new NotImplementedException();
        }


        public override IAvatar SaveAvatar(IAvatar avatar)
        {
            if (avatar.Id == Guid.Empty)
            {
                // _graphClient.ExecutionConfiguration.EncryptionLevel = Neo4j.Driver.EncryptionLevel.Encrypted;

                avatar.Id = Guid.NewGuid();
                try
                {
                    string res = string.Empty;
                    //using (var session = _driver.AsyncSession())
                    //{
                    //    string qry = "CREATE (a:Avtar {Title: '" + avatar.Title + "',FirstName :'" + avatar.FirstName + "',";
                    //    qry += "LastName:'" + avatar.LastName + "',Email:'" + avatar.Email + "'})RETURN a";
                    //    var greeting = session.WriteTransactionAsync(tx =>
                    //    {                            
                    //        var result = tx.RunAsync(qry).Result;

                    //        res = result.ToString();
                    //        return result.SingleAsync();
                    //    });
                    //}

                    
                    Console.WriteLine(res);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                //_graphClient.Cypher
                //    .Unwind(persons, "person")
                //    .Merge("(p:Person { Id: person.Id })")
                //    .OnCreate()
                //    .Set("p = person")
                //    .ExecuteWithoutResults();

                //GraphClient.Cypher
                //   .Merge("(a:Avatar { Id: avatar.Id })") //Only create if doesn't alreadye exists.
                //   .OnCreate()
                //   .Set("a = avatar") //Once created, set the properties.
                //   .ExecuteWithoutResultsAsync();
            }
            else
            {
                //GraphClient.Cypher
                //   .Match("(a:Avatar)")
                //   .Where((Avatar a) => a.Id == avatar.Id)
                //   .Set("a = avatar") //Set the properties.
                //   .ExecuteWithoutResultsAsync();


                /*
                ITransactionalGraphClient txClient = _graphClient;

                using (var tx = txClient.BeginTransaction())
                {
                    txClient.Cypher
                        .Match("(m:Movie)")
                        .Where((Movie m) => m.title == originalMovieName)
                        .Set("m.title = {newMovieNameParam}")
                        .WithParam("newMovieNameParam", newMovieName)
                        .ExecuteWithoutResults();

                    txClient.Cypher
                        .Match("(m:Movie)")
                        .Where((Movie m) => m.title == newMovieName)
                        .Create("(p:Person {name: {actorNameParam}})-[:ACTED_IN]->(m)")
                        .WithParam("actorNameParam", newActorName)
                        .ExecuteWithoutResults();

                    tx.CommitAsync();
                }*/
            }

            return avatar;
        }

        public async override Task<IAvatar> SaveAvatarAsync(IAvatar Avatar)
        {
            try
            {
                var session = _driver.AsyncSession(WithDatabase);

                var avatarList = await session.ReadTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MATCH (p:Avatar {name: $name})
                        SET p.EMail = $eMail,p.FirstName = $firstName,p.LastName = $lastName
                        RETURN p.LastName as lastname",
                        new 
                        {
                            name=Avatar.Title,
                            eMail = Avatar.Email,
                            firstName = Avatar.FirstName,
                            lastName=Avatar.LastName,
                        }
                    );

                    return await cursor.ToListAsync(record => new Avatar {
                        LastName= record["lastname"].As<string>()                        
                    });
                });

                if (avatarList != null)
                {
                    if (avatarList.Count > 0)
                    {
                        IAvatar objAv = new Avatar { FirstName = "Record updated successfully" };                        
                        return objAv;
                    }
                }
                
                return await session.WriteTransactionAsync(async transaction =>
                {
                    var cursor = await transaction.RunAsync(@"
                        MERGE (p1:Avatar { name:$Name, FirstName: $firstName ,
                                            LastName:$lastName,EMail:$eMail,
                                            username:$userName,password:$Password})                        
                        RETURN p1.name as name",
                        new { Name=Avatar.Title, 
                                firstName=Avatar.FirstName, 
                                lastName=Avatar.LastName, 
                                eMail=Avatar.Email,
                                userName = Avatar.Username,
                                Password = Avatar.Password,
                            }
                    );

                    return await cursor.SingleAsync(record=> new Avatar { Title = record["name"].As<string>() });                    
                });
            }
            catch (Exception ex)
            {
                IAvatar objAv = new Avatar { FirstName = ex.ToString() };
                return objAv;
            }              
            
        }

        private static void WithDatabase(SessionConfigBuilder sessionConfigBuilder)
        {
            //var neo4jVersion = System.Environment.GetEnvironmentVariable("NEO4J_VERSION") ?? "";
            //if (!neo4jVersion.StartsWith("4"))
            //{
            //    return;
            //}

            sessionConfigBuilder.WithDatabase("neo4j");
        }
        public override OASISResult<IHolon> SaveHolon(IHolon holon, bool saveChildrenRecursive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<OASISResult<IHolon>> SaveHolonAsync(IHolon holon, bool saveChildrenRecursive = true)
        {
            throw new NotImplementedException();
        }

        public override OASISResult<IEnumerable<IHolon>> SaveHolons(IEnumerable<IHolon> holons, bool saveChildrenRecursive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<OASISResult<IEnumerable<IHolon>>> SaveHolonsAsync(IEnumerable<IHolon> holons, bool saveChildrenRecursive = true)
        {
            throw new NotImplementedException();
        }

        public override Task<ISearchResults> SearchAsync(ISearchParams searchParams)
        {
            throw new NotImplementedException();
        }

        public override void ActivateProvider()
        {
            Connect().Wait();
            base.ActivateProvider();
        }

        public override void DeActivateProvider()
        {
            Disconnect().Wait();
            base.DeActivateProvider();
        }

        public override IHolon LoadHolon(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<IHolon> LoadHolonAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IHolon LoadHolon(string providerKey)
        {
            throw new NotImplementedException();
        }

        public override Task<IHolon> LoadHolonAsync(string providerKey)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IHolon> LoadHolonsForParent(Guid id, HolonType type = HolonType.All)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<IHolon>> LoadHolonsForParentAsync(Guid id, HolonType type = HolonType.All)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IHolon> LoadHolonsForParent(string providerKey, HolonType type = HolonType.All)
        {
            throw new NotImplementedException();
        }

        public override Task<OASISResult<IEnumerable<IHolon>>> LoadHolonsForParentAsync(string providerKey, HolonType type = HolonType.All)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IHolon> LoadAllHolons(HolonType type = HolonType.All)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<IHolon>> LoadAllHolonsAsync(HolonType type = HolonType.All)
        {
            throw new NotImplementedException();
        }

        public override IAvatarDetail LoadAvatarDetail(Guid id)
        {
            throw new NotImplementedException();
        }

        public override IAvatarDetail LoadAvatarDetailByEmail(string avatarEmail)
        {
            throw new NotImplementedException();
        }

        public override IAvatarDetail LoadAvatarDetailByUsername(string avatarUsername)
        {
            throw new NotImplementedException();
        }

        public override Task<IAvatarDetail> LoadAvatarDetailAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override async Task<IAvatarDetail> LoadAvatarDetailByUsernameAsync(string avatarUsername)
        {
            throw new NotImplementedException();
        }

        public override async Task<IAvatarDetail> LoadAvatarDetailByEmailAsync(string avatarEmail)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IAvatarDetail> LoadAllAvatarDetails()
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<IAvatarDetail>> LoadAllAvatarDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public override IAvatarDetail SaveAvatarDetail(IAvatarDetail Avatar)
        {
            throw new NotImplementedException();
        }

        public override Task<IAvatarDetail> SaveAvatarDetailAsync(IAvatarDetail Avatar)
        {
            throw new NotImplementedException();
        }
    }
}

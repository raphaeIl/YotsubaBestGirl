using Serilog;
using System.Collections.Concurrent;
using YotsubaBestGirl.Database;
using YotsubaBestGirl.Database.Entities;

namespace YotsubaBestGirl.GameServer.Services
{
    public interface ISessionService
    {
        string CreateSession(string uuid); // given uuid from req, create a session and return the session key, also create account if doesnt exist
        int GetPlayerIdBySession(string sessionKey); // retrieves player id by session key, which is present in every req

        bool TryGetUser(string sessionKey, out UserDB user); // get user in db, null if not exist
        UserDB CreateUser(string sessionKey); // create a default user in the db, fields not set, should be set outside, mid design?
    }

    public class SessionService : ISessionService
    {
        private readonly YotsubaContext dbContext;

        // maps sessionkey -> id (uid NOT uuid)
        private readonly ConcurrentDictionary<string, int> _sessions = new();

        public SessionService(YotsubaContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public string CreateSession(string uuid)
        {
            var account = dbContext.PlayerAccounts.FirstOrDefault(a => a.Uuid == uuid);

            if (account == null)
            {
                account = new PlayerAccountDB()
                {
                    Uuid = uuid
                };

                dbContext.PlayerAccounts.Add(account);
                dbContext.SaveChanges();
            }

            var sessionKey = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            _sessions[sessionKey] = account.Id;
            
            return sessionKey;
        }

        public int GetPlayerIdBySession(string sessionKey)
        {
            if (!_sessions.TryGetValue(sessionKey, out var playerId))
            {
                return -1;
            }

            return dbContext.PlayerAccounts.Where(x => x.Id == playerId).FirstOrDefault().Id;
        }

        public bool TryGetUser(string sessionKey, out UserDB user)
        {
            if (!_sessions.TryGetValue(sessionKey, out var playerId))
            {
                user = null;
                return false;
            }

            user = dbContext.Users.FirstOrDefault(u => u.Uid == playerId);
            return user != null;
        }

        // creates a default user in db
        public UserDB CreateUser(string sessionKey)
        {
            if (this.TryGetUser(sessionKey, out UserDB user))
            {
                Log.Warning("User already exists for session key: {SessionKey}", sessionKey);
                return user;
            }

            var playerId = this.GetPlayerIdBySession(sessionKey);

            if (playerId == -1)
            {
                throw new InvalidDataException("Invalid session key or player ID not found.");
            }

            user = new UserDB()
            {
                Uid = playerId,
            };

            Log.Information("Created new user with uid {uid}", user.Uid);
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            Log.Information("Sucessfully added user to db");
            return user;
        }
    }

    internal static class SessionServiceExtensions
    {
        public static void AddSessionService(this IServiceCollection services)
        {
            services.AddSingleton<ISessionService, SessionService>();
        }
    }
}

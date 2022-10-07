using Newtonsoft.Json;

namespace telegram_bot
{
    public class DB
    {
        private string path;
        
        
        private Dictionary<long, BotUser> dbUsers;
        private Dictionary<string, string> dbKeyWords;
        
        public DB(string root, string localDir, string dbName, string extention)
        {
            path = Path.Join(root, localDir, dbName + "." + extention);
        }
        public void Update(long id, BotUser user)
        {
            if (!FindByKey(id))
            {
                return;
            }
            dbUsers = ReadAllUsers();
            dbUsers[id] = user;
            var serializedDict = JsonConvert.SerializeObject(dbUsers);
            File.WriteAllText(path, serializedDict);
        }
        public void Update(string key, string answer)
        {
            if (!FindByKey(key))
            {
                return;
            }
            dbKeyWords = ReadAllAnswers();
            dbKeyWords[key] = answer;
            var serializedDict = JsonConvert.SerializeObject(dbKeyWords);
            File.WriteAllText(path, serializedDict);
        }

        public void Add(BotUser user)
        {
            if (FindByKey(user.id))
            {
                return;
            }
            dbUsers = ReadAllUsers();
            dbUsers.Add(user.id, user);
            var serializedDict = JsonConvert.SerializeObject(dbUsers);
            File.WriteAllText(path, serializedDict);
        }
        public void Add(string key, string answer)
        {
            if (FindByKey(key))
            {
                return;
            }
            dbKeyWords = ReadAllAnswers();
            dbKeyWords.Add(key, answer);
            var serializedDict = JsonConvert.SerializeObject(dbKeyWords);
            File.WriteAllText(path, serializedDict);
        }
        public void Delete(long id)
        {
            if (!FindByKey(id))
            {
                return;
            }
            dbUsers = ReadAllUsers();
            dbUsers.Remove(id);
            var serializedDict = JsonConvert.SerializeObject(dbUsers);
            File.WriteAllText(path, serializedDict);
        }
        public void Delete(string key)
        {
            if (!FindByKey(key))
            {
                return;
            }
            dbKeyWords = ReadAllAnswers();
            dbKeyWords.Remove(key);
            var serializedDict = JsonConvert.SerializeObject(dbKeyWords);
            File.WriteAllText(path, serializedDict);
        }
        public BotUser GetByKey(long id)
        {
            if (FindByKey(id))
            {
                return null;
            }
            dbUsers = ReadAllUsers();
            return dbUsers[id];
        }
        public string GetByKey(string key)
        {
            if (FindByKey(key))
            {
                return null;
            }
            dbKeyWords = ReadAllAnswers();
            return dbKeyWords[key];
        }

        public bool FindByKey(long id)
        {
            dbUsers = ReadAllUsers();
            return dbUsers.ContainsKey(id);
        }
        public bool FindByKey(string key)
        {
            dbKeyWords = ReadAllAnswers();
            return dbKeyWords.ContainsKey(key);
        }

        public Dictionary<long, BotUser> ReadAllUsers()
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                var emptyDict = new Dictionary<long, BotUser>();
                return emptyDict;
            }
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<long, BotUser>>(json);
        }
        public Dictionary<string, string> ReadAllAnswers()
        {
            if (!File.Exists(path))
            {
                File.Create(path);
                var emptyDict = new Dictionary<string, string>();
                return emptyDict;
            }
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

    }
}


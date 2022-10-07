using Newtonsoft.Json;

namespace telegram_bot
{
    public class DB
    {
        private string path;
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
            var currDict = ReadAll();
            currDict[id] = user;
            var serializedDict = JsonConvert.SerializeObject(currDict);
            File.WriteAllText(path, serializedDict);
        }

        public void Add(BotUser user)
        {
            if (FindByKey(user.id))
            {
                return;
            }
            var currDict = ReadAll();
            currDict.Add(user.id, user);
            var serializedDict = JsonConvert.SerializeObject(currDict);
            File.WriteAllText(path, serializedDict);
        }
        public void Delete(long id)
        {
            if (!FindByKey(id))
            {
                return;
            }
            var currDict = ReadAll();
            currDict.Remove(id);
            var serializedDict = JsonConvert.SerializeObject(currDict);
            File.WriteAllText(path, serializedDict);
        }
        public BotUser GetByKey(long id)
        {
            if (FindByKey(id))
            {
                return null;
            }
            var currDict = ReadAll();
            return currDict[id];
        }

        public bool FindByKey(long id)
        {
            var currDict = ReadAll();
            return currDict.ContainsKey(id);
        }

        public Dictionary<long, BotUser> ReadAll()
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

    }
}


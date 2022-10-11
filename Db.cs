using Newtonsoft.Json;
using telegram_bot.enums;

namespace telegram_bot
{
    public class Db<TKey, TValue>
    {
        protected readonly string _path;
        protected Dictionary<TKey, TValue> Table;

        protected Db(string dbName, string fileExtention)
        {
            var curDir = Directory.GetCurrentDirectory();
            var parent2 = Directory.GetParent(curDir).FullName;
            var parent1 = Directory.GetParent(parent2).FullName;
            var parent = Directory.GetParent(parent1).FullName;

            Directory.CreateDirectory(System.IO.Path.Join(parent, "db"));
            
            _path = System.IO.Path.Join(parent, "db", dbName + "." + fileExtention);
            Table = new Dictionary<TKey, TValue>();
        }
        public void Update(TKey id, TValue user)
        {
            if (!FindByKey(id))
            {
                return;
            }
            Table = ReadAllTable();
            Table[id] = user;
            WriteAllTable(Table);
        }
        public void Add(TKey id, TValue user)
        {
            if (FindByKey(id))
            {
                return;
            }
            Table = ReadAllTable();
            Table.Add(id, user);
            WriteAllTable(Table);
        }
        public void Delete(TKey id)
        {
            if (!FindByKey(id))
            {
                return;
            }
            Table = ReadAllTable();
            Table.Remove(id);
            WriteAllTable(Table);
        }
        public bool FindByKey(TKey id)
        {
            Table = ReadAllTable();
            return Table.ContainsKey(id);
        }
        public Dictionary<TKey, TValue> ReadAllTable()
        {
            var emptyDict = new Dictionary<TKey, TValue>();
            if (!File.Exists(_path))
            { 
                File.Create(_path);
                WriteAllTable(emptyDict);
                return emptyDict;
            }
            var json = File.ReadAllText(_path);
            if (json.Length == 0)
            {
                json = "{}";
                WriteAllTable(emptyDict);
            }
            return JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(json);
        }

        protected void WriteAllTable(Dictionary<TKey, TValue> dict)
        {
            if (!File.Exists(_path))
            { 
                File.Create(_path);
            }
            File.WriteAllText(_path, JsonConvert.SerializeObject(dict));
        }
    }
    public class DbUsers : Db<long, BotUser>
    {
        public DbUsers(string dbName, string fileExtention) : base(dbName, fileExtention) {}
        public BotUser GetOrCreate(long id, BotUser user)
        {
            if (FindByKey(id))
            {
                return GetByKey(id);
            }
            else
            {
                var emptyGame = new Game(0, 0, 0);
                var newUser = new BotUser(GameStatus.NotPlaying, id, user.name, emptyGame);
                Add(id, newUser);
                return user;
            }
        }
        public BotUser GetByKey(long id)
        {
            if (!FindByKey(id))
            {
                return null;
            }
            Table = ReadAllTable();
            return Table[id];
        }
    }
    public class DbKeyWords : Db<string, string>
    {
        public DbKeyWords(string dbName, string fileExtention) : base(dbName, fileExtention)
        {
            Table["/start"] = "Bot game 'Guess number' - guess number from range\n" +
                              "/start - начать\n" +
                              "/help - commands\n" +
                              "/play - start game (set left and right borders)";
            Table["/help"] = "Bot game 'Guess number' - guess number from range\n" +
                              "/start - начать\n" +
                              "/help - commands\n" +
                              "/play - start game (set left and right borders)";
            Table[""] = "Bot game 'Guess number' - guess number from range\n" +
                             "/start - начать\n" +
                             "/help - commands\n" +
                             "/play - start game (set left and right borders)";
            Table["/end"] = "You have completed the game\n" +
                            "/start - начать\n" +
                            "/help - commands\n" +
                            "/play - start game (set left and right borders)";
            Table["/play"] = "set left and right border by pattern: [leftBorder] [rightBorder]";
            Table["Привет"] = "Привет!";
            Table["Пока"] = "Пока:(";
            WriteAllTable(Table);
        }
        public string GetByKey(string key)
        {
            Table = ReadAllTable();
            if (!FindByKey(key))
            {
                return null;
            }
            return Table[key];
        }
    }
}


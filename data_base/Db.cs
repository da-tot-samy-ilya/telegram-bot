using Newtonsoft.Json;
using telegram_bot.bot;
using File = System.IO.File;
namespace telegram_bot.data_base
{
    public class Db<TKey, TValue> where TKey : notnull
    {
        private readonly string _path;
        protected Dictionary<TKey, TValue> Table;

        protected Db(string dbName, string fileExtention)
        {
            Directory.CreateDirectory(@"..\..\..\data_base\db");
            _path = Path.Join(@"..\..\..\data_base\db", dbName + "." + fileExtention);
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
            if (json == "")
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
}
using MongoDB.Bson;
using MongoDB.Driver;
using telegram_bot.bot;

namespace telegram_bot
{
    public class UsersDb //123123123
    {
        private readonly MongoClientSettings _settings;
        private MongoClient _client;
        private IMongoDatabase _db;
        private IMongoCollection<BotUser> _collection;
    
        public UsersDb(string dbName, string collectionName)
        {
            _settings = MongoClientSettings.FromConnectionString(
                "mongodb+srv://aboba:1234@tinder.sltinsc.mongodb.net/?retryWrites=true&w=majority");
            _settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _client = new MongoClient(_settings);
            _db = _client.GetDatabase(dbName); //"tinder-users"
            _collection = _db.GetCollection<BotUser>(collectionName); //"users"
        }
        public void Update(long userid, BotUser user)
        {
            var filter = Builders<BotUser>.Filter.Eq(p => p.id, user.id);
            _collection.ReplaceOne(filter, user);
        }
        public void Add(BotUser user)
        {
            if (_collection.CountDocuments(p => p.id == user.id) == 0)
            {
                _collection.InsertOne(user);
            }
        }
        public void Delete(BotUser user)
        {
            _collection.DeleteMany(p => p.id == user.id);
        }
        public List<BotUser> ReadAll()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }
        public BotUser GetOrCreate(long userId, String userName, int lastMessageId)
        {
            List<BotUser> users = _collection.Find(p=>p.id == userId).ToList();
            if (users.Count == 0)
            {
                var user = new BotUser(userId, userName, lastMessageId);
                Add(user);
                return user;
            }
            return users[0];//
        }
    }
}
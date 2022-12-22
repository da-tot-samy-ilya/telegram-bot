using telegram_bot.bot;

namespace telegram_bot.data_base
{
    public class UsersDb : Db<long, BotUser>
    {
        public UsersDb(string dbName, string fileExtention) : base(dbName, fileExtention)
        {
        }

        public BotUser GetOrCreate(long id, string userName, int lastMessageId)
        {
            if (FindByKey(id))
            {
                return GetByKey(id);
            }
            else
            {
                var newUser = new BotUser(id, userName, lastMessageId);
                Add(id, newUser);
                return newUser;
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
};
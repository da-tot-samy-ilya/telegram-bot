using telegram_bot.enums;

namespace telegram_bot
{
    public class Logic
    {
        static DbKeyWords dbKeyWords = new DbKeyWords("keyWords", "json");

        private static string MessageProcessing(string message)
        {
            var keyWordsInMessage = message.Split(' ');
            var keyWord = "";
            foreach (var keyWordInMessage in keyWordsInMessage)
            {
                if (dbKeyWords.ReadAllTable().ContainsKey(keyWordInMessage))
                {
                    if (keyWord == "")
                        keyWord = keyWordInMessage;
                    else
                    {
                        keyWord = "";
                        break;
                    }
                }
            }

            if (keyWordsInMessage.Length == 2
                && int.TryParse(keyWordsInMessage[0], out int number1)
                && int.TryParse(keyWordsInMessage[1], out int number2))
                return keyWordsInMessage[0] + " " + keyWordsInMessage[1];
            return keyWord;
        }

        public static string GenerateAnswer(DbUsers dbUsers, long userId, BotUser user, string message)
        {
            user = dbUsers.GetOrCreate(userId, user);

            var keyWord = MessageProcessing(message);

            if (user.gameStatus == GameStatus.NotPlaying)
                return user.gameProps.UserIsNotPlaying(keyWord, user, dbKeyWords, dbUsers);

            if (user.gameStatus == GameStatus.ChoosingRange)
                return user.gameProps.UserIsChoosingRange(keyWord, user, dbKeyWords, dbUsers);

            return user.gameProps.UserIsPlaying(keyWord, user, dbKeyWords, dbUsers);
        }

    }
}
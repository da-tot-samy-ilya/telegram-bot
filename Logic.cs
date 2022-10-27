using telegram_bot.enums;

namespace telegram_bot
{
    public class Logic
    {
        private readonly DbKeyWords _dbKeyWords;
        private readonly DbUsers _dbUsers;
        public Logic(DbUsers dbUsers, DbKeyWords dbKeyWords)
        {
            _dbKeyWords = dbKeyWords;
            _dbUsers = dbUsers;
        }
        
        private string GetHighlightKeyword(string message) // Выделение ключевых слов: если есть одно, возвращает его, если ни одного "", если 2, то ""
        {
            var keyWordsInMessage = message.Split(' ');
            var keyWord = "";
            foreach (var keyWordInMessage in keyWordsInMessage)
            {
                if (_dbKeyWords.ReadAllTable().ContainsKey(keyWordInMessage))
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
            return keyWord;
        }

        public string GenerateAnswer(long userId, BotUser user, string message) // ответ на сообщение пользователя
        {
            user = _dbUsers.GetOrCreate(userId, user); // проверка существования пользователя

            var keyWord = GetHighlightKeyword(message); // выделение ключевых слов

            return null;

            /*if (user.GameStatus == GameStatus.NotPlaying) 
                return user.GameProps.GetReplyIfNotPlaying(keyWord, user, _dbKeyWords, _dbUsers);

            if (keyWord == "")
                keyWord = message;

            if (user.GameStatus == GameStatus.ChoosingRange)
                return user.GameProps.GetReplyIfChoosingRange(keyWord, user, _dbKeyWords, _dbUsers);

            return user.GameProps.GetReplyIfPlaying(keyWord, user, _dbKeyWords, _dbUsers);*/
        }

    }
}
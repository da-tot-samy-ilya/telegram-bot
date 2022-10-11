
using telegram_bot.enums;
namespace telegram_bot
{
    public class Game
    {
        public int leftBorder = 0;
        public int rightBorder = 1;
        public int choosedNumber = 0;

        public Game(int LeftBorder, int RightBorder, int Number)
        {
            leftBorder = LeftBorder;
            rightBorder = RightBorder;
            choosedNumber = Number;
        }
        
        public string UserIsPlaying(string message, BotUser user, DbKeyWords dbKeyWords,DbUsers dbUsers)
        {
            if (message == "/end" || message == "/help")
                return dbKeyWords.GetByKey(message);
            if (int.TryParse(message, out var numForTryParse))
            {
                if (numForTryParse <= user.gameProps.rightBorder && numForTryParse >= user.gameProps.leftBorder)
                {
                    if (numForTryParse == user.gameProps.choosedNumber)
                    {
                        user.gameStatus = GameStatus.NotPlaying;
                        dbUsers.Update(user.id, user);
                        return $"You win, That`s {user.gameProps.choosedNumber}!\nTry to play again: /play";
                    }
                    if (numForTryParse > user.gameProps.choosedNumber)
                    {
                        return $"Try to choose lesser number from [{user.gameProps.leftBorder.ToString()},{user.gameProps.rightBorder.ToString()}]";
                    }
                    return $"Try to choose bigger number from [{user.gameProps.leftBorder.ToString()},{user.gameProps.rightBorder.ToString()}]";
                }
                return $"You went out of range: [{user.gameProps.leftBorder.ToString()},{user.gameProps.rightBorder.ToString()}]\n Please, try again";
            }
            return dbKeyWords.GetByKey("");
        }

        public string UserIsNotPlaying(string message, BotUser user, DbKeyWords dbKeyWords, DbUsers dbUsers)
        {
            if (message == "/play")
            {
                user.gameStatus = GameStatus.ChoosingRange;
                dbUsers.Update(user.id, user);
                return dbKeyWords.GetByKey(message);
            }
            else if (dbKeyWords.FindByKey(message) || message != "/end")
                return dbKeyWords.GetByKey(message);
            return dbKeyWords.GetByKey("");
        }

        public string UserIsChoosingRange(string message, BotUser user, DbKeyWords dbKeyWords, DbUsers dbUsers)
        {
            if (message == "/end" || message == "/help")
                return dbKeyWords.GetByKey(message);
                
            var splitted = message.Split(' ');

            var isFirstNum = int.Parse(splitted[0]);
            var isSecondtNum = int.Parse(splitted[1]);

            user.gameStatus = GameStatus.Playing;
            user.gameProps.leftBorder = isFirstNum;
            user.gameProps.rightBorder = isSecondtNum;
            
            dbUsers.Update(user.id, user);

            var rnd = new Random();
            user.gameProps.choosedNumber = rnd.Next(isFirstNum, isSecondtNum);
            return $"Nice! Your range is [{isFirstNum.ToString()},{isSecondtNum.ToString()}]\n Now try guess what number do I guess...";
        }
    }
};



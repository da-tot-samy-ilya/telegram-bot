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
        
        public string UserIsPlaying(string message, BotUser user, Dictionary<string, string> keyWords, DB dbKeyWords)
        {
            if (message == "/end" || message == "/help")
                return keyWords[message]; 
            if (int.TryParse(message, out var numForTryParse))
            {
                if (numForTryParse <= user.gameProps.rightBorder && numForTryParse >= user.gameProps.leftBorder)
                {
                    if (numForTryParse == user.gameProps.choosedNumber)
                    {
                        user.gameStatus = GameStatus.NotPlaying;
                        dbKeyWords.Update(user.id, user);
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
            return keyWords[""];
        }

        public string UserIsNotPlaying(string message, BotUser user, Dictionary<string, string> keyWords, DB dbKeyWords)
        {
            if (message == "/play")
            {
                user.gameStatus = GameStatus.ChoosingRange;
                dbKeyWords.Update(user.id, user);
                return keyWords[message];
            }
            else if (keyWords.ContainsKey(message) || message != "/end")
                return keyWords[message];
            return keyWords[""];
        }

        public string UserIsChoosingRange(string message, BotUser user, Dictionary<string, string> keyWords, DB dbKeyWords)
        {
            if (message == "/end" || message == "/help")
                return keyWords[message];
                
            var splitted = message.Split(' ');

            var isFirstNum = int.Parse(splitted[0]);
            var isSecondtNum = int.Parse(splitted[1]);

            user.gameStatus = GameStatus.Playing;
            dbKeyWords.Update(user.id, user);
            user.gameProps.leftBorder = isFirstNum;
            user.gameProps.rightBorder = isSecondtNum;

            var rnd = new Random();
            user.gameProps.choosedNumber = rnd.Next(isFirstNum, isSecondtNum);
            return $"Nice! Your range is [{isFirstNum.ToString()},{isSecondtNum.ToString()}]\n Now try guess what number do I guess...";
        }
    }
};


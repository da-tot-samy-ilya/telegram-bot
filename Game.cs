using telegram_bot.enums;
namespace telegram_bot
{
    public class Game
    {
        public int LeftBorder;
        public int RightBorder;
        public int ChoosedNumber;
        public readonly Random Random;

        public Game(int leftBorder, int rightBorder, int number, int seed)
        {
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            ChoosedNumber = number;
            Random = new Random(seed); //сид 
        }
        
        public string GetReplyIfPlaying(string message, BotUser user, DbKeyWords dbKeyWords,DbUsers dbUsers)
        {
            if (message == "/help")
                return "You are in the game! /end - for stop the game\n" + dbKeyWords.GetByKey(message);
            if (message == "/end")
            {
                user.GameStatus = GameStatus.NotPlaying;
                dbUsers.Update(user.Id, user);
                return dbKeyWords.GetByKey(message);
            }
            if (int.TryParse(message, out var numForTryParse))
            {
                if (numForTryParse <= user.GameProps.RightBorder && numForTryParse >= user.GameProps.LeftBorder)
                {
                    if (numForTryParse == user.GameProps.ChoosedNumber)
                    {
                        user.GameStatus = GameStatus.NotPlaying;
                        dbUsers.Update(user.Id, user);
                        return $"You win, That`s {user.GameProps.ChoosedNumber}!\nTry to play again: /play";
                    }
                    if (numForTryParse > user.GameProps.ChoosedNumber)
                    {
                        return $"Try to choose lesser number from [{user.GameProps.LeftBorder.ToString()},{user.GameProps.RightBorder.ToString()}]";
                    }
                    return $"Try to choose bigger number from [{user.GameProps.LeftBorder.ToString()},{user.GameProps.RightBorder.ToString()}]";
                }
                return $"You went out of range: [{user.GameProps.LeftBorder.ToString()},{user.GameProps.RightBorder.ToString()}]\n Please, try again";
            }
            return "You are in the game! /end - for stop the game\n" + dbKeyWords.GetByKey("");
        }

        public string GetReplyIfNotPlaying(string message, BotUser user, DbKeyWords dbKeyWords, DbUsers dbUsers)
        {
            if (message == "/play")
            {
                user.GameStatus = GameStatus.ChoosingRange;
                dbUsers.Update(user.Id, user);
                return dbKeyWords.GetByKey(message);
            }
            else if (dbKeyWords.FindByKey(message) && message != "/end")
                return dbKeyWords.GetByKey(message);
            return dbKeyWords.GetByKey("");
        }

        public string GetReplyIfChoosingRange(string message, BotUser user, DbKeyWords dbKeyWords, DbUsers dbUsers)
        {
            if (message == "/end")
            {
                user.GameStatus = GameStatus.NotPlaying;
                dbUsers.Update(user.Id, user);
                return dbKeyWords.GetByKey(message);
            }
            if (message == "/help")
                return "You are in the game! /end - for stop the game\n" + dbKeyWords.GetByKey(message);

            var splitted = message.Split(' ');
            if (splitted.Length == 2 && int.TryParse(splitted[0], out var firstNum) && int.TryParse(splitted[1], out var secondtNum))
            {
                user.GameStatus = GameStatus.Playing;
                user.GameProps.LeftBorder = firstNum;
                user.GameProps.RightBorder = secondtNum;

                user.GameProps.ChoosedNumber = Random.Next(firstNum, secondtNum);
                dbUsers.Update(user.Id, user);

                return $"Nice! Your range is [{firstNum.ToString()},{secondtNum.ToString()}]\n Now try guess what number do I guess...";
            }
            return "You are in the game! /end - for stop the game \n" +
                dbKeyWords.GetByKey("/play");
        }
    }
};



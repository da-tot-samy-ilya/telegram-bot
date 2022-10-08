﻿using telegram_bot.enums;

namespace telegram_bot
{
    public class Logic
    {
        static string root = Path.GetTempPath();
        static DB dbKeyWords = new DB(root, "db", "users","json");
        
        public static string MessageProcessing(string message)
        {
            var keyWordsInMessage = message.Split(' ');
            var keyWord = "";
            foreach (var keyWordInMessage in keyWordsInMessage)
            {
                if (dbKeyWords.dbKeyWords.ContainsKey(keyWordInMessage))
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

        public static string GenerateAnswer(DB db, long userId, string userName, string message)
        {
            BotUser user = db.ReturnUserByIdAndName(userId, userName);
            
            var keyWord = MessageProcessing(message);

            if (user.gameStatus == GameStatus.NotPlaying)
                return user.gameProps.UserIsNotPlaying(keyWord, user, dbKeyWords.dbKeyWords, dbKeyWords);

            if (user.gameStatus == GameStatus.ChoosingRange)
                return user.gameProps.UserIsChoosingRange(keyWord, user, dbKeyWords.dbKeyWords, dbKeyWords);

            return user.gameProps.UserIsPlaying(keyWord, user, dbKeyWords.dbKeyWords, dbKeyWords);
        }
        
        
        /*public static string GenerateAnswer(BotUser user, string message)
        {
            var answer = "";
            int numForTryParse;
            switch (message)
            {
                case "/start":
                    answer = "Bot game 'Guess number' - guess number from range\n" +
                                "/start - начать\n" +
                                "/help - commands\n" +
                                "/play - start game (set left and right borders)";
                    break;
                case "/help":
                    answer = "Bot game 'Guess number' - guess number from range\n" +
                             "/start - начать\n" +
                             "/help - commands\n" +
                             "/play - start game (set left and right borders)";
                    break;
                case "/play":
                    user.gameStatus = GameStatus.ChoosingRange;
                    answer = "set left and right border by pattern: [leftBorder] [rightBorder]";
                    break;
                default:
                    if (user.gameStatus == GameStatus.Playing)
                    {
                        if (int.TryParse(message, out numForTryParse))
                        {
                            var inputNumber = int.Parse(message);
                            if (inputNumber <= user.gameProps.rightBorder && inputNumber >= user.gameProps.leftBorder)
                            {
                                if (inputNumber == user.gameProps.choosedNumber)
                                {
                                    user.gameStatus = GameStatus.NotPlaying;
                                    answer = $"You win, That`s {user.gameProps.choosedNumber}!\nTry to play again: /play";
                                }
                                else if (inputNumber > user.gameProps.choosedNumber)
                                {
                                    answer = $"Try to choose lesser number from [{user.gameProps.leftBorder.ToString()},{user.gameProps.rightBorder.ToString()}]";
                                }
                                else
                                {
                                    answer = $"Try to choose bigger number from [{user.gameProps.leftBorder.ToString()},{user.gameProps.rightBorder.ToString()}]";
                                }
                            }
                            else
                            {
                                answer = $"You went out of range: [{user.gameProps.leftBorder.ToString()},{user.gameProps.rightBorder.ToString()}]\n Please, try again";
                            }
                        }
                        else
                        {
                            answer = "That is not number!\nPlease, try again";

                        }
                    }
                    else if (user.gameStatus == GameStatus.ChoosingRange)
                    {
                        var splitted = message.Split(' ');
                        
                        if (splitted.Length == 2)
                        {
                            var isFirstNum = int.TryParse(splitted[0], out numForTryParse);
                            var isSecondtNum = int.TryParse(splitted[1], out numForTryParse);
                            if (isFirstNum && isSecondtNum)
                            {
                                user.gameStatus = GameStatus.Playing;
                                user.gameProps.leftBorder = int.Parse(splitted[0]);
                                user.gameProps.rightBorder = int.Parse(splitted[1]);
                                answer = $"Nice! Your range is [{user.gameProps.leftBorder.ToString()},{user.gameProps.rightBorder.ToString()}]\n Now try guess what number do I guess...";
                                var rnd = new Random();
                                user.gameProps.choosedNumber = rnd.Next(user.gameProps.leftBorder, user.gameProps.rightBorder);
                            }
                            else
                            {
                                answer = "Incorrectly specified range, use pattern: [leftBorder] [rightBorder]";
                            }
                        }
                        else
                        {
                            answer = "Incorrectly specified range, use pattern: [leftBorder] [rightBorder]";
                        }
                    }
                    else
                    {
                        answer = "Bot game 'Guess number' - guess number from range\n" +
                                 "/start - начать\n" +
                                 "/help - commands\n" +
                                 "/play - start game (set left and right borders)";
                    }
                    break;
            }

            return answer;
        }*/
    }
};



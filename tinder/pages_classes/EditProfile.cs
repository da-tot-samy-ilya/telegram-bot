using telegram_bot.bot;
using telegram_bot.bot.enums;
using telegram_bot.data_base;
using telegram_bot.tinder.enums;

namespace telegram_bot.tinder.pages_classes
{
    public class EditProfile : Page
    {
        public string[] cancelButton = new string[] { "Cancel" };
        public EditProfile(UsersDb usersDb, string text = "", string imgId = ""): base(usersDb)
        {
            this.text = text;
            rowsCount = 2;
            columnsCount = 4;

            keyboard = new Dictionary<string, string>
            {
                ["To main"] = "/main",
                ["Name"] = "/set_name",
                ["Age"] = "/set_age",
                ["Town"] = "/set_town",
                ["Description"] = "/set_description",
                ["Search purpose"] = "/set_search_purpose",
                ["Interests"] = "/set_interests",
                ["Photo"] = "/set_images", 
            };
        }
        public override Answer getAnswer(Message message, int oldMessage)
        {
            var user = message.user;
            if (message.type == BotMessageType.incorrectType)
            {
                return GenerateCurrentProfile(user, "", true, false);
            }
            switch (user.localStatus)
            {
                case UserLocalStatus.EditProfileBase:
                    return Base(user, message);
                case UserLocalStatus.EditProfileSetAge:
                    return SetAge(user, message);
                case UserLocalStatus.EditProfileSetDescription:
                    return SetDescription(user, message);
                case UserLocalStatus.EditProfileSetImages:
                    return SetImages(user, message);
                case UserLocalStatus.EditProfileSetInterests:
                    return SetInterests(user, message);
                case UserLocalStatus.EditProfileSetName:
                    return SetName(user, message);
                case UserLocalStatus.EditProfileSetTown:
                    return SetTown(user, message);
                case UserLocalStatus.EditProfileSetSearchPurpose:
                    return SetSearchPurpose(user, message);
                default:
                    return GenerateCurrentProfile(user, "", true, false);
            }
        }

        private Answer GenerateCurrentProfile(BotUser user, string sendingMessage, 
            bool isToUpdateLastMessage, bool isClickCancelButton)
        {
            var purposes = "";
            for (var i = 0; i < user.searchPurposes.Count; i++)
            {
                purposes += user.searchPurposes[i];
                if (i != user.searchPurposes.Count - 1)
                {
                    purposes += ", ";
                }
            }

            var interests = "";
            for (var i = 0; i < user.interests.Count; i++)
            {
                interests += user.interests[i];
                if (i != user.interests.Count - 1)
                {
                    interests += ", ";
                }
            }

            var answerText = sendingMessage + $"{user.firstName} {user.lastName}\n" +
                       $"{user.age} years\n" +
                       $"Town: {user.town}\n" +
                       $"{user.description}\n" +
                       $"Purposes: {purposes}\n" +
                       $"Interests: {interests}";
            if (!user.isHasPhoto)
            {
                return new Answer(isToUpdateLastMessage, true, user, 
                    BotMessageType.text, answerText, user.photoId, keyboard, rowsCount, columnsCount,
                    null, isClickCancelButton);
            }
            return new Answer(isToUpdateLastMessage, true, user, 
                BotMessageType.img, answerText, user.photoId, keyboard, rowsCount, columnsCount,
                    null, isClickCancelButton);
        }

        private Answer Base(BotUser user, Message message)
        {
            var answerText = "";
            if (message.type == BotMessageType.command) 
            {
                switch (message.text)
                {
                    case "/set_name":
                        user.localStatus = UserLocalStatus.EditProfileSetName;
                        answerText = "Input your name (separate your first and last names by space)";
                        _usersDb.Update(user.id, user);
                        break;
                    case "/set_description":
                        user.localStatus = UserLocalStatus.EditProfileSetDescription;
                        answerText = "Input your profile description";
                        _usersDb.Update(user.id, user);
                        break;
                    case "/set_interests":
                        user.localStatus = UserLocalStatus.EditProfileSetInterests;
                        answerText = "Input your interests (separate only by space)";
                        _usersDb.Update(user.id, user);
                        break;
                    case "/set_search_purpose":
                        user.localStatus = UserLocalStatus.EditProfileSetSearchPurpose;
                        answerText = "Input your search purposes (separate only by space)";
                        _usersDb.Update(user.id, user);
                        break;
                    case "/set_images":
                        user.localStatus = UserLocalStatus.EditProfileSetImages;
                        answerText = "Send only one image for your profile";
                        _usersDb.Update(user.id, user);
                        break;
                    case "/set_town":
                        user.localStatus = UserLocalStatus.EditProfileSetTown;
                        answerText = "Input your town";
                        _usersDb.Update(user.id, user);
                        break;
                    case "/set_age":
                        user.localStatus = UserLocalStatus.EditProfileSetAge;
                        answerText = "Input your age (only number)";
                        _usersDb.Update(user.id, user);
                        break;
                    default:
                        return GenerateCurrentProfile(user, "", true, false);
                }
                return new Answer(true, true, user, BotMessageType.text, answerText,
                    "", null, 0, 0, cancelButton);
            }
            return GenerateCurrentProfile(user, "", true, false);
        }
        private Answer SetAge(BotUser user, Message message)
        {
            if (message.type == BotMessageType.text && message.text == "Cancel")
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                _usersDb.Update(user.lastMessageId, user);
                return GenerateCurrentProfile(user, "", true, true);
            }

            if (message.type == BotMessageType.text && int.TryParse(message.text, out var v))
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                user.age = int.Parse(message.text);
                _usersDb.Update(user.id, user);
                var sendingMessage = "The age has been changed\n";
                return GenerateCurrentProfile(user, sendingMessage, false, true);
            }
            
            return new Answer(true, false, user,
                BotMessageType.text, "This is not number\nInput your age (only number)");
        }
        private Answer SetDescription(BotUser user, Message message)
        {
            if (message.type == BotMessageType.text && message.text == "Cancel")
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                return GenerateCurrentProfile(user, "", true, true);
            }
            if (message.type == BotMessageType.text)
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                user.description = message.text;
                _usersDb.Update(user.id, user);
                var sendingMessage = "The description has been changed\n";
                return GenerateCurrentProfile(user, sendingMessage, false, true);
            }
            return new Answer(true, false, user,
                BotMessageType.text, "This is not text\nInput your profile description");
        }
        private Answer SetImages(BotUser user, Message message)
        {
            if (message.type == BotMessageType.text && message.text == "Cancel")
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                return GenerateCurrentProfile(user, "", true, true);
            }
            if (message.type == BotMessageType.img)
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                user.photoId = message.photoId;
                user.isHasPhoto = true;
                _usersDb.Update(user.id, user);
                var sendingMessage = "The photo has been changed\n";
                return GenerateCurrentProfile(user, sendingMessage, false, true);
            }
            return new Answer(true, false, user,
                BotMessageType.text, "This is not image\nSend only one image for your profile");
        }
        private Answer SetInterests(BotUser user, Message message)
        {
            if (message.type == BotMessageType.text && message.text == "Cancel")
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                return GenerateCurrentProfile(user, "", true, true);
            }
            if (message.type == BotMessageType.text)
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                user.interests = message.text.Split().ToList();
                _usersDb.Update(user.id, user);
                var sendingMessage = "The interests has been changed\n";
                return GenerateCurrentProfile(user, sendingMessage, false, true);
            }
            return new Answer(true, false, user,
                BotMessageType.text, "This is not text\nInput your interests (separate only by space)");
        }
        private Answer SetName(BotUser user, Message message)
        {
            if (message.type == BotMessageType.text && message.text == "Cancel")
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                return GenerateCurrentProfile(user, "", true, true);
            }
            if (message.type == BotMessageType.text)
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                var nameList = message.text.Split().ToList();
                user.firstName = nameList[0];
                user.lastName = nameList.Count < 2 ? "" : nameList[1];
                
                _usersDb.Update(user.id, user);
                var sendingMessage = "The name has been changed\n";
                return GenerateCurrentProfile(user, sendingMessage, false, true);
            }
            return new Answer(true, false, user,
                BotMessageType.text, "This is not text\nInput your name (separate your first and last names by space)");
        }
        private Answer SetTown(BotUser user, Message message)
        {
            if (message.type == BotMessageType.text && message.text == "Cancel")
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                return GenerateCurrentProfile(user, "", true, true);
            }
            if (message.type == BotMessageType.text)
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                user.town = message.text;
                _usersDb.Update(user.id, user);
                var sendingMessage = "The town has been changed\n";
                return GenerateCurrentProfile(user, sendingMessage, false, true);
            }
            return new Answer(true, false,user,
                BotMessageType.text, "This is not text\nInput your town");
        }
        private Answer SetSearchPurpose(BotUser user, Message message)
        {
            if (message.type == BotMessageType.text && message.text == "Cancel")
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                
                return GenerateCurrentProfile(user, "", true, true);
            }
            if (message.type == BotMessageType.text)
            {
                user.localStatus = UserLocalStatus.EditProfileBase;
                user.searchPurposes = message.text.Split().ToList();
                _usersDb.Update(user.id, user);
                var sendingMessage = "The search purpose has been changed\n";
                return GenerateCurrentProfile(user, sendingMessage, false, true);
            }
            return new Answer(true, false, user,
                BotMessageType.text, "This is not text\nInput your search purposes (separate only by space)");
        }
    }
}

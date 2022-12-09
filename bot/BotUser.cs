using telegram_bot.tinder.enums;

namespace telegram_bot.bot
{
    public class BotUser
    {
        public long id;
        public string firstName;
        public string lastName;
        public string nickName;
        public int age;
        public string photoId;
        public string description;
        public string town;
        
        public List<long> sentLikes;
        public List<long> recieved_likes;
        public List<long> matches;

        public List<string> interests;
        public List<string> searchPurposes;

        public PagesEnum onWhichPage;
        public bool isLockedUpOnCurrPage;
        public UserLocalStatus localStatus;

        public BotUser(long userId,string userNickName, string userFirstName = "", string userPhotoId = null,
            string userLastName = "", int userAge = 14, string userDescription = "", string userTown = "")
        {
            id = userId;
            nickName = userNickName;
            firstName = userFirstName;
            lastName = userLastName;
            age = userAge;
            photoId = userPhotoId;
            description = userDescription;
            town = userTown;
            
            sentLikes = new List<long>();
            recieved_likes = new List<long>();
            matches = new List<long>();
            interests = new List<string>();
            searchPurposes = new List<string>();

            onWhichPage = PagesEnum.main;
            isLockedUpOnCurrPage = false;
            localStatus = UserLocalStatus.MainPageBase;
        }

    }
};


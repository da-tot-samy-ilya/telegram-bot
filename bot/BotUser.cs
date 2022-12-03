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
        public string discribition;

        public List<long> sentLikes;
        public List<long> recieved_likes;
        public List<long> matches;

        public List<Interests> interests;
        public List<SearchPurpose> searchPurpose;

        public PagesEnum onWhichPage;

        // TODO: add personal_setting = { search_filters {  } }
        // TODO: add geolocation 

        // TODO: сделать функцию getPage(Pages page) -> Page (типа возвращает страницу по enum`у)   
        public BotUser(long userId,string userNickName, string userFirstName = "", string userPhotoId = null,
            string userLastName = "", int userAge = 14, string userDiscribition = "")
        {
            id = userId;
            nickName = userNickName;
            firstName = userFirstName;
            lastName = userLastName;
            age = userAge;
            photoId = userPhotoId;
            discribition = userDiscribition;

            sentLikes = new List<long>();
            recieved_likes = new List<long>();
            matches = new List<long>();
            interests = new List<Interests>();
            searchPurpose = new List<SearchPurpose>();

            onWhichPage = PagesEnum.main;
        }

    }
};


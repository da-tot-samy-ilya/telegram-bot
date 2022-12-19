using telegram_bot.tinder.enums;

namespace telegram_bot.tinder.pages_classes
{
    public class Pages
    {
        private MainPage _mainPage;
        private EditProfile _editProfile;
        private JokesGenerator _jokesGenerator;

        public Pages(MainPage mainPage, EditProfile editProfile, JokesGenerator jokesGenerator)
        {
            _mainPage = mainPage;
            _editProfile = editProfile;
            _jokesGenerator = jokesGenerator;
        }

        public PagesEnum GetPageEnumByCommand(string command)
        {
            return command switch
            {
                "/main" => PagesEnum.main,
                "/edit_profile" => PagesEnum.edit_profile,
                "/jokes_generator" => PagesEnum.jokes_generator,
                /*"/sent_likes" => PagesEnum.sent_likes,
                "/show_people" => PagesEnum.show_people,
                "/recieved_likes" => PagesEnum.recieved_likes,
                "/matches" => PagesEnum.matches,
                "/settings" => PagesEnum.settings,
                "/delete_all_data" => PagesEnum.delete_all_data,
                "/set_search_filters" => PagesEnum.set_search_filters,
                "/notifications" => PagesEnum.notifications,*/
                _ => PagesEnum.not_page
            };
        }
        public Page GetPageByEnum(PagesEnum pagesEnum) {
            return pagesEnum switch
            {
                PagesEnum.main => _mainPage,
                PagesEnum.edit_profile => _editProfile,
                PagesEnum.jokes_generator => _jokesGenerator,
            };
        }

        public UserLocalStatus GetUserLocalStatusEnumByEnum(PagesEnum pagesEnum)
        {
            return pagesEnum switch
            {
                PagesEnum.main => UserLocalStatus.MainPageBase,
                PagesEnum.edit_profile => UserLocalStatus.EditProfileBase,
                PagesEnum.jokes_generator => UserLocalStatus.JokesGeneratorBase,
            };
        }
    }
}
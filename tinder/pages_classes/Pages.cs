namespace telegram_bot.tinder.pages_classes
{
    public class Pages
    {
        public MainPage _mainPage;
        public EditProfile _editProfile;

        public Pages()
        {
            _mainPage = new MainPage();
            _editProfile = new EditProfile();
            // TODO: добавить страницы в конструктор
        }
    }
}
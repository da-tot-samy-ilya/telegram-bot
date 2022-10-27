using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace telegram_bot
{
    public class MainPage
    {
        public InlineKeyboardMarkup inlineKeyboard;
        public MainPage()
        {
            inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Показать людей", callbackData: "/show_people"),
                    InlineKeyboardButton.WithCallbackData(text: "Мэтчи", callbackData: "/matches"),
                },

                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Отправленные лайки", callbackData: "/sent_likes"),
                    InlineKeyboardButton.WithCallbackData(text: "Полученный лайки", callbackData: "/received_likes"),
                },

                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "Настройки", callbackData: "/settings"),
                    InlineKeyboardButton.WithCallbackData(text: "Редактировать профиль", callbackData: "/edit_profile"),
                },

            });

        }
    }
}

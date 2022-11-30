﻿using telegram_bot.bot;

namespace telegram_bot.tinder.pages_classes
{
    public class MainPage : Page
    {
        public MainPage(string text = "", string imgId = "")
        {
            this.text = text;
            this.imgId = imgId;
            row = 3;
            column = 2;

            keyboard = new Dictionary<string, string>
            {
                ["Показать людей"] = "/show_people",
                ["Мэтчи"] = "/matches",
                ["Отправленные лайки"] = "/sent_likes",
                ["Полученный лайки"] = "/received_likes",
                ["Настройки"] = "/settings",
                ["Редактировать профиль"] = "/edit_profile",
            };
        }

        /*public override int row { get => row  ;
                set => this.row; }

        public override int column { get => column;
            set => throw new NotSupportedException(); }*/

        public override Answer getAnswer()
        {
            throw new NotSupportedException();
        }
    }

}

/*inlineKeyboard = new InlineKeyboardMarkup(new[]
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

            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Редактировать профиль", callbackData: "/edit_profile"),
            }

        });*/

/*public override Request getAnswer( bool refreshThePage, BotUser user, Request message, int messageId ) =>
           new Request(messageId, MessageType.text, text, "", refreshThePage);*/
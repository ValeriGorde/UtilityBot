using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public class TextController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public TextController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }


        public async Task Handle(Message message, CancellationToken ct)
        {
            string countType = _memoryStorage.GetSession(message.Chat.Id).LanguageCode; // Здесь получим язык из сессии пользователя

            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Подсчёт символов" , $"sim"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел" , $"sum")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот подсчитывает количество символов или рассчитвает сумму чисел.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}В зависимости от того какой режим вы выберите.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
            }

            Calculation calculation = new Calculation();
            int sum = calculation.Start(countType, message);

            if (countType == "sim")
            {
                await _telegramClient.SendTextMessageAsync(message.From.Id, $"Длина сообщения: {sum} знаков", cancellationToken: ct);

            }
            else if (countType == "sum") 
            {
                await _telegramClient.SendTextMessageAsync(message.From.Id, $"Сумма введённых чисел: {sum}", cancellationToken: ct);
            }
        }
    }
}


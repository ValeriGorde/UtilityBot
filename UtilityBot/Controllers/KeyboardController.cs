using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    internal class KeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        public KeyboardController(ITelegramBotClient telegramClient, IStorage memoryStorage)
        {
            _telegramClient = telegramClient;
            _memoryStorage = memoryStorage;
        }
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            // Генерим информационное сообщение
            string countType = callbackQuery.Data switch
            {
                "sim" => " Подсчёт символов",
                "sum" => " Сумма чисел",
                _ => String.Empty
            };

            switch (callbackQuery?.Data) 
            {
                case "sim":
                    await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, "Напишите сообщение, чтобы мы подсчитали в нём количество символов!", cancellationToken: ct);
                    break;
                case "sum":
                    await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, "Напишите числа через пробел и мы подсчитаем их сумму!", cancellationToken: ct);
                    break;

            }
        }
    }
}

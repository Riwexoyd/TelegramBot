using AspNetCoreTelegramBot.Models;
using AspNetCoreTelegramBot.Services;
using AspNetCoreTelegramBot.TextHandlers;

using Moq;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTelegramBot.Unit.Tests.Services
{
    /// <summary>
    /// Тесты на сервис обработки текстовых сообщений
    /// </summary>
    [TestFixture]
    public class TextHandlerServiceTests
    {
        /// <summary>
        /// Тестовое сообщение
        /// </summary>
        private const string TestText = "test";

        /// <summary>
        /// Пользователь
        /// </summary>
        private User user;

        /// <summary>
        /// Чат
        /// </summary>
        private Chat chat;

        /// <summary>
        /// Обработчик текстовых сообщений, возвращающий true, т.е. обработавший сообщение
        /// </summary>
        private Mock<ITextHandler> trueHandler;

        /// <summary>
        /// Обработчик текстовых сообщений, возвращающий false при обработке сообщения
        /// </summary>
        private Mock<ITextHandler> falseHandler;

        /// <summary>
        /// Инициализация перед каждым тестом
        /// </summary>
        [SetUp]
        public void Init()
        {
            user = new User();
            chat = new Chat();

            trueHandler = new Mock<ITextHandler>();
            trueHandler.Setup(i => i.HandleAsync(user, chat, TestText))
                .Returns(Task.FromResult(true));

            falseHandler = new Mock<ITextHandler>();
            falseHandler.Setup(i => i.HandleAsync(user, chat, TestText))
                .Returns(Task.FromResult(false));
        }

        /// <summary>
        /// Если текст будет null, то должно возникать исключение
        /// </summary>
        [Test]
        public void WithNullTextShouldThrowException()
        {
            var textHandlerService = new TextHandlerService(new List<ITextHandler>());
            Assert.ThrowsAsync<ArgumentNullException>(() => textHandlerService.HandleTextAsync(user, chat, null));
        }

        /// <summary>
        /// Если пустой текст, то должно возникать исключение
        /// </summary>
        [Test]
        public void WithEmptyTextShouldThrowException()
        {
            var textHandlerService = new TextHandlerService(new List<ITextHandler>());
            Assert.ThrowsAsync<ArgumentNullException>(() => textHandlerService.HandleTextAsync(user, chat, ""));
        }

        /// <summary>
        /// Если отправитель пустой, то должно возникать исключение
        /// </summary>
        [Test]
        public void WithNullUserShouldThrowException()
        {
            var textHandlerService = new TextHandlerService(new List<ITextHandler>());
            Assert.ThrowsAsync<ArgumentNullException>(() => textHandlerService.HandleTextAsync(null, chat, TestText));
        }

        /// <summary>
        /// Если чат пустой, то должно возникать исключение
        /// </summary>
        [Test]
        public void WithNullChatShouldThrowException()
        {
            var textHandlerService = new TextHandlerService(new List<ITextHandler>());
            Assert.ThrowsAsync<ArgumentNullException>(() => textHandlerService.HandleTextAsync(user, null, TestText));
        }

        /// <summary>
        /// При обработке текста через сервис должен вызываться мокнутый обработчик
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task ShouldCallHandleAsync()
        {
            var textHandlerService = new TextHandlerService(new List<ITextHandler>
            {
                trueHandler.Object
            });

            await textHandlerService.HandleTextAsync(user, chat, TestText);

            trueHandler.Verify(i => i.HandleAsync(user, chat, TestText));
        }
    }
}
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Persistence.Repositories;
using System.Threading;
using Microsoft.Bot.Schema;
using MyShift.Notifications.Helpers.Cards;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Builder;

namespace MyShift.Notifications.Helpers.Notification
{
    public class NotificationDataHelper : INotificationDataHelper
    {
        private readonly IBotFrameworkHttpAdapter _adapter;
        private readonly string _appId;
        private readonly IBot _bot;
        private readonly ILogsRepository _logsRepository;
        public NotificationDataHelper(IBot bot, IBotFrameworkHttpAdapter adapter, IConfiguration configuration, ILogsRepository logsRepository)
        {
            _bot = bot;
            _adapter = adapter;
            _appId = configuration["MicrosoftAppId"] ?? string.Empty;
            _logsRepository = logsRepository;
        }
        public async Task<bool> SendNotification(ConversationReference conversationReference)
        {   
            await ((BotAdapter)_adapter).ContinueConversationAsync(_appId, conversationReference, BotCallback, default(CancellationToken));

            return true;
        }

        private async Task BotCallback( ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var attachment = MessageFactory.Attachment(UpcomingShiftCard.GetCard());
            await turnContext.SendActivityAsync(attachment);
        }
    }
}

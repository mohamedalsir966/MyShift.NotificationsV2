using AdaptiveCards;
using MyShift.Notifications.Entitys;
using MyShift.Notifications.Helpers.Notification;
using Persistence.Repositories;
using System.Threading.Tasks;

namespace MyShift.Notifications.Service
{
    public class ServiceEng : IService
    {
        private readonly ILogsRepository _logsRepository;
        private readonly INotificationDataHelper _notificationDataHelper;
        public ServiceEng( ILogsRepository logsRepository , INotificationDataHelper notificationDataHelper)
        {
            _logsRepository = logsRepository;
            _notificationDataHelper = notificationDataHelper;
        }
        public async Task GetDataToNotifiy()
        {
            var dataTobeNotifyed = await _logsRepository.GetLogsQueries();

            foreach (var item in dataTobeNotifyed)
            {
                //we need to get it by the user how ??
                var conversationReference = await _logsRepository.GetConversationReference();
                //her we just need to send it should i reseve any  data back??

                var result = await _notificationDataHelper.SendNotification(conversationReference);
            }

           // var updateData = await _logsRepository.UpdateLogsCommand(dataTobeNotifyed);
           
          
        }

       
    }
}

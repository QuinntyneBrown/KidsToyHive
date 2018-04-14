using Core.Entities;

namespace ChatService.Features.Messages
{
    public class MessageApiModel
    {        
        public int MessageId { get; set; }        

        public static MessageApiModel FromMessage(Message message)
        {
            var model = new MessageApiModel();
            model.MessageId = message.MessageId;            
            return model;
        }
    }
}

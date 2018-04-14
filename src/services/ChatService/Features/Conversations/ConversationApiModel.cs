using Core.Entities;

namespace ChatService.Features.Conversations
{
    public class ConversationApiModel
    {        
        public int ConversationId { get; set; }
        public string Name { get; set; }

        public static ConversationApiModel FromConversation(Conversation conversation)
        {
            var model = new ConversationApiModel();
            model.ConversationId = conversation.ConversationId;            
            return model;
        }
    }
}

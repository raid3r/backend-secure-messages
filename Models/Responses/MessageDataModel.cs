using RequestMessageDataModel = SecureMessages.Models.Requests.MessageDataModel ;

namespace SecureMessages.Models.Responses;

public class MessageDataModel: RequestMessageDataModel
{
    public bool Found { get; set; }
}

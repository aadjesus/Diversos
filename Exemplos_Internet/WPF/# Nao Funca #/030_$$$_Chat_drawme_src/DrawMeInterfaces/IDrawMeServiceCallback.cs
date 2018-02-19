using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrawMeObjects;
using System.ServiceModel;

namespace DrawMeInterfaces
{
    public interface IDrawMeServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateUsersList(List<ChatUser> listChatUsers);

        [OperationContract(IsOneWay = true)]
        void OnInkStrokesUpdate(ChatUser chatUser, byte[] bytesStroke);

        [OperationContract(IsOneWay = true)]
        void ServerDisconnected();
    }
}

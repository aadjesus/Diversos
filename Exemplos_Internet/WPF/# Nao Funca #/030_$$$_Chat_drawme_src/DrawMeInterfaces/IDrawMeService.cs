using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DrawMeObjects;
using System.IO;

namespace DrawMeInterfaces
{
    [
       ServiceContract
       (
           Name = "DrawMeService",
           Namespace = "http://DrawMe/DrawMeService/",
           SessionMode = SessionMode.Required,
           CallbackContract = typeof(IDrawMeServiceCallback)
       )
    ]
    public interface IDrawMeService
    {
        [OperationContract()]
        bool Join(ChatUser chatUser);

        [OperationContract()]
        void Leave(ChatUser chatUser);

        [OperationContract()]
        bool IsUserNameTaken(string strUserName);

        [OperationContract()]
        void SendInkStrokes(MemoryStream memoryStream);
    }
}

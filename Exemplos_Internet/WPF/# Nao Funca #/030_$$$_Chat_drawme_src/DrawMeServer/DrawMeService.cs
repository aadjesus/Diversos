using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using DrawMeObjects;
using System.IO;
using DrawMeInterfaces;

namespace DrawMeServer
{
    [
        ServiceBehavior
        (
            ConcurrencyMode = ConcurrencyMode.Single, 
            InstanceContextMode = InstanceContextMode.Single
        )
    ]
    public class DrawMeService : IDrawMeService
    {
        public static Dictionary<IDrawMeServiceCallback, ChatUser> s_dictCallbackToUser = new Dictionary<IDrawMeServiceCallback, ChatUser>();
       
        public DrawMeService()
        { 
        }

        public bool Join(ChatUser chatUser)
        {
            IDrawMeServiceCallback client = OperationContext.Current.GetCallbackChannel<IDrawMeServiceCallback>();

            if (s_dictCallbackToUser.ContainsValue(chatUser) == false)
            {
                s_dictCallbackToUser.Add(client, chatUser);
            }

            foreach (IDrawMeServiceCallback callbackClient in s_dictCallbackToUser.Keys)
            {
                callbackClient.UpdateUsersList(s_dictCallbackToUser.Values.ToList());
            }

            return true;
        }

        public void Leave(ChatUser chatUser)
        {
            IDrawMeServiceCallback client = OperationContext.Current.GetCallbackChannel<IDrawMeServiceCallback>();
            if (s_dictCallbackToUser.ContainsKey(client))
            {
                s_dictCallbackToUser.Remove(client);
            }

            foreach (IDrawMeServiceCallback callbackClient in s_dictCallbackToUser.Keys)
            {
                if (chatUser.IsServer)
                {
                    if (callbackClient != client)
                    {
                        //server user logout, disconnect clients
                        callbackClient.ServerDisconnected();
                    }
                }
                else
                {
                    //normal user logout
                    callbackClient.UpdateUsersList(s_dictCallbackToUser.Values.ToList());
                }
            }

            if (chatUser.IsServer)
            {
                s_dictCallbackToUser.Clear();
            }
        }

        public bool IsUserNameTaken(string strNickName)
        {
            foreach (ChatUser chatUser in s_dictCallbackToUser.Values)
            {
                if (chatUser.NickName.ToUpper().CompareTo(strNickName) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void SendInkStrokes(MemoryStream memoryStream)
        {
            IDrawMeServiceCallback client = OperationContext.Current.GetCallbackChannel<IDrawMeServiceCallback>();
            
            foreach (IDrawMeServiceCallback callbackClient in s_dictCallbackToUser.Keys)
            {
                if (callbackClient != OperationContext.Current.GetCallbackChannel<IDrawMeServiceCallback>())
                {
                    callbackClient.OnInkStrokesUpdate(s_dictCallbackToUser[client], memoryStream.GetBuffer());
                }
            }
        }

    }
}

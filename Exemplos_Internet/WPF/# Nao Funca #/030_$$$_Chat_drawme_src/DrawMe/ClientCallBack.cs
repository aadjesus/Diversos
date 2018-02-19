using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Threading;
using System.IO;
using DrawMeInterfaces;

namespace DrawMe
{
    [
        CallbackBehavior
        (
            ConcurrencyMode = ConcurrencyMode.Single,
            UseSynchronizationContext = false
        )
    ]
    public class ClientCallBack : IDrawMeServiceCallback
    {
        public static ClientCallBack Instance;

        private SynchronizationContext m_uiSyncContext = null;
        private DrawMeWindow m_mainWindow;
        public ClientCallBack(SynchronizationContext uiSyncContext, DrawMeWindow mainWindow)
        {
            m_uiSyncContext = uiSyncContext;
            m_mainWindow = mainWindow;
        }

        public void OnInkStrokesUpdate(DrawMeObjects.ChatUser chatUser, byte[] bytesStroke)
        {
            SendOrPostCallback callback =
                      delegate(object state)
                      {
                          m_mainWindow.OnInkStrokesUpdate(state as byte[] );
                      };

            m_uiSyncContext.Post(callback, bytesStroke);

            SendOrPostCallback callback2 =
                      delegate(object objchatUser)
                      {
                          m_mainWindow.LastUserDraw(objchatUser as DrawMeObjects.ChatUser);
                      };
            m_uiSyncContext.Post(callback2, chatUser);
        }

        public void UpdateUsersList(List<DrawMeObjects.ChatUser> listChatUsers)
        {
            SendOrPostCallback callback =
                     delegate(object objListChatUsers)
                     {
                         m_mainWindow.UpdateUsersList(objListChatUsers as List<DrawMeObjects.ChatUser>);
                     };

            m_uiSyncContext.Post(callback, listChatUsers);
        }

        public void ServerDisconnected()
        {
            SendOrPostCallback callback =
                        delegate(object dummy)
                        {
                            m_mainWindow.ServerDisconnected();
                        };

            m_uiSyncContext.Post(callback, null);
        }
    }
}

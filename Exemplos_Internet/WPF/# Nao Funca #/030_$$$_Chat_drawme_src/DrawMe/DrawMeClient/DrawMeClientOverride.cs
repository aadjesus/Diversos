using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DrawMeObjects;

namespace DrawMe
{
    partial class DrawMeServiceClient
    {
        public static DrawMeServiceClient Instance = null;

        public DrawMeServiceClient
        (
            ChatUser chatUser,
            System.ServiceModel.InstanceContext callbackInstance, 
            string strEndPointConfigurationName, 
            System.ServiceModel.EndpointAddress remoteAddress
        ) :
            base(callbackInstance, strEndPointConfigurationName, remoteAddress)
        {
            m_chatUser = chatUser;
        }
    
        ChatUser m_chatUser;
        public ChatUser ChatUser
        {
            get { return m_chatUser; }
        }

        public bool Join()
        {
            if (m_chatUser == null)
            {
                return false;
            }
            return Join(m_chatUser);
        }

        public void Leave()
        {
            if (m_chatUser == null)
            {
                return;
            }
            Leave(m_chatUser);
        }
    }
}

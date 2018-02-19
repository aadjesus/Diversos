using DrawMeInterfaces;
namespace DrawMe
{
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class DrawMeServiceClient : System.ServiceModel.DuplexClientBase<IDrawMeService>, IDrawMeService
    {

        public DrawMeServiceClient(System.ServiceModel.InstanceContext callbackInstance) :
            base(callbackInstance)
        {
        }

        public DrawMeServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
            base(callbackInstance, endpointConfigurationName)
        {
        }

        public DrawMeServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public DrawMeServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public DrawMeServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(callbackInstance, binding, remoteAddress)
        {
        }

        public bool Join(DrawMeObjects.ChatUser chatUser)
        {
            return base.Channel.Join(chatUser);
        }

        public void Leave(DrawMeObjects.ChatUser chatUser)
        {
            base.Channel.Leave(chatUser);
        }

        public bool IsUserNameTaken(string strUserName)
        {
            return base.Channel.IsUserNameTaken(strUserName);
        }

        public void SendInkStrokes(System.IO.MemoryStream memoryStream)
        {
            base.Channel.SendInkStrokes(memoryStream);
        }
    }

}


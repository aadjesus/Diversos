using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Runtime.Serialization;



namespace WebClientByManual
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Channel of type IMath(Implemented Contract)
            // Multiple channels can also be created. Need to Just specify the A & B for it
            // A --> Address
            // B --> Binding Type
            // C --> Channel (This is the type of contract that you have created with the service)
            // -------------------
            // |  C  |  B  |  A  |
            // -------------------
            IMath MathChannel = new ChannelFactory<IMath>
                                        (
                                        new BasicHttpBinding(),
                                        new EndpointAddress("http://localhost:8081/MathService")
                                        ).CreateChannel();

            IEnglish EnglishChannel = new ChannelFactory<IEnglish>
                                        (
                                        new BasicHttpBinding(),
                                        new EndpointAddress("http://localhost:8081/EnglishService")
                                        ).CreateChannel();

            // Call the Exposed methods through the Channel
            double sum = MathChannel.Add(23, 44);
            Console.WriteLine("Call via BasicHttpBinding: {0}", sum);

            string plural = EnglishChannel.Plural("Ball");
            Console.WriteLine("Call via BasicHttpBinding: {0}", plural);

            Console.ReadKey(true);

            // Close the channels that were used to read...
            ((IChannel)MathChannel).Close();
            ((IChannel)EnglishChannel).Close();
            //((IChannel)SessionStateChannel).Close();
        }
    }

    // Interface Required to access the Contract in the Service
    [ServiceContract()]
    public interface IMath
    {
        [OperationContract]
        double Add(double x, double y);
        [OperationContract]
        double Subtract(double x, double y);
        [OperationContract]
        double Multiply(double x, double y);
        [OperationContract]
        double Divide(double x, double y);
    }

    [ServiceContract()]
    public interface IEnglish
    {
        [OperationContract]
        string Plural(string x);
    }
}

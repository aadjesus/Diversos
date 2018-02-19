using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

/*

    HOW TO HOST THE WCF SERVICE IN THIS LIBRARY IN ANOTHER PROJECT
    You will need to do the following things: 
    1)    Add a Host project to your solution
        a.    Right click on your solution
        b.    Select Add
        c.    Select New Project
        d.    Choose an appropriate Host project type (e.g. Console Application)
    2)    Add a new source file to your Host project
        a.    Right click on your Host project
        b.    Select Add
        c.    Select New Item
        d.    Select "Code File"
    3)    Paste the contents of the "MyServiceHost" class below into the new Code File
    4)    Add an "Application Configuration File" to your Host project
        a.    Right click on your Host project
        b.    Select Add
        c.    Select New Item
        d.    Select "Application Configuration File"
    5)    Paste the contents of the App.Config below that defines your service endoints into the new Config File
    6)    Add the code that will host, start and stop the service
        a.    Call MyServiceHost.StartService() to start the service and MyServiceHost.EndService() to end the service
    7)    Add a Reference to System.ServiceModel.dll
        a.    Right click on your Host Project
        b.    Select "Add Reference"
        c.    Select "System.ServiceModel.dll"
    8)    Add a Reference from your Host project to your Service Library project
        a.    Right click on your Host Project
        b.    Select "Add Reference"
        c.    Select the "Projects" tab
    9)    Set the Host project as the "StartUp" project for the solution
        a.    Right click on your Host Project
        b.    Select "Set as StartUp Project"

    ################# START MyServiceHost.cs #################

    using System;
    using System.ServiceModel;

    // A WCF service consists of a contract (defined below), 
    // a class which implements that interface, and configuration 
    // entries that specify behaviors and endpoints associated with 
    // that implementation (see <system.serviceModel> in your application
    // configuration file).

    internal class MyServiceHost
    {
        internal static ServiceHost myServiceHost = null;

        internal static void StartService()
        {
            //Consider putting the baseAddress in the configuration system
            //and getting it here with AppSettings
            Uri baseAddress = new Uri("http://localhost:8080/service1");

            //Instantiate new ServiceHost 
            myServiceHost = new ServiceHost(typeof(WCFServiceLibrary2.service1), baseAddress);

            //Open myServiceHost
            myServiceHost.Open();
        }

        internal static void StopService()
        {
            //Call StopService from your shutdown logic (i.e. dispose method)
            if (myServiceHost.State != CommunicationState.Closed)
                myServiceHost.Close();
        }
    }

    ################# END MyServiceHost.cs #################
    ################# START App.config or Web.config #################

    <system.serviceModel>
    <services>
         <service name="WCFServiceLibrary2.service1">
           <endpoint contract="WCFServiceLibrary2.IService1" binding="wsHttpBinding"/>
         </service>
       </services>
    </system.serviceModel>

    ################# END App.config or Web.config #################

*/
namespace WCFServiceLibrary2
{
    // You have created a class library to define and implement your WCF service.
    // You will need to add a reference to this library from another project and add 
    // the code to that project to host the service as described below.  Another way
    // to create and host a WCF service is by using the Add New Item, WCF Service 
    // template within an existing project such as a Console Application or a Windows 
    // Application.

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
        [OperationContract]
        TestClass ChangeTestClassValues(TestClass ts);
        [OperationContract]
        TestClassMessageContract ChangeMessageContractClassValues(TestClassMessageContract ts);
    }


    [ServiceContract()]
    public interface IEnglish
    {
        [OperationContract]
        string Plural(string x);
    }

    
    public class MathService : IMath, IEnglish
    {
        public double Add(double x, double y) { return x + y; }      
        public double Subtract(double x, double y) { return x - y; }      
        public double Multiply(double x, double y) { return x * y; }      
        public double Divide(double x, double y) { return x / y; }
        public TestClass ChangeTestClassValues(TestClass ts)
        {
            ts.FirstName = "Radhakrishnan";
            ts.LastName = "Ramachandran";
            return ts;
        }
        public TestClassMessageContract ChangeMessageContractClassValues(TestClassMessageContract ts)
        {
            ts.FirstName = "Radhakrishnan Message Contract";
            ts.LastName = "Ramachandran Message Contract";
            return ts;
        }
        public string Plural(string x)
        {
            return x + "s";
        }
    }


    [DataContract]
    public class TestClass
    {
        string firstName;
        string lastName;

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }


    [MessageContract]
    public class TestClassMessageContract
    {
        [MessageBodyMember]
        string firstName;
        [MessageBodyMember]
        string lastName;

        [MessageBodyMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [MessageBodyMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
    }
}

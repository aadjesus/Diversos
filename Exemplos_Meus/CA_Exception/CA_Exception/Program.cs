using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace CA_Exception
{
    class Program
    {
        static void Main(string[] args)
        {
            int int_ini = -2147483648;
            int int_fim = 2147483647;

            Int32 int32_ini = -2147483648;
            Int32 int32_fim = 2147483648;

            Int64 int64_ini = -2147483648;
            Int64 int64_fim = 2147483648;

            if (int_ini == 0 || int_fim == 0 || int32_ini == 0 || int32_fim == 0 || int64_ini == 0 || int64_fim == 0)
            {

            }
            //try
            //{
            //    // This code forces a division by 0 and catches the 
            //    // resulting exception.
            //    try
            //    {
            //        int zero = 0;
            //        int ecks = 1 / zero;
            //    }
            //    catch (Exception ex)
            //    {
            //        // Create a new exception to throw again.
            //        SecondLevelException newExcept =
            //            new SecondLevelException(
            //                "Forced a division by 0 and threw " +
            //                "another exception.", ex);

            //        SecondLevelException newExcept1 = new SecondLevelException(ex);
            //        if (newExcept1 == null)
            //        {

            //        }

            //        Console.WriteLine(
            //            "Forced a division by 0, caught the " +
            //            "resulting exception, \n" +
            //            "and created a derived exception:\n");
            //        Console.WriteLine("HelpLink: {0}",
            //            newExcept.HelpLink);
            //        Console.WriteLine("Source:   {0}",
            //            newExcept.Source);

            //        // This FileStream is used for the serialization.
            //        FileStream stream =
            //            new FileStream("NewException.dat",
            //                FileMode.Create);

            //        try
            //        {
            //            //// Serialize the derived exception.
            //            SoapFormatter formatter =
            //                new SoapFormatter(null,
            //                    new StreamingContext(
            //                        StreamingContextStates.File));
            //            formatter.Serialize(stream, newExcept);

            //            // Rewind the stream and deserialize the 
            //            // exception.
            //            stream.Position = 0;
            //            SecondLevelException deserExcept =
            //                (SecondLevelException)
            //                    formatter.Deserialize(stream);

            //            Console.WriteLine(
            //                "\nSerialized the exception, and then " +
            //                "deserialized the resulting stream " +
            //                "into a \nnew exception. " +
            //                "The deserialization changed the case " +
            //                "of certain properties:\n");

            //            // Throw the deserialized exception again.
            //            //throw deserExcept;
            //        }
            //        catch (SerializationException se)
            //        {
            //            Console.WriteLine("Failed to serialize: {0}",
            //                se.ToString());
            //        }
            //        finally
            //        {
            //            stream.Close();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("HelpLink: {0}", ex.HelpLink);
            //    Console.WriteLine("Source:   {0}", ex.Source);

            //    Console.WriteLine();
            //    Console.WriteLine(ex.ToString());
            //}
        }
    }

    [Serializable()]
    class SecondLevelException : Exception, ISerializable
    {
        public SecondLevelException(Exception inner)
            : base(String.Format("{0} - {1}", "XXX", inner.Message))
        {
            HelpLink = "xxxxxxxxxxxxxxxx";
        }

        // This public constructor is used by class instantiators.
        public SecondLevelException(string message, Exception inner) :
            base(String.Format("{0} - {1}", "XXX", message), inner)
        {
            HelpLink = "http://MSDN.Microsoft.com";
            Source = "Exception_Class_Samples";
        }

        // This protected constructor is used for deserialization.
        protected SecondLevelException(SerializationInfo info,
            StreamingContext context) :
            base(info, context)
        { }

        // GetObjectData performs a custom serialization.
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info,
            StreamingContext context)
        {
            // Change the case of two properties, and then use the 
            // method of the base class.
            HelpLink = HelpLink.ToLower();
            Source = Source.ToUpper();

            base.GetObjectData(info, context);
        }
    }

}

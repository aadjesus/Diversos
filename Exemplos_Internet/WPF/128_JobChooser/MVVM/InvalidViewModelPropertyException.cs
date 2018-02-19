using System;
using System.Runtime.Serialization;

namespace MVVM
{
    /// <summary>Thrown when a property is changed that is not present in a View-Model class.</summary>
    [Serializable]
    public class InvalidViewModelPropertyException : Exception
    {
        public InvalidViewModelPropertyException()
            : base()
        { ;}

        public InvalidViewModelPropertyException(String message)
            : base(message)
        { ;}

        public InvalidViewModelPropertyException(String message, Exception theException)
            : base(message, theException)
        { ;}

        protected InvalidViewModelPropertyException(SerializationInfo theSerializationInfo, StreamingContext theStreamingContext)
            : base(theSerializationInfo, theStreamingContext)
        { ;}
    }
}
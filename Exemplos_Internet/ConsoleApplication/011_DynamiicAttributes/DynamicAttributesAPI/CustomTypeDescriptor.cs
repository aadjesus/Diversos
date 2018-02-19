using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DynamicAttributes
{
    /// <summary>
    /// A generic custom type descriptor for the specified type
    /// </summary>
    public sealed class CustomTypeDescriptionProvider<T> : TypeDescriptionProvider where T : ISecurable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomTypeDescriptionProvider(TypeDescriptionProvider parent)
            : base(parent)
        {
        }

        /// <summary>
        /// Create and return a custom type descriptor and chains it with the original
        /// custom type descriptor
        /// </summary>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new SecuredAttributeCustomTypeDescriptor<T>(base.GetTypeDescriptor(objectType, instance));
        }
    }


    /// <summary>
    /// A custom type descriptor which attaches a <see cref="SecuredAttribute"/> to 
    /// an instance of a type which implements <see cref="ISecurable"/>
    /// </summary>
    public sealed class SecuredAttributeCustomTypeDescriptor<T> : CustomTypeDescriptor where T : ISecurable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SecuredAttributeCustomTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
        }

        public override AttributeCollection GetAttributes()
        {
            Type securableType = typeof(T).GetInterface(typeof(ISecurable).Name);
            if (securableType != null)
            {
                ISecurable securableInstance = GetPropertyOwner(base.GetProperties().Cast<PropertyDescriptor>().First()) as ISecurable;
                string[] instanceLevelRoles = securableInstance.GetRoles();
                List<Attribute> attributes = new List<Attribute>(base.GetAttributes().Cast<Attribute>());
                SecuredAttribute securedAttrib = new SecuredAttribute(instanceLevelRoles);
                TypeDescriptor.AddAttributes(securableInstance, securedAttrib);
                attributes.Add(securedAttrib);
                return new AttributeCollection(attributes.ToArray());
            }
            return base.GetAttributes();

        }
        /// <summary>
        /// This method add a new property to the original collection
        /// </summary>
        public override PropertyDescriptorCollection GetProperties()
        {
            // Enumerate the original set of properties and create our new set with it
            PropertyDescriptorCollection originalProperties = base.GetProperties();
            List<PropertyDescriptor> newProperties = new List<PropertyDescriptor>();
            Type securableType = typeof(T).GetInterface("ISecurable");
            if (securableType != null)
            {
                foreach (PropertyDescriptor pd in originalProperties)
                {
                    ISecurable securableInstance = GetPropertyOwner(pd) as ISecurable;
                    string[] propertyRoles = securableInstance.GetRoles(pd.Name);
                    SecuredAttribute securedAttrib = new SecuredAttribute(propertyRoles);
                    // Create a new property and add it to the collection
                    PropertyDescriptor newProperty = TypeDescriptor.CreateProperty(typeof(T), pd.Name, pd.PropertyType, securedAttrib);
                    newProperties.Add(newProperty);
                }
                // Finally return the list
                return new PropertyDescriptorCollection(newProperties.ToArray(), true);
            }
            return base.GetProperties();
        }
    }

}

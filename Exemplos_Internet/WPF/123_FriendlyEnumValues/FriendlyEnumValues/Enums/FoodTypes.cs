using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using FriendlyEnumValues.Resources;

namespace FriendlyEnumValues
{
    public enum FoodTypes : int
    {
        [LocalizableDescription(@"Pizza", typeof(Resource))]
        Pizza = 1,

        [LocalizableDescription(@"Burger", typeof(Resource))]
        Burger = 2,

        [LocalizableDescription(@"SpagBol", typeof(Resource))]
        SpagBol = 3
    }
}

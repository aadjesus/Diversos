using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FriendlyEnumValues
{
    public class TestViewModel
    {
        private TestClass testclass = new TestClass { FoodType = FoodTypes.Burger };

        public TestClass TestableClass
        {
            get { return this.testclass; }
        }


    }
}

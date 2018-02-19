using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserControlAsDataTemplateDemo
{
    public class DataItem
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public DataItem(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
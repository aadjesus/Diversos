using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserControlAsDataTemplateDemo
{
    /// <summary>
    /// A simple class holding a few items of data in various formats
    /// </summary>
    public sealed class DataItem
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Progress { get; set; }

        /// <summary>
        /// A parameterised constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="progress"></param>
        public DataItem(string name, int age, double progress)
        {
            Name = name;
            Age = age;
            Progress = progress;
        }
    }
}
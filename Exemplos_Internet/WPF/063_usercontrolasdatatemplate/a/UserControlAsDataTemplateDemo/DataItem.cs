using System;

namespace UserControlAsDataTemplateDemo
{
    /// <summary>
    /// A simple class holding a few items of data in various formats
    /// </summary>
    public sealed class DataItem
    {

        #region Properties

        /// <summary>
        /// The name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The age property
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The progress property
        /// </summary>
        public double Progress { get; set; }

        #endregion

        #region Constructor

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

        #endregion

    }
}
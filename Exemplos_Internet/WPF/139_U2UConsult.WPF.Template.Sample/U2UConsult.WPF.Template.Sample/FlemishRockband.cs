namespace U2UConsult.WPF.Template.Sample
{
    using System.Collections.Generic;

    /// <summary>
    /// Just a sample class.
    /// </summary>
    public class FlemishRockband
    {
        /// <summary>
        /// Gets or sets the name of the rock band.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ToString override, often used in bindings.
        /// </summary>
        /// <returns>My name.</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Returns the best three bands.
        /// </summary>
        /// <returns>As I said: the best three bands.</returns>
        public static List<FlemishRockband> Top3()
        {
            return new List<FlemishRockband>()
                {
                    new FlemishRockband() { Name = "dEUS" },
                    new FlemishRockband() { Name = "The Van Jets" },
                    new FlemishRockband() { Name = "CPeX" }
                };
        }
    }
}

namespace VMCommanding.Model
{
    /// <summary>
    /// Stores attributes of a person.
    /// </summary>
    public class Person
    {
        public static Person[] GetPeople()
        {
            return new Person[]
            {
                new Person("Barney", 30),
                new Person("Fred", 32),
                new Person("Wilma", 28)
            };
        }

        private Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
            this.IsAlive = true;
        }

        public int Age { get; private set; }
        public bool IsAlive { get; set; }
        public string Name { get; private set; }
    }
}
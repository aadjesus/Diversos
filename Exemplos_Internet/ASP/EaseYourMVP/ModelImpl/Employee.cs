using System;

namespace Research.MVP.ModelImpl
{

    public class Employee
    {

        // Fields

        private string name;
        private DateTime birthDate;
        private DateTime hiredDate;


        // Methods

        public Employee(string name, DateTime birthDate, DateTime hiredDate)
        {
            this.name = name;
            this.birthDate = birthDate;
            this.hiredDate = hiredDate;
        }


        // properties

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public DateTime HiredDate
        {
            get { return this.hiredDate; }
            set { this.hiredDate = value; }
        }

        public DateTime BirthDate
        {
            get { return this.birthDate; }
            set { this.birthDate = value; }
        }


    }

}

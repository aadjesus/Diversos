using System;
using Research.MVP.Presenters;
using Research.MVP.ViewsImpl;
using System.Collections.Generic;
using Research.MVP.ModelImpl;

namespace Research.MVP.PresentersImpl
{

    public class EmployeesListPresenter : GenericPresenter<IEmployeesListView>
    {

        // Methods

        public override BasicPresenter Initialize()
        {
            base.Initialize();

            if ((null != this.Root) && (this.Root is IDateTimeFormatChanger))
            {
                (this.Root as IDateTimeFormatChanger).DateTimeFormatChanged += new DateTimeFormatEventHandler(EmployeesListPresenter_DateTimeFormatChanged);
            }

            // we do a fake data source
            this.View.Source = this.FakeSource;

            return this;
        }

        private void EmployeesListPresenter_DateTimeFormatChanged(object sender, DateTimeFormatEventArgs e)
        {
            this.View.DateTimeFormat = !string.IsNullOrEmpty(e.Format) ? e.Format : string.Empty;
            this.View.Source = this.FakeSource;
        }


        // Properties

        public IEnumerable<Employee> FakeSource
        {
            get
            {
                yield return new Employee("Bill", new DateTime(1954, 2, 14), new DateTime(2003, 07, 04));
                yield return new Employee("John", new DateTime(1983, 4, 2), new DateTime(2000, 04, 02));
                yield return new Employee("Emma", new DateTime(1965, 3, 5), new DateTime(2003, 11, 17));
            }
        }

    }

}

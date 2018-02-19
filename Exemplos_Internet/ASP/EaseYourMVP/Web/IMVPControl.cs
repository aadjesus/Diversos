using System;
using Research.MVP.Presenters;

namespace Research.MVP.Web
{

    /// <summary>
    /// Contains MVP logic for controls.
    /// </summary>
    public interface IMVPControl
    {

        /// <summary>
        /// Hierarchizes the current instance into the hierarchy of the specified presenter.
        /// </summary>
        /// <param name="shouldBeParent">The logical parent presenter.</param>
        void Hierarchize(BasicPresenter shouldBeParent);

    }

}

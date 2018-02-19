using System;
using Research.MVP.Views;

namespace Research.MVP.Presenters
{

    /// <summary>
    /// Base presenter class that strong types the bound view.
    /// </summary>
    /// <typeparam name="V">View type argument.</typeparam>
    public abstract class GenericPresenter<V> : BasicPresenter
        where V : class, IBasicView
    {

        // Properties

        /// <summary>
        /// Gets the bound view.
        /// </summary>
        /// <value>The bound view.</value>
        protected V View
        {
            get { return base.ViewInternal as V; }
        }

    }

}

using System;
using System.Collections.Generic;
using Research.MVP.Views;

namespace Research.MVP.Presenters
{

    /// <summary>
    /// Basic view functionality (view binding and hierarchy).
    /// </summary>
    public abstract class BasicPresenter
    {

        // Fields

        private IBasicView view;
        private BasicPresenter parent;
        private IList<BasicPresenter> children;
        private bool initialized = false;


        // Events

        /// <summary>
        /// Occurs when the presenter parent was just changed.
        /// </summary>
        public event EventHandler ParentChanged;


        // Methods

        /// <summary>
        /// Binds to view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        public BasicPresenter BindToView(IBasicView view)
        {
            if (null != this.view)
            {
                throw new InvalidOperationException("The presenter is already bound.");
            }

            this.view = view;

            if (null == this.view)
            {
                throw new ArgumentNullException("view");
            }

            return this;
        }

        /// <summary>
        /// Initializes this presenter instance.
        /// </summary>
        /// <returns>This.</returns>
        public virtual BasicPresenter Initialize()
        {
            if (this.initialized)
            {
                throw new InvalidOperationException("Cannot initialize twice.");
            }
            this.initialized = true;

            return this;
        }

        /// <summary>
        /// Processes this presenter instance.
        /// </summary>
        /// <returns>This.</returns>
        public virtual BasicPresenter Process()
        {
            return this;
        }


        // Properties

        /// <summary>
        /// Gets or sets the parent presenter.
        /// </summary>
        /// <value>The parent presenter.</value>
        public virtual BasicPresenter Parent
        {
            get { return this.parent; }
            set
            {
                if (value != this.parent)
                {
                    if (null != this.parent)
                    {
                        this.parent.Children.Remove(this as BasicPresenter);
                    }

                    this.parent = value;

                    if (null != this.parent)
                    {
                        this.parent.Children.Add(this as BasicPresenter);
                    }

                    if (null != this.ParentChanged)
                    {
                        this.ParentChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the root presenter.
        /// </summary>
        /// <value>The root presenter.</value>
        public BasicPresenter Root
        {
            get { return (null == this.parent) ? this : this.parent.Root; }
        }

        /// <summary>
        /// Gets the children presenters.
        /// </summary>
        /// <value>The children presenters.</value>
        public IList<BasicPresenter> Children
        {
            get
            {
                if (null == this.children)
                {
                    this.children = new List<BasicPresenter>();
                }
                return this.children;
            }
        }        

        internal protected IBasicView ViewInternal
        {
            get
            {
                // ensure presenter binding even with a small 
                // performace penalty cost due to view instance checking
                if (null == this.view)
                {
                    throw new InvalidOperationException("Use of unbound presenter.");
                }
                return this.view;
            }
        }

    }

}

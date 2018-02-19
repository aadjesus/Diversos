using System;
using System.Web.UI;
using Research.MVP.Presenters;
using Research.MVP.Views;
using System.Collections;
using System.Collections.Generic;

namespace Research.MVP.Web
{

    /// <summary>
    /// Base MVP user control.
    /// </summary>
    /// <typeparam name="P">The presenter type argument.</typeparam>
    /// <typeparam name="V">The view type argument.</typeparam>
    public abstract class MVPPage<P, V> : Page, IMVPControl
        where P : GenericPresenter<V>, new()
        where V : class, IBasicView
    {

        // Fields

        private P presenter;


        // Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="MVPUserControl&lt;P, V&gt;"/> class.
        /// </summary>
        public MVPPage()
        {
            this.presenter = new P().BindToView(this as V) as P;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.presenter.Initialize();
        }

        /// <summary>
        /// Called after a child control is added to the <see cref="P:System.Web.UI.Control.Controls"></see> collection of the <see cref="T:System.Web.UI.Control"></see> object.
        /// </summary>
        /// <param name="control">The <see cref="T:System.Web.UI.Control"></see> that has been added.</param>
        /// <param name="index">The index of the control in the <see cref="P:System.Web.UI.Control.Controls"></see> collection.</param>
        protected override void AddedControl(Control control, int index)
        {
            base.AddedControl(control, index);

            foreach (IMVPControl mvpControl in this.FindMVPChildren(control))
            {
                mvpControl.Hierarchize(this.presenter);
            }
        }

        /// <summary>
        /// Called after a control is removed from the <see cref="P:System.Web.UI.Control.Controls"></see> collection of another control.
        /// </summary>
        /// <param name="control">The <see cref="T:System.Web.UI.Control"></see> that has been removed.</param>
        protected override void RemovedControl(Control control)
        {
            base.RemovedControl(control);

            // suppose we have 
            // <res:MVPControl ID="parent">
            //  <div>
            //   <res:MVPControl ID="child">
            // the current event is rised in the parent just for the div
            // so we have to find the child so not to loose it
            foreach (IMVPControl mvpControl in this.FindMVPChildren(control))
            {
                mvpControl.Hierarchize(null);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.presenter.Process();
        }

        private IEnumerable<IMVPControl> FindMVPChildren(Control control)
        {
            if (null != control)
            {
                if (control is IMVPControl)
                {
                    yield return (control as IMVPControl);
                }
                else if (0 < control.Controls.Count)
                {
                    foreach (Control childControl in control.Controls)
                    {
                        foreach (IMVPControl mvpControl in this.FindMVPChildren(childControl))
                        {
                            yield return mvpControl;
                        }
                    }                    
                }
            }
        }


        // Properties

        /// <summary>
        /// Gets the presenter of the current view instance.
        /// </summary>
        /// <value>The presenter of the current view instance.</value>
        protected P Presenter
        {
            get { return this.presenter; }
        }


        #region IMVPControl Members

        /// <summary>
        /// Hierarchizes the current instance into the hierarchy of the specified presenter.
        /// </summary>
        /// <param name="shouldBeParent">The logical parent presenter.</param>
        public void Hierarchize(BasicPresenter shouldBeParent)
        {
            this.presenter.Parent = shouldBeParent;
        }

        #endregion

    }

}

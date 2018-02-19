/******************************************************************************* 
 *  Copyright 2009 http://www.SoapPanda.com.
 *  
 *  Author: Anantjot Anand
 *
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GenericForm
{
    public class ObjectPanel : Panel
    {
        public ObjectPanel()
        {
        }
        public ObjectPanel(object obj)
        {
            this.instance = obj;
        }
        private Object instance;

        public Object Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        //this will relayout the child controls that were below the child control
        //as the child control has either expanded or collapsed by the user
        public void ReLayoutChild(Control child, Control newCtl, bool bExpand)
        {
            for (int i = 0; i < Controls.Count; i++)
            {
                if (child != Controls[i] && Controls[i].Top > child.Top)
                    if (bExpand)
                        Controls[i].Top += newCtl.Height;
                    else
                        Controls[i].Top -= newCtl.Height;
            }
        }

        public virtual void Reset()
        {
            instance = null;
            this.Tag = null;
            this.Controls.Clear();
        }

    }
}

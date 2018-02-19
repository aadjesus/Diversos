using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace SimpleMDIApp
{
    public class ContactController : Controller
    {
        public ContactWorkItem ContactWorkItem
        {
            get { return base.WorkItem as ContactWorkItem; }
        }
    }
}
/*
The MIT License

Copyright (c) 2008 Kevin Moore (http://j832.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using J832.Common;

namespace J832.Wpf
{

    [TypeConverter(typeof(ActionICommandConverter))]
    public class ActionICommand : ICommand
    {
        public static ActionICommand Create(Action action)
        {
            Util.RequireNotNull(action, "action");
            return Create(action, null);
        }

        public static ActionICommand Create(Action action, Func<bool> canExecuteFunction)
        {
            Util.RequireNotNull(action, "action");

            Action foo;
            return Create(action, canExecuteFunction, out foo);
        }

        public static ActionICommand Create(Action action, Func<bool> canExecuteFunction, out Action canExecuteChanged)
        {
            Util.RequireNotNull(action, "action");

            ActionICommand command = new ActionICommand(action, canExecuteFunction);

            canExecuteChanged = command.onCanExecuteChanged;

            return command;
        }

        public bool CanExecute
        {
            get
            {
                if (m_canExecuteFunction != null)
                {
                    return m_canExecuteFunction();
                }
                else
                {
                    return true;
                }
            }
        }

        public void Execute()
        {
            m_action();
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }

        protected virtual void OnCanExecuteChanged(EventArgs args)
        {
            Util.RequireNotNull(args, "args");

            EventHandler handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        #region Implementation

        private ActionICommand(Action action, Func<bool> canExecuteFunction)
        {
            Util.RequireNotNull(action, "action");
            m_action = action;

            m_canExecuteFunction = canExecuteFunction;
        }

        private void onCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        private readonly Func<bool> m_canExecuteFunction;
        private readonly Action m_action;

        #endregion

    }

    public class ActionICommandConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(ICommand))
            {
                return true;
            }
            else if (destinationType == typeof(string))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
        }

        public override object ConvertTo(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value,
            Type destinationType)
        {
            if (destinationType == typeof(ICommand))
            {
                return (ICommand)value;
            }
            else if (destinationType == typeof(string))
            {
                return ((ActionICommand)value).ToString();
            }
            else
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
    }
}
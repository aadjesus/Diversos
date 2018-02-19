using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

using PhysicsHost.DataAccess;
using PhysicsHost.ViewModel;

namespace PhysicsHost
{
    /// <summary>
    /// Explitly updates and validates the underlying bound
    /// Order object.
    /// </summary>
    public partial class EditOrderWindow : Window
    {
        #region Data
        OrderViewModel orderViewModel = new OrderViewModel();
        #endregion

        #region Ctor
        public EditOrderWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Command Sinks
        /// <summary>
        /// Sumbit the bound data changes back into the underlying
        /// <see cref="Order">Order data object</see> and see
        /// if we are able to update the Database
        /// </summary>
        private void OrderViewModelSubmitChangesCommand_Executed(
            object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (UpdateBindings())
                {
                    this.orderViewModel.SubmitChanges();
                    this.Close();
                    MessageBoxHelper.ShowMessageBox(
                        "Successfully updated Order", "Order updated");
                }
                else
                {
                    MessageBoxHelper.ShowErrorBox(
                        "Error updating Order", "Order error");
                }
            }
            catch (BindingException bex)
            {
                MessageBoxHelper.ShowErrorBox(
                    "Binding error occurred\r\n" + bex.Message,
                    "Binding error");
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowErrorBox(
                    "An Error occurred trying to update the database\r\n" 
                    + ex.Message,"Database save error");
            }

        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Cancel, so close window
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// Update the changes back into the underlying
        /// <see cref="Order">Order data object</see>
        /// <returns>True if all binding values were valis. This
        /// is done by using Paul Stovells <see cref="ErrorProvider">
        /// ErrorProvider</see> class</returns>
        private bool UpdateBindings()
        {
            try
            {
                UpdateSingleBinding(txtShipName, TextBox.TextProperty);
                UpdateSingleBinding(txtShipAddress, TextBox.TextProperty);
                UpdateSingleBinding(txtShipCity, TextBox.TextProperty);
                UpdateSingleBinding(txtShipRegion, TextBox.TextProperty);
                UpdateSingleBinding(txtShipPostalCode, TextBox.TextProperty);
                UpdateSingleBinding(txtShipCountry, TextBox.TextProperty);
                return errorProvider.Validate();
            }
            catch
            {
                throw new BindingException(string.Format(
                    "There was a problem updating the Bindings for Customer {0}",
                    (this.DataContext as Customer).CustomerID));
            }
        }

        /// <summary>
        /// Updates a single TextBox binding
        /// </summary>
        private void UpdateSingleBinding(DependencyObject target, DependencyProperty dp)
        {
            BindingExpression bindingExpression = 
                BindingOperations.GetBindingExpression(target, dp);
            bindingExpression.UpdateSource();
        }

        #endregion
    }
}

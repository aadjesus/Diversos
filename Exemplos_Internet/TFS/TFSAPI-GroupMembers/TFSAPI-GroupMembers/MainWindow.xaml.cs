using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;

namespace TFSAPI_GroupMembers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TfsTeamProjectCollection _tfs;
        private IGroupSecurityService _gss;
        private ICommonStructureService _css;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConnectClick(object sender, RoutedEventArgs e)
        {
            DisableUi();
            var tpp = new TeamProjectPicker(TeamProjectPickerMode.NoProject, false);
            tpp.ShowDialog();
            if (tpp.SelectedTeamProjectCollection == null) return;

            EnableUi();

            _tfs = tpp.SelectedTeamProjectCollection;
            _css = (ICommonStructureService)_tfs.GetService<ICommonStructureService>();
            _gss = (IGroupSecurityService)_tfs.GetService<IGroupSecurityService>();

            var allSids = _gss.ReadIdentity(SearchFactor.AccountName,
                "Project Collection Valid Users", QueryMembership.Expanded);

            listAllUsers.ItemsSource = _gss.ReadIdentities(SearchFactor.Sid, allSids.Members,
                QueryMembership.None).Where(a => a.Type == IdentityType.WindowsUser
                    || a.Type == IdentityType.WindowsGroup);

            listProjects.ItemsSource = _css.ListAllProjects();
        }

        private void EnableUi()
        {
            listGroups.IsEnabled = true;
            listProjects.IsEnabled = true;
            btnAddGroup.IsEnabled = true;
            btnRemoveGroup.IsEnabled = true;
            listUsers.IsEnabled = true;
            txtGroupName.IsEnabled = true;
            listAllUsers.IsEnabled = true;
            btnAddUserToGroup.IsEnabled = true;
            btnRemoveUserFromGroup.IsEnabled = true;
        }

        private void DisableUi()
        {
            listGroups.IsEnabled = false;
            listProjects.IsEnabled = false;
            btnAddGroup.IsEnabled = false;
            btnRemoveGroup.IsEnabled = false;
            listUsers.IsEnabled = false;
            txtGroupName.IsEnabled = false;
            btnAddUserToGroup.IsEnabled = false;
            btnRemoveUserFromGroup.IsEnabled = false;
        }

        private void ListProjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listProjects.SelectedItem == null) return;
            var project = listProjects.SelectedItem as ProjectInfo;
            listGroups.ItemsSource = _gss.ListApplicationGroups(project.Uri);
        }

        private void ListGroupsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listGroups.SelectedItem == null) return;

            var group = listGroups.SelectedItem as Identity;

            var sids = _gss.ReadIdentity(SearchFactor.Sid, group.Sid, QueryMembership.Expanded);
            if (sids == null || sids.Members.Length == 0)
            {
                listUsers.ItemsSource = null;
                return;
            }

            listUsers.ItemsSource = _gss.ReadIdentities(SearchFactor.Sid, sids.Members, QueryMembership.None);
        }

        private void BtnAddGroupClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtGroupName.Text) || listProjects.SelectedItem == null) return;

            var project = listProjects.SelectedItem as ProjectInfo;
            var groupname = txtGroupName.Text;
            try
            {
                var result = _gss.CreateApplicationGroup(project.Uri, groupname, "Your Group Description");
                ListProjectsSelectionChanged(sender, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnRemoveGroupClick(object sender, RoutedEventArgs e)
        {
            if (listGroups.SelectedItem == null) return;

            var result = MessageBox.Show("You are about to remove application group from TFS, press yes to continue",
                                            "Remove Group", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            var group = listGroups.SelectedItem as Identity;
            _gss.DeleteApplicationGroup(group.Sid);
            ListProjectsSelectionChanged(sender, null);
        }

        private void BtnRemoveUserFromGroupClick(object sender, RoutedEventArgs e)
        {
            if (listUsers.SelectedItem == null || listGroups.SelectedItem == null) return;

            var group = listGroups.SelectedItem as Identity;
            var user = listUsers.SelectedItem as Identity;

            _gss.RemoveMemberFromApplicationGroup(group.Sid, user.Sid);
            ListGroupsSelectionChanged(sender, null);
        }

        private void BtnAddUserToGroupClick(object sender, RoutedEventArgs e)
        {
            if (listAllUsers.SelectedItem == null || listGroups.SelectedItem == null) return;

            var group = listGroups.SelectedItem as Identity;
            var user = listAllUsers.SelectedItem as Identity;

            try
            {
                _gss.AddMemberToApplicationGroup(group.Sid, user.Sid);
                ListGroupsSelectionChanged(sender, null);
            }
            catch (Exception ex)
            {//TF50235: The group Test Group already has a member Administrators.
                MessageBox.Show(ex.Message);
            }
        }
    }
}

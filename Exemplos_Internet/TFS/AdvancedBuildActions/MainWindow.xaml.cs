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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TFS_API_AdvancedBuildActions
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TfsTeamProjectCollection _server;
        private IBuildServer _buildServer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConnectTfsClick(object sender, RoutedEventArgs e)
        {
            using (var tpp = new TeamProjectPicker(TeamProjectPickerMode.NoProject, false))
            {
                tpp.ShowDialog();
                if (tpp.SelectedTeamProjectCollection == null) return;

                _server = tpp.SelectedTeamProjectCollection;
                _server.EnsureAuthenticated();
                listProjects.ItemsSource = listDefinitionsTeamProject.ItemsSource = ((WorkItemStore)_server.GetService(typeof(WorkItemStore))).Projects;
                _buildServer = (IBuildServer)_server.GetService(typeof(IBuildServer));
                listTfsControllers.ItemsSource = _buildServer.QueryBuildControllers();
                tabControl.IsEnabled = true;
            }
        }

        private void ListProjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listProjects.SelectedItem != null)
            {
                listBuildDefs.ItemsSource = _buildServer.QueryBuildDefinitions(((Project)listProjects.SelectedItem).Name);
            }
        }

        private void ListBuildDefsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBuildDefs.SelectedItem == null) return;
            //True to Include Agents for the Controller.
            listControllers.ItemsSource =
                _buildServer.QueryBuildControllers(true).Where(
                    b => b.Name.Equals(((IBuildDefinition)listBuildDefs.SelectedItem).BuildController.Name));
        }

        private void ListControllersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listControllers.SelectedItem == null) return;
            listAgents.ItemsSource = ((IBuildController)listControllers.SelectedItem).Agents;
        }

        private void BtnAddControllerClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //Creates a new service host with the specified name and base URL.
                var serviceHost = _buildServer.CreateBuildServiceHost(txtMachineName.Text, new Uri("http://" + txtServiceHostName.Text + ":" + txtPort.Text + "/"));
                serviceHost.Save();
                //Creates a build controller that is associated with the current service host.
                var controller = serviceHost.CreateBuildController(txtControllerName.Text);
                controller.Save();

                listTfsControllers.ItemsSource = _buildServer.QueryBuildControllers();
            }
            catch (BuildServiceHostAlreadyExistsException alreadyExistsException)
            {
                MessageBox.Show(alreadyExistsException.Message);
            }
        }

        private void ListTfsControllersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listTfsControllers.SelectedItem == null) return;
            var controller = (IBuildController)listTfsControllers.SelectedItem;
            //txtPort            => controller.ServiceHost.BaseUrl.Port;
            //txtMachineName     => controller.ServiceHost.BaseUrl.Host;
            //txtControllerName  => controller.Name;
            //txtServiceHostName => controller.ServiceHost.Name;
        }

        private void BtnRemoveClick(object sender, RoutedEventArgs e)
        {
            if (listTfsControllers.SelectedItem == null) return;
            //Obtain the IBuildController and using IBuildServer calling the DeleteBuildControllers method.
            var controller = (IBuildController)listTfsControllers.SelectedItem;
            _buildServer.DeleteBuildControllers(new IBuildController[] { controller });
            listTfsControllers.ItemsSource = _buildServer.QueryBuildControllers();
        }

        private void ListDefinitionsTeamProjectSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listDefinitionsTeamProject.SelectedItem == null) return;
            comboProcessTemplates.ItemsSource = _buildServer.QueryProcessTemplates(
                ((Project)listDefinitionsTeamProject.SelectedItem).Name);

            listAddDefinitionBuildDefinitions.ItemsSource = _buildServer.QueryBuildDefinitions(
                ((Project)listDefinitionsTeamProject.SelectedItem).Name);
        }

        private void BtnAddDefinitionClick(object sender, RoutedEventArgs e)
        {
            if (listDefinitionsTeamProject.SelectedItem != null && listTfsControllers.SelectedItem != null)
            {
                if (string.IsNullOrEmpty(txtDefinitionName.Text) || string.IsNullOrEmpty(txtDropLocation.Text) ||
                    comboProcessTemplates.SelectedItem == null)
                {
                    MessageBox.Show(
                        "Please make sure the following fields has value:\n1.Definiton Name\n2.Drop Location\n3.Process Template",
                        "Missing Values", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    var proj = (Project)listDefinitionsTeamProject.SelectedItem;
                    var controller = (IBuildController)listTfsControllers.SelectedItem;

                    //Create new instance of build definition under the Team Project.
                    var definition = _buildServer.CreateBuildDefinition(proj.Name);
                    definition.Name = txtDefinitionName.Text;
                    //Define Build Definition Trigger - // All, Batch , Gated, Individual, None, Schedule, ScheduleForced
                    definition.ContinuousIntegrationType = (ContinuousIntegrationType)comboContinuousIntegrationTypes.SelectedItem;
                    //Define Build Controller
                    definition.BuildController = controller;
                    definition.DefaultDropLocation = txtDropLocation.Text;
                    definition.Description = txtDescription.Text;
                    definition.Enabled = (bool)chxIsEnabled.IsChecked ? true : false;
                    //Create a fake workspace folder
                    definition.Workspace.AddMapping("$/", "c:\\someFakeFolder", WorkspaceMappingType.Map);
                    //Assign the Process Template for the new build definition
                    definition.Process = (IProcessTemplate)comboProcessTemplates.SelectedItem;
                    definition.Save();

                    //Refresh the Build Definitions List
                    listAddDefinitionBuildDefinitions.ItemsSource = _buildServer.QueryBuildDefinitions(((Project)listDefinitionsTeamProject.SelectedItem).Name);
                }
                catch (BuildServerException ex)
                {
                    MessageBox.Show(ex.Message, "Opps", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnClearClick(object sender, RoutedEventArgs e)
        {
            listAddDefinitionBuildDefinitions.SelectedItem = null;
        }

        private void BtnRemoveDefinitionClick(object sender, RoutedEventArgs e)
        {
            if (listAddDefinitionBuildDefinitions.SelectedItem == null) return;
            _buildServer.DeleteBuildDefinitions(new IBuildDefinition[] { (IBuildDefinition)listAddDefinitionBuildDefinitions.SelectedItem });
            listAddDefinitionBuildDefinitions.ItemsSource = _buildServer.QueryBuildDefinitions(((Project)listDefinitionsTeamProject.SelectedItem).Name);
        }

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem)
            {
                groupTFSControllers.Visibility = ((TabItem) e.AddedItems[0]) != flowTab
                                                     ? System.Windows.Visibility.Visible
                                                     : System.Windows.Visibility.Hidden;

                groupTeamProjects.Visibility = groupBuildDefinitions.Visibility =
                ((TabItem) e.AddedItems[0]) != adControllersTab && ((TabItem)e.AddedItems[0]) != flowTab ? System.Windows.Visibility.Visible : Visibility.Hidden;
            }
        }

private void BtnAddBuildClick(object sender, RoutedEventArgs e)
{
    if (listAddDefinitionBuildDefinitions.SelectedItem == null) return;

    if(string.IsNullOrEmpty(txtBuildName.Text) || string.IsNullOrEmpty(txtLocalPath.Text) || string.IsNullOrEmpty(txtServerPath.Text) || comboBuildStatus.SelectedItem == null)
    {
        MessageBox.Show("Please make sure the following fields has valid value:\n1.Build Name\n2.Local Path\n3.Server Path\n4.Status",
            "Missing Values", MessageBoxButton.OK, MessageBoxImage.Information);
        return;
    }

    try
    {
        var buildDefinition = (IBuildDefinition)listAddDefinitionBuildDefinitions.SelectedItem;
        IBuildDetail buildDetail = buildDefinition.CreateManualBuild(txtBuildName.Text);

        IBuildProjectNode buildProjectNode = buildDetail.Information.AddBuildProjectNode(
            DateTime.Now.AddSeconds(10),            // Finish Time = The time at which the project finished building.
            comboFlavor.SelectedValue.ToString(),   //Flavor = The flavor (configuration) the project was built for.
            txtLocalPath.Text,                      //Local Path = The local path of the project file.
            comboPlatform.SelectedValue.ToString(), //Platform = The platform the project was built for.
            txtServerPath.Text,                     // Server Path = The server path of the project file.
            DateTime.Now,                           //Start Time = The time at which the project was built.
            "default");                             //Target Name = The targets for which the project was built.

        buildProjectNode.Save();
        buildDetail.FinalizeStatus((BuildStatus)comboBuildStatus.SelectedItem);

        ClearAddBuildForm();
    }
    catch (AccessDeniedException accessDeniedException)
    {
        MessageBox.Show(accessDeniedException.Message, "Access Denied Exception", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    catch (BuildNumberAlreadyExistsException buildNumberAlreadyExistsException)
    {
        MessageBox.Show(buildNumberAlreadyExistsException.Message, "Build Number Already Exists Exception", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    catch (InvalidFinalStatusException invalidFinalStatusException)
    {
        MessageBox.Show(invalidFinalStatusException.Message, "Invalid Final Status Exception", MessageBoxButton.OK, MessageBoxImage.Error);
    }
    finally
    {
        listBuilds.ItemsSource = ((IBuildDefinition)listAddDefinitionBuildDefinitions.SelectedItem).QueryBuilds();
    }
}

        private void ClearAddBuildForm()
        {
            txtBuildName.Text = string.Empty;
            comboBuildStatus.SelectedItem = null;
        }

private void ListAddDefinitionBuildDefinitionsSelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if(listAddDefinitionBuildDefinitions.SelectedItem == null) return;
    listBuilds.ItemsSource = ((IBuildDefinition) listAddDefinitionBuildDefinitions.SelectedItem).QueryBuilds();
}

private void BtnRemoveBuildClick(object sender, RoutedEventArgs e)
{
    if (listBuilds.SelectedItem == null) return;
    _buildServer.DeleteBuilds(new IBuildDetail[] { (IBuildDetail)listBuilds.SelectedItem });
    listBuilds.ItemsSource = ((IBuildDefinition)listAddDefinitionBuildDefinitions.SelectedItem).QueryBuilds();
}
    }
}

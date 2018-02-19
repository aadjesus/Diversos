using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Proxy;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;

namespace UsersList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_dp_Click(object sender, EventArgs e)
        {
            DomainProjectPicker dp = new DomainProjectPicker();
            dp.ShowDialog();
            if (dp.SelectedServer != null)
            {
                list_projects.DataSource = dp.SelectedProjects;
                GetTfsUsers(dp.SelectedServer);
            }
        }

        private void GetTfsUsers(TeamFoundationServer server)
        {
            IGroupSecurityService gss = (IGroupSecurityService)server.GetService(typeof(IGroupSecurityService));
            Identity SIDS = gss.ReadIdentity(
                SearchFactor.EveryoneApplicationGroup,//,SearchFactor.AccountName, 
                null, //"Team Foundation Valid Users", 
                QueryMembership.Direct);//QueryMembership.Expanded);



            Identity[] UserId = gss.ReadIdentities(SearchFactor.Sid, SIDS.Members, QueryMembership.None);
            list_users.Items.Clear();
            
            foreach (Identity user in UserId)
            {
                ListViewItem li = new ListViewItem(user.DisplayName);
                li.SubItems.Add(user.AccountName);
                li.SubItems.Add(user.MailAddress);
                li.SubItems.Add(user.Sid);

                list_users.Items.Add(li);
            }
        }
    }
}

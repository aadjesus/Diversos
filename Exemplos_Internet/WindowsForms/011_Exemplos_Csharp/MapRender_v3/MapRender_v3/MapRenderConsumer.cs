using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapRender_v3
{
    public partial class MapRenderConsumer : Form
    {
        public MapRenderConsumer()
        {
            InitializeComponent();
        }

        private void btnGetMap2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();                
                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                _extent.XMax = -46.5669571428571;
                _extent.YMax = -23.6290014285714;
                _extent.XMin = -46.5747235820896;
                _extent.YMin = -23.6343517910448;

                mii = mg.getMap("", (MapRenderWS.Extent)_extent, mio, _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnZoomIn2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                _extent.XMax = -46.5669571428571;
                _extent.YMax = -23.6290014285714;
                _extent.XMin = -46.5747235820896;
                _extent.YMin = -23.6343517910448;

                mii = mg.getZoom("", (MapRenderWS.Extent)_extent, -0.1, mio, _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnZoomOut2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                mii = mg.getZoom("", (MapRenderWS.Extent)_extent, .1, mio, _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnFullExtent2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                mii = mg.getZoomFullExtent("", mio, _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnZoomRadius2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                MapRenderWS.Point point = new MapRenderWS.Point();
                point.x = -46.6658367922849;
                point.y = -23.558544847211;

                mii = mg.getZoomRadius("", point, 200, mio, _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnZoomWindow2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                MapRenderWS.Extent newExtent = new MapRenderWS.Extent();

                newExtent.XMin = 0;
                newExtent.XMax = 0;
                newExtent.YMax = 0;
                newExtent.YMin = 0;

                mii = mg.getZoomWindow("", _extent, newExtent, mio, _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnZoomState2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                mii = mg.getZoomState("", mio, "SP", _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnZoomCity2_3_Click(object sender, EventArgs e)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 1000;
                mis.width = 1000;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions(); 
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                MapRenderWS.City city = new MapRenderWS.City();
                city.name = "SAO PAULO";
                city.state = "SP";

                mii = mg.getZoomCity("", mio, city, _token);

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnGetPan2_3_Click(object sender, EventArgs e)
        {
            GetPan(2, 3);
        }

        public void GetPan(int direction, double percNavegation)
        {
            MapRenderWS.Extent _extent = new MapRenderWS.Extent();

            string temp = "";

            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                MapRenderWS.MapSize mis = new MapRenderWS.MapSize();
                mis.height = 600;
                mis.width = 600;

                MapRenderWS.MapOptions mio = new MapRenderWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                MapRenderWS.MapRender mg = new MapRenderWS.MapRender();
                MapRenderWS.MapInfo mii = new MapRenderWS.MapInfo();

                mii = mg.getPan("", direction, percNavegation, (MapRenderWS.Extent)_extent, mio, _token);

                temp = mii.url;

                MessageBox.Show(mii.url);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }
    }
}
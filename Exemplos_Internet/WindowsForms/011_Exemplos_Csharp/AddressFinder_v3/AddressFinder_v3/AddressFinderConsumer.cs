using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AddressFinder_v3
{
    public partial class AddressFinderConsumer : Form
    {
        public AddressFinderConsumer()
        {
            InitializeComponent();
        }

        private void btnFindCity2_3_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                AddressFinderWS.City cidade = new AddressFinderWS.City();
                cidade.name = "Sao Paulo";
                cidade.state = "SP";

                AddressFinderWS.AddressOptions ao = new AddressFinderWS.AddressOptions();

                AddressFinderWS.ResultRange rr = new AddressFinderWS.ResultRange();

                rr.recordsPerPage = 10;
                rr.pageIndex = 1;
                ao.resultRange = rr;

                AddressFinderWS.AddressFinder af = new AddressFinderWS.AddressFinder();

                AddressFinderWS.CityLocationInfo cli = af.findCity(cidade, ao, _token);

                int x = 0;
                string Texto = "";

                foreach (AddressFinderWS.CityLocation l in cli.cityLocation)
                {
                    Texto += "Descrição: " + l.city.name +
                    " UF= " + l.city.state +
                    " X = " + l.point.x.ToString() +
                    " Y = " + l.point.y.ToString() +
                    " CarAccess = " + l.carAccess +
                    " CEP Inicial = " + l.zipRangeStart +
                    " CEP Final = " + l.zipRangeEnd + "\n";

                    x++;
                }

                MessageBox.Show(Texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnFindAddress2_3_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                AddressFinderWS.Address ad = new AddressFinderWS.Address();

                AddressFinderWS.City city = new AddressFinderWS.City();
                city.name = "Sao Paulo";
                city.state = "SP";
                ad.city = city;
                ad.street = "paulista";
                ad.houseNumber = "1000";
                ad.zip = "";
                ad.district = "";

                AddressFinderWS.AddressOptions ao = new AddressFinderWS.AddressOptions();

                AddressFinderWS.ResultRange rr = new AddressFinderWS.ResultRange();
                rr.recordsPerPage = 20;
                rr.pageIndex = 1;

                ao.resultRange = rr;
                ao.matchType = 1;
                ao.searchType = 2;
                ao.usePhonetic = true;

                AddressFinderWS.AddressFinder af = new AddressFinderWS.AddressFinder();

                AddressFinderWS.AddressInfo result = af.findAddress(ad, ao, _token);

                string texto = "";

                foreach (AddressFinderWS.AddressLocation al in result.addressLocation)
                {
                    texto +=
                         "Address: " + al.address.street
                       + ", Nr.: " + al.address.houseNumber
                       + ", ZipL: " + al.zipL
                       + ", ZipR: " + al.zipR
                       + ", Cidade: " + al.address.city.name
                       + ", Estado: " + al.address.city.state
                       + ", Bairro: " + al.address.district
                       + ", X : " + al.point.x
                       + ", Y : " + al.point.y;
                    if (al.carAccess)
                        texto += ", Acesso de carro: SIM" + "\n";
                    else
                        texto += ", Acesso de carro: NÃO" + "\n";
                }

                MessageBox.Show(texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnGetAddress2_3_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                AddressFinderWS.AddressFinder af = new AddressFinderWS.AddressFinder();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                AddressFinderWS.Address ad = new AddressFinderWS.Address();
                ad.street = "Paulista";
                ad.houseNumber = "1000";
                ad.zip = "";

                AddressFinderWS.City city = new AddressFinderWS.City();
                city.name = "Sao Paulo";
                city.state = "SP";

                ad.city = city;
                AddressFinderWS.Point point = new AddressFinderWS.Point();
 
                point.x = -46.652009736617;
                point.y = -23.5650109993641;

                AddressFinderWS.AddressLocation al = new AddressFinderWS.AddressLocation();
                al = af.getAddress(point, _token, 5);

                string texto = "";

                texto += "endereco = " + al.address.street + "\n";
                texto += "numero = " + al.address.houseNumber + "\n";
                texto += "zipL = " + al.zipL + "\n";
                texto += "zipR = " + al.zipR + "\n";
                texto += "cidade = " + al.address.city.name + "\n";
                texto += "estado = " + al.address.city.state + "\n";
                texto += "bairro = " + al.address.district + "\n";
                texto += "Acesso carro = " + al.carAccess + "\n";
                texto += "Datasource = " + al.dataSource + "\n";

                MessageBox.Show(texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnGetXY2_3_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                AddressFinderWS.Address ad = new AddressFinderWS.Address();
                ad.street = "Paulista";
                ad.houseNumber = "1000";
                ad.zip = "";

                AddressFinderWS.City city = new AddressFinderWS.City();
                city.name = "Sao Paulo";
                city.state = "SP";

                ad.city = city;

                AddressFinderWS.Point point = new AddressFinderWS.Point();

                AddressFinderWS.AddressFinder af = new AddressFinderWS.AddressFinder();

                point = af.getXY(ad, _token);

                string texto = "";
                texto = "X=" + point.x.ToString() + ", Y=" + point.y.ToString();

                MessageBox.Show(texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnRadiusMap2_3_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                AddressFinderWS.Address ad = new AddressFinderWS.Address();
                ad.street = "Paulista";
                ad.houseNumber = "1000";

                AddressFinderWS.City city = new AddressFinderWS.City();
                city.name = "Sao Paulo";
                city.state = "SP";

                ad.city = city;
                ad.zip = "";

                AddressFinderWS.Point point = new AddressFinderWS.Point();

                AddressFinderWS.MapOptions mo = new AddressFinderWS.MapOptions();

                AddressFinderWS.MapSize ms = new AddressFinderWS.MapSize();
                ms.height = 248;
                ms.width = 338;

                mo.mapSize = ms;
                mo.scaleBar = true;

                AddressFinderWS.MapInfo mi = new AddressFinderWS.MapInfo();
                AddressFinderWS.AddressFinder af = new AddressFinderWS.AddressFinder();

                mi = af.getXYRadiusWithMap(ad, mo, 300, _token);

                string texto = mi.url;

                MessageBox.Show(texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }

        private void btnFindPOI2_3_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                AddressFinderWS.City city = new AddressFinderWS.City();
                city.name = "Sao Paulo";
                city.state = "SP";

                AddressFinderWS.ResultRange rr = new AddressFinderWS.ResultRange();
                rr.recordsPerPage = 10;
                rr.pageIndex = 1;

                AddressFinderWS.AddressFinder af = new AddressFinderWS.AddressFinder();

                AddressFinderWS.POIInfo result = af.findPOI("cinema", city, rr, _token);

                string texto = "";

                foreach (AddressFinderWS.POILocation pl in result.poiLocations)
                {
                    texto +=
                        "POI : " + pl.name
                        + ", Bairro: " + pl.district
                        + ", X: " + pl.point.x
                        + ", Y: " + pl.point.y;
                    if (pl.carAccess)
                        texto += ", Acesso de carro: SIM" + "\n";
                    else
                        texto += ", Acesso de carro: NÃO" + "\n";
                }

                MessageBox.Show(texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Route_v3
{
    public partial class RouteConsumer : Form
    {
        public RouteConsumer()
        {
            InitializeComponent();
        }

        private void btnRouteTotals3_0_Click(object sender, EventArgs e)
        {
            AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();
            
            string _token = auth.getToken("bgmdod", "!bgm#do!d");

            RouteWS.Route rf = new RouteWS.Route();

            RouteWS.RouteOptions ro = new RouteWS.RouteOptions();
            RouteWS.RouteDetails rd = new RouteWS.RouteDetails();

            rd.optimizeRoute = false;
            rd.routeType = 0;
            rd.descriptionType = 0;

            rd.poiRoute = null;
            rd.barrierPoints = null;
            rd.barriersList = null;

            RouteWS.Vehicle ve = new RouteWS.Vehicle();
            ve.tankCapacity = 50;
            ve.averageConsumption = 7;
            ve.fuelPrice = 2.5;
            ve.averageSpeed = 60;
            ve.tollFeeCat = 2;

            ro.language = "portugues";
            ro.vehicle = ve;
            ro.routeDetails = rd;

            RouteWS.RouteStop[] rs = new RouteWS.RouteStop[2];
            rs[0] = new RouteWS.RouteStop();
            rs[1] = new RouteWS.RouteStop();

            rs[0].description = "Nicaragua";
            RouteWS.Point point = new RouteWS.Point();
            point.x = -49.2411452173913;
            point.y = -25.4008260869565;
            rs[0].point = point;

            rs[1].description = "Henrique Martins Torres";
            RouteWS.Point point1 = new RouteWS.Point();
            point1.x = -49.2499139449541;
            point1.y = -25.5094316513761;
            rs[1].point = point1;

            string texto = "";

            try
            {
                RouteWS.RouteTotals rt = new RouteWS.RouteTotals();

                rt = rf.getRouteTotals(rs, ro, _token);

                texto = "Custo total: " + rt.totalCost.ToString() + "\n";
                texto += "Distancia Total: " + rt.totalDistance.ToString() + "\n";
                texto += "Custo total do combustivel: " + rt.totalfuelCost.ToString() + "\n";
                texto += "Combustível gasto: " + rt.totalFuelUsed.ToString() + "\n";
                texto += "Tempo total: " + rt.totalTime.ToString() + "\n";
                texto += "Gasto com pedágio: " + rt.totaltollFeeCost + "\n";

                MessageBox.Show(texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message, "Erro");
            }
        }

        private void btnGetRoute3_0_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                RouteWS.Route rf = new RouteWS.Route();

                RouteWS.RouteDetails rd = new RouteWS.RouteDetails();
                rd.optimizeRoute = false;
                rd.routeType = 0;
                rd.descriptionType = 0;

                rd.poiRoute = null;
                rd.barrierPoints = null;
                rd.barriersList = null;

                RouteWS.Vehicle ve = new RouteWS.Vehicle();
                ve.tankCapacity = 64;
                ve.averageConsumption = 10;
                ve.fuelPrice = 2.15;
                ve.averageSpeed = 30;
                ve.tollFeeCat = 10;

                ve.tankCapacity = 1;
                ve.averageConsumption = 1;
                ve.fuelPrice = 1;
                ve.averageSpeed = 60;
                ve.tollFeeCat = 8;

                RouteWS.RouteOptions ro = new RouteWS.RouteOptions();
                ro.language = "portugues";
                ro.vehicle = ve;
                ro.routeDetails = rd;

                RouteWS.RouteStop[] rs = new RouteWS.RouteStop[2];
                rs[0] = new RouteWS.RouteStop();
                rs[1] = new RouteWS.RouteStop();

                rs[0].description = "1";
                RouteWS.Point point = new RouteWS.Point();
                point.x = -46.5747235820896;
                point.y = -23.6343517910448;
                rs[0].point = point;

                rs[1].description = "2";
                RouteWS.Point point1 = new RouteWS.Point();
                point1.x = -46.5669571428571;
                point1.y = -23.6290014285714;
                rs[1].point = point1;

                RouteWS.RouteInfo ri = new RouteWS.RouteInfo();

                ri = rf.getRoute(rs, ro, _token);

                string texto = "*** Segment Description";

                foreach (RouteWS.SegmentDescription s in ri.segDescription)
                {
                    texto += "\r\nComando: " + s.command
                        + ", Cidade: " + s.city.name
                        + ", Descricao: " + s.description;
                    if (s.tollFee != null)
                        texto += ", tollDetails: " + s.tollFeeDetails.name
                            + "-" + s.tollFeeDetails.address
                            + "-" + s.tollFeeDetails.direction
                            + "-" + s.tollFeeDetails.concession
                            + "-" + s.tollFeeDetails.phone
                            + "-" + s.tollFeeDetails.state
                            + "-Preço:" + s.tollFeeDetails.price.ToString()
                            + "-Preço por eixo:" + s.tollFeeDetails.pricePerAxle.ToString();

                    texto += ", Distancia acumulada: " + s.cumulativeDistance.ToString()
                        + ", Distancia: " + s.distance.ToString()
                        + ", Tipo da estrada: " + s.roadType;
                }

                texto += "\r\n*** Route Summary";
                foreach (RouteWS.RouteSummary s in ri.routeSummary)
                    texto += "\r\nDescricao: " + s.description
                        + ", Distancia: " + s.distance.ToString()
                        + ", x: " + s.point.x.ToString()
                        + ", y: " + s.point.y.ToString();

                texto += "\r\n*** Route Totals"
                    + "|| Custo total: " + ri.routeTotals.totalCost.ToString()
                    + ", Distancia Total: " + ri.routeTotals.totalDistance.ToString()
                    + ", Custo total do combustivel: " + ri.routeTotals.totalfuelCost.ToString()
                    + ", Combustível gasto:" + ri.routeTotals.totalFuelUsed.ToString()
                    + ", Tempo total:" + ri.routeTotals.totalTime.ToString()
                    + ", Gasto com pedágio: " + ri.routeTotals.totaltollFeeCost.ToString();


                texto += "\r\n*** Road Type:"
                    + "|| twoLaneHighway: " + ri.roadType.twoLaneHighway.ToString()
                    + ", secondLaneUnderConstruction: " + ri.roadType.secondLaneUnderConstruction.ToString()
                    + ", oneLaneRoadway: " + ri.roadType.oneLaneRoadway.ToString()
                    + ", pavingWorkInProgress: " + ri.roadType.pavingWorkInProgress.ToString()
                    + ", dirtRoad: " + ri.roadType.dirtRoad.ToString()
                    + ", roadwayInPoorConditions: " + ri.roadType.roadwayInPoorConditions.ToString()
                    + ", ferry: " + ri.roadType.ferry.ToString();

                MessageBox.Show(" IDROTA = " + ri.routeId.ToString());
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message, "Erro");
            }
        }

        private void btnRouteDescription3_0_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                RouteWS.Route rf = new RouteWS.Route();

                RouteWS.RouteOptions ro = new RouteWS.RouteOptions();
                RouteWS.RouteDetails rd = new RouteWS.RouteDetails();

                rd.optimizeRoute = false;
                rd.routeType = 1;
                rd.descriptionType = 0;
                rd.poiRoute = null;
                rd.barrierPoints = null;
                rd.barriersList = null;

                RouteWS.Vehicle ve = new RouteWS.Vehicle();
                ve.tankCapacity = 50;
                ve.averageConsumption = 10;
                ve.fuelPrice = 2.5;
                ve.averageSpeed = 60;
                ve.tollFeeCat = 0;

                ro.language = "portugues";
                ro.vehicle = ve;
                ro.routeDetails = rd;

                RouteWS.RouteStop[] rs = new RouteWS.RouteStop[2];
                rs[0] = new RouteWS.RouteStop();
                rs[1] = new RouteWS.RouteStop();

                rs[0].description = "Nicaragua";
                RouteWS.Point point = new RouteWS.Point();
                point.x = -49.2411452173913;
                point.y = -25.4008260869565;
                rs[0].point = point;

                rs[1].description = "Henrique Martins Torres";
                RouteWS.Point point1 = new RouteWS.Point();
                point1.x = -49.2499139449541;
                point1.y = -25.5094316513761;
                rs[1].point = point1;

                RouteWS.SegmentDescription[] seg = rf.getRouteDescription(rs, ro, _token);

                string texto = "";

                foreach (RouteWS.SegmentDescription s in seg)
                {
                    texto += "\r\nComando: " + s.command
                        + ",\n Cidade: " + s.city.name
                        + ",\n Descricao: " + s.description;
                    if (s.tollFee != null)
                        texto += ",\n tollDetails: " + s.tollFeeDetails.name
                            + "-" + s.tollFeeDetails.address + "\n"
                            + "-" + s.tollFeeDetails.direction + "\n"
                            + "-" + s.tollFeeDetails.concession + "\n"
                            + "-" + s.tollFeeDetails.phone + "\n"
                            + "-" + s.tollFeeDetails.state + "\n"
                            + "-Preço:" + s.tollFeeDetails.price.ToString() + "\n"
                            + "-Preço por eixo:" + s.tollFeeDetails.pricePerAxle.ToString();

                    texto += ",\n Distancia acumulada: " + s.cumulativeDistance.ToString()
                        + ",\n Distancia: " + s.distance.ToString()
                        + ",\n Tipo da estrada: " + s.roadType + "\n";
                }

                MessageBox.Show(texto);
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message, "Erro");
            }
        }

        private void btnRouteSummary3_0_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                RouteWS.Route rf = new RouteWS.Route();

                RouteWS.RouteOptions ro = new RouteWS.RouteOptions();
                RouteWS.RouteDetails rd = new RouteWS.RouteDetails();

                rd.optimizeRoute = false;
                rd.routeType = 0;
                rd.descriptionType = 0;

                rd.poiRoute = null;
                rd.barrierPoints = null;
                rd.barriersList = null;

                RouteWS.Vehicle ve = new RouteWS.Vehicle();
                ve.tankCapacity = 64;
                ve.averageConsumption = 10;
                ve.fuelPrice = 2.15;
                ve.averageSpeed = 70;
                ve.tollFeeCat = 2;

                ro.language = "portugues";
                ro.vehicle = ve;
                ro.routeDetails = rd;

                RouteWS.RouteStop[] rs = new RouteWS.RouteStop[2];
                rs[0] = new RouteWS.RouteStop();
                rs[1] = new RouteWS.RouteStop();

                rs[0].description = "Nicaragua";
                RouteWS.Point point = new RouteWS.Point();
                point.x = -49.2411452173913;
                point.y = -25.4008260869565;
                rs[0].point = point;

                rs[1].description = "Henrique Martins Torres";
                RouteWS.Point point1 = new RouteWS.Point();
                point1.x = -49.2499139449541;
                point1.y = -25.5094316513761;
                rs[1].point = point1;

                RouteWS.RouteSummary[] sum = rf.getRouteSummary(rs, ro, _token);
                
                string texto = "";

                foreach (RouteWS.RouteSummary s in sum)
                    texto
                        += "\r\ndescription: " + s.description
                        + ", distance: " + s.distance.ToString();

                MessageBox.Show(texto);

            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message, "Erro");
            }
        }

        private void btnGetRouteWithMap3_0_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string _token = auth.getToken("bgmdod", "!bgm#do!d");

                RouteWS.Route rf = new RouteWS.Route();

                RouteWS.RouteDetails rd = new RouteWS.RouteDetails();
                rd.optimizeRoute = false;
                rd.routeType = 1;
                rd.descriptionType = 0;

                string[] poiRoute = new string[1];
                RouteWS.Point[] barrierPoint = new RouteWS.Point[2];

                barrierPoint[0] = new RouteWS.Point();
                barrierPoint[0].x = -49.2476611009174;
                barrierPoint[0].y = -25.5115095412844;

                barrierPoint[1] = new RouteWS.Point();
                barrierPoint[1].x = -49.2450674358974;
                barrierPoint[1].y = -25.5129256410256;

                rd.poiRoute = null;
                rd.barrierPoints = barrierPoint;
                rd.barriersList = null;

                RouteWS.Vehicle ve = new RouteWS.Vehicle();
                ve.tankCapacity = 0;
                ve.averageConsumption = 0;
                ve.fuelPrice = 0;
                ve.averageSpeed = 60;
                ve.tollFeeCat = 10;

                RouteWS.MapSize mis = new RouteWS.MapSize();
                mis.height = 600;
                mis.width = 800;

                RouteWS.MapOptions mio = new RouteWS.MapOptions();
                mio.scaleBar = true;
                mio.mapSize = mis;

                RouteWS.RouteOptions ro = new RouteWS.RouteOptions();
                ro.language = "portugues";
                ro.vehicle = ve;
                ro.routeDetails = rd;
                
                RouteWS.RouteLine[] rl = new Route_v3.RouteWS.RouteLine[2];
                rl[0] = new Route_v3.RouteWS.RouteLine();
                rl[1] = new Route_v3.RouteWS.RouteLine();

                rl[0].RGB = "0,255,0";
                rl[0].transparency = 0.6;
                rl[0].width = 9;

                rl[1].RGB = "0,192,255";
                rl[1].transparency = 1;
                rl[1].width = 3;

                ro.routeLine = rl;

                RouteWS.RouteStop[] rs = new RouteWS.RouteStop[2];
                rs[0] = new RouteWS.RouteStop();
                rs[1] = new RouteWS.RouteStop();

                rs[0].description = "R. ANGÉLICA MARIA TABORDA SANTOS";
                RouteWS.Point point = new RouteWS.Point();
                point.x = -49.2415237614679;
                point.y = -25.5129955045872;
                rs[0].point = point;

                rs[1].description = "Henrique Martins Torres";
                RouteWS.Point point1 = new RouteWS.Point();
                point1.x = -49.2487510656925;
                point1.y = -25.5118991036895;
                rs[1].point = point1;

                RouteWS.RouteInfo ri = new RouteWS.RouteInfo();

                ri = rf.getRouteWithMap(rs, ro, mio, _token);

                string texto = "*** Segment Description";

                foreach (RouteWS.SegmentDescription s in ri.segDescription)
                {
                    texto += "\r\nComando: " + s.command
                        + ", Cidade: " + s.city.name
                        + ", Descricao: " + s.description;
                    if (s.tollFee != null)
                        texto += ", tollDetails: " + s.tollFeeDetails.name
                            + "-" + s.tollFeeDetails.address
                            + "-" + s.tollFeeDetails.direction
                            + "-" + s.tollFeeDetails.concession
                            + "-" + s.tollFeeDetails.phone
                            + "-" + s.tollFeeDetails.state
                            + "-Preço:" + s.tollFeeDetails.price.ToString()
                            + "-Preço por eixo:" + s.tollFeeDetails.pricePerAxle.ToString();

                    texto += ", Distância acumulada: " + s.cumulativeDistance.ToString()
                        + ", Distância: " + s.distance.ToString()
                        + ", Tipo da estrada: " + s.roadType;
                }

                texto += "\r\n*** Route Summary";

                foreach (RouteWS.RouteSummary s in ri.routeSummary)
                {
                    texto += "\nDescricao: " + s.description
                        + ", Distância: " + s.distance.ToString()
                        + ", x: " + s.point.x.ToString()
                        + ", y: " + s.point.y.ToString();
                }

                texto += "\r\n*** Route Totals"
                    + "\r\nCusto total: " + ri.routeTotals.totalCost.ToString()
                    + "\r\nDistancia Total: " + ri.routeTotals.totalDistance.ToString()
                    + "\r\nCusto total do combustivel: " + ri.routeTotals.totalfuelCost.ToString()
                    + "\r\nCombustível gasto:" + ri.routeTotals.totalFuelUsed.ToString()
                    + "\r\nTempo total:" + ri.routeTotals.totalTime.ToString()
                    + "\r\nGasto com pedágio: " + ri.routeTotals.totaltollFeeCost.ToString();


                texto += "\r\n*** Road Type:"
                    + "\r\ntwoLaneHighway: " + ri.roadType.twoLaneHighway.ToString()
                    + "\r\nsecondLaneUnderConstruction: " + ri.roadType.secondLaneUnderConstruction.ToString()
                    + "\r\noneLaneRoadway: " + ri.roadType.oneLaneRoadway.ToString()
                    + "\r\npavingWorkInProgress: " + ri.roadType.pavingWorkInProgress.ToString()
                    + "\r\ndirtRoad: " + ri.roadType.dirtRoad.ToString()
                    + "\r\nroadwayInPoorConditions: " + ri.roadType.roadwayInPoorConditions.ToString()
                    + "\r\nferry: " + ri.roadType.ferry.ToString();

                MessageBox.Show(texto + "\nMAPA = " + ri.MapInfo.url + " - IDROTA = " + ri.routeId + "\n\n" +
                    "XMAX = " + ri.MapInfo.extent.XMax.ToString() + "\r\n" +
                    "XMIN = " + ri.MapInfo.extent.XMin.ToString() + "\r\n" +
                    "YMAX = " + ri.MapInfo.extent.YMax.ToString() + "\r\n" +
                    "YMIN = " + ri.MapInfo.extent.YMin.ToString() + "\r\n");

            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message, "Erro");
            }
        }
    }
}
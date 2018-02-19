using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Traffic_v1
{
    public partial class TrafficConsumer : Form
    {
        public TrafficConsumer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string token = auth.getToken("bgmdod", "!bgm#do!d");

                TrafficWS.City cidade = new TrafficWS.City();
                cidade.name = EdtCidade.Text;
                cidade.state = EdtUf.Text; 

                TrafficWS.ResultRange rr = new Traffic_v1.TrafficWS.ResultRange();
                rr.pageIndex = Convert.ToInt16(EdtPag.Text);
                rr.recordsPerPage = Convert.ToInt16(EdtRegPag.Text);

                TrafficWS.Traffic tf = new TrafficWS.Traffic();

                TrafficWS.TrafficInfo result = tf.getInfo(cidade, rr, token);

                int recordCount = result.recordCount;
                string aux = "";

                LbCount.Text = recordCount.ToString();

                listBox1.Items.Clear();  

                for (int i = 0; i < recordCount; i++)
                {
                    aux = result.info[i].date + ", " +
                        result.info[i].time + ", " +
                        result.info[i].situation + ", " +
                        result.info[i].km + ", " +
                        result.info[i].city.state + ", " +
                        result.info[i].city.name;
                    
                    listBox1.Items.Add(aux); 
                }

                tf.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
             

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if ((EdtUf.Text != string.Empty) && (EdtCidade.Text != string.Empty))
                {

                    AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                    string token = auth.getToken("bgmdod", "!bgm#do!d");

                    TrafficWS.City cidade = new TrafficWS.City();
                    cidade.name = EdtCidade.Text;
                    cidade.state = EdtUf.Text;

                    TrafficWS.ResultRange rr = new Traffic_v1.TrafficWS.ResultRange();
                    rr.pageIndex = Convert.ToInt16(EdtPag.Text);
                    rr.recordsPerPage = Convert.ToInt16(EdtRegPag.Text);

                    TrafficWS.Traffic tf = new TrafficWS.Traffic();

                    TrafficWS.CorridorInfo result = tf.getCorridor(cidade, rr, token);

                    int recordCount = result.recordCount;
                    string aux = "";

                    LbCount.Text = recordCount.ToString();   

                    listBox1.Items.Clear();

                    for (int i = 0; i < recordCount; i++)
                    {
                        aux = result.corridor[i].codCorridor.ToString() + ", " +
                                result.corridor[i].nameCorridor.ToString() + ", " +
                                result.corridor[i].levelCongested.ToString() + ", " +
                                result.corridor[i].city.state + ", " +
                                result.corridor[i].city.name + ", " +
                                result.corridor[i].point.x.ToString() + ", " +
                                result.corridor[i].point.y.ToString() + ", ";

                        listBox1.Items.Add(aux);
                    }

                    tf.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if ((EdtUf.Text != string.Empty) && (EdtCidade.Text != string.Empty) && (EdtIdCorredor.Text != string.Empty ))
                {

                    AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                    string token = auth.getToken("bgmdod", "!bgm#do!d");

                    TrafficWS.City cidade = new TrafficWS.City();
                    cidade.name = EdtCidade.Text;
                    cidade.state = EdtUf.Text;

                    TrafficWS.ResultRange rr = new Traffic_v1.TrafficWS.ResultRange();
                    rr.pageIndex = Convert.ToInt16(EdtPag.Text);
                    rr.recordsPerPage = Convert.ToInt16(EdtRegPag.Text);

                    TrafficWS.Traffic tf = new TrafficWS.Traffic();

                    TrafficWS.ExcerptInfo result = tf.getExcerpt(cidade, Convert.ToInt16(EdtIdCorredor.Text), rr, token); 

                    int recordCount = result.recordCount;
                    string aux = "";

                    LbCount.Text = recordCount.ToString();

                    listBox1.Items.Clear();

                    for (int i = 0; i < recordCount; i++)
                    {
                        aux = result.excerpt[i].codCorridor.ToString() + ", " +
                                result.excerpt[i].codExcerpt.ToString() + ", " +
                                result.excerpt[i].nameExcerpt + ", SENTIDO - " +
                                result.excerpt[i].direction + ", " +
                                result.excerpt[i].levelCongested.ToString() + ", " +
                                result.excerpt[i].city.state + ", " +
                                result.excerpt[i].city.name + ", " +
                                result.excerpt[i].point.x.ToString() + ", " +
                                result.excerpt[i].point.y.ToString() + ", " +
                                result.excerpt[i].distance.ToString() + ", " +
                                result.excerpt[i].order.ToString();  

                        listBox1.Items.Add(aux);
                    }

                    tf.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if ((EdtUf.Text != string.Empty) && (EdtCidade.Text != string.Empty) && (EdtIdCorredor.Text != string.Empty))
                {

                    AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                    string token = auth.getToken("bgmdod", "!bgm#do!d");

                    TrafficWS.City cidade = new TrafficWS.City();
                    cidade.name = EdtCidade.Text;
                    cidade.state = EdtUf.Text;

                    TrafficWS.ResultRange rr = new Traffic_v1.TrafficWS.ResultRange();
                    rr.pageIndex = Convert.ToInt16(EdtPag.Text);
                    rr.recordsPerPage = Convert.ToInt16(EdtRegPag.Text);

                    TrafficWS.Traffic tf = new TrafficWS.Traffic();

                    TrafficWS.ExcerptInfo result = tf.getExcerptsCongested(cidade, Convert.ToInt16(EdtIdCorredor.Text), rr, token);

                    int recordCount = result.recordCount;
                    string aux = "";

                    LbCount.Text = recordCount.ToString();

                    listBox1.Items.Clear();

                    for (int i = 0; i < recordCount; i++)
                    {
                        aux = result.excerpt[i].codCorridor.ToString() + ", " +
                                result.excerpt[i].codExcerpt.ToString() + ", " +
                                result.excerpt[i].nameExcerpt + ", SENTIDO - " +
                                result.excerpt[i].direction + ", " +
                                result.excerpt[i].levelCongested.ToString() + ", " +
                                result.excerpt[i].city.state + ", " +
                                result.excerpt[i].city.name + ", " +
                                result.excerpt[i].point.x.ToString() + ", " +
                                result.excerpt[i].point.y.ToString() + ", " +
                                result.excerpt[i].distance.ToString() + ", " +
                                result.excerpt[i].order.ToString();

                        listBox1.Items.Add(aux);
                    }

                    tf.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                string token = auth.getToken("bgmdod", "!bgm#do!d");

                TrafficWS.City cidade = new TrafficWS.City();
                cidade.name = EdtCidade.Text;
                cidade.state = EdtUf.Text;

                TrafficWS.Traffic tf = new TrafficWS.Traffic();

                TrafficWS.CorridorInfo result = tf.getAllCongested(cidade, token); 

                int recordCount = result.recordCount;
                int recordCount2 = 0;
                string aux = "";

                LbCount.Text = recordCount.ToString();

                listBox1.Items.Clear();

                for (int i = 0; i < recordCount; i++)
                {
                    aux = result.corridor[i].codCorridor.ToString() + ", " +
                            result.corridor[i].nameCorridor.ToString() + ", " +
                            result.corridor[i].levelCongested.ToString() + ", " +
                            result.corridor[i].city.state + ", " +
                            result.corridor[i].city.name + ", " +
                            result.corridor[i].point.x.ToString() + ", " +
                            result.corridor[i].point.y.ToString() + ", ";

                    listBox1.Items.Add(aux);

                    recordCount2 = result.corridor[i].excerptInfo.recordCount;

                    for (int j = 0; j < recordCount2; j++)
                    {
                        aux = "    " + result.corridor[i].excerptInfo.excerpt[j].nameExcerpt + ", SENTIDO - " +
                            result.corridor[i].excerptInfo.excerpt[j].direction + ", " +
                            result.corridor[i].excerptInfo.excerpt[j].distance.ToString() + ", " +
                            result.corridor[i].excerptInfo.excerpt[j].order.ToString();

                        listBox1.Items.Add(aux);

                    }

                }

                tf.Dispose();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                if ((EdtUf.Text != string.Empty) && (EdtCidade.Text != string.Empty))
                {

                    AuthenticationWS.Authentication auth = new AuthenticationWS.Authentication();

                    string token = auth.getToken("bgmdod", "!bgm#do!d");

                    TrafficWS.City cidade = new TrafficWS.City();
                    cidade.name = EdtCidade.Text;
                    cidade.state = EdtUf.Text;

                    TrafficWS.ResultRange rr = new Traffic_v1.TrafficWS.ResultRange();
                    rr.pageIndex = Convert.ToInt16(EdtPag.Text);
                    rr.recordsPerPage = Convert.ToInt16(EdtRegPag.Text);

                    TrafficWS.Traffic tf = new TrafficWS.Traffic();

                    TrafficWS.CorridorInfo result = tf.getCorridorsCongested(cidade, rr, token);

                    int recordCount = result.recordCount;
                    string aux = "";

                    LbCount.Text = recordCount.ToString();

                    listBox1.Items.Clear();

                    for (int i = 0; i < recordCount; i++)
                    {
                        aux = result.corridor[i].codCorridor.ToString() + ", " +
                                result.corridor[i].nameCorridor.ToString() + ", " +
                                result.corridor[i].levelCongested.ToString() + ", " +
                                result.corridor[i].city.state + ", " +
                                result.corridor[i].city.name + ", " +
                                result.corridor[i].point.x.ToString() + ", " +
                                result.corridor[i].point.y.ToString() + ", ";

                        listBox1.Items.Add(aux);
                    }

                    tf.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
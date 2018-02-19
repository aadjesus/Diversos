using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Globus5.WPF.Comum;
using Globus5.WPF.Comum.ws.gestaoDeFrotaOnline;
using Globus5.WPF.Comum.ws.escala;
using Globus5.WPF.Comum.ws.frota;
using Globus5.WPF.Comum.ws.globus;
using System.Threading;
using System.Threading.Tasks;

namespace Globus5.WPF.Sistemas.GestaoDeFrotaOnline.Resultados
{
    public partial class HistoricoDeDeteccoes : Form
    {
        private void popularParemtrsos()
        {
            _listaCodIntLinha = new int[] { 26565, 26566, 26569 };

            #region Veiculos

            _listaCodigoVeic = new int[] {                           	7419	
                          ,	7699	
                          ,	7690	
                          ,	7713	
                          ,	7833	
                          ,	7681	
                          ,	7680	
                          ,	7694	
                          ,	7674	
                          ,	7700	
                           ,	3758	
                           ,	8749	
                           ,	8729	
                           ,	8754	
                           ,	8732	
                           ,	8756	
                           ,	8757	
                           ,	8772	
                           ,	8733	
                           ,	8758	
                           ,	8792	
                           ,	8793	
                           ,	8750	
                           ,	8730	
                           ,	8759	
                           ,	8760	
                           ,	8761	
                           ,	8765	
                           ,	8731	
                           ,	8734	
                           ,	8762	
                           ,	8774	
                           ,	8778	
                           ,	8771	
                           ,	8766	
                           ,	8763	
                           ,	8764	
                           ,	8770	
                           ,	8769	
                           ,	8768	
                           ,	8817	
                           ,	8767	
                           ,	8794	
                           ,	8775	
                           ,	8797	
                           ,	8773	
                           ,	8781	
                           ,	8777	
                           ,	8776	
                           ,	8802	
                           ,	8779	
                           ,	8803	
                           ,	8826	
                           ,	8782	
                           ,	8804	
                           ,	8795	
                           ,	8798	
                           ,	8783	
                           ,	8796	
                           ,	8799	
                           ,	8784	
                           ,	9877	
                           ,	9910	
                           ,	9878	
                           ,	9901	
                           ,	9895	
                           ,	9913	
                           ,	9918	
                           ,	9919	
                           ,	9914	
                           ,	9905	
                           ,	9885	
                           ,	9896	
                           ,	9915	
                           ,	9912	
                           ,	9900	
                           ,	10082	
                           ,	10057	
                           ,	10178	
                           ,	10044	
                           ,	10046	
                           ,	10058	
                           ,	10032	
                           ,	10024	
                           ,	10061	
                           ,	10055	
                           ,	10036	
                           ,	10037	
                           ,	10088	
                           ,	10167	
                           ,	10087	
                           ,	10086	
                           ,	10174	
                           ,	10162	
                           ,	10159	
                           ,	10182	
                           ,	10085	
                           ,	10084	
                           ,	10161	
                           ,	10083	
                            ,	10172	
                            ,	10200	
                            ,	10197	
                            ,	10177	
                            ,	10158	
                            ,	10171	
                            ,	10170	
                            ,	10154	
                            ,	10181	
                            ,	10153	
                            ,	10155	
                            ,	10194	
                            ,	10185	
                            ,	10191	
                            ,	10180	
                            ,	10157	
                            ,	10189	
                            ,	10152	
                            ,	10187	
                            ,	6198	
                            ,	6194	
                            ,	6192	
                            ,	6281	
                            ,	6282	
                            ,	6316	
                            ,	6283	
                            ,	6284	
                            ,	6285	
                            ,	6286	
                            ,	6315	
                            ,	6287	
                            ,	6288	
                            ,	6289	
                            ,	6290	
                            ,	6291	
                            ,	6292	
                            ,	6293	
                            ,	6524	
                            ,	6517	
                            ,	6516	
                            ,	6526	
                            ,	6565	
                            ,	6569	
                            ,	6593	
                            ,	6590	
                            ,	6588	
                            ,	9465	
                            ,	9570	
                            ,	9506	
                            ,	9507	
                            ,	9499	
                            ,	9508	
                            ,	9509	
                            ,	9510	
                            ,	9500	
                            ,	9501	
                            ,	9511	
                            ,	9512	
                            ,	9513	
                            ,	9514	
                            ,	9520	
                            ,	9515	
                            ,	9516	
                            ,	9517	
                            ,	9518	
                            ,	9519	
                            ,	9531	
                            ,	9532	
                            ,	9533	
                            ,	9534	
                            ,	9541	
                            ,	9542	
                            ,	9571	
                            ,	9543	
                            ,	9544	
                            ,	9572	
                            ,	9577	
                            ,	9678	
                            ,	9578	
                            ,	10318	
                            ,	10325	
                            ,	10343	
                            ,	10316	
                            ,	10341	
                            ,	10317	
                            ,	10329	
                            ,	10321	
                            ,	10324	
                            ,	10320	
                            ,	10323	
                            ,	10395	
                            ,	10322	
                            ,	10326	
                            ,	10328	
                            ,	10327	
                            ,	10337	
                            ,	10364	
                            ,	10338	
                            ,	10361	
                            ,	10388	
                            ,	10340	
                            ,	10386	
                            ,	10342	
                            ,	10387	
                            ,	10357	
                            ,	10363	
                            ,	10371	
                            ,	10372	
                            ,	1679	
                            ,	1680	
                            ,	1681	
                            ,	1682	
                            ,	1683	
                            ,	1684	
                            ,	4641	
                            ,	4628	
                            ,	4646	
                            ,	4604	
                            ,	4649	
                            ,	5623	
                            ,	5700	
                            ,	5843	
                            ,	6561	
                            ,	6539	
                            ,	6546	
                            ,	6537	
                            ,	6538	
                            ,	6544	
                            ,	6543	
                            ,	6548	
                            ,	6554	
                            ,	6564	
                            ,	6540	
                            ,	6552	
                            ,	6549	
                            ,	6541	
                            ,	6553	
                            ,	6644	
                            ,	6634	
                            ,	6636	
                            ,	6638	
                            ,	6651	
                            ,	6647	
                            ,	6660	
                            ,	6645	
                            ,	6648	
                            ,	6655	
                            ,	6657	
                            ,	6658	
                            ,	6659	
                            ,	6662	
                            ,	6661	
                            ,	6669	
                            ,	6670	
                            ,	6668	
                            ,	6681	
                            ,	6671	
                            ,	6682	
                            ,	6680	
                            ,	6684	
                            ,	6685	
                            ,	6689	
                            ,	6683	
                            ,	6687	
                            ,	6686	
                            ,	6710	
                            ,	6711	
                            ,	6706	
                            ,	6693	
                            ,	6845	
                            ,	6763	
                            ,	7118	
                            ,	7119	
                            ,	7120	
                            ,	7121	
                            ,	7122	
                            ,	7123	
                            ,	7124	
                            ,	7127	
                            ,	7128	
                            ,	7129	
                            ,	7130	
                            ,	7131	
                            ,	7133	
                            ,	7134	
                            ,	7135	
                            ,	10260	
                            ,	10261	
                            ,	2586	
                            ,	5283	
                            ,	2588	
                            ,	3987	
                            ,	6925	
                            ,	7174	
                            ,	7080	
                            ,	7857	
                            ,	8524	
                            ,	10339	
                            ,	9767	
                            ,	7474	
                            ,	10010	
                            ,	9027	
                            ,	10012	
                            ,	10011	
                            ,	10009	
                            ,	7821	
                            ,	7675	
                            ,	7706	
                            ,	7489	
                            ,	7486	
                            ,	7815	
                            ,	7411	
                            ,	7837	
                            ,	7835	
                            ,	7608	
                            ,	7418	
                            ,	7840	
                            ,	7453	
                            ,	7423	
                            ,	7410	
                            ,	7413	
                            ,	7587	
                            ,	7414	
                            ,	7415	
                            ,	7842	
                            ,	7412	
                            ,	7490	
                            ,	7487	
                            ,	7839	
                            ,	7838	
                            ,	7416	
                            ,	7844	
                            ,	7693	
                            ,	7583	
                            ,	9021	
                            ,	7845	
                            ,	7451	
                            ,	7691	
                            ,	7695	
                            ,	7491	
                            ,	7673	
                            ,	7612	
                            ,	7417	
                            ,	7834	
                            ,	7420	
                            ,	7843	
                            ,	7841	
                            ,	8978	
                            ,	8800	
                            ,	8827	
                            ,	8780	
                            ,	8801	
                            ,	8828	
                            ,	8809	
                            ,	8829	
                            ,	8822	
                            ,	8810	
                            ,	8830	
                            ,	8818	
                            ,	8823	
                            ,	8811	
                            ,	8819	
                            ,	8820	
                            ,	8821	
                            ,	8831	
                            ,	8832	
                            ,	8824	
                            ,	8833	
                            ,	8834	
                            ,	8835	
                            ,	8836	
                            ,	8812	
                            ,	8813	
                            ,	8825	
                            ,	8837	
                            ,	8838	
                            ,	8917	
                            ,	8899	
                            ,	8902	
                            ,	8918	
                            ,	8906	
                            ,	8925	
                            ,	8907	
                            ,	8922	
                            ,	8903	
                            ,	8908	
                            ,	8919	
                            ,	8924	
                            ,	8920	
                            ,	8909	
                            ,	8910	
                            ,	8914	
                            ,	8921	
                            ,	8900	
                            ,	8911	
                            ,	8901	
                            ,	8904	
                            ,	10034	
                            ,	10029	
                            ,	10033	
                            ,	10050	
                            ,	10059	
                            ,	10035	
                            ,	10042	
                            ,	10062	
                            ,	10019	
                            ,	10060	
                            ,	10130	
                            ,	10038	
                            ,	10063	
                            ,	10064	
                            ,	10048	
                            ,	10047	
                            ,	10020	
                            ,	10065	
                            ,	10051	
                            ,	10066	
                            ,	10067	
                            ,	10028	
                            ,	10021	
                            ,	10045	
                            ,	10080	
                            ,	10031	
                            ,	10022	
                            ,	10075	
                            ,	10052	
                            ,	10039	
                            ,	10025	
                            ,	10027	
                            ,	10049	
                            ,	10040	
                            ,	10077	
                            ,	10074	
                            ,	10023	
                            ,	10056	
                            ,	10041	
                            ,	10043	
                            ,	10076	
                            ,	10018	
                            ,	10026	
                            ,	10053	
                            ,	10054	
                            ,	10073	
                            ,	10072	
                            ,	10030	
                            ,	8915	
                            ,	8912	
                            ,	8905	
                            ,	8897	
                            ,	8898	
                            ,	8923	
                            ,	8927	
                            ,	8913	
                            ,	8926	
                            ,	1500	
                            ,	1513	
                            ,	7861	
                            ,	4655	
                            ,	4657	
                            ,	4718	
                            ,	6545	
                            ,	6542	
                            ,	6547	
                            ,	6563	
                            ,	6585	
                            ,	6562	
                            ,	6550	
                            ,	6551	
                            ,	6568	
                            ,	6572	
                            ,	6592	
                            ,	6567	
                            ,	6566	
                            ,	6589	
                            ,	6591	
                            ,	6594	
                            ,	6587	
                            ,	6570	
                            ,	6613	
                            ,	6614	
                            ,	6611	
                            ,	6612	
                            ,	6616	
                            ,	6615	
                            ,	6617	
                            ,	6620	
                            ,	6618	
                            ,	6619	
                            ,	6653	
                            ,	6621	
                            ,	6649	
                            ,	6623	
                            ,	6622	
                            ,	6627	
                            ,	6641	
                            ,	6626	
                            ,	6628	
                            ,	6629	
                            ,	6630	
                            ,	6639	
                            ,	6646	
                            ,	6640	
                            ,	6642	
                            ,	6650	
                            ,	6637	
                            ,	6635	
                            ,	6643	
                            ,	6652	
                            ,	6656	
                            ,	10069	
                            ,	10235	
                            ,	10008	
                            ,	10013	
                            ,	9709	
                            ,	8443	
                            ,	8529	
                            ,	8696	
 };
            #endregion

            #region local

            _listaCodLocalidade = new int[] {1	
		                                ,	2	
		                                ,	3	
		                                ,	825	
		                                ,	1046	
		                                ,	1061	
		                                ,	1067	
		                                ,	1068	
		                                ,	1084	
		                                ,	1108	
		                                 ,	1134	
		                                 ,	1140	
		                                 ,	1166	
		                                 ,	1182	
		                                 ,	1184	
		                                 ,	1187	
		                                 ,	1188	
		                                 ,	1189	
		                                 ,	1191	
		                                 ,	1192	
		                                 ,	1195	
		                                 ,	1197	
		                                 ,	1211	
		                                 ,	1217	
		                                 ,	1259	
		                                 ,	1263	
		                                 ,	1264	
		                                 ,	1267	
		                                 ,	1286	
		                                 ,	1288	
		                                 ,	1291	
		                                 ,	1292	
		                                 ,	1294	
		                                 ,	1297	
		                                 ,	1300	
		                                 ,	1304	
		                                 ,	1315	
		                                 ,	1323	
		                                 ,	1329	
		                                 ,	1336	
		                                 ,	1366	
		                                 ,	1433	
		                                 ,	1443	
		                                 ,	1444	
		                                 ,	1448	
		                                 ,	4007	
		                                 ,	4010	
		                                 ,	4014	
		                                 ,	4017	
		                                 ,	4024	
		                                 ,	4025	
		                                 ,	4029	
		                                 ,	4030	
		                                 ,	4031	
		                                 ,	4033	
		                                 ,	4037	
		                                 ,	4038	
		                                 ,	4039	
		                                 ,	4047	
		                                 ,	4052	
		                                 ,	4053	
		                                 ,	4054	
		                                 ,	4060	
		                                 ,	4061	
		                                 ,	4062	
		                                 ,	4063	
		                                 ,	4064	
		                                 ,	4065	
		                                 ,	4068	
		                                 ,	4069	
		                                 ,	4070	
		                                 ,	4071	
		                                 ,	4072	
		                                 ,	4073	
		                                 ,	4074	
		                                 ,	4075	
		                                 ,	4077	
		                                 ,	4078	
		                                 ,	4079	
		                                 ,	4082	
		                                 ,	4085	
		                                 ,	4087	
		                                 ,	4088	
		                                 ,	4093	
		                                 ,	4094	
		                                 ,	4095	
		                                 ,	4096	
		                                 ,	4098	
		                                 ,	4100	
		                                 ,	4101	
		                                 ,	4102	
		                                 ,	4103	
		                                 ,	4104	
		                                 ,	4105	
		                                 ,	4106	
		                                 ,	4107	
		                                 ,	4108	
		                                 ,	4109	
		                                 ,	4112	
		                                 ,	4113	
		                                  ,	4114	
		                                  ,	4115	
		                                  ,	4117	
		                                  ,	4118	
		                                  ,	4122	
		                                  ,	4123	
		                                  ,	4125	
		                                  ,	4126	
		                                  ,	4127	
		                                  ,	4129	
		                                  ,	4131	
		                                  ,	4137	
		                                  ,	4138	
		                                  ,	4140	
		                                  ,	4152	
		                                  ,	4158	
		                                  ,	4167	
		                                  ,	4168	
		                                  ,	4169	
		                                  ,	4170	
		                                  ,	4172	
		                                  ,	4178	
		                                  ,	4181	
		                                  ,	4183	
		                                  ,	4185	
		                                  ,	4186	
		                                  ,	4187	
		                                  ,	4188	
		                                  ,	4189	
		                                  ,	4197	
		                                  ,	4198	
		                                  ,	4284	
		                                  ,	4288	
		                                  ,	4296	
		                                  ,	4297	
		                                  ,	7009	
		                                  ,	7039	
		                                  ,	7054	
		                                  ,	7063	
		                                  ,	7074	
		                                  ,	7076	
		                                  ,	7099	
		                                  ,	7128	
		                                  ,	7132	
		                                  ,	7139	
		                                  ,	7145	
		                                  ,	7156	
		                                  ,	7165	
		                                  ,	7166	
		                                  ,	7187	
		                                  ,	7188	
		                                  ,	7201	
		                                  ,	7203	
		                                  ,	7209	
		                                  ,	8000	
		                                  ,	8003	
		                                  ,	8035	
		                                  ,	8040	
		                                  ,	8041	
		                                  ,	8042	
		                                  ,	8043	
		                                  ,	8058	
		                                  ,	8064	
		                                  ,	8071	
		                                  ,	8075	
		                                  ,	8084	
		                                  ,	8091	
		                                  ,	8106	
		                                  ,	8118	
		                                  ,	8119	
		                                  ,	8125	
		                                  ,	8126	
		                                  ,	8145	
		                                  ,	8158	
		                                  ,	8159	
		                                  ,	8162	
		                                  ,	8255	
		                                  ,	8290	
		                                  ,	8303	
		                                  ,	8304	
		                                  ,	8311	
		                                  ,	8342	
		                                  ,	8353	
		                                  ,	8354	
		                                  ,	8367	
		                                  ,	8444	
		                                  ,	8449	
		                                  ,	8524	
		                                  ,	8547	
		                                  ,	8548	
		                                  ,	8564	
		                                  ,	8594	
		                                  ,	8668	
		                                  ,	8730	
		                                  ,	8740	
		                                  ,	8755	
		                                  ,	8786	
		                                  ,	8796	
		                                  ,	8798	
		                                  ,	8799	
		                                  ,	8923	
		                                  ,	8925	
		                                  ,	8926	
		                                  ,	8927	
		                                  ,	8928	
		                                  ,	8929	
		                                  ,	8930	
		                                  ,	8945	
		                                  ,	8948	
		                                  ,	8951	
		                                  ,	8952	
		                                  ,	8953	
		                                  ,	8955	
		                                  ,	8956	
		                                  ,	8957	
		                                  ,	8958	
		                                  ,	8959	
		                                  ,	8962	
		                                  ,	8963	
		                                  ,	8964	
		                                  ,	8965	
		                                  ,	8966	
		                                  ,	8967	
		                                  ,	8968	
		                                  ,	8969	
		                                  ,	8972	
		                                  ,	8973	
		                                  ,	8974	
		                                  ,	8975	
		                                  ,	8976	
		                                  ,	8979	
		                                  ,	8986	
		                                  ,	8987	
		                                  ,	8988	
		                                  ,	8990	
		                                  ,	8991	
		                                  ,	8992	
		                                  ,	8993	
		                                  ,	8995	
		                                  ,	8997	
		                                  ,	8998	
		                                  ,	8999	
		                                  ,	9001	
		                                  ,	9003	
		                                  ,	9004	
		                                  ,	9005	
		                                  ,	9006	
		                                  ,	9007	
		                                  ,	9009	
		                                  ,	9010	
		                                  ,	9011	
		                                  ,	9012	
		                                  ,	9013	
		                                  ,	9014	
		                                  ,	9015	
		                                  ,	9016	
		                                  ,	9018	
		                                  ,	9019	
		                                  ,	9021	
		                                  ,	9022	
		                                  ,	9023	
		                                  ,	9024	
		                                  ,	9025	
		                                  ,	9026	
		                                  ,	9034	
		                                  ,	9037	
		                                  ,	9039	
		                                  ,	9041	
		                                  ,	9094	
		                                  ,	9095	
		                                  ,	9097	
		                                  ,	9100	
		                                  ,	9103	
		                                  ,	9104	
		                                  ,	9108	
		                                  ,	9112	
		                                  ,	9113	
		                                  ,	9114	
		                                  ,	9115	
		                                  ,	9142	
		                                  ,	9155	
		                                  ,	9156	
		                                  ,	9157	
		                                  ,	9158	
		                                  ,	9172	
		                                  ,	9174	
		                                  ,	9176	
		                                  ,	9185	
		                                  ,	9192	
		                                  ,	9199	
		                                  ,	9215	
		                                  ,	9218	
		                                  ,	9227	
		                                  ,	9229	
		                                  ,	9231	
		                                  ,	9244	
		                                  ,	9247	
		                                  ,	9251	
		                                  ,	9255	
		                                  ,	9270	
		                                  ,	9277	
		                                  ,	9278	
		                                  ,	9281	
		                                  ,	9289	
		                                  ,	9291	
		                                  ,	9297	
		                                  ,	9307	
		                                  ,	9309	
		                                  ,	9311	
		                                  ,	9318	
		                                  ,	9321	
		                                  ,	9323	
		                                  ,	9330	
		                                  ,	9338	
		                                  ,	9370	
		                                  ,	9379	
		                                  ,	9382	
		                                  ,	9392	
		                                  ,	9396	
		                                  ,	9416	
		                                  ,	9426	
		                                  ,	9432	
		                                  ,	9436	
		                                  ,	9437	
		                                  ,	9438	
		                                  ,	9454	
		                                  ,	9458	
		                                  ,	9462	
		                                  ,	9466	
		                                  ,	9470	
		                                  ,	9476	
		                                  ,	9485	
		                                  ,	9489	
		                                  ,	9490	
		                                  ,	9515	
		                                  ,	9521	
		                                  ,	9524	
		                                  ,	9526	
		                                  ,	9529	
		                                  ,	9530	
		                                  ,	9532	
		                                  ,	9553	
		                                  ,	9555	
		                                  ,	9558	
		                                  ,	9560	
		                                  ,	9561	
		                                  ,	9574	
		                                  ,	9580	
		                                  ,	9583	
		                                  ,	9584	
		                                  ,	9586	
		                                  ,	9588	
		                                  ,	9589	
		                                  ,	9590	
		                                  ,	9598	
		                                  ,	9600	
		                                  ,	9608	
		                                  ,	9621	
		                                  ,	9624	
		                                  ,	9627	
		                                  ,	9631	
		                                  ,	9632	
		                                  ,	9633	
		                                  ,	9639	
		                                  ,	9649	
		                                  ,	9650	
		                                  ,	9651	
		                                  ,	9653	
		                                  ,	9654	
		                                  ,	9658	
		                                  ,	9660	
		                                  ,	9664	
		                                  ,	9666	
		                                  ,	9668	
		                                  ,	9671	
		                                  ,	9707	
		                                  ,	9709	
		                                  ,	9713	
		                                  ,	9716	
		                                  ,	9733	
		                                  ,	9735	
		                                  ,	9745	
		                                  ,	9749	
		                                  ,	9751	
		                                  ,	9752	
		                                  ,	9754	
		                                  ,	9763	
		                                  ,	9765	
		                                  ,	9793	
		                                  ,	9794	
		                                  ,	9795	
		                                  ,	9796	
		                                  ,	9797	
		                                  ,	9798	
		                                  ,	9800	
		                                  ,	9873	
		                                  ,	9877	
		                                  ,	9891	
		                                  ,	9904	
		                                  ,	9909	
		                                  ,	9917	
		                                  ,	9919	
		                                  ,	9934	
		                                  ,	9935	
		                                  ,	9936	
		                                  ,	9982	
		                                  ,	9994	
		                                  ,	9996	
		                                  ,	9997	

 };

            #endregion
        }

        public HistoricoDeDeteccoes()
        {
            InitializeComponent();

            tmEdtDataInicial.Time = new DateTime(2012, 03, 01, 20, 00, 00);
            tmEdtDataFinal.Time = new DateTime(2012, 03, 02, 10, 00, 00);
            this.rdGrpSentido.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem("A", "Ambos"));
            this.rdGrpSentido.SelectedIndex = 0;
            popularParemtrsos();

            _listaLocaisDTO = Globus5.WPF.Comum.WebServices.EscalaWSApp.RetornarLocaisPorCodigos(_listaCodLocalidade).ToArray();
            _listaVeiculoDTO = Globus5.WPF.Comum.WebServices.FrotaWSApp.RetornarTodosVeiculosDaEmpresaFiliais(8, new int[] { 9 });
            _listaLinhasPermitidasParaUsuario = WebServices.GlobusWSApp.RetornarLinhasPermitidasParaUsuarioInibeRelatorio("BGMRODOTEC");
            _listaOcorrenciaValidadorDTO = WebServices.GestaoDeFrotaOnLineWSApp.RetornarOcorrenciasValidador();

            _timerTela.Tick += new EventHandler(TimerTela_Tick);
            _timerTela.Interval = 100;

            elementHost1.Child = usrCtrlRelatorioGerencial1;
            this.usrCtrlRelatorioGerencial1.PopulaHistoricoDeDeteccoes(this);
        }

        private Globus5.WPF.Sistemas.GestaoDeFrotaOnline.UsrCtrlRelatorioGerencial usrCtrlRelatorioGerencial1 = new UsrCtrlRelatorioGerencial();


        private void button1_Click(object sender, EventArgs e)
        {
            //if (bw.IsBusy != true)
            //{
            //    bw.RunWorkerAsync();
            //}

            _timerTela.Start();

        }

        private DateTime _dataInicial;
        private DevExpress.XtraEditors.TimeEdit tmEdtDataInicial = new DevExpress.XtraEditors.TimeEdit();
        private DevExpress.XtraEditors.TimeEdit tmEdtDataFinal = new DevExpress.XtraEditors.TimeEdit();
        private DevExpress.XtraEditors.RadioGroup rdGrpSentido = new DevExpress.XtraEditors.RadioGroup();
        private int[] _listaCodIntLinha;
        private int[] _listaCodigoVeic;
        private int[] _listaCodLocalidade;
        private eHistorico _historico = eHistorico.Gerencial;
        private List<GerencialItinerarioDTO> _listaGerencialItinerarioDTO = new List<GerencialItinerarioDTO>();

        private sLinhasPermitidasParaUsuario[] _listaLinhasPermitidasParaUsuario;
        private OcorrenciaValidadorDTO[] _listaOcorrenciaValidadorDTO;
        private LocaisDTO[] _listaLocaisDTO;
        private VeiculoDTO[] _listaVeiculoDTO;
        private System.Windows.Forms.Timer _timerTela = new System.Windows.Forms.Timer();
        private Thread _threadConsolidado;

        public enum eHistorico
        {
            /// <summary>
            /// Histórico de detecções
            /// </summary>
            Deteccoes,
            /// <summary>
            /// Histórico consolidado
            /// </summary>
            Consolidado,
            /// <summary>
            /// Análise gerencial
            /// </summary>
            Gerencial
        }


        private void TabelaConsolidado(IEnumerable<GerencialItinerarioDTO> historicoConsolidado)
        {
            PopulaTabelaConsolidadoGerencial(historicoConsolidado);
        }

        private void PopulaTabelaConsolidadoGerencial(IEnumerable<GerencialItinerarioDTO> historicoConsolidado)
        {
            try
            {
                DateTime menorDataProgramado = historicoConsolidado.Min(m => m.Programado);
                DateTime maiorDataProgramado = historicoConsolidado.Max(m => m.Programado);

                this.usrCtrlRelatorioGerencial1.PopulaGrafico(
                    (from a in historicoConsolidado
                     join b in _listaVeiculoDTO
                                     .Select(s => new
                                     {
                                         s.CodigoVeic,
                                         s.PrefixoVeic,
                                         s.PlacaAtualVeic
                                     }) on a.CodIntVeiculo equals b.CodigoVeic into join_Veic
                     from b in join_Veic.DefaultIfEmpty()
                     join c in _listaLinhasPermitidasParaUsuario on a.CodIntLinha equals c.CodIntLinha into join_Linha
                     from c in join_Linha.DefaultIfEmpty()
                     join e in _listaLocaisDTO on a.Localidade equals e.CodLocalidade into join_Local
                     from e in join_Local.DefaultIfEmpty()
                     select new DadosGrafico()
                     {
                         CodigoLinha = c == null ? "" : c.CodigoLinha,
                         NomeLinha = c == null ? "" : c.NomeLinha,
                         Dia = menorDataProgramado.ToString("MMyyyy").Equals(maiorDataProgramado.ToString("MMyyyy"))
                                    ? a.Programado.ToString("dd")
                                    : menorDataProgramado.ToString("yyyy").Equals(maiorDataProgramado.ToString("yyyy"))
                                    ? a.Programado.ToString("MM/dd")
                                    : a.Programado.ToString("yyyy/MM/dd"),
                         Hora = a.Programado.ToString("HH"),
                         Localidade = e == null ? a.Localidade.ToString() : e.CodLocalidade.ToString(),
                         DescLocalidade = e == null ? "" : e.DescLocalidade,
                         PrefixoVeic = b == null ? "" : b.PrefixoVeic,
                         PlacaAtualVeic = b == null ? "" : b.PlacaAtualVeic,
                         TotalPeso = (a.PesoOcorHorario ?? 0) + (a.PesoOcorComboio ?? 0) + (a.PesoOcorMensagem ?? 0) + (a.PesoOcorProgNaoRea ?? 0) + (a.PesoOcorReaNaoProg ?? 0),
                         PesoOcorHorario = a.PesoOcorHorario ?? 0,
                         PesoOcorComboio = a.PesoOcorComboio ?? 0,
                         PesoOcorMensagem = a.PesoOcorMensagem ?? 0,
                         PesoOcorProgNaoRea = a.PesoOcorProgNaoRea ?? 0,
                         PesoOcorReaNaoProg = a.PesoOcorReaNaoProg ?? 0,
                         DescOcorHorario = RetornaOcorrencia(a.OcorrenciaHorario),
                         DescOcorComboio = RetornaOcorrencia(a.OcorrenciaComboio),
                         DescOcorMensagem = RetornaOcorrencia(a.OcorrenciaMensagem),
                         DescOcorProgNaoRea = RetornaOcorrencia(a.OcorrenciaProgNaoRea),
                         DescOcorReaNaoProg = RetornaOcorrencia(a.OcorrenciaReaNaoProg)
                     }));
            }
            catch (Exception)
            {
            }
        }

        private string RetornaOcorrencia(int? ocorrencia)
        {
            OcorrenciaValidadorDTO ocorrenciaValidadorDTO = _listaOcorrenciaValidadorDTO
                .Where(w => w.Ocorrencia.Equals(ocorrencia ?? 0))
                .SingleOrDefault();

            return ocorrenciaValidadorDTO == null
                ? ""
                : ocorrenciaValidadorDTO.Ocorrencia + " - " + ocorrenciaValidadorDTO.DescOcorrencia;
        }

        private void TimerTela_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_threadConsolidado == null)
                {
                    bool ocultarMes = this.tmEdtDataInicial.Time.Month.Equals(this.tmEdtDataFinal.Time.Month);
                    bool ocultarAno = this.tmEdtDataInicial.Time.Year.Equals(this.tmEdtDataFinal.Time.Year);

                    _listaGerencialItinerarioDTO.Clear();

                    _threadConsolidado = new Thread(new ThreadStart(ConsolidarDados));
                    _threadConsolidado.SetApartmentState(ApartmentState.STA);
                    _threadConsolidado.Start();

                    usrCtrlRelatorioGerencial1.LimpaDocPanel();
                }
                else if (!_threadConsolidado.IsAlive)
                {
                    _timerTela.Stop();
                    _threadConsolidado.Abort();
                    _threadConsolidado = null;
                    this.usrCtrlRelatorioGerencial1.usrCtrlGraficoRelatorioGerencial.LiberaNavegacao = true;
                }
                else
                {
                    TabelaConsolidado(_listaGerencialItinerarioDTO);
                    usrCtrlRelatorioGerencial1.usrCtrlGraficoRelatorioGerencial.comboBoxTipoGrafico_SelectedIndexChanged(
                        usrCtrlRelatorioGerencial1.usrCtrlGraficoRelatorioGerencial.comboBoxTipoGrafico, null);
                }
            }
            catch (Exception)
            {
            }
        }

        private void ConsolidarDados()
        {
            _dataInicial = this.tmEdtDataInicial.Time;
            while (_dataInicial < this.tmEdtDataFinal.Time)
            {
                try
                {

                    //string[] retornoConsulta = WebServices.GestaoDeFrotaOnLineWSApp.RetornarHistoricoConsolidado(
                    string[] retornoConsulta = WebServices.GestaoDeFrotaOnLineWSApp.RetornarHistoricoConsolidado(
                                _dataInicial,
                                _dataInicial.AddHours(1) > this.tmEdtDataFinal.Time
                                    ? this.tmEdtDataFinal.Time
                                    : _dataInicial.AddHours(1),
                                Util.RetornaValorRadioGroup(this.rdGrpSentido),
                                _listaCodLocalidade,
                                _listaCodIntLinha,
                                _listaCodigoVeic,
                                _historico.Equals(eHistorico.Gerencial));


                    if (!retornoConsulta
                            .Where(w => !w.StartsWith(":|"))
                            .Count().Equals(0))
                    {
                        PopularEstruturaGerencial(retornoConsulta);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    _dataInicial = _dataInicial.AddHours(1);
                }
            }
        }

        private void PopularEstruturaGerencial(string[] retornoConsulta)
        {
            Parallel.ForEach(retornoConsulta, f => 
            {
                string[] campo = f.Split('|');
                if (!f.StartsWith(":|" + FGlobus.Util.Constantes.CARACTER_SEPARACAO) && campo.Count() > 14)
                    _listaGerencialItinerarioDTO.Add(new GerencialItinerarioDTO()
                    {
                        CodIntLinha = Convert.ToInt32(campo[0]),
                        Localidade = Convert.ToInt32(campo[1]),
                        CodIntVeiculo = Convert.ToInt32(campo[2]),
                        Programado = Convert.ToDateTime(campo[3]),
                        Ocorrencia = Convert.ToInt32(campo[4]),
                        OcorrenciaHorario = Convert.ToInt32(campo[5]),
                        OcorrenciaComboio = Convert.ToInt32(campo[6]),
                        OcorrenciaMensagem = Convert.ToInt32(campo[7]),
                        OcorrenciaProgNaoRea = Convert.ToInt32(campo[8]),
                        OcorrenciaReaNaoProg = Convert.ToInt32(campo[9]),
                        PesoOcorHorario = Convert.ToInt32(campo[10]),
                        PesoOcorComboio = Convert.ToInt32(campo[11]),
                        PesoOcorMensagem = Convert.ToInt32(campo[12]),
                        PesoOcorProgNaoRea = Convert.ToInt32(campo[13]),
                        PesoOcorReaNaoProg = Convert.ToInt32(campo[14])
                    });
            });

            //_listaGerencialItinerarioDTO.AddRange(retornoConsulta
            //    .AsParallel()
            //    .Where(w => !w.StartsWith(":|"))
            //    .Select(s => s.Split('|'))
            //    .Select(s => new GerencialItinerarioDTO()
            //    {
            //        CodIntLinha = Convert.ToInt32(s[0]),
            //        Localidade = Convert.ToInt32(s[1]),
            //        CodIntVeiculo = Convert.ToInt32(s[2]),
            //        Programado = Convert.ToDateTime(s[3]),
            //        Ocorrencia = Convert.ToInt32(s[4]),
            //        OcorrenciaHorario = Convert.ToInt32(s[5]),
            //        OcorrenciaComboio = Convert.ToInt32(s[6]),
            //        OcorrenciaMensagem = Convert.ToInt32(s[7]),
            //        OcorrenciaProgNaoRea = Convert.ToInt32(s[8]),
            //        OcorrenciaReaNaoProg = Convert.ToInt32(s[9]),
            //        PesoOcorHorario = Convert.ToInt32(s[10]),
            //        PesoOcorComboio = Convert.ToInt32(s[11]),
            //        PesoOcorMensagem = Convert.ToInt32(s[12]),
            //        PesoOcorProgNaoRea = Convert.ToInt32(s[13]),
            //        PesoOcorReaNaoProg = Convert.ToInt32(s[14])
            //    })
            //    .ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.usrCtrlRelatorioGerencial1.Imprimir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.usrCtrlRelatorioGerencial1.LimpaDocPanel();
        }


        public void PararConsulta()
        {

        }

    }


}

/*
 
 Forma de burlar o warning
 * 
#pragma warning disable 1591
#pragma warning restore 1591


//183,184,197,420,465,602,626,657,658,672,684,809,824,1058,1060,1200,1201,1202,1203,1522,1570,1574,1580,1581,1584,1589,1590,1592,1598,1607,1616,1633,1634,1635,1645,1658,1682,1683,1684,1685,1687,1690,1691,1692,1694,1695,1696,1697,1699,1707,1709,1720,1723,1956,1957,2002,2014,2023,2029,3000,3001,3002,3003,3004,3005,3006,3007,3008,3009,3010,3011,3012,3013,3014,3015,3016,3017,3018,3022,3023,3026,3027,5000,108,114,162,164,251,252,253,278,279,280,435,436,437,440,444,458,464,467,469,472,618,652,728,1571,1572,1587,1668,1698,1710,1711,1927,3019,3021,67,105,168,169,219,282,414,419,642,659,660,661,665,675,693,1700,1717,1718,28,78,109,402,422,429,628,649,1573,1591,1610,1712,3024
 183,184,197,420,465,602,626,657,658,672,684,809,824,1058,1060,1200,1201,1202,1203,1522,1570,1580,1581,1584,1589,1590,1592,1598,1607,1616,1633,1634,1635,1645,1658,1682,1683,1684,1685,1687,1690,1691,1692,1694,1695,1696,1697,1699,1707,1709,1720,1723,1956,1957,2002,2014,2023,2029,3000,3001,3002,3003,3004,3005,3006,3007,3008,3009,3010,3011,3012,3013,3014,3015,3016,3017,3018,3022,3023,3026,3027,5000,108,114,162,164,251,252,253,278,279,280,435,436,437,440,444,458,464,467,469,472,618,652,728,1571,1572,1587,1668,1698,1710,1711,1927,3019,3021,67,105,168,169,219,282,414,419,642,659,660,661,665,675,693,1700,1717,1718,28,78,109,402,422,429,628,649,1573,1591,1610,1712,3024

183,184,197,420,465,602,626,657,658,672,684,688,809,824,1058,1060,1200,
1201,1202,1203,1522,1570,1574,1580,1581,1584,1589,1590,1592,1598,1607,
1616,1633,1634,1635,1645,1658,1682,1683,1684,1685,1687,1690,1691,1692,
1694,1695,1696,1697,1699,1707,1709,1720,1723,1911,1956,1957,2002,2014,
2023,2029,3000,3001,3002,3003,3004,3005,3006,3007,3008,3009,3010,3011,
3012,3013,3014,3015,3016,3017,3018,3022,3023,3026,3027,5000,108,114,162,
164,251,252,253,278,279,280,435,436,437,440,444,458,464,467,469,472,618,
652,728,1571,1572,1587,1668,1698,1710,1711,1927,3019,3021,67,105,168,169,
219,282,414,419,642,659,660,661,665,675,693,1700,1717,1718,28,78,109,402,
422,429,628,649,1573,1591,1610,1712,3024


108,168,414,420,429,465,467,618,649,675,1058,1060,1591,1598,1607,1610,1616,1658,1683,1685,1690,1691,1699,1700,1701,1762,1956,3003,3007,3009
 
 */

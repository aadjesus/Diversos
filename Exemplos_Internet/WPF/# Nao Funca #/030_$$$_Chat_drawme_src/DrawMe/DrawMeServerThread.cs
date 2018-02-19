using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DrawMe
{
    public class DrawMeServerThread
    {
        public static ServiceHost host = null;
        public static void Run()
        {
            try
            {
                host = new ServiceHost(typeof(DrawMeServer.DrawMeService));
                host.Open();
                while (true)
                {
                    if (DrawMe.App.s_bShutDownServerThread)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(200);
                }
                host.Close();
            }
            catch (System.Threading.ThreadAbortException abortEx)
            {
                System.Diagnostics.Trace.WriteLine("DrawMeServer ThreadAbortException: {0}", abortEx.Message.ToString());
                host.Close();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("DrawMeServer exception: {0}", ex.Message.ToString());
            }
            host = null;
        }
    }
}

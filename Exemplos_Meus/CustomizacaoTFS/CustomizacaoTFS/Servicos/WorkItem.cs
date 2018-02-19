using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.IO;
using TFSObjects;

namespace Servicos
{
    public class WorkItemValidation
    {
        public static void ValidaWorkItem(string xml)
        {
            string WorkItemType="";
            string NewState="";
            string OldState="";
            string teamProject = "";
            string OldRev = "";
            string NewRev = "";
            int WorkItemID=0;
            try
            {

                var xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);
                var nodesString = xmldoc.SelectNodes(@"WorkItemChangedEvent/CoreFields/StringFields/Field");
                
                foreach (XmlNode Field in nodesString)
                {
                    if (Field["ReferenceName"].InnerXml == "System.WorkItemType")                    
                        WorkItemType = Field["NewValue"].InnerXml;
                    if (Field["ReferenceName"].InnerXml == "System.State")
                        NewState = Field["NewValue"].InnerXml;
                    if (Field["ReferenceName"].InnerXml == "System.State")
                        OldState = Field["OldValue"].InnerXml;
                    
                }
                var nodesInt = xmldoc.SelectNodes(@"WorkItemChangedEvent/CoreFields/IntegerFields/Field");

                foreach (XmlNode Field in nodesInt)
                {
                    if (Field["ReferenceName"].InnerXml == "System.Id")
                        WorkItemID = Convert.ToInt32(Field["NewValue"].InnerXml);
                    if (Field["ReferenceName"].InnerXml == "System.TeamProject")
                        teamProject = Field["NewValue"].InnerXml;
                    if (Field["ReferenceName"].InnerXml == "System.Rev")
                        NewRev = Field["NewValue"].InnerXml;
                    if (Field["ReferenceName"].InnerXml == "System.Rev")
                        OldRev = Field["OldValue"].InnerXml;
                }

                VerificaRegras(WorkItemType, OldState, NewState, WorkItemID, teamProject, OldRev, NewRev);
            }
            catch (Exception ex)
            {
                string ArquivoLog = @"c:\temp\ErroWI.log";
                using (StreamWriter sw = new StreamWriter(ArquivoLog, true))
                {
                    sw.WriteLine("Error in WorkItem Service at: " + DateTime.Now.ToString("dd-MM-yy hh:mm:ss"));
                    sw.WriteLine(ex.Message);
                    sw.WriteLine(ex.StackTrace);
                    sw.Close();
                }      
            }



        }

        private static void VerificaRegras(string WorkItemType, string OldState, string NewState, int WorkItemID, string teamProject, string OldRev, string NewRev)
        {
            if (WorkItemType == "_Tarefa" && NewState == "Rejeitada" && OldState != "Rejeitada")
            {
                var task = new Task(WorkItemID);
                task.BuscaCR();
                task.CriaProximaTask(false, true);
            }
  
            
            if (WorkItemType == "_Tarefa" && NewState == "Concluído" && OldState != "Concluído")
            {
                var task = new Task(WorkItemID);
                task.BuscaCR();
                //task.AtualizaOrcamento();
                task.CriaProximaTask(false, false);
            }

            if (WorkItemType == "_Demanda" && OldState == "" && NewState == "Pendente")
            {
                var task = new Task(WorkItemID);            
                task.CriaProximaTask(true,false);
            }

            if (WorkItemType == "_Demanda" && OldState != "" && NewState == "Cancelada")
            {
                var task = new Task(WorkItemID);
                task.ExcluiWI(true);
            }


            if (WorkItemType == "_Demanda")
            {
                var task = new Task(WorkItemID);
                var alterou = task.VerificaAlteracaoDescription(int.Parse(NewRev), int.Parse(OldRev));
                task.EditaWI(alterou);
            }


        }        
    }
}
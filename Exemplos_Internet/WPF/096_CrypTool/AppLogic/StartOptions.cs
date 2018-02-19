using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace CrypTool.AppLogic
{
    public class StartOptions
    {
        private bool bShowHowToStartDialog;
        private bool bShowExampleFile;

        public StartOptions()
        {
            readOptions();
        }

        private void readOptions()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("CrypTool.xml");

            XmlNodeList nodeList;

            nodeList = doc.GetElementsByTagName("ShowHowToStartDialog");

            if (nodeList[0].InnerText == "1")
                this.bShowHowToStartDialog = true;
            else
                this.bShowHowToStartDialog = false;


            nodeList = doc.GetElementsByTagName("ShowExampleFileOnStartup");

            if (nodeList[0].InnerText == "1")
                this.bShowExampleFile = true;
            else
                this.bShowExampleFile = false;
        }
        private void writeOptions()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("CrypTool.xml");

            XmlNodeList nodeList;

            nodeList = doc.GetElementsByTagName("ShowHowToStartDialog");

            if (this.bShowHowToStartDialog)
                nodeList[0].InnerText = "1";
            else
                nodeList[0].InnerText = "0";
           
            nodeList = doc.GetElementsByTagName("ShowExampleFileOnStartup");

            if (this.bShowExampleFile)
                nodeList[0].InnerText = "1";
            else
                nodeList[0].InnerText = "0";

            doc.Save("CrypTool.xml");
        }

        public bool getShowHowToStartDialog()
        {
            return this.bShowHowToStartDialog;
        }
        public bool getShowExampleFile()
        {
            return this.bShowExampleFile;
        }
        public void setShowHowToStartDialog(bool bStatus)
        {
            this.bShowHowToStartDialog = bStatus;
        }
        public void setShowExampleFile(bool bStatus)
        {
            this.bShowExampleFile = bStatus;
        }
        public void saveOptions()
        {
            writeOptions();
        }
    }
}

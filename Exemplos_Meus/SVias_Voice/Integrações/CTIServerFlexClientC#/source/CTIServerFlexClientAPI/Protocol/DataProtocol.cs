using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CTIServerFlexClientAPI.Protocol
{
    /// <summary>
    /// Base do ProtocoloVoice.
    /// Contém as definições de tipos de dados e estruturas.
    /// Permite a inclusão, alteração e consulta de dados de uma forma simples.
    /// Estes dados no momento da transmissão, serão formatados para o protocolo em
    /// questão e enviados para o destino.
    /// </summary>
    public class DataProtocol
    {
        public enum DATA_TYPE_SIMPLE
        {
            CLR = 1, CLD, CLS, DEV, CAL, BTO, BFR, ITP, IVR, CID,
            SCR, QSZ, MQU, AQT, LAG, AAG, WAG, NRA, ABC, OVC,
            NRC, ACT, ATT, APT, LWT, EWT, SML, PRI, CDU, CDT,
            MAT, ANS, BCO, RRU, MCD, MCC, DTO, DST, OST, NCI,
            TIM, SEM, AST, AID, CST, CAU, NDO, FWT, FDN, MWO,
            CTO, AGR, SST, CFT, FFT, AFT, MFT, PVD, CPN, GRP,
            CCT, ATK, ADD, PRT, DSN, FMT, MDT, LCP, RMP, USR,
            DSC, EER, DPO, DSO, CPO, CSO, CB1, CB2, CB3, CB4,
            CB5, CB6, CB7, CB8, CB9, C10, VNA, VNB, VCA, VNL,
            VDT, VHR, VDS, VFE, VWU, VRS, VBF, VBT, VIT, VMU,
            VFR, BDU, RPT, TBU, AFI, SPT, SPR, SPL, SPAR, SPEL,
            UFP, SPER, SPVS, SPW, VADT, VADSS, VADUD, VADES, VADTT, LNG,
            CRS, WVB, RER, BTE, CPE, CPT, NIQ, NIS, ECD, LST,
            GNA, PRN, PRV, PLS, SPP, DTI, TBN, TRN, ASS, KEY,
            LIN, ANM, CNC, URN, CHN, DBI, PPT, GRM, SOS, TFE,
            GRT, RCR, DLS, DMD, CAFT, MAFT, PDFT, VUFT, VSFT, FRRE,
            SNU, GRE, AUF, NPR, CCP, ADS, CAT, ICA, STO, LNG2,
            PDC, QUT, WED, TD1, TD2, TD3, TD4, TD5, TD6, TD7,
            TD8, TD9, TD10, DTP, ASW, ASR, ASP, TVC, TVP, CCC,
            SFL, CPS, PTY, ADS2, DCO, PGS, PPG, AAW, BLM, MTP,
            RID, ROT, PLM, RCM, TTB,

            PID = 1000, ETP, CTP, ATP, ANB, RCD, VER, MNB, MNM, MET,
            PAS, NAW, OMD, TMD, MCR, CLI, LEV, LER, APN, APP,
            SVN,
        }
        public enum DATA_TYPE_STRUCTURED
        {
            TBL = 2000, REC
        }
        private LinkedList<Field> fields;
        public DataProtocol()
        {
            fields = new LinkedList<Field>();
        }
        private Field GetField(int name)
        {
            foreach (Field field in fields)
            {
                if (field.Name == name)
                {
                    return field;
                }
            }
            return null;
        }
        public int Bigtolittle(int x)
        {
            return (int)(((((x) & 0xFF000000) >> 24) +
                   (((x) & 0x00FF0000) >> 8) +
                   (((x) & 0x0000FF00) << 8) +
                   (((x) & 0x000000FF) << 24)));
        }
        public int Littletobig(int x)
        {
            return Bigtolittle(x);
        }
        public void Replace(int name, Object value)
        {
            Field field = GetField(name);
            if (field == null)
                fields.AddLast(new Field(name, value));
            else
                field.Value = value;
        }
        public void Replace(VoiceProtocol.DATA_TYPE_SIMPLE name, Object value)
        {
            Replace((int)name, value);
        }
        public void Replace(VoiceProtocol.DATA_TYPE_STRUCTURED name, Object value)
        {
            Replace((int)name, value);
        }
        public void Add(int name, Object value)
        {
            fields.AddLast(new Field(name, value));
        }
        public void Add(VoiceProtocol.DATA_TYPE_SIMPLE name, Object value)
        {
            Add((int)name, value);
        }
        public void Add(VoiceProtocol.DATA_TYPE_STRUCTURED name, Object value)
        {
            Add((int)name, value);
        }
        public Object Get(int name)
        {
            Field ret = GetField(name);
            if (ret == null) return "";
            return ret.Value;
        }
        public Object Get(VoiceProtocol.DATA_TYPE_SIMPLE name)
        {
            return Get((int)name);
        }
        public Object Get(VoiceProtocol.DATA_TYPE_STRUCTURED name)
        {
            return Get((int)name);
        }
        public int GetInt(int name)
        {
            Field ret = GetField(name);
            if (ret == null) return 0;
            return int.Parse(ret.Value.ToString());
        }
        public int GetInt(VoiceProtocol.DATA_TYPE_SIMPLE name)
        {
            return GetInt((int)name);
        }
        public LinkedList<Field> Fields
        {
            get { return fields; }
        }
        public int GetSize()
        {
            int ret = 0;
            foreach (Field field in fields)
            {
            if (field.Value is DataProtocol) {
                    DataProtocol dp = (DataProtocol)field.Value;
                    ret += dp.GetSize();
                }
                else {
                    ret += field.Value.ToString().Length;
                }
                ret += 8;
            }
            return ret;
        }
        public void Read(BinaryReader stream, int size)
        {
            while (size > 0)
            {
                int id = Bigtolittle(stream.ReadInt32());
                int sizeId = Bigtolittle(stream.ReadInt32());
                if (id >= (int)DATA_TYPE_STRUCTURED.TBL) 
                {
                    DataProtocol dp = new DataProtocol();
                    dp.Read(stream, sizeId);
                    Add(id, dp);
                }
                else 
                {
                    String value = new String(stream.ReadChars(sizeId));
                    Add(id, value);
                }
                size -= 8;
                size -= sizeId;
            }
        }
        public void Write(BinaryWriter stream)
        {
            foreach (Field field in fields)
            {
                if (field.Value is DataProtocol)
                {
                    DataProtocol dp = (DataProtocol)field.Value;
                    stream.Write(Bigtolittle(field.Name));
                    stream.Write(Bigtolittle(dp.GetSize()));
                    dp.Write(stream);
                }
                else
                {
                    stream.Write(Bigtolittle(field.Name));
                    stream.Write(Bigtolittle(field.Value.ToString().Length));
                    stream.Write(field.Value.ToString().ToCharArray());
                }
            }
        }
        override public String ToString()
        {
            String ret = "";
            foreach (Field field in fields)
            {
                ret = ret + "<" + field.Name + "=" + field.Value.ToString() + ">";
            }
            return "[" + ret + "]";
        }
    }
}

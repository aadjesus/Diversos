using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using CTIServerFlexClientAPI.Protocol;

namespace CTIServerFlexClientAPI.Tables
{
    public class AgentCall : TableRecord
    {
        #region CallStates
        /*
         * Desconhecido
         */
        public static int STT_UNKNOWN = 0;
        /**
         * Chamada na fila.
         */
        public static int STT_QUEUED = 1;
        /**
         * Chamada na tocando.
         */
        public static int STT_RINGING = 3;
        /**
         * Chamada na atendida.
         */
        public static int STT_ANSWERED = 7;
        /**
         * Chamada na na espera.
         */
        public static int STT_HOLD = 10;
        /**
         * Chamada redirecionada.
         */
        public static int STT_TRANSFERED = 11;
        /**
         * Chamada redirecionada.
         */
        public static int STT_DEFLECTED = 15;
        /**
         * Chamada finalizada.
         */
        public static int STT_CLEARED = 25;
        /**
         * Chamada originada.
         */
        public static int STT_ORIGINATED = 26;
        /**
         * Iniciando chamada. Telefone fora do gancho.
         */
        public static int STT_OFFHOOK = 27;
        /**
         * Chamada em falha. Telefone fora do gancho, com tom de falha.
         */
        public static int STT_FAILURE = 31;
        #endregion

        private DateTime startTime;
	    public AgentCall(Table table):base(table) {
            startTime = DateTime.Now;
	    }
        public TimeSpan GetTotalTime()
        {
            return DateTime.Now.Subtract(startTime);
        }
	    public String Device
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.DEV).ToString(); }
	    }
        public String Called
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.CLD).ToString(); }
        }
        public String Caller
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.CLR).ToString(); }
        }
        public String CallId
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.CAL).ToString(); }
	    }
	    public int StateTo
        {
            get { return Data.GetInt(DataProtocol.DATA_TYPE_SIMPLE.STO); }
	    }
	    public int State
        {
            get { return Data.GetInt(DataProtocol.DATA_TYPE_SIMPLE.CST); }
	    }
        public bool IsState(int state)
        {
            return State == state;
        }
        public bool IsStateTo(int state)
        {
            return StateTo == state ;
        }
	    public String DeviceTo
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.DTO).ToString(); }
	    }
        public String InternalCallId
        {
            get { return Data.Get(DataProtocol.DATA_TYPE_SIMPLE.ICA).ToString(); }
        }
        public int Cause
        {
            get { return Data.GetInt(DataProtocol.DATA_TYPE_SIMPLE.CAU); }
        }
        public bool IsActive()
        {
            return "[2][3][7][26][27][34]".IndexOf("[" + State + "]") >= 0;
        }
        public bool IsDeleted()
        {
            return "[25][11][15]".IndexOf("[" + State + "]") >= 0;
        }
        override public String ToString()
        {
            return Key + ":" + InternalCallId + ":" + CallId + ":" + Device + ":" + State + ":" + DeviceTo + ":" + StateTo + ":" + Caller + ":" + Called + ":" + Cause;
        }
    }
}

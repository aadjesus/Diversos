using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;
using CTIServerFlexClientAPI.VStream;

namespace CTIServerFlexClientAPI.Protocol
{
    /// <summary>
    /// Respons�vel pela formata��o de interpreta��o dos bytes do protocolo propriet�rio
    /// (ProtocoloVoice).
    /// </summary>
    public class VoiceProtocol:DataProtocol
    {
        /// <summary>
        /// Tipos de pacotes permitidos pelo ProtocoloVoice
        /// </summary>
        public enum PACKET_TYPE
        {
            NONE = 0,
            LOGON, RESPONSE_LOGON,
            GETDATA, RESPONSE_GETDATA,
            SETDATA, RESPONSE_SETDATA,
            COMMAND, RESPONSE_COMMAND,
            EVENT, RESPONSE_EVENT
        }
        private static int VP_ID = 0x544b595a;
        private PACKET_TYPE _type;
        public VoiceProtocol()
        {
            _type = PACKET_TYPE.NONE;
        }
        /// <summary>
        /// Retorna o tipo de dado presente no protocolo.
        /// </summary>
        public PACKET_TYPE Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// L� os dados a partir do stream passado e interpreta baseado no protocolo
        /// propriet�rio (ProtocoloVoice).
        /// </summary>
        /// <param name="stream">Normalmente um socket</param>
        public void Read(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            while (br.ReadInt32() != VP_ID) {
                TraceSwitch trace = new TraceSwitch("TraceLevel", "N�vel do trace da aplica��o");
                if (trace.TraceError)
                    Trace.TraceError("Package error. Magic number not found");
            }
            _type = (PACKET_TYPE)base.Bigtolittle(br.ReadInt32());
            int size = base.Bigtolittle(br.ReadInt32());
            size -= 12;
            base.Read(br, size);
        }
        /// <summary>
        /// Baseado nos dados armazenados na classe, gera o ProtocoloVoice de sa�da
        /// e envia para o stream passado como par�metro.
        /// </summary>
        /// <param name="stream">Sa�da para os bytes formatados. Normalmente socket.</param>
        public void Write(Stream stream)
        {
            BufferedStream bs = new BufferedStream(stream);
            BinaryWriter bw = new BinaryWriter(bs);
            int size = 12 + base.GetSize() ;
            bw.Write(VP_ID);
            bw.Write(Bigtolittle((int)_type));
            bw.Write(Bigtolittle(size));
            base.Write(bw);
            bs.Flush();
        }
    }
}

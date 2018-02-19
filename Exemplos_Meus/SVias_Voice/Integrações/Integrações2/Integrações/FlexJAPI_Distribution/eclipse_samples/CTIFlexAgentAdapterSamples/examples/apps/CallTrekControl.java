/*
 * Created on 12/06/2005
 *
 */
package examples.apps;

import java.awt.image.ImageObserver;
import java.io.IOException;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Iterator;
import java.util.Vector;
import java.util.logging.Level;

import javax.swing.ImageIcon;

import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.AgentInformations;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAgentLogoff;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAgentLogon;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAgentNotReady;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAgentReady;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAnswer;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandConnectionClear;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandDeflect;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandHold;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandRetrieve;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetInformation;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetSendTo;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.executable.Command;
import br.com.voicetechnology.flexapi.executable.CommandListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Filter;
import br.com.voicetechnology.flexapi.tables.FilteredTable;
import br.com.voicetechnology.flexapi.tables.Record;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.util.Protocol;
import examples.tables.views.AgentInfoTableModel;
import examples.tables.views.AgentInfoTableModelListener;

/**
 * @author Roberto Elvira
 * Voice Technology Ind.<br>
 *
 * Exemplo de manipulador da JAPI para controlar as chamadas presentes em 
 * um ramal.
 */
public class CallTrekControl implements CollectionListener, FlexStreamListener, AgentInfoTableModelListener {
    private class FilteredInformation {
        private FilteredTable informationSelected = null;
        private AgentCall agentCall = null;
        /**
         * Altera a chamada selecionada para filtrar as informações
         * @param call
         * Chamada para filtrar as informações
         */
        public void setCall(AgentCall call) {
            if (agentCall != call) {
                if (informationSelected != null) {
                    informationSelected.release();
                }
                agentCall = call;
                if (call==null) return;
                informationSelected = agentAdapter.getInformations(call);
            }
        }
        /**
         * Método de acesso a propriedade.
         * @return
         * Chamada filtrada.
         */
        public AgentCall getCall() {
            return agentCall;
        }
        /**
         * Acessa o valor de uma propriedade das informações filtradas.
         * @param propertyName
         * Nome da propriedade para acessar.<br>
         * Ex.: "CID"
         * @return
         * Valor da propriedade.
         */
        public String getValue(String propertyName) {
            if (informationSelected == null) return "";
            AgentInformations ai = new AgentInformations(informationSelected);
            return ai.getValue(propertyName);
        }
        /**
         * Libera o relacionamento dos objetos.
         */
        public void release() {
            setCall(null);
        }            
        /**
         * @see java.lang.Object#equals(java.lang.Object)
         */
        public boolean equals(Object obj) {
            if (!(obj instanceof FilteredInformation)) return false;
            return ((FilteredInformation)obj).getCall() == agentCall;
        }
    }
    private Vector listener = null;
    private AgentAdapter agentAdapter = null;
    private AgentCall selectedCall = null;
    private AgentInfoTableModel privateModel = null;
    private AgentInfoTableModel queueModel = null;
    private FilteredTable privateTable =  null;
    private FilteredTable queueTable = null;
    private FilteredInformation informationSelected = null;
    private String myDevice = null;
    private String holdDevice = null;
    private Vector queueDevices = null;
    private Vector adnDevices = null;
    private int cmdError = 0;
    private ImageIcon[] icons;
    private ImageIcon imgPointer;
    private ImageIcon imgNull;
    private static final int ICON_ANSWER = 0;
    private static final int ICON_RING = 1;
    private static final int ICON_HOLD = 2;
    private static final int ICON_QUEUE = 3;
    private static final int ICON_OFFHOOK = 4;
    /**
     * Construtor da classe.
     * @param imgObserver
     * Container dos gifs animados.
     * Deve ser passado o painel que contém as tabelas onde os
     * gifs aparecerão ou o próprio JFrame. 
     */
    public CallTrekControl(ImageObserver imgObserver) {
        informationSelected = new FilteredInformation();
        icons = new ImageIcon[5];
        icons[ICON_ANSWER] = new ImageIcon(getClass().getResource("icons/c_falando.gif"));
        icons[ICON_RING] = new ImageIcon(getClass().getResource("icons/c_tocando.gif"));
        icons[ICON_HOLD] = new ImageIcon(getClass().getResource("icons/c_espera.png"));
        icons[ICON_QUEUE] = new ImageIcon(getClass().getResource("icons/c_fila.png"));
        icons[ICON_OFFHOOK] = new ImageIcon(getClass().getResource("icons/c_falando.gif"));
        imgPointer = new ImageIcon(getClass().getResource("icons/selected.gif"));
        imgNull = new ImageIcon(getClass().getResource("icons/null.gif"));

        icons[ICON_ANSWER].setImageObserver(imgObserver);
        icons[ICON_RING].setImageObserver(imgObserver);
        icons[ICON_HOLD].setImageObserver(imgObserver);
        icons[ICON_QUEUE].setImageObserver(imgObserver);
        icons[ICON_OFFHOOK].setImageObserver(imgObserver);
        imgPointer.setImageObserver(imgObserver);

        listener = new Vector();
        privateTable = new FilteredTable();
        privateTable.setName("Filtered-PrivateTable");
        queueTable = new FilteredTable();
        queueTable.setName("Filtered-QueueTable");

        privateModel = new AgentInfoTableModel(privateTable, "POINTER,CST,CLR", "DSC", "*,,Telefone,Cliente");
        privateModel.setListener(this);
        privateModel.setAutomaticUpdate(true);

        queueModel = new AgentInfoTableModel(queueTable, "POINTER,CST,CLR", "DSC", "*,,Telefone,Cliente");
        queueModel.setListener(this);
        queueModel.setAutomaticUpdate(true);
    }
    /**
     * Libera todas as intâncias deste objeto. Instâncias ficam presas como "listener" em objetos
     * notificadores (AgentInfoTableModel por exemplo).<br>
     * Somente chamar este método na finalização.<br>
     * Ex.:<br>
     * CallTrekControl ccc = new CallTrekControl(this);<br>
     * usa o "ccc" ...<br>
     * ccc.release();<br>
     * "ccc" neste ponto está inutilizado.<br>
     * ccc = null;<br>
     */
    public void release() {
        setAgentAdapter(null);
        privateModel.setListener(null);
        privateModel.release();
        queueModel.setListener(null);
        queueModel.release();
        privateTable.removeListener(this);
        privateTable.release();
        queueTable.removeListener(this);
        queueTable.release();
        listener.clear();
        informationSelected.release();
    }
    /**
     * Recria os modelos para as atuais configurações
     * de devices e agentAdapter.
     */
    private void updateModels() {
        if ((myDevice != null) && (holdDevice != null) && (adnDevices != null)) {
            String devs = myDevice + "|" + holdDevice;
            Iterator i = adnDevices.iterator();
            while (i.hasNext()) {
                devs = devs + "|" + (String)i.next();
            }
            Filter filter = new Filter();
            filter.set("DEV", devs);
            privateTable.setFilter(filter);
        }
        if (queueDevices != null) {
            String devs = "";
            Iterator i = queueDevices.iterator();
            while (i.hasNext()) {
                devs = devs + (String)i.next();
                if (i.hasNext()) {
                    devs = devs + "|";
                }
            }
            Filter filter = new Filter();
            filter.set("DEV", devs);
            queueTable.setFilter(filter);
        }
    }
    /**
     * Setter para agentAdapter.
     * @param arg
     * Instância válida de agentAdapter.
     */
    public void setAgentAdapter(AgentAdapter arg) {
        if (agentAdapter != null) {
            agentAdapter.getStream().removeListener(this);
            agentAdapter.getCalls().removeListener(this);
        }
        if (arg == null) {
            return;
        }
        agentAdapter = arg;
        agentAdapter.getStream().addListener(this);
        agentAdapter.getCalls().addListener(this);
        myDevice = agentAdapter.getApplicationInfo().getDevice().getRamal();

        privateModel.setAgentAdapter(agentAdapter);
        queueModel.setAgentAdapter(agentAdapter);
        
        privateTable.setSourceTable(agentAdapter.getCalls());
        queueTable.setSourceTable(agentAdapter.getCalls());
    }
    /**
     * Setter para holdDevice
     * @param arg
     * Identifica o ramal para fila particular, que deve
     * ser mostrado na mesma lista das chamadas pertencentes
     * ao ramal original. Neste objeto chamado de "holdDevice".
     */
    public void setHoldDevice(String arg) {
        holdDevice = arg;
        updateModels();
    }
    /**
     * Setter para adnDevices e grupos associados.
     * @param arg1
     * Identifica os ramais associados ao grupo de entrada.
     * As chamadas nestes ramais, serão mostradas em "privateTable".
     * Neste objeto, será chamado de "adnDevices".
     * Deve ser passado uma lista de ramais, separados por vírgula e na
     * ordem com seus respectivos grupos.<br>
     * Ex.: 1001,1002
     * @param arg2
     * Identifica os ramais que devem ser mostrados separadamente
     * em uma outra tabela: "queueTable".
     * Neste objeto chamado de "queueDevices".
     * Deve ser passado uma lista de ramais, separados por vírgula e
     * na ordem de associação com seus adns (parâmetro arg2).<br>
     * Ex.: 10000,20000
     */
    public void setAdnDevices(String arg1, String arg2) {
        String[] devs = arg1.split(",");
        adnDevices = new Vector();
        for (int i = 0; i < devs.length; i++) {
            adnDevices.add(devs[i].trim());
        }
        devs = arg2.split(",");
        queueDevices = new Vector();
        for (int i = 0; i < devs.length; i++) {
            queueDevices.add(devs[i].trim());
        }
        updateModels();
    }
	/**
	 * @see br.com.voicetechnology.flexapi.tables.views.AgentInfoTableModelListener#getValueOfField(java.lang.Object, br.com.voicetechnology.flexapi.tables.Record, java.lang.String)
	 */
	public Object getValueOfField(Object owner, Record record, String fieldName) {
        if (fieldName.equals("LOCALTIME")) {
            if (record instanceof AgentCall) {
                AgentCall ac = (AgentCall)record;
                return String.valueOf(ac.getTotalTime().getTime() / 1000);
            }
    		return "Tempo";
        } else if (fieldName.equals("CST")) {
            if (record instanceof AgentCall) {
                AgentCall ac = (AgentCall)record;
                int img = ICON_ANSWER;
                switch (ac.getState()) {
                    case AgentCall.STT_HOLD:
                        img = ICON_HOLD;
                        break;
                    case AgentCall.STT_FAILURE:
                    case AgentCall.STT_OFFHOOK:
                    case AgentCall.STT_ORIGINATED:
                        img = ICON_OFFHOOK;
                        break;
                    case AgentCall.STT_QUEUED:
                        img = ICON_QUEUE;
                        break;
                    case AgentCall.STT_RINGING:
                        img = ICON_RING;
                        break;
                    case AgentCall.STT_ANSWERED:
                    default:
                        img = ICON_ANSWER;
                        break;
                }
                return icons[img];
            }
            return "";
        } else if (fieldName.equals("POINTER")) {
            if (record instanceof AgentCall) {
                AgentCall ac = (AgentCall)record;
                if (ac == selectedCall) {
                    return imgPointer;
                }
            }
            return imgNull;
        } else {
            return record == null ? "" : record.get(fieldName);
        }
	}
    /**
     * Adiciona um novo listener.
     * @param arg
     * Instância do listener para adicionar.
     */
    public void addListener(CallTrekControlListener arg) {
        listener.remove(arg);
        listener.add(arg);
    }
    /**
     * Remove o listener.
     * @param arg
     * Instância do listener para remover.
     */
    public void removeListener(CallTrekControlListener arg) {
        listener.remove(arg);
    }
    /**
     * Notifica os listeners que a chamada ativa alterou.
     */
    private void onSelectedCallChanged() {
    	Object []aux = listener.toArray();
        for (int i=0; i<aux.length; i++) {
        	((CallTrekControlListener)aux[i]).onSelectedCallChanged(this);
        }
        //privateModel.fireTableDataChanged();
        //queueModel.fireTableDataChanged();
    }
    /**
     * Setter para selectedCall.
     * @param arg
     * Nova chamada selecionada.
     */
    public void setSelectedCall(AgentCall arg) {
        selectedCall = arg;
        AgentAdapter.getLogger().log(Level.FINE, "SelectedCall:" + selectedCall);
        onSelectedCallChanged();
    }
    /**
     * Getter de selectedCall.
     * @return
     * Instância de AgentCall que representa a chamada
     * selecionada ou null caso nenhuma.
     */
    public AgentCall getSelectedCall() {
        return selectedCall;
    }
	/**
     * Recupera o modelo de tabela para visualização dos
     * dados das chamadas que estão no ramal principal do
     * agente ou na fila relacionada com ele marcada como
     * ramal de espera.
	 * @return
     * Instância de AgentInfotTableModel.
	 */
	public AgentInfoTableModel getPrivateModel() {
		return privateModel;
	}
    /**
     * Recupera o modelo de tabela para visualização dos
     * dados das chamadas que estão na fila de entrada
     * marcada como ramal secundário.
     * @return
     * Instância de AgentInfotTableModel.
     */
	public AgentInfoTableModel getQueueModel() {
		return queueModel;
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#added(java.lang.Object, java.lang.Object, int)
	 */
	public void added(Object owner, Object item, int index) {
        if (selectedCall == null) {
            setSelectedCall(agentAdapter.getActiveCall());
        }
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#deleted(java.lang.Object, java.lang.Object, int)
	 */
	public void deleted(Object owner, Object item, int index) {
        if (item == selectedCall) {
           setSelectedCall(agentAdapter.getActiveCall());
        }
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#updated(java.lang.Object, java.lang.Object, int)
	 */
	public void updated(Object owner, Object item, int index) {
        if (item == selectedCall) {
            setSelectedCall(selectedCall);
         }
	}
    /**
     * Executa o comando de modo sincrono.
     * @param cmd
     * Comando para ser executado.
     * @throws Exception
     * Erro na execução.
     */
    public void executeSync(Command cmd) throws Exception {
        AgentAdapter.getLogger().log(Level.FINE, "ExecuteSync:" + cmd.getClass().getName());
        cmdError = -1;
        cmd.addListener(new CommandListener(){
            public void onCommandResponse(Command arg0) {
                cmdError = arg0.getResponse().getResponse();
            }
            public void onCommandTimeout(Command arg0) {
                cmdError = 1001;
            }
        });
        agentAdapter.execute(cmd);
        int i = 0;
        while (cmdError == -1) {
            try {
				Thread.sleep(200);
                if (i==20) {
                    cmdError = 1001;
                } else {
                    i++;
                }
			} catch (Exception e) {
                return;
			}
        }
        if (cmdError != 0) {
            AgentAdapter.getLogger().log(Level.WARNING, "ExecuteSync:" + cmdError);
            throw new Exception(String.valueOf(cmdError));
        } else {
            AgentAdapter.getLogger().log(Level.FINE, "ExecuteSync:Success");
        }
        return;
    }
    /**
     * Verifica se o ramal informado é do próprio telefone (ODN ou ADNs),
     * ou se não
     * @param device
     * String contendo o ramal para verificar.
     * @return
     * Verdadeiro se o ramal fizer parte do próprio aparelho.
     */
    private boolean isMyPhone(String device) {
        return (device.equals(myDevice) || adnDevices.contains(device));
    }
    /**
     * Executa se possível um comando de atendimento da chamada selecionada.
     * Se nenhuma selecionada, tenta atender a atual ativa.
     * A chamada selecionada poderá ser uma das seguintes situações:<br>
     * <b>-Chamada no grupo acd de entrada ou no grupo de espera<\b><br>
     * Este comando executa um "Deflect" da chamada para o ramal da atendente.<br>
     * <b>-Chamada no próprio ramal da atendente<\b><br>
     * Se a chamada estiver tocando, executa um "Answer".<br>
     * Se a chamada estiver na espera, executa um "Retrieve".<br>
     * @throws IOException
     * Erro no comando de atendimento.
     */
    public void cmdAnswer() throws Exception {
        Command cmd;
        if (selectedCall == null) {
            AgentCall ac = agentAdapter.getActiveCall();
            if (ac == null) {
                throw new NullPointerException("Chamada não selecionada");
            }
            if (ac.isState(AgentCall.STT_HOLD)) {
                cmd = new CommandRetrieve();
                ((CommandRetrieve)cmd).setToCall(ac);
            } else {
                cmd = new CommandAnswer();
                ((CommandAnswer)cmd).setToCall(ac);
            }
            setSelectedCall(ac);
        } else {
            if (isMyPhone(selectedCall.getDevice())) {
                if (selectedCall.isState(AgentCall.STT_HOLD)) {
                    cmd = new CommandRetrieve();
                    ((CommandRetrieve)cmd).setToCall(selectedCall);
                } else {
                    cmd = new CommandAnswer();
                    ((CommandAnswer)cmd).setToCall(selectedCall);
                }
            } else {
                cmd = new CommandDeflect();
                ((CommandDeflect)cmd).setToCall(selectedCall);
                ((CommandDeflect)cmd).setDeviceTo(myDevice);
            }
        }
        agentAdapter.execute(cmd);
    }
    /**
     * Desliga a chamada selecionada.
     * Se nenhuma selecionada, busca a chamada ativa no ramal.
     * @throws IOException
     * Erro no comando de clear.
     */
    public void cmdClear() throws Exception {
        Command cmd;
        if (selectedCall == null) {
            cmd = agentAdapter.getCommandConnectionClear();
            if (cmd == null) {
                throw new NullPointerException("Chamada não selecionada");
            }
        } else {
            cmd = new CommandConnectionClear();
            ((CommandConnectionClear)cmd).setToCall(selectedCall);
        }
        agentAdapter.execute(cmd);
    }
    /**
     * Disca para o destino especificado no argumento.
     * Se existir uma chamada ativa e falando, envia um consultation,
     * caso contrário, envia um dial.
     * @param arg
     * Número destino da chamada.
     * @throws IOException
     * Erro no comando.
     */
    public void cmdDial(String arg) throws Exception {
        AgentCall ac = agentAdapter.getActiveCall();
        Command cmd;
        if (ac == null) {
            cmd = agentAdapter.getCommandMakeCall(myDevice, arg);
        } else {
            if (!ac.isState(AgentCall.STT_HOLD)) {
                if (ac.getDevice().equals(myDevice)) {
                    cmd = agentAdapter.getCommandConsultation(arg);
                } else {
                    cmd = agentAdapter.getCommandHold();
                    ((CommandHold)cmd).setToCall(ac);
                    executeSync(cmd);
                    cmd = agentAdapter.getCommandMakeCall(myDevice, arg);
                }
            } else {
                cmd = agentAdapter.getCommandMakeCall(myDevice, arg);
            }
        }
        agentAdapter.execute(cmd);
    }
    /**
     * Verifica se existe uma chamada na espera e uma falando,
     * se sim, executa a transferência.
     * @throws IOException
     * Erro no comando.
     */
    public void cmdTransfer() throws Exception {
        Command cmd = agentAdapter.getCommandTransfer();
        if (cmd == null) {
            throw new NullPointerException("Chamadas inexistentes");
        }
        agentAdapter.execute(cmd);
    }
    /**
     * Transfere a chamada selecionada para o destino informado no argumento, passando
     * os parâmetros desejados.
     * @param arg
     * String contendo o destino da chamada selecionada.
     * @param properties
     * Propriedades para adicionar a chamada que será transferida.
     * @throws IOException
     * Erro no comando.
     */
    public void cmdDeflect(String arg, Hashtable properties) throws Exception {
        if (selectedCall == null) {
            throw new NullPointerException("Chamada não selecionada");
        } else {
            if(properties!=null) {
	        	CommandSetInformation cmd = new CommandSetInformation();
	            cmd.setToCall(selectedCall);
	            Enumeration e = properties.keys();
	            while (e.hasMoreElements()) {
	                String prn = (String)e.nextElement();
	                String prv = (String)properties.get(prn);
	                cmd.setPropertyName(prn);
	                cmd.setPropertyValue(prv);
	                agentAdapter.execute(cmd);
	            }
            }
            CommandDeflect cmd2 = new CommandDeflect();
            cmd2.setToCall(selectedCall);
            cmd2.setDeviceTo(arg);
            agentAdapter.execute(cmd2);
        }
    }
    /**
     * Verifica se existe uma chamada na espera e uma falando,
     * se sim, executa a conferência.
     * @throws IOException
     * Erro no comando.
     */
    public void cmdConference() throws Exception {
        Command cmd = agentAdapter.getCommandConference();
        if (cmd == null) {
            throw new NullPointerException("Chamadas inexistentes");
        }
        agentAdapter.execute(cmd);
    }
    /**
     * Executa se possível um comando de espera
     * da chamada selecionada.
     * Se nenhuma selecionada, tenta colocar na
     * espera a chamada atual ativa.
     * @throws IOException
     * Erro no comando.
     */
    public void cmdHold() throws Exception {
        if (selectedCall == null) {
            AgentCall ac = agentAdapter.getActiveCall();
            if (ac == null) {
                throw new IOException("Não existe chamada ativa para comando");
            }
            setSelectedCall(ac);
        }
        if (queueDevices.indexOf(selectedCall.getDevice()) >= 0) {
            throw new IOException("Não pode estacionar a chamada da fila de entrada");
        }
        // notifica para visualizar a chamada
        setSendTo(selectedCall.getInternalCallId(), holdDevice);
        // envia o comando de deflect
        CommandDeflect cmd = new CommandDeflect();
        cmd.setToCall(selectedCall);
        cmd.setDeviceTo(holdDevice);
        agentAdapter.execute(cmd);
    }
    /**
     * Loga o agente no grupo acd.
     * @param pin
     * Identificador do agente.
     * @param pass
     * Senha do agente.
     * @param group
     * Grupo para se logar.
     * @throws IOException
     * Erro no comando.
     */
    public void cmdLogon(String pin, String pass, String group) throws Exception {
        CommandAgentLogon cmd = new CommandAgentLogon();
        cmd.setDevice(myDevice);
        cmd.setAgentId(pin);
        cmd.setPassword(pass);
        cmd.setGroup(group);
        executeSync(cmd);
    }
    /**
     * Desloga o agente.
     * @param pin
     * Identificador do agente.
     * @param pass
     * Senha do agente.
     * @throws IOException
     * Erro no comando.
     */
    public void cmdLogoff(String pin, String pass) throws Exception {
        CommandAgentLogoff cmd = new CommandAgentLogoff();
        cmd.setDevice(myDevice);
        cmd.setAgentId(pin);
        cmd.setPassword(pass);
        executeSync(cmd);
    }
    /**
     * Habilita o agente.
     * @throws Exception
     */
    public void cmdReady(int reasonCode) throws Exception {
        CommandAgentReady cmd = new CommandAgentReady();
        cmd.setReasonCode(reasonCode);
        cmd.setDevice(myDevice);
        executeSync(cmd);
        Iterator i = adnDevices.iterator();
        while (i.hasNext()) {
            cmd.setDevice((String)i.next());
            executeSync(cmd);
        }
    }
    /**
     * Desabilita o agente.
     * @throws Exception
     */
    public void cmdNotReady(int reasonCode) throws Exception {
        CommandAgentNotReady cmd = new CommandAgentNotReady();
        cmd.setReasonCode(reasonCode);
        cmd.setDevice(myDevice);
        executeSync(cmd);
        Iterator i = adnDevices.iterator();
        while (i.hasNext()) {
            cmd.setDevice((String)i.next());
            executeSync(cmd);
        }
    }
    /**
     * Envia o comando para solicitar a visualização das chamadas
     * que estão em outro ramal.
     * Normalmente usado o par:
     * ALL com secundaryDevice, ou
     * InternalCallId com primaryDevice.
     * @param ica
     * Internal call id da chamada para visualizar, ou ALL para todas.
     * @param dev
     * Ramal para visualizar. Ou primaryDevice, ou secundaryDevice.
     */
    private void setSendTo(String ica, String dev) {
        try {
            CommandSetSendTo cmd = new CommandSetSendTo();
            cmd.setDeviceTo(myDevice);
            cmd.setDevice(dev);
            cmd.setInternalCallId(ica);
            agentAdapter.execute(cmd);
        } catch (Exception e1) {
        }
    }
    /**
     * Busca o valor da informação associada a chamada selecionada
     * baseado no nome da propriedade.
     * @param arg
     * Nome da propriedade para buscar.
     * @return
     * Valor da propriedade associada a chamada informada.
     * @throws IOException
     * Erro no comando.
     */
    public String getInformation(String arg) throws IOException {
        if (selectedCall == null) {
            throw new NullPointerException("Chamada não selecionada");
        }
        informationSelected.setCall(selectedCall);
        return informationSelected.getValue(arg);
    }
    /**
     * Verifica se a chamada selecionada está no ramal da operadora (ODN ou ADNs).
     * @return
     * Verdadeiro se sim, falso se não ou null.
     */
    public boolean isSelectedCallMyTerminal() {
        if (selectedCall == null) return false;
        if (selectedCall.getDevice().equals(myDevice)) return true;
        return adnDevices.indexOf(selectedCall.getDevice()) >= 0;
    }
    /**
     * Verifica se a chamada selecionada está na fila particular de espera.
     * @return
     * Verdadeiro se sim, falso se não ou null.
     */
    public boolean isSelectedCallHoldDevice() {
        if (selectedCall == null) return false;
        return selectedCall.getDevice().equals(holdDevice);
    }
    /**
     * Verifica se a chamada selecionada está na fila de entrada.
     * @return
     * Verdadeiro se sim, falso se não ou null.
     */
    public boolean isSelectedCallSecundary() {
        if (selectedCall == null) return false;
        return queueDevices.indexOf(selectedCall.getDevice()) >= 0;
    }
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onError()
	 */
	public void onError() {
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onDisconnect()
	 */
	public void onDisconnect() {
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onConnect()
	 */
	public void onConnect() {
        Iterator i = queueDevices.iterator();
        while (i.hasNext()) {
            String dev = (String)(i.next());
            FlexAPI.getLogger().log(Level.FINE, "Enviando setSendTo: ALL," + dev);
            setSendTo("ALL", dev);
        }
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onReceive(br.com.voicetechnology.util.Protocol)
	 */
	public void onReceive(Protocol protocol) {
	}
	/**
     * Getter para tabela que contém as chamadas do ramal principal e da fila particular.
	 * @return
     * Instância de tabela.
	 */
	public Table getPrivateTable() {
		return privateTable;
	}
    /**
     * Getter para tabela que contém as chamadas do ramal secundário (fila principal).
     * @return
     * Instância de tabela.
     */
	public Table getQueueTable() {
		return queueTable;
	}
}

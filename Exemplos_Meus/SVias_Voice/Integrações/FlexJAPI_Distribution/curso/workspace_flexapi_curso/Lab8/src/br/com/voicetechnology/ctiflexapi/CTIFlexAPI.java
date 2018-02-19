package br.com.voicetechnology.ctiflexapi;

import java.util.Calendar;
import java.util.Collection;
import java.util.LinkedList;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAnswer;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandConnectionClear;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandLogonAPI;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandMakeCall;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetSendTo;
import br.com.voicetechnology.flexapi.agentadapter.synchronous.DefaultProcessor;
import br.com.voicetechnology.flexapi.executable.Command;
import br.com.voicetechnology.flexapi.executable.CommandListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Record;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.util.Protocol;


public class CTIFlexAPI implements CTIFlexAPIMBean, FlexStreamListener, CommandListener {
	private class SetSendTo implements Runnable {
		private String deviceView;
		public SetSendTo(String deviceView) {
			this.deviceView = deviceView;
			new Thread(this,"SetSendTo").start();
		}
		synchronized public void run() {
			CommandSetSendTo cmd = new CommandSetSendTo();
			cmd.setDevice(deviceView);
			cmd.setDeviceTo(device);
			cmd.setInternalCallId("ALL");
			DefaultProcessor proc = new DefaultProcessor(api);
			setToLog("Enviando setSendTo: Device=" + deviceView + ", DeviceTo=" + device);
			int ret = proc.execute(cmd);
			setToLog("Retorno=" + ret);
		}
	}
	private static final int CTISERVERFLEX_PORT = 2556;
	private static final int APP_NUMBER = 1234;
	private static final String VERSION = "1.0.0";
	
	private StringBuffer log;
	private String state;
	private String flexAddress;
	private String sendTo;
	private String device;
    private AgentAdapter api;
    
	public CTIFlexAPI() {
		flexAddress = "127.0.0.1";
		sendTo = "";
		device = "VOICE-JB";
		state = "Criando...";
        log = new StringBuffer();
        api = new AgentAdapter();
	}
	private void setToLog(String msg) {
		System.out.println(msg);
		log.append(Calendar.getInstance().getTime());
		log.append(":");
		log.append(msg);
		log.append("<br>");
		int len = log.length();
		if (len>1000) {
			log.delete(1, len-1000);
		}
	}
	public Object getInstance() {
		return this;
	}
	public void setName(String name) {
		this.device = name;
	}
	public String getName() {
		return device;
	}
	public String getFlexAddress() {
		return flexAddress;
	}
	public void setFlexAddress(String flexAddress) {
		this.flexAddress = flexAddress;
	}
	public int getPort() {
		return CTISERVERFLEX_PORT;
	}
	public String getLog() {
		return log.toString();
	}
	public void start() {
		// inicia a operacao.
		// este metodo he chamado pelo JBoss
		state = "Iniciando";
		setToLog("Iniciando");
        api.getCommandControl().addListener(this);
        api.getStream().addListener(this);
		connect();
	}
	public void stop() {
		// finaliza a operacao.
		// este metodo he chamado pelo JBoss
		state = "Finalizando";
		setToLog("Finalizando");
        api.getCommandControl().removeListener(this);
		api.getStream().removeListener(this);
		disconnect();
	}
	public String connect() {
		state = "Conectando";
		setToLog("Conectando");
		try {
			String URL = "//" + flexAddress + ":" + CTISERVERFLEX_PORT;
	        setToLog("Device=" + device);
	        api.getApplicationInfo().getDevice().setRamal(device);
	        setToLog("AppNumber=" + APP_NUMBER);
	        api.getApplicationInfo().setNumber(APP_NUMBER);
	        setToLog("Version=" + VERSION);
	        api.getApplicationInfo().setVersion(VERSION);
	        setToLog("URL=" + URL);
	        api.getStream().setURL(URL);
	        setToLog("Openning");
	        api.getStream().open();
	        return "Sucesso";
		}
		catch(Exception e) {
			state = "Erro ao conectar";
			setToLog("Exception:" + e.getMessage());
			return "Exception:" + e.getMessage();
		}
	}
	public String disconnect() {
		state = "Desconectando";
		setToLog("Desconectando");
		try {
	        api.getStream().close();
	        return "Sucesso";
		}
		catch(Exception e) {
			state = "Erro ao desconectar";
			setToLog("Exception:" + e.getMessage());
			return "Exception:" + e.getMessage();
		}
	}
	private String sendSetSendTo() {
		try {
			if (sendTo.length() == 0)
				return "Sucesso";
			String []devs = sendTo.split(",");
			for (int d=0; d<devs.length; d++) {
				new SetSendTo(devs[d]);
			}
			return "Sucesso";
		}
		catch(Exception e) {
			return "Exception:" + e.getMessage();
		}
	}
	synchronized public String setSendTo(String devices) {
		LinkedList listSendTo = new LinkedList();
		if (sendTo.length()>0) {
			String []bkp = sendTo.split(",");
			for (int r=0; r<bkp.length; r++) {
				listSendTo.add(bkp[r]);
			}
		}
		Object []devs = devices.split(",");
		for (int d=0; d<devs.length; d++) {
			if (!listSendTo.contains(devs[d])) {
				if (api.getStream().isConnected()) {
					new SetSendTo(devs[d].toString());
				}
				sendTo = sendTo + (sendTo.length()>0?",":"") + devs[d].toString();
			}
		}
		return "Sucesso";
	}
	public String getSendTo() {
		return sendTo;
	}
	public String getState() {
		return state;
	}
	public String viewTables() {
		Collection tables = api.getTables();
		return HTMLHelper.getTables(tables);
	}
	public String viewTable(String tableName) {
		Table table = api.getTable(tableName);
		if (table == null)
			return "A tabela [" + tableName + "] não existe.";
		return HTMLHelper.getTable(table) ;
	}
	public String viewCalls() {
		return viewTable("Calls");
	}
	public String viewAgents() {
		return viewTable("Agents");
	}
	public String viewInformations() {
		return viewTable("Informations");
	}
	public String viewConnection() {
		StringBuffer ret = new StringBuffer();
		ret.append("<table width=100% border>");
		ret.append("<tr><td>Estado</td><td>" + state + "</td></tr>");
		ret.append("</table>");
		return ret.toString();
	}
	public String viewLog() {
		StringBuffer ret = new StringBuffer();
		ret.append("<table width=100% border>");
		ret.append("<tr><td>Log</td></tr><tr><td>" + getLog() + "</td></tr>");
		ret.append("</table>");
		return ret.toString();
	}
	public void onError() {
		state = "Erro ao conectar";
		setToLog("onError(): Erro na conexão");
	}
	public void onDisconnect() {
		state = "Desconectado";
		setToLog("onDisconnected(): Desconectado do Flex");
	}
	public void onConnect() {
		state = "Conectado";
		setToLog("onConnected(): Conectado ao Flex");
		sendSetSendTo();
	}
	public void onReceive(Protocol arg0) {
	}
	private String commandSynch(Command command) {
		DefaultProcessor proc = new DefaultProcessor(api);
		setToLog("Enviando comando: " + command.toString());
		int ret = proc.execute(command);
		setToLog("Retorno=" + ret);
		return "Retorno=" + ret;
	}
	public String commandMakeCall(String device, String deviceTo) {
		CommandMakeCall cmd = new CommandMakeCall();
		cmd.setDevice(device);
		cmd.setDeviceTo(deviceTo);
		return commandSynch(cmd);
	}
	public String commandClearCall(String device) {
		Table calls = api.getCalls();
		for (int c=0; c<calls.size(); c++) {
			Record rec = (Record)calls.get(c);
			if (rec.get("DEV").equals(device)) {
				if ("[27][26][3][7]".indexOf(rec.get("CST").toString())>0) {
					CommandConnectionClear cmd = new CommandConnectionClear();
					cmd.setToCall((AgentCall)rec);
					return commandSynch(cmd);
				}
			}
		}
		return "Erro. Não encontrou nenhuma chamada no ramal [" + device + "] com um estado compatível com o comando.";
	}
	public String commandAnswerCall(String device) {
		Table calls = api.getCalls();
		for (int c=0; c<calls.size(); c++) {
			Record rec = (Record)calls.get(c);
			if (rec.get("DEV").equals(device)) {
				if ("[3]".indexOf(rec.get("CST").toString())>0) {
					CommandAnswer cmd = new CommandAnswer();
					cmd.setToCall((AgentCall)rec);
					return commandSynch(cmd);
				}
			}
		}
		return "Erro. Não encontrou nenhuma chamada no ramal [" + device + "] com um estado compatível com o comando.";
	}
	private void processLogonOk() {
		//sendSetSendTo();
	}
	public void onCommandResponse(Command arg0) {
		if (arg0 instanceof CommandLogonAPI) {
			if (arg0.getResponse().getResponse()==0)
				processLogonOk();
		}
	}
	public void onCommandTimeout(Command arg0) {
	}
}

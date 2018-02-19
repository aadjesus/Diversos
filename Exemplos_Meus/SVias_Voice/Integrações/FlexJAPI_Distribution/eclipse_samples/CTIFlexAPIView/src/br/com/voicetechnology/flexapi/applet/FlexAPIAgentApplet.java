package br.com.voicetechnology.flexapi.applet;

import java.awt.Dimension;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.net.URISyntaxException;
import java.net.URL;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;

import javax.swing.JApplet;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JPasswordField;
import javax.swing.JTextField;
import javax.swing.SwingConstants;

import netscape.javascript.JSObject;
import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentInformations;
import br.com.voicetechnology.flexapi.agentadapter.synchronous.DefaultProcessor;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.executable.Command;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.util.Protocol;

/**
 * @author Roberto
 * Voice Technology Ind.<br>
 *<br>
 *<br>
 * <b>FlexAPIAgentApplet</b><br>
 * Container para a JAPI; api java para comunicação com o CTIServerFlex.<br>
 * Este container, visa a utilização da API java em ambientes web, onde o aplicativo 
 * que instanciará a API é um browser.<br>
 * Possui métodos para acesso a API principal e chama javascripts automaticamente 
 * quando eventos são notificados pelo servidor, caso eles existam.<br>
 * Como a conexão entre o applet e o servidor CTI é através de TCP-IP, algumas observações são necessárias:<br>
 * O java possui uma segurança que impede a comunicação TCP-IP que não seja entre a 
 * máquina cliente (a que roda o browser) e o servidor que forneceu o applet (servidor web).<br>
 * Portanto, temos duas situações:<br>
 * <br>
 * - Se o applet estiver hospedado no próprio servidor CTI.<br>
 * Não há problema com a segurança do java, a conexão será possível, 
 * mas o próprio browser poderá barrar as comunicações entre 
 * frames, se eles estiverem  em servidores diferentes. Isto ocorre, se tivermos 
 * um frame com o applet e outro frame com a tela do front-end.<br>
 * <br>
 * - Se o applet estiver hospedado em um servidor diferente, por exemplo no web server do front-end.<br>
 * Nesta situação, não temos problemas com o browser quando a chamadas entre frames, já que os frames da 
 * página principal, vem do mesmo servidor, mas temos um problema de segurança, sendo que um 
 * servidor forneceu o applet, e este applet está tentando se conectar em outra máquina, a do servidor CTI.<br>
 * Para que esta solução funcione, deve-se permitir este tipo de interação.<br>
 * Na máquina cliente que rodará o browser, deve-se colocar no arquivo 
 * java.policy que fica no subdiretorio “ %javapath%/lib/security “ dentro 
 * da sessao grant {}:<br>
 * <b>permission java.net.SocketPermission "IPFLEX:2556", "connect,resolve";</b><br>
 * Onde IPFLEX, e o endereço IP do servidor CTI.<br>
 * Parte do arquivo ficará como abaixo:<br>
 * ...<br>
 * grant {<br>
 * 		permission java.lang.RuntimePermission "stopThread";<br>
 * 		permission java.net.SocketPermission "localhost:1024-", "listen";<br>
 * 		permission java.net.SocketPermission "192.168.0.10:2556", "connect,resolve";<br>
 * 	...<br>
 */
public class FlexAPIAgentApplet extends JApplet implements FlexStreamListener, CollectionListener {
	private JSObject window = null;
	private class ScriptFunctionCall implements Runnable {
		private String functionName;
		private Object []objs;
		public ScriptFunctionCall(String functionName, Object []objs) {
			this.functionName = functionName;
			this.objs = objs;
		}
		public void run() {
			try {
				AgentAdapter.getLogger().info("Rodando script:" + functionName + "(" + objs + ")");
				window.call(functionName, objs);
			}
			catch (Exception e) {
				AgentAdapter.getLogger().info("Não encontrou função:" + functionName);
				AgentAdapter.getLogger().throwing("FlexAPIAgentApplet", "runScript", e);
			}
		}
	}
	
	public static final String VERSION = "1.4.0";
	private Executor executor;
	private AgentAdapter agentAdapter = null;  //  @jve:decl-index=0:
	private String prefixFuncName = null;  //  @jve:decl-index=0:
	private JPanel pnlContainer = null;
	private JButton btnMakeCall = null;
	private JButton btnAnswer = null;
	private JButton btnClear = null;
	private JButton btnConsulta = null;
	private JButton btnTransfer = null;
	private JButton btnReconnect = null;
	private JTextField txtDial = null;
	private JLabel lblResult = null;
	private JButton btnLogon = null;
	private JButton btnLogoff = null;
	private JButton btnReady = null;
	private JButton btnNotReady = null;
	private JLabel lblpin = null;
	private JLabel lblPass = null;
	private JTextField txtPin = null;
	private JPasswordField txtPass = null;
	private JLabel lblGroup = null;
	private JTextField txtGroup = null;
	private JButton btnConnect = null;
	private JTextField txtAddress = null;
	private JButton btnDisconnect = null;
	private JTextField txtDevice = null;
	private JLabel lblAddress = null;
	private JLabel lblDevice = null;
	/**
	 * Não deve ser chamado diretamente.
	 */
	public FlexAPIAgentApplet() {
		super();
		executor = Executors.newSingleThreadExecutor();
	}
	/**
	 * Busca o valor de uma variável associada a chamada atual.<br>
	 * Este método deve ser chamado para recuperar o dado de transferência de tela.<br>
	 * Uma variável é criada, ou alterada quando a chamada é iniciada. Quando a chamada 
	 * chega ao front-end, este método deve ser envocado para recuperar o valor desta 
	 * variável.
	 * 
	 * @param name
	 * 	Nome previamente combinado para transferência de dados entre aplicativos.
	 * @return
	 * 	String com o valor da variável solicitada.
	 */
	public String getValue(String name) {
		AgentInformations informations = new AgentInformations(agentAdapter.getInformations());
		return informations.getValue(name);
	}
	/**
	 * Não deve ser chamado diretamente.
	 */
	public void init() {
		this.setSize(new Dimension(476, 216));
        this.setContentPane(getPnlContainer());
		this.prefixFuncName  = getParent().getName() + "_";
		try {
			this.window = JSObject.getWindow(this);
		}
		catch (Exception e) {
			AgentAdapter.getLogger().throwing("FlexAPIAgentApplet", "init", e);
		}
		agentAdapter =  new AgentAdapter();
		
		agentAdapter.getStream().addListener(this);
		
		agentAdapter.getApplicationInfo().getDevice().setRamal(getString("device", "0"));
		agentAdapter.getApplicationInfo().setNumber(getInteger("number", 0));
		agentAdapter.getApplicationInfo().setVersion(getString("version", FlexAPI.VERSION));
		URL url = this.getCodeBase();
		String addr = "//" + url.getHost() + ":2556";
		addr = getString("url", addr);
		try {
			AgentAdapter.getLogger().info("URL=" + addr);
			agentAdapter.getStream().setURL(addr);
			
			txtAddress.setText(agentAdapter.getStream().getURL());
			txtDevice.setText(agentAdapter.getApplicationInfo().getDevice().getRamal());
		} catch (URISyntaxException e) {
			AgentAdapter.getLogger().throwing("FlexAPIAgentApplet", "init", e);
		}
	}
	private int getInteger(String name, int def) {
		try {
			return Integer.parseInt(getParameter(name));
		} catch (Exception e) {
			return def;
		}
	}
	private String getString(String name, String def) {
		String val = getParameter(name);
		if (val == null)
			return def;
		return val;
	}
	/**
	 * Método para acessar a API java.
	 * @return
	 * Retorna uma instância de AgentAdapter, que é a API principal para comunicação 
	 * com o servidor CTI.
	 */
	public AgentAdapter getAPI() {
		return agentAdapter;
	}
	/**
	 * Inicia a comunicação com o servidor CTI.<br>
	 * Este deve ser o primeiro método chamado.
	 */
	public void openAPI() {
		AgentAdapter.getLogger().info("API.openAPI()");
        agentAdapter.getStream().open();
    }
	/**
	 * Fecha a conexão com o servidor CTI.<br>
	 * Após a chamada deste método, nenhum comando será executado 
	 * e mais nenhum evento chegará.
	 */
	public void closeAPI() {
		AgentAdapter.getLogger().info("API.closeAPI()");
    	agentAdapter.getStream().close();
    }
	private void runScript (String script, Object obj) {
		String functionName = prefixFuncName + script;
		executor.execute(new ScriptFunctionCall(functionName, new Object[]{obj}));
	}
	private void runScript (String script, Object obj1, Object obj2, Object obj3) {
		String functionName = prefixFuncName + script;
		Object []objs = new Object[]{obj1, obj2, obj3};
		executor.execute(new ScriptFunctionCall(functionName, objs));
	}
	/**
	 * Não deve ser chamado diretamente.<br>
	 * Este método é chamado automaticamente quando ocorre uma desconexão do servidor CTI.<br>
	 * Chama uma função javascript hospedado no browser com o nome:<br>
	 * AppletName_onDisconnect(){}
	 */
	public void onDisconnect() {
		runScript("onDisconnect", null);
	}
	/**
	 * Não deve ser chamado diretamente.<br>
	 * Este método é chamado automaticamente quando ocorre a conexão com o servidor CTI.<br>
	 * Chama uma função javascript hospedado no browser com o nome:<br>
	 * AppletName_onConnect(){}
	 */
	public void onConnect() {
		runScript("onConnect", null);
		agentAdapter.getCalls().addListener(this);
		agentAdapter.getAgents().addListener(this);
	}
	/**
	 * Não deve ser chamado diretamente.<br>
	 * Este método é chamado automaticamente quando ocorre um erro na desconexão com o servidor CTI.<br>
	 * Chama uma função javascript hospedado no browser com o nome:<br>
	 * AppletName_onError(){}
	 */
	public void onError() {
		runScript("onError", null);
	}
	/**
	 * Não deve ser chamado diretamente.<br>
	 * Este método é chamado automaticamente quando dados chegam pela conexão 
	 * com o servidor CTI.<br>
	 * Chama uma função javascript hospedado no browser com o nome:<br>
	 * AppletName_onReceive(Protocol protocol){}
	 */
	public void onReceive(Protocol arg0) {
		runScript("onReceive", arg0);
	}
	
	private int processSync(Command cmd) {
		if (cmd == null) {
			return -2;
		}
		AgentAdapter.getLogger().info("Executando comando Sync:" + cmd);
		DefaultProcessor df = new DefaultProcessor(agentAdapter);
		return df.execute(cmd);
	}
	
	public int makeCall(String destination) {
		Command cmd = agentAdapter.getCommandMakeCall(
				agentAdapter.getApplicationInfo().getDevice().getRamal(), 
				destination);
		return processSync(cmd);
	}
	
	public int consultationCall(String destination) {
		Command cmd = agentAdapter.getCommandConsultation(destination);
		return processSync(cmd);
	}
	
	public int transferCall() {
		Command cmd = agentAdapter.getCommandTransfer();
		return processSync(cmd);
	}
	
	public int reconnectCall() {
		Command cmd = agentAdapter.getCommandReconnect();
		return processSync(cmd);
	}
	
	public int clearCall() {
		Command cmd = agentAdapter.getCommandConnectionClear();
		return processSync(cmd);
	}
	
	public int answerCall() {
		Command cmd = agentAdapter.getCommandAnswer();
		return processSync(cmd);
	}
	
	public int agentLogon(String pin, String pas, String group) {
		Command cmd = agentAdapter.getCommandAgentLogon(pin, pas, group);
		return processSync(cmd);
	}
	
	public int agentLogoff(String pin, String pas, String group) {
		Command cmd = agentAdapter.getCommandAgentLogoff(pin, pas, group);
		return processSync(cmd);
	}
	
	public int agentReady() {
		Command cmd = agentAdapter.getCommandAgentReady();
		return processSync(cmd);
	}
	
	public int agentNotReady(int cause) {
		Command cmd = agentAdapter.getCommandAgentNotReady(cause);
		return processSync(cmd);
	}
	
	public int agentReady(int cause) {
		Command cmd = agentAdapter.getCommandAgentWorkReady(cause);
		return processSync(cmd);
	}
	
	/**
	 * Não deve ser chamado diretamente.<br>
	 * Este método é chamado automaticamente quando um registro é 
	 * adicionado a uma tabela interna.<br>
	 * Chama uma função javascript hospedado no browser com o nome:<br>
	 * AppletName_TableName_added(table, register, index){}
	 */
	public void added(Object arg0, Object arg1, int arg2) {
		Table table = (Table)arg0;
		runScript(table.getName() + "_added", arg0, arg1, Integer.valueOf(arg2));
	}
	/**
	 * Não deve ser chamado diretamente.<br>
	 * Este método é chamado automaticamente quando um registro é 
	 * apagado de uma tabela interna.<br>
	 * Chama uma função javascript hospedado no browser com o nome:<br>
	 * AppletName_TableName_deleted(table, register, index){}
	 */
	public void deleted(Object arg0, Object arg1, int arg2) {
		Table table = (Table)arg0;
		runScript(table.getName() + "_deleted", arg0, arg1, Integer.valueOf(arg2));
	}
	/**
	 * Não deve ser chamado diretamente.<br>
	 * Este método é chamado automaticamente quando um registro é 
	 * alterado de uma tabela interna.<br>
	 * Chama uma função javascript hospedado no browser com o nome:<br>
	 * AppletName_TableName_updated(table, register, index){}
	 */
	public void updated(Object arg0, Object arg1, int arg2) {
		Table table = (Table)arg0;
		runScript(table.getName() + "_updated", arg0, arg1, Integer.valueOf(arg2));
	}
	private void setResult(int result) {
		lblResult.setText("Result=" + result);
	}
	/**
	 * This method initializes pnlContainer	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getPnlContainer() {
		if (pnlContainer == null) {
			GridBagConstraints gridBagConstraints42 = new GridBagConstraints();
			gridBagConstraints42.gridx = 1;
			gridBagConstraints42.fill = GridBagConstraints.BOTH;
			gridBagConstraints42.gridy = 1;
			lblDevice = new JLabel();
			lblDevice.setText("Device:");
			lblDevice.setHorizontalAlignment(SwingConstants.RIGHT);
			GridBagConstraints gridBagConstraints33 = new GridBagConstraints();
			gridBagConstraints33.gridx = 1;
			gridBagConstraints33.fill = GridBagConstraints.BOTH;
			gridBagConstraints33.gridy = 0;
			lblAddress = new JLabel();
			lblAddress.setText("Address:");
			lblAddress.setHorizontalAlignment(SwingConstants.RIGHT);
			GridBagConstraints gridBagConstraints13 = new GridBagConstraints();
			gridBagConstraints13.fill = GridBagConstraints.BOTH;
			gridBagConstraints13.gridy = 1;
			gridBagConstraints13.weightx = 1.0;
			gridBagConstraints13.gridx = 2;
			GridBagConstraints gridBagConstraints32 = new GridBagConstraints();
			gridBagConstraints32.gridx = 0;
			gridBagConstraints32.fill = GridBagConstraints.BOTH;
			gridBagConstraints32.gridy = 1;
			GridBagConstraints gridBagConstraints22 = new GridBagConstraints();
			gridBagConstraints22.fill = GridBagConstraints.BOTH;
			gridBagConstraints22.gridy = 0;
			gridBagConstraints22.weightx = 1.0;
			gridBagConstraints22.gridx = 2;
			GridBagConstraints gridBagConstraints12 = new GridBagConstraints();
			gridBagConstraints12.gridx = 0;
			gridBagConstraints12.fill = GridBagConstraints.BOTH;
			gridBagConstraints12.gridy = 0;
			GridBagConstraints gridBagConstraints111 = new GridBagConstraints();
			gridBagConstraints111.fill = GridBagConstraints.BOTH;
			gridBagConstraints111.gridy = 6;
			gridBagConstraints111.weightx = 1.0;
			gridBagConstraints111.gridx = 3;
			GridBagConstraints gridBagConstraints10 = new GridBagConstraints();
			gridBagConstraints10.gridx = 2;
			gridBagConstraints10.fill = GridBagConstraints.BOTH;
			gridBagConstraints10.gridy = 6;
			lblGroup = new JLabel();
			lblGroup.setText("Group:");
			lblGroup.setHorizontalAlignment(SwingConstants.RIGHT);
			GridBagConstraints gridBagConstraints9 = new GridBagConstraints();
			gridBagConstraints9.fill = GridBagConstraints.BOTH;
			gridBagConstraints9.gridy = 5;
			gridBagConstraints9.weightx = 1.0;
			gridBagConstraints9.gridx = 3;
			GridBagConstraints gridBagConstraints8 = new GridBagConstraints();
			gridBagConstraints8.fill = GridBagConstraints.BOTH;
			gridBagConstraints8.gridy = 4;
			gridBagConstraints8.weightx = 1.0;
			gridBagConstraints8.gridx = 3;
			GridBagConstraints gridBagConstraints71 = new GridBagConstraints();
			gridBagConstraints71.gridx = 2;
			gridBagConstraints71.fill = GridBagConstraints.BOTH;
			gridBagConstraints71.gridy = 5;
			lblPass = new JLabel();
			lblPass.setText("PASS:");
			lblPass.setHorizontalAlignment(SwingConstants.RIGHT);
			GridBagConstraints gridBagConstraints61 = new GridBagConstraints();
			gridBagConstraints61.gridx = 2;
			gridBagConstraints61.fill = GridBagConstraints.BOTH;
			gridBagConstraints61.gridy = 4;
			lblpin = new JLabel();
			lblpin.setText("PIN:");
			lblpin.setHorizontalAlignment(SwingConstants.RIGHT);
			GridBagConstraints gridBagConstraints41 = new GridBagConstraints();
			gridBagConstraints41.gridx = 1;
			gridBagConstraints41.fill = GridBagConstraints.BOTH;
			gridBagConstraints41.gridy = 4;
			GridBagConstraints gridBagConstraints31 = new GridBagConstraints();
			gridBagConstraints31.gridx = 0;
			gridBagConstraints31.fill = GridBagConstraints.BOTH;
			gridBagConstraints31.gridy = 6;
			GridBagConstraints gridBagConstraints21 = new GridBagConstraints();
			gridBagConstraints21.gridx = 0;
			gridBagConstraints21.fill = GridBagConstraints.BOTH;
			gridBagConstraints21.gridy = 5;
			GridBagConstraints gridBagConstraints11 = new GridBagConstraints();
			gridBagConstraints11.gridx = 0;
			gridBagConstraints11.fill = GridBagConstraints.BOTH;
			gridBagConstraints11.gridy = 4;
			GridBagConstraints gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridx = 0;
			gridBagConstraints.fill = GridBagConstraints.BOTH;
			gridBagConstraints.gridy = 7;
			lblResult = new JLabel();
			lblResult.setText("Result=-1");
			GridBagConstraints gridBagConstraints7 = new GridBagConstraints();
			gridBagConstraints7.fill = GridBagConstraints.BOTH;
			gridBagConstraints7.gridy = 3;
			gridBagConstraints7.weightx = 1.0;
			gridBagConstraints7.gridwidth = 2;
			gridBagConstraints7.gridx = 2;
			GridBagConstraints gridBagConstraints6 = new GridBagConstraints();
			gridBagConstraints6.gridx = 3;
			gridBagConstraints6.fill = GridBagConstraints.BOTH;
			gridBagConstraints6.gridy = 2;
			GridBagConstraints gridBagConstraints4 = new GridBagConstraints();
			gridBagConstraints4.gridx = 2;
			gridBagConstraints4.fill = GridBagConstraints.BOTH;
			gridBagConstraints4.gridy = 2;
			GridBagConstraints gridBagConstraints1 = new GridBagConstraints();
			gridBagConstraints1.gridx = 1;
			gridBagConstraints1.fill = GridBagConstraints.BOTH;
			gridBagConstraints1.gridy = 3;
			GridBagConstraints gridBagConstraints5 = new GridBagConstraints();
			gridBagConstraints5.gridx = 1;
			gridBagConstraints5.fill = GridBagConstraints.BOTH;
			gridBagConstraints5.gridy = 2;
			GridBagConstraints gridBagConstraints3 = new GridBagConstraints();
			gridBagConstraints3.gridx = 0;
			gridBagConstraints3.fill = GridBagConstraints.BOTH;
			gridBagConstraints3.gridy = 3;
			GridBagConstraints gridBagConstraints2 = new GridBagConstraints();
			gridBagConstraints2.gridx = 0;
			gridBagConstraints2.fill = GridBagConstraints.BOTH;
			gridBagConstraints2.gridy = 2;
			pnlContainer = new JPanel();
			pnlContainer.setLayout(new GridBagLayout());
			pnlContainer.add(getBtnAnswer(), gridBagConstraints2);
			pnlContainer.add(getBtnClear(), gridBagConstraints3);
			pnlContainer.add(getBtnTransfer(), gridBagConstraints5);
			pnlContainer.add(getBtnReconnect(), gridBagConstraints1);
			pnlContainer.add(getBtnMakeCall(), gridBagConstraints4);
			pnlContainer.add(getBtnConsulta(), gridBagConstraints6);
			pnlContainer.add(getTxtDial(), gridBagConstraints7);
			pnlContainer.add(lblResult, gridBagConstraints);
			pnlContainer.add(getBtnLogon(), gridBagConstraints11);
			pnlContainer.add(getBtnLogoff(), gridBagConstraints21);
			pnlContainer.add(getBtnReady(), gridBagConstraints31);
			pnlContainer.add(getBtnNotReady(), gridBagConstraints41);
			pnlContainer.add(lblpin, gridBagConstraints61);
			pnlContainer.add(lblPass, gridBagConstraints71);
			pnlContainer.add(getTxtPin(), gridBagConstraints8);
			pnlContainer.add(getTxtPass(), gridBagConstraints9);
			pnlContainer.add(lblGroup, gridBagConstraints10);
			pnlContainer.add(getTxtGroup(), gridBagConstraints111);
			pnlContainer.add(getBtnConnect(), gridBagConstraints12);
			pnlContainer.add(getTxtAddress(), gridBagConstraints22);
			pnlContainer.add(getBtnDisconnect(), gridBagConstraints32);
			pnlContainer.add(getTxtDevice(), gridBagConstraints13);
			pnlContainer.add(lblAddress, gridBagConstraints33);
			pnlContainer.add(lblDevice, gridBagConstraints42);
		}
		return pnlContainer;
	}
	/**
	 * This method initializes btnMakeCall	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnMakeCall() {
		if (btnMakeCall == null) {
			btnMakeCall = new JButton();
			btnMakeCall.setText("Dial");
			btnMakeCall.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					setResult(makeCall(txtDial.getText()));
				}
			});
		}
		return btnMakeCall;
	}
	/**
	 * This method initializes btnAnswer	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnAnswer() {
		if (btnAnswer == null) {
			btnAnswer = new JButton();
			btnAnswer.setText("Answer");
			btnAnswer.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					setResult(answerCall());
				}
			});
		}
		return btnAnswer;
	}
	/**
	 * This method initializes btnClear	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnClear() {
		if (btnClear == null) {
			btnClear = new JButton();
			btnClear.setText("Clear");
			btnClear.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					setResult(clearCall());
				}
			});
		}
		return btnClear;
	}
	/**
	 * This method initializes btnConsulta	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnConsulta() {
		if (btnConsulta == null) {
			btnConsulta = new JButton();
			btnConsulta.setText("Consult");
			btnConsulta.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					setResult(consultationCall(txtDial.getText()));
				}
			});
		}
		return btnConsulta;
	}
	/**
	 * This method initializes btnTransfer	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnTransfer() {
		if (btnTransfer == null) {
			btnTransfer = new JButton();
			btnTransfer.setText("Transfer");
			btnTransfer.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					setResult(transferCall());
				}
			});
		}
		return btnTransfer;
	}
	/**
	 * This method initializes btnReconnect	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnReconnect() {
		if (btnReconnect == null) {
			btnReconnect = new JButton();
			btnReconnect.setText("Reconnect");
			btnReconnect.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					setResult(reconnectCall());
				}
			});
		}
		return btnReconnect;
	}
	/**
	 * This method initializes txtDial	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getTxtDial() {
		if (txtDial == null) {
			txtDial = new JTextField();
		}
		return txtDial;
	}
	/**
	 * This method initializes btnLogon	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnLogon() {
		if (btnLogon == null) {
			btnLogon = new JButton();
			btnLogon.setText("AgentLogon");
			btnLogon.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					agentLogon(txtPin.getText(), new String(txtPass.getPassword()), txtGroup.getText());
				}
			});
		}
		return btnLogon;
	}
	/**
	 * This method initializes btnLogoff	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnLogoff() {
		if (btnLogoff == null) {
			btnLogoff = new JButton();
			btnLogoff.setText("AgentLogoff");
			btnLogoff.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					agentLogoff(txtPin.getText(), new String(txtPass.getPassword()), txtGroup.getText());
				}
			});
		}
		return btnLogoff;
	}
	/**
	 * This method initializes btnReady	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnReady() {
		if (btnReady == null) {
			btnReady = new JButton();
			btnReady.setText("AgentReady");
			btnReady.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					agentReady();
				}
			});
		}
		return btnReady;
	}
	/**
	 * This method initializes btnNotReady	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnNotReady() {
		if (btnNotReady == null) {
			btnNotReady = new JButton();
			btnNotReady.setText("AgentNotReady");
			btnNotReady.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					agentNotReady(0);
				}
			});
		}
		return btnNotReady;
	}
	/**
	 * This method initializes txtPin	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getTxtPin() {
		if (txtPin == null) {
			txtPin = new JTextField();
		}
		return txtPin;
	}
	/**
	 * This method initializes txtPass	
	 * 	
	 * @return javax.swing.JPasswordField	
	 */
	private JPasswordField getTxtPass() {
		if (txtPass == null) {
			txtPass = new JPasswordField();
		}
		return txtPass;
	}
	/**
	 * This method initializes txtGroup	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getTxtGroup() {
		if (txtGroup == null) {
			txtGroup = new JTextField();
		}
		return txtGroup;
	}
	/**
	 * This method initializes btnConnect	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnConnect() {
		if (btnConnect == null) {
			btnConnect = new JButton();
			btnConnect.setText("Connect");
			btnConnect.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					try {
						agentAdapter.getStream().setURL(txtAddress.getText().toString());
						openAPI();
					}
					catch(Exception t) {
						AgentAdapter.getLogger().throwing("FlexAPIAgentApplet", "getBtnConnect.actionPerformed", t);
					}
				}
			});
		}
		return btnConnect;
	}
	/**
	 * This method initializes txtAddress	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getTxtAddress() {
		if (txtAddress == null) {
			txtAddress = new JTextField();
			txtAddress.setText("//127.0.0.1:2556");
		}
		return txtAddress;
	}
	/**
	 * This method initializes btnDisconnect	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnDisconnect() {
		if (btnDisconnect == null) {
			btnDisconnect = new JButton();
			btnDisconnect.setText("Disconnect");
			btnDisconnect.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					closeAPI();
				}
			});
		}
		return btnDisconnect;
	}
	/**
	 * This method initializes txtDevice	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getTxtDevice() {
		if (txtDevice == null) {
			txtDevice = new JTextField();
		}
		return txtDevice;
	}
}  //  @jve:decl-index=0:visual-constraint="10,10"

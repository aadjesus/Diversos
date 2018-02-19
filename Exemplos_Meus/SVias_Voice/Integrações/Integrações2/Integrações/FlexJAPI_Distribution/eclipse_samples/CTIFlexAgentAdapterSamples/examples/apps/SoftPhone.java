/*
 * Created on 22/07/2004
 *
 */
package examples.apps;
import java.awt.BorderLayout;
import java.awt.Frame;
import java.awt.GridLayout;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.util.logging.Level;

import javax.swing.JApplet;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTable;

import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.executable.ExecuteCloseSession;
import br.com.voicetechnology.flexapi.agentadapter.executable.ExecuteOpenSession;
import br.com.voicetechnology.flexapi.agentadapter.executable.ExecuteSetSession;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Record;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.flexapi.views.table.TablePanel;
import br.com.voicetechnology.util.Protocol;
import examples.apps.swing.GetDialNumber;
import examples.tables.views.AgentInfoTableModel;
import examples.tables.views.AgentInfoTableModelListener;
/**
 * @author Roberto<br>
 * Voice Technology Ind.<br>
 * <br>
 * Teste de SoftPhone.
 */
public class SoftPhone extends JApplet implements WindowListener, CollectionListener, FlexStreamListener, AgentInfoTableModelListener {

	private AgentAdapter agentAdapter = null;
	private javax.swing.JPanel jContentPane = null;
	private javax.swing.JButton btnConnect = null;
	private javax.swing.JPanel jPanel1 = null;
	private javax.swing.JLabel jLabel = null;
	private javax.swing.JTextArea txtLog = null;
	private javax.swing.JTextField txtDevice = null;
	private javax.swing.JTabbedPane jTabbedPane = null;
	private javax.swing.JTextField txtConnection = null;
	private javax.swing.JLabel lblDevice = null;
	private javax.swing.JLabel lblConnection = null;
	private javax.swing.JScrollPane jScrollPane = null;
	private javax.swing.JDialog dlgLogon = null;  //  @jve:visual-info  decl-index=0 visual-constraint="11,323"
	private javax.swing.JPanel jContentPane1 = null;
	private examples.apps.swing.ButtonPane buttonPane = null;
	private javax.swing.JDialog dlgDial = null;  //  @jve:visual-info  decl-index=0 visual-constraint="198,324"
	private javax.swing.JPanel jContentPane2 = null;
	private javax.swing.JLabel jLabel1 = null;
	private javax.swing.JTextField txtNumber = null;
	private javax.swing.JButton jButton = null;
	private javax.swing.JButton btnOpenSession = null;
	private javax.swing.JPanel jPanel = null;
	private javax.swing.JTextField txtSDescription = null;
	private javax.swing.JLabel jLabel2 = null;
	private javax.swing.JTextField txtSIca = null;
	private javax.swing.JLabel jLabel3 = null;
	private javax.swing.JTextField txtSDevice = null;
	private javax.swing.JLabel jLabel4 = null;
	private javax.swing.JButton jButton1 = null;
	private javax.swing.JButton jButton2 = null;
	private JPanel jPanel2 = null;
	private JTable jTable = null;
	private JScrollPane jScrollPane1 = null;
	/**
	 * Construtor da classe.
	 */
	public SoftPhone() {
		super();
		initialize();
	}
	/**
	 * This method initializes this
	 */
	public void init() {
        this.setSize(429, 300);
        this.setContentPane(getJContentPane());
	}
	/**
	 * Inicialização do applet.
	 */
	private void initialize() {
        // cria a instância da api antes de iniciar os componentes da
        // tela.
        this.agentAdapter = new AgentAdapter();
        this.setContentPane(getJContentPane());
        this.setSize(500, 300);
        this.setLocation(0, 0);
        // adiciona os observadores na api, após a criação da tela,
        // pois existem notificações que utilizam os componentes visuais.
		try {
			agentAdapter.getTables().addListener(this);
			agentAdapter.getStream().addListener(this);
            buttonPane.setReasonCode("Motivo 1", 1);
            buttonPane.setReasonCode("Motivo 2", 2);
            buttonPane.setReasonCode("Motivo 3", 3);
			buttonPane.setAgentAdapter(agentAdapter);
			buttonPane.setOnGetDialNumber(
				new GetDialNumber() {
					/**
					 * @see examples.apps.swing.GetDialNumber#OnGetDialNumber()
					 */
					public String onGetDialNumber() {
						getDlgDial().setVisible(true);
						return getTxtNumber().getText();
					}
				}
			);
		} catch (Exception e) {
            FlexAPI.getLogger().log(Level.SEVERE, "Erro.", e);
		}
	}
	/**
     * Cria jContentPane
	 * @return JPanel
	 */
	private javax.swing.JPanel getJContentPane() {
		if (jContentPane == null) {
			jContentPane = new javax.swing.JPanel();
			jContentPane.setLayout(new java.awt.BorderLayout());
			jContentPane.add(getJPanel1(), java.awt.BorderLayout.CENTER);
			jContentPane.setPreferredSize(new java.awt.Dimension(50, 300));
			jContentPane.add(getJPanel2(), java.awt.BorderLayout.NORTH);
		}
		return jContentPane;
	}
	/**
	 * This method initializes jPanel2
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel2() {
		if (jPanel2 == null) {
			jPanel2 = new JPanel();
			jPanel2.setLayout(new BorderLayout());
			jPanel2.add(getButtonPane(), java.awt.BorderLayout.CENTER);
			jPanel2.add(getJPanel(), java.awt.BorderLayout.SOUTH);
		}
		return jPanel2;
	}
	/**
	 * This method initializes jTable
	 * @return javax.swing.JTable
	 */
	private JTable getJTable() {
		if (jTable == null) {
			jTable = new JTable();
            AgentInfoTableModel model = new AgentInfoTableModel(agentAdapter.getCalls(), "DEV,CLR,CST,LOCALTIME", "DTI", "Ramal,Chamador,Estado,TempoTotal,Início");
            model.setAgentAdapter(agentAdapter);
            model.setListener(this);
            jTable.setModel(model);
		}
		return jTable;
	}
	/**
	 * This method initializes jScrollPane1
	 * @return javax.swing.JScrollPane
	 */
	private JScrollPane getJScrollPane1() {
		if (jScrollPane1 == null) {
			jScrollPane1 = new JScrollPane();
			jScrollPane1.setViewportView(getJTable());
		}
		return jScrollPane1;
	}
    	/**
     * Main do aplicativo.
	 * @param args
     * Argumentos.
	 */
	public static void main(String[] args) {
		Frame frm = new Frame("Teste Flex Agent API");
		frm.setLayout(new GridLayout(1, 0));
		frm.setSize(500, 400);
		SoftPhone vt = new SoftPhone();
		frm.add(vt);
		vt.start();
		frm.addWindowListener(vt);
		frm.setVisible(true);
	}
	/**
	 * @see java.awt.event.WindowListener#windowOpened(java.awt.event.WindowEvent)
	 */
	public void windowOpened(WindowEvent arg0) {
	}
	/**
	 * @see java.awt.event.WindowListener#windowClosing(java.awt.event.WindowEvent)
	 */
	public void windowClosing(WindowEvent arg0) {
		System.exit(0);
	}
	/**
	 * @see java.awt.event.WindowListener#windowClosed(java.awt.event.WindowEvent)
	 */
	public void windowClosed(WindowEvent arg0) {
	}
	/**
	 * @see java.awt.event.WindowListener#windowIconified(java.awt.event.WindowEvent)
	 */
	public void windowIconified(WindowEvent arg0) {
	}
	/**
	 * @see java.awt.event.WindowListener#windowDeiconified(java.awt.event.WindowEvent)
	 */
	public void windowDeiconified(WindowEvent arg0) {
	}
	/**
	 * @see java.awt.event.WindowListener#windowActivated(java.awt.event.WindowEvent)
	 */
	public void windowActivated(WindowEvent arg0) {
	}
	/**
	 * @see java.awt.event.WindowListener#windowDeactivated(java.awt.event.WindowEvent)
	 */
	public void windowDeactivated(WindowEvent arg0) {
	}
	/**
     * Conecta ao servidor.
	 * @param connection
     * Endereço para conexão IP:PORTA
	 * @param ramal
     * Ramal para logon.
	 */
	private void connect(String connection, String ramal) {
		try {
			if (agentAdapter.getStream().isConnected())
				agentAdapter.getStream().close();
			agentAdapter.getStream().setURL(connection);
			agentAdapter.getApplicationInfo().getDevice().setRamal(ramal);
            agentAdapter.getApplicationInfo().setNumber(1);
            agentAdapter.getApplicationInfo().setVersion("1.0.0");
			agentAdapter.getStream().open();
		} catch (Exception e) {
            FlexAPI.getLogger().log(Level.SEVERE, "Erro.", e);
		}
	}
	/**
     * Cria btnConnect
	 * @return JButton
	 */
	private javax.swing.JButton getBtnConnect() {
		if (btnConnect == null) {
			btnConnect = new javax.swing.JButton();
			btnConnect.setBounds(42, 52, 88, 26);
			btnConnect.setText("Conecta");
			btnConnect.setActionCommand("Execute");
			btnConnect.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					connect(txtConnection.getText(), txtDevice.getText());
					dlgLogon.dispose();
					dlgLogon = null;
				}
			});
		}
		return btnConnect;
	}

	/**
	 * This method initializes jPanel1
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJPanel1() {
		if (jPanel1 == null) {
			jPanel1 = new javax.swing.JPanel();
			jPanel1.setLayout(new java.awt.BorderLayout());
			jPanel1.add(getJLabel(), java.awt.BorderLayout.SOUTH);
			jPanel1.add(getJScrollPane(), java.awt.BorderLayout.CENTER);
			jPanel1.add(getJTabbedPane(), java.awt.BorderLayout.NORTH);
		}
		return jPanel1;
	}
	/**
     * Cria jLabel.
	 * @return JLabel
	 */
	private javax.swing.JLabel getJLabel() {
		if (jLabel == null) {
			jLabel = new javax.swing.JLabel();
			jLabel.setText("Conexão");
		}
		return jLabel;
	}
	/**
	 * This method initializes txtLog
	 * @return javax.swing.JTextArea
	 */
	private javax.swing.JTextArea getTxtLog() {
		if (txtLog == null) {
			txtLog = new javax.swing.JTextArea();
		}
		return txtLog;
	}
	/**
	 * This method initializes txtDevice
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtDevice() {
		if (txtDevice == null) {
			txtDevice = new javax.swing.JTextField();
			txtDevice.setBounds(53, 5, 113, 20);
		}
		return txtDevice;
	}
	/**
	 * This method initializes jTabbedPane
	 * @return javax.swing.JTabbedPane
	 */
	private javax.swing.JTabbedPane getJTabbedPane() {
		if (jTabbedPane == null) {
			jTabbedPane = new javax.swing.JTabbedPane();
			jTabbedPane.setPreferredSize(new java.awt.Dimension(29, 200));
			jTabbedPane.addTab("Agent Info", null, getJScrollPane1(), null);
		}
		return jTabbedPane;
	}
	/**
	 * This method initializes lblDevice
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getLblDevice() {
		if (lblDevice == null) {
			lblDevice = new javax.swing.JLabel();
			lblDevice.setBounds(6, 5, 45, 19);
			lblDevice.setText("Device:");
			lblDevice.setHorizontalTextPosition(javax.swing.SwingConstants.TRAILING);
			lblDevice.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
		}
		return lblDevice;
	}
	/**
	 * This method initializes lblConnection
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getLblConnection() {
		if (lblConnection == null) {
			lblConnection = new javax.swing.JLabel();
			lblConnection.setBounds(6, 23, 45, 19);
			lblConnection.setText("Conn:");
			lblConnection.setHorizontalTextPosition(javax.swing.SwingConstants.TRAILING);
			lblConnection.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
		}
		return lblConnection;
	}
	/**
	 * This method initializes txtConnection
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtConnection() {
		if (txtConnection == null) {
			txtConnection = new javax.swing.JTextField();
			txtConnection.setBounds(53, 23, 113, 20);
			txtConnection.setText("//localhost:2556");
		}
		return txtConnection;
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onError()
	 */
	public void onError() {
		onDisconnect();
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onDisconnect()
	 */
	public void onDisconnect() {
		jLabel.setText("Desconectado do servidor");
		getDlgLogon().setVisible(true);
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onConnect()
	 */
	public void onConnect() {
		jLabel.setText("Conectado com servidor");
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onReceive(br.com.voicetechnology.util.Protocol)
	 */
	public void onReceive(Protocol protocol) {
	}
	/**
     * Notifica que uma tabela foi reestruturada.
	 * @param table
     * Tabela.
	 */
	public void onTableRestructured(Table table) {
		int idx = jTabbedPane.indexOfTab(table.getName());
		if (idx >= 0) jTabbedPane.remove(idx);
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#added(java.lang.Object, java.lang.Object, int)
	 */
	public void added(Object owner, Object item, int index) {
		if (item == null) return;
		Table table = (Table)item;
		txtLog.append("Tabela adicionada:" + table.getName() + "\r\n");
		txtLog.append(table.toString() + "\r\n");
        TablePanel tp = new TablePanel(table);
        if (table.getName().equals("Calls")) {
            tp.getTableModel().setVisibleFields("DEV,CLR,CST", "Ramal,#A,Estado");
        }
		jTabbedPane.addTab(table.getName(), tp);
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#deleted(java.lang.Object, java.lang.Object, int)
	 */
	public void deleted(Object owner, Object item, int index) {
        txtLog.append("Tabela apagada:");
		if (item == null) {
            txtLog.append("Todas\r\n");
            for (int i = jTabbedPane.getTabCount() - 1; i >= 0; i--) {
                if (!jTabbedPane.getTitleAt(i).equals("Agent Info")) {
                    jTabbedPane.remove(i);
                }
            }
            return;
        }
		txtLog.append(((Table)item).getName() + "\r\n");
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#updated(java.lang.Object, java.lang.Object, int)
	 */
	public void updated(Object owner, Object item, int index) {
		if (item == null) return;
		txtLog.append("Tabela alterada:" + ((Table)item).getName() + "\r\n");
	}
	/**
	 * This method initializes jScrollPane
	 * @return javax.swing.JScrollPane
	 */
	private javax.swing.JScrollPane getJScrollPane() {
		if (jScrollPane == null) {
			jScrollPane = new javax.swing.JScrollPane();
			jScrollPane.setViewportView(getTxtLog());
		}
		return jScrollPane;
	}
	/**
	 * This method initializes jContentPane1
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJContentPane1() {
		if (jContentPane1 == null) {
			jContentPane1 = new javax.swing.JPanel();
			jContentPane1.setLayout(null);
			jContentPane1.add(getTxtDevice(), null);
			jContentPane1.add(getLblDevice(), null);
			jContentPane1.add(getLblConnection(), null);
			jContentPane1.add(getTxtConnection(), null);
			jContentPane1.add(getBtnConnect(), null);
		}
		return jContentPane1;
	}
	/**
	 * This method initializes dlgLogon
	 * @return javax.swing.JDialog
	 */
	private javax.swing.JDialog getDlgLogon() {
		if (dlgLogon == null) {
			dlgLogon = new javax.swing.JDialog();
			dlgLogon.setContentPane(getJContentPane1());
			dlgLogon.setSize(178, 123);
			dlgLogon.setResizable(false);
			dlgLogon.setTitle("Logon");
			dlgLogon.setModal(true);
		}
		return dlgLogon;
	}
	/**
	 * This method initializes buttonPane
	 * @return br.com.voicetechnology.flexapi.agentadapter.swing.ButtonPane
	 */
	private examples.apps.swing.ButtonPane getButtonPane() {
		if (buttonPane == null) {
			buttonPane = new examples.apps.swing.ButtonPane();
			buttonPane.setPreferredSize(new java.awt.Dimension(532, 110));
		}
		return buttonPane;
	}
	/**
	 * This method initializes jContentPane2
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJContentPane2() {
		if (jContentPane2 == null) {
			jContentPane2 = new javax.swing.JPanel();
			jContentPane2.setLayout(new java.awt.BorderLayout());
			jContentPane2.add(getJLabel1(), java.awt.BorderLayout.WEST);
			jContentPane2.add(getTxtNumber(), java.awt.BorderLayout.CENTER);
			jContentPane2.add(getJButton(), java.awt.BorderLayout.SOUTH);
		}
		return jContentPane2;
	}
	/**
	 * This method initializes dlgDial
	 * @return javax.swing.JDialog
	 */
	private javax.swing.JDialog getDlgDial() {
		if (dlgDial == null) {
			dlgDial = new javax.swing.JDialog();
			dlgDial.setContentPane(getJContentPane2());
			dlgDial.setSize(178, 83);
			dlgDial.setTitle("Disca");
			dlgDial.setModal(true);
			dlgDial.setResizable(false);
		}
		return dlgDial;
	}
	/**
	 * This method initializes jLabel1
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel1() {
		if (jLabel1 == null) {
			jLabel1 = new javax.swing.JLabel();
			jLabel1.setText("Número:");
		}
		return jLabel1;
	}
	/**
	 * This method initializes txtNumber
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtNumber() {
		if (txtNumber == null) {
			txtNumber = new javax.swing.JTextField();
		}
		return txtNumber;
	}
	/**
	 * This method initializes jButton
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getJButton() {
		if (jButton == null) {
			jButton = new javax.swing.JButton();
			jButton.setText("Ok");
			jButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					dlgDial.dispose();
					dlgDial = null;
				}
			});
		}
		return jButton;
	}
	/**
	 * This method initializes btnOpenSession
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getBtnOpenSession() {
		if (btnOpenSession == null) {
			btnOpenSession = new javax.swing.JButton();
			btnOpenSession.setText("Open Session");
			btnOpenSession.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					ExecuteOpenSession cmd = new ExecuteOpenSession();
					cmd.setDevice(txtSDevice.getText());
					cmd.setInternalCallId(txtSIca.getText());
					cmd.setDescription(txtSDescription.getText());
					try {
						agentAdapter.execute(cmd);
					} catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando.", e1);
					}
				}
			});
		}
		return btnOpenSession;
	}
	/**
	 * This method initializes jPanel
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJPanel() {
		if (jPanel == null) {
			jPanel = new javax.swing.JPanel();
			java.awt.GridBagConstraints consGridBagConstraints2 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints3 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints5 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints6 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints4 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints7 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints8 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints9 = new java.awt.GridBagConstraints();
			java.awt.GridBagConstraints consGridBagConstraints10 = new java.awt.GridBagConstraints();
			consGridBagConstraints9.gridx = 2;
			consGridBagConstraints9.gridy = 2;
			consGridBagConstraints10.gridx = 2;
			consGridBagConstraints10.gridy = 1;
			consGridBagConstraints7.gridx = 2;
			consGridBagConstraints7.gridy = 0;
			consGridBagConstraints2.gridx = 0;
			consGridBagConstraints2.gridy = 0;
			consGridBagConstraints2.ipadx = 72;
			consGridBagConstraints2.ipady = 10;
			consGridBagConstraints3.gridx = 1;
			consGridBagConstraints3.gridy = 0;
			consGridBagConstraints3.weightx = 1.0;
			consGridBagConstraints3.fill = java.awt.GridBagConstraints.HORIZONTAL;
			consGridBagConstraints3.ipadx = 109;
			consGridBagConstraints3.ipady = 6;
			consGridBagConstraints5.gridx = 0;
			consGridBagConstraints5.gridy = 1;
			consGridBagConstraints5.ipadx = 52;
			consGridBagConstraints5.ipady = 10;
			consGridBagConstraints6.gridx = 1;
			consGridBagConstraints6.gridy = 1;
			consGridBagConstraints6.weightx = 1.0;
			consGridBagConstraints6.fill = java.awt.GridBagConstraints.HORIZONTAL;
			consGridBagConstraints6.ipadx = 49;
			consGridBagConstraints6.ipady = 6;
			consGridBagConstraints4.gridx = 0;
			consGridBagConstraints4.gridy = 2;
			consGridBagConstraints4.ipadx = 91;
			consGridBagConstraints4.ipady = 10;
			consGridBagConstraints8.gridx = 1;
			consGridBagConstraints8.gridy = 2;
			consGridBagConstraints8.weightx = 1.0;
			consGridBagConstraints8.fill = java.awt.GridBagConstraints.HORIZONTAL;
			consGridBagConstraints8.ipadx = 109;
			consGridBagConstraints8.ipady = 6;
			jPanel.setLayout(new java.awt.GridBagLayout());
			jPanel.add(getJLabel4(), consGridBagConstraints2);
			jPanel.add(getTxtSDevice(), consGridBagConstraints3);
			jPanel.add(getJLabel3(), consGridBagConstraints4);
			jPanel.add(getJLabel2(), consGridBagConstraints5);
			jPanel.add(getTxtSDescription(), consGridBagConstraints6);
			jPanel.add(getBtnOpenSession(), consGridBagConstraints7);
			jPanel.add(getTxtSIca(), consGridBagConstraints8);
			jPanel.add(getJButton1(), consGridBagConstraints9);
			jPanel.add(getJButton2(), consGridBagConstraints10);
		}
		return jPanel;
	}
	/**
	 * This method initializes txtSDescription
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtSDescription() {
		if (txtSDescription == null) {
			txtSDescription = new javax.swing.JTextField();
			txtSDescription.setText("Descripion");
		}
		return txtSDescription;
	}
	/**
	 * This method initializes jLabel2
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel2() {
		if (jLabel2 == null) {
			jLabel2 = new javax.swing.JLabel();
			jLabel2.setText("Descrição:");
			jLabel2.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
		}
		return jLabel2;
	}
	/**
	 * This method initializes txtSIca
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtSIca() {
		if (txtSIca == null) {
			txtSIca = new javax.swing.JTextField();
		}
		return txtSIca;
	}
	/**
	 * This method initializes jLabel3
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel3() {
		if (jLabel3 == null) {
			jLabel3 = new javax.swing.JLabel();
			jLabel3.setText("ICA:");
			jLabel3.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
		}
		return jLabel3;
	}
	/**
	 * This method initializes txtSDevice
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtSDevice() {
		if (txtSDevice == null) {
			txtSDevice = new javax.swing.JTextField();
		}
		return txtSDevice;
	}
	/**
	 * This method initializes jLabel4
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel4() {
		if (jLabel4 == null) {
			jLabel4 = new javax.swing.JLabel();
			jLabel4.setText("Device:");
			jLabel4.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
		}
		return jLabel4;
	}
	/**
	 * This method initializes jButton1
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getJButton1() {
		if (jButton1 == null) {
			jButton1 = new javax.swing.JButton();
			jButton1.setText("Close Session");
			jButton1.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					ExecuteCloseSession cmd = new ExecuteCloseSession();
					cmd.setDevice(txtSDevice.getText());
					cmd.setInternalCallId(txtSIca.getText());
					cmd.setDescription(txtSDescription.getText());
					try {
						agentAdapter.execute(cmd);
					} catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando.", e1);
					}
				}
			});
		}
		return jButton1;
	}
	/**
	 * This method initializes jButton2
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getJButton2() {
		if (jButton2 == null) {
			jButton2 = new javax.swing.JButton();
			jButton2.setText("Set Value");
			jButton2.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					ExecuteSetSession cmd = new ExecuteSetSession();
					cmd.setDevice(txtSDevice.getText());
					cmd.setInternalCallId(txtSIca.getText());
					cmd.setDescription(txtSDescription.getText());
					try {
						agentAdapter.execute(cmd);
					} catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando.", e1);
					}
				}
			});
		}
		return jButton2;
	}
	/**
	 * @see br.com.voicetechnology.flexapi.tables.views.AgentInfoTableModelListener#getValueOfField(java.lang.Object, br.com.voicetechnology.flexapi.tables.Record, java.lang.String)
	 */
	public Object getValueOfField(Object owner, Record record, String fieldName) {
        if (fieldName.equals("LOCALTIME")) {
            if (record instanceof AgentCall) {
                AgentCall ac = (AgentCall)record;
                //return DateFormat.getDateInstance().format(ac.getTotalTime());
                return String.valueOf(ac.getTotalTime().getTime() / 1000);
            }
    		return "Tempo";
        } else {
            return record.get(fieldName);
        }
	}
}  //  @jve:visual-info  decl-index=0 visual-constraint="10,10"

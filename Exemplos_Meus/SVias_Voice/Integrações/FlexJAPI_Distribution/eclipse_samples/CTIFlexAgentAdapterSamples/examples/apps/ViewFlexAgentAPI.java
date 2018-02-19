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
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JTextField;

import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Filter;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.flexapi.views.table.TablePanel;
import br.com.voicetechnology.util.Protocol;

/**
 * @author Roberto<br>
 * Voice Technology Ind.<br>
 * <br>
 * 
 * Testes gerais da JAPI.
 */
public class ViewFlexAgentAPI extends JApplet implements WindowListener, CollectionListener, FlexStreamListener  {

	private AgentAdapter agentAdapter = null;
	private javax.swing.JPanel jContentPane = null;
	private javax.swing.JPanel jPanel = null;
	private javax.swing.JButton btnConnect = null;
	private javax.swing.JPanel jPanel1 = null;
	private javax.swing.JLabel jLabel = null;
	private javax.swing.JTextArea txtLog = null;
	private javax.swing.JTextField txtDevice = null;
	private javax.swing.JTabbedPane jTabbedPane = null;
	private javax.swing.JTextField txtConnection = null;
	private javax.swing.JLabel jLabel1 = null;
	private javax.swing.JLabel jLabel2 = null;
	private javax.swing.JScrollPane jScrollPane = null;
	private JPanel jPanel2 = null;
	private JPanel jPanel3 = null;
	private JLabel jLabel3 = null;
	private JButton btnOk = null;
	private JTextField txtFilter = null;
	private JLabel jLabel4 = null;
	private JTextField txtField = null;
	/**
	 * Construtor da classe.
	 */
	public ViewFlexAgentAPI() {
		super();
        init();
	}
	/**
	 * This method initializes this
	 */
	public void init() {
        this.setSize(427, 300);
        this.setContentPane(getJContentPane());
        try {
            agentAdapter = new AgentAdapter();
            agentAdapter.getTables().addListener(this);
            agentAdapter.getStream().addListener(this);
        } catch (Exception e) {
            FlexAPI.getLogger().log(Level.SEVERE, "Erro.", e);
        }
	}
	/**
     * Cria jContentPane.
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
			jPanel2.add(getJPanel(), java.awt.BorderLayout.CENTER);
			jPanel2.add(getJPanel3(), java.awt.BorderLayout.EAST);
		}
		return jPanel2;
	}
	/**
	 * This method initializes jPanel3
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel3() {
		if (jPanel3 == null) {
			jLabel4 = new JLabel();
			jLabel3 = new JLabel();
			GridLayout gridLayout2 = new GridLayout();
			jPanel3 = new JPanel();
			jPanel3.setLayout(gridLayout2);
			jPanel3.setPreferredSize(new java.awt.Dimension(150, 0));
			jLabel3.setText("Filtro:");
			gridLayout2.setRows(3);
			gridLayout2.setColumns(2);
			jLabel4.setText("Campo:");
			jPanel3.add(jLabel4, null);
			jPanel3.add(getTxtField(), null);
			jPanel3.add(jLabel3, java.awt.BorderLayout.WEST);
			jPanel3.add(getTxtFilter(), java.awt.BorderLayout.CENTER);
			jPanel3.add(getBtnOk(), java.awt.BorderLayout.EAST);
		}
		return jPanel3;
	}
	/**
	 * This method initializes jButton
	 * @return javax.swing.JButton
	 */
	private JButton getBtnOk() {
		if (btnOk == null) {
			btnOk = new JButton();
			btnOk.setText("Ok");
			btnOk.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    Filter filter = new Filter();
                    filter.set(txtField.getText(), txtFilter.getText());
                    agentAdapter.getTable("Informations").setFilter(filter);
				}
			});
		}
		return btnOk;
	}
	/**
	 * This method initializes jTextField
	 * @return javax.swing.JTextField
	 */
	private JTextField getTxtFilter() {
		if (txtFilter == null) {
			txtFilter = new JTextField();
			txtFilter.setText("[DTI]");
		}
		return txtFilter;
	}
	/**
	 * This method initializes jTextField1
	 * @return javax.swing.JTextField
	 */
	private JTextField getTxtField() {
		if (txtField == null) {
			txtField = new JTextField();
			txtField.setText("PRN");
		}
		return txtField;
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
		ViewFlexAgentAPI vt = new ViewFlexAgentAPI();
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
     * Cria jPanel
	 * @return JPanel
	 */
	private javax.swing.JPanel getJPanel() {
		if (jPanel == null) {
			jPanel = new javax.swing.JPanel();
			jPanel.setLayout(null);
			jPanel.add(getBtnConnect(), null);
			jPanel.add(getTxtDevice(), null);
			jPanel.add(getTxtConnection(), null);
			jPanel.add(getJLabel1(), null);
			jPanel.add(getJLabel2(), null);
			jPanel.setPreferredSize(new java.awt.Dimension(10, 50));
		}
		return jPanel;
	}
	/**
     * Conecta ao servidor.
	 * @param connection
     * Endereço para conexão. IP:PORTA
	 * @param ramal
     * Ramal para conexão.
	 */
	private void connect(String connection, String ramal) {
		try {
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
     * Cria jButton2.
	 * @return JButton
	 */
	private javax.swing.JButton getBtnConnect() {
		if (btnConnect == null) {
			btnConnect = new javax.swing.JButton();
			btnConnect.setBounds(5, 5, 88, 26);
			btnConnect.setText("Conecta");
			btnConnect.setActionCommand("Execute");
			btnConnect.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					connect(txtConnection.getText(), txtDevice.getText());
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
			txtDevice.setBounds(147, 6, 113, 20);
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
		}
		return jTabbedPane;
	}
	/**
	 * This method initializes jLabel1
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel1() {
		if (jLabel1 == null) {
			jLabel1 = new javax.swing.JLabel();
			jLabel1.setBounds(100, 6, 45, 19);
			jLabel1.setText("Device:");
			jLabel1.setHorizontalTextPosition(javax.swing.SwingConstants.TRAILING);
			jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
		}
		return jLabel1;
	}
	/**
	 * This method initializes jLabel2
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel2() {
		if (jLabel2 == null) {
			jLabel2 = new javax.swing.JLabel();
			jLabel2.setBounds(100, 24, 45, 19);
			jLabel2.setText("Conn:");
			jLabel2.setHorizontalTextPosition(javax.swing.SwingConstants.TRAILING);
			jLabel2.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
		}
		return jLabel2;
	}
	/**
	 * This method initializes txtConnection
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtConnection() {
		if (txtConnection == null) {
			txtConnection = new javax.swing.JTextField();
			txtConnection.setBounds(147, 24, 113, 20);
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
		jTabbedPane.removeAll();
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
     * Tabela reestruturada.
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
		jTabbedPane.addTab(table.getName(), new TablePanel(table));
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#deleted(java.lang.Object, java.lang.Object, int)
	 */
	public void deleted(Object owner, Object item, int index) {
		if (item == null) return;
		txtLog.append("Tabela apagada:" + ((Table)item).getName() + "\r\n");
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
}  //  @jve:visual-info  decl-index=0 visual-constraint="10,10"

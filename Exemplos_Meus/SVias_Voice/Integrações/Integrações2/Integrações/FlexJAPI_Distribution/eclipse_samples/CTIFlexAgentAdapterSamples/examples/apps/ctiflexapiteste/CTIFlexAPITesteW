/*
 * Created on 16/08/2005
 *
 */
package examples.apps.ctiflexapiteste;

import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.awt.Toolkit;
import java.io.IOException;
import java.net.URISyntaxException;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTabbedPane;
import javax.swing.JTextArea;
import javax.swing.JTextField;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.flexapi.views.table.TablePanel;
import br.com.voicetechnology.util.Protocol;
/**
 * CTIFlexAPITeste<br>
 * @author Roberto Elvira
 */
public class CTIFlexAPITesteWindow extends JFrame implements FlexStreamListener, CollectionListener {
    private final int STT_Disconnected = 0;
    private final int STT_Connected = 1;
    private AgentAdapter api = null;
    private CTIFACommands frmCommands = null;
	private javax.swing.JPanel jContentPane = null;
	private javax.swing.JMenuBar jJMenuBar = null;
	private javax.swing.JMenu fileMenu = null;
	private javax.swing.JMenu viewMenu = null;
	private javax.swing.JMenu helpMenu = null;
	private javax.swing.JMenuItem exitMenuItem = null;
	private javax.swing.JMenuItem aboutMenuItem = null;
	private javax.swing.JMenuItem commandsMenuItem = null;
	private JPanel jPanel = null;
	private JLabel jLabel = null;
	private JTextField txtURL = null;
	private JButton btnOpenClose = null;
	private JLabel lblState = null;
	private JTabbedPane jTabbedPane = null;
	private JScrollPane jScrollPane = null;
	private JTextArea txtLog = null;
	private JTabbedPane tbpnlTables = null;
	private JLabel jLabel1 = null;
	private JTextField txtDevice = null;
	private JLabel jLabel2 = null;
	private JTextField txtAppNumber = null;
	/**
	 * This method initializes jPanel	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel() {
		if (jPanel == null) {
			GridBagConstraints gridBagConstraints26 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints25 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints24 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints23 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints22 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints21 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints20 = new GridBagConstraints();
			jLabel2 = new JLabel();
			jLabel1 = new JLabel();
			jLabel = new JLabel();
			jPanel = new JPanel();
			jPanel.setLayout(new GridBagLayout());
			jLabel.setText("URL:");
			jLabel.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
			jLabel1.setText("Device:");
			jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
			jLabel2.setText("App Number:");
			jLabel2.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
			gridBagConstraints20.gridx = 0;
			gridBagConstraints20.gridy = 0;
			gridBagConstraints20.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints21.gridx = 1;
			gridBagConstraints21.gridy = 0;
			gridBagConstraints21.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints22.gridx = 2;
			gridBagConstraints22.gridy = 2;
			gridBagConstraints23.gridx = 0;
			gridBagConstraints23.gridy = 1;
			gridBagConstraints23.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints24.gridx = 1;
			gridBagConstraints24.gridy = 1;
			gridBagConstraints24.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints25.gridx = 0;
			gridBagConstraints25.gridy = 2;
			gridBagConstraints25.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints26.gridx = 1;
			gridBagConstraints26.gridy = 2;
			gridBagConstraints26.fill = java.awt.GridBagConstraints.HORIZONTAL;
			jPanel.add(jLabel, gridBagConstraints20);
			jPanel.add(getTxtURL(), gridBagConstraints21);
			jPanel.add(getBtnOpenClose(), gridBagConstraints22);
			jPanel.add(jLabel1, gridBagConstraints23);
			jPanel.add(getTxtDevice(), gridBagConstraints24);
			jPanel.add(jLabel2, gridBagConstraints25);
			jPanel.add(getTxtAppNumber(), gridBagConstraints26);
		}
		return jPanel;
	}
	/**
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtURL() {
		if (txtURL == null) {
			txtURL = new JTextField();
			txtURL.setText("//127.0.0.1:2556");
			txtURL.setPreferredSize(new java.awt.Dimension(150,20));
		}
		return txtURL;
	}
    private void log(String msg) {
        txtLog.append(msg  + "\r\n");
    }
    private void setConnectionState(int state) {
        switch (state) {
            case STT_Disconnected:
                btnOpenClose.setText("Open");
                lblState.setText("Desconectado");
                return;
            case STT_Connected:
                btnOpenClose.setText("Close");
                lblState.setText("Conectado");
                return;
        }
    }
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnOpenClose() {
		if (btnOpenClose == null) {
			btnOpenClose = new JButton();
			btnOpenClose.setText("Open");
			btnOpenClose.setPreferredSize(new java.awt.Dimension(80,18));
			btnOpenClose.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    if (btnOpenClose.getText().equals("Open")) {
                        try {
							openAPI();
						} catch (URISyntaxException e1) {
                            log(e1.getMessage());
						}
                    } else {
                        closeAPI();
                    }
				}
			});
		}
		return btnOpenClose;
	}
	/**
	 * This method initializes jTabbedPane	
	 * 	
	 * @return javax.swing.JTabbedPane	
	 */    
	private JTabbedPane getJTabbedPane() {
		if (jTabbedPane == null) {
			jTabbedPane = new JTabbedPane();
			jTabbedPane.addTab("Log", null, getJScrollPane(), null);
			jTabbedPane.addTab("Tabelas", null, getTbpnlTables(), null);
		}
		return jTabbedPane;
	}
	/**
	 * This method initializes jScrollPane	
	 * 	
	 * @return javax.swing.JScrollPane	
	 */    
	private JScrollPane getJScrollPane() {
		if (jScrollPane == null) {
			jScrollPane = new JScrollPane();
			jScrollPane.setViewportView(getTxtLog());
		}
		return jScrollPane;
	}
	/**
	 * This method initializes jTextArea	
	 * 	
	 * @return javax.swing.JTextArea	
	 */    
	private JTextArea getTxtLog() {
		if (txtLog == null) {
			txtLog = new JTextArea();
		}
		return txtLog;
	}
	/**
	 * This method initializes jTabbedPane1	
	 * 	
	 * @return javax.swing.JTabbedPane	
	 */    
	private JTabbedPane getTbpnlTables() {
		if (tbpnlTables == null) {
			tbpnlTables = new JTabbedPane();
		}
		return tbpnlTables;
	}
	/**
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtDevice() {
		if (txtDevice == null) {
			txtDevice = new JTextField();
			txtDevice.setText("1000");
		}
		return txtDevice;
	}
	/**
	 * This method initializes jTextField1	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtAppNumber() {
		if (txtAppNumber == null) {
			txtAppNumber = new JTextField();
			txtAppNumber.setText("1");
		}
		return txtAppNumber;
	}
       public static void main(String[] args) {
		CTIFlexAPITesteWindow application = new CTIFlexAPITesteWindow();
		application.setVisible(true);
	}

	/**
	 * This is the default constructor
	 */
	public CTIFlexAPITesteWindow() {
		super();
		initialize();
        
        api = new AgentAdapter();
        // marca as características do aplicativo
        api.getApplicationInfo().setVersion("1.0.0");
        // visualiza o estado da conexão com o servidor
        api.getStream().addListener(this);
        api.getTables().addListener(this);
	}
    private void openAPI() throws URISyntaxException {
        api.getApplicationInfo().getDevice().setRamal(txtDevice.getText());
        try {
            api.getApplicationInfo().setNumber(Integer.parseInt(txtAppNumber.getText()));
        } catch (Exception e) {
            txtAppNumber.setText("0");
            api.getApplicationInfo().setNumber(0);
        }
        api.getStream().setURL(txtURL.getText());
        api.getStream().close();
        api.getStream().open();
    }
    private void closeAPI() {
        api.getStream().close();
    }
	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		this.setDefaultCloseOperation(javax.swing.JFrame.EXIT_ON_CLOSE);
		this.setJMenuBar(getJJMenuBar());
		this.setSize(400,300);
		this.setContentPane(getJContentPane());
		this.setTitle("CTI Server Flex API Teste");
	}
	/**
	 * This method initializes jContentPane
	 * 
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJContentPane() {
		if(jContentPane == null) {
			lblState = new JLabel();
			jContentPane = new javax.swing.JPanel();
			jContentPane.setLayout(new java.awt.BorderLayout());
			lblState.setText("State");
			jContentPane.add(getJPanel(), java.awt.BorderLayout.NORTH);
			jContentPane.add(lblState, java.awt.BorderLayout.SOUTH);
			jContentPane.add(getJTabbedPane(), java.awt.BorderLayout.CENTER);
		}
		return jContentPane;
	}
	/**
	 * This method initializes jJMenuBar	
	 * 	
	 * @return javax.swing.JMenuBar	
	 */    
	private javax.swing.JMenuBar getJJMenuBar() {
		if (jJMenuBar == null) {
			jJMenuBar = new javax.swing.JMenuBar();
			jJMenuBar.add(getFileMenu());
			jJMenuBar.add(getViewMenu());
			jJMenuBar.add(getHelpMenu());
		}
		return jJMenuBar;
	}
	/**
	 * This method initializes jMenu	
	 * 	
	 * @return javax.swing.JMenu	
	 */    
	private javax.swing.JMenu getFileMenu() {
		if (fileMenu == null) {
			fileMenu = new javax.swing.JMenu();
			fileMenu.setText("File");
			fileMenu.add(getExitMenuItem());
		}
		return fileMenu;
	}
	/**
	 * This method initializes jMenu	
	 * 	
	 * @return javax.swing.JMenu	
	 */    
	private javax.swing.JMenu getViewMenu() {
		if (viewMenu == null) {
			viewMenu = new javax.swing.JMenu();
			viewMenu.setText("View");
			viewMenu.add(getCommandsMenuItem());
		}
		return viewMenu;
	}
	/**
	 * This method initializes jMenu	
	 * 	
	 * @return javax.swing.JMenu	
	 */    
	private javax.swing.JMenu getHelpMenu() {
		if (helpMenu == null) {
			helpMenu = new javax.swing.JMenu();
			helpMenu.setText("Help");
			helpMenu.add(getAboutMenuItem());
		}
		return helpMenu;
	}
	/**
	 * This method initializes jMenuItem	
	 * 	
	 * @return javax.swing.JMenuItem	
	 */    
	private javax.swing.JMenuItem getExitMenuItem() {
		if (exitMenuItem == null) {
			exitMenuItem = new javax.swing.JMenuItem();
			exitMenuItem.setText("Exit");
			exitMenuItem.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
					System.exit(0);
				}
			});
		}
		return exitMenuItem;
	}
	/**
	 * This method initializes jMenuItem	
	 * 	
	 * @return javax.swing.JMenuItem	
	 */    
	private javax.swing.JMenuItem getAboutMenuItem() {
		if (aboutMenuItem == null) {
			aboutMenuItem = new javax.swing.JMenuItem();
			aboutMenuItem.setText("About");
			aboutMenuItem.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    try {
						Runtime.getRuntime().exec("C:\\Arquivos de programas\\Internet Explorer\\iexplore.exe file:" + getClass().getResource("help.htm").getFile());
					} catch (IOException e1) {
							e1.printStackTrace();
					}
				}
			});
		}
		return aboutMenuItem;
	}
    /**
     * Inicia o formulário de comandos.
     * @return
     * Instância do formulário de comandos.
     */
     private CTIFACommands getFrmCommands() {
        if (frmCommands == null) {
            frmCommands = new CTIFACommands(api);
        }
        return frmCommands;
    }
	/**
	 * This method initializes jMenuItem	
	 * 	
	 * @return javax.swing.JMenuItem	
	 */    
	private javax.swing.JMenuItem getCommandsMenuItem() {
		if (commandsMenuItem == null) {
			commandsMenuItem = new javax.swing.JMenuItem();
			commandsMenuItem.setText("Commands");
			commandsMenuItem.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    getFrmCommands().setVisible(true);
				}
			});
		}
		return commandsMenuItem;
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
        setConnectionState(STT_Disconnected);
        tbpnlTables.removeAll();
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onConnect()
	 */
	public void onConnect() {
        setConnectionState(STT_Connected);
        Toolkit.getDefaultToolkit().beep();
	}
	/**
	 * @see br.com.voicetechnology.flexapi.stream.FlexStreamListener#onReceive(br.com.voicetechnology.util.Protocol)
	 */
	public void onReceive(Protocol protocol) {
        log("Received:" + protocol.toString());
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#added(java.lang.Object, java.lang.Object, int)
	 */
	public void added(Object owner, Object item, int index) {
        if (item == null) return;
        Table table = (Table)item;
        log("Tabela adicionada:" + table.getName());
        log(table.toString());
        tbpnlTables.addTab(table.getName(), new TablePanel(table));
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#deleted(java.lang.Object, java.lang.Object, int)
	 */
	public void deleted(Object owner, Object item, int index) {
        if (item == null) return;
        log("Tabela apagada:" + ((Table)item).getName());
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#updated(java.lang.Object, java.lang.Object, int)
	 */
	public void updated(Object owner, Object item, int index) {
        if (item == null) return;
        log("Tabela alterada:" + ((Table)item).getName());
	}
}

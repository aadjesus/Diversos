/*
 * Created on 16/08/2005
 *
 */
package examples.apps.ctiflexapiteste;

import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;
import java.util.Iterator;

import javax.swing.DefaultComboBoxModel;
import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JDialog;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTabbedPane;
import javax.swing.JTextField;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandConnectionClear;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandDeflect;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandMakePredictiveCall;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetInformation;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetSendTo;
import br.com.voicetechnology.flexapi.executable.Command;

/**
 * CTIFAHelp<br>
 * @author Roberto Elvira
 */
public class CTIFACommands extends JDialog {
    private int devType = -1;
    private AgentAdapter api = null;
	private javax.swing.JPanel jContentPane = null;
	private JTabbedPane jTabbedPane = null;
	private JPanel jPanel = null;
	private JPanel jPanel1 = null;
	private JPanel jPanel2 = null;
	private JButton btnAnswer = null;
	private JButton btnClear = null;
	private JButton btnHold = null;
	private JButton btnRetrieve = null;
	private JButton btnAlternate = null;
	private JButton btnReconnect = null;
	private JButton btnTransfer = null;
	private JPanel jPanel3 = null;
	private JButton btnDial = null;
	private JButton btnConsult = null;
	private JButton btnDeflect = null;
	private JTextField txtNumber = null;
	private JButton btnPredictive = null;
	private JCheckBox cbAutoControl = null;
	private JButton btnLogon = null;
	private JButton btnLogoff = null;
	private JButton btnReady = null;
	private JButton btnNotReady = null;
	private JButton btnWork = null;
	private JPanel jPanel4 = null;
	private JLabel jLabel = null;
	private JLabel jLabel1 = null;
	private JLabel jLabel2 = null;
	private JTextField txtPIN = null;
	private JTextField txtPAS = null;
	private JTextField txtGroup = null;
	private JLabel jLabel3 = null;
	private JTextField txtReason = null;
	private JComboBox jComboBox = null;
	private JButton btnSnapshotDevice = null;
	private JButton btnSetSendTo = null;
	private JPanel jPanel5 = null;
	private JTextField txtInternalCallId = null;
	private JLabel jLabel4 = null;
	private JLabel jLabel5 = null;
	private JTextField txtDeviceView = null;
	private JPanel jPanel6 = null;
	private JButton btnSetInformation = null;
	private JLabel jLabel6 = null;
	private JLabel jLabel7 = null;
	private JTextField txtNameSetInfo = null;
	private JTextField txtValueSetInfo = null;
	/**
	 * This is the default constructor
	 */
	public CTIFACommands(AgentAdapter api) {
		super();
        this.api = api;
		initialize();
	}
	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		this.setTitle("Comandos");
		this.setSize(200,300);
		this.setContentPane(getJContentPane());
	}
	/**
	 * This method initializes jContentPane
	 * 
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJContentPane() {
		if(jContentPane == null) {
			jContentPane = new javax.swing.JPanel();
			jContentPane.setLayout(new java.awt.BorderLayout());
			jContentPane.add(getJTabbedPane(), java.awt.BorderLayout.CENTER);
		}
		return jContentPane;
	}
	/**
	 * This method initializes jTabbedPane	
	 * 	
	 * @return javax.swing.JTabbedPane	
	 */    
	private JTabbedPane getJTabbedPane() {
		if (jTabbedPane == null) {
			jTabbedPane = new JTabbedPane();
			jTabbedPane.addTab("Chamadas", null, getJPanel(), null);
			jTabbedPane.addTab("Agente", null, getJPanel1(), null);
			jTabbedPane.addTab("Extras", null, getJPanel2(), null);
		}
		return jTabbedPane;
	}
	/**
	 * This method initializes jPanel	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel() {
		if (jPanel == null) {
			GridBagConstraints gridBagConstraints16 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints9 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints7 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints6 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints5 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints4 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints3 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints2 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints1 = new GridBagConstraints();
			jPanel = new JPanel();
			jPanel.setLayout(new GridBagLayout());
			gridBagConstraints1.gridx = 0;
			gridBagConstraints1.gridy = 0;
			gridBagConstraints2.gridx = 1;
			gridBagConstraints2.gridy = 0;
			gridBagConstraints3.gridx = 0;
			gridBagConstraints3.gridy = 2;
			gridBagConstraints4.gridx = 1;
			gridBagConstraints4.gridy = 2;
			gridBagConstraints5.gridx = 0;
			gridBagConstraints5.gridy = 4;
			gridBagConstraints6.gridx = 1;
			gridBagConstraints6.gridy = 4;
			gridBagConstraints7.gridx = 0;
			gridBagConstraints7.gridy = 6;
			gridBagConstraints9.gridx = 0;
			gridBagConstraints9.gridy = 7;
			gridBagConstraints9.gridwidth = 2;
			gridBagConstraints16.gridx = 1;
			gridBagConstraints16.gridy = 6;
			gridBagConstraints16.weightx = 1.0;
			gridBagConstraints16.fill = java.awt.GridBagConstraints.HORIZONTAL;
			jPanel.add(getBtnAnswer(), gridBagConstraints1);
			jPanel.add(getBtnClear(), gridBagConstraints2);
			jPanel.add(getBtnHold(), gridBagConstraints3);
			jPanel.add(getBtnRetrieve(), gridBagConstraints4);
			jPanel.add(getBtnAlternate(), gridBagConstraints5);
			jPanel.add(getBtnReconnect(), gridBagConstraints6);
			jPanel.add(getBtnTransfer(), gridBagConstraints7);
			jPanel.add(getJPanel3(), gridBagConstraints9);
			jPanel.add(getJComboBox(), gridBagConstraints16);
		}
		return jPanel;
	}
	/**
	 * This method initializes jPanel1	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel1() {
		if (jPanel1 == null) {
			GridBagConstraints gridBagConstraints61 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints51 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints41 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints31 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints21 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints15 = new GridBagConstraints();
			jPanel1 = new JPanel();
			jPanel1.setLayout(new GridBagLayout());
			gridBagConstraints15.gridx = 0;
			gridBagConstraints15.gridy = 0;
			gridBagConstraints21.gridx = 2;
			gridBagConstraints21.gridy = 0;
			gridBagConstraints31.gridx = 0;
			gridBagConstraints31.gridy = 1;
			gridBagConstraints41.gridx = 2;
			gridBagConstraints41.gridy = 1;
			gridBagConstraints51.gridx = 0;
			gridBagConstraints51.gridy = 2;
			gridBagConstraints61.gridx = 0;
			gridBagConstraints61.gridy = 4;
			gridBagConstraints61.gridwidth = 3;
			gridBagConstraints61.fill = java.awt.GridBagConstraints.HORIZONTAL;
			jPanel1.add(getBtnLogon(), gridBagConstraints15);
			jPanel1.add(getBtnLogoff(), gridBagConstraints21);
			jPanel1.add(getBtnReady(), gridBagConstraints31);
			jPanel1.add(getBtnNotReady(), gridBagConstraints41);
			jPanel1.add(getBtnWork(), gridBagConstraints51);
			jPanel1.add(getJPanel4(), gridBagConstraints61);
		}
		return jPanel1;
	}
	/**
	 * This method initializes jPanel2	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel2() {
		if (jPanel2 == null) {
			GridBagConstraints gridBagConstraints = new GridBagConstraints();
			gridBagConstraints.gridx = 0;
			gridBagConstraints.gridy = 2;
			GridBagConstraints gridBagConstraints23 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints17 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints22 = new GridBagConstraints();
			jPanel2 = new JPanel();
			jPanel2.setLayout(new GridBagLayout());
			gridBagConstraints22.gridx = 0;
			gridBagConstraints22.gridy = 0;
			gridBagConstraints17.gridx = 1;
			gridBagConstraints17.gridy = 0;
			gridBagConstraints23.gridx = 0;
			gridBagConstraints23.gridy = 1;
			jPanel2.add(getBtnSnapshotDevice(), gridBagConstraints22);
			jPanel2.add(getJPanel5(), gridBagConstraints23);
			jPanel2.add(getJPanel6(), gridBagConstraints);
		}
		return jPanel2;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnAnswer() {
		if (btnAnswer == null) {
			btnAnswer = new JButton();
			btnAnswer.setText("Answer");
			btnAnswer.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    executeCommand(api.getCommandAnswer());
				}
			});
		}
		return btnAnswer;
	}
	/**
	 * This method initializes jButton1	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnClear() {
		if (btnClear == null) {
			btnClear = new JButton();
			btnClear.setText("Clear");
			btnClear.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    Command cmd = api.getCommandConnectionClear();
                    if (cmd != null) {
                        executeCommand(cmd);
                    } else {
                        Iterator i = api.getCalls().iterator();
                        while (i.hasNext()) {
                            AgentCall ac = (AgentCall)i.next();
                            if (ac.isState(1)) {
                                CommandConnectionClear cd = new CommandConnectionClear();
                                cd.setToCall(ac);
                                executeCommand(cd);
                                return;
                            }
                        }
                        
                    }
				}
			});
		}
		return btnClear;
	}
	/**
	 * This method initializes jButton2	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnHold() {
		if (btnHold == null) {
			btnHold = new JButton();
			btnHold.setText("Hold");
			btnHold.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    executeCommand(api.getCommandHold());
				}
			});
		}
		return btnHold;
	}
	/**
	 * This method initializes jButton3	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnRetrieve() {
		if (btnRetrieve == null) {
			btnRetrieve = new JButton();
			btnRetrieve.setText("Retrieve");
			btnRetrieve.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    executeCommand(api.getCommandRetrieve());
				}
			});
		}
		return btnRetrieve;
	}
	/**
	 * This method initializes jButton4	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnAlternate() {
		if (btnAlternate == null) {
			btnAlternate = new JButton();
			btnAlternate.setText("Alternate");
		}
		return btnAlternate;
	}
	/**
	 * This method initializes jButton5	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnReconnect() {
		if (btnReconnect == null) {
			btnReconnect = new JButton();
			btnReconnect.setText("Reconnect");
		}
		return btnReconnect;
	}
	/**
	 * This method initializes jButton6	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnTransfer() {
		if (btnTransfer == null) {
			btnTransfer = new JButton();
			btnTransfer.setText("Transfer");
		}
		return btnTransfer;
	}
	/**
	 * This method initializes jPanel3	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel3() {
		if (jPanel3 == null) {
			GridBagConstraints gridBagConstraints14 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints13 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints12 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints11 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints10 = new GridBagConstraints();
			jPanel3 = new JPanel();
			jPanel3.setLayout(new GridBagLayout());
			gridBagConstraints10.gridx = 0;
			gridBagConstraints10.gridy = 1;
			gridBagConstraints11.gridx = 0;
			gridBagConstraints11.gridy = 2;
			gridBagConstraints12.gridx = 1;
			gridBagConstraints12.gridy = 1;
			gridBagConstraints12.weightx = 1.0;
			gridBagConstraints12.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints13.gridx = 1;
			gridBagConstraints13.gridy = 0;
			gridBagConstraints14.gridx = 1;
			gridBagConstraints14.gridy = 2;
			jPanel3.add(getBtnDial(), new GridBagConstraints());
			jPanel3.add(getBtnConsult(), gridBagConstraints10);
			jPanel3.add(getBtnDeflect(), gridBagConstraints11);
			jPanel3.add(getTxtNumber(), gridBagConstraints12);
			jPanel3.add(getBtnPredictive(), gridBagConstraints13);
			jPanel3.add(getCbAutoControl(), gridBagConstraints14);
		}
		return jPanel3;
	}
    private void executeCommand(Command cmd) {
        try {
            if (cmd == null) {
                JOptionPane.showMessageDialog(null, "Impossível realizar o comando");
                return;
            }
            if (devType >= 0) {
                try {
                    cmd.getProtocol().get("DSC");
                } catch (Exception e) {
                    cmd.getProtocol().replace("DSC", devType);
                }
            }
			api.execute(cmd);
		} catch (Exception e) {
            JOptionPane.showMessageDialog(null, e);
		}
    }
	/**
	 * This method initializes jButton7	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnDial() {
		if (btnDial == null) {
			btnDial = new JButton();
			btnDial.setText("Dial");
			btnDial.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    executeCommand(api.getCommandMakeCall(api.getApplicationInfo().getDevice().getRamal(), txtNumber.getText()));
				}
			});
		}
		return btnDial;
	}
	/**
	 * This method initializes jButton8	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnConsult() {
		if (btnConsult == null) {
			btnConsult = new JButton();
			btnConsult.setText("Consult");
			btnConsult.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    executeCommand(api.getCommandConsultation(txtNumber.getText()));
				}
			});
		}
		return btnConsult;
	}
	/**
	 * This method initializes jButton9	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnDeflect() {
		if (btnDeflect == null) {
			btnDeflect = new JButton();
			btnDeflect.setText("Deflect");
			btnDeflect.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    Command cmd = api.getCommandDeflect(txtNumber.getText());
                    if (cmd != null) {
                        executeCommand(cmd);
                    } else {
                        Iterator i = api.getCalls().iterator();
                        while (i.hasNext()) {
                            AgentCall ac = (AgentCall)i.next();
                            if (ac.isState(1)) {
                                CommandDeflect cd = new CommandDeflect();
                                cd.setToCall(ac);
                                executeCommand(cd);
                                return;
                            }
                        }
                        
                    }
				}
			});
		}
		return btnDeflect;
	}
	/**
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtNumber() {
		if (txtNumber == null) {
			txtNumber = new JTextField();
			txtNumber.setPreferredSize(new java.awt.Dimension(100,20));
		}
		return txtNumber;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnPredictive() {
		if (btnPredictive == null) {
			btnPredictive = new JButton();
			btnPredictive.setText("Predictive");
			btnPredictive.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    CommandMakePredictiveCall cmd = (CommandMakePredictiveCall) api.getCommandMakePredictiveCall(api.getApplicationInfo().getDevice().getRamal(), txtNumber.getText()); 
                    cmd.setMode(cbAutoControl.isSelected() ? 1 : 0);
                    executeCommand(cmd);
				}
			});
		}
		return btnPredictive;
	}
	/**
	 * This method initializes jCheckBox	
	 * 	
	 * @return javax.swing.JCheckBox	
	 */    
	private JCheckBox getCbAutoControl() {
		if (cbAutoControl == null) {
			cbAutoControl = new JCheckBox();
			cbAutoControl.setText("AutoControl");
		}
		return cbAutoControl;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnLogon() {
		if (btnLogon == null) {
			btnLogon = new JButton();
			btnLogon.setText("Logon");
			btnLogon.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    executeCommand(api.getCommandAgentLogon(txtPIN.getText(), txtPAS.getText(), txtGroup.getText()));
				}
			});
		}
		return btnLogon;
	}
	/**
	 * This method initializes jButton1	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnLogoff() {
		if (btnLogoff == null) {
			btnLogoff = new JButton();
			btnLogoff.setText("Logoff");
			btnLogoff.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    executeCommand(api.getCommandAgentLogoff(txtPIN.getText(), txtPAS.getText(), txtGroup.getText()));
				}
			});
		}
		return btnLogoff;
	}
	/**
	 * This method initializes jButton2	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnReady() {
		if (btnReady == null) {
			btnReady = new JButton();
			btnReady.setText("Ready");
			btnReady.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    executeCommand(api.getCommandAgentReady());
				}
			});
		}
		return btnReady;
	}
	/**
	 * This method initializes jButton3	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnNotReady() {
		if (btnNotReady == null) {
			btnNotReady = new JButton();
			btnNotReady.setText("NotReady");
			btnNotReady.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    int i;
                    try {
                        i = Integer.parseInt(txtReason.getText());
                    } catch (Exception e1) {
                        i = 0;
                    }
                    executeCommand(api.getCommandAgentNotReady(i));
				}
			});
		}
		return btnNotReady;
	}
	/**
	 * This method initializes jButton4	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnWork() {
		if (btnWork == null) {
			btnWork = new JButton();
			btnWork.setText("Work");
			btnWork.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    int i;
                    try {
                        i = Integer.parseInt(txtReason.getText());
                    } catch (Exception e1) {
                        i = 0;
                    }
                    executeCommand(api.getCommandAgentWorkReady(i));
				}
			});
		}
		return btnWork;
	}
	/**
	 * This method initializes jPanel4	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel4() {
		if (jPanel4 == null) {
			jLabel3 = new JLabel();
			GridBagConstraints gridBagConstraints121 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints131 = new GridBagConstraints();
			jLabel2 = new JLabel();
			jLabel1 = new JLabel();
			jLabel = new JLabel();
			GridBagConstraints gridBagConstraints71 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints8 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints91 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints101 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints111 = new GridBagConstraints();
			jPanel4 = new JPanel();
			jPanel4.setLayout(new GridBagLayout());
			jLabel.setText("PIN:");
			gridBagConstraints71.gridx = 0;
			gridBagConstraints71.gridy = 1;
			jLabel1.setText("PAS:");
			gridBagConstraints8.gridx = 0;
			gridBagConstraints8.gridy = 2;
			jLabel2.setText("Grupo:");
			gridBagConstraints91.gridx = 1;
			gridBagConstraints91.gridy = 0;
			gridBagConstraints91.weightx = 1.0;
			gridBagConstraints91.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints101.gridx = 1;
			gridBagConstraints101.gridy = 1;
			gridBagConstraints101.weightx = 1.0;
			gridBagConstraints101.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints111.gridx = 1;
			gridBagConstraints111.gridy = 2;
			gridBagConstraints111.weightx = 1.0;
			gridBagConstraints111.fill = java.awt.GridBagConstraints.HORIZONTAL;
			jPanel4.setPreferredSize(new java.awt.Dimension(10,80));
			gridBagConstraints121.gridx = 0;
			gridBagConstraints121.gridy = 3;
			jLabel3.setText("Reason:");
			gridBagConstraints131.gridx = 1;
			gridBagConstraints131.gridy = 3;
			gridBagConstraints131.weightx = 1.0;
			gridBagConstraints131.fill = java.awt.GridBagConstraints.HORIZONTAL;
			jPanel4.add(jLabel, new GridBagConstraints());
			jPanel4.add(jLabel1, gridBagConstraints71);
			jPanel4.add(jLabel2, gridBagConstraints8);
			jPanel4.add(jLabel3, gridBagConstraints121);
			jPanel4.add(getTxtPIN(), gridBagConstraints91);
			jPanel4.add(getTxtPAS(), gridBagConstraints101);
			jPanel4.add(getTxtGroup(), gridBagConstraints111);
			jPanel4.add(getTxtReason(), gridBagConstraints131);
		}
		return jPanel4;
	}
	/**
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtPIN() {
		if (txtPIN == null) {
			txtPIN = new JTextField();
		}
		return txtPIN;
	}
	/**
	 * This method initializes jTextField1	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtPAS() {
		if (txtPAS == null) {
			txtPAS = new JTextField();
		}
		return txtPAS;
	}
	/**
	 * This method initializes jTextField2	
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
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtReason() {
		if (txtReason == null) {
			txtReason = new JTextField();
		}
		return txtReason;
	}
	/**
	 * This method initializes jComboBox	
	 * 	
	 * @return javax.swing.JComboBox	
	 */    
	private JComboBox getJComboBox() {
		if (jComboBox == null) {
			jComboBox = new JComboBox();
            jComboBox.setModel(new DefaultComboBoxModel(new String("None,Ramal,Tronco/VDN").split(",")));
            jComboBox.addItemListener(new java.awt.event.ItemListener() {
            	public void itemStateChanged(java.awt.event.ItemEvent e) {
                    if (e.getItem().toString().equals("None")) {
                        devType = -1;
                    } else if (e.getItem().toString().equals("Ramal")) {
                        devType = 0;
                    } else {
                        devType = 4;
                    }
            	}
            });
		}
		return jComboBox;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnSnapshotDevice() {
		if (btnSnapshotDevice == null) {
			btnSnapshotDevice = new JButton();
			btnSnapshotDevice.setText("SnapshotDevice");
			btnSnapshotDevice.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    executeCommand(api.getCommandSnapshotDevice());
				}
			});
		}
		return btnSnapshotDevice;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnSetSendTo() {
		if (btnSetSendTo == null) {
			btnSetSendTo = new JButton();
			btnSetSendTo.setText("SetSendTo");
			btnSetSendTo.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    CommandSetSendTo cmd = new CommandSetSendTo();
                    cmd.setDevice(txtDeviceView.getText());
                    cmd.setDeviceTo(api.getApplicationInfo().getDevice().getRamal());
                    cmd.setInternalCallId(txtInternalCallId.getText());
                    executeCommand(cmd);
				}
			});
		}
		return btnSetSendTo;
	}
	/**
	 * This method initializes jPanel5	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel5() {
		if (jPanel5 == null) {
			jLabel5 = new JLabel();
			jLabel4 = new JLabel();
			GridBagConstraints gridBagConstraints42 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints32 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints52 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints62 = new GridBagConstraints();
			GridBagConstraints gridBagConstraints72 = new GridBagConstraints();
			jPanel5 = new JPanel();
			jPanel5.setLayout(new GridBagLayout());
			jPanel5.setPreferredSize(new java.awt.Dimension(160,72));
			gridBagConstraints32.gridy = 2;
			gridBagConstraints32.gridwidth = 2;
			gridBagConstraints42.weightx = 1.0;
			gridBagConstraints42.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints42.gridx = 0;
			gridBagConstraints42.gridy = 1;
			gridBagConstraints52.gridx = 0;
			gridBagConstraints52.gridy = 0;
			jLabel4.setText(" InternalCallId: ");
			gridBagConstraints62.gridx = 1;
			gridBagConstraints62.gridy = 0;
			jLabel5.setText(" DeviceView: ");
			gridBagConstraints72.gridx = 1;
			gridBagConstraints72.gridy = 1;
			gridBagConstraints72.weightx = 1.0;
			gridBagConstraints72.fill = java.awt.GridBagConstraints.HORIZONTAL;
			jPanel5.add(getBtnSetSendTo(), gridBagConstraints32);
			jPanel5.add(getTxtInternalCallId(), gridBagConstraints42);
			jPanel5.add(jLabel4, gridBagConstraints52);
			jPanel5.add(jLabel5, gridBagConstraints62);
			jPanel5.add(getTxtDeviceView(), gridBagConstraints72);
		}
		return jPanel5;
	}
	/**
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtInternalCallId() {
		if (txtInternalCallId == null) {
			txtInternalCallId = new JTextField();
			txtInternalCallId.setText("ALL");
		}
		return txtInternalCallId;
	}
	/**
	 * This method initializes jTextField1	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtDeviceView() {
		if (txtDeviceView == null) {
			txtDeviceView = new JTextField();
		}
		return txtDeviceView;
	}
	/**
	 * This method initializes jPanel6	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel6() {
		if (jPanel6 == null) {
			GridBagConstraints gridBagConstraints25 = new GridBagConstraints();
			gridBagConstraints25.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints25.gridy = 1;
			gridBagConstraints25.weightx = 1.0;
			gridBagConstraints25.gridx = 1;
			GridBagConstraints gridBagConstraints24 = new GridBagConstraints();
			gridBagConstraints24.fill = java.awt.GridBagConstraints.HORIZONTAL;
			gridBagConstraints24.gridy = 1;
			gridBagConstraints24.weightx = 1.0;
			gridBagConstraints24.gridx = 0;
			GridBagConstraints gridBagConstraints20 = new GridBagConstraints();
			gridBagConstraints20.gridx = 1;
			gridBagConstraints20.gridy = 0;
			jLabel7 = new JLabel();
			jLabel7.setText("Value:");
			GridBagConstraints gridBagConstraints19 = new GridBagConstraints();
			gridBagConstraints19.gridy = 2;
			gridBagConstraints19.gridwidth = 2;
			GridBagConstraints gridBagConstraints18 = new GridBagConstraints();
			gridBagConstraints18.gridx = 0;
			gridBagConstraints18.gridy = 0;
			jLabel6 = new JLabel();
			jLabel6.setText("Name:");
			jPanel6 = new JPanel();
			jPanel6.setPreferredSize(new java.awt.Dimension(160,72));
			jPanel6.setLayout(new GridBagLayout());
			jPanel6.add(getBtnSetInformation(), gridBagConstraints19);
			jPanel6.add(jLabel6, gridBagConstraints18);
			jPanel6.add(jLabel7, gridBagConstraints20);
			jPanel6.add(getTxtNameSetInfo(), gridBagConstraints24);
			jPanel6.add(getTxtValueSetInfo(), gridBagConstraints25);
		}
		return jPanel6;
	}
	/**
	 * This method initializes btnSetInformation	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getBtnSetInformation() {
		if (btnSetInformation == null) {
			btnSetInformation = new JButton();
			btnSetInformation.setText("Set Information");
			btnSetInformation.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    CommandSetInformation cmd = new CommandSetInformation();
                    AgentCall ac = api.getActiveCall();
                    if (ac == null)
                    	ac = api.getHoldCall();
                    if (ac == null) {
                    	ac = api.getOtherCall();
                    }
                    if (ac == null) {
                    	JOptionPane.showMessageDialog(CTIFACommands.this, new String("Não encontrou chamada para adicionar informação."), "Comando inválido", JOptionPane.ERROR_MESSAGE, null);
                    	return;
                    }
                    cmd.setToCall(ac);
                    cmd.setPropertyName(txtNameSetInfo.getText());
                    cmd.setPropertyValue(txtValueSetInfo.getText());
                    executeCommand(cmd);
				}
			});
		}
		return btnSetInformation;
	}
	/**
	 * This method initializes txtNameSetInfo	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getTxtNameSetInfo() {
		if (txtNameSetInfo == null) {
			txtNameSetInfo = new JTextField();
		}
		return txtNameSetInfo;
	}
	/**
	 * This method initializes txtValueSetInfo	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getTxtValueSetInfo() {
		if (txtValueSetInfo == null) {
			txtValueSetInfo = new JTextField();
		}
		return txtValueSetInfo;
	}
     }

/*
 * Created on 13/06/2005
 *
 */
package examples.apps;

import java.awt.Toolkit;
import java.awt.datatransfer.StringSelection;
import java.io.IOException;
import java.net.URISyntaxException;
import java.util.logging.Level;

import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JSplitPane;
import javax.swing.JTable;
import javax.swing.JTextArea;
import javax.swing.table.TableColumn;

import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import javax.swing.JTextField;
/**
 * @author Roberto Elvira
 * Voice Technology Ind.<br>
 *
 * Tela para teste de CallTrekControl.
 */
public class CallTrekSample extends JFrame {
    private AgentAdapter agentAdapter = null;
    private CallTrekControl callTrekControl = null;

	private javax.swing.JPanel jContentPane = null;
	private JSplitPane jSplitPane = null;
	private JScrollPane jScrollPane = null;
	private JSplitPane jSplitPane1 = null;
	private JTable tblPrimary = null;
	private JScrollPane jScrollPane1 = null;
	private JTable tblSecundary = null;
	private JScrollPane jScrollPane2 = null;
	private JTextArea txtLog = null;
	private JPanel jPanel = null;
	private JButton btnAnswer = null;
	private JButton btnClear = null;  //  @jve:decl-index=0:
	private JButton btnHold = null;
	private JButton btnReady = null;
	private JButton btnNReady = null;
	private JButton btnDial = null;
	private JTextField txtDial = null;
	private JButton btnTransfer = null;
	/**
	 * This method initializes jSplitPane	
	 * 	
	 * @return javax.swing.JSplitPane	
	 */    
	private JSplitPane getJSplitPane() {
		if (jSplitPane == null) {
			jSplitPane = new JSplitPane();
			jSplitPane.setLeftComponent(getJScrollPane());
			jSplitPane.setRightComponent(getJSplitPane1());
			jSplitPane.setDividerLocation(200);
		}
		return jSplitPane;
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
	 * This method initializes jSplitPane1	
	 * 	
	 * @return javax.swing.JSplitPane	
	 */    
	private JSplitPane getJSplitPane1() {
		if (jSplitPane1 == null) {
			jSplitPane1 = new JSplitPane();
			jSplitPane1.setOrientation(javax.swing.JSplitPane.VERTICAL_SPLIT);
			jSplitPane1.setTopComponent(getJScrollPane1());
			jSplitPane1.setBottomComponent(getJScrollPane2());
			jSplitPane1.setDividerLocation(100);
		}
		return jSplitPane1;
	}
	/**
	 * This method initializes jTable	
	 * 	
	 * @return javax.swing.JTable	
	 */    
	private JTable getTblPrimary() {
		if (tblPrimary == null) {
			tblPrimary = new JTable();
			tblPrimary.setRowSelectionAllowed(false);
			tblPrimary.addMouseListener(new java.awt.event.MouseAdapter() {
				public void mouseClicked(java.awt.event.MouseEvent e) {
                    tblSecundary.clearSelection();
                    int idx = tblPrimary.getSelectedRow();
                    if (idx < 0) {
                        callTrekControl.setSelectedCall(null);
                        return;
                    }
                    callTrekControl.setSelectedCall((AgentCall)callTrekControl.getPrivateTable().get(idx));
				}
			});
		}
		return tblPrimary;
	}
	/**
	 * This method initializes jScrollPane1	
	 * 	
	 * @return javax.swing.JScrollPane	
	 */    
	private JScrollPane getJScrollPane1() {
		if (jScrollPane1 == null) {
			jScrollPane1 = new JScrollPane();
			jScrollPane1.setViewportView(getTblPrimary());
		}
		return jScrollPane1;
	}
	/**
	 * This method initializes jTable1	
	 * 	
	 * @return javax.swing.JTable	
	 */    
	private JTable getTblSecundary() {
		if (tblSecundary == null) {
			tblSecundary = new JTable();
			tblSecundary.setRowSelectionAllowed(false);
			tblSecundary.addMouseListener(new java.awt.event.MouseAdapter() { 
				public void mouseClicked(java.awt.event.MouseEvent e) {
                    tblPrimary.clearSelection();
                    int idx = tblSecundary.getSelectedRow();
                    if (idx < 0) {
                        callTrekControl.setSelectedCall(null);
                        return;
                    }
                    callTrekControl.setSelectedCall((AgentCall)callTrekControl.getQueueTable().get(idx));
				}
			});
		}
		return tblSecundary;
	}
	/**
	 * This method initializes jScrollPane2	
	 * 	
	 * @return javax.swing.JScrollPane	
	 */    
	private JScrollPane getJScrollPane2() {
		if (jScrollPane2 == null) {
			jScrollPane2 = new JScrollPane();
			jScrollPane2.setViewportView(getTblSecundary());
		}
		return jScrollPane2;
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
	 * This method initializes jPanel	
	 * 	
	 * @return javax.swing.JPanel	
	 */    
	private JPanel getJPanel() {
		if (jPanel == null) {
			jPanel = new JPanel();
			jPanel.add(getBtnAnswer(), null);
			jPanel.add(getBtnClear(), null);
			jPanel.add(getBtnHold(), null);
			jPanel.add(getBtnDial(), null);
			jPanel.add(getTxtDial(), null);
			jPanel.add(getBtnTransfer(), null);
			jPanel.add(getBtnReady(), null);
			jPanel.add(getBtnNReady(), null);
		}
		return jPanel;
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
                    try {
						callTrekControl.cmdAnswer();
					} catch (Exception e1) {
						txtLog.append(e1.getMessage());
					}
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
                    try {
                        callTrekControl.cmdClear();
                    } catch (Exception e1) {
                        txtLog.append(e1.getMessage());
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
                    try {
						callTrekControl.cmdHold();
					} catch (Exception e1) {
                        txtLog.append(e1.getMessage());
					}
				}
			});
		}
		return btnHold;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnReady() {
		if (btnReady == null) {
			btnReady = new JButton();
			btnReady.setText("Ready");
			btnReady.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    try {
                        callTrekControl.cmdReady(0);
                    } catch (Exception e1) {
                        txtLog.append(e1.getMessage());
                    }
				}
			});
		}
		return btnReady;
	}
	/**
	 * This method initializes jButton1	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnNReady() {
		if (btnNReady == null) {
			btnNReady = new JButton();
			btnNReady.setText("NReady");
			btnNReady.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    try {
                        callTrekControl.cmdNotReady(0);
                    } catch (Exception e1) {
                        txtLog.append(e1.getMessage());
                    }
				}
			});
		}
		return btnNReady;
	}
	/**
	 * This is the default constructor
	 */
	public CallTrekSample() {
		super();
		initialize();
	}
	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
        this.agentAdapter = new AgentAdapter();
        this.agentAdapter.getApplicationInfo().setNumber(22154);
        this.agentAdapter.getApplicationInfo().setVersion("1.0.0");
        this.agentAdapter.getApplicationInfo().getDevice().setRamal("2150");
        try {
			this.agentAdapter.getStream().setURL("//192.168.0.10:2556");
		} catch (URISyntaxException e) {
            FlexAPI.getLogger().log(Level.INFO, "Erro no endereco.", e);
		}

        this.callTrekControl = new CallTrekControl(this);
        this.callTrekControl.setAgentAdapter(agentAdapter);
        this.callTrekControl.setHoldDevice("2020");
        this.callTrekControl.setAdnDevices("2003","2020");

        this.callTrekControl.addListener(new CallTrekControlListener() {
			public void onSelectedCallChanged(CallTrekControl arg) {
                AgentCall sl = callTrekControl.getSelectedCall();
                if (sl == null) {
                    txtLog.append("Nenhuma Chamada Selecionada\n");
                } else {
                    txtLog.append(sl.toString() + "\n");
					try {
                        StringSelection ss = new StringSelection(arg.getInformation("DSC"));
                        Toolkit.getDefaultToolkit().getSystemClipboard().setContents(ss, ss);
					} catch (IOException e) {
					}
                }
			}
            }
        );

		this.setDefaultCloseOperation(javax.swing.JFrame.EXIT_ON_CLOSE);
		this.setSize(600, 400);
		this.setContentPane(getJContentPane());
		this.setTitle("Call Trek Sample");
        
        this.tblPrimary.setModel(callTrekControl.getPrivateModel());
        TableColumn tc = this.tblPrimary.getColumn(new String("*"));
        tc.setMaxWidth(30);
        tc.setResizable(false);
        tc = this.tblPrimary.getColumn(new String(""));
        tc.setMaxWidth(50);
        tc.setResizable(false);
        
        this.tblSecundary.setModel(callTrekControl.getQueueModel());
        tc = this.tblSecundary.getColumn(new String("*"));
        tc.setMaxWidth(30);
        tc.setResizable(false);
        tc = this.tblSecundary.getColumn(new String(""));
        tc.setMaxWidth(50);
        tc.setResizable(false);

        this.agentAdapter.getStream().open();
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
			jContentPane.add(getJSplitPane(), java.awt.BorderLayout.CENTER);
			jContentPane.add(getJPanel(), java.awt.BorderLayout.NORTH);
		}
		return jContentPane;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnDial() {
		if (btnDial == null) {
			btnDial = new JButton();
			btnDial.setText("Dial");
			btnDial.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    try {
                        callTrekControl.cmdDial(txtDial.getText());
                    } catch (Exception e1) {
                        txtLog.append(e1.getMessage());
                    }
				}
			});
		}
		return btnDial;
	}
	/**
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */    
	private JTextField getTxtDial() {
		if (txtDial == null) {
			txtDial = new JTextField();
			txtDial.setPreferredSize(new java.awt.Dimension(50,20));
			txtDial.setText("2003");
		}
		return txtDial;
	}
	/**
	 * This method initializes jButton	
	 * 	
	 * @return javax.swing.JButton	
	 */    
	private JButton getBtnTransfer() {
		if (btnTransfer == null) {
			btnTransfer = new JButton();
			btnTransfer.setText("Transfer");
			btnTransfer.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
                    try {
                        callTrekControl.cmdTransfer();
                    } catch (Exception e1) {
                        txtLog.append(e1.getMessage());
                    }
				}
			});
		}
		return btnTransfer;
	}
       public static void main(String[] args) {
        CallTrekSample application = new CallTrekSample();
        application.setVisible(true);
    }
}

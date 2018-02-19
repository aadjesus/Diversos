/*
 * Created on 04/04/2005
 *
 */
package examples.apps.swing;

import java.awt.BorderLayout;
import java.util.Observable;
import java.util.Observer;
import java.util.logging.Level;

import javax.swing.DefaultComboBoxModel;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JPanel;

import br.com.voicetechnology.flexapi.FlexAPI;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.AgentCommandEnabled;
import br.com.voicetechnology.flexapi.agentadapter.CallCommandEnabled;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandAnswer;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandConnectionClear;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandMakeCall;
import br.com.voicetechnology.flexapi.executable.Command;
/**
 * ButtonPane
 * <br>
 * @author Roberto Elvira<br>
 * Voice Technology Ind.<br>
 */
public class ButtonPane extends JPanel implements Observer {
	private static final String ICONS_PATH = "/examples/apps/icons_2";
    /**
     * ReasonCode<br>
     * @author Roberto Elvira
     */
    private class ReasonCode {
        private String name = "";
        private int code;
        /**
         * Construtor da classe
         * @param arg0
         * Nome do motivo.
         * @param arg1
         * Número do código relativo ao motivo.
         */
        public ReasonCode(String arg0, int arg1) {
            name = arg0;
            code = arg1;
        }
        /**
         * Getter
         * @return
         * Inteiro com o código.
         */
        public int getCode() {
            return code;
        }
        /**
         * Getter
         * @return
         * String contendo o nome.
         */
        public String getName() {
            return name;
        }
        /**
         * @see java.lang.Object#toString()
         */
        public String toString() {
            return name;
        }
    }
    private DefaultComboBoxModel reasonCodeModel = null;
	private javax.swing.JButton btnDial = null;
	private javax.swing.JButton btnAnswer = null;
	private javax.swing.JButton btnHangup = null;
	private javax.swing.JButton btnHold = null;
	private AgentAdapter agentAdapter = null;
	private javax.swing.JTextField txtDial = null;
	private javax.swing.JLabel jLabel = null;
	private GetDialNumber getDialNumber = null;
	private JButton btnConsultation = null;
	private JPanel jPanel = null;
	private JPanel jPanel1 = null;
	private JButton btnLogon = null;
	private JButton btnLogoff = null;
	private JButton btnReady = null;
	private JButton btnNotReady = null;
	private JLabel jLabel1 = null;
	private JButton btnTransfer = null;
	private JLabel jLabel2 = null;
	private JComboBox cbReasonCode = null;
	/**
	 * This is the default constructor
	 */
	public ButtonPane() {
		super();
		initialize();
	}
	/**
	 * This method initializes this
	 */
	private void initialize() {
        reasonCodeModel = new DefaultComboBoxModel();
        this.setLayout(new BorderLayout());
		this.setSize(521, 103);
		this.add(getJPanel(), java.awt.BorderLayout.CENTER);
		this.add(getJPanel1(), java.awt.BorderLayout.WEST);
	}
    /**
     * Insere um tipo de motivo de pausa
     * @param name
     * Nome do motivo.
     * @param code
     * Número que representa o motivo.
     */
    public void setReasonCode(String name, int code) {
        reasonCodeModel.addElement(new ReasonCode(name, code));
    }
	/**
     * Método de acesso.
	 * @param arg
     * Marca objeto.
	 */
	public void setOnGetDialNumber(GetDialNumber arg) {
		this.getDialNumber = arg;
	}
	/**
	 * This method initializes btnDial
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getBtnDial() {
		if (btnDial == null) {
			btnDial = new javax.swing.JButton();
			btnDial.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Dial.GIF")));
			btnDial.setPreferredSize(new java.awt.Dimension(40, 40));
			btnDial.setToolTipText("Dial");
			btnDial.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if (getDialNumber != null) {
						String dialNumber = getDialNumber.onGetDialNumber();
						if (dialNumber.trim().length() > 0) {
							txtDial.setText(dialNumber);
						}
					}
					if (txtDial.getText().trim().length() == 0) {
						return;
					}
					CommandMakeCall cmd = new CommandMakeCall();
					cmd.setDevice(agentAdapter.getApplicationInfo().getDevice().getRamal());
					cmd.setDeviceTo(txtDial.getText());
					try {
						agentAdapter.execute(cmd);
					} catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
					}
				}
			});
		}
		return btnDial;
	}
	/**
	 * This method initializes btnAnswer
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getBtnAnswer() {
		if (btnAnswer == null) {
			btnAnswer = new javax.swing.JButton();
			btnAnswer.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Answer.GIF")));
			btnAnswer.setPreferredSize(new java.awt.Dimension(40, 40));
			btnAnswer.setToolTipText("Answer");
			btnAnswer.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    AgentCall ac = agentAdapter.getActiveCall();
                    CommandAnswer cmd = new CommandAnswer();
                    cmd.setDevice(ac.getDevice());
                    cmd.setCallId(ac.getCallId());
                    try {
                        agentAdapter.execute(cmd);
                    } catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
                    }
				}
			});
		}
		return btnAnswer;
	}
		/**
	 * This method initializes btnHangup
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getBtnHangup() {
		if (btnHangup == null) {
			btnHangup = new javax.swing.JButton();
			btnHangup.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Hangup.GIF")));
			btnHangup.setPreferredSize(new java.awt.Dimension(40, 40));
			btnHangup.setToolTipText("Clear");
			btnHangup.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					AgentCall ac = agentAdapter.getActiveCall();
					CommandConnectionClear cmd = new CommandConnectionClear();
					cmd.setDevice(ac.getDevice());
					cmd.setCallId(ac.getCallId());
					try {
						agentAdapter.execute(cmd);
					} catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
					}
				}
			});
		}
		return btnHangup;
	}
	/**
	 * This method initializes btnHold
	 * @return javax.swing.JButton
	 */
	private javax.swing.JButton getBtnHold() {
		if (btnHold == null) {
			btnHold = new javax.swing.JButton();
			btnHold.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Hold.GIF")));
			btnHold.setPreferredSize(new java.awt.Dimension(40, 40));
			btnHold.setToolTipText("Hold");
			btnHold.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    Command cmd = agentAdapter.getCommandHold();
                    if (cmd != null) {
                        try {
							agentAdapter.execute(cmd);
						} catch (Exception e1) {
                            FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
						}
                    }
				}
			});
		}
		return btnHold;
	}
	/**
     * Método de acesso.
	 * @param arg
     * Marca o adapter.
	 */
	public void setAgentAdapter(AgentAdapter arg) {
		this.agentAdapter = arg;
		this.agentAdapter.getCallCommandEnabled().addObserver(this);
        this.agentAdapter.getAgentCommandEnabled().addObserver(this);
		this.update(this.agentAdapter.getCallCommandEnabled(), null);
	}
	/**
	 * @see java.util.Observer#update(java.util.Observable, java.lang.Object)
	 */
	public void update(Observable arg0, Object arg1) {
		if (arg0 instanceof CallCommandEnabled) {
			CallCommandEnabled cce = (CallCommandEnabled)arg0;
			btnDial.setEnabled(cce.isMakeCall());
			txtDial.setEnabled(cce.isMakeCall());
			btnAnswer.setEnabled(cce.isAnswer());
			btnHangup.setEnabled(cce.isClearConnection());
			btnHold.setEnabled(cce.isHold());
            btnConsultation.setEnabled(cce.isConsultation());
            btnTransfer.setEnabled(cce.isTransfer());
		}
        if (arg0 instanceof AgentCommandEnabled) {
            AgentCommandEnabled ace = (AgentCommandEnabled)arg0;
            btnLogon.setEnabled(ace.isLogon());
            btnLogoff.setEnabled(ace.isLogoff());
            btnReady.setEnabled(ace.isReady());
            btnNotReady.setEnabled(ace.isNotReady());
        }
	}
	/**
	 * This method initializes txtDial
	 * @return javax.swing.JTextField
	 */
	private javax.swing.JTextField getTxtDial() {
		if (txtDial == null) {
			txtDial = new javax.swing.JTextField();
			txtDial.setPreferredSize(new java.awt.Dimension(100, 20));
		}
		return txtDial;
	}
	/**
	 * This method initializes jLabel
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel() {
		if (jLabel == null) {
			jLabel = new javax.swing.JLabel();
			jLabel.setText("Discar:");
		}
		return jLabel;
	}
	/**
	 * This method initializes jButton
	 * @return javax.swing.JButton
	 */
	private JButton getBtnConsultation() {
		if (btnConsultation == null) {
			btnConsultation = new JButton();
			btnConsultation.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Consultation.gif")));
			btnConsultation.setPreferredSize(new java.awt.Dimension(40, 40));
			btnConsultation.setToolTipText("Consultation");
			btnConsultation.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    if (getDialNumber != null) {
                        String dialNumber = getDialNumber.onGetDialNumber();
                        if (dialNumber.trim().length() > 0) {
                            txtDial.setText(dialNumber);
                        }
                    }
                    if (txtDial.getText().trim().length() == 0) {
                        return;
                    }
                    Command cmd = agentAdapter.getCommandConsultation(txtDial.getText());
                    if (cmd == null) {
                        return;
                    }
                    try {
                        agentAdapter.execute(cmd);
                    } catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
                    }
				}
			});
		}
		return btnConsultation;
	}
	/**
	 * This method initializes jPanel
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel() {
		if (jPanel == null) {
			jPanel = new JPanel();
            jPanel.add(getJLabel(), null);
            jPanel.add(getTxtDial(), null);
            jPanel.add(getBtnDial(), null);
            jPanel.add(getBtnAnswer(), null);
            jPanel.add(getBtnHangup(), null);
            jPanel.add(getBtnHold(), null);
            jPanel.add(getBtnConsultation(), null);
            jPanel.add(getBtnTransfer(), null);
		}
		return jPanel;
	}
	/**
	 * This method initializes jPanel1
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel1() {
		if (jPanel1 == null) {
			jLabel2 = new JLabel();
			jLabel1 = new JLabel();
			jPanel1 = new JPanel();
            jPanel1.setPreferredSize(new java.awt.Dimension(100, 30));
			jLabel1.setText("State");
			jLabel1.setPreferredSize(new java.awt.Dimension(90, 16));
			jLabel2.setText("Code:");
			jLabel2.setPreferredSize(new java.awt.Dimension(90, 16));
            jPanel1.add(jLabel1, null);
			jPanel1.add(getBtnLogon(), null);
			jPanel1.add(getBtnLogoff(), null);
			jPanel1.add(getBtnReady(), null);
			jPanel1.add(getBtnNotReady(), null);
			jPanel1.add(jLabel2, null);
			jPanel1.add(getCbReasonCode(), null);
		}
		return jPanel1;
	}
	/**
	 * This method initializes jButton
	 * @return javax.swing.JButton
	 */
	private JButton getBtnLogon() {
		if (btnLogon == null) {
			btnLogon = new JButton();
			btnLogon.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Logon.GIF")));
			btnLogon.setPreferredSize(new java.awt.Dimension(20, 20));
			btnLogon.setToolTipText("Logon");
			btnLogon.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    LogonDialog ld = new LogonDialog();
                    if (ld.getLogon()) {
                        Command cmd = agentAdapter.getCommandAgentLogon(ld.getAgentId(), ld.getPassword(), ld.getAcdGroup());
                        try {
    						agentAdapter.execute(cmd);
    					} catch (Exception e1) {
                            FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
    					}
                    }
                }
			});
		}
		return btnLogon;
	}
	/**
	 * This method initializes jButton1
	 * @return javax.swing.JButton
	 */
	private JButton getBtnLogoff() {
		if (btnLogoff == null) {
			btnLogoff = new JButton();
			btnLogoff.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Logoff.GIF")));
			btnLogoff.setPreferredSize(new java.awt.Dimension(20, 20));
			btnLogoff.setToolTipText("Logoff");
			btnLogoff.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    LogonDialog ld = new LogonDialog();
                    if (ld.getLogon()) {
                        Command cmd = agentAdapter.getCommandAgentLogoff(ld.getAgentId(), ld.getPassword(), ld.getAcdGroup());
                        try {
    						agentAdapter.execute(cmd);
    					} catch (Exception e1) {
                            FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
                        }
					}
				}
			});
		}
		return btnLogoff;
	}
	/**
	 * This method initializes jButton2
	 * @return javax.swing.JButton
	 */
	private JButton getBtnReady() {
		if (btnReady == null) {
			btnReady = new JButton();
			btnReady.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Ready.GIF")));
			btnReady.setPreferredSize(new java.awt.Dimension(20, 20));
			btnReady.setToolTipText("Ready");
			btnReady.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    Command cmd = agentAdapter.getCommandAgentReady();
                    try {
						agentAdapter.execute(cmd);
					} catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
					}
				}
			});
		}
		return btnReady;
	}
	/**
	 * This method initializes jButton3
	 * @return javax.swing.JButton
	 */
	private JButton getBtnNotReady() {
		if (btnNotReady == null) {
			btnNotReady = new JButton();
			btnNotReady.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/NotReady.GIF")));
			btnNotReady.setPreferredSize(new java.awt.Dimension(20, 20));
			btnNotReady.setToolTipText("Not Ready");
			btnNotReady.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    ReasonCode rc = (ReasonCode)cbReasonCode.getSelectedItem();
                    int iRc = rc == null ? 0 : rc.getCode();
                    Command cmd = agentAdapter.getCommandAgentNotReady(iRc);
                    try {
						agentAdapter.execute(cmd);
					} catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
					}
				}
			});
		}
		return btnNotReady;
	}
	/**
	 * This method initializes jButton4
	 * @return javax.swing.JButton
	 */
	private JButton getBtnTransfer() {
		if (btnTransfer == null) {
			btnTransfer = new JButton();
			btnTransfer.setIcon(new ImageIcon(getClass().getResource(ICONS_PATH + "/Transfer.gif")));
			btnTransfer.setPreferredSize(new java.awt.Dimension(40, 40));
			btnTransfer.setToolTipText("Transfer");
			btnTransfer.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    try {
                        agentAdapter.execute(agentAdapter.getCommandTransfer());
                    } catch (Exception e1) {
                        FlexAPI.getLogger().log(Level.SEVERE, "Erro no comando", e1);
                    }
				}
			});
		}
		return btnTransfer;
	}
	/**
	 * This method initializes jComboBox
	 * @return javax.swing.JComboBox
	 */
	private JComboBox getCbReasonCode() {
		if (cbReasonCode == null) {
			cbReasonCode = new JComboBox();
            cbReasonCode.setModel(reasonCodeModel);
		}
		return cbReasonCode;
	}
        }  //  @jve:visual-info  decl-index=0 visual-constraint="10,10"

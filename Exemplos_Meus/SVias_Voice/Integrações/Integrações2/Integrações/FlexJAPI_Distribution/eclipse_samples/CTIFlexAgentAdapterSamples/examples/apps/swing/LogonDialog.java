/*
 * Created on 24/05/2005
 *
 */
package examples.apps.swing;

import javax.swing.JDialog;

import javax.swing.JPanel;
import javax.swing.JButton;
import java.awt.GridLayout;
import javax.swing.JLabel;
import javax.swing.JTextField;
/**
 * LogonDialog<br>
 * @author Roberto Elvira
 */
public class LogonDialog extends JDialog {
    private boolean ok = false;
	private javax.swing.JPanel jContentPane = null;
	private JPanel jPanel = null;
	private JButton jButton = null;
	private JButton jButton1 = null;
	private JPanel jPanel1 = null;
	private JLabel jLabel = null;
	private JTextField txtAgentId = null;
	private JLabel jLabel1 = null;
	private JTextField txtPassword = null;
	private JLabel jLabel2 = null;
	private JTextField txtAcdGroup = null;
	/**
	 * This is the default constructor
	 */
	public LogonDialog() {
		super();
		initialize();
	}
	/**
	 * This method initializes this
	 */
	private void initialize() {
		this.setModal(true);
		this.setTitle("Agent Logon");
		this.setSize(180, 140);
		this.setContentPane(getJContentPane());
	}
    /**
     * Getter.
     * @return
     * String contendo o Agent Id.
     */
    public String getAgentId() {
        return getTxtAgentId().getText();
    }
    /**
     * Getter.
     * @return
     * String contendo o password.
     */
    public String getPassword() {
        return getTxtPassword().getText();
    }
    /**
     * Getter.
     * @return
     * String contendo o acd group.
     */
    public String getAcdGroup() {
        return getTxtAcdGroup().getText();
    }
    /**
     * Visualiza o dialogo para preenchimento dos valores
     * de logon.
     * @return
     * Verdadeiro se preenchido e false se não.
     */
    public boolean getLogon() {
        setVisible(true);
        return ok;
    }
	/**
	 * This method initializes jContentPane
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJContentPane() {
		if (jContentPane == null) {
			jContentPane = new javax.swing.JPanel();
			jContentPane.setLayout(new java.awt.BorderLayout());
			jContentPane.add(getJPanel(), java.awt.BorderLayout.SOUTH);
			jContentPane.add(getJPanel1(), java.awt.BorderLayout.CENTER);
		}
		return jContentPane;
	}
	/**
	 * This method initializes jPanel
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel() {
		if (jPanel == null) {
			jPanel = new JPanel();
			jPanel.add(getJButton(), null);
			jPanel.add(getJButton1(), null);
		}
		return jPanel;
	}
	/**
	 * This method initializes jButton
	 * @return javax.swing.JButton
	 */
	private JButton getJButton() {
		if (jButton == null) {
			jButton = new JButton();
			jButton.setText("Ok");
			jButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
				    ok = true;
                    dispose();
				}
			});
		}
		return jButton;
	}
	/**
	 * This method initializes jButton1
	 * @return javax.swing.JButton
	 */
	private JButton getJButton1() {
		if (jButton1 == null) {
			jButton1 = new JButton();
			jButton1.setText("Cancel");
			jButton1.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
                    ok = false;
                    dispose();
                }
			});
		}
		return jButton1;
	}
	/**
	 * This method initializes jPanel1
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel1() {
		if (jPanel1 == null) {
			jLabel2 = new JLabel();
			jLabel1 = new JLabel();
			jLabel = new JLabel();
			GridLayout gridLayout1 = new GridLayout();
			jPanel1 = new JPanel();
			jPanel1.setLayout(gridLayout1);
			gridLayout1.setRows(3);
			gridLayout1.setColumns(2);
			jLabel.setText("Agent Id:");
			jLabel.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
			jLabel1.setText("Password:");
			jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
			jLabel2.setText("ACD Group:");
			jLabel2.setHorizontalAlignment(javax.swing.SwingConstants.RIGHT);
			jPanel1.add(jLabel, null);
			jPanel1.add(getTxtAgentId(), null);
			jPanel1.add(jLabel1, null);
			jPanel1.add(getTxtPassword(), null);
			jPanel1.add(jLabel2, null);
			jPanel1.add(getTxtAcdGroup(), null);
		}
		return jPanel1;
	}
	/**
	 * This method initializes jTextField
	 * @return javax.swing.JTextField
	 */
	private JTextField getTxtAgentId() {
		if (txtAgentId == null) {
			txtAgentId = new JTextField();
		}
		return txtAgentId;
	}
	/**
	 * This method initializes jTextField1
	 * @return javax.swing.JTextField
	 */
	private JTextField getTxtPassword() {
		if (txtPassword == null) {
			txtPassword = new JTextField();
		}
		return txtPassword;
	}
	/**
	 * This method initializes jTextField2
	 * @return javax.swing.JTextField
	 */
	private JTextField getTxtAcdGroup() {
		if (txtAcdGroup == null) {
			txtAcdGroup = new JTextField();
		}
		return txtAcdGroup;
	}
       }

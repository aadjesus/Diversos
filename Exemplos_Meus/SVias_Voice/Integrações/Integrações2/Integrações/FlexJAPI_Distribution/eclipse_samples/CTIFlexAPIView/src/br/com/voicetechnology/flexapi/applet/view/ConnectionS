package br.com.voicetechnology.flexapi.applet.view;

import java.awt.BorderLayout;
import java.awt.Color;

import javax.swing.JApplet;
import javax.swing.JLabel;
import javax.swing.JPanel;

import br.com.voicetechnology.flexapi.applet.FlexAPIAgentApplet;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.util.Protocol;

public class ConnectionStateApplet extends JApplet implements FlexStreamListener {
    private final int STT_Disconnected = 0;
    private final int STT_Connected = 1;
	private JPanel jContentPane = null;
	private JLabel lblState = null;
	/**
	 * This is the default constructor
	 */
	public ConnectionStateApplet() {
		super();
	}
	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	public void init() {
		this.setSize(300, 20);
		this.setContentPane(getJContentPane());
		FlexAPIAgentApplet applet = (FlexAPIAgentApplet)this.getAppletContext().getApplet(getParameter("API"));
		applet.getAPI().getStream().addListener(this);
	}

	/**
	 * This method initializes jContentPane
	 * 
	 * @return javax.swing.JPanel
	 */
	private JPanel getJContentPane() {
		if (jContentPane == null) {
			jContentPane = new JPanel();
			jContentPane.setLayout(new BorderLayout());
			jContentPane.add(getLblState(), java.awt.BorderLayout.CENTER);
		}
		return jContentPane;
	}

	/**
	 * This method initializes lblState	
	 * 	
	 * @return javax.swing.JLabel	
	 */
	private JLabel getLblState() {
		if (lblState == null) {
			lblState = new JLabel();
			lblState.setText("Connection");
		}
		return lblState;
	}
    private void setConnectionState(int state) {
        switch (state) {
            case STT_Disconnected:
            	lblState.setBackground(Color.RED);
                lblState.setText("Desconectado");
                return;
            case STT_Connected:
            	lblState.setBackground(Color.GREEN);
                lblState.setText("Conectado");
                return;
        }
    }
	public void onError() {
        onDisconnect();
	}
	public void onDisconnect() {
        setConnectionState(STT_Disconnected);
	}
	public void onConnect() {
        setConnectionState(STT_Connected);
	}
	public void onReceive(Protocol protocol) {
	}
}

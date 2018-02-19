package br.com.voicetechnology.flexapi.applet.view;

import java.awt.BorderLayout;

import javax.swing.JApplet;
import javax.swing.JPanel;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.applet.FlexAPIAgentApplet;
import br.com.voicetechnology.flexapi.tables.Table;
import br.com.voicetechnology.flexapi.views.table.TablePanel;

public class TableViewApplet extends JApplet {

	private JPanel jContentPane = null;

	/**
	 * This is the default constructor
	 */
	public TableViewApplet() {
		super();
	}

	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	public void init() {
		this.setSize(300, 200);
		this.setContentPane(getJContentPane());
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
			jContentPane.setLayout(new BorderLayout());
			String tableName = getParameter("TableName");
			System.out.println("TableName:" + tableName);
			if (tableName != null) {
				FlexAPIAgentApplet applet = (FlexAPIAgentApplet)this.getAppletContext().getApplet(getParameter("API"));
				AgentAdapter aa = applet.getAPI();
				Table tbl = null;
				if (tableName.equals("Calls")) {
					tbl = aa.getCalls();
				}
				if (tableName.equals("Agents")) {
					tbl = aa.getAgents();
				}
				if (tableName.equals("Informations")) {
					tbl = aa.getInformations();
				}
				if (tbl == null) {
					tbl = aa.getTable(tableName);
				}
				if (tbl != null) {
					jContentPane.add(new TablePanel(tbl), java.awt.BorderLayout.CENTER);
				}
			}
		}
		return jContentPane;
	}

}

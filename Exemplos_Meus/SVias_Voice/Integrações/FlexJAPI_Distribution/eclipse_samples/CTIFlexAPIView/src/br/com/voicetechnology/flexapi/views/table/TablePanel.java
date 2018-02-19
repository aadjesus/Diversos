/*
 * Created on 05/08/2004
 *
 */
package br.com.voicetechnology.flexapi.views.table;

import javax.swing.JPanel;
import javax.swing.event.TableModelListener;

import br.com.voicetechnology.flexapi.tables.Table;


/**
 * TablePanel<br>
 * Classe de conveniência que implementa as funcionalidades
 * de um painel java JPanel para mostrar os dados de uma
 * tabela.
 * @author Roberto Elvira
 */
public class TablePanel extends JPanel {
	private javax.swing.JLabel jLabel = null;
	private javax.swing.JScrollPane jScrollPane = null;
	private javax.swing.JTable jTable = null;
	private Table table = null;
	/**
     * Construtor da classe
	 * @param arg
     * Tabela para ser visualizada
	 */
	public TablePanel(Table arg) {
		super();
		table = arg;
		initialize();
	}
	/**
	 * This method initializes this
	 */
	private void initialize() {
		this.setLayout(new java.awt.BorderLayout());
		this.add(getJLabel(), java.awt.BorderLayout.NORTH);
		this.add(getJScrollPane(), java.awt.BorderLayout.CENTER);
		this.setSize(300, 146);
	}
    /**
     * Libera todos os recursos alocados pelo objeto.
     * Deve ser o último método chamado antes da liberação da instância.
     * Retira os listeners das tabelas para liberação correta do objeto e
     * portanto da memória.
     * Após esta chamada, o objeto não estará mais utilizável.
     */
    public void release() {
        getTableModel().release();
    }
	/**
	 * This method initializes jLabel
	 * @return javax.swing.JLabel
	 */
	private javax.swing.JLabel getJLabel() {
		if (jLabel == null) {
			jLabel = new javax.swing.JLabel();
			jLabel.setText(table.getName());
		}
		return jLabel;
	}
	/**
	 * This method initializes jTable
	 * @return javax.swing.JTable
	 */
	public javax.swing.JTable getJTable() {
		if (jTable == null) {
			jTable = new javax.swing.JTable();
            TableModel cttm = new TableModel(table);
			jTable.setModel(cttm);
		}
		return jTable;
	}
    /**
     * Getter.
     * @return
     * Instância de {@link TableModel}
     */
    public TableModel getTableModel() {
        return (TableModel)getJTable().getModel();
    }
    /**
     * Adiciona um listener para a tabela.
     * @param listener
     * Listener.
     */
    public void addTableListener(TableModelListener listener) {
        getJTable().getModel().addTableModelListener(listener);
    }
    /**
     * Remove listener da tabela.
     * @param listener
     * Instância para remover.
     */
    public void removeTableListener(TableModelListener listener) {
        getJTable().getModel().removeTableModelListener(listener);
    }
	/**
	 * This method initializes jScrollPane
	 * @return javax.swing.JScrollPane
	 */
	private javax.swing.JScrollPane getJScrollPane() {
		if (jScrollPane == null) {
			jScrollPane = new javax.swing.JScrollPane();
			jScrollPane.setViewportView(getJTable());
		}
		return jScrollPane;
	}
}  //  @jve:visual-info  decl-index=0 visual-constraint="10,10"

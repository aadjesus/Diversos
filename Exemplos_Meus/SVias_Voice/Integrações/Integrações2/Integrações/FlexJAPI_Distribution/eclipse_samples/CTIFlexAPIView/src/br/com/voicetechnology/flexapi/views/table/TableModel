/*
 * Created on 28/07/2004
 *
 */
package br.com.voicetechnology.flexapi.views.table;

import javax.swing.table.DefaultTableModel;

import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.tables.Record;
import br.com.voicetechnology.flexapi.tables.Table;
/**
 * TableModel<br>
 * Classe de conveniência para visualização das tabelas
 * do servidor.
 * Implementa a interface DefaultTableModel para ser usada
 * com o componente JTable.
 * @author Roberto Elvira
 */
public class TableModel extends DefaultTableModel  implements CollectionListener {
	private Table table = null;
    private String[] fieldsNames = null;
    /**
     * Construtor da classe
     * @param arg
     * Tabela para ser visualizada pelo modelo.
     */
    public TableModel(Table arg) {
        super(arg, arg.getField());
        table = arg;
        table.addListener(this);
        table.getField().addListener(this);
    }
    /**
     * Libera todos os recursos alocados pelo objeto.
     * Deve ser o último método chamado antes da liberação da instância.
     * Retira os listeners das tabelas para liberação correta do objeto e
     * portanto da memória.
     * Após esta chamada, o objeto não estará mais utilizável.
     */
    public void release() {
        table.getField().removeListener(this);
    }
    /**
     * Setter.
     * Altera os campos que devem estar visíveis.
     * @param arg0
     * Nomes originais dos campos separados por vírgula.<br>
     * Ex.: "DEV,CLR,CST"
     * @param arg1
     * Nomes dos campos para tradução, separados por vírgula.<br>
     * Ex.: "Ramal, Chamador, Estado"
     */
    public void setVisibleFields(String arg0, String arg1) {
        fieldsNames = new String(arg1).split(",");
        this.setColumnIdentifiers(new String(arg0).split(","));
    }
    /**
     * @see javax.swing.table.TableModel#getColumnName(int)
     */
    public String getColumnName(int column) {
        if (fieldsNames != null) {
            return fieldsNames[column];
        }
        return super.getColumnName(column);
    }
    /**
     * @see javax.swing.table.TableModel#getValueAt(int, int)
     */
    public Object getValueAt(int row, int column) {
        try {
            String fieldName = (String)columnIdentifiers.get(column);
            Record rec = (Record)table.get(row);
            return rec.get(fieldName);
        } catch (Exception e) {
            return null;
        }
    }
    /**
	 * @see javax.swing.table.TableModel#isCellEditable(int, int)
	 */
	public boolean isCellEditable(int arg0, int arg1) {
		return false;
	}
    /**
     * Chamado para sinalizar a alteração de dados de uma tabela
     * quando este está fora do controle da mesma.
     * @param arg
     * Tabela com os dados.
     */
	public void onTableAppended(Table arg) {
		if (table == arg) {
			fireTableDataChanged();
        }
	}
	/**
     * Chamado para sinalizar a alteração de dados de uma tabela
     * quando este está fora do controle da mesma.
	 * @param arg
     * Tabela com os dados.
	 */
	public void onTableUpdated(Table arg) {
		if (table == arg) {
			fireTableDataChanged();
		}
	}
    /**
     * Chamado para sinalizar a alteração de dados de uma tabela
     * quando este está fora do controle da mesma.
     * @param arg
     * Tabela com os dados.
     */
	public void onTableDeleted(Table arg) {
		if (table == arg) {
			fireTableDataChanged();
		}
	}
    /**
     * Chamado para sinalizar a alteração de dados de uma tabela
     * quando este está fora do controle da mesma.
     * @param arg
     * Tabela com os dados.
     */
	public void onTableRestructured(Table arg) {
		if (table == arg) {
			fireTableStructureChanged();
		}
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#added(java.lang.Object, java.lang.Object, int)
	 */
	public void added(Object owner, Object item, int index) {
		fireTableStructureChanged();
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#deleted(java.lang.Object, java.lang.Object, int)
	 */
	public void deleted(Object owner, Object item, int index) {
		fireTableDataChanged();
	}
	/**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#updated(java.lang.Object, java.lang.Object, int)
	 */
	public void updated(Object owner, Object item, int index) {
		fireTableDataChanged();
	}
}

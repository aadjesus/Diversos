/*
 * Created on 28/07/2004
 *
 */
package examples.tables.views;

import javax.swing.table.DefaultTableModel;
import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.AgentInformations;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.tables.Table;
/**
 * TableModel<br>
 * Classe de conveniência para visualização das tabelas
 * do servidor.
 * Implementa a interface DefaultTableModel para ser usada
 * com o componente JTable.
 * @author Roberto Elvira
 */
public class AgentInfoTableModel extends DefaultTableModel implements Runnable, CollectionListener {
    private AgentAdapter agentAdapter = null;
	private Table viewTable = null;
    private String[] viewFields = null;
    private String[] infoFields = null;
    private AgentInfoTableModelListener listener = null;
    private Thread thr = null;
    /**
     * Construtor da classe para manipular uma tabela
     * específica. Usado normalmente para tabelas filtradas.
     * @param arg0
     * Tabela para manipular.
     * @param arg1
     * String contendo os campos para visualizar da tabela
     * de chamadas.
     * @param arg2
     * String contendo as propriedades para visualizar da tabela de informações.
     * @param arg3
     * String contendo a tradução dos campos.
     */
    public AgentInfoTableModel(Table arg0, String arg1, String arg2, String arg3) {
        super();
        viewTable = arg0;
        viewTable.addListener(this);
        viewFields = arg1.split(",");
        infoFields = arg2.split(",");
        setColumnIdentifiers(new String(arg3).split(","));
    }
    /**
     * Limpa os recursos utilizados e libera todas as instâncias para
     * este objeto.
     */
    public void release() {
        viewTable.release();
        viewTable.removeListener(this);
    }
    /**
     * @see javax.swing.table.TableModel#getColumnClass(int)
     */
    public Class getColumnClass(int c) {
        return getValueAt(0, c).getClass();
    }
    /**
     * Setter para AgentAdapter.
     * @param arg
     * Instância de AgentAdapter.
     */
    public void setAgentAdapter(AgentAdapter arg) {
        agentAdapter = arg;
    }
    /**
     * Marca se a atualização de campos calculados deve
     * ser automática ou não.
     * Somente marque para "True", caso existam campos
     * do tipo temporizador que deve ficar atualizando
     * automaticamente.
     * @param arg
     * True para atualização automática de campos
     * calculados, false para atualização manual.
     */
    public void setAutomaticUpdate(boolean arg) {
        if (arg) {
            if (thr != null) {
                return;
            }
            thr = new Thread(this, "AgentInfoTableModel");
            thr.setDaemon(true);
            thr.start();
        } else {
            if (thr == null) {
                return;
            }
            thr.interrupt();
        }
    }
    /**
     * Setter.
     * @param arg
     * Instância da classe que implementa o listener.
     */
    public void setListener(AgentInfoTableModelListener arg) {
        listener = arg;
    }
    /**
     * @see javax.swing.table.TableModel#getRowCount()
     */
    public int getRowCount() {
        return viewTable == null ? 0 : viewTable.size();
    }
    /**
     * @see javax.swing.table.TableModel#isCellEditable(int, int)
     */
    public boolean isCellEditable(int arg0, int arg1) {
        return false;
    }
    /**
     * @see javax.swing.table.TableModel#getValueAt(int, int)
     */
    public Object getValueAt(int row, int column) {
        try {
            AgentCall rec;
            if (column >= viewFields.length) {
                if (agentAdapter == null) {
                    return "ERR";
                }
                rec = (AgentCall)viewTable.get(row);
                Table tbl = agentAdapter.getInformations(rec);
                AgentInformations info = new AgentInformations(tbl);
                Object ret;
                ret = info.getValue(infoFields[column - viewFields.length]);
                tbl.release();
                return ret == null ? "-" : ret;
            } else {
                rec = (AgentCall)viewTable.get(row);
                Object ret;
                if (listener != null) {
                    ret = listener.getValueOfField(this, rec, viewFields[column]);
                } else {
                    ret = rec.get(viewFields[column]);
                }
                return ret == null ? "--" : ret;
            }
        } catch (Exception e) {
            return e.getMessage();
        }
    }
    /**
	 * @see br.com.voicetechnology.flexapi.collection.CollectionListener#added(java.lang.Object, java.lang.Object, int)
	 */
	public void added(Object owner, Object item, int index) {
        fireTableDataChanged();
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
	/**
	 * @see java.lang.Runnable#run()
	 */
	public void run() {
        while (true) {
            try {
				Thread.sleep(2000);
                fireTableDataChanged();
			} catch (InterruptedException e) {
                thr = null;
                break;
			}
        }
	}
}

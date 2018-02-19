/*
 * Created on 25/05/2005
 *
 */
package examples.tables.views;

import br.com.voicetechnology.flexapi.tables.Record;

/**
 * AgentInfoTableModelListener<br>
 * @author Roberto Elvira
 */
public interface AgentInfoTableModelListener {
    /**
     * Solicita o valor para ser mostrado baseado no campo e valor original.
     * @param owner
     * Instância do modelo. Normalmente uma instância da
     * classe AgentInfoTableModel.
     * @param record
     * Registro atual que está solicitando o valor do campo.
     * @param fieldName
     * Nome do campo que está solicitando o valor.
     * @return
     * Objeto contendo o novo valor para ser mostrado na tabela.
     */
    Object getValueOfField(Object owner, Record record, String fieldName);
}

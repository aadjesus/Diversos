package laboratory;

import java.net.URISyntaxException;
import java.util.Iterator;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.AgentCall;
import br.com.voicetechnology.flexapi.agentadapter.AgentInformations;
import br.com.voicetechnology.flexapi.agentadapter.AgentRecord;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.tables.Record;
import br.com.voicetechnology.flexapi.tables.Table;

/**
 * Laborat�rio 4.
 * Monitora��o das tabelas de chamadas.
 * Este exemplo demostra como monitorar um ramal para uma poss�vel transfer�ncia
 * de dados.
 * Neste caso, ap�s o recebimento da chamada, ou seja, quando for detectado que
 * o ramal est� tocando (STT_RINGING), os dados presentes nela podem ser
 * acessados.
 * 
 * @author Roberto Elvira
 */

public class Lab4 implements CollectionListener {
	private static final String RAMAL = "2840";
	private static final int APP_NUMBER = 28400;
	private static final String VERSION = "1.0.0";
	private static final String URL = "//192.168.0.10:2556";
	
    private AgentAdapter api = null;
    
	public Lab4() throws URISyntaxException {
        api = new AgentAdapter();
        api.getApplicationInfo().getDevice().setRamal(RAMAL);
        api.getApplicationInfo().setNumber(APP_NUMBER);
        api.getApplicationInfo().setVersion(VERSION);
        api.getStream().setURL(URL);
        
        api.getCalls().addListener(this);

        api.getStream().open();
	}
	public void close() {
		api.getStream().close();
	}
	public static void main(String[] args) throws Exception {
		Lab4 main =	new Lab4();
		System.in.read();
		main.close();
	}
	private void printData(AgentCall call) {
		// Acessa a tabela de informa��es.
		// Nesta tabela, est�o todos os dados relacionados a chamada passada como par�metro.
		// A tabela � organizada com registros com as informa��es da chamada. Ela possui
		// um campo chamado PRN (Property Name) e um chamado PRV (Property Value).
		Table tbl = api.getInformations(call);
		// Utiliza um adapter de conveni�ncia para acesso facilitado aos dados.
		// Este adapter, busca um registro na tabela onde o campo PRN � igual ao passado
		// no par�metro e retorna o valor do campo PRV (property value).
		AgentInformations ai = new AgentInformations(tbl);
		String clr = ai.getValue("CLR");
		System.out.println("Dado recebido: CLR=" + clr);
		// Mostra todos os dados.
		System.out.println("Mostrando todos os dados:");
		Iterator i = tbl.iterator();
		while (i.hasNext()) {
			AgentRecord ar = (AgentRecord)i.next();
			System.out.println(ar.get("PRN") + "=" + ar.get("PRV"));
		}
		// Libera os relacionamentos entre os objetos.
		// Se este relacionamento n�o for desfeito, ocorrer� um crescimento de mem�ria
		// por conta dos objetos relacionados acumulados.
		tbl.release();
	}
	public void added(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Record record = (Record)arg1;
		System.out.println("Added:" + record.toString());
		AgentCall call = (AgentCall)arg1;
		System.out.println(call.getTable().getField().toString());
		System.out.println("Active:" + call.isActive());
		if (call.isState(AgentCall.STT_RINGING)) {
			printData(call);
		}
	}
	public void deleted(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Record record = (Record)arg1;
		System.out.println("Deleted:" + record.toString());
		AgentCall call = (AgentCall)arg1;
		System.out.println("Active:" + call.isActive());
		System.out.println("Chamada desligada.");
	}
	public void updated(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Record record = (Record)arg1;
		System.out.println("Updated:" + record.toString());
		AgentCall call = (AgentCall)arg1;
		System.out.println("Active:" + call.isActive());
	}
}

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
 * Laboratório 4.
 * Monitoração das tabelas de chamadas.
 * Este exemplo demostra como monitorar um ramal para uma possível transferência
 * de dados.
 * Neste caso, após o recebimento da chamada, ou seja, quando for detectado que
 * o ramal está tocando (STT_RINGING), os dados presentes nela podem ser
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
		// Acessa a tabela de informações.
		// Nesta tabela, estão todos os dados relacionados a chamada passada como parâmetro.
		// A tabela é organizada com registros com as informações da chamada. Ela possui
		// um campo chamado PRN (Property Name) e um chamado PRV (Property Value).
		Table tbl = api.getInformations(call);
		// Utiliza um adapter de conveniência para acesso facilitado aos dados.
		// Este adapter, busca um registro na tabela onde o campo PRN é igual ao passado
		// no parâmetro e retorna o valor do campo PRV (property value).
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
		// Se este relacionamento não for desfeito, ocorrerá um crescimento de memória
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

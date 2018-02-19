package laboratory;

import java.net.URISyntaxException;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.tables.Table;

/**
 * Laboratório 3.
 * Neste laboratório, iremos firmar os conceitos vistos sobre as tabelas
 * internas do servidor e como monitorá-las.
 * A abordagem neste exemplo, poderia ser utilizada em aplicativos visualizadores
 * do servidor.
 * Não é usual em aplicativos Front-end.
 * 
 * @author Roberto Elvira
 */

public class Lab3 implements CollectionListener {
	private static final String RAMAL = "2840";
	private static final int APP_NUMBER = 28400;
	private static final String VERSION = "1.0.0";
	private static final String URL = "//192.168.0.10:2556";
	
    private AgentAdapter api = null;
    
	public Lab3() throws URISyntaxException {
        api = new AgentAdapter();
        api.getApplicationInfo().getDevice().setRamal(RAMAL);
        api.getApplicationInfo().setNumber(APP_NUMBER);
        api.getApplicationInfo().setVersion(VERSION);
        api.getStream().setURL(URL);
        
        api.getTables().addListener(this);

        api.getStream().open();
	}
	public void close() {
		api.getStream().close();
	}
	public static void main(String[] args) throws Exception {
		Lab3 main =	new Lab3();
		System.in.read();
		main.close();
	}
	public void added(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Table table = (Table)arg1;
		System.out.println("Added:" + table.getName());
		System.out.println(table.toString());
	}
	public void deleted(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Table table = (Table)arg1;
		System.out.println("Deleted:" + table.getName());
		System.out.println(table.toString());
	}
	public void updated(Object arg0, Object arg1, int arg2) {
		if (arg1 == null) return;
		Table table = (Table)arg1;
		System.out.println("Updated:" + table.getName() + ":" + arg2);
		System.out.println(table.toString());
	}
}

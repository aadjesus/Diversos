package laboratory;

import java.net.URISyntaxException;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;

/**
 * Laboratório 1.
 * Informações mínimas necessárias para conexão com o servidor CTI.
 *  
 * @author Roberto Elvira
 */
public class Lab1 {
	private static final String RAMAL = "2840";
	private static final int APP_NUMBER = 28400;
	private static final String VERSION = "1.0.0";
	private static final String URL = "//192.168.0.10:2556";
	
    private AgentAdapter api = null;
    
	public Lab1() throws URISyntaxException {
        api = new AgentAdapter();
        api.getApplicationInfo().getDevice().setRamal(RAMAL);
        api.getApplicationInfo().setNumber(APP_NUMBER);
        api.getApplicationInfo().setVersion(VERSION);
        api.getStream().setURL(URL);
        api.getStream().open();
	}
	public void close() {
		api.getStream().close();
	}
	
	public static void main(String[] args) throws Exception {
		Lab1 main =	new Lab1();
		System.in.read();
		main.close();
	}
}

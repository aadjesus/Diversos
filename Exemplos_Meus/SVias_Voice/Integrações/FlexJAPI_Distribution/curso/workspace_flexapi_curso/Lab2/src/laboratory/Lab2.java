package laboratory;

import java.net.URISyntaxException;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.util.Protocol;

/**
 * Laboratório 2.
 * Verificação do estado da conexão com o servidor.
 *  
 * @author Roberto Elvira
 */

public class Lab2 implements FlexStreamListener {
	private static final String RAMAL = "2840";
	private static final int APP_NUMBER = 28400;
	private static final String VERSION = "1.0.0";
	private static final String URL = "//192.168.0.10:2556";
	
    private AgentAdapter api = null;
    
	public Lab2() throws URISyntaxException {
        api = new AgentAdapter();
        api.getApplicationInfo().getDevice().setRamal(RAMAL);
        api.getApplicationInfo().setNumber(APP_NUMBER);
        api.getApplicationInfo().setVersion(VERSION);
        api.getStream().setURL(URL);
        api.getStream().setReconnectTime(5);

        api.getStream().addListener(this);

        api.getStream().open();
	}
	public void close() {
		api.getStream().close();
	}
	public static void main(String[] args) throws Exception {
		Lab2 main =	new Lab2();
		System.in.read();
		main.close();
	}

	public void onError() {
		System.out.println("Erro na conexao");
	}
	public void onDisconnect() {
		System.out.println("Desconectado do servidor");
	}
	public void onConnect() {
		System.out.println("Conectado ao servidor");
	}
	public void onReceive(Protocol arg0) {
		System.out.println("Recebeu dados:" + arg0.toString());
	}
}

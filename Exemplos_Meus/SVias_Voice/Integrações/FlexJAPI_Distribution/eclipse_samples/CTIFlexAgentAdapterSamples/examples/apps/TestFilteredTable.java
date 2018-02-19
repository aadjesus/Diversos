package examples.apps;

import java.io.IOException;
import java.net.URISyntaxException;

import br.com.voicetechnology.flexapi.agentadapter.AgentAdapter;
import br.com.voicetechnology.flexapi.agentadapter.executable.CommandSetSendTo;
import br.com.voicetechnology.flexapi.collection.CollectionListener;
import br.com.voicetechnology.flexapi.stream.FlexStreamListener;
import br.com.voicetechnology.flexapi.tables.Filter;
import br.com.voicetechnology.flexapi.tables.FilteredTable;
import br.com.voicetechnology.util.Protocol;

public class TestFilteredTable implements CollectionListener {
    private AgentAdapter api = null;
    private FilteredTable filteredTable = null;
	public TestFilteredTable() throws URISyntaxException {
        api = new AgentAdapter();
        // marca as características do aplicativo
        api.getApplicationInfo().getDevice().setRamal("TESTE");
        api.getApplicationInfo().setNumber(21500);
        api.getApplicationInfo().setVersion("1.0.0");
        
        Filter filter = null;
        filter = new Filter();
        //filter.set("DEV", "2150[1|2]*");
        //filter.set("DEV", "2150");
        filter.set("DEV", "2150|21501|21502");
        
        filteredTable = new FilteredTable();
        filteredTable.setSourceTable(api.getCalls());
        filteredTable.setFilter(filter);
        
        filteredTable.addListener(this);

        api.getStream().addListener(new FlexStreamListener() {
			public void onError() {
			}
			public void onDisconnect() {
			}
			public void onConnect() {
		        try {
			        CommandSetSendTo cmd = new CommandSetSendTo();
			        cmd.setDeviceTo("TESTE");
			        cmd.setInternalCallId("ALL");
			        
			        cmd.setDevice("21501");
					api.execute(cmd);

			        cmd.setDevice("21502");
					api.execute(cmd);
					
			        cmd.setDevice("2150");
					api.execute(cmd);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
			public void onReceive(Protocol arg0) {
			}
        });
       	api.getStream().setURL("//127.0.0.1:2556");
        api.getStream().open();
	}
	public void added(Object arg0, Object arg1, int arg2) {
		System.out.println("Added:" + arg1 + ", " + arg2);
	}
	public void deleted(Object arg0, Object arg1, int arg2) {
		System.out.println("Deleted:" + arg1 + ", " + arg2);
	}
	public void updated(Object arg0, Object arg1, int arg2) {
		System.out.println("Updated:" + arg1 + ", " + arg2);
	}
	
	/**
	 * @param args
	 * @throws URISyntaxException 
	 * @throws IOException 
	 */
	public static void main(String[] args) throws URISyntaxException, IOException {
		new TestFilteredTable();
		System.in.read();
	}
}

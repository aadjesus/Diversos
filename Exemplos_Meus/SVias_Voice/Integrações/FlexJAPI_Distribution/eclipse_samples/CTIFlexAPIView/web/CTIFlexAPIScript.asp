<%
CTISERVERFLEX_ADDRESS = "//10.1.0.115:2556"
%>
<html>
<head>
<META HTTP-EQUIV="Window-target" CONTENT="framePopup">
<script language=javascript>
	var sttObj;
	var logObj;
	var apiObj;
	function InitializeAPI()
	{
		sttObj = document.getElementById("txtState");
		logObj = document.getElementById("txtLog");
		apiObj = document.getElementById("FlexAPIAgentApplet");
		log("Inicializando");
		apiObj.openAPI();
		log("Iniciou");
		document.getElementById("txtVersion").value = apiObj.getAPI().VERSION;
	}
	function FinalizeAPI()
	{
		apiObj.closeAPI();
	}
	function log(msg)
	{
		apiObj.getAPI().getLogger().info(msg);
		logObj.value = logObj.value + msg + "\n";
		if (logObj.value.length > 1000) {
			logObj.value = "";
		}
	}
	function logClear()
	{
		logObj.value = "";
	}
	function setConnectState(state)
	{
		sttObj.value = state;
	}


	function FlexAPIAgentApplet_onConnect()
	{
		log("onConnect");
		setConnectState("Conectado");
		
		apiObj.agentLogon("6999","","");
		apiObj.agentReady();
	}
	function FlexAPIAgentApplet_onDisconnect()
	{
		log("onDisconnect");
		setConnectState("Desconectado");
	}
	function FlexAPIAgentApplet_onError()
	{
		log("onError");
		setConnectState("Desconectado");
	}
	function FlexAPIAgentApplet_onReceive(protocol)
	{
		log("onReceive");
	}


	function callPopupLocal(parameter,callid)
	{
		parent.framePopup.location.replace("popup.asp?cid=" + parameter + "&cal=" + callid);
	}
	function callPopupSimul()
	{
		callPopupLocal(document.getElementById("cid").value,0);
	}
	function verifyPopup(call)
	{
		var state;
		var cid;
		var callid;
		state = call.getState();
		log("Evento:" + state);
		if (state == 3) {
			state = call.get("CAU");
			log("State:" + state);
			if (state == 22) {
				cid = apiObj.getValue("CID");
				callid = call.getCallId();
				log("Evento de ring. Gerando popup:" + cid + ":" + callid);
				callPopupLocal(cid, callid);
			}
		}
	}
	function updateAgentState(agent)
	{
		var state;
		state = agent.getState();
		log("AgentState:" + state);
		switch (state)
		{
			case 0://logon
				log("Logado");
				document.getElementById("txtAgentState").value="Logon";
				break;
			case 1://logoff
				log("Deslogado");
				document.getElementById("txtAgentState").value="Logoff";
				break;
			case 2://notready
				log("NaoDisponivel");
				document.getElementById("txtAgentState").value="NotReady";
				break;
			case 3://ready
				log("Disponivel");
				document.getElementById("txtAgentState").value="Ready";
				break;
			case 4://work
				log("Trabalhando");
				document.getElementById("txtAgentState").value="Work";
				break;
		}
	}
	function FlexAPIAgentApplet_Calls_added(arg1, arg2, arg3)
	{
		log("Calls.added:" + arg2 + "," + arg3);
		verifyPopup(arg2);
	}
	function FlexAPIAgentApplet_Calls_deleted(arg1, arg2, arg3)
	{
		log("Calls.deleted:" + arg2 + "," + arg3);
		callPopupLocal("Desligada",0);
	}
	function FlexAPIAgentApplet_Calls_updated(arg1, arg2, arg3)
	{
		log("Calls.updated:" + arg2 + "," + arg3);
		verifyPopup(arg2);
	}

	function FlexAPIAgentApplet_Agents_added(arg1, arg2, arg3)
	{
		log("Agents.added:" + arg2 + "," + arg3);
		updateAgentState(arg2);
	}
	function FlexAPIAgentApplet_Agents_deleted(arg1, arg2, arg3)
	{
		log("Agents.deleted:" + arg2 + "," + arg3);
		updateAgentState(arg2);
	}
	function FlexAPIAgentApplet_Agents_updated(arg1, arg2, arg3)
	{
		log("Agents.updated:" + arg2 + "," + arg3);
		updateAgentState(arg2);
	}
</script>

</head>

<body onLoad="InitializeAPI()" onUnload="FinalizeAPI()">
Para hospedar os dois frames num mesmo servidor e IP diferente do CTIServerFlex:<br>
- Na máquina cliente que rodará o browser, colocar no arquivo java.policy 
que fica no subdiretorio %javapath%/lib/security, dentro da sessao grant {}<br>
<br>
permission java.net.SocketPermission "IPFLEX:2556", "connect,resolve";<br>
<br>
Com isto, a seguranca do java sera desabilitada, permitindo a conexao IP fora 
do servidor de origem da pagina.
<br>
<applet name="FlexAPIAgentApplet"
	id="FlexAPIAgentApplet"
	archive=flexapi1.5_1.4.0.jar,flexapiagentadapter1.5_1.5.0.jar,sockets5.1.0.jar,flexapiview1.5_1.4.0.jar
	code=br.com.voicetechnology.flexapi.applet.FlexAPIAgentApplet width=400
	height=200 MAYSCRIPT>
	<param name="scriptable" value="true">
	<param name="url" value="<%=CTISERVERFLEX_ADDRESS%>">
	<param name="device" value="<%=request("ramal")%>">
	<param name="number" value="<%=request("ramal")%>">
</applet>

<table width=100% border>
	<tr>
		<td>
			API Version:<input type="text" name="txtVersion" id="txtVersion"></input>
		</td>
	</tr>
	<tr>
		<td>
			Estado do Agente:<input type="text" name="txtAgentState" id="txtAgentState"></input>
		</td>
	</tr>
	<tr>
		<td>
			Simula Popup:
			<input type=text name="cid" id="cid" value="01199995586"></input>
			<input type=button name=btnSimul value="Simula" onClick="callPopupSimul()"></input>
		</td>
	</tr>
	<tr>
		<td>
			<input type=button value="Clear" onClick="logClear()"></input><br>
			<textarea rows="10" cols="50" name="txtLog" id="txtLog"></textarea>
		</td>
	</tr>
	<tr>
		<td>
			Estado da conexao IP:<input type="text" name="txtState" id="txtState"></input>
		</td>
	</tr>
</table>
	
</body>
</html>

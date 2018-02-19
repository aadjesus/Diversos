<%
callid = request("cal")
tel = request("cid")
if mid(tel,5,3)="543" then
	cliente = "Roberto"
else
	cliente = "Desconhecido"
end if
%>
<html>
<head>
<script language="JavaScript" src="voicescripts.js"></script>
</head>
<body>
<table>
<tr>
	<td>
		PIN:<input type=text name="pin" id="pin"></input><br>
		PASS:<input type=text name="pas" id="pas"></input>
	</td>
	<td>
		<input type=button name=btnLogon value="Logon" onClick="logonFunction()"></input>
	</td>
	<td>
		<input type=button name=btnReady value="Ready" onClick="readyFunction()"></input>
	</td>
	<td>
		<input type=button name=btnNotReady value="NotReady" onClick="notReadyFunction()"></input>
	</td>
	<td>
		<input type=button name=btnLogoff value="Logoff" onClick="logoffFunction()"></input>
	</td>
	<td>
	</td>
</tr>
<tr>
	<td>
		Destino:<input type=text name="phone" id="phone"></input>
	</td>
	<td>
		<input type=button name=btnDial value="Disca" onClick="dialFunction()"></input>
	</td>
	<td>
		<input type=button name=btnConsult value="Consulta" onClick="consultFunction()"></input>
	</td>
	<td>
		<input type=button name=btnTransfer value="Transfere" onClick="transferFunction()"></input>
	</td>
	<td>
		<input type=button name=btnReconnect value="Reconecta" onClick="reconnectFunction()"></input>
	</td>
	<td>
		<input type=button name=btnClear value="Desliga" onClick="connectionClearFunction()"></input>
	</td>
</tr>
</table>
Tela com os dados do cliente:<br>
Id:<%=callid%> <br>
Cliente:<%=cliente%> <br>
Telefone:<%=tel%> <br>
</body>
</html>

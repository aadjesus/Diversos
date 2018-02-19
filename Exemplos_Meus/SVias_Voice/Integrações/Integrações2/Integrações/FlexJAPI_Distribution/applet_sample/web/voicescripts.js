	function logonFunction()
	{
		var api;
		var pin;
		var pas;
		var result;
		pin = document.getElementById("pin").value;
		pas = document.getElementById("pas").value;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.agentLogon(pin, pas, "");
		alert(result);
	}
	function logoffFunction()
	{
		var api;
		var result;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.agentLogoff("", "", "");
		alert(result);
	}
	function readyFunction()
	{
		var api;
		var result;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.agentReady();
		alert(result);
	}
	function notReadyFunction()
	{
		var api;
		var result;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.agentNotReady(0);
		alert(result);
	}

	function dialFunction()
	{
		var api;
		var phone;
		var result;
		phone = document.getElementById("phone").value;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.makeCall(phone);
		alert(result);
	}

	function consultFunction()
	{
		var api;
		var phone;
		var result;
		phone = document.getElementById("phone").value;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.consultationCall(phone);
		alert(result);
	}

	function transferFunction()
	{
		var api;
		var result;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.transferCall();
		alert(result);
	}

	function reconnectFunction()
	{
		var api;
		var result;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.reconnectCall();
		alert(result);
	}

	function connectionClearFunction()
	{
		var api;
		var result;
		api = parent.frameCTI.document.getElementById("FlexAPIAgentApplet");
		result = api.clearCall();
		alert(result);
	}

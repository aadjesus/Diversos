<%@page contentType="text/html"
   import="java.util.*,
   javax.management.*,
   br.com.voicetechnology.ctiflexapi.*"
%>
<html>
<body>
<%
	Object []servers = MBeanServerFactory.findMBeanServer(null).toArray();
	MBeanServer mbs = (MBeanServer)servers[0];
	ObjectName obj = new ObjectName("VoiceTechnology.CTIServerFlex:service=japi");
	CTIFlexAPI api = (CTIFlexAPI)mbs.invoke(obj,"getInstance",null, null);
%>
<%=api.viewConnection()%><br><br>
<%=api.viewLog()%><br>

<script>
window.setTimeout('UpdateScreen()',5000);
function UpdateScreen()
{
	location.href='connection_log_show.jsp';
	window.setTimeout('UpdateScreen()',5000);
}
</script>
</body>
</html>

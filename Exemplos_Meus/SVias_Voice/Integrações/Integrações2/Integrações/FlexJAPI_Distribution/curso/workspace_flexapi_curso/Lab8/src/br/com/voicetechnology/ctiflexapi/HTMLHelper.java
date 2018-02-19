package br.com.voicetechnology.ctiflexapi;

import java.util.Collection;

import br.com.voicetechnology.flexapi.collection.CollectionObject;
import br.com.voicetechnology.flexapi.tables.Record;
import br.com.voicetechnology.flexapi.tables.Table;

public class HTMLHelper {
	public static String getTables(Collection tables) {
		Object []tbs = tables.toArray();
		StringBuffer ret = new StringBuffer();
		ret.append("<b>Tables:</b>" + tbs.length + "<br>");
		ret.append("<table width=100% border>");
		ret.append("<tr><th>Table</th><th>Records</th></tr>");
		for (int t=0; t<tbs.length; t++) {
			Table table = (Table)tbs[t];
			ret.append("<tr><td>");
			ret.append(table.getName());
			ret.append("</td><td>");
			ret.append(table.size());
			ret.append("</td></tr>");
		}
		ret.append("</table>");
		return ret.toString();
	}
	public static String getTable(Table table) {
		StringBuffer sbHeader = new StringBuffer();
		CollectionObject fields = table.getField();
		sbHeader.append("<tr>");
		for (int c=0; c<fields.size(); c++) {
			sbHeader.append("<th>" + fields.get(c) + "</th>");
		}
		sbHeader.append("</tr>");
		
		StringBuffer ret = new StringBuffer();
		ret.append("<b>Table:</b>" + table.getName() + "<br>");
		ret.append("<b>Description:</b>" + table.getDescription() + "<br>");
		ret.append("<b>Records:</b>" + table.size() + "<br>");
		ret.append("<table width=100% border>");
		ret.append(sbHeader);
		for (int r=0; r<table.size(); r++) {
			Record rec = (Record)table.get(r);
			ret.append("<tr>");
			for (int c=0; c<fields.size(); c++) {
				ret.append("<td>" + rec.get(c) + "</td>");
			}
			ret.append("</tr>");
		}
		ret.append("</table>");
		return ret.toString();
	}
}

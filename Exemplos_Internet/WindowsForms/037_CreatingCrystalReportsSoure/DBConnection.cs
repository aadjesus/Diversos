
/*
 *		Author : Ishara Gunarathna 
 *		Date   : 10 January 2006
 *		e-mail : isharasjc@yahoo.com
 *		Class  : DBConnection
 */


using System;
using System.Data;
using System.Data.OleDb;

namespace CreatingCrystalReports
{
	/// <summary>
	/// Summary description for DBConnection.
	/// </summary>
	public class DBConnection
	{
		public DBConnection()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		
		// to create a connection to the DataBase
		public OleDbConnection ConnectionToDataBase()
		{
			try
			{
				string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\myDB.mdb;User ID=Admin;Password=";
				OleDbConnection conn = new OleDbConnection(connString);
				
				return conn;
			}
			catch(System.Data.OleDb.OleDbException ex){return null;}
		}



		// to get data from the DB
		public OleDbDataAdapter getDataFromDB()
		{
			try
			{
				OleDbConnection conn = ConnectionToDataBase();
				conn.Open();
				string query = "SELECT * FROM studentInfo";
				OleDbDataAdapter oleDA = new OleDbDataAdapter(query,conn);

				return oleDA;
			}
			catch(System.Data.OleDb.OleDbException ex){return null;}
			catch(System.NullReferenceException EN){return null;}
			catch(System.InvalidOperationException IO){return null;}
		}
	}
}

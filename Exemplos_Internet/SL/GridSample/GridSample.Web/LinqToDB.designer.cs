﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GridSample.Web
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SampleData")]
	public partial class LinqToDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public LinqToDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SampleDataConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public LinqToDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqToDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqToDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqToDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.SelectClient")]
		public ISingleResult<SelectClientResult> SelectClient()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((ISingleResult<SelectClientResult>)(result.ReturnValue));
		}
	}
	
	public partial class SelectClientResult
	{
		
		private short _ClientID;
		
		private string _ClientName;
		
		private System.DateTime _DateofIssue;
		
		private string _issueComment;
		
		private string _CategoryName;
		
		private string _SubClientName;
		
		private System.DateTime _UpdatedDate;
		
		private int _UpdatedBy;
		
		public SelectClientResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClientID", DbType="SmallInt NOT NULL")]
		public short ClientID
		{
			get
			{
				return this._ClientID;
			}
			set
			{
				if ((this._ClientID != value))
				{
					this._ClientID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ClientName", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string ClientName
		{
			get
			{
				return this._ClientName;
			}
			set
			{
				if ((this._ClientName != value))
				{
					this._ClientName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateofIssue", DbType="Date NOT NULL")]
		public System.DateTime DateofIssue
		{
			get
			{
				return this._DateofIssue;
			}
			set
			{
				if ((this._DateofIssue != value))
				{
					this._DateofIssue = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_issueComment", DbType="VarChar(MAX)")]
		public string issueComment
		{
			get
			{
				return this._issueComment;
			}
			set
			{
				if ((this._issueComment != value))
				{
					this._issueComment = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryName", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string CategoryName
		{
			get
			{
				return this._CategoryName;
			}
			set
			{
				if ((this._CategoryName != value))
				{
					this._CategoryName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SubClientName", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string SubClientName
		{
			get
			{
				return this._SubClientName;
			}
			set
			{
				if ((this._SubClientName != value))
				{
					this._SubClientName = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdatedDate", DbType="SmallDateTime NOT NULL")]
		public System.DateTime UpdatedDate
		{
			get
			{
				return this._UpdatedDate;
			}
			set
			{
				if ((this._UpdatedDate != value))
				{
					this._UpdatedDate = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdatedBy", DbType="Int NOT NULL")]
		public int UpdatedBy
		{
			get
			{
				return this._UpdatedBy;
			}
			set
			{
				if ((this._UpdatedBy != value))
				{
					this._UpdatedBy = value;
				}
			}
		}
	}
}
#pragma warning restore 1591

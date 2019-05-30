namespace MissingLineIDForm
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MerretTranslator")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertMDADiagnostic(MDADiagnostic instance);
    partial void UpdateMDADiagnostic(MDADiagnostic instance);
    partial void DeleteMDADiagnostic(MDADiagnostic instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::MissingLineIDTool.Properties.Settings.Default.MerretTranslatorConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<MDADiagnostic> MDADiagnostics
		{
			get
			{
				return this.GetTable<MDADiagnostic>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name= "ReferenceStg.Table")]
	public partial class MDADiagnostic : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private decimal _MDANumber;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnMDANumberChanging(decimal value);
    partial void OnMDANumberChanged();
    #endregion
		
		public MDADiagnostic()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MDANumber", DbType="Decimal(18,0) NOT NULL", IsPrimaryKey=true)]
		public decimal MDANumber
		{
			get
			{
				return this._MDANumber;
			}
			set
			{
				if ((this._MDANumber != value))
				{
					this.OnMDANumberChanging(value);
					this.SendPropertyChanging();
					this._MDANumber = value;
					this.SendPropertyChanged("MDANumber");
					this.OnMDANumberChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591

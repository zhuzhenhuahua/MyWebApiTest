using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLite.Data;
using Zzh.Lib.Util;
using NLite.Reflection;
using System.Data.Common;
using Zzh.Model.DB;

namespace Zzh.Lib.DB.Context
{
   public class NLiteDBContex:DbContext
    {
        private static DbConfiguration _dbConfiguartion=null;
        static NLiteDBContex()
        {
            L10NConfig.Init();
            _dbConfiguartion = getConfiguration(L10NConfig.StrConn, L10NConfig.SqlDBType);
        }
        private static DbConfiguration getConfiguration(string conn, SQLDBType dataType)
        {
            if (dataType == SQLDBType.Oracle)
            {
                DbConnection con = null;
                try
                {
                    var factory = System.Data.Common.DbProviderFactories.GetFactory("Oracle.ManagedDataAccess.Client");

                    con = factory.CreateConnection();
                    con.ConnectionString = conn;
                    con.Open();
                }
                catch (Exception ex)
                {
                   
                }
                finally
                {
                    if (con != null)
                    {
                        con.Dispose();
                    }
                    { }
                }
                return DbConfiguration
                   .Configure(conn, "Oracle.ManagedDataAccess.Client")
                   .SetSqlLogger(() => SqlLog.Debug)
                   .AddFromAssemblyOf<NLiteDBContex>(t => t.HasAttribute<TableAttribute>(false))
                   ;
            }
            else if (dataType == SQLDBType.SqlServer)
            {
                return DbConfiguration
                   .Configure(conn, "System.Data.SqlClient")
                   .SetSqlLogger(() => SqlLog.Debug)
                   .AddFromAssemblyOf<NLiteDBContex>(t => t.HasAttribute<TableAttribute>(false));
            }
            return null;

        }
        public NLiteDBContex() : base(_dbConfiguartion)
        {

        }
        public IDbSet<Sys_Areas> Sys_Areas { get; set; }
    }
    public class L10NConfig
    {
        public static string xmlPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AppL10NXmlDir"];
            }
        }
        public static string ItemID
        {
            get
            {
                return XMLHelper.GetXmlNodeValue(xmlPath + "/config.xml", "//L10NConfig//Locale", "ID");
            }
        }
        public static string StrConn { get; private set; }
        public static SQLDBType SqlDBType { get; set; }
        public static void Init()
        {
            string settingXml = xmlPath + ItemID + "//Setting.xml";
            string providerName = XMLHelper.GetXmlNodeValue(settingXml, "//SettingsConfig//SysMangerDBSetting", "ProviderName");
            StrConn = XMLHelper.GetXmlNodeValue(settingXml, "//SettingsConfig//SysMangerDBSetting", "ConnectionString");
            if (providerName.Equals("System.Data.SqlClient"))
            {
                SqlDBType = SQLDBType.SqlServer;
            }
            else if (providerName.Equals("Oracle.ManagedDataAccess.Client"))
            {
                SqlDBType = SQLDBType.Oracle;
            }
        }
    }
    public enum SQLDBType
    {
        Oracle = 0,
        SqlServer = 1
    }
}

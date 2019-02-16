using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB;

namespace Zzh.Lib.DB.Context
{
   public class MDBContext:DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

        }
        public MDBContext() :base("DefaultConnection")
        {
            Init();
        }
        public MDBContext(string conn) : base(conn)
        {
            Init();
        }
        private void Init()
        {
            try
            {
                //EF连接Access数据库时，数据库不需要先创建表，EF会根据Model实体自动创建表。
                this.Configuration.LazyLoadingEnabled = true;
                this.Database.Connection.ConnectionString = @"Provider=Microsoft.ACE.OleDb.12.0;Data Source=D:\DataStandard.mdb";
                this.Database.Initialize(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DbSet<dm_drillBrand> dm_drillBrands { get; set; }
    }
}

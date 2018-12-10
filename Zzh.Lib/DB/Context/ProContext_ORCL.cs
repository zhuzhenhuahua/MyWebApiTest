using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Model.DB;
using Zzh.Model.DB.Configuration;

namespace Zzh.Lib.DB.Context
{
    public class ProContext_ORCL : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
            modelBuilder.HasDefaultSchema("HBSCDT");
        }
        public ProContext_ORCL() : base("name=OraString")
        {
            Init();
        }
        public ProContext_ORCL(string conn) : base(conn)
        {
            Init();
        }
        private void Init()
        {
            try
            {
                this.Configuration.LazyLoadingEnabled = true;
                this.Database.Initialize(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DbSet<ZHSCRB1> ZHSCRB1S { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzh.Lib.DB.Context;

namespace Zzh.Lib.DB.Context
{
    public class RepositoryVisiter : IDisposable
    {
        public readonly ProContext DB;

        //public ProContext Context => context;
        static object _syncObject = new object();

        public RepositoryVisiter()
        {
            DB = new ProContext();
        }
        public RepositoryVisiter(string conn)
        {
            DB = new ProContext(conn);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.DB.Dispose();
                }
                disposedValue = true;
            }
        }

        // ~ProRepository() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region 公共虚方法

        #endregion
    }
}

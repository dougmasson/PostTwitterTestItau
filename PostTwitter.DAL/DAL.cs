using PostTwitter.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PostTwitter.DataAcess
{
    /// <summary>
    /// Data Acess Layer
    /// </summary>
    public class DAL : IDisposable
    {
        /// <summary>
        /// Busca as #Tag para pesquisar
        /// </summary>
        /// <returns></returns>
        public List<HashTag> BuscarHashTags()
        {
            List<HashTag> hashTags = null;

            try
            {
                using (var context = new PostTwitterDbContext())
                {
                    hashTags = context.HashTags.Where(x => x.idStatus.Equals((int)ENUM.STATUS.ATIVO)).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return hashTags;
        }

        /// <summary>
        /// Criar registro de Execução
        /// </summary>
        /// <returns></returns>
        public Execucao InsertExecucao()
        {
            Execucao execucao = new Execucao()
            {
                Dataexecucao = DateTime.Now,
                Usuario = "system",
                idStatus = (int)ENUM.STATUS.EM_PROCESSAMENTO
            };

            try
            {
                using (var context = new PostTwitterDbContext())
                {
                    context.Add(execucao);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return execucao;
        }

        /// <summary>
        ///  Armazenar as mensagens na base de dados
        /// </summary>
        /// <param name="execucao"></param>
        /// <param name="lstTwitters"></param>
        public void InsertTwitters(Execucao execucao, List<Twitters> lstTwitters)
        {
            using (var context = new PostTwitterDbContext())
            {
                if (context.Database.CanConnect() == false)
                    return;

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (lstTwitters == null || lstTwitters.Count == 0)
                            throw new Exception("Lista Vazia");

                        foreach (var item in lstTwitters)
                        {
                            context.Add(item);
                        }

                        execucao.idStatus = (int)ENUM.STATUS.CONCLUIDO;
                        context.Update(execucao);

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        execucao.idStatus = (int)ENUM.STATUS.ERRO;
                        context.Update(execucao);
                        context.SaveChanges();

                        throw ex;
                    }
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        ~DAL()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

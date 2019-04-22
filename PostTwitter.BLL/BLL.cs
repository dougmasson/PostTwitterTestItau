using Microsoft.Extensions.Configuration;
using PostTwitter.DataAcess;
using PostTwitter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using Tweetinvi;
using Tweetinvi.Models;

namespace PostTwitter.BusinnesLayer
{
    /// <summary>
    /// Businnes Logic Layer
    /// </summary>
    public class BLL : BLLBasic, IDisposable
    {
        private CredentialsTwitter credentialsTwitter;

        public BLL()
        {
            CarregarParametros();
        }

        /// <summary>
        /// Carregar parametros do appsettings.json
        /// </summary>
        private void CarregarParametros()
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            credentialsTwitter = new CredentialsTwitter();
            configuration.GetSection("CredentialsTwitter").Bind(credentialsTwitter);
        }

        /// <summary>
        /// Iniciar Camada
        /// </summary>
        public void Init()
        {
            Console.WriteLine("Iniciar");

            try
            {
                Auth.SetUserCredentials(credentialsTwitter.consumerKey, credentialsTwitter.consumerSecret, credentialsTwitter.accessToken, credentialsTwitter.accessTokenSecret);
                var user = User.GetAuthenticatedUser();

                if (user != null)
                    Console.WriteLine("Usuario autenticado com sucesso");
                else
                    throw new Exception("Erro ao autenticar usuário");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar as Postagens do Twitter para as #tags parametrizadas na base de dados e salva-las
        /// </summary>
        /// <returns></returns>
        public Execucao ListarPostagensTwitter()
        {
            Console.WriteLine("Carregar Postagens");

            Execucao execucao = null;

            try
            {
                using (DAL dal = new DAL())
                {
                    List<HashTag> hashTags = dal.BuscarHashTags();
                    execucao = dal.InsertExecucao();

                    List<Twitters> postagensTwitter = PostagensTwitter(execucao, hashTags);

                    Console.WriteLine("Inserir na base de dados");
                    dal.InsertTwitters(execucao, postagensTwitter);

                    Console.WriteLine("Carregamento finalizado!!!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return execucao;
        }

        /// <summary>
        /// Carregar as Postagens do Twitter
        /// </summary>
        /// <param name="execucao"></param>
        /// <param name="hashTags"></param>
        /// <returns></returns>
        private List<Twitters> PostagensTwitter(Execucao execucao, List<HashTag> hashTags)
        {
            List<Twitters> lstTwitters = new List<Twitters>();

            RateLimit.RateLimitTrackerMode = RateLimitTrackerMode.TrackAndAwait;

            foreach (var hashTag in hashTags)
            {
                var searchParameter = Search.CreateTweetSearchParameter(hashTag.Descricao);
                searchParameter.SearchType = SearchResultType.Recent;
                searchParameter.MaximumNumberOfResults = 100;
                searchParameter.Since = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                IEnumerable<ITweet> tweets = Search.SearchTweets(searchParameter);

                foreach (var item in tweets)
                {
                    lstTwitters.Add(new Twitters()
                    {
                        idHashTag = hashTag.Id,
                        texto = item.FullText,
                        datatwitte = item.CreatedAt,
                        idioma = item.Language.ToString(),
                        usuario = item.CreatedBy.Name,
                        qtdseguidores = item.CreatedBy.FollowersCount,
                        idExecucao = execucao.Id,
                    });
                }
            }

            return lstTwitters;
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

        ~BLL()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
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

using System;
using Tweetinvi;
using Tweetinvi.Models;

namespace PostTwitter.BusinnesLayer
{
    public class BLL : IDisposable
    {
        public void Init()
        {
            Auth.SetUserCredentials(MyCredentials.CONSUMER_API_KEY, MyCredentials.CONSUMER_API_SECRET_KEY, MyCredentials.ACCESS_TOKEN, MyCredentials.ACCESS_TOKEN_SECRET);
            var user = User.GetAuthenticatedUser();

            Console.Write(user);
        }

        public void GetHastTag(string query)
        {
            RateLimit.RateLimitTrackerMode = RateLimitTrackerMode.TrackAndAwait;

            var searchParameter = Search.CreateTweetSearchParameter(query);
            searchParameter.SearchType = SearchResultType.Recent;
            searchParameter.MaximumNumberOfResults = 100; 
            searchParameter.Since = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var tweets = Search.SearchTweets(searchParameter);

            foreach (var item in tweets)
            {
                
            }

            //var tweets2 = SearchAsync.SearchTweets(searchParameter);

            //foreach (var item in tweets2.Result)
            //{

            //}
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

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BLL() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

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

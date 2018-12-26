using System;
using System.Activities;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;

namespace ActivityLibrary1
{
    public class GetFeed : CodeActivity
    {
        public InArgument<string> FeedUrl { get; set; }
        public OutArgument<SyndicationFeed> FeedResult { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var feed = SyndicationFeed.Load(
                XmlReader.Create(FeedUrl.Get(context)));

            FeedResult.Set(context, feed);
        }
    }

    public class GetFeed2 : CodeActivity<SyndicationFeed>
    {
        public InArgument<string> FeedUrl { get; set; }
        //public OutArgument<SyndicationFeed> FeedResult { get; set; }

        protected override SyndicationFeed Execute(CodeActivityContext context)
        {
            var feed = SyndicationFeed.Load(
                XmlReader.Create(FeedUrl.Get(context)));

            //FeedResult.Set(context, feed);
            return feed;
        }
    }

    public class GetFeedAsync : AsyncCodeActivity<SyndicationFeed>
    {
        public InArgument<string> FeedUrl { get; set; }

        protected override IAsyncResult BeginExecute(AsyncCodeActivityContext context, 
            AsyncCallback callback, object state)
        {
            HttpWebRequest request = HttpWebRequest.Create(FeedUrl.Get(context)) as HttpWebRequest;
            context.UserState = request; //storing custom state for later use

            return request.BeginGetResponse(callback, state); //using the parameters as-is, so that runtime can handle them
        }

        protected override SyndicationFeed EndExecute(AsyncCodeActivityContext context, IAsyncResult result)
        {
            HttpWebRequest request = context.UserState as HttpWebRequest;
            var response = request.EndGetResponse(result) as HttpWebResponse;

            var feed = SyndicationFeed.Load(XmlReader.Create(response.GetResponseStream()));
            return feed;
        }
    }
}

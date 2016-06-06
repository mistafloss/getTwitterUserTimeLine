using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using System.Configuration;
using Newtonsoft.Json;

namespace TwitterTimeLine.Controllers
{
    public class HomeController : Controller
    {


        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string twitterUsername)
        {
            try
            {
                //Retrieve consumer key and consumer secret
                string twitterConsumerKey = ConfigurationManager.AppSettings["TwitterConsumerKey"].ToString();
                string twitterConsumerSecret = ConfigurationManager.AppSettings["TwitterConsumerSecret"].ToString();
                var twitterService = new TwitterService(twitterConsumerKey, twitterConsumerSecret);

                //Authenticate twitter service using Access token and Access token secret
                var twitterAccessToken = ConfigurationManager.AppSettings["TwitterAccessToken"].ToString();
                var twitterAccessTokenSecret = ConfigurationManager.AppSettings["TwitterAccessTokenSecret"].ToString();

                twitterService.AuthenticateWith(twitterAccessToken, twitterAccessTokenSecret);

                IEnumerable<TwitterStatus> tweets = twitterService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName = twitterUsername, Count = 10 });

              
                ViewBag.Tweets = tweets;

                return View();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return View();
            }


        }

       


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Cards.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Deck()
        {
           
            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            WR.UserAgent = ".NET Framework Test Client";

            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();
            StreamReader reader = new StreamReader(Response.GetResponseStream());
            string cardData = reader.ReadToEnd();

            JObject DeckJson = JObject.Parse(cardData);
            ViewBag.DeckId = DeckJson["deck_id"];


            HttpWebRequest NewWR = WebRequest.CreateHttp($"https://deckofcardsapi.com/api/deck/" + ViewBag.DeckId + "/draw/?count=5");
            WR.UserAgent = ".NET Framework Test Client";
            HttpWebResponse NewResponse = (HttpWebResponse)NewWR.GetResponse();
            StreamReader Newreader = new StreamReader(NewResponse.GetResponseStream());
            string hand = Newreader.ReadToEnd();

            JObject NewDeckJson = JObject.Parse(hand);
            
            ViewBag.Hand = NewDeckJson["cards"];
           
            return View();
        }

                
    }
}
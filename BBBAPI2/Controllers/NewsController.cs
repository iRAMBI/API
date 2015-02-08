using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BBBAPI2.Models;
using Newtonsoft.Json.Linq;

namespace BBBAPI2.Controllers
{
    public class NewsController : ApiController
    {
        private irambidbEntities db = new irambidbEntities();

        // GET: api/News
       /* public IQueryable<News> GetNews()
        {
            return db.News;
        }*/

        public IHttpActionResult GetCriticalNews()
        {
            var result = from articles in db.News
                         where articles.priority == "critical"
                         select articles;

            var resultList = result.ToList();

            List<News> newsArticles = new List<News>();
            string dataString = "[";

            foreach(News article in resultList){
                dataString += "{ 'newsid': '" + article.newsid + "', 'userid' : '" + article.userid + "', 'title' : '" + article.title + "', 'content' : '" + article.content + "'},";
            }

            dataString = dataString.Substring(0, dataString.Length - 1);
            dataString += "]";

            


            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Critical News Fetched",
                data = JObject.Parse("{ 'criticalnews': " + dataString + "}")
                //data = resultList
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));

        }


        // GET: api/News/5
        [ResponseType(typeof(News))]
        public IHttpActionResult GetNews(int id)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            return Ok(news);
        }

        // PUT: api/News/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNews(int id, News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != news.newsid)
            {
                return BadRequest();
            }

            db.Entry(news).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/News
        [ResponseType(typeof(News))]
        public IHttpActionResult PostNews(News news)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.News.Add(news);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = news.newsid }, news);
        }

        // DELETE: api/News/5
        [ResponseType(typeof(News))]
        public IHttpActionResult DeleteNews(int id)
        {
            News news = db.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }

            db.News.Remove(news);
            db.SaveChanges();

            return Ok(news);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsExists(int id)
        {
            return db.News.Count(e => e.newsid == id) > 0;
        }
    }
}
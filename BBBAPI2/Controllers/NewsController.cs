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
using System.Diagnostics;

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

        //Gets all standard news
        public IHttpActionResult GetStandardNews(string userid, string token)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token, userid))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            //now get news

            //gets all articles that match the user's programid
            var result = from articles in db.News
                         where (from users in db.Users
                                where users.userid == userid
                                select users.programid).Contains(articles.programid)
                         select articles;

            List<News> resultList = result.ToList();
            string dataString = "[";

            foreach (News article in resultList)
            {
                /*if (article.priority.Equals("critical") && Convert.ToDateTime(article.datetime) <= DateTime.Now)
                {
                    //this is an expired critical message
                    continue;
                }*/
                

                //foreach article get the number of comments for it
                /*var count = (from comments in db.Comments
                             where comments.newsid == article.newsid
                             select comments).Count();*/
                //foreach article resolve the name of the author
                /*var author = (from user in db.Users
                              where user.userid == article.userid
                              select user);
                List<User> authors = author.ToList();

                string fname = authors.First().firstname;
                string lname = authors.First().lastname;*/

                dataString += "{ 'newsid': '" + article.newsid 
                    + "', 'userid' : '" + article.userid 
                    + "', 'author': '" + article.User.firstname + " " + article.User.lastname 
                    + "', 'coursesectionid' : '" + article.coursesectionid 
                    + "', 'datetime' : '" + article.datetime
                    + "', 'title' : '" + article.title 
                    + "', 'content' : '" + article.content 
                    + "', 'numcomments' : " + article.Comments.Count + "},";
            }

            dataString = dataString.Substring(0, dataString.Length - 1);
            dataString += "]";

            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Standard News Fetched",
                data = JObject.Parse("{ 'news': " + dataString + "}")
                //data = resultList
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));

            
            /*var commentCount = from articles in db.News
                               where articles.newsid == articles.Comments.newsid
                               select articles;

            */

           /* var result = from articles in db.News
                           join comment in db.Comments on articles.newsid equals comment.newsid
                           where (from users in db.Users
                                  where users.userid == userid
                                  select users.programid).Contains(articles.programid)
                           group comment by articles into grp
                           select new { userid = grp.userid, newsid = grp.Key , comments = grp.Count() };*/


            /*var total = from comments in db.Comments
                            where comments.newsid == result.newsid*/
            
        }

        //Gets all critical news articles
        public IHttpActionResult GetCriticalNews()
        {
            var result = db.getCriticalNews();

            var resultList = result.ToList();

            string dataString = "[";

            foreach(var article in resultList){
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

        //Gets a single article
        public IHttpActionResult GetArticle(string userid, int newsid, string token)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token, userid))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            //find article
            var result = from articles in db.News
                         where (from users in db.Users
                                where users.userid == userid
                                select users.programid).Contains(articles.programid) &&
                                articles.newsid == newsid
                         select articles;
           
            //if no article respond with 404
            if (result.FirstOrDefault() == null)
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 404,
                    message = "Article Not Found"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, error));
            }

            News article = result.FirstOrDefault();

            //get comments
            var commentResults = from comments in db.Comments
                                 where comments.newsid == article.newsid
                                 orderby comments.datetime ascending
                                 select comments;

            List<Comment> commentList = commentResults.ToList();
            string dataString = "[";

            foreach(Comment comment in commentList){
                dataString += "{ 'commentid': '" + comment.commentid 
                    + "', 'userid' : '" + comment.userid 
                    + "', 'newsid' : '" + comment.newsid  
                    + "', 'datetime' : '" + comment.datetime 
                    + "', 'content' : '" + comment.content + "' },";
            }


            //if there are no comments don't cut off the start of the array
            if (dataString.Length > 1)
            {
                dataString = dataString.Substring(0, dataString.Length - 1);
            }
            
            dataString += "]";

            //respond
            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Article Fetched",
                data = JObject.Parse("{ 'newsid': '" + article.newsid 
                + "' , 'title' : '" + article.title 
                + "' , 'content' : '" + article.content 
                + "' , 'authorname' : '" + article.User.firstname + " " + article.User.lastname 
                + "' , 'authorid' : '" + article.userid 
                + "' , 'datetime' : '" + article.datetime 
                + "' , 'numcomments' : '" + commentList.Count 
                + "' , 'comments' : " + dataString + "}")
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));




        }

        [HttpPost]
        public IHttpActionResult PostNewsArticle(string userid, string token, [FromBody] News body)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token, userid))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            //formalize data with any missing content
            body.userid = userid;
            body.datetime = DateTime.Now;
            body.active = true;

            Debug.WriteLine(body.ToString());

            db.News.Add(body);
            db.SaveChanges();

            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 201,
                message = "Successfuly Created News Article",
                data = JObject.Parse("{ 'newsid': " + body.newsid + "' }")
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.Created, success));

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
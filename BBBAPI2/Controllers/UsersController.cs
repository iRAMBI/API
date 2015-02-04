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
using Newtonsoft.Json;

namespace BBBAPI2.Controllers
{
    public class UsersController : ApiController
    {
        private irambidbEntities db = new irambidbEntities();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
       /* [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.userid)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/
/*
        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.userid }, user);
        }
*/
        // POST: api/auth
        [ResponseType(typeof(User))]
        public IHttpActionResult PostAuthenticate(User user)
        {
            var email = user.email;
            var password = user.password;

            var result = from aUser in db.Users
                         where aUser.email == email
                         && aUser.password == password
                         select aUser;

            if (result.FirstOrDefault() == null)
            {
                Console.WriteLine("No Results Found");
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Credentials"
                };

                //return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, JsonConvert.SerializeObject(error)));
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));


            }
            else
            {
                string newToken = TokenGenerator.CreateToken(new Random().Next(15,30));
                User theUser = result.FirstOrDefault();
                theUser.token = newToken;

                db.SaveChanges();




                JSONResponderClass success = new JSONResponderClass()
                {
                    statuscode = 200,
                    message = "Authentication Successful",
                    data = JObject.Parse("{ 'token': '" + newToken + "' }")
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));
            }
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.userid }, user);*/
            
            //string json = JsonConvert.SerializeObject(user);
            
        }


        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

       /* private bool UserExists(int id)
        {
            return db.Users.Count(e => e.userid == id) > 0;
        }*/
    }
}
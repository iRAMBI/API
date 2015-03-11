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
    public class ContactController : ApiController
    {
        private irambidbEntities db = new irambidbEntities();

        [HttpGet]
        public IHttpActionResult GetContact(string userid, string token, string page = null)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token. GetContact"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            //get all contacts in the same program as the user's id
            var results = from users in db.Users
                          where (from user in db.Users
                                 where user.userid == userid
                                 select user.programid).Contains(users.programid)
                           orderby users.lastname ascending
                          select users;

            //offset logic for paging
            int offset = 50;
            if (page != null)
            {
                if (Convert.ToInt32(page) > 1)
                {
                    offset *= Convert.ToInt32(page);
                }
            }

            //offset logic for paging
            List<User> userList = results.Skip(offset - 50).Take(offset).ToList();
            string dataString = "[";

            foreach (User singleUser in userList)
            {
                dataString += "{ 'userid' : '" + singleUser.userid + "', 'name' : '" + singleUser.firstname + " " + singleUser.lastname + "' },";
            }

            dataString = dataString.Substring(0, dataString.Length - 1);
            dataString += "]";

            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Contacts Fetched",
                data = JObject.Parse("{ 'contacts': " + dataString + "}")
                //data = resultList
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));


        }

        public IHttpActionResult GetSpecificContact(string userid, string contactid, string token)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token. GetSpecificContact"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            //get information about specific contact
            var result = from contact in db.Teachers
                         where contact.userid == contactid
                         select contact;

            Teacher theContact = result.FirstOrDefault();

            if (theContact == null)
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 404,
                    message = "Contact Not Found"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, error));
            }

           /* var resultucs = from ucs in db.UserCourseSections
                            where ucs.userid == contactid
                            select ucs;

            List<UserCourseSection> csList = resultucs.ToList();
            String dataString = "[";

            foreach (UserCourseSection ucs in csList)
            {

                var tresult = from teacher in db.UserCourseSections
                              where teacher.role == "Instructor" && teacher.coursesectionid == ucs.coursesectionid
                              select teacher;

                UserCourseSection tucs = tresult.FirstOrDefault();


                dataString += "{ 'coursesectionid' : '" + ucs.coursesectionid 
                    + "', 'courseid' : '" + ucs.CourseSection.courseid 
                    + "', 'coursename': '" + ucs.CourseSection.Course.name 
                    + "', 'datetimestart' : '" + ucs.CourseSection.datetimestart 
                    + "', 'datetimeend' : '" + ucs.CourseSection.datetimeend 
                    + "', 'teacherid' : '" + tucs.userid + "' },";
            }



            dataString = dataString.Substring(0, dataString.Length - 1);
            dataString += "]";*/


            //respond
            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Contacts Fetched",
                data = JObject.Parse("{ 'userid': '" +  theContact.userid 
                    + "', 'name' : '" + theContact.User.firstname + " " + theContact.User.lastname 
                    + "' , 'department' : '" + theContact.facultyid  //is this correct for department ?
                    + "', 'position' : '" + theContact.position 
                    + "', 'email' : '" + theContact.User.email
                    + "', 'alternate' : '" + theContact.alternateemail 
                    + "', 'phone' : '" + theContact.User.phonenumber
                    + "', 'officelocation' : '" + theContact.officelocation
                    + "', 'officehours' : " 
                        + "{ 'monday' : '" + theContact.ohmonday 
                        + "', 'tuesday' : '" + theContact.ohtuesday 
                        + "' , 'wednesday' : '" + theContact.ohwednesday 
                        + "' , 'thursday' : '" + theContact.ohthursday 
                        + "' , 'friday' : '" + theContact.ohfriday 
                        + "'}"
                    + "}" )
                //data = resultList
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));
        }

        [HttpGet]
        public IHttpActionResult GetPostableContacts(string userid, string token)
        {
            Debug.WriteLine("Made it here");
            //validate token
            if (!TokenGenerator.ValidateToken(token))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token. Here"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            // gets all course secitons the user is enrolled in. This will include the global course section
            var results2 = from ucs in db.UserCourseSections
                           where ucs.userid == userid
                           select ucs;



            //get all contacts in the same program as the user's id
           /* var results = from users in db.Users
                          where (from user in db.Users
                                 where user.userid == userid
                                 select user.programid).Contains(users.programid)
                          orderby users.lastname ascending
                          select users;*/

           // List<User> userList = results.ToList();

            string dataString = "[";

            List<UserCourseSection> ucsList = results2.ToList();

            foreach (UserCourseSection ucsItem in ucsList)
            {
                dataString += "{ 'coursesectionid' : '" + ucsItem.CourseSection.courseid + "', 'coursename' : '" + ucsItem.CourseSection.Course.name + "' },";
            }

           /* foreach (User singleUser in userList)
            {
                dataString += "{ 'userid' : '" + singleUser.userid + "', 'name' : '" + singleUser.firstname + " " + singleUser.lastname + "', 'coursesection' : '" + singleUser.UserCourseSections.ToList().ToString() + "' },";
            }*/

            dataString = dataString.Substring(0, dataString.Length - 1);
            dataString += "]";

            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Postable Contacts Fetched",
                data = JObject.Parse("{ 'coursesections': " + dataString + "}")
                //data = resultList
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));

        }



        // GET: api/Contact
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Contact/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Contact/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
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
        }

        // POST: api/Contact
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.userid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.userid }, user);
        }

        // DELETE: api/Contact/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
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

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.userid == id) > 0;
        }
    }
}
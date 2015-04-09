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


        [HttpPost]
        public IHttpActionResult PostSearchContact(string userid, string token, [FromBody] Search search)
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

            //get all user course sections that contain the same corsesection id as me and have a user
            // with a firstname and lastname that contains the search content
            var results = from ucs in db.UserCourseSections
                          where (from myucs in db.UserCourseSections 
                                 where myucs.userid == userid 
                                 select myucs.coursesectionid).Contains(ucs.coursesectionid) 
                          && (ucs.User.firstname + " " + ucs.User.lastname).Contains(search.searchContent)
                          select ucs;

            List<UserCourseSection> userList = results.ToList();

            string dataString = "[";

            foreach (UserCourseSection singleUser in userList)
            {
                dataString += "{ 'userid' : '" + singleUser.User.userid + "', 'name' : '" + singleUser.User.firstname + " " + singleUser.User.lastname + "', 'coursesectionid' : '" + singleUser.coursesectionid + "', 'coursename': '" + singleUser.CourseSection.Course.name + "' },";
            }

            dataString = dataString.Substring(0, dataString.Length - 1);
            dataString += "]";

            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Contacts Search Results Fetched",
                data = JObject.Parse("{ 'contacts': " + dataString + "}")
                //data = resultList
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));
        }



        [HttpGet]
        public IHttpActionResult GetContact(string userid, string token, string page = null)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token, userid))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token."
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
                //don't include ourselves in the list
                if (singleUser.userid != userid)
                {
                    dataString += "{ 'userid' : '" + singleUser.userid + "', 'name' : '" + singleUser.firstname + " " + singleUser.lastname + "' },";
                }
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

        [HttpGet]
        public IHttpActionResult GetSpecificContact(string userid, string contactid, string token)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token, userid))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token. GetSpecificContact"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            //get information about specific contact, assuming a teacher
            var result = from contact in db.Teachers
                         where contact.userid == contactid
                         select contact;

            Teacher theContact = result.FirstOrDefault();

            //if null then maybe its a student?
            if (theContact == null)
            {

                User aStudentUser = (from studentContact in db.Users
                              where studentContact.userid == contactid
                              select studentContact).FirstOrDefault();

                //if there is no student then this is obviously not a contact
                if (aStudentUser == null)
                {
                    JSONResponderClass error = new JSONResponderClass()
                    {
                        statuscode = 404,
                        message = "Contact Not Found"
                    };

                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, error));
                }
                else
                {
                    //respond
                    JSONResponderClass studentSuccess = new JSONResponderClass()
                    {
                        statuscode = 200,
                        message = "Contacts Fetched",
                        data = JObject.Parse("{ 'userid': '" + aStudentUser.userid
                            + "', 'name' : '" + aStudentUser.firstname + " " + aStudentUser.lastname
                            + "' , 'department' : '" + ""
                            + "', 'position' : '" + "Student"
                            + "', 'email' : '" + aStudentUser.email
                            + "', 'alternate' : '" + ""
                            + "', 'phone' : '" + aStudentUser.phonenumber
                            + "', 'officelocation' : '" + ""
                            + "', 'officehours' : "
                                + "{ 'monday' : '" + ""
                                + "', 'tuesday' : '" + ""
                                + "' , 'wednesday' : '" + ""
                                + "' , 'thursday' : '" + ""
                                + "' , 'friday' : '" + ""
                                + "'}"
                            + "}")
                    };

                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, studentSuccess));
                }               
            }


            //respond
            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Contacts Fetched",
                data = JObject.Parse("{ 'userid': '" +  theContact.userid 
                    + "', 'name' : '" + theContact.User.firstname + " " + theContact.User.lastname 
                    + "' , 'department' : '" + theContact.Faculty.facultyname
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
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));
        }

        [HttpGet]
        public IHttpActionResult GetPostableContacts(string userid, string token)
        {
            Debug.WriteLine("Made it here");
            //validate token
            if (!TokenGenerator.ValidateToken(token, userid))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token."
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            // gets all course secitons the user is enrolled in. This will include the global course section
            var results2 = from ucs in db.UserCourseSections
                           where ucs.userid == userid
                           select ucs;

            string dataString = "[";

            List<UserCourseSection> ucsList = results2.ToList();

            foreach (UserCourseSection ucsItem in ucsList)
            {
                dataString += "{ 'coursesectionid' : '" + ucsItem.CourseSection.coursesectionid + "', 'coursename' : '" + ucsItem.CourseSection.Course.name + "' },";
            }

            dataString = dataString.Substring(0, dataString.Length - 1);
            dataString += "]";

            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Postable Contacts Fetched",
                data = JObject.Parse("{ 'coursesections': " + dataString + "}")
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));

        }

        /*

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
         */
    }
}
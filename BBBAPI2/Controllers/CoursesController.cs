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
    public class CoursesController : ApiController
    {
        private irambidbEntities db = new irambidbEntities();

        // GET: api/Courses
        public IQueryable<Course> GetCourses()
        {
            return db.Courses;
        }

        public IHttpActionResult GetCourses(string userid, string token)
        {
            //validate token
            if (!TokenGenerator.ValidateToken(token))
            {
                JSONResponderClass error = new JSONResponderClass()
                {
                    statuscode = 403,
                    message = "Invalid Token"
                };

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error));
            }

            /*var result2 = from user in db.UserCourseSections
                          where user.userid == userid
                          select user;

            var result3 = from teachers in db.UserCourseSections
                          where teachers.role == "instructor"
                          select teachers;*/

          
            var result = from courseSection in db.UserCourseSections
                         where courseSection.userid == userid
                         select courseSection.CourseSection;

            List<CourseSection> courseSectionList = result.ToList();
            string dataString = "[";

            // TODO Get Teacher id somehow
            foreach (CourseSection cs in courseSectionList)
            {

                //find what teacher teaches this course section
                var tuserid = from teacher in db.UserCourseSections
                             where teacher.role == "instructor" &&
                                   teacher.coursesectionid == cs.coursesectionid
                             select teacher.User.userid;

                string teacherid = tuserid.FirstOrDefault() == null ? "" : tuserid.FirstOrDefault();

                dataString += "{ 'coursesectionid' : '" + cs.coursesectionid + "' , 'courseid' : '" + cs.courseid + "' , 'coursename' : '" + cs.Course.name + "' , 'teacherid' : '" + teacherid + "' , 'datetimestart' : '" + cs.datetimestart + "' , 'datetimeend' : '" + cs.datetimeend + "'},";
            }

            if (courseSectionList.Count > 1)
            {
                dataString = dataString.Substring(0, dataString.Length - 1);
            }
            
            dataString += "]";

            JSONResponderClass success = new JSONResponderClass()
            {
                statuscode = 200,
                message = "Courses Fetched",
                data = JObject.Parse("{ 'courses': " + dataString + "}")
                //data = resultList
            };

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, success));


        }

        // GET: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult GetCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCourse(int id, Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != course.courseid)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        [ResponseType(typeof(Course))]
        public IHttpActionResult PostCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Courses.Add(course);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = course.courseid }, course);
        }

        // DELETE: api/Courses/5
        [ResponseType(typeof(Course))]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return NotFound();
            }

            db.Courses.Remove(course);
            db.SaveChanges();

            return Ok(course);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CourseExists(int id)
        {
            return db.Courses.Count(e => e.courseid == id) > 0;
        }
    }
}
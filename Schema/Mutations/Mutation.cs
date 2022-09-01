using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using HotChocolate;
using HotChocolate.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Mutations
{
    public class Mutation
    {
        private readonly List<CourseResult> _courses;
        public Mutation()
        {
            _courses = new List<CourseResult>();
        }

        public async Task<CourseResult> CreateCourse(string name, Subject subject, Guid instructorId, [Service]ITopicEventSender topicEventSender)
        {
            CourseResult courseType = new CourseResult()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Subject = subject,
                InstructorId=instructorId
            };

            _courses.Add(courseType);
            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseType);

            return courseType;
        }

        public CourseResult UpdateCourse(Guid id, string name, Subject subject, Guid instructorId)
        {
            var course = _courses.FirstOrDefault(x => x.Id == id);
            
            if (course==null)
            {
                throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));                
            }

            course.Name = name;
            course.Subject = subject;
            course.InstructorId = instructorId;
            return course;
        }

        public bool DeleteCourse(Guid id)
        {            
            return _courses.RemoveAll(x => x.Id == id) >= 1; ;
        }
    }
}

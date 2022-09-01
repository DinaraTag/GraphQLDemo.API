using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Queries
{
    public enum Subject
    {
        Math,
        Science,
        History
    }

    public class CourseType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Subject Subject { get; set; }
        public InstructorType Instructor { get; set; }
        public IEnumerable<StudentType> Students { get; set; }
    }
}

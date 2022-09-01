using Bogus;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Queries
{
    public class Query
    {
        private readonly Faker<InstructorType> _instructorFaker;
        private readonly Faker<StudentType> _studentFaker;
        private readonly Faker<CourseType> _courseFaker;

        public Query()
        {
            _instructorFaker = new Faker<InstructorType>()
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.Salary, f => f.Random.Double(0, 1000000));
            _studentFaker = new Faker<StudentType>()
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.FirstName, f => f.Name.FirstName())
                .RuleFor(x => x.LastName, f => f.Name.LastName())
                .RuleFor(x => x.GPA, f => f.Random.Double(1, 5));
            _courseFaker = new Faker<CourseType>()
                .RuleFor(x => x.Id, f => Guid.NewGuid())
                .RuleFor(x => x.Name, f => f.Name.JobTitle())
                .RuleFor(x => x.Subject, f => f.PickRandom<Subject>())
                .RuleFor(x => x.Instructor, f => _instructorFaker.Generate())
                .RuleFor(x => x.Students, f => _studentFaker.Generate(3));
        }
        
        public IEnumerable<CourseType> GetCourses()
        {           
            return _courseFaker.Generate(5);
        }

        public async Task<CourseType> GetCourseByIdAsync(Guid id)
        {
            await Task.Delay(1000);
            CourseType course= _courseFaker.Generate();
            course.Id = id;

            return course;
        }


        [GraphQLDeprecated("This query is depricated")]
        public string Instructions => "Instructions";
    }
}

using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDemo.API.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreated([EventMessage]CourseResult course) => course;
    }
}

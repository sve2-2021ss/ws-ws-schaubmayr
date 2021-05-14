using GraphQL.Types;
using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.GraphTypes.InputTypes
{
    public class ProjectInputType : InputObjectGraphType
    {
        public ProjectInputType()
        {
            Name = "ProjectInput";
            Field<NonNullGraphType<IntGraphType>>(nameof(ProjectDto.IdLab));
            Field<NonNullGraphType<StringGraphType>>(nameof(ProjectDto.Name));
            Field<NonNullGraphType<StringGraphType>>(nameof(ProjectDto.Customer));
        }
    }
}

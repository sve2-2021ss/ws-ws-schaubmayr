using GraphQL.Types;
using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.GraphTypes.InputTypes
{
    public class LabInputType : InputObjectGraphType
    {
        public LabInputType()
        {
            Name = "LabInput";
            Field<NonNullGraphType<StringGraphType>>(nameof(LabDto.Name));
            Field<NonNullGraphType<StringGraphType>>(nameof(LabDto.Location));
        }
    }
}

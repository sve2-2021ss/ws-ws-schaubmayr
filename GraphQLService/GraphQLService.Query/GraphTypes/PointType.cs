using GraphQL.Types;
using GraphQLService.Dto.Models;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.GraphTypes
{
    public class PointType : ObjectGraphType<PointDto>
    {
        public PointType(IPointRepository pointRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.IdProject).Description("IdProject");
            Field(x => x.IdSeries).Description("IdSeries");
            Field(x => x.IdPoint).Description("IdPoint");
            Field(x => x.IdList, nullable:true).Description("IdList");
            Field(x => x.Name).Description("Name");
            Field(x => x.Timestamp, nullable: true).Description("Timestamp");
        }
    }
}

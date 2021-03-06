using GraphQL.Types;
using GraphQLService.Dto.Models;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.GraphTypes
{
    public class SeriesType : ObjectGraphType<SeriesDto>
    {
        public SeriesType(ISeriesRepository seriesRepository, IPointRepository pointRepository)
        {
            Field(x => x.IdLab).Description("IdLab");
            Field(x => x.IdProject).Description("IdProject");
            Field(x => x.IdSeries).Description("IdSeries");
            Field(x => x.Name).Description("Name");
            Field(x => x.Timestamp, nullable: true).Description("Timestamp");

            Field<ListGraphType<PointType>>(
              "points",
              resolve: x => pointRepository.GetPointsForSeries(x.Source.IdLab, x.Source.IdProject, x.Source.IdSeries)
          );
        }
    }
}

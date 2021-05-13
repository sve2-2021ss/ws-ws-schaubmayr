using GraphQL.Types;
using GraphQLService.Dto.Models;
using GraphQLService.Query.GraphTypes;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLService.Query
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(
            ILabRepository labRepository, 
            IProjectRepository projectRepository, 
            IListRepository listRepository,
             ISeriesRepository seriesRepository)
        {
            Field<ListGraphType<LabType>>(
                "labs",
                resolve: context => labRepository.GetAll()
                );

            Field<ListGraphType<ProjectType>>(
                "projects",
                resolve: context => projectRepository.GetAll()
                );

            Field<ListGraphType<ListType>>(
               "lists",
               resolve: context => listRepository.GetAll()
               );

            Field<ListGraphType<SeriesType>>(
            "series",
            resolve: context => seriesRepository.GetAll()
            );
        }
    }
}

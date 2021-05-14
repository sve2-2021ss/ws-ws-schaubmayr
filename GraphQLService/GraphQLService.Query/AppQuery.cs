using GraphQL;
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
             ISeriesRepository seriesRepository,
             IPointRepository pointRepository)
        {
            #region GetAll
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

            Field<ListGraphType<PointType>>(
              "points",
              resolve: x => pointRepository.GetAll()
          );
            #endregion
            #region GetById
            Field<LabType>(
              "lab",
              arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name="idLab" }),
              resolve: context =>
              {
                  return labRepository.GetById(context.GetArgument<int>("idLab"));
              }
              );

            Field<ProjectType>(
             "project",
               arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" }
                 ),
             resolve: context =>
             {
                 return projectRepository.GetById(
                   context.GetArgument<int>("idLab"),
                   context.GetArgument<int>("idProject"));
             }
             );

            Field<SeriesType>(
             "serie",
               arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idSeries" }
                 ),
             resolve: context =>
             {
                 return seriesRepository.GetById(
                   context.GetArgument<int>("idLab"),
                   context.GetArgument<int>("idProject"),
                   context.GetArgument<int>("idSeries"));
             }
             );

            Field<ListType>(
             "list",
               arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idList" }
                 ),
             resolve: context =>
             {
                 return listRepository.GetById(
                   context.GetArgument<int>("idLab"),
                   context.GetArgument<int>("idProject"),
                   context.GetArgument<int>("idList"));
             }
             );

            Field<PointType>(
             "point",
             arguments: new QueryArguments(
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idLab" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idProject" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idSeries" },
                 new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "idPoint" }
                 ),
             resolve: context =>
             {
                 return pointRepository.GetById(
                     context.GetArgument<int>("idLab"),
                     context.GetArgument<int>("idProject"),
                     context.GetArgument<int>("idSeries"),
                     context.GetArgument<int>("idPoint"));
             }
             );
            #endregion
        }
    }
}

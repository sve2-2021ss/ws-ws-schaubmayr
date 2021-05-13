using GraphQLService.DAL.Database;
using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Logic
{
    public static class DtoConverters
    {
        public static LabDto Convert(this Lab entity)
        {
            return new LabDto()
            {
                IdLab = entity.IdLab,
                Location = entity.Location,
                Name = entity.Name,
                Timestamp = entity.Timestamp
            };
        }

        public static IEnumerable<LabDto> Convert(this IEnumerable<Lab> collection)
        {
            foreach (var item in collection)
            {
                yield return item.Convert();
            }
        }

        public static ProjectDto Convert(this Project entity)
        {
            return new ProjectDto()
            {
                IdLab = entity.IdLab,
                IdProject = entity.IdProject,
                Customer = entity.Customer,
                Name = entity.Name,
                Timestamp = entity.Timestamp
            };
        }

        public static IEnumerable<ProjectDto> Convert(this IEnumerable<Project> collection)
        {
            foreach (var item in collection)
            {
                yield return item.Convert();
            }
        }

        public static ListDto Convert(this List entity)
        {
            return new ListDto()
            {
                IdLab = entity.IdLab,
                IdProject = entity.IdProject,
                IdList = entity.IdList,
                Name = entity.Name,
                Timestamp = entity.Timestamp
            };
        }

        public static IEnumerable<ListDto> Convert(this IEnumerable<List> collection)
        {
            foreach (var item in collection)
            {
                yield return item.Convert();
            }
        }
        public static SeriesDto Convert(this Series entity)
        {
            return new SeriesDto()
            {
                IdLab = entity.IdLab,
                IdProject = entity.IdProject,
                IdSeries = entity.IdSeries,
                Name = entity.Name,
                Timestamp = entity.Timestamp
            };
        }

        public static IEnumerable<SeriesDto> Convert(this IEnumerable<Series> collection)
        {
            foreach (var item in collection)
            {
                yield return item.Convert();
            }
        }

    }
}

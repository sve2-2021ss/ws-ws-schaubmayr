using GraphQLService.DAL.Database;
using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Logic
{
    public static class DtoConverters
    {
        /// <summary>
        /// Converts Lab Entity to LabDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Converts Project Entity to ProjectDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Converts List Entity to ListDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Converts Series Entity to SeriesDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Converts Point Entity to PointDto
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static PointDto Convert(this Point entity)
        {
            return new PointDto()
            {
                IdLab = entity.IdLab,
                IdProject = entity.IdProject,
                IdSeries = entity.IdSeries,
                IdList = entity.IdList,
                IdPoint = entity.IdPoint,
                Name = entity.Name,
                Timestamp = entity.Timestamp
            };
        }

        public static IEnumerable<PointDto> Convert(this IEnumerable<Point> collection)
        {
            foreach (var item in collection)
            {
                yield return item.Convert();
            }
        }

    }
}

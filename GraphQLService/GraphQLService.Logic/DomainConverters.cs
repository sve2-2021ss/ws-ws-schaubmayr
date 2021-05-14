using GraphQLService.DAL.Database;
using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Logic
{
    public static class DomainConverters
    {
        /// <summary>
        /// Converts LabDto Entity to Lab
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Lab Convert(this LabDto entity)
        {
            return new Lab()
            {
                IdLab = entity.IdLab,
                Location = entity.Location,
                Name = entity.Name,
                Timestamp = entity.Timestamp
            };
        }

        /// <summary>
        /// Converts ProjectDto Entity to Project
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Project Convert(this ProjectDto entity)
        {
            return new Project()
            {
                IdLab = entity.IdLab,
                IdProject = entity.IdProject,
                Customer = entity.Customer,
                Name = entity.Name,
                Timestamp = entity.Timestamp
            };
        }
    }
}

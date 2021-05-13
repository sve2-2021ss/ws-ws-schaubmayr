﻿using GraphQLService.DAL;
using GraphQLService.Dto.Models;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GraphQLService.Logic;

namespace GraphQLService.Query.Implementations
{
    public class SeriesRepository : ISeriesRepository
    {
        private IDbService _dbService;

        public SeriesRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public IEnumerable<SeriesDto> GetAll()
        {
            return _dbService.GetDbContext().Series.Convert();
        }

        public IEnumerable<SeriesDto> GetSeriesForLab(int idLab)
        {
            return _dbService.GetDbContext().Series.Where(e => e.IdLab == idLab).Convert();
        }

        public IEnumerable<SeriesDto> GetSeriesForProject(int idLab, int idProject)
        {
            return _dbService.GetDbContext().Series
                .Where(e => e.IdLab == idLab && e.IdProject == idProject)
                .Convert();
        }
    }
}

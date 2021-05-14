using GraphQLService.DAL;
using GraphQLService.Dto.Models;
using GraphQLService.Logic;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphQLService.Query.Implementations
{
    public class PointRepository : IPointRepository
    {
        private IDbService _dbService;

        public PointRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        public IEnumerable<PointDto> GetAll()
        {
            return _dbService.GetDbContext().Points.Convert();
        }

        public PointDto GetById(int idLab, int idProject, int idSeries, int idPoint)
        {
            return _dbService.GetDbContext().Points.SingleOrDefault(e => e.IdLab == idLab
           && e.IdProject == idProject && e.IdSeries == idSeries)?.Convert();
        }

        public IEnumerable<PointDto> GetPointsForLab(int idLab)
        {
            return _dbService.GetDbContext().Points.Where(e => e.IdLab == idLab).Convert();
        }

        public IEnumerable<PointDto> GetPointsForList(int idLab, int idProject, int idList)
        {
            return _dbService.GetDbContext().Points.Where(e => e.IdLab == idLab 
            && e.IdProject == idProject && e.IdList == idList).Convert();
        }

        public IEnumerable<PointDto> GetPointsForProject(int idLab, int idProject)
        {
            return _dbService.GetDbContext().Points.Where(e => e.IdLab == idLab
          && e.IdProject == idProject).Convert();
        }

        public IEnumerable<PointDto> GetPointsForSeries(int idLab, int idProject, int idSeries)
        {
            return _dbService.GetDbContext().Points.Where(e => e.IdLab == idLab
            && e.IdProject == idProject && e.IdSeries == idSeries).Convert();
        }
    }
}

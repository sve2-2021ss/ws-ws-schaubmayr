using GraphQLService.DAL;
using GraphQLService.Dto.Models;
using GraphQLService.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GraphQLService.Logic;
using System.Threading.Tasks;

namespace GraphQLService.Query.Implementations
{
    public class SeriesRepository : ISeriesRepository
    {
        private IDbService _dbService;

        public SeriesRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public Task<SeriesDto> Create(SeriesDto series)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SeriesDto> GetAll()
        {
            return _dbService.GetDbContext().Series.Convert();
        }

        public SeriesDto GetById(int idLab, int idProject, int idSeries)
        {
            return _dbService.GetDbContext().Series
               .SingleOrDefault(e => e.IdLab == idLab && e.IdProject == idProject && e.IdSeries == idSeries)
               ?.Convert();
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

using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.Query.Interfaces
{
    public interface ISeriesRepository
    {
        public IEnumerable<SeriesDto> GetAll();
        public IEnumerable<SeriesDto> GetSeriesForProject(int idLab, int idProject);
        public IEnumerable<SeriesDto> GetSeriesForLab(int idLab);
        public SeriesDto GetById(int idLab, int idProject, int idSeries);
    }
}

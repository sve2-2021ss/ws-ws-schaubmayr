using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.Interfaces
{
    public interface ISeriesRepository
    {
        public IEnumerable<SeriesDto> GetAll();
        public IEnumerable<SeriesDto> GetSeriesForProject(int idLab, int idProject);
        public IEnumerable<SeriesDto> GetSeriesForLab(int idLab);
    }
}

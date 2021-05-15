using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.Query.Interfaces
{
    public interface IPointRepository
    {
        public IEnumerable<PointDto> GetAll();
        public IEnumerable<PointDto> GetPointsForProject(int idLab, int idProject);
        public IEnumerable<PointDto> GetPointsForLab(int idLab);
        public IEnumerable<PointDto> GetPointsForList(int idLab, int idProject, int idList);
        public IEnumerable<PointDto> GetPointsForSeries(int idLab, int idProject, int idSeries);
        public PointDto GetById(int idLab, int idProject, int idSeries, int idPoint);
    }
}

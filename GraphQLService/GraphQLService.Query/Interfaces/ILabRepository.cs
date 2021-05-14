using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLService.Query.Interfaces
{
    public interface ILabRepository
    {
        IEnumerable<LabDto> GetAll();
        LabDto GetById(int idLab);
        Task<LabDto> Create(LabDto lab);
        Task<LabDto> Update(LabDto lab, int idLab);
        Task<bool> Delete(int idLab);
    }
}

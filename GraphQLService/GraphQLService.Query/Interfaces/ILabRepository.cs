using GraphQLService.Dto.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLService.Query.Interfaces
{
    public interface ILabRepository
    {
        IEnumerable<LabDto> GetAll();
    }
}

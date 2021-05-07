using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLService.Dto.Models
{
    public class LabDto
    {
        public int IdLab { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}

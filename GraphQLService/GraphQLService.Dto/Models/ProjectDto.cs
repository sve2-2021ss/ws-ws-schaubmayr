using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLService.Dto.Models
{
    public class ProjectDto
    {
        public int IdLab { get; set; }
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}

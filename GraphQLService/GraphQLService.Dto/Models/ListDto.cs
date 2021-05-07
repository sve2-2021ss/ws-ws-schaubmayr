using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLService.Dto.Models
{
    public class ListDto
    {
        public int IdLab { get; set; }
        public int IdProject { get; set; }
        public int IdList { get; set; }
        public string Name { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}

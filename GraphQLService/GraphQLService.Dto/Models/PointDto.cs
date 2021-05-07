using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLService.Dto.Models
{
    public class PointDto
    {
        public int IdLab { get; set; }
        public int IdProject { get; set; }
        public int IdSeries { get; set; }
        public int IdPoint { get; set; }
        public string Name { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? IdList { get; set; }
    }
}

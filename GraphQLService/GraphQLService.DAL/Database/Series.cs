using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLService.DAL.Database
{
    public partial class Series
    {
        public Series()
        {
            Points = new HashSet<Point>();
        }

        public int IdLab { get; set; }
        public int IdProject { get; set; }
        public int IdSeries { get; set; }
        public string Name { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual ICollection<Point> Points { get; set; }
    }
}

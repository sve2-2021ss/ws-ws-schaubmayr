using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLService.DAL.Database
{
    public partial class Lab
    {
        public Lab()
        {
            Projects = new HashSet<Project>();
        }

        public int IdLab { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}

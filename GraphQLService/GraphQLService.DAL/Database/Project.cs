using System;
using System.Collections.Generic;

#nullable disable

namespace GraphQLService.DAL.Database
{
    public partial class Project
    {
        public int IdLab { get; set; }
        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public DateTime? Timestamp { get; set; }

        public virtual Lab IdLabNavigation { get; set; }
    }
}

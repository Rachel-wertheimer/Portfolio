using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_service
{
    public  class RepositoryDTO
    {
            public string Name { get; set; }
            public string Language { get; set; }
            public string Url { get; set; }
            public string Description { get; set; }
            public int Status { get; set; }
            public string? PresentationUrl { get; set; }

    }
}

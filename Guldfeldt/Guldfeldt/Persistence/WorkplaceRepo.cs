using Guldfeldt.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Persistence
{
    public class WorkplaceRepo
    {
        string? ConnectionString;

        private List<Workplace> Workplaces;
        public WorkplaceRepo()
        {
            Workplaces = new List<Workplace>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }
    }
}

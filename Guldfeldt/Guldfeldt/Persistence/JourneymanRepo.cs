using Guldfeldt.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guldfeldt.Persistence
{
    public class JourneymanRepo
    {
        string? ConnectionString;

        private List<Journeyman> Journeymen;
        public JourneymanRepo()
        {
            Journeymen = new List<Journeyman>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }
    }
}

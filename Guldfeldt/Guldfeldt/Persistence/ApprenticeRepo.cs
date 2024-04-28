using Guldfeldt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Configuration;


namespace Guldfeldt.Persistence
{
    public class ApprenticeRepo 
    {
        string? ConnectionString;

        private List<Apprentice> Apprentices;
        public ApprenticeRepo()
        {
            Apprentices = new List<Apprentice>();

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("MyDBConnection");

        }
    }
}

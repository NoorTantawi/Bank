using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.infrastructure.DTO
{
    public class DashboardDTO
    {
        public int NumberOfClients { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfAccountOpenings { get; set; }
        public int NumberOfLoans { get; set; }

    }
}

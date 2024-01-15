using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.Repositories
{
    internal static class ConnectionStrings
    {
        public static string ConnectionString { get; set; } = @"Server=tcp:mihailosqlserver.database.windows.net,1433;Initial Catalog=ShiftPointCalculator;Persist Security Info=False;User ID=Mihailo;Password=Mika2211;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    }
}
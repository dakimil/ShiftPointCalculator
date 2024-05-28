using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftPointCalculator.Repositories
{
    internal static class ConnectionStrings
    {
        //public static string ConnectionString { get; set; } = @"Encrypt=True;TrustServerCertificate=False;Server=tcp:mihailosqlserver.database.windows.net,1433;Initial Catalog=ShiftPointCalculator;Persist Security Info=False;User ID=Mihailo;Password=Mika2211;MultipleActiveResultSets=False;Connection Timeout=30;";
        public static string ConnectionString { get; set; } = @"Encrypt=False;TrustServerCertificate=True;Server=localhost;Initial Catalog=ShiftPointCalculator;Persist Security Info=False;User ID=sa;Password=ivivouerc;MultipleActiveResultSets=False;Connection Timeout=30;";

    }
}
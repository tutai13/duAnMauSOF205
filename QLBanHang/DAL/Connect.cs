using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Connect
    {
        protected SqlConnection _conn = new SqlConnection(@"Data Source=LAPTOP-25SOSMQ7;Initial Catalog=QLBH;Integrated Security=True;Encrypt=False");
    }
}

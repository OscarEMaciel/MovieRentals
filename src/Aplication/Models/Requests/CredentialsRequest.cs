using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Models.Requests
{
    public class CredentialsRequest
    {
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}

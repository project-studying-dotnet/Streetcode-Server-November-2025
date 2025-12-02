using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.DTO.Streetcode.Types
{
    public class CreatePersonStreetcodeDTO : CreateStreetcodeDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}

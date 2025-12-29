using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.DTO.Users
{
	public class LogoutRequestDto
	{
		public string RefreshToken { get; set; } = string.Empty;
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
	public class AddLoginRequest
	{
		public int Id { get; set; }
		public string userName { get; set; }
		public string password { get; set; }
	}
}

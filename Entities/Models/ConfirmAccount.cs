using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ConfirmAccount
    {
        public string EmailAddress { get; set; }
        public string ConfirmCode { get; set; }
    }
}

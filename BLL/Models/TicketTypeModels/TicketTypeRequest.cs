using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.TicketTypeModels
{
    public class TicketTypeRequest
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool IsActive { get; set; }
    }
}

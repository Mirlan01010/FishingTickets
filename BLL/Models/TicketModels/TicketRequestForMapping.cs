using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.TicketModels
{
    public class TicketRequestForMapping
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? MiddleName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int RegionId { get; set; }
        public int TicketTypeId { get; set; }
        public int WaterBodyId { get; set; }
        public int CitizenShipId { get; set; }

    }
}

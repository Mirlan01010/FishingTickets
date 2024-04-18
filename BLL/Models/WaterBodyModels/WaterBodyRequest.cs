using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.WaterBodyModels
{
    public class WaterBodyRequest
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int RegionId { get; set; }
    }
}

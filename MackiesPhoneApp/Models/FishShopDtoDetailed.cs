using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public  class FishShopDtoDetailed
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? OperationAreaId { get; set; }
        public int? EmployeeId { get; set; }

        public OperationAreaDto Area { get; set; }
        public EmployeeDto? Employee { get; set; }
    }
}

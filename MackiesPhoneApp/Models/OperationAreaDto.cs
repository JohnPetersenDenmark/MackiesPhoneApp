using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MackiesPhoneApp.Models
{
    public  class OperationAreaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public List<LocationDto> Locations { get; set; }
        public List<TemplateScheduleDto> TemplateSchedules { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sachin_452_Models.ViewModel
{
    public class FilterDataOrderModel
    {
        public DateTime StartDate { get; set; }

        public DateTime Enddate { get; set; }

        public string StartDateFormatted => StartDate.ToString("yyyy-MM-dd");
        public string EndDateFormatted => Enddate.ToString("yyyy-MM-dd");
    }
}

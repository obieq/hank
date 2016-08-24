using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elephant.Hank.DataService.DBSchema
{
    public class TblRestaurant : BaseTable
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}

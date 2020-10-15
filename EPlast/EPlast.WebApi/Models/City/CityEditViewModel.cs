using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPlast.WebApi.Models.City
{
    public class CityEditViewModel
    {
        public string UserID { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<CityViewModel> AllCities { get; set; }
        public string UserCity { get; set; }
        public CityEditViewModel()
        {
            AllCities = new List<CityViewModel>();
        }
    }
}

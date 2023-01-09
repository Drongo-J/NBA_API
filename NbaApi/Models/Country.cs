using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NbaApi.Models
{
    public class Country
    {
        public string CountryName { get; set; }
        public string ImagePath { get; set; } = @"\Assets\noImage.png";
    }
}

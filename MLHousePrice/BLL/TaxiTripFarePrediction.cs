using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace MLHousePrice.DAL
{
    public class TaxiTripFarePrediction
    {
        [ColumnName("Score")]
        public float FareAmount;
    }
}

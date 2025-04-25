using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarUtils
{
    public static class PanelCalculator
    {
        /// Returns the estimated annual energy output (kWh)
        public static decimal EstimateAnnualOutput(decimal dailyIntensity, decimal panelSize)
        {
            // dailyIntensity (kWh/m²/day) * panelSize (kW) * 365 days
            return dailyIntensity * panelSize * 365m;
        }
    }
}

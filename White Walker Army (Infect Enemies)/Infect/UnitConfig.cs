using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace White_Walker_Army__Infect_Enemies_.Infect
{
    public class UnitUnitConfig
    {
        [JsonProperty("unitInfectUnit")]
        public List<UnitInfectUnit> UnitInfectUnit { get; set; }
    }
}

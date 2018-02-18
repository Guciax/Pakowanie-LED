using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakowanie_LED
{
    public class PackingLayers
    {
        public PackingLayers(string layerName, bool completed, DateTime completionTime, int modulesPerLayer,List<string> moduleQrCodes)
        {

            LayerName = layerName;
            Completed = completed;
            CompletionTime = completionTime;
            ModulesPerLayer = modulesPerLayer;
            ModuleQrCodes = moduleQrCodes;
        }


        public string LayerName { get; }
        public bool Completed { get; set; }
        public DateTime CompletionTime { get; set; }
        public int ModulesPerLayer { get; }
        public List<string> ModuleQrCodes { get; set; }
    }
}

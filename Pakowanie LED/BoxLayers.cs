using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pakowanie_LED
{
    class BoxLayers
    {
        public BoxLayers(string layerName, bool completed, DateTime completionTime)
        {
            LayerName = layerName;
            Completed = completed;
            CompletionTime = completionTime;
        }

        public string LayerName { get; }
        public bool Completed { get; set; }
        public DateTime CompletionTime { get; set; }
    }
}

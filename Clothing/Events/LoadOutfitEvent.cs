using Clothing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothing.Events
{
    public class LoadOutfitEvent
    {
        private Outfit _outfit;

        public Outfit Outfit
        {
            get { return _outfit; }
        }

        public LoadOutfitEvent(Outfit outfit)
        {
            _outfit = outfit;
        }
    }
}

using Clothing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothing.Events
{
    public class DeleteOutfitEvent
    {
        private Outfit _outfit;

        public Outfit Outfit
        {
            get { return _outfit; }
        }

        public DeleteOutfitEvent(Outfit outfit)
        {
            _outfit = outfit;
        }
    }
}

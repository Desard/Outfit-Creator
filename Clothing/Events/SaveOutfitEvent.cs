using Clothing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothing.Events
{
    public class SaveOutfitEvent
    {
        private Outfit _outfit;
        private bool _updateOutfit;

        public bool UpdateOutfit
        {
            get { return _updateOutfit; }
            set { _updateOutfit = value; }
        }


        public Outfit Outfit
        {
            get { return _outfit; }
        }

        public SaveOutfitEvent(Outfit outfit, bool update)
        {
            _outfit = outfit;
            UpdateOutfit = update;
        }
    }
}

using Caliburn.Micro;
using Clothing.Events;
using Clothing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clothing.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<CreateOutfitEvent>, IHandle<SaveOutfitEvent>, IHandle<LoadOutfitEvent>, IHandle<DeleteOutfitEvent>
    {
        private DisplayOutfitViewModel _displayOutfitVM;
        private CreateOutfitViewModel _createOutfitVM;
        private IEventAggregator _events;
        private IWindowManager _windowManager;
        private SimpleContainer _container;

        public string Item { get; set; }

        public ShellViewModel(DisplayOutfitViewModel displayOutfitVM, CreateOutfitViewModel createOutfitVM, IEventAggregator events, IWindowManager windowManager, SimpleContainer container)
        {
            _events = events;
            _container = container;
            _windowManager = windowManager;
            _displayOutfitVM = displayOutfitVM;
            _createOutfitVM = createOutfitVM;
            _events.Subscribe(this);
            ActivateItem(_displayOutfitVM);
        }

        public void Handle(SaveOutfitEvent message)
        {
            if (message.UpdateOutfit)
                DBAccess.UpdateOutfit(message.Outfit);
            else
                message.Outfit.Id = DBAccess.SaveOutfit(message.Outfit);

            _displayOutfitVM.Update(message);
        }

        public void Handle(CreateOutfitEvent message)
        {
            ResetCreateWindow();
            _windowManager.ShowDialog(_createOutfitVM);
        }

        public void Handle(LoadOutfitEvent message)
        {
            ResetCreateWindow();
            _createOutfitVM.LoadOutfitIntoUI(message);
            _windowManager.ShowDialog(_createOutfitVM);
        }

        public void ResetCreateWindow()
        {
            _createOutfitVM = _container.GetInstance<CreateOutfitViewModel>();
        }

        public void Handle(DeleteOutfitEvent message)
        {
            DBAccess.DeleteOutfit(message.Outfit);
        }
    }
}

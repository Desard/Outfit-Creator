using Caliburn.Micro;
using Clothing.Events;
using Clothing.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Clothing.ViewModels
{
    public class DisplayOutfitViewModel : Screen, IHandle<DeleteOutfitEvent>
    {
        private IEventAggregator _events;
        private ObservableCollection<Outfit> _listViewOutfits;

        public ObservableCollection<Outfit> ListViewOutfits
        {
            get { return _listViewOutfits; }
            set { _listViewOutfits = value; NotifyOfPropertyChange(() => ListViewOutfits); }
        }

        public DisplayOutfitViewModel(IEventAggregator events)
        {
            _events = events;
            _events.Subscribe(this);
            ListViewOutfits = DBAccess.LoadOutfits();
            Outfit addOutfit = new Outfit();
            addOutfit.TitleName = "";
            addOutfit.isAddSymbol = true;

            string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string currentAssemblyParentPath = Path.GetDirectoryName(currentAssemblyPath);

            addOutfit.TitleImage = new BitmapImage(new Uri(String.Format("file:///{0}/../Images/Add.jpg", currentAssemblyParentPath)));

            ListViewOutfits.Add(addOutfit);
            NotifyOfPropertyChange(() => ListViewOutfits);
        }

        public void Update(SaveOutfitEvent message)
        {
            if (!message.UpdateOutfit)
                ListViewOutfits.Insert(ListViewOutfits.Count - 1, message.Outfit);
            else
            {
                for (int i = 0; i < ListViewOutfits.Count; i++)
                {
                    if (ListViewOutfits[i].Id == message.Outfit.Id)
                    {
                        ListViewOutfits.RemoveAt(i);
                        ListViewOutfits.Insert(i, message.Outfit);
                        break;
                    }
                }
            }
        }

        public void HoverStart(Object sender)
        {
            Border border = (Border)sender;
            Color color = Colors.Blue;
            border.Effect = new DropShadowEffect { Color = color, ShadowDepth = 2, Opacity = 1 };
        }

        public void HoverEnd(Object sender)
        {
            Border border = (Border)sender;
            Color color = Colors.Black;
            border.Effect = new DropShadowEffect { Color = color, ShadowDepth = 2, Opacity = 1 };
        }

        public void OutfitClicked(Object sender)
        {
            Border border = (Border)sender;
            Outfit outfit = border.DataContext as Outfit;

            if (outfit.isAddSymbol)
                _events.PublishOnUIThread(new CreateOutfitEvent());
            else
                _events.PublishOnUIThread(new LoadOutfitEvent(outfit));
        }

        public void Handle(DeleteOutfitEvent message)
        {
            for (int i = 0; i < ListViewOutfits.Count; i++)
            {
                if (ListViewOutfits[i].Id == message.Outfit.Id)
                {
                    ListViewOutfits.RemoveAt(i);
                    break;
                }
            }
        }
    }
}

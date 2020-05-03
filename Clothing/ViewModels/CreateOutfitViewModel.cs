using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Clothing.Interfaces;
using Clothing.Models;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using Clothing.Events;

namespace Clothing.ViewModels
{
    public class CreateOutfitViewModel : Screen
    {
        private IEventAggregator _events;
        private Outfit _outfit;
        private string _titleName;
        private SimpleContainer _container;
        private bool _alteringOutfit = false;

        public string TitleName
        {
            get { return _titleName; }
            set { _titleName = value; NotifyOfPropertyChange(() => TitleName); }
        }

        public CreateOutfitViewModel(SimpleContainer container, IEventAggregator events)
        {
            _container = container;
            _events = events;
            _outfit = (Outfit)_container.GetInstance<IOutfit>();
            TitleName = "Title";
        }

        public void LoadOutfitIntoUI(LoadOutfitEvent message)
        {
            _outfit = message.Outfit;
            TitleName = _outfit.TitleName;
            _alteringOutfit = true;
        }

        public void SaveOutfit()
        {
            _outfit.TitleName = TitleName;
            _events.PublishOnUIThread(new SaveOutfitEvent(_outfit, _alteringOutfit));
            TryClose();
        }

        public void DeleteOutfit()
        {
            _events.PublishOnUIThread(new DeleteOutfitEvent(_outfit));
            TryClose();
        }

        public void ImageDropped(Object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length == 1)
                {
                    string sourceFilePath = files[0];
                    string fileExtension = Path.GetExtension(sourceFilePath);

                    if (fileExtension == ".jpg" || fileExtension == ".png")
                    {
                        Border border = (Border)sender;
                        BitmapImage bitmapImage = new BitmapImage(new Uri(sourceFilePath, UriKind.Absolute));

                        _outfit.AddImageByName(bitmapImage, border.Name);

                        ImageBrush brush = new ImageBrush(bitmapImage);
                        border.Background = brush;
                    }
                }
            }
        }

        public void HoverStart(Object sender)
        {
            Border border = (Border)sender;
            Color color = Colors.Blue;
            border.Effect = new DropShadowEffect {Color = color, ShadowDepth = 2, Opacity = 1};

            Panel.SetZIndex(border, 1);
            border.Width = 130;
            border.Height = 130;
        }

        public void HoverEnd(Object sender)
        {
            Border border = (Border)sender;
            Color color = Colors.Black;
            border.Effect = new DropShadowEffect { Color = color, ShadowDepth = 2, Opacity = 1 };

            Panel.SetZIndex(border, 0);
            border.Width = 60;
            border.Height = 60;
        }

        public void SaveDeleteButtonHoverStart(Object sender)
        {
            Border border = (Border)sender;
            Color color = Colors.Blue;
            border.Effect = new DropShadowEffect { Color = color, ShadowDepth = 2, Opacity = 1 };
        }

        public void SaveDeleteButtonHoverEnd(Object sender)
        {
            Border border = (Border)sender;
            Color color = Colors.Black;
            border.Effect = new DropShadowEffect { Color = color, ShadowDepth = 2, Opacity = 1 };
        }

        public void ImageContainerLoaded(Object sender)
        {
            Border border = (Border)sender;

            if (_outfit.GetImageByName(border.Name) == null)
            {
                ImageBrush imageBrush = (ImageBrush)border.Background;
                BitmapImage image = new BitmapImage(new Uri(imageBrush.ImageSource.ToString(), UriKind.Absolute));
                _outfit.AddImageByName(image, border.Name);
            }
            else
            {
                border.Background = new ImageBrush(_outfit.GetImageByName(border.Name));
            }
        }
    }
}

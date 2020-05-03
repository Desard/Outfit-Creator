using Clothing.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Clothing.DatabaseHelper
{
    public class DBOutfit
    {
        public int Id { get; set; }
        public string TitleName { get; set; }
        public Byte[] TitleImage { get; set; }
        public Byte[] Chest { get; set; }
        public Byte[] Pants { get; set; }
        public Byte[] Shoes { get; set; }
        public Byte[] Necklace { get; set; }
        public Byte[] Earrings { get; set; }
        public Byte[] Rings { get; set; }
        public Byte[] Head { get; set; }
        public Byte[] Wrist { get; set; }

        public DBOutfit()
        {

        }

        public DBOutfit(Outfit outfit)
        {
            Id = outfit.Id;
            TitleName = outfit.TitleName;
            Chest = ImageToByteArray(outfit.Chest);
            Pants = ImageToByteArray(outfit.Pants);
            Shoes = ImageToByteArray(outfit.Shoes);
            Necklace = ImageToByteArray(outfit.Necklace);
            Earrings = ImageToByteArray(outfit.Earrings);
            Rings = ImageToByteArray(outfit.Rings);
            Head = ImageToByteArray(outfit.Head);
            Wrist = ImageToByteArray(outfit.Wrist);
            TitleImage = ImageToByteArray(outfit.TitleImage);
        }
        public Outfit ConvertToOutfit()
        {
            Outfit outfit = new Outfit();

            outfit.Id = Id;
            outfit.TitleName = TitleName;
            outfit.Chest = ByteArrayToImage(Chest);
            outfit.Pants = ByteArrayToImage(Pants);
            outfit.Shoes = ByteArrayToImage(Shoes);
            outfit.Necklace = ByteArrayToImage(Necklace);
            outfit.Earrings = ByteArrayToImage(Earrings);
            outfit.Rings = ByteArrayToImage(Rings);
            outfit.Head = ByteArrayToImage(Head);
            outfit.Wrist = ByteArrayToImage(Wrist);
            outfit.TitleImage = ByteArrayToImage(TitleImage);

            return outfit;
        }

        private Byte[] ImageToByteArray(BitmapImage bitmapImage)
        {
            Byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

        private BitmapImage ByteArrayToImage(Byte[] array)
        {
            if (array != null)
            {
                using (var ms = new MemoryStream(array))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
            }

            //if (array != null)
            //{
            //    var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(array);
            //    return (BitmapImage)bitmap;
            //}
            return null;
        }
    }
}

using Clothing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Clothing.Models
{
    public class Outfit : IOutfit
    {
        public int Id { get; set; }
        public bool isAddSymbol { get; set; } = false;
        private string _titleName;

        public string TitleName
        {
            get { return _titleName; }
            set { _titleName = value; }
        }

        public BitmapImage TitleImage { get; set; }
        public BitmapImage Chest { get; set; }
        public BitmapImage Pants { get; set; }
        public BitmapImage Shoes { get; set; }
        public BitmapImage Necklace { get; set; }
        public BitmapImage Earrings { get; set; }
        public BitmapImage Rings { get; set; }
        public BitmapImage Head { get; set; }
        public BitmapImage Wrist { get; set; }

        public void AddImageByName(BitmapImage image, string name)
        {
            switch (name)
            {
                case "Head":
                    Head = image;
                    break;
                case "Chest":
                    Chest = image;
                    break;
                case "Pants":
                    Pants = image;
                    break;
                case "Shoes":
                    Shoes = image;
                    break;
                case "Necklace":
                    Necklace = image;
                    break;
                case "Earrings":
                    Earrings = image;
                    break;
                case "Rings":
                    Rings = image;
                    break;
                case "Wrist":
                    Wrist = image;
                    break;
                case "TitleImage":
                    TitleImage = image;
                    break;
            }
        }

        public BitmapImage GetImageByName(string name)
        {
            BitmapImage bitmapImage = null;
            switch (name)
            {
                case "Head":
                    bitmapImage = Head;
                    break;
                case "Chest":
                    bitmapImage = Chest;
                    break;
                case "Pants":
                    bitmapImage = Pants;
                    break;
                case "Shoes":
                    bitmapImage = Shoes;
                    break;
                case "Necklace":
                    bitmapImage = Necklace;
                    break;
                case "Earrings":
                    bitmapImage = Earrings;
                    break;
                case "Rings":
                    bitmapImage = Rings;
                    break;
                case "Wrist":
                    bitmapImage = Wrist;
                    break;
                case "TitleImage":
                    bitmapImage = TitleImage;
                    break;
            }

            return bitmapImage;
        }
    }
}

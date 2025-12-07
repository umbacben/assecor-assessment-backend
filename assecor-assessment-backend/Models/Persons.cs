using System;
using System.Linq;

namespace assecor_assessment_backend.Models
{
    public class Persons
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Color { get; set; }

        private Dictionary<int, string> ColorDictionary = new Dictionary<int, string>(){
            {1, "blau" },
            {2, "grün" },
            {3, "violett" },
            {4, "rot" },
            {5, "gelb" },
            {6, "türkis" },
            {7, "weiß" }
        };

        public Persons()
        {
            Id = -1;
            FirstName = "";
            LastName = "";
            Zipcode = "";
            City = "";
            Color = "";
        }

        public Persons(int _Id, string _FirstName, string _LastName, string _Location, int _Color)
        {
            this.Id = _Id;
            this.FirstName = _FirstName.Trim();
            this.LastName = _LastName.Trim();
            var Locations = _Location.Trim().Split(" ");
            Zipcode = Locations[0];
            City = Locations[1];
            Color = ColorDictionary[_Color];
        }

        public Persons(int _Id, string _FirstName, string _LastName, string _ZipCode, string _City, string _Color)
        {
            this.Id = _Id;
            this.FirstName = _FirstName.Trim();
            this.LastName = _LastName.Trim();
            Zipcode = _ZipCode;
            City = _City;
            Color = _Color;
        }

        public bool ToCSVString(out string CVSString)
        {
            int ColorKey;
            if (!DoesColorExist(out ColorKey))
            {
                CVSString = "";
                return false;
            }
            CVSString = $"{LastName},{FirstName},{Zipcode} {City},{ColorKey}";
            return true;
        }

        public bool DoesColorExist(out int colorkey)
        {
            if (!ColorDictionary.ContainsValue(Color))
            {
                colorkey = -1;
                return false;
            }

            colorkey = ColorDictionary.FirstOrDefault(Item => Item.Value == Color).Key;

            return true;
        }
    }
}

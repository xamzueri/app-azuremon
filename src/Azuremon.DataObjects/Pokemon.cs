using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Azuremon.DataObjects
{
    public class Pokemon : BaseDataObject
    {

        public Pokemon()
        {
            Categories = new List<Category>();
        }
        public double Number { get; set; }

        public string Name { get; set; }

        public double Stamina { get; set; }

        public double Attack { get; set; }

        public double Defense { get; set; }

        public string CaptureRate { get; set; }

        public string FleeRate { get; set; }

        public string Candy { get; set; }

        public string QuickMoves { get; set; }

        public string SpecialMoves { get; set; }

        public virtual ICollection<Category> Categories { get; set; }


#if MOBILE
        public string Type { get; set; }

        bool isFavorite;
        [Newtonsoft.Json.JsonIgnore]
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                SetProperty(ref isFavorite, value);
            }
        }

        [Newtonsoft.Json.JsonIgnore]
        public Category MainCategory => Categories != null && Categories.Count > 0 ? Categories.ElementAt(0) : null;

        [Newtonsoft.Json.JsonIgnore]
        public Category SecondaryCategory => Categories != null && Categories.Count > 1 ? Categories.ElementAt(1) : null;

#endif
    }
}

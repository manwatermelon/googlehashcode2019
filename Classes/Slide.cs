using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode2019.Classes {
class Slide
    {
        public List<Photo> Photos = new List<Photo>();

        public override string ToString()
        {
            var description = string.Empty;
            foreach (Photo photo in this.Photos)
            {
                description += photo.index + " ";
            }

            description.TrimEnd(' ');
            return description;
        }
    }
}

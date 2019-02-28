using System.Collections.Generic;

namespace GoogleHashCode2019.Classes {
public class Slide
    {
        public List<Photo> Photos = new List<Photo>();

        public override string ToString()
        {
            var description = string.Empty;
            foreach (Photo photo in this.Photos)
            {
                description += photo.Index + " ";
            }

            description.TrimEnd(' ');
            return description;
        }
    }
}

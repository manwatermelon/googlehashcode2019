using System.Collections.Generic;

namespace GoogleHashCode2019.Classes {
public class Slide
    {
        public List<Photo> Photos = new List<Photo>();
        public List<string> SlideTags = new List<string>();

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

        public void AddPhoto(Photo photo)
        {
            this.Photos.Add(photo);
            this.SlideTags.AddRange(photo.Tags);
        }
    }
}

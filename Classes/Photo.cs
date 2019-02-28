using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode2019.Classes
{
    public class Photo
    {
        public enum ePhotoOrientation { H = 0, V = 1 };

        public ePhotoOrientation Orientation;
        public List<string> Tags;
        public int index;

        public Photo(string parameters, int index)
        {
            var a = parameters.Split(" ".ToCharArray());
            string orientationString = a[0];

            this.index = index;
            this.Orientation = (ePhotoOrientation)Enum.Parse(typeof(ePhotoOrientation), a[0]);

            int tagCount = Int32.Parse(a[1]);

            this.Tags = new List<string>();
            for (int j = 2; j < a.Count(); j++)
            {
                string stringTag = a[j];
                this.Tags.Add(stringTag);
            }

        }

        public Photo()
        {

        }
    }
}

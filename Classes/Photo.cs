using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleHashCode2019.Classes
{
    class Photo
    {
        public enum ePhotoOrientation { H = 1, V = 2 };

        public ePhotoOrientation Orientation;
        public List<string> Tags;
    }
}

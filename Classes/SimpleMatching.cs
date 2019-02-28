using System;
using System.Collections.Generic;

namespace GoogleHashCode2019.Classes
{
    public static class SimpleMatching
    {
        public static List<Slide> GetSlides(List<Photo> photoList)
        {
            List<Slide> slideList = new List<Slide>();
            List<Photo> horizontalPhotos = photoList.FindAll(photo => photo.Orientation == Photo.ePhotoOrientation.H);
            List<Photo> verticalPhotos = photoList.FindAll(photo => photo.Orientation == Photo.ePhotoOrientation.V);

            Slide slide = new Slide();

            Photo lastPhoto = null;
            if (horizontalPhotos.Count > 0)
            {
                lastPhoto = horizontalPhotos[0];
                lastPhoto.IsUsed = true;
                slide.AddPhoto(lastPhoto);
            }
            else
            {
                Photo firstVertPhoto = verticalPhotos[0];
                firstVertPhoto.IsUsed = true;
                slide.AddPhoto(firstVertPhoto);

                Photo secVertPhoto = verticalPhotos[1];
                secVertPhoto.IsUsed = true;
                slide.AddPhoto(secVertPhoto);
            }

            slideList.Add(slide);


            int photosLeft = photoList.Count - slide.Photos.Count;
            while (photosLeft > 0)
            {
                Photo nextPhoto = null;
                foreach (string tag in slide.SlideTags)
                {
                    nextPhoto = photoList.Find(photo => !photo.IsUsed && photo.Tags.Contains(tag));
                    if (nextPhoto != null)
                    {
                        break;
                    }
                }

                if (nextPhoto == null)
                {
                    nextPhoto = photoList.Find(photo => !photo.IsUsed);
                }

                if (nextPhoto == null)
                {
                    break;
                }


                nextPhoto.IsUsed = true;
                photosLeft--;

                slide = new Slide();
                slide.AddPhoto(nextPhoto);

                if (nextPhoto.Orientation.Equals(Photo.ePhotoOrientation.V))
                {
                    Photo secondVertPhoto = verticalPhotos.Find(photo => !photo.IsUsed);
                    if (secondVertPhoto != null)
                    {
                        photosLeft--;
                        secondVertPhoto.IsUsed = true;
                        slide.AddPhoto(secondVertPhoto);
                    }
                }

                slideList.Add(slide);

            }

            return slideList;
        }
    }
}

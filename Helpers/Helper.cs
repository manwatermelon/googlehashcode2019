using System;
using System.Collections.Generic;
using System.Linq;
using GoogleHashCode2019.Classes;

namespace GoogleHashCode2019.Helpers
{
    public static class Helper
    {
        public static Stats GetIntersectWeight(Photo p1, Photo p2)
        {
            Stats stats = new Stats
            {
                Common = p1.Tags.Intersect(p2.Tags).Count()
            };
            stats.MinDiff = Math.Min(p1.Tags.Count - stats.Common, p2.Tags.Count - stats.Common);
            stats.MinDiff = Math.Min(stats.MinDiff, stats.Common);
            return stats;
        }

        public static Stats GetStatsSH(Photo hP, Slide vS)
        {
            Stats res = new Stats();

            var tmp = new List<string>();
            foreach(var p in vS.Photos)
            {
                tmp.AddRange(p.Tags);
            }
            res.Common = hP.Tags.Intersect(tmp).Count();
            res.MinDiff = Math.Min(hP.Tags.Count - res.Common, tmp.Count - res.Common);
            res.MinDiff = Math.Min(res.MinDiff, res.Common);

            return res;
        }


        public static List<Slide> ProcessVList(List<Photo> vPhotos)
        {
            List<Photo> copy = new List<Photo>(vPhotos);
            List<Slide> slides = new List<Slide>();
            do
            {
                Photo last = copy.Last();
                Photo best = null;
                int bestIndex = -1;
                Stats s1 = new Stats();
                for (int i = 0; i < copy.Count-2; i ++)
                {
                    Stats tmpStats = new Stats();
                    Photo tmp = copy.ElementAt(i);
                    tmpStats = GetIntersectWeight(last, tmp);
                    if (tmpStats.MinDiff > s1.MinDiff)
                    {
                        s1 = tmpStats;
                        bestIndex = i;
                    }

                }
                if (bestIndex != -1)
                {
                    best = copy.ElementAt(bestIndex);
                    Slide slide = new Slide();
                    slide.Photos = new List<Photo>() { last, best };
                    slides.Add(slide);
                    copy.RemoveAt(copy.Count - 1);
                    copy.RemoveAt(bestIndex);
                }

            } while (copy.Count > 1);

            return slides;
        }

        public static List<Slide> ProcessSlides(List<Slide> vSlides, List<Photo> hPhoto)
        {
            List<Slide> result = new List<Slide>();

            if (hPhoto.Count > 0)
            {
                Slide lastSlide = null;
                List<Photo> copy = new List<Photo>(hPhoto);
                Photo bestFromHor = null;
                Slide tmpVSlide = null;

                Photo last = copy.Last();
                Stats curBestStats = new Stats();
                Stats curBestPstats = new Stats();
                int bestIndex = -1;
                Stats s1 = new Stats();
                for (int i = 0; i < copy.Count - 2; i++)
                {
                    Stats tmpStats = new Stats();
                    Photo tmp = copy.ElementAt(i);
                    tmpStats = GetIntersectWeight(last, tmp);
                    if (tmpStats.MinDiff > s1.MinDiff)
                    {
                        s1 = tmpStats;
                        bestIndex = i;
                        curBestStats = tmpStats;
                    }

                }
                if (bestIndex != -1)
                {
                    bestFromHor = copy.ElementAt(bestIndex);
                }


                foreach (var slide in vSlides)
                {
                    if (curBestPstats.MinDiff < GetStatsSH(bestFromHor, slide).MinDiff)
                    {
                        tmpVSlide = slide;
                        curBestPstats = GetStatsSH(bestFromHor, slide);
                    }
                }

                Slide lastPhotoSlide = new Slide();
                lastPhotoSlide.Photos.Add(last);
                result.Add(lastPhotoSlide);

                if (curBestStats.MinDiff < curBestPstats.MinDiff)
                {
                    //take vertical slide
                    result.Add(tmpVSlide);
                    lastSlide = tmpVSlide;

                }
                else
                {
                    Slide horPhotoSlide = new Slide();
                    horPhotoSlide.Photos.Add(bestFromHor);
                    result.Add(horPhotoSlide);
                    lastSlide = horPhotoSlide;
                }



            }


            return result;
        }



    }
}

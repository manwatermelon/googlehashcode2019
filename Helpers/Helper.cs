﻿using System;
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

        public static Stats CompareSlides(Slide s1, Slide s2)
        {
            Stats res = new Stats();
            List<string> tmp1 = CombineTags(s1);

            var tmp2 = CombineTags(s2);

            res.Common = tmp1.Intersect(tmp2).Count();
            res.MinDiff = Math.Min(tmp1.Count - res.Common, tmp2.Count - res.Common);
            res.MinDiff = Math.Min(res.MinDiff, res.Common);

            return res;
        }

        private static List<string> CombineTags(Slide s1)
        {
            var tmp1 = new List<string>();
            foreach (var p in s1.Photos)
            {
                tmp1.AddRange(p.Tags);
            }

            return tmp1;
        }

        public static List<Slide> ProcessVList(List<Photo> vPhotos)
        {
            List<Photo> copy = new List<Photo>(vPhotos);
            List<Slide> slides = new List<Slide>();
            if (vPhotos.Count > 0)
            {
                do
                {
                    Photo last = copy.Last();
                    Photo best = null;
                    int bestIndex = 0;
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

                } while (copy.Count > 2);
            }

            return slides;
        }

        public static List<Slide> ProcessSlides(List<Slide> vSlides, List<Photo> hPhoto)
        {
            List<Slide> result = new List<Slide>();

            if (hPhoto.Count > 0)
            {
                Slide lastSlide = null;
                List<Photo> copy = new List<Photo>(hPhoto);
                Photo last = copy.Last();
                bool shouldCheckV = vSlides.Count > 2;
                int failCount = 0;
                while ((shouldCheckV && vSlides.Count > 2) || copy.Count > 2) 
                {

                    Photo bestFromHor = null;
                    Slide tmpVSlide = null;
                    
                    Stats curBestStats = new Stats();
                    Stats curBestPstats = new Stats();
                    int bestIndex = 0;
                    Stats s1 = new Stats();
                    for (int i = 0; i < copy.Count - 1; i++)
                    {
                        Stats tmpStats = new Stats();

                        Random r = new Random();
                        int rIntIdx = r.Next(0, copy.Count - 1); //for ints

                        Photo tmp = copy.ElementAt(rIntIdx);
                        tmpStats = GetIntersectWeight(last, tmp);
                        if (tmpStats.MinDiff > s1.MinDiff)
                        {
                            s1 = tmpStats;
                            bestIndex = rIntIdx;
                            curBestStats = tmpStats;
                        }

                        if (tmpStats.MinDiff > 2)
                        {
                            break;
                        }
                        else if (tmpStats.MinDiff == 2 && i > (copy.Count - 2) / 10)
                        {
                            break;
                        }
                        else if (tmpStats.MinDiff == 1 && i > (copy.Count - 2) / 5)
                        {
                            break;
                        }
                    }
                    if (copy.Count > 0)
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

                    // Slide lastPhotoSlide = new Slide();
                    //lastPhotoSlide.Photos.Add(last);
                    //  result.Add(lastPhotoSlide);
                    //  copy.Remove(last);vSlides

                        if (tmpVSlide != null && curBestStats.MinDiff < curBestPstats.MinDiff)
                        {
                            //take vertical slide
                            if (!result.Contains(tmpVSlide))
                            {
                                result.Add(tmpVSlide);
                            }

                            lastSlide = tmpVSlide;
                            vSlides.Remove(tmpVSlide);
                        }
                        else
                        {
                            Slide horPhotoSlide = new Slide();
                            horPhotoSlide.Photos.Add(bestFromHor);
                            if (!result.Contains(horPhotoSlide))
                            {
                                result.Add(horPhotoSlide);
                            }

                            lastSlide = horPhotoSlide;
                            copy.Remove(bestFromHor);
                        }

                        last = lastSlide.Photos.Last();


                    if (curBestStats.MinDiff == 0)
                    {
                        failCount++;
                        if (failCount > 500)
                        {
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    } else
                    {
                        failCount = 0;
                    }
                } 

            }


            return result;
        }



    }
}

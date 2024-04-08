using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations.Utils
{
    public static class RandomUtils
    {
        /// <summary>
        /// Get a random element from an IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pCollection"></param>
        /// <returns></returns>
        public static T GetRandomElement<T>(this IEnumerable<T> pCollection)
        {
            if (pCollection == null) return default;

            int lCollectionCount = pCollection.Count();
            return lCollectionCount > 0 ? pCollection.ElementAt(RandInt(lCollectionCount)) : default;
        }

        /// <summary>
        ///  Draws a Boolean random as a function of a percentage, for example Bool(0.45f) equals 45% chance of getting true
        /// </summary>
        /// <param name="pPourcent"></param>
        /// <example>
        /// Bool(0.45f) equals 45% chance of getting true
        /// </example>
        /// <returns></returns>
        public static bool Bool(float pPourcent = 0.5f) => Random.value < pPourcent;

        public static int RandInt(int pMin, int pMax = int.MaxValue) => Random.Range(pMin, pMax);

        public static int RandInt(int pMax = int.MaxValue) => RandInt(0, pMax);

        public static float Randf(float pMin, float pMax = float.MaxValue) => Random.Range(pMin, pMax);

        public static float Randf(float pMax = float.MaxValue) => Randf(0, pMax);
    }
}

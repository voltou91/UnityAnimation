using System;

//Author : Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public static class AnimationsRegistery
    {
        #region Linear
        public static float Linear(float pTime) => pTime;
        #endregion

        #region Sine
        public static float Sine(float pTime) => (float)(1 - Math.Cos(pTime * Math.PI / 2));
        #endregion

        #region Quint
        public static float Quint(float pTime) => pTime * pTime * pTime * pTime * pTime;
        #endregion

        #region Quart
        public static float Quart(float pTime) => pTime * pTime * pTime * pTime;
        #endregion

        #region Quad
        public static float Quad(float pTime) => pTime * pTime;
        #endregion

        #region Expo
        public static float Expo(float pTime) => (float)Math.Pow(2, 10 * (pTime - 1));
        #endregion

        #region Elastic
        public static float Elastic(float pTime) => (float)(-Math.Pow(2, 10 * (pTime - 1)) * Math.Sin((pTime - 1.075f) * (2 * Math.PI) / 0.3f));
        #endregion

        #region Cubic
        public static float Cubic(float pTime) =>  pTime * pTime * pTime;
        #endregion

        #region Circ
        public static float Circ(float pTime) => (float)(1 - Math.Sqrt(1 - (pTime * pTime)));
        #endregion

        #region Bounce
        public static float Bounce(float pTime) => 1 - BounceOut(1 - pTime);
        
        private static float BounceOut(float pTime)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            if (pTime < 1 / d1)
            {
                return n1 * pTime * pTime;
            }
            else if (pTime < 2 / d1)
            {
                pTime -= 1.5f / d1;
                return n1 * pTime * pTime + 0.75f;
            }
            else if (pTime < 2.5f / d1)
            {
                pTime -= 2.25f / d1;
                return n1 * pTime * pTime + 0.9375f;
            }
            else
            {
                pTime -= 2.625f / d1;
                return n1 * pTime * pTime + 0.984375f;
            }
        }
        #endregion

        #region Back
        public static float Back(float pTime) => pTime * pTime * ((1.7f + 1) * pTime - 1.7f);
        #endregion
    }
}

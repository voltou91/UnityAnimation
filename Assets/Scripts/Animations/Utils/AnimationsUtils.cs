using System;
using Unity.VisualScripting;
using UnityEngine;
using static Com.IsartDigital.Animations.AnimationBase;

namespace Com.IsartDigital.Animations.Utils
{
    public static class AnimationsUtils
    {
        public static void StopAllAnimations(this MonoBehaviour pObject)
        {
            foreach(AnimationBase lAnimation in pObject.GetComponents<AnimationBase>()) lAnimation.StopAnimation();
        }

        public static GenericAnimation<float, float> BeginAnimation(this MonoBehaviour pObject, float pInitialValue, float pFInalValue, float pDuration = 1, float pBeginAfter = 0f, TransitionType pTransitionType = TransitionType.Linear, EaseType pEaseType = EaseType.In, Action<float> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolate = null)
        {
            return pObject.AddComponent<FloatAnimation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolate);
        }

        public static GenericAnimation<Vector2, Vector2> BeginAnimation(this MonoBehaviour pObject, Vector2 pInitialValue, Vector2 pFInalValue, float pDuration = 1, float pBeginAfter = 0f, TransitionType pTransitionType = TransitionType.Linear, EaseType pEaseType = EaseType.In, Action<Vector2> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolateX = null, AnimationCurve pInterpolateY = null)
        {
            return pObject.AddComponent<Vector2Animation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolateX, pInterpolateY);
        }

        public static GenericAnimation<Vector3, Vector3> BeginAnimation(this MonoBehaviour pObject, Vector3 pInitialValue, Vector3 pFInalValue, float pDuration = 1, float pBeginAfter = 0f, TransitionType pTransitionType = TransitionType.Linear, EaseType pEaseType = EaseType.In, Action<Vector3> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolateX = null, AnimationCurve pInterpolateY = null, AnimationCurve pInterpolateZ = null)
        {
            return pObject.AddComponent<Vector3Animation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolateX, pInterpolateY, pInterpolateZ);
        }

        public static GenericAnimation<Vector3, Quaternion> BeginAnimation(this MonoBehaviour pObject, Vector3 pInitialValue, Vector3 pFInalValue, float pDuration = 1, float pBeginAfter = 0f, TransitionType pTransitionType = TransitionType.Linear, EaseType pEaseType = EaseType.In, Action<Quaternion> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolateX = null, AnimationCurve pInterpolateY = null, AnimationCurve pInterpolateZ = null)
        {
            return pObject.AddComponent<QuaternionAnimation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolateX, pInterpolateY, pInterpolateZ);
        }

        public static GenericAnimation<Color, Color> BeginAnimation(this MonoBehaviour pObject, Color pInitialValue, Color pFInalValue, float pDuration = 1, float pBeginAfter = 0f, TransitionType pTransitionType = TransitionType.Linear, EaseType pEaseType = EaseType.In, Action<Color> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolateR = null, AnimationCurve pInterpolateG = null, AnimationCurve pInterpolateB = null, AnimationCurve pInterpolateA = null)
        {
            return pObject.AddComponent<ColorAnimation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolateR, pInterpolateG, pInterpolateB, pInterpolateA);
        }
    }
}

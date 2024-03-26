using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Com.IsartDigital.Animations.Utils
{
    public static class Animations
    {
        public static GenericAnimation<Vector3> BeginVector3Animation(this MonoBehaviour pObject, Vector3 pInitialValue, Vector3 pFInalValue, float pDuration = 1, float pBeginAfter = 0f, Vector3Animation.TransitionType pTransitionType = Vector3Animation.TransitionType.Linear, Vector3Animation.EaseType pEaseType = Vector3Animation.EaseType.In, Action<Vector3> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolationCurve = null)
        {
            return pObject.AddComponent<Vector3Animation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolationCurve).StartAnimation();
        }
    }
}

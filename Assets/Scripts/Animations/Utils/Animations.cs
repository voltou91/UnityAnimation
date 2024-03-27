using System;
using Unity.VisualScripting;
using UnityEngine;
using static Com.IsartDigital.Animations.AnimationBase;

namespace Com.IsartDigital.Animations.Utils
{
    public static class Animations
    {
        public static GenericAnimation<Vector3> BeginVector3Animation(this MonoBehaviour pObject, Vector3 pInitialValue, Vector3 pFInalValue, float pDuration = 1, float pBeginAfter = 0f, TransitionType pTransitionType = TransitionType.Linear, EaseType pEaseType = EaseType.In, Action<Vector3> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolateX = null, AnimationCurve pInterpolateY = null, AnimationCurve pInterpolateZ = null)
        {
            return pObject.AddComponent<Vector3Animation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolateX, pInterpolateY, pInterpolateZ).StartAnimation();
        }
    }
}

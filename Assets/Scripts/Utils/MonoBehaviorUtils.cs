using Com.IsartDigital.Animations;
using System;
using Unity.VisualScripting;
using UnityEngine;

public static class MonoBehaviorUtils
{
    public static void BeginAnimation<T>(this MonoBehaviour pObject, T pInitialValue, T pFInalValue, float pDuration = 1, float pBeginAfter = 0f, SimpleAnimation.TransitionType pTransitionType = SimpleAnimation.TransitionType.Linear, SimpleAnimation.EaseType pEaseType = SimpleAnimation.EaseType.In, Action<T> pValueToReceive = null, Action pOnAnimationBegin = null, Action pOnAnimationEnd = null, AnimationCurve pInterpolationCurve = null)
    {
        pObject.AddComponent<SimpleAnimation>().SetupAnimation(pInitialValue, pFInalValue, pDuration, pBeginAfter, pTransitionType, pEaseType, pValueToReceive, pOnAnimationBegin, pOnAnimationEnd, pInterpolationCurve).StartAnimation();
    }
}

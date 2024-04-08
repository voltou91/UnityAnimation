using System;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public class FloatAnimation : GenericAnimation<float, float>
    {
        [Header("Interpolate curve")]
        [SerializeField] private AnimationCurve _Interpolate = AnimationCurve.Linear(0, 0, 1, 1);

        public override float Evaluate(float pTime)
        {
            return m_InitialValue + (m_FinalValue - m_InitialValue) * _Interpolate.Evaluate(m_EaseFunction(GetRatio(pTime)) * m_Duration); 
        }

        public GenericAnimation<float, float> SetupAnimation(float pInitialValue, float pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<float> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolate)
        {
            if (pInterpolate != null) _Interpolate = pInterpolate;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
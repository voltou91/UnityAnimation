using System;
using UnityEngine;

namespace Com.IsartDigital.Animations
{
    public class Vector2Animation : GenericAnimation<Vector2>
    {
        [Header("Interpolate Vector2 parameters")]
        [SerializeField] private AnimationCurve _InterpolateX = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateY = AnimationCurve.Linear(0, 0, 1, 1);

        public override Vector2 Evaluate(float pTime)
        {
            pTime = GetRatio(pTime);
            Vector2 lValue = (m_FinalValue - m_InitialValue);

            return m_InitialValue + new Vector2(
                lValue.x * _InterpolateX.Evaluate(m_EaseFunction(pTime)), 
                lValue.y * _InterpolateY.Evaluate(m_EaseFunction(pTime))
                );
        }

        public GenericAnimation<Vector2> SetupAnimation(Vector2 pInitialValue, Vector2 pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<Vector2> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateX, AnimationCurve pInterpolateY)
        {
            if (pInterpolateX != null) _InterpolateX = pInterpolateX;
            if (pInterpolateY != null) _InterpolateY = pInterpolateY;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
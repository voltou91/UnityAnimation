using System;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public class Vector2Animation : GenericAnimation<Vector2, Vector2>
    {
        [Header("Interpolate Vector2 parameters")]
        [SerializeField] private AnimationCurve _InterpolateX = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateY = AnimationCurve.Linear(0, 0, 1, 1);

        public override Vector2 Evaluate(float pTime)
        {
            Vector2 lValue = (m_FinalValue - m_InitialValue);
            float lEaseValue = m_EaseFunction(GetRatio(pTime)) * m_Duration;

            return m_InitialValue + new Vector2(
                lValue.x * _InterpolateX.Evaluate(lEaseValue), 
                lValue.y * _InterpolateY.Evaluate(lEaseValue)
                );
        }

        public GenericAnimation<Vector2, Vector2> SetupAnimation(Vector2 pInitialValue, Vector2 pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<Vector2> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateX, AnimationCurve pInterpolateY)
        {
            if (pInterpolateX != null) _InterpolateX = pInterpolateX;
            if (pInterpolateY != null) _InterpolateY = pInterpolateY;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
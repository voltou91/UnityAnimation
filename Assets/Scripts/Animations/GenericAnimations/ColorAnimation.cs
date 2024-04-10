using System;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public class ColorAnimation : GenericAnimation<Color, Color>
    {
        [Header("Interpolate Vector3 parameters")]
        [SerializeField] private AnimationCurve _InterpolateR = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateG = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateB = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateA = AnimationCurve.Linear(0, 0, 1, 1);

        public override Color Evaluate(float pTime)
        {
            Color lValue = (m_FinalValue - m_InitialValue);

            return m_InitialValue + new Color(
                lValue.r * _InterpolateR.Evaluate(pTime), 
                lValue.g * _InterpolateG.Evaluate(pTime), 
                lValue.b * _InterpolateB.Evaluate(pTime),
                lValue.a * _InterpolateA.Evaluate(pTime)
                );
        }

        public GenericAnimation<Color, Color> SetupAnimation(Color pInitialValue, Color pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<Color> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateR, AnimationCurve pInterpolateG, AnimationCurve pInterpolateB, AnimationCurve pInterpolateA)
        {
            if (pInterpolateR != null) _InterpolateR = pInterpolateR;
            if (pInterpolateG != null) _InterpolateG = pInterpolateB;
            if (pInterpolateB != null) _InterpolateB = pInterpolateG;
            if (pInterpolateA != null) _InterpolateA = pInterpolateA;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
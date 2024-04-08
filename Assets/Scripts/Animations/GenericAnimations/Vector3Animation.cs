using System;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public class Vector3Animation : GenericAnimation<Vector3, Vector3>
    {
        [Header("Interpolate Vector3 parameters")]
        [SerializeField] private AnimationCurve _InterpolateX = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateY = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateZ = AnimationCurve.Linear(0, 0, 1, 1);

        public override Vector3 Evaluate(float pTime)
        {
            Vector3 lValue = (m_FinalValue - m_InitialValue);
            float lEaseValue = m_EaseFunction(GetRatio(pTime)) * m_Duration;

            return m_InitialValue + new Vector3(
                lValue.x * _InterpolateX.Evaluate(lEaseValue), 
                lValue.y * _InterpolateY.Evaluate(lEaseValue), 
                lValue.z * _InterpolateZ.Evaluate(lEaseValue)
                );
        }

        public GenericAnimation<Vector3, Vector3> SetupAnimation(Vector3 pInitialValue, Vector3 pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<Vector3> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateX, AnimationCurve pInterpolateY, AnimationCurve pInterpolateZ)
        {
            if (pInterpolateX != null) _InterpolateX = pInterpolateX;
            if (pInterpolateY != null) _InterpolateY = pInterpolateY;
            if (pInterpolateZ != null) _InterpolateZ = pInterpolateZ;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
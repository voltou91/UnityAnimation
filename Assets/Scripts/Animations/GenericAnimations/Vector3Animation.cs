using System;
using UnityEngine;

namespace Com.IsartDigital.Animations
{
    public class Vector3Animation : GenericAnimation<Vector3>
    {
        [Header("Interpolate Vector3 parameters")]
        [SerializeField] private AnimationCurve _InterpolateX = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateY = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateZ = AnimationCurve.Linear(0, 0, 1, 1);

        public override Vector3 Evaluate(float pTime)
        {
            pTime = GetRatio(pTime);
            Vector3 lValue = (m_FinalValue - m_InitialValue);

            return m_InitialValue + new Vector3(
                lValue.x * _InterpolateX.Evaluate(m_EaseFunction(pTime)), 
                lValue.y * _InterpolateY.Evaluate(m_EaseFunction(pTime)), 
                lValue.z * _InterpolateZ.Evaluate(m_EaseFunction(pTime))
                );
        }

        public GenericAnimation<Vector3> SetupAnimation(Vector3 pInitialValue, Vector3 pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<Vector3> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateX, AnimationCurve pInterpolateY, AnimationCurve pInterpolateZ)
        {
            _InterpolateX = pInterpolateX;
            _InterpolateY = pInterpolateX;
            _InterpolateZ = pInterpolateZ;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
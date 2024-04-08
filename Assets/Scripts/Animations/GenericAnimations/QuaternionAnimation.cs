using System;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public class QuaternionAnimation : GenericAnimation<Vector3, Quaternion>
    {
        [Header("Interpolate quaternion parameters")]
        [SerializeField] private AnimationCurve _InterpolateX = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateY = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateZ = AnimationCurve.Linear(0, 0, 1, 1);

        public override Quaternion Evaluate(float pTime)
        {
            Vector3 lValue = (m_FinalValue - m_InitialValue);
            float lEaseValue = m_EaseFunction(GetRatio(pTime)) * m_Duration;

            print(lEaseValue);
            return Quaternion.Euler(
                lValue.x * _InterpolateX.Evaluate(lEaseValue),
                lValue.y * _InterpolateY.Evaluate(lEaseValue),
                lValue.z * _InterpolateZ.Evaluate(lEaseValue)
                ) * Quaternion.Euler(m_InitialValue);
        }

        public GenericAnimation<Vector3, Quaternion> SetupAnimation(Vector3 pInitialValue, Vector3 pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<Quaternion> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateX, AnimationCurve pInterpolateY, AnimationCurve pInterpolateZ)
        {
            if (pInterpolateX != null) _InterpolateX = pInterpolateX;
            if (pInterpolateY != null) _InterpolateY = pInterpolateY;
            if (pInterpolateZ != null) _InterpolateZ = pInterpolateZ;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
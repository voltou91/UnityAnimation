using System;
using UnityEngine;

namespace Com.IsartDigital.Animations
{
    public class QuaternionAnimation : GenericAnimation<Quaternion>
    {
        [Header("Interpolate Vector3 parameters")]
        [SerializeField] private AnimationCurve _InterpolateX = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateY = AnimationCurve.Linear(0, 0, 1, 1);
        [SerializeField] private AnimationCurve _InterpolateZ = AnimationCurve.Linear(0, 0, 1, 1);

        public override Quaternion Evaluate(float pTime)
        {
            pTime = GetRatio(pTime);
            Quaternion lValue = (m_FinalValue * m_InitialValue);
          
            return Quaternion.Euler(
                lValue.x * _InterpolateX.Evaluate(m_EaseFunction(pTime)),
                lValue.y * _InterpolateY.Evaluate(m_EaseFunction(pTime)),
                lValue.z * _InterpolateZ.Evaluate(m_EaseFunction(pTime))
                ) * m_InitialValue;
        }

        public GenericAnimation<Quaternion> SetupAnimation(Quaternion pInitialValue, Quaternion pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<Quaternion> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateX, AnimationCurve pInterpolateY, AnimationCurve pInterpolateZ)
        {
            _InterpolateX = pInterpolateX;
            _InterpolateY = pInterpolateY;
            _InterpolateZ = pInterpolateZ;
            SetupAnimation(pInitialValue, pFinalValue, pDuration, pBeginAfter, pTransitionType, pEase, pObject, pOnAnimationBegin, pOnAnimationEnd);

            return this;
        }
    }
}
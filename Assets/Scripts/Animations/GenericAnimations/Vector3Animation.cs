using System;
using UnityEngine;

namespace Com.IsartDigital.Animations
{
    public class Vector3Animation : SimpleAnimationGeneric<Vector3>
    {
        [Header("Interpolate Vector3 parameters")]
        [SerializeField] private bool _InterpolateX = false;
        [SerializeField] private bool _InterpolateY = false;
        [SerializeField] private bool _InterpolateZ = false;

        protected override Vector3 Evaluate(float pTime)
        {
            Vector3 lValue = (m_FinalValue - m_InitialValue);
            float lInterpolate = m_InterpolationCurve.Evaluate(m_EaseFunction(pTime));
            return m_InitialValue + new Vector3(_InterpolateX ? lValue.x * lInterpolate : lValue.x * pTime, _InterpolateY ? lValue.y * lInterpolate : lValue.y * pTime, _InterpolateZ ? lValue.z * lInterpolate : lValue.z * pTime);
        }

        private void OnDrawGizmosSelected()
        {
           
            Vector3 lLastPos = m_InitialValue;
            Vector3 lNextPos;
            for (float i = 0f; i < 1f; i += 0.01f)
            {
                Gizmos.color = Color.HSVToRGB(1,i,1);
                lNextPos = Evaluate(i);
                Gizmos.DrawLine(lLastPos, lNextPos);
                lLastPos = lNextPos;
            }
        }
    }
}

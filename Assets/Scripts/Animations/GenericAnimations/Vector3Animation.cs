using UnityEngine;

namespace Com.IsartDigital.Animations
{
    public class Vector3Animation : GenericAnimation<Vector3>
    {
        [Header("Interpolate Vector3 parameters")]
        [SerializeField] private bool _InterpolateX = false;
        [SerializeField] private bool _InterpolateY = false;
        [SerializeField] private bool _InterpolateZ = false;

        public override Vector3 Evaluate(float pTime)
        {
            pTime = GetRatio(pTime);
            Vector3 lValue = (m_FinalValue - m_InitialValue);
            float lInterpolate = m_InterpolationCurve.Evaluate(m_EaseFunction(pTime));
            return m_InitialValue + new Vector3(_InterpolateX ? lValue.x * lInterpolate : lValue.x * m_EaseFunction(pTime), _InterpolateY ? lValue.y * lInterpolate : lValue.y * m_EaseFunction(pTime), _InterpolateZ ? lValue.z * lInterpolate : lValue.z * m_EaseFunction(pTime));
        }
    }
}

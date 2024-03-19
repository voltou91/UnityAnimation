using UnityEngine;
using Com.IsartDigital.Animations;

public class Test : MonoBehaviour
{
    [SerializeField] private AnimationCurve _Curve;
    [SerializeField] private SimpleAnimation.EaseType easeType;
    [SerializeField] private SimpleAnimation.TransitionType transitionType;

    [Header("Time")]
    [Range(0, 5)]
    [SerializeField] private float _BeginAfter = 0;
    [Range(1, 10)]
    [SerializeField] private float _Duration = 5;

    void Start()
    {
        BeginAnim();
    }

    private void BeginAnim()
    {
        this.BeginAnimation(Vector3.zero, Vector3.forward * 10, _Duration, _BeginAfter, transitionType, easeType, x => transform.position = x, pOnAnimationEnd: () => BeginAnim(), pInterpolationCurve: _Curve);
    }
}

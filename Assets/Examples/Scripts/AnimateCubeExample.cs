using Com.IsartDigital.Animations.Utils;
using UnityEngine;
using static Com.IsartDigital.Animations.AnimationBase;

namespace Com.IsartDigital.Animations.Examples
{
    public class AnimateCubeExample : MonoBehaviour
    {
        public enum AnimationMode
        {
            Simple,
            WithTransitionAndEase,
            Loop
        }

        [SerializeField] private AnimationMode _AnimationMode = AnimationMode.Simple;
        [SerializeField] private float _AnimationDuration = 3.5f;
        [SerializeField] private float _AnimationStartDelay = 0.5f;
        [SerializeField] private TransitionType _TransitionType = TransitionType.Linear;
        [SerializeField] private EaseType _EaseType = EaseType.In;
        void Start()
        {
            switch (_AnimationMode)
            {
                case AnimationMode.Simple:
                    this.BeginAnimation(Vector3.zero, Vector3.forward * 10, _AnimationDuration, _AnimationStartDelay, pValueToReceive: x => transform.position = x);
                    break;
                case AnimationMode.WithTransitionAndEase:
                    this.BeginAnimation(Vector3.zero, Vector3.forward * 10, _AnimationDuration, _AnimationStartDelay, _TransitionType, _EaseType, x => transform.position = x);
                    break;
                case AnimationMode.Loop:
                    BeginNewAnimationLoop();
                    break;
                default:
                    break;
            }
        }

        private void BeginNewAnimationLoop()
        {
            this.BeginAnimation(Vector3.zero, Vector3.forward * 10, _AnimationDuration, _AnimationStartDelay, _TransitionType, _EaseType, x => transform.position = x, () => print("Animation begin !"), BeginNewAnimationLoop);
        }
    }
}

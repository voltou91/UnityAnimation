using UnityEngine;

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
        [SerializeField] private Vector3Animation.TransitionType _TransitionType = Vector3Animation.TransitionType.Linear;
        [SerializeField] private Vector3Animation.EaseType _EaseType = Vector3Animation.EaseType.In;
        void Start()
        {
            switch (_AnimationMode)
            {
                case AnimationMode.Simple:
                    this.BeginVector3Animation(Vector3.zero, Vector3.forward * 10, _AnimationDuration, _AnimationStartDelay, pValueToReceive: x => transform.position = x);
                    break;
                case AnimationMode.WithTransitionAndEase:
                    this.BeginVector3Animation(Vector3.zero, Vector3.forward * 10, _AnimationDuration, _AnimationStartDelay, _TransitionType, _EaseType, x => transform.position = x);
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
            this.BeginVector3Animation(Vector3.zero, Vector3.forward * 10, _AnimationDuration, _AnimationStartDelay, _TransitionType, _EaseType, x => transform.position = x, () => print("Animation begin !"), BeginNewAnimationLoop);
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.IsartDigital.Animations
{
    public class SimpleAnimation : MonoBehaviour
    {
        private Action<object> _ValueToSend;
        private Action _OnAnimationBegin;
        private Action _OnAnimationEnd;

        private dynamic _InitialValue;
        private dynamic _FinalValue;
        
        private float _Duration;
        private float _ElapsedTime;
        private bool _IsDestroyed = false;

        private TransitionType _TransitionType = TransitionType.Linear;
        private AnimationCurve _InterpolationCurve = AnimationCurve.Linear(0, 0, 1, 1);
        private Func<float, float> _SelectedInterpolateFunction = _InterpolateFunctions[TransitionType.Linear];


        private static Dictionary<TransitionType, Func<float, float>> _InterpolateFunctions = new Dictionary<TransitionType, Func<float, float>>()
        {
            { TransitionType.Linear, AnimationsRegistery.Linear },
            { TransitionType.Sine, AnimationsRegistery.Sine },
            { TransitionType.Quint, AnimationsRegistery.Quint },
            { TransitionType.Quart, AnimationsRegistery.Quart },
            { TransitionType.Quad, AnimationsRegistery.Quad },
            { TransitionType.Expo, AnimationsRegistery.Expo },
            { TransitionType.Elastic, AnimationsRegistery.Elastic },
            { TransitionType.Cubic, AnimationsRegistery.Cubic },
            { TransitionType.Circ, AnimationsRegistery.Circ },
            { TransitionType.Bounce, AnimationsRegistery.Bounce },
            { TransitionType.Back, AnimationsRegistery.Back }
        };

        public enum TransitionType
        {
            Linear,
            Sine,
            Quint,
            Quart,
            Quad,
            Expo,
            Elastic,
            Cubic,
            Circ,
            Bounce,
            Back
        };

        public enum EaseType
        {
            In,
            Out,
            InOut,
            OutIn
        };

        public SimpleAnimation SetupAnimation<T>(T pInitialValue, T pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<T> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd, AnimationCurve pInterpolateCurve)
        {
            _ValueToSend = (x) => pObject?.Invoke((T)x);
            _OnAnimationBegin = pOnAnimationBegin;
            _OnAnimationEnd = pOnAnimationEnd;
            _InitialValue = pInitialValue;
            _FinalValue = pFinalValue;
            _Duration = Mathf.Abs(pDuration);
            _ElapsedTime = Mathf.Abs(pBeginAfter);
            _TransitionType = pTransitionType;
            if (pInterpolateCurve != null) _InterpolationCurve = pInterpolateCurve;

            switch (pEase)
            {
                case EaseType.In:
                    _SelectedInterpolateFunction = In;
                    break;
                case EaseType.Out:
                    _SelectedInterpolateFunction = Out;
                    break;
                case EaseType.InOut:
                    _SelectedInterpolateFunction = InOut;
                    break;
                case EaseType.OutIn:
                    _SelectedInterpolateFunction = OutIn;
                    break;
                default:
                    break;
            }

            return this;
        }

        private IEnumerator<object> Animate()
        {
            yield return new WaitForSeconds(_ElapsedTime);
            _ElapsedTime = 0;

            while (!_IsDestroyed)
            {
                if (_ElapsedTime > _Duration)
                {
                    _ValueToSend?.Invoke(_FinalValue);
                    StopAnimation();
                    yield return null;
                }

                _ValueToSend?.Invoke(_InitialValue + (_FinalValue - _InitialValue) * _InterpolationCurve.Evaluate(_SelectedInterpolateFunction(_ElapsedTime / _Duration)));
                _ElapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public void StartAnimation()
        {
            if (_IsDestroyed) return;
            StopAllCoroutines();

            StartCoroutine(Animate());
            _OnAnimationBegin?.Invoke();
        }

        public void StopAnimation()
        {
            if (_IsDestroyed) return;
            _IsDestroyed = true;
            StopAllCoroutines();

            _OnAnimationEnd?.Invoke();
            Destroy(this);
        }

        private float In(float pTime) => _InterpolateFunctions[_TransitionType](pTime);

        /// <summary>
        /// Reverse the "In" function
        /// </summary>
        /// <param name="pTime"></param>
        private float Out(float pTime) => 1 - In(1 - pTime);
        

        /// <summary>
        /// Play the "In" transition for the first half of the time then play the "Out" transition
        /// </summary>
        /// <param name="pTime"></param>
        private float InOut(float pTime) => pTime > 0.5f ? 0.5f * Out(pTime * 2 - 1) + 0.5f : 0.5f * In(pTime * 2);
        

        /// <summary>
        /// Play the "Out" transition for the first half of the time then play the "In" transition
        /// </summary>
        /// <param name="pTime"></param>
        /// <returns></returns>
        private float OutIn(float pTime) => pTime > 0.5f ? 0.5f * In(pTime * 2 - 1) + 0.5f : 0.5f * Out(pTime * 2);
    }
}

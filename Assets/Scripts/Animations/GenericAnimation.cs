using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Com.IsartDigital.Animations
{
    public class GenericAnimation<T> : AnimationBase
    {
        [Header("Values")]
        [SerializeField] protected T m_InitialValue;
        [SerializeField] protected T m_FinalValue;


        [Header("Events")]
        public UnityEvent<T> OnValueUpdated = new UnityEvent<T>();
        public UnityEvent OnAnimationBegin = new UnityEvent();
        public UnityEvent OnAnimationEnd = new UnityEvent();

        public T InitialValue { get => m_InitialValue; private set => m_InitialValue = value; }
        public T FinalValue { get => m_FinalValue; private set => m_FinalValue = value; }

        public GenericAnimation<T> SetupAnimation(T pInitialValue, T pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<T> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd)
        {
            OnValueUpdated.AddListener((x) => pObject?.Invoke((T)x));
            OnAnimationBegin.AddListener(() => pOnAnimationBegin?.Invoke());
            OnAnimationEnd.AddListener(() => pOnAnimationEnd?.Invoke());
            m_InitialValue = pInitialValue;
            m_FinalValue = pFinalValue;
            m_Duration = Mathf.Abs(pDuration);
            m_StartDelay = Mathf.Abs(pBeginAfter);
            m_TransitionType = pTransitionType;
            m_EaseType = pEase;

            return this;
        }

        protected virtual void Start()
        {
            if (m_Duration <= 0)
            {
                StopAnimation();
                return;
            }
            m_ElapsedTime = Mathf.Abs(m_StartDelay);

            if (m_StartOnEnable) StartAnimation();
        }

        public GenericAnimation<T> StartAnimation()
        {
            if (m_IsDestroyed) return this;
            StopAllCoroutines();

            StartCoroutine(Animate());
            OnAnimationBegin?.Invoke();
            return this;
        }

        public void StopAnimation()
        {
            if (m_IsDestroyed) return;
            m_IsDestroyed = true;
            StopAllCoroutines();

            OnAnimationEnd?.Invoke();
            Destroy(this);
        }

        protected IEnumerator<object> Animate()
        {
            yield return new WaitForSeconds(m_ElapsedTime);
            m_ElapsedTime = 0;

            while (!m_HasFinised)
            {
                if (m_ElapsedTime > m_Duration)
                {
                    m_HasFinised = true;
                    OnValueUpdated?.Invoke(m_FinalValue);
                    StopAnimation();
                    yield return null;
                }

                OnValueUpdated?.Invoke(Evaluate(m_ElapsedTime));
                m_ElapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public virtual T Evaluate(float pTime) => default;
    }
}
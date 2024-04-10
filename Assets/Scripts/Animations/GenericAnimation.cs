using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public abstract class GenericAnimation<T, U> : AnimationBase
    {
        [Header("Values")]
        [SerializeField] protected T m_InitialValue;
        [SerializeField] protected T m_FinalValue;


        [Header("Events")]
        public UnityEvent<U> OnValueUpdated = new UnityEvent<U>();
        public UnityEvent OnAnimationBegin = new UnityEvent();
        public UnityEvent OnAnimationEnd = new UnityEvent();

        public T InitialValue { get => m_InitialValue; private set => m_InitialValue = value; }
        public T FinalValue { get => m_FinalValue; private set => m_FinalValue = value; }

        public GenericAnimation<T, U> SetupAnimation(T pInitialValue, T pFinalValue, float pDuration, float pBeginAfter, TransitionType pTransitionType, EaseType pEase, Action<U> pObject, Action pOnAnimationBegin, Action pOnAnimationEnd)
        {
            if (m_IsInitialised) return this;

            m_IsInitialised = true;
            OnValueUpdated.AddListener((x) => pObject?.Invoke((U)x));
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

        public override void StartAnimation()
        {
            if (m_IsDestroyed) return;
            StopAllCoroutines();

            StartCoroutine(Animate());
            OnAnimationBegin?.Invoke();
            return;
        }

        public override void StopAnimation()
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

            while (!m_HasFinished)
            {
                if (m_ElapsedTime > m_Duration)
                {
                    m_HasFinished = true;
                    OnValueUpdated?.Invoke(Evaluate(m_Duration));
                    StopAnimation();
                    yield return null;
                }

                OnValueUpdated?.Invoke(Evaluate(m_EaseFunction(GetRatio(m_ElapsedTime)) * m_Duration));
                m_ElapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        public abstract U Evaluate(float pTime);
    }
}
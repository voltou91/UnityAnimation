using System;
using System.Collections.Generic;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    public abstract class AnimationBase : MonoBehaviour
    {
        protected bool m_HasFinished = false;
        protected bool m_IsInitialised = false;
        protected bool m_IsDestroyed = false;
        protected float m_ElapsedTime = 0;

        [SerializeField] protected float m_Duration = 1;

        [Header("Start")]
        [SerializeField] protected bool m_StartOnEnable = true;
        [SerializeField] protected float m_StartDelay = 0f;


        [Header("Transitions type")]
        [SerializeField] protected EaseType m_EaseType = EaseType.In;
        [SerializeField] protected TransitionType m_TransitionType = TransitionType.Linear;

        public const string DURATION_NAME = nameof(m_Duration);
        public float StartDelay { get => m_StartDelay; private set => m_StartDelay = value; }
        public float Duration { get => m_Duration; private set => m_Duration = value; }

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

        protected static Dictionary<TransitionType, Func<float, float>> m_InterpolateFunctions = new Dictionary<TransitionType, Func<float, float>>()
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

        protected Func<float, float> m_EaseFunction
        {
            get
            {
                switch (m_EaseType)
                {
                    case EaseType.Out:
                        return Out;
                    case EaseType.InOut:
                        return InOut;
                    case EaseType.OutIn:
                        return OutIn;
                    default:
                        return In;
                }
            }
        }

        public abstract void StartAnimation();
        public abstract void StopAnimation();

        public bool HasFinished() => m_HasFinished;

        private float In(float pTime) => m_InterpolateFunctions[m_TransitionType](pTime);

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

        public float GetRatio(float pTime) => pTime / m_Duration;
    }
}
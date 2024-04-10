using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEditorInternal;

namespace Com.IsartDigital.Animations.Utils
{
    public class DebugAnimations : MonoBehaviour
    {
        [Range(0, 250)] public uint NumberOfPointPrecision = 100;

        private const string DEBUGGER_NAME = "Animation Debugger";
        private const string GLOBAL_POSITION = "set_position";
        private const string LOCAL_POSITION = "set_localPosition";

        public GameObject AnimationDebugger { get; private set; }

        private List<AnimationBase> _Animations = new List<AnimationBase>();
        public void RegisterAnimations(List<AnimationBase> pAnimations) => _Animations = pAnimations;

        public void RegisterAnimation(AnimationBase pAnimation) => _Animations.Add(pAnimation);

        private void Start() => Destroy();
        
        public void Destroy()
        {
            DestroyImmediate(GetOrCreateAnimationDebugger());
            DestroyImmediate(this);
        }

        private bool _IsInit = false;
        private float _ElapsedTime;
        private float _MaxAnimationTime = 0;
        private DateTime _LastTime;
        private void OnDrawGizmosSelected()
        {
            if (AnimationDebugger == null) AnimationDebugger = GetOrCreateAnimationDebugger();

            if (Application.isPlaying || _Animations == null || _Animations.Count == 0) return;
            
            if (!_IsInit)
            {
                _IsInit = true;
                _LastTime = DateTime.Now;
                _ElapsedTime = -_Animations.Max(x => x.StartDelay);
                _MaxAnimationTime = _Animations.Max(x => x.Duration);
            }

            if (_ElapsedTime > 0)
            {
                IEnumerable<AnimationBase> lAnimations = _Animations.Where(x => x.Duration > _ElapsedTime);
                foreach (AnimationBase lAnimation in lAnimations) EvaluateAnimation(lAnimation);
            }

            _ElapsedTime += (DateTime.Now - _LastTime).Milliseconds * 0.001f;
            _LastTime = DateTime.Now;
            if (_ElapsedTime > _MaxAnimationTime) _IsInit = false;
        }

        private GameObject GetOrCreateAnimationDebugger()
        {
            InternalEditorUtility.AddTag(DEBUGGER_NAME);
            GameObject lGameObject = GetComponentsInChildren<Transform>().Where(x => x.CompareTag(DEBUGGER_NAME)).FirstOrDefault()?.gameObject ?? CreateDebugger();
            
            lGameObject.transform.parent = transform;

            return lGameObject;
        }

        private GameObject CreateDebugger()
        {
            GameObject lGameObject = Instantiate(gameObject);
            lGameObject.tag = DEBUGGER_NAME;
            lGameObject.name = DEBUGGER_NAME;
            foreach (AnimationBase lComponent in lGameObject.GetComponents<AnimationBase>()) DestroyImmediate(lComponent);
            foreach (DebugAnimations lComponent in lGameObject.GetComponents<DebugAnimations>()) DestroyImmediate(lComponent);
            return lGameObject;
        }

        private void EvaluateAnimation(AnimationBase pAnimation)
        {
            dynamic dynamicAnimation = pAnimation;
            int lCount = dynamicAnimation.OnValueUpdated.GetPersistentEventCount();
            for (int i = 0; i < lCount; i++)
            {
                if (dynamicAnimation.OnValueUpdated.GetPersistentMethodName(i).Contains(GLOBAL_POSITION)) DrawLine(dynamicAnimation as Vector3Animation);
                if (dynamicAnimation.OnValueUpdated.GetPersistentMethodName(i).Contains(LOCAL_POSITION)) DrawLine(dynamicAnimation as Vector3Animation, true);
            }

            InvokeOnTargets(dynamicAnimation.OnValueUpdated, AnimationDebugger, dynamicAnimation.Evaluate(_ElapsedTime));
        }

        public void InvokeOnTargets<T>(UnityEvent<T> pUnityEvent, GameObject ptarget, T pValue)
        {
            MethodInfo lMethodInfo;
            for (int i = 0; i < pUnityEvent.GetPersistentEventCount(); i++)
            {
                bool lIsString = GetGenericMethod<T>(ptarget, pUnityEvent.GetPersistentMethodName(i), out lMethodInfo);
                lMethodInfo?.Invoke(lMethodInfo.DeclaringType.IsSubclassOf(typeof(Component)) ? ptarget.GetComponent(lMethodInfo.DeclaringType) : ptarget, new object[] { lIsString ? pValue.ToString() : pValue });
            }
        }

        private bool GetGenericMethod<T>(GameObject pTarget, string pMethodName, out MethodInfo pMethodInfo)
        {
            IEnumerable<MethodInfo> lMethods;
            ParameterInfo[] lParameters;
            foreach (Component lComponent in pTarget.GetComponents<Component>())
            {
                lMethods = lComponent.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == pMethodName);
                foreach (MethodInfo lMethod in lMethods)
                {
                    lParameters = lMethod.GetParameters();
                    if (lParameters.Length == 1 && (lParameters[0].ParameterType == typeof(T) || lParameters[0].ParameterType == typeof(string)))
                    {
                        pMethodInfo = lMethod;
                        return lParameters[0].ParameterType == typeof(string);
                    }
                }
            }
            pMethodInfo = null;
            return false;
        }

        private void DrawLine(Vector3Animation pAnimation, bool pIsLocal = false)
        {
            Vector3 lLastPos = pAnimation.InitialValue + (pIsLocal ? transform.position : Vector3.zero);
            Vector3 lNextPos;
            float lRatio = pAnimation.Duration / NumberOfPointPrecision;
            for (float i = 0f; i <= pAnimation.Duration; i += lRatio)
            {
                Gizmos.color = Color.HSVToRGB(i * 0.1f, i * 0.5f, i);
                lNextPos = pAnimation.Evaluate(i) + (pIsLocal ? transform.position : Vector3.zero);
                Gizmos.DrawLine(lLastPos, lNextPos);
                lLastPos = lNextPos;
            }
            Gizmos.DrawLine(lLastPos, pAnimation.FinalValue + (pIsLocal ? transform.position : Vector3.zero));
        }
    }
}
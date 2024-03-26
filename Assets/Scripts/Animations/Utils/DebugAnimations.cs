using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Com.IsartDigital.Animations.Utils
{
    public class DebugAnimations : MonoBehaviour
    {
        [Range(0, 250)]
        [SerializeField] private uint _NumberOfPointPrecision = 100;

        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3 Scale { get; private set; }

        private Mesh _Mesh => GetComponent<MeshFilter>().sharedMesh;
        private Color _Color => GetComponent<MeshRenderer>().sharedMaterial.color;
        private List<AnimationBase> _Animations = new List<AnimationBase>();

        public static DebugAnimations GetOrCreate(MonoBehaviour pGameObject)
        {
            if (!pGameObject.TryGetComponent(out DebugAnimations lDebugAnimation)) lDebugAnimation = pGameObject.AddComponent<DebugAnimations>();
            
            return lDebugAnimation;
        }

        private bool _IsInit = false;
        private float _ElapsedTime;
        private float _MaxAnimationTime = 0;
        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying) return;
            
            if (!_IsInit)
            {
                _IsInit = true;
                Position = Vector3.zero;
                Rotation = Quaternion.identity;
                Scale = Vector3.one;
                _ElapsedTime = -GetComponentsInChildren<AnimationBase>().Max(x => x.StartDelay);
                _MaxAnimationTime = GetComponentsInChildren<AnimationBase>().Max(x => x.Duration);
            }

            if (_ElapsedTime > 0)
            {
                IEnumerable<AnimationBase> _Animations = GetComponentsInChildren<AnimationBase>().Where(x => x.Duration > _ElapsedTime);
                foreach (AnimationBase _Animation in _Animations)
                {
                    switch (_Animation)
                    {
                        case Vector3Animation lAnimation:
                            EvaluateVector3Animation(lAnimation);
                            break;
                    }
                }
                DrawMesh();
            }

            _ElapsedTime += Time.fixedDeltaTime;
            if (_ElapsedTime > _MaxAnimationTime) _IsInit = false;
        }

        private void EvaluateVector3Animation(Vector3Animation pAnimation)
        {
            string lMethodName;
            int lCount = pAnimation.OnValueUpdated.GetPersistentEventCount();
            for (int i = 0; i < lCount; i++)
            {
                lMethodName = pAnimation.OnValueUpdated.GetPersistentMethodName(i);
                if (lMethodName.Contains("position")) UpdatePosition(pAnimation);
                if (lMethodName.Contains("cale")) UpdateScale(pAnimation);
            }
        }

        private void DrawLine(Vector3Animation pAnimation)
        {
            Vector3 lLastPos = pAnimation.InitialValue;
            Vector3 lNextPos;
            float lRatio = pAnimation.Duration / _NumberOfPointPrecision;
            for (float i = 0f; i <= pAnimation.Duration; i += lRatio)
            {
                Gizmos.color = Color.HSVToRGB(i * 0.1f, i * 0.5f, i);
                lNextPos = pAnimation.Evaluate(i);
                Gizmos.DrawLine(lLastPos, lNextPos);
                lLastPos = lNextPos;
            }
            Gizmos.DrawLine(lLastPos, pAnimation.FinalValue);
        }

        public DebugAnimations UpdatePosition(Vector3Animation pAnimation)
        {
            DrawLine(pAnimation);
            Position = pAnimation.Evaluate(_ElapsedTime);
            return this;
        }
        public DebugAnimations UpdateRotation(Quaternion pRotation)
        {
            Rotation = pRotation;
            return this;
        }
        public DebugAnimations UpdateScale(Vector3Animation pAnimation)
        {
            Scale = pAnimation.Evaluate(_ElapsedTime);
            return this;
        }

        public void DrawMesh()
        {
            Color lColor = _Color;
            lColor.a = 0.5f;
            Gizmos.color = lColor;
            Gizmos.DrawMesh(_Mesh, Position, Quaternion.identity, Scale);
        }
    }
}
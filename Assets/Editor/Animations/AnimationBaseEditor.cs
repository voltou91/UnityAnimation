using Com.IsartDigital.Animations.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    [CustomEditor(typeof(AnimationBase), true)]
    public class AnimationBaseEditor : Editor
    {
        private AnimationBase _Animation;

        private void OnEnable() => _Animation = (AnimationBase)target;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            GUILayout.Space(DebugAnimationEditor.ELEMENT_DISTANCE);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Show animation simulation", GUILayout.MinHeight(DebugAnimationEditor.BUTTON_HEIGHT)) && !_Animation.TryGetComponent(out DebugAnimations lDebugAnimation))
            {
                _Animation.AddComponent<DebugAnimations>().RegisterAnimation(_Animation);
            }

            SerializedProperty lDuration = serializedObject.FindProperty(AnimationBase.DURATION_NAME);
            lDuration.floatValue = Mathf.Max(0, lDuration.floatValue);

            UpdateCurves();
            serializedObject.ApplyModifiedProperties();
        }

        private const float MULTIPLICATOR = 1.5f;
        private void UpdateCurves()
        {
            SerializedProperty lProperty = serializedObject.GetIterator();

            AnimationCurve lCurve;
            while (lProperty.Next(true))
            {
                if ((lCurve = lProperty.animationCurveValue) != null)
                {
                    if (_Animation.Duration > 0 && lCurve.keys.Length < 3)
                    {
                        lCurve.AddKey(new Keyframe(0, 0));
                        lCurve.AddKey(new Keyframe(_Animation.Duration, 1));
                        lCurve.AddKey(new Keyframe(_Animation.Duration * MULTIPLICATOR, MULTIPLICATOR));
                    }

                    UpdateKeyFrame(lCurve, 0, new Keyframe(0, 0));
                    UpdateKeyFrame(lCurve, lCurve.keys.Length - 2, new Keyframe(_Animation.Duration, 1));
                    UpdateKeyFrame(lCurve, lCurve.keys.Length - 1, new Keyframe(_Animation.Duration * MULTIPLICATOR, MULTIPLICATOR));

                    lProperty.animationCurveValue = lCurve;
                }
            }
        }

        private void UpdateKeyFrame(AnimationCurve pCurve, int pIndex, Keyframe pKeyFrame)
        {
            pCurve.MoveKey(pIndex, pKeyFrame);
            pCurve.SmoothTangents(pIndex, 0);
        }
    }
}

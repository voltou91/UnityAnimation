using Com.IsartDigital.Animations.Utils;
using System.Collections.Generic;
using System.Linq;
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
            List<Keyframe> lKeys;
            while (lProperty.Next(true))
            {
                if ((lCurve = lProperty.animationCurveValue) != null)
                {
                    lKeys = lCurve.keys.ToList();

                    if (_Animation.Duration > 0) while (lKeys.Count < 3) lKeys.Add(new Keyframe());
                    
                    UpdateKeyFrame(lCurve, 0, lKeys, new Keyframe(0, 0));
                    UpdateKeyFrame(lCurve, lKeys.Count - 2, lKeys, new Keyframe(_Animation.Duration, 1));
                    UpdateKeyFrame(lCurve, lKeys.Count - 1, lKeys, new Keyframe(_Animation.Duration * MULTIPLICATOR, MULTIPLICATOR));

                    lCurve.keys = lKeys.ToArray();
                    lProperty.animationCurveValue = lCurve;
                }
            }
        }

        private void UpdateKeyFrame(AnimationCurve pCurve, int pIndex, List<Keyframe> pKeys, Keyframe pKeyFrame)
        {
            if (pIndex >= pCurve.keys.Length) return;

            pKeyFrame.inTangent = 1f/_Animation.Duration;
            pKeyFrame.outTangent = 1f/_Animation.Duration;
            pKeys[pIndex] = pKeyFrame;
        }
    }
}

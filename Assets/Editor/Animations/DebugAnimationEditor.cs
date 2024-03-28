using Com.IsartDigital.Animations.Utils;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Com.IsartDigital.Animations
{
    [CustomEditor(typeof(DebugAnimations), true)]
    public class DebugAnimationEditor : Editor
    {
        private DebugAnimations _DebugAnimation;
        public const float BUTTON_HEIGHT = 40;
        public const float ELEMENT_DISTANCE = 15;
        public override void OnInspectorGUI()
        {
            _DebugAnimation = (DebugAnimations)target;

            GUILayout.Space(ELEMENT_DISTANCE);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Simulate all animations", GUILayout.MinHeight(BUTTON_HEIGHT)))
            {
                _DebugAnimation.RegisterAnimations(_DebugAnimation.GetComponentsInChildren<AnimationBase>().ToList());
            }

            GUILayout.Space(ELEMENT_DISTANCE);
            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Stop simulations", GUILayout.MinHeight(BUTTON_HEIGHT)))
            {
                _DebugAnimation.Destroy();
                return;
            }

            GUILayout.Space(ELEMENT_DISTANCE);
            GUI.backgroundColor = Color.white;
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DebugAnimations.NumberOfPointPrecision)));

            serializedObject.ApplyModifiedProperties();
        }
    }
}

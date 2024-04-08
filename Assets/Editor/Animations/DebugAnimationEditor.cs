using Com.IsartDigital.Animations.Utils;
using System.Linq;
using UnityEditor;
using UnityEngine;

//Author: Lilian Lafond
namespace Com.IsartDigital.Animations
{
    [CustomEditor(typeof(DebugAnimations), true)]
    public class DebugAnimationEditor : Editor
    {
        private DebugAnimations _DebugAnimation;
        public const float BUTTON_HEIGHT = 40;
        public const float ELEMENT_DISTANCE = 15;

        private void OnEnable() => _DebugAnimation = (DebugAnimations)target;
        
        public override void OnInspectorGUI()
        {
            DrawCategory(Color.green);
            if (GUILayout.Button("Simulate all animations", GUILayout.MinHeight(BUTTON_HEIGHT)))
            {
                _DebugAnimation.RegisterAnimations(_DebugAnimation.GetComponentsInChildren<AnimationBase>().ToList());
            }

            DrawCategory(Color.red);
            if (GUILayout.Button("Stop simulations", GUILayout.MinHeight(BUTTON_HEIGHT)))
            {
                _DebugAnimation.Destroy();
                return;
            }

            DrawCategory(Color.white);
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(DebugAnimations.NumberOfPointPrecision)));

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawCategory(Color pColor)
        {
            GUILayout.Space(ELEMENT_DISTANCE);
            GUI.backgroundColor = pColor;
        }
    }
}

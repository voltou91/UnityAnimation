using Com.IsartDigital.Animations.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Com.IsartDigital.Animations
{
    [CustomEditor(typeof(AnimationBase), true)]
    public class AnimationBaseEditor : Editor
    {
        private AnimationBase _Animation;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _Animation = (AnimationBase)target;
            GUILayout.Space(DebugAnimationEditor.ELEMENT_DISTANCE);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Show animation simulation", GUILayout.MinHeight(DebugAnimationEditor.BUTTON_HEIGHT)) && !_Animation.TryGetComponent(out DebugAnimations lDebugAnimation))
            {
                _Animation.AddComponent<DebugAnimations>().RegisterAnimation(_Animation);
            }
        }
    }
}

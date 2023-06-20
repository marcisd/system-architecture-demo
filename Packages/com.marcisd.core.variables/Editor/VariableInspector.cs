using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MSD.Editor
{
    [CustomEditor(typeof(Variable<>), true)]
    public class GenericVariableInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            PropertyInfo valueInfo = target.GetType().GetProperty("Value");
            object value = valueInfo?.GetValue(target);
            string valueDisplay = value != null ? value.ToString() : string.Empty;
            
            EditorGUILayout.LabelField("Runtime Value");
            GUIStyle style = new GUIStyle(EditorStyles.helpBox)
            {
                fontSize = EditorStyles.label.fontSize
            };
            EditorGUILayout.SelectableLabel(valueDisplay, style);
        }
    }
}

using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MSD.Modules.RemoteVariables.Editor
{
    [CustomEditor(typeof(RemoteVariable<>), true)]
    public class GenericRemoteVariableInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            DrawStatus();
            DrawAssignmentId();
            DrawRuntimeValue();
            DrawRemoteConfigWindowButton();
        }

        private void DrawStatus()
        {
            PropertyInfo statisInfo = target.GetType().GetProperty("Status");
            object statusProp = statisInfo?.GetValue(target);
            Status status = statusProp != null ? (Status) statusProp : Status.Error;

            Color color = status == Status.Found ? Color.green : Color.red;

            using (new GUIColorScope(color))
            {
                EditorGUILayout.LabelField("Status", status.ToString());
            }
        }

        private void DrawAssignmentId()
        {
            PropertyInfo assignmentIdInfo = target.GetType().GetProperty("AssignmentId");
            object assignmentIdProp = assignmentIdInfo?.GetValue(target);
            string assignmentIdDisplay = assignmentIdProp != null ? assignmentIdProp.ToString() : string.Empty;
            
            EditorGUILayout.LabelField("Assignment Id", assignmentIdDisplay);
        }

        private void DrawRuntimeValue()
        {
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

        private void DrawRemoteConfigWindowButton()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Open Remote Config Window"))
                {
                    RemoteConfigEditorAccessor.GetEditorWindow();
                }
            }

            
        }
    }

}

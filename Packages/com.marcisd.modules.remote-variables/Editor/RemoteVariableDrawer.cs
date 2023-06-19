using System.Reflection;
using UnityEditor;
using UnityEngine;
using MSD.Editor;

/*===============================================================
Project:	Remote Variables
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		21/04/2020 18:40
===============================================================*/

namespace MSD.Modules.RemoteVariables.Editor
{
	[CustomPropertyDrawer(typeof(RemoteVariable<>), true)]
	public class RemoteVariableDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty keyProp = property.FindPropertyRelative("_key");
			SerializedProperty fallbackValueProp = property.FindPropertyRelative("_fallbackValue");
			float keyHeight = EditorGUI.GetPropertyHeight(keyProp);

			Rect keyRect = new Rect(position) 
			{
				height = keyHeight,
			};

			Rect fallbackValueRect = new Rect(position)
			{
				y = keyRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				height = EditorGUIUtility.singleLineHeight,
			};
			
			Rect runtimeValueRect = new Rect(position) 
			{
				y = fallbackValueRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				height = EditorGUIUtility.singleLineHeight,
			};

			Rect buttonRect = new Rect(position) 
			{
				y = runtimeValueRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				height = EditorGUIUtility.singleLineHeight,
			};

			using (new EditorGUI.DisabledScope(Application.isPlaying)) 
			{
				EditorGUI.PropertyField(keyRect, keyProp);
				EditorGUI.PropertyField(fallbackValueRect, fallbackValueProp);
			}

			DrawRuntimeValue(runtimeValueRect, property);
			DrawRemoteConfigWindowButton(buttonRect);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SerializedProperty keyProp = property.FindPropertyRelative("_key");
			float height = EditorGUI.GetPropertyHeight(keyProp);
			height += EditorGUIUtility.singleLineHeight * 3;
			return height;
		}

		private void DrawRuntimeValue(Rect rect, SerializedProperty property)
		{
			Rect labelRect = new Rect(rect)
			{
				width = EditorGUIUtility.labelWidth,
			};

			Rect selectableLabelRect = new Rect(rect)
			{
				x = rect.xMin + EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing,
				width = rect.width - EditorGUIUtility.labelWidth,
			};

			EditorGUI.LabelField(labelRect, "Runtime Value");

			if (Application.isPlaying)
			{
				object instance = property.GetObjectInstance();
				PropertyInfo valueInfo = fieldInfo.FieldType.GetProperty("Value");
				PropertyInfo statusInfo = fieldInfo.FieldType.GetProperty("Status");

				object valueProp = valueInfo?.GetValue(instance);
				string valueDisplay = valueProp != null ? valueProp.ToString() : string.Empty;

				object statusProp = statusInfo?.GetValue(instance);
				Status status = statusProp != null
					? (Status) statusProp
					: Status.Error;

				if (status == Status.Found)
				{
					EditorGUI.SelectableLabel(selectableLabelRect, valueDisplay);
				}
				else
				{
					using (new GUIColorScope(Color.red))
					{
						EditorGUI.LabelField(selectableLabelRect, status.ToString());
					}
				}
			}
			else
			{
				EditorGUI.LabelField(selectableLabelRect, string.Empty);
			}
		}

		private void DrawRemoteConfigWindowButton(Rect rect)
		{
			GUIContent label = new GUIContent("Open Remote Config Window");
			Vector2 buttonSize = GUI.skin.button.CalcSize(label);

			Rect buttonRect = new Rect(rect)
			{
				x = rect.width - buttonSize.x,
				width = buttonSize.x,
			};
			
			if (GUI.Button(buttonRect, label))
			{
				RemoteConfigEditorAccessor.GetEditorWindow();
			}
		}
	}
}

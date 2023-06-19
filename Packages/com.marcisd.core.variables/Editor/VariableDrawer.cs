using System.Reflection;
using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		
===============================================================*/

namespace MSD.Editor
{
	[CustomPropertyDrawer(typeof(Variable<>), true)]
	public class VariableDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty initialValueProp = property.FindPropertyRelative("_value");

			Rect initialValueRect = new Rect(position) 
			{
				height = EditorGUI.GetPropertyHeight(initialValueProp),
			};

			Rect labelRect = new Rect(position) 
			{
				y = initialValueRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				height = EditorGUIUtility.singleLineHeight,
			};

			Rect selectableLabelRect = new Rect(position) 
			{
				x = initialValueRect.xMin + EditorGUIUtility.labelWidth + EditorGUIUtility.standardVerticalSpacing,
				y = initialValueRect.yMax + EditorGUIUtility.standardVerticalSpacing,
				width = initialValueRect.width - EditorGUIUtility.labelWidth,
				height = EditorGUIUtility.singleLineHeight,
			};

			EditorGUI.PropertyField(initialValueRect, initialValueProp);

			PropertyInfo valueInfo = fieldInfo.FieldType.GetProperty("Value");

			object instance = property.GetObjectInstance();
			object value = valueInfo?.GetValue(instance);
			string valueDisplay = value != null ? value.ToString() : string.Empty;

			EditorGUI.LabelField(labelRect, "Runtime Value");
			EditorGUI.SelectableLabel(selectableLabelRect, valueDisplay);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SerializedProperty initialValueProp = property.FindPropertyRelative("_value");
			float height = EditorGUI.GetPropertyHeight(initialValueProp);
			height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
			return height;
		}
	}
}

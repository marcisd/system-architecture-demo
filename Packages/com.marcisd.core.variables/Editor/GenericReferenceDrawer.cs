using UnityEngine;
using UnityEditor;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       15/11/2018 19:42
===============================================================*/

namespace MSD.Editor
{
	[CustomPropertyDrawer(typeof(GenericReference<,>), true)]
	public class GenericReferenceDrawer : PropertyDrawer
	{
		private static readonly string[] POPUP_OPTIONS = { "Use Constant", "Use Variable" };

		private static GUIStyle s_popupStyle;
		
		private static GUIStyle PopupStyle 
		{
			get 
			{
				if (s_popupStyle == null) 
				{
					s_popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions")) 
					{
						imagePosition = ImagePosition.ImageOnly
					};
				}
				return s_popupStyle;
			}
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			using (new EditorGUI.PropertyScope(position, label, property)) 
			{
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), label);

				SerializedProperty useConstantProp = property.FindPropertyRelative("_useConstant");
				SerializedProperty constantValueProp = property.FindPropertyRelative("_constantValue");
				SerializedProperty variableProp = property.FindPropertyRelative("_variable");

				if (useConstantProp == null || constantValueProp == null || variableProp == null) 
				{
					EditorGUI.HelpBox(position, "Improper use of GenericReferenceBase class", MessageType.Error);
					return;
				}

				Rect buttonRect = new Rect(position) 
				{
					yMin = position.yMin + PopupStyle.margin.top,
					width = PopupStyle.fixedWidth + PopupStyle.margin.right
				};
				position.xMin = buttonRect.xMax;

				using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel))
				using (EditorGUI.ChangeCheckScope changeCheck = new EditorGUI.ChangeCheckScope()) 
				{
					int result = EditorGUI.Popup(buttonRect, useConstantProp.boolValue ? 0 : 1, POPUP_OPTIONS,
						PopupStyle);
					useConstantProp.boolValue = result == 0;

					EditorGUI.PropertyField(position, useConstantProp.boolValue ? constantValueProp : variableProp,
						GUIContent.none);

					if (changeCheck.changed) 
					{
						property.serializedObject.ApplyModifiedProperties();
					}
				}
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SerializedProperty useConstantProp = property.FindPropertyRelative("_useConstant");
			SerializedProperty constantValueProp = property.FindPropertyRelative("_constantValue");

			if (useConstantProp.boolValue) 
			{
				return EditorGUI.GetPropertyHeight(constantValueProp);
			}

			return base.GetPropertyHeight(property, label);
		}
	}
}

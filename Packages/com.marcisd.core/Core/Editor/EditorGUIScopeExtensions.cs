using System;
using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		
===============================================================*/

namespace MSD
{
	public class MenuBarScope : EditorGUILayout.HorizontalScope
	{
		public MenuBarScope(params GUILayoutOption[] options) : base(EditorStyles.toolbar, options) { }
	}

	public class VerticalBoxScope : EditorGUILayout.VerticalScope
	{
		public VerticalBoxScope(params GUILayoutOption[] options) : base(EditorStyles.helpBox, options) { }
	}

	public class HorizontalBoxScope : EditorGUILayout.HorizontalScope
	{
		public HorizontalBoxScope(params GUILayoutOption[] options) : base(EditorStyles.helpBox, options) { }
	}

	public class OddEvenBoxScope : EditorGUILayout.VerticalScope
	{
		public OddEvenBoxScope(int integer, params GUILayoutOption[] options) : base(integer % 2 == 0 ? Styles.BoxOdd : Styles.BoxEven, options) { }
	}

	public class OddEvenRowScope : EditorGUILayout.HorizontalScope
	{
		public OddEvenRowScope(int integer, params GUILayoutOption[] options) : base(integer % 2 == 0 ? Styles.RowOdd : Styles.RowEven, options) { }
	}

	public class NoIndentScope : EditorGUI.IndentLevelScope
	{
		public NoIndentScope() : base(-EditorGUI.indentLevel) { }
	}

	public class IndentFromNoneScope : EditorGUI.IndentLevelScope
	{
		public IndentFromNoneScope(int indentLevel) : base(-EditorGUI.indentLevel + indentLevel) { }
	}

	public class FoldoutHeaderGroupScope : IDisposable
	{
		private readonly bool _isLayout;
		private readonly Rect _innerRect;

		public bool Foldout { get; private set; }

		public Rect InnerRect {
			get {
				if (_isLayout) {
					return _innerRect;
				}
				Debugger.LogWarning("Not used in layout mode.");
				return default;
			}
		}

		#region Layout

		public FoldoutHeaderGroupScope(bool foldout, string content)
			: this(foldout, new GUIContent(content), Color.white) { }

		public FoldoutHeaderGroupScope(bool foldout, GUIContent content)
			: this(foldout, content, Color.white) { }

		public FoldoutHeaderGroupScope(bool foldout, string content, Color backgroundColor)
			: this(foldout, new GUIContent(content), backgroundColor) { }

		public FoldoutHeaderGroupScope(bool foldout, GUIContent content, Color backgroundColor)
		{
			using (new GUIBackgroundColorScope(backgroundColor)) {
				Foldout = EditorGUILayout.BeginFoldoutHeaderGroup(foldout, content);
			}
			_isLayout = true;
		}

		#endregion Layout
		#region Non-Layout

		public FoldoutHeaderGroupScope(Rect position, bool foldout, string content)
			: this(position, foldout, new GUIContent(content), Color.white, false) { }

		public FoldoutHeaderGroupScope(Rect position, bool foldout, string content, bool alignHeader)
			: this(position, foldout, new GUIContent(content), Color.white, alignHeader) { }

		public FoldoutHeaderGroupScope(Rect position, bool foldout, GUIContent content)
			: this(position, foldout, content, Color.white, false) { }

		public FoldoutHeaderGroupScope(Rect position, bool foldout, GUIContent content, bool alignHeader)
			: this(position, foldout, content, Color.white, alignHeader) { }

		public FoldoutHeaderGroupScope(Rect position, bool foldout, string content, Color backgroundColor)
			: this(position, foldout, new GUIContent(content), backgroundColor, false) { }

		public FoldoutHeaderGroupScope(Rect position, bool foldout, string content, Color backgroundColor, bool alignHeader)
			: this(position, foldout, new GUIContent(content), backgroundColor, alignHeader) { }

		public FoldoutHeaderGroupScope(Rect position, bool foldout, GUIContent content, Color backgroundColor)
			: this(position, foldout, content, backgroundColor, false) { }

		public FoldoutHeaderGroupScope(Rect position, bool foldout, GUIContent content, Color backgroundColor, bool alignHeader)
		{
			Rect headerRect = CalculateHeaderRect(position, alignHeader);

			using (new GUIBackgroundColorScope(backgroundColor)) {
				Foldout = EditorGUI.BeginFoldoutHeaderGroup(headerRect, foldout, content);
			}
			_isLayout = false;

			_innerRect = new Rect(position) {
				height = position.height - EditorGUIUtility.singleLineHeight,
				y = position.y + EditorGUIUtility.singleLineHeight,
			};
		}

		#endregion Non-Layout

		public void Dispose()
		{
			if (_isLayout) {
				EditorGUILayout.EndFoldoutHeaderGroup();
			} else {
				EditorGUI.EndFoldoutHeaderGroup();
			}
		}

		private Rect CalculateHeaderRect(Rect position, bool alignHeader)
		{
			Rect headerRect = new Rect(position) {
				height = EditorGUIUtility.singleLineHeight,
			};

			if (alignHeader) {
				headerRect.x += 10f;
				headerRect.width -= 10f;
			}

			return headerRect;
		}
	}

	public class GUIColorScope : IDisposable
	{
		private readonly Color _cachedColor;

		public GUIColorScope(Color color)
		{
			_cachedColor = GUI.color;
			GUI.color = color;
		}

		public void Dispose()
		{
			GUI.color = _cachedColor;
		}
	}

	public class GUIBackgroundColorScope : IDisposable
	{
		private readonly Color _cachedColor;

		public GUIBackgroundColorScope(Color color)
		{
			_cachedColor = GUI.backgroundColor;
			GUI.backgroundColor = color;
		}

		public void Dispose()
		{
			GUI.backgroundColor = _cachedColor;
		}
	}

	public class GUIContentColorScope : IDisposable
	{
		private readonly Color _cachedColor;

		public GUIContentColorScope(Color color)
		{
			_cachedColor = GUI.contentColor;
			GUI.contentColor = color;
		}

		public void Dispose()
		{
			GUI.contentColor = _cachedColor;
		}
	}

	public class EditorGUILabelWidthScope : IDisposable
	{
		private readonly float _cachedWidth;

		public EditorGUILabelWidthScope(float width)
		{
			_cachedWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = width;
		}

		public void Dispose()
		{
			EditorGUIUtility.labelWidth = _cachedWidth;
		}
	}

	public class EditorGUIFieldWidthScope : IDisposable
	{
		private readonly float _cachedWidth;

		public EditorGUIFieldWidthScope(float width)
		{
			_cachedWidth = EditorGUIUtility.fieldWidth;
			EditorGUIUtility.fieldWidth = width;
		}

		public void Dispose()
		{
			EditorGUIUtility.fieldWidth = _cachedWidth;
		}
	}

	public class EditorGUILabelAndFieldWidthScope : IDisposable
	{
		private readonly float _cachedLabelWidth;
		private readonly float _cachedFieldWidth;

		public EditorGUILabelAndFieldWidthScope(float labelWidth, float fieldWidth)
		{
			_cachedLabelWidth = EditorGUIUtility.labelWidth;
			_cachedFieldWidth = EditorGUIUtility.fieldWidth;

			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUIUtility.fieldWidth = fieldWidth;
		}

		public void Dispose()
		{
			EditorGUIUtility.labelWidth = _cachedLabelWidth;
			EditorGUIUtility.fieldWidth = _cachedFieldWidth;
		}
	}

	internal static class Styles
	{
		private static GUIStyle s_boxOdd;

		public static GUIStyle BoxOdd {
			get {
				if (s_boxOdd == null) {
					s_boxOdd = new GUIStyle(EditorStyles.helpBox);
					GUIStyle bg = "CN Box";
					s_boxOdd.normal.background = bg.normal.background;
				}
				return s_boxOdd;
			}
		}

		private static GUIStyle s_boxEven;

		public static GUIStyle BoxEven {
			get {
				if (s_boxEven == null) {
					s_boxEven = new GUIStyle(EditorStyles.helpBox);
					GUIStyle bg = "Box";
					s_boxEven.normal.background = bg.normal.background;
				}
				return s_boxEven;
			}
		}

		private static GUIStyle s_rowOdd;

		public static GUIStyle RowOdd {
			get {
				if (s_rowOdd == null) {
					s_rowOdd = "CN EntryBackOdd";
					s_rowOdd.padding = new RectOffset();
					s_rowOdd.margin = new RectOffset();
				}
				return s_rowOdd;
			}
		}

		private static GUIStyle s_rowEven;

		public static GUIStyle RowEven {
			get {
				if (s_rowEven == null) {
					s_rowEven = "CN EntryBackEven";
					s_rowEven.padding = new RectOffset();
					s_rowEven.margin = new RectOffset();
				}
				return s_rowEven;
			}
		}
	}
}

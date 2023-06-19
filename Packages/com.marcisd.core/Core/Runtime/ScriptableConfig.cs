using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		05/04/2019 19:59
===============================================================*/

namespace MSD
{
	public abstract class ScriptableConfig<T> : ScriptableObject
		where T : ScriptableConfig<T>
	{
		private static T s_instance;
		public static T Instance {
			get {
				if (s_instance == null) {
					string configResourcesPath = typeof(T).ToString();
					s_instance = Resources.Load<T>(configResourcesPath);

					if (s_instance == null) {
						s_instance = CreateInstance<T>();

#if UNITY_EDITOR
						CreateInstanceAsset(s_instance);
#endif
					}
				}
				return s_instance;
			}
		}

#if UNITY_EDITOR

		protected static void SelectInstance()
		{
			Selection.objects = new Object[] { Instance };
		}

		protected static void DirtyEditor()
		{
			EditorUtility.SetDirty(Instance);
		}

		private static void CreateInstanceAsset(T instance)
		{
			string assetName = $"{typeof(T)}.asset";
			string relativePath = ResourcesPath();
			string assetPath = Path.Combine(relativePath, assetName);

			CreateAsset(instance, assetPath);

			string ResourcesPath()
			{
				string fullPath = Path.Combine(Application.dataPath, "Resources");
				if (!Directory.Exists(fullPath)) { Directory.CreateDirectory(fullPath); }
				return Path.Combine("Assets", "Resources");
			}

			void CreateAsset(T instance, string path)
			{
				AssetDatabase.CreateAsset(instance, path);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
		}

#endif

	}
}

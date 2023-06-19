using System.Diagnostics;
using Debug = UnityEngine.Debug;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		14/08/2019 15:00
===============================================================*/

/*
Enable logging by adding the "MSD_DEBUG" to build settings Scripting Define Symbols.
*/

namespace MSD
{
	public static class Debugger
	{
		const string DEBUG_DEFINE = "MSD_DEBUG";

		[Conditional(DEBUG_DEFINE)]
		public static void Log(object message)
		{
			Debug.Log(message);
		}

		[Conditional(DEBUG_DEFINE)]
		public static void LogWarning(object message)
		{
			Debug.LogWarning(message);
		}

		[Conditional(DEBUG_DEFINE)]
		public static void LogError(object message)
		{
			Debug.LogError(message);
		}

		[Conditional(DEBUG_DEFINE)]
		public static void Log(string prepend, object message)
		{
			Debug.Log($"{prepend} {message}");
		}

		[Conditional(DEBUG_DEFINE)]
		public static void LogWarning(string prepend, object message)
		{
			Debug.LogWarning($"{prepend} {message}");
		}

		[Conditional(DEBUG_DEFINE)]
		public static void LogError(string prepend, object message)
		{
			Debug.LogError($"{prepend} {message}");
		}
	}
}

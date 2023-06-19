using System;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UObject = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*===============================================================
Project:	Remote Variables
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		21/04/2020 10:47
===============================================================*/

namespace MSD.Modules.RemoteVariables
{
	public class RemoteConfigFetcher : ScriptableConfig<RemoteConfigFetcher>
	{
		private static readonly string DEBUG_PREPEND = $"[{nameof(RemoteConfigFetcher)}]";

		private struct UserAttributes
		{
		}

		private struct AppAttributes
		{
		}

		[SerializeField] private bool _shouldFetchOnAppStart = true;

		[SerializeField, HideInInspector] private string _environmentName = string.Empty;

		[SerializeField, HideInInspector] private string _environmentId = string.Empty;

		public static event Action OnFetchCompleted = delegate { };
		
		public string EnvironmentName
		{
			get => _environmentName;
			private set => _environmentName = value;
		}

		public string EnvironmentId 
		{
			get => _environmentId;
			private set => _environmentId = value;
		}

		[field: NonSerialized] public string AssignmentId { get; private set; }

		[field: NonSerialized] public ConfigOrigin ConfigOrigin { get; private set; }

		[field: NonSerialized] public ConfigRequestStatus ConfigRequestStatus { get; private set; }

		public void Fetch()
		{
			_ = FetchAsync();
		}

		public async Task FetchAsync()
		{
			if (Utilities.CheckForInternetConnection())
			{
				await UnityServices.InitializeAsync();
				
				if (!AuthenticationService.Instance.IsSignedIn)
				{
					await AuthenticationService.Instance.SignInAnonymouslyAsync();
				}
			}
			
			Debugger.Log(DEBUG_PREPEND, "Start fetch!");
			RemoteConfigService.Instance.SetEnvironmentID(EnvironmentId);
			await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
		}

		internal void SetEnvironment(string name, string id)
		{
			EnvironmentName = name;
			EnvironmentId = id;
		}

		[RuntimeInitializeOnLoadMethod]
		private static void Bootstrap()
		{
			RemoteConfigService.Instance.FetchCompleted += FetchCompleted;

			if (Instance._shouldFetchOnAppStart) 
			{
				Instance.Fetch();
			}

			void FetchCompleted(ConfigResponse configResponse)
			{
				Instance.FetchCompleted(configResponse);
				OnFetchCompleted.Invoke();
			}
		}

		private void FetchCompleted(ConfigResponse configResponse)
		{
			ConfigOrigin = configResponse.requestOrigin;
			ConfigRequestStatus = configResponse.status;

			if (configResponse.status.HasFlag(ConfigRequestStatus.Success)) 
			{
				EnvironmentId = RemoteConfigService.Instance.appConfig.environmentId;
				AssignmentId = RemoteConfigService.Instance.appConfig.assignmentId;
				Debugger.Log(DEBUG_PREPEND, "Fetch completed successfully!");
			} 
			else 
			{
				Debugger.LogWarning(DEBUG_PREPEND, $"Fetch completed with status: {configResponse.status}");
			}
		}

#if UNITY_EDITOR

		[MenuItem("MSD/Config/Remote Config Fetcher")]
		internal static void ShowConfig()
		{
			SelectInstance();
		}

#endif
		
	}
}

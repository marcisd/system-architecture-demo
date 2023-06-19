using System;
using Unity.Services.RemoteConfig;
using UnityEngine;

/*===============================================================
Project:	Remote Variables
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		21/04/2020 11:27
===============================================================*/

namespace MSD.Modules.RemoteVariables
{
    [Serializable]
    public abstract class RemoteVariable<T> : IValueChangeObservable<T>,
        ISerializationCallbackReceiver
    {
        private static readonly string DEBUG_PREPEND = $"[{nameof(RemoteVariable<T>)}]";

        [SerializeField] [TextArea(2, 5)] private string _key = string.Empty;

        [SerializeField] private T _fallbackValue;

        [NonSerialized] private T _value = default;

        public event Action<T> OnValueChanged = delegate { };

        [field: NonSerialized] public string AssignmentId { get; private set; } = string.Empty;

        [field: NonSerialized] public Status Status { get; private set; } = Status.Uninitialized;

        public T Value
        {
            get
            {
                if (RemoteConfigService.Instance.requestStatus != ConfigRequestStatus.Success)
                {
                    return _fallbackValue;
                }

                if (AssignmentId != RemoteConfigService.Instance.appConfig.assignmentId)
                {
                    AssignmentId = RemoteConfigService.Instance.appConfig.assignmentId;
                    if (RemoteConfigService.Instance.appConfig.HasKey(_key))
                    {
                        Debugger.Log(DEBUG_PREPEND, $"Fetched value for key: {_key}");
                        _value = GetValue(_key);
                        Status = Status.Found;
                    }
                    else
                    {
                        Debugger.LogError(DEBUG_PREPEND, "Key not found in current config!");
                        _value = _fallbackValue;
                        Status = Status.NotFound;
                    }

                    OnValueChanged?.Invoke(_value);
                }
                
                return _value;
            }
        }

        protected abstract T GetValue(string key);

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // Nothing to do for ISerializationCallbackReceiver.OnBeforeSerialize()
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            RemoteConfigFetcher.OnFetchCompleted -= UpdateValue;
            RemoteConfigFetcher.OnFetchCompleted += UpdateValue;

            void UpdateValue()
            {
                _ = Value;
            }
        }
    }
}

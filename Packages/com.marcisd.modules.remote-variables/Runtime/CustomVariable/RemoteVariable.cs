using System;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace MSD.Modules.RemoteVariables
{
    public abstract class RemoteVariable<T> : CustomVariable<T>,
        IValueChangeObservable<T>,
        IValueChangeObservable,
        ISerializationCallbackReceiver
    {
        private static readonly string DEBUG_PREPEND = $"[{nameof(RemoteVariable<T>)}]";

        [SerializeField] [TextArea(2, 5)] private string _key = string.Empty;

        [SerializeField] private T _fallbackValue;

        [NonSerialized] private T _value = default;

        public event Action<T> OnValueChanged = delegate { };

        private event Action NonGenericOnValueChanged = delegate { };

        protected override bool IsReadonly => true;

        [field: NonSerialized] public string AssignmentId { get; private set; } = string.Empty;

        [field: NonSerialized] public Status Status { get; private set; } = Status.Uninitialized;

        protected override T GetValue()
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
                NonGenericOnValueChanged.Invoke();
            }

            return _value;
        }

        protected abstract T GetValue(string key);

        protected override void SetValue(T value) => throw new NotImplementedException();

        event Action IValueChangeObservable.OnValueChanged
        {
            add => NonGenericOnValueChanged += value;
            remove => NonGenericOnValueChanged -= value;
        }

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
                GetValue();
            }
        }
    }
}

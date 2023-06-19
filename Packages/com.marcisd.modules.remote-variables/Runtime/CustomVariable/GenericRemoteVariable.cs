using System;
using UnityEngine;

namespace MSD.Modules.RemoteVariables
{
    public abstract class GenericRemoteVariable<T> : CustomVariable<T>,
        IValueChangeObservable<T>,
        IValueChangeObservable,
        ISerializationCallbackReceiver
    {
        protected abstract RemoteVariable<T> Remote { get; }

        public event Action<T> OnValueChanged = delegate { };

        private event Action NonGenericOnValueChanged = delegate { };

        protected override bool IsReadonly => true;

        protected override T GetValue() => Remote.Value;

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
            Remote.OnValueChanged -= ValueChanged;
            Remote.OnValueChanged += ValueChanged;

            void ValueChanged(T value)
            {
                OnValueChanged.Invoke(value);
                NonGenericOnValueChanged.Invoke();
            }
        }
    }
}

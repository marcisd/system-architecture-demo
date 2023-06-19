using System;
using UnityEngine;

namespace MSD
{
    public abstract class GenericVariable<T> : CustomVariable<T>,
        IValueChangeObservable<T>,
        IValueChangeObservable,
        ISerializationCallbackReceiver
    {
        [SerializeField] protected Variable<T> _variable = new Variable<T>();

        public event Action<T> OnValueChanged = delegate { };

        private event Action NonGenericOnValueChanged = delegate { };

        public void SetValueWithoutNotify(T value) => _variable.SetValueWithoutNotify(value);

        public void Reset() => _variable.Reset();

        public void OnValidate() => _variable.Reset();

        protected override bool IsReadonly => false;

        protected override T GetValue() => _variable.Value;

        protected override void SetValue(T value) => _variable.Value = value;

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
            _variable.OnValueChanged -= ValueChanged;
            _variable.OnValueChanged += ValueChanged;

            void ValueChanged(T value)
            {
                OnValueChanged.Invoke(value);
                NonGenericOnValueChanged.Invoke();
            }
        }
    }
}

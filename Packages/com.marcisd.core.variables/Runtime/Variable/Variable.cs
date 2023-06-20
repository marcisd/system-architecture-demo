using System;
using System.Collections.Generic;
using UnityEngine;

namespace MSD
{
    public abstract class Variable<T> : CustomVariable<T>,
        IValueChangeObservable<T>,
        IValueChangeObservable
    {
        [SerializeField] private T _value = default;

        [field: NonSerialized] private T RuntimeValue { get; set; } = default;

        public event Action<T> OnValueChanged = delegate { };

        private event Action NonGenericOnValueChanged = delegate { };

        protected override bool IsReadonly => false;
        
        protected override T GetValue() => RuntimeValue;

        protected override void SetValue(T value)
        {
            if (!EqualityComparer<T>.Default.Equals(RuntimeValue, value))
            {
                RuntimeValue = value;
                OnValueChanged.Invoke(RuntimeValue);
                NonGenericOnValueChanged.Invoke();
            }
        }

        public void SetValueWithoutNotify(T value)
        {
            RuntimeValue = value;
        }

        public void Reset()
        {
            RuntimeValue = _value;
        }

        public void OnValidate()
        {
            Reset();
        }

        event Action IValueChangeObservable.OnValueChanged
        {
            add => NonGenericOnValueChanged += value;
            remove => NonGenericOnValueChanged -= value;
        }
    }
}

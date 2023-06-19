using System;
using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:41
===============================================================*/

namespace MSD
{
    [Serializable]
    public abstract class GenericReference<TValue, TCustomVariable> : ISerializationCallbackReceiver
        where TCustomVariable : CustomVariable<TValue>
    {
        [SerializeField] private bool _useConstant;

        [SerializeField] private TValue _constantValue;

        [SerializeField] private TCustomVariable _variable;

        public event Action<TValue> OnValueChanged = delegate { };

        protected GenericReference()
        {
            _useConstant = true;
            _constantValue = default;
            _variable = null;
        }

        protected GenericReference(TValue value)
        {
            _useConstant = true;
            _constantValue = value;
            _variable = null;
        }

        protected GenericReference(TCustomVariable variable)
        {
            _useConstant = false;
            _constantValue = variable != null ? variable.Value : default;
            _variable = variable;
        }

        public TValue Value => _useConstant ? _constantValue : _variable.Value;

        public static implicit operator TValue(GenericReference<TValue, TCustomVariable> reference) => reference.Value;
        
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // Nothing to do for ISerializationCallbackReceiver.OnBeforeSerialize()
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (_useConstant && _variable == null)
            {
                return;
            }

            if (_variable is IValueChangeObservable<TValue> observable)
            {
                observable.OnValueChanged -= OnVariableValueChanged;
                observable.OnValueChanged += OnVariableValueChanged;
            }

            void OnVariableValueChanged(TValue value)
            {
                OnValueChanged.Invoke(value);
            }
        }
    }
}

using UnityEngine;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       16/01/2019 14:10
===============================================================*/

namespace MSD
{
    public abstract class CustomVariable<T> : ScriptableObject
    {
        [TextArea(2, 5)] [SerializeField] private string _developerDescription;

        public T Value
        {
            get => GetValue();
            set
            {
                if (IsReadonly)
                {
                    Debugger.LogWarning("Attempted to set a read only custom variable.");
                }
                else
                {
                    SetValue(value);
                }
            }
        }

        protected abstract bool IsReadonly { get; }

        protected abstract T GetValue();

        protected abstract void SetValue(T value);

        public override string ToString() => Value.ToString();

        public static implicit operator T(CustomVariable<T> variable) => variable.Value;
    }
}
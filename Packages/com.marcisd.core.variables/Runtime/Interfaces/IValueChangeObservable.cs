using System;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		07/11/2019 13:17
===============================================================*/

namespace MSD
{
    public interface IValueChangeObservable
    {
        event Action OnValueChanged;
    }

    public interface IValueChangeObservable<out T>
    {
        event Action<T> OnValueChanged;
    }
}

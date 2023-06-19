using System;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:56
===============================================================*/

namespace MSD
{
    [Serializable]
    public sealed class FloatReference : GenericReference<float, CustomVariable<float>>
    {
        public FloatReference()
        {
        }

        public FloatReference(float value) : base(value)
        {
        }

        public FloatReference(CustomVariable<float> value) : base(value)
        {
        }
    }
}

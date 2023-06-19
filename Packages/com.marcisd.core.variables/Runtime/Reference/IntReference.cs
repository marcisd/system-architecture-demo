using System;

/*===============================================================
Project:	Core Library
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       06/11/2018 15:55
===============================================================*/

namespace MSD
{
    [Serializable]
    public sealed class IntReference : GenericReference<int, CustomVariable<int>>
    {
        public IntReference()
        {
        }

        public IntReference(int value) : base(value)
        {
        }

        public IntReference(CustomVariable<int> value) : base(value)
        {
        }
    }
}

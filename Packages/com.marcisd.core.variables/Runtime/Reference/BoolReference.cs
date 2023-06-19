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
    public sealed class BoolReference : GenericReference<bool, CustomVariable<bool>>
    {
        public BoolReference()
        {
        }

        public BoolReference(bool value) : base(value)
        {
        }

        public BoolReference(CustomVariable<bool> value) : base(value)
        {
        }
    }
}

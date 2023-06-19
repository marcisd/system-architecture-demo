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
    public sealed class StringReference : GenericReference<string, CustomVariable<string>>
    {
        public StringReference()
        {
        }

        public StringReference(string value) : base(value)
        {
        }

        public StringReference(CustomVariable<string> value) : base(value)
        {
        }
    }
}

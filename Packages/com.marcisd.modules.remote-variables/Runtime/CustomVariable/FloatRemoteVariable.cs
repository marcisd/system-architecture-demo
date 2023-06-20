using Unity.Services.RemoteConfig;
using UnityEngine;

/*===============================================================
Project:	Remote Variables
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		21/04/2020 13:34
===============================================================*/

namespace MSD.Modules.RemoteVariables
{
    [CreateAssetMenu(menuName = "MSD/Modules/Remote Variables/Float")]
    public sealed class FloatRemoteVariable : RemoteVariable<float>
    {
        protected override float GetValue(string key)
        {
            return RemoteConfigService.Instance.appConfig.GetFloat(key);
        }
    }
}

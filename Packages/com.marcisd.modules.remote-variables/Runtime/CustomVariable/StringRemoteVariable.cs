using Unity.Services.RemoteConfig;
using UnityEngine;

/*===============================================================
Project:	Remote Variables
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		21/04/2020 18:40
===============================================================*/

namespace MSD.Modules.RemoteVariables
{
    [CreateAssetMenu(menuName = "MSD/Modules/Remote Variables/String")]
    public sealed class StringRemoteVariable : RemoteVariable<string>
    {
        protected override string GetValue(string key)
        {
            return RemoteConfigService.Instance.appConfig.GetString(key);
        }
    }
}

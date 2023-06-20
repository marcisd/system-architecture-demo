using Unity.Services.RemoteConfig;
using UnityEngine;

/*===============================================================
Project:	Remote Variables
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		21/04/2020 18:38
===============================================================*/

namespace MSD.Modules.RemoteVariables
{
    [CreateAssetMenu(menuName = "MSD/Modules/Remote Variables/Int")]
    public sealed class IntRemoteVariable : RemoteVariable<int>
    {
        protected override int GetValue(string key)
        {
            return RemoteConfigService.Instance.appConfig.GetInt(key);
        }
    }
}

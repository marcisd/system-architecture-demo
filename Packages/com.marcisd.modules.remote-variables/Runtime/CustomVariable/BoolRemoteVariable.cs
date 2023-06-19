using System;
using Unity.Services.RemoteConfig;
using UnityEngine;

/*===============================================================
Project:	Remote Variables
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		21/04/2020 18:39
===============================================================*/

namespace MSD.Modules.RemoteVariables
{
    [CreateAssetMenu(menuName = "MSD/Modules/Remote Variables/Bool")]
    public sealed class BoolRemoteVariable : GenericRemoteVariable<bool>
    {
        [Serializable]
        public class BoolRemote : RemoteVariable<bool>
        {
            protected override bool GetValue(string key)
            {
                return RemoteConfigService.Instance.appConfig.GetBool(key);
            }
        }

        [SerializeField] private BoolRemote _remote;

        protected override RemoteVariable<bool> Remote => _remote;
    }
}

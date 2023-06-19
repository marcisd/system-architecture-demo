using System;
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
    public sealed class FloatRemoteVariable : GenericRemoteVariable<float>
    {
        [Serializable]
        public class FloatRemote : RemoteVariable<float>
        {
            protected override float GetValue(string key)
            {
                return RemoteConfigService.Instance.appConfig.GetFloat(key);
            }
        }

        [SerializeField] private FloatRemote _remote;

        protected override RemoteVariable<float> Remote => _remote;
    }
}

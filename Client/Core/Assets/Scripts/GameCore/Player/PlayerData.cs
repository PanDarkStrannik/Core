using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Balance/PlayerData")]
    public class PlayerData : SerializedScriptableObject
    {
        [SerializeField, ListDrawerSettings(ListElementLabelName = nameof(BasePlayerModule.ModuleName), HideRemoveButton = true)]
        private List<BasePlayerModule> _modules = new List<BasePlayerModule>();

        public IReadOnlyCollection<BasePlayerModule> Modules => _modules.ToHashSet();
    }
}

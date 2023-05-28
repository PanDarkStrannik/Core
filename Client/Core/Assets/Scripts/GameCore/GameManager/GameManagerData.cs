using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameCore
{
    [CreateAssetMenu(fileName = "New GameManagerData", menuName = "Balance/GameManagerData")]
    public class GameManagerData : SerializedScriptableObject
    {
        [SerializeField, ListDrawerSettings(ListElementLabelName = nameof(BaseGameManagerModule.ModuleName), HideRemoveButton = true)]
        private List<BaseGameManagerModule> _modules = new List<BaseGameManagerModule>();

        public IReadOnlyCollection<BaseGameManagerModule> Modules => _modules.ToHashSet();
    }
}

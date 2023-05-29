using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Systems.WorldActionsSystem
{
    public class WorldActionData
    {
        [SerializeField] private string _name;

        public string Name => _name;
    }
}

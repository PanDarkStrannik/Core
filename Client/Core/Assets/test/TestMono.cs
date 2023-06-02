using System.Collections;
using System.Collections.Generic;
using GameCore.Patterns;
using UnityEngine;

namespace GameCore
{
    public class TestMono : MonoBehaviour
    {
        private readonly TestModule _testModule = new TestModule();
        private DataControllerFabric _dataControllerFabric;
        private IController _testController;

        private void Awake()
        {
            _dataControllerFabric = new DataControllerFabric();
            _testController = _dataControllerFabric.Create(_testModule);
        }
    }
}

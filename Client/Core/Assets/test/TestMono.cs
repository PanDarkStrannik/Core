using GameCore.Patterns;
using GameCore.Utils;
using UnityEngine;

namespace GameCore
{
    public class TestMono : MonoBehaviour
    {
        private readonly TestModule _testModule = new TestModule();
        private DataControllerFabric _dataControllerFabric;
        private IController _testController;

        private readonly SimpleEvent _simpleEvent = new SimpleEvent();

        private void Awake()
        {
            _dataControllerFabric = new DataControllerFabric();
            _testController = _dataControllerFabric.Create(_testModule);

            var test2 = new Test2();
            var test3 = new Test2();

            _simpleEvent.Subscribe(test2.Test5);
            _simpleEvent.Subscribe(test3.Test5);
            _simpleEvent.Invoke();
            _simpleEvent.Unsubscribe(test2);
            _simpleEvent.Invoke();
        }
    }
}

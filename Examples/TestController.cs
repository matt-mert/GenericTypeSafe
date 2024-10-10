using MattMert.Common;
using MattMert.GenericGrids;
using MattMert.GenericPools;
using MattMert.GenericSignals;
using UnityEngine;

namespace MattMert.Testing
{
    public class SomeVolume : AVolume<TestObject> {}
    public class SomeSurface : ASurface<TestObject> {}
    public class SomePool : APool<TestObject> {}
    public class SomeSignal : ASignal<string> {}
    
    public class TestController : MonoBehaviour
    {
        [SerializeField] private TestObject prefab;

        private GridVolume<TestObject> _volume;
        private GridSurface<TestObject> _surface;

        private void ExampleForSignals()
        {
            var signal = new GenericSignal<int>();
            signal.Dispatch(5);
        }

        private void ExampleForGrids()
        {
            _volume = new GridVolume<TestObject>(3, 3, 3);
            _surface = new GridSurface<TestObject>(2, 3, GridAxis.y);
            _volume.CreateVolume(Getter);
            _surface.CreateSurface(Getter);
            
            return;

            TestObject Getter()
            {
                return Instantiate(prefab, transform);
            }
        }

        private void ExampleForTypeSafe()
        {
            TypeSafe.Get<SomeVolume>().Initialize(2, 2, 2);
            
            TypeSafe.Get<SomeSurface>().Initialize(3, 4, GridAxis.z);
            
            TypeSafe.Get<SomePool>().Initialize(prefab);
            
            TypeSafe.Get<SomeSignal>().Dispatch("Hello World!");
        }

        private void ExampleForCombination()
        {
            TypeSafe.Get<SomeVolume>().CreateVolume(Getter);
            
            return;

            TestObject Getter()
            {
                return TypeSafe.Get<SomePool>().Get();
            }
        }

        private void AnotherExample()
        {
            TypeSafe.Get<SomePool>().Initialize(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy);

            return;
            
            TestObject CreateFunc()
            {
                return TypeSafe.Get<SomePool>().Get();
            }

            void ActionOnGet(TestObject obj)
            {
                TypeSafe.Get<SomeSignal>().Dispatch($"{obj.name} is here!");
            }

            void ActionOnRelease(TestObject obj)
            {
                TypeSafe.Get<SomeSignal>().Dispatch($"{obj.name} is gone!");
            }

            void ActionOnDestroy(TestObject obj)
            {
                TypeSafe.Get<SomeSignal>().Dispatch($"{obj.name} is destroyed!");
            }
        }
    }
}
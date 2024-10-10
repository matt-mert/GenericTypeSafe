using MattMert.GenericGrids;
using MattMert.GenericPools;
using UnityEngine;

namespace MattMert.Testing
{
    public class TestObject : MonoBehaviour, IGridObject, IPoolObject
    {
        public IGridUnit GridUnit { get; }
        
        void IGridObject.OnCreate()
        {
            gameObject.SetActive(true);
        }

        void IGridObject.OnDispose()
        {
            Destroy(gameObject);
        }

        void IGridObject.OnShift()
        {
            transform.position = GridUnit.Coords.ToVector3();
        }

        void IPoolObject.OnGet()
        {
            Debug.Log("Hello there!");
        }

        void IPoolObject.OnRelease()
        {
            Debug.Log("Good bye!");
        }
    }
}
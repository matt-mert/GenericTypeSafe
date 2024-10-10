using UnityEngine;

namespace MattMert.GenericGrids
{
    public abstract class AGridObject : MonoBehaviour, IGridObject
    {
        public IGridUnit GridUnit { get; }
        public abstract void OnCreate();
        public abstract void OnDispose();
        public abstract void OnShift();
    }
    
    public static partial class GridsUtilities
    {
        
    }
    
    public static partial class GridsExtensions
    {
        public static Vector3Int ToVector3Int(this GridAxis axis)
        {
            return axis switch
            {
                GridAxis.x => Vector3Int.right,
                GridAxis.y => Vector3Int.up,
                GridAxis.z => Vector3Int.forward,
                _ => default
            };
        }
        
        public static Vector3Int ToVector3Int(this GridAxis axis, int i)
        {
            return axis switch
            {
                GridAxis.x => i * Vector3Int.right,
                GridAxis.y => i * Vector3Int.up,
                GridAxis.z => i * Vector3Int.forward,
                _ => default
            };
        }
        
        public static Vector3 ToVector3(this GridAxis axis)
        {
            return axis switch
            {
                GridAxis.x => Vector3.right,
                GridAxis.y => Vector3.up,
                GridAxis.z => Vector3.forward,
                _ => default
            };
        }
        
        public static Vector3 ToVector3(this GridAxis axis, int i)
        {
            return axis switch
            {
                GridAxis.x => i * Vector3.right,
                GridAxis.y => i * Vector3.up,
                GridAxis.z => i * Vector3.forward,
                _ => default
            };
        }

        public static (int, int, int) ToCoords(this Vector3Int vector)
        {
            return (vector.x, vector.y, vector.z);
        }

        public static (int, int, int) ToCoords(this Vector3 vector)
        {
            return ((int)vector.x, (int)vector.y, (int)vector.z);
        }

        public static (int, int) ToCoords(this Vector2Int vector)
        {
            return (vector.x, vector.y);
        }

        public static (int, int) ToCoords(this Vector2 vector)
        {
            return ((int)vector.x, (int)vector.y);
        }

        public static Vector3Int ToVector3Int(this (int, int, int) coords)
        {
            return new Vector3Int(coords.Item1, coords.Item2, coords.Item3);
        }

        public static Vector3 ToVector3(this (int, int, int) coords)
        {
            return new Vector3(coords.Item1, coords.Item2, coords.Item3);
        }

        public static Vector2Int ToVector2Int(this (int, int) coords)
        {
            return new Vector2Int(coords.Item1, coords.Item2);
        }

        public static Vector2 ToVector2(this (int, int) coords)
        {
            return new Vector2(coords.Item1, coords.Item2);
        }

        public static IGridUnit GetUnit(this IGridVolume volume, Vector3Int coords)
        {
            return volume.GetUnit(coords.x, coords.y, coords.z);
        }
        
        public static IGridUnit GetUnit(this IGridVolume volume, Vector3 coords)
        {
            return volume.GetUnit((int)coords.x, (int)coords.y, (int)coords.z);
        }

        public static IGridUnit GetUnit(this IGridSurface surface, Vector2Int coords)
        {
            return surface.GetUnit(coords.x, coords.y);
        }

        public static IGridUnit GetUnit(this IGridSurface surface, Vector2 coords)
        {
            return surface.GetUnit((int)coords.x, (int)coords.y);
        }
    }
}
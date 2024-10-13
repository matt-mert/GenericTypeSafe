using System;
using MattMert.Common;

namespace MattMert.GenericGrids
{
    public class Grids : ATypeSafeMain<GridHub>
    {
    }

    public class GridHub : ATypeSafeHub
    {
    }

    public abstract class AVolume<T> : ATypeSafe where T : class, IGridObject
    {
        private GridVolume<T> _volume;

        public void Initialize(int width, int height, int depth)
        {
            _volume = new GridVolume<T>(width, height, depth);
        }

        public override void Destroy()
        {
            _volume.DisposeUnits();
            _volume = null;
        }

        public int GetWidth() => _volume.Width;
        public int GetHeight() => _volume.Height;
        public int GetDepth() => _volume.Depth;
        public int GetCount() => _volume.Count;
        public GridSurface<T> GetSurface(GridAxis normal, int index) => _volume.GetSurface(normal, index);
        public GridUnit<T> GetUnit(int i, int j, int k) => _volume.GetUnit(i, j, k);
        public GridUnit<T> GetUnit((int, int, int) coords) => _volume.GetUnit(coords);
        public GridUnit<T>[] GetUnits() => _volume.GetUnits();
        public GridUnit<T>[] GetUnits((int, int, int) from, (int, int, int) to) => _volume.GetUnits(from, to);
        public void CreateVolume(Func<T> getter) => _volume.CreateVolume(getter);
        public void RemoveVolume() => _volume.RemoveVolume();
        public void ResizeVolume(int width, int height, int depth) => _volume.ResizeVolume(width, height, depth);
        public void TrimVolume((int, int, int) from, (int, int, int) to) => _volume.TrimVolume(from, to);
        public void DisposeUnits() => _volume.DisposeUnits();
        public void AddSurface(GridAxis normal) => _volume.AddSurface(normal);
        public void InsertSurface(GridAxis normal, int index) => _volume.InsertSurface(normal, index);
        public void RemoveSurface(GridAxis normal, int index) => _volume.RemoveSurface(normal, index);
    }

    public abstract class ASurface<T> : ATypeSafe where T : class, IGridObject
    {
        private GridSurface<T> _surface;

        public void Initialize(int width, int height, GridAxis normal)
        {
            _surface = new GridSurface<T>(width, height, normal);
        }

        public override void Destroy()
        {
            _surface.DisposeUnits();
            _surface = null;
        }

        public GridAxis GetNormal() => _surface.Normal;
        public int GetWidth() => _surface.Width;
        public int GetHeight() => _surface.Height;
        public int GetCount() => _surface.Count;
        public IGridLine GetLine(GridAxis axis, int index) => _surface.GetLine(axis, index);
        public GridUnit<T> GetUnit(int i, int j) => _surface.GetUnit(i, j);
        public GridUnit<T> GetUnit((int, int) coords) => _surface.GetUnit(coords);
        public GridUnit<T>[] GetUnits() => _surface.GetUnits();
        public GridUnit<T>[] GetUnits((int, int) from, (int, int) to) => _surface.GetUnits(from, to);
        public void CreateSurface(Func<T> getter) => _surface.CreateSurface(getter);
        public void RemoveSurface() => _surface.RemoveSurface();
        public void ResizeSurface(int width, int height) => _surface.ResizeSurface(width, height);
        public void TrimSurface((int, int) from, (int, int) to) => _surface.TrimSurface(from, to);
        public void DisposeUnits() => _surface.DisposeUnits();
        public void AddLine(GridAxis axis) => _surface.AddLine(axis);
        public void InsertLine(GridAxis axis, int index) => _surface.InsertLine(axis, index);
        public void RemoveLine(GridAxis axis, int index) => _surface.RemoveLine(axis, index);
    }

    public abstract class ALine<T> : ATypeSafe where T : class, IGridObject
    {
        private GridLine<T> _line;

        public void Initialize(int length, GridAxis axis)
        {
            _line = new GridLine<T>(length, axis);
        }

        public override void Destroy()
        {
            _line.DisposeUnits();
            _line = null;
        }

        public GridAxis GetAxis() => _line.Axis;
        public int GetLength() => _line.Length;
        public GridUnit<T> GetUnit(int index) => _line.GetUnit(index);
        public GridUnit<T>[] GetUnits() => _line.GetUnits();
        public GridUnit<T>[] GetUnits(int from, int to) => _line.GetUnits(from, to);
        public void CreateLine(Func<T> getter) => _line.CreateLine(getter);
        public void RemoveLine() => _line.RemoveLine();
        public void ResizeLine(int length) => _line.ResizeLine(length);
        public void TrimLine(int from, int to) => _line.TrimLine(from, to);
        public void DisposeUnits() => _line.DisposeUnits();
        public void AddUnit() => _line.AddUnit();
        public void InsertUnit(int index) => _line.InsertUnit(index);
        public void RemoveUnit(int index) => _line.RemoveUnit(index);
    }
}

using System.Collections.Generic;
using GridPack.Cells;
using GridPack.Units;

namespace GridPack.Grid.UnitGenerators
{
    public interface IUnitGenerator
    {
        List<Unit> SpawnUnits(List<Cell> cells);
    }
}

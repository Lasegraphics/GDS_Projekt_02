using GridPack.Grid;
using GridPack.Grid.GridStates;

namespace GridPack.Players
{
    public class HumanPlayer : Player
    {
        public override void Play(CellGrid cellGrid)
        {
            cellGrid.CellGridState = new CellGridStateWaitingForInput(cellGrid);
        }
    }
}

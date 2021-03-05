namespace GridPack.Units.UnitStates
{
    public abstract class UnitState
    {
        //Klasa obsługująca stan jednostki.
        protected Unit _unit;

        public UnitState(Unit unit)
        {
            _unit = unit;
        }

        public abstract void Apply();
        public abstract void MakeTransition(UnitState state);
    }
}


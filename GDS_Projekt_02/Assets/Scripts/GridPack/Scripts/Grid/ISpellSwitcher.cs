using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridPack.Grid
{
    public interface ISpellSwitcher
    {
        void Activate();
        void Deactivate();
    }
}

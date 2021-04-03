using System.Collections;
using GridPack.Cells;
using GridPack.Units;
using UnityEngine;
using System; 
using System.Threading; 
//using Random = UnityEngine.Random;

namespace GridPack.SceneScripts
{
    public class ArmoredEntity : Entity
    {
        public override event EventHandler<AttackEventArgs> UnitAttacked;
        public override event EventHandler<AttackEventArgs> UnitDestroyed;
        protected static bool IsIgnored;
        public override void Initialize()
        {
            base.Initialize();
            TotalArmorPoints = ArmorPoints;
            IsIgnored = false;
        }
    }
}


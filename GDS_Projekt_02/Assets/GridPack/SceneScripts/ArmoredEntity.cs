using System.Collections;
using GridPack.Cells;
using GridPack.Units;
using UnityEngine;
using System; 
using System.Threading; 
using Random = UnityEngine.Random;

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


        /*  MarkAsDefending(aggressor); 
          int damageTaken = Defend(aggressor, damage);
          damageTaken = Random.Range(attackMin,attackMax);




          if(HitPoints <= 0)
          {
              if (UnitDestroyed != null)
              {
                  UnitDestroyed.Invoke(this, new AttackEventArgs(aggressor, this, damage));
              }
              OnDestroyed();
          }
          Debug.Log("Obecne Zdrowie: " + HitPoints + " Zadane Obrazenia: " + damageTaken);
          */
    }


}


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

        public override void DefendHandler(Unit aggressor, int damage)
        {
            Thread th = Thread.CurrentThread; 
            int armorDamageTaken = Defend(aggressor, damage);
            //armorDamageTaken = Random.Range(attackMin,attackMax);
            if(ArmorPoints >= 0)
            {
                try 
                {
                    ArmorPoints -= armorDamageTaken;
                    Debug.Log("Obecny Pancerz: " + ArmorPoints + " Zadane Obrazenia: " + armorDamageTaken);
                }
                
                catch(ThreadAbortException)
                {
                    if(ArmorPoints == 0)
                    {
                     th.Abort();
                    }
                }
            }
                
           if(ArmorPoints <= 0 || IsIgnored == true)
            {
                int damageTaken = Defend(aggressor, damage);
                //damageTaken = Random.Range(attackMin,attackMax);
                HitPoints -= damageTaken;  
                DefenceActionPerformed();
                IsIgnored = false; 
                if(UnitAttacked != null)
                {
                    UnitAttacked?.Invoke(this, new AttackEventArgs(aggressor, this, damage)); 
                }

                if(HitPoints <= 0)
                {
                    if (UnitDestroyed != null)
                    {
                        UnitDestroyed.Invoke(this, new AttackEventArgs(aggressor, this, damage));
                    }
                    OnDestroyed();
                }
                Debug.Log("Obecne Zdrowie: " + HitPoints + " Zadane Obrazenia: " + damageTaken);
                
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

}
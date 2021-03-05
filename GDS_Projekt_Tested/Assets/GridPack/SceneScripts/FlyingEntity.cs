using System.Collections;
using System.Collections.Generic;
using GridPack.Cells;
using UnityEngine;

namespace GridPack.SceneScripts
{
    public class FlyingEntity : Entity
    {
        public override void Initialize()
        {
            base.Initialize();
            (Cell as MyOtherHexagon).IsSkyTaken = true;
        }

        public override bool IsCellTraversable(Cell cell)
        {
            return !(cell as MyOtherHexagon).IsSkyTaken; //Pozwala przejść jednosce przez dowolne pole nie zajęte przez inną jednostkę latającą
        }
        public override void Move(Cell destinationCell, List<Cell> path)
        {
            (Cell as MyOtherHexagon).IsSkyTaken = false;
            (destinationCell as MyOtherHexagon).IsSkyTaken = true;
            base.Move(destinationCell, path);
        }

        protected override IEnumerator MovementAnimation(List<Cell> path)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 6;
            yield return StartCoroutine(base.MovementAnimation(path));
            GetComponent<SpriteRenderer>().sortingOrder = 3;
        }

        protected override void OnDestroyed()
        {
            (Cell as MyOtherHexagon).IsSkyTaken = false;
            base.OnDestroyed();
        }
    }
}
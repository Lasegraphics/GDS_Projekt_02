using GridPack.Cells;
using UnityEngine;

namespace GridPack.SceneScripts
{
    public class MyOtherHexagon : Hexagon
    {
        public GroundType GroundType;
        public bool IsSkyTaken;//Wskazuje czy jednostka latająca zajmuje hexagon 
        private Vector3 dimensions = new Vector3(5.3f, 4.6f, 0f);

        public void Start()
        {
            SetColor(new Color(1, 1, 1, 0));
        }
      
        public override void MarkAsPlayerEntity()
        {
            SetColor(new Color(0, 1, 0, 1));
        }

        public override void MarkAsEnemyEntity()
        {
            if (CurrentUnit !=null)
            {
                CurrentUnit.isBlinking = true;
            } 
            SetColor(new Color(1, 0, 0, 0.5f));
        }

        public override void MarkAsReachable()
        {
            SetColor(new Color(1, 0.92f, 0.016f, 1));
        }
        public override void MarkAsPath()
        {
            SetColor(new Color(0, 1, 0, 1));
        }
        public override void MarkAsHighlighted()
        {
            SetColor(new Color(0.5f, 0.5f, 0.5f, 0.25f));
        }
        public override void UnMark()
        {
            if (CurrentUnit != null)
            {
                CurrentUnit.isBlinking = false;
            }    
            SetColor(new Color(1, 1, 1, 0));
        }
        private void SetColor(Color color)
        {
            var highlighter = transform.Find("Highlighter");
            var spriteRenderer = highlighter.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
            foreach (Transform child in highlighter.transform)
            {
                var childColor = new Color(color.r, color.g, color.b, 1);
                spriteRenderer = child.GetComponent<SpriteRenderer>();
                if (spriteRenderer == null) continue;
                child.GetComponent<SpriteRenderer>().color = childColor;
            }
        }

        public override Vector3 GetCellDimensions()
        {
            return dimensions;
        }
    }

    public enum GroundType
    {
        Land,
        Water,
        Forest
    };
}
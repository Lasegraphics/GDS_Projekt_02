using System; 
using UnityEngine;
using GridPack.Grid; 
using GridPack.Grid.GridStates; 
using GridPack.SceneScripts;

namespace GridPack.SceneScripts
{
    public class TurnChanger : MonoBehaviour
    {
        public CellGrid EndTrn; 
        public Entity entity; 
        private bool isFinished;
        private int unitDiscard;
        public void StartGame()
        {
           EndTrn.LevelLoading += onLevelLoading;
           EndTrn.LevelLoadingDone += onLevelLoadingDone;
           isFinished = false; 
           unitDiscard = 0;
        }

        private void onLevelLoading(object sender, EventArgs e)
        {
            Debug.Log("Level is loading");
        }

        private void onLevelLoadingDone(object sender, EventArgs e)
        {
             Debug.Log("Level loading done");
             Debug.Log("Press n to end turn");
        }

        public void ChangeTurn()
        {   
            ++unitDiscard;
            if(unitDiscard == 5)
            {
                isFinished = true; 
                unitDiscard = 0; 
            }
        }
     
        void Update()
        {
          if(isFinished == true)
          {
            EndTrn.EndTurn();
            isFinished = false; 
          }
            
        }
    }
}

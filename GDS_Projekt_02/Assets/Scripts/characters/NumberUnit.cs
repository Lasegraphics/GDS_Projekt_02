using GridPack.Grid;
using GridPack.Units;
using UnityEngine;

namespace characters
{
    public class NumberUnit : MonoBehaviour
    {
        private UiManager uiManager;
        private ScorePanelControll scorePanelControll;
        private CellGrid cellGrid;
        private EnemyScorePanel enemyScorePanel;
        private AudioManager audioManager;
        public bool isSelected;

        int playerNumber;

        private void Awake()
        {
            cellGrid = FindObjectOfType<CellGrid>();
            enemyScorePanel = FindObjectOfType<EnemyScorePanel>();
            uiManager = FindObjectOfType<UiManager>();
            scorePanelControll = FindObjectOfType<ScorePanelControll>();
            audioManager = FindObjectOfType<AudioManager>();
            playerNumber = GetComponent<Unit>().PlayerNumber;
        }

        private void OnMouseEnter()
        {
            if (uiManager.isStart == false)
            {
                if (playerNumber != cellGrid.CurrentPlayerNumber)
                {

                    uiManager.ActiveEnemyScorePanel();
                    enemyScorePanel.UpgradeParameters(gameObject);

                    enemyScorePanel.UpgadeParameters(gameObject.GetComponent<Unit>());
                }
            }

        }

        private void OnMouseOver()
        {
            if (uiManager.isStart == false)
            {
                if (playerNumber != cellGrid.CurrentPlayerNumber)
                {
                    enemyScorePanel.ChangeHpSlidder(gameObject.GetComponent<Unit>());
                    enemyScorePanel.ChangeArmorSlidder(gameObject.GetComponent<Unit>());
                }
            }
        }

        private void OnMouseExit()
        {
            uiManager.CloseEnemyScorePanel();
            enemyScorePanel.RestEvents();
        }

        private void OnMouseDown()
        {
            if (uiManager.isStart == false)
            {
                if (isSelected == false)
                {
                    if (playerNumber == cellGrid.CurrentPlayerNumber)
                    {
                        audioManager.Play("SelectUnit");
                        foreach (var item in FindObjectsOfType<NumberUnit>())
                        {
                            item.GetComponent<NumberUnit>().isSelected = false;
                        }

                        isSelected = true;
                        uiManager.ActiveScorePanel();
                        scorePanelControll.TakeUnit(gameObject);
                    }
                }
            }
        }

        public void DeselectallUnits()
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Unit"))
            {
                item.GetComponent<NumberUnit>().isSelected = false;
            }
        }
    }
}


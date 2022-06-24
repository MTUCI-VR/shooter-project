using TMPro;
using UnityEngine;
using ShooterProject.Scripts.GameStatistics;

namespace ShooterProject.Scripts.GameManager.Menus
{////////////////////////////////////////////////////////////////////////// В Папку Меню
    public class GameStatsMenu : MonoBehaviour // :Menu
    {
        #region Fields

        [SerializeField]
        private TextMeshProUGUI gameStatsText;

        #endregion

        #region Life Cycle

        private void OnEnable()
        {
            Print();
        }

        // private void OnEnable() // override
        // {
        //     GameStats.OnDeadEnemyCountChanged += Print;
        //     GameStats.OnCompleteWaveCountChanged += Print;
        // }

        // private void OnDisable() // override
        // {
        //     GameStats.OnDeadEnemyCountChanged -= Print;
        //     GameStats.OnCompleteWaveCountChanged -= Print;
        // }

		#endregion

        #region Private Methods

        private void Print()
        {
            Debug.Log($"Количество убитых врагов: {GameStats.deadEnemyCount}\nКоличество пройденных волн: {GameStats.completeWaveCount}");
            // gameStatsText.text = $"Количество убитых врагов: {GameStats.DeadEnemyCount}\nКоличество пройденных волн: {GameStats.CompleteWaveCount}";
        }

        #endregion
    }
}

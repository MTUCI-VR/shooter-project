using TMPro;
using UnityEngine;
using ShooterProject.Scripts.GameStatistics;

namespace ShooterProject.Scripts.GameManager.Menus
{
	public class GameStatsMenu : Menu
	{
		#region Fields

		[SerializeField]
		private TextMeshProUGUI gameStatsText;

		#endregion

		#region Life Cycle

		private void Start()
		{
			Print();
		}

		#endregion

		#region Private Methods

		private void Print()
		{
			gameStatsText.text = $"Количество убитых врагов: {GameStats.DeadEnemyCount}\nКоличество пройденных волн: {GameStats.CompleteWaveCount}";
		}

		#endregion
	}
}

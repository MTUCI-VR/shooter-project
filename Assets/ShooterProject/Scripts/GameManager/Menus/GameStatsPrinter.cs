using TMPro;
using UnityEngine;
using ShooterProject.Scripts.GameStatistics;

namespace ShooterProject.Scripts.GameManager.Menus
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class GameStatsPrinter : MonoBehaviour
	{
		#region Fields

		private TextMeshProUGUI _gameStatsText;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_gameStatsText = GetComponent<TextMeshProUGUI>();
		}

		private void Start()
		{
			if (GameStats.CompletedGamesCount > 0)
			{
				_gameStatsText.text = $"Zombies killed: {GameStats.DeadEnemyCount}\nWaves complited: {GameStats.CompleteWaveCount}\nGames complited: {GameStats.CompletedGamesCount}";
			}
			else
			{
				_gameStatsText.text = string.Empty;
			}
		}

		#endregion
	}
}

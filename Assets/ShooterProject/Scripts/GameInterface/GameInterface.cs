using TMPro;
using UnityEngine;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.PlayerScripts;
using ShooterProject.Scripts.WaveControllers;

namespace ShooterProject.Scripts.GameInterface
{
	public class GameInterface : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private WaveController waveControler;

		[SerializeField]
		private TextMeshProUGUI _gameInterfaceText;

		private Health _playerHealth;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_playerHealth = Player.instance.PlayerHealth;
		}

		private void OnEnable()
		{
			AddListeners();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}

		#endregion

		#region Private Methods

		private void AddListeners()
		{
			_playerHealth.OnChanged += Print;
			waveControler.OnTimeBetweenWavesChanged += Print;
		}

		private void RemoveListeners()
		{
			_playerHealth.OnChanged -= Print;
			waveControler.OnTimeBetweenWavesChanged -= Print;
		}

		private void Print()
		{
			_gameInterfaceText.text = $"HP: {_playerHealth.CurrentHealth}\n00:{waveControler.TimeBetweenWavesInSeconds}";
		}

		#endregion
	}
}

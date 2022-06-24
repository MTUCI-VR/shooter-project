using TMPro;
using UnityEngine;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.PlayerScripts;
using ShooterProject.Scripts.Waves;
using System.Collections;

namespace ShooterProject.Scripts.GameInterface
{
	public class GameInterface : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private TextMeshProUGUI _gameInterfaceText;

		private Health _playerHealth;

		private float _wavePreparationTime;

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
			WavesProvider.Instance.OnWavePreparationStarted += Print;
		}

		private void RemoveListeners()
		{
			_playerHealth.OnChanged -= Print;
			WavesProvider.Instance.OnWavePreparationStarted -= Print;
		}

		private void Print(Health playerHealth)
		{
			_gameInterfaceText.text = $"HP: {_playerHealth.CurrentHealth}\n00:{_wavePreparationTime}";
		}
		private void Print(float wavePreparationTime)
		{
			StartCoroutine(WavePreparationTimeIndication(wavePreparationTime));
		}
	
		private IEnumerator WavePreparationTimeIndication(float wavePreparationTime)
		{
			_wavePreparationTime = wavePreparationTime;

			while (_wavePreparationTime > 0)
			{
				_gameInterfaceText.text = $"HP: {_playerHealth.CurrentHealth}\n00:{_wavePreparationTime}";

				_wavePreparationTime -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		#endregion
	}
}

using UnityEngine;
using TMPro;
using ShooterProject.Scripts.Waves;
using System.Collections;

namespace _Developers.EgorKirnosov.Scripts.Waves
{
	[RequireComponent(typeof(WavesProvider))]
	public class WaveInfoTextIndicator : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private TextMeshProUGUI textLabel;

		private WavesProvider _wavesProvider;

		#endregion

		#region LifeCycle
		private void Awake()
		{
			_wavesProvider = GetComponent<WavesProvider>();
		}
		private void OnEnable()
		{
			_wavesProvider.OnWavePreparationStarted += WaveEnded;
			_wavesProvider.OnWaveStarted += WaveStarted;
			_wavesProvider.OnWavesEnded += WavesEnded;
		}
		private void OnDisable()
		{
			WavesProvider.Instance.OnWavePreparationStarted -= WaveEnded;
			WavesProvider.Instance.OnWaveStarted -= WaveStarted;
		}

		#endregion

		#region Private Methods

		private void WavesEnded()
		{
			StopAllCoroutines();
			textLabel.text = $"Все волны побеждены!";
		}
		private void WaveEnded(float preparationTime)
		{
			StartCoroutine(WavePreparationTimeIndication(preparationTime));
		}
		private void WaveStarted(int wave)
		{
			StopAllCoroutines();
			textLabel.text = $"Волна {wave} начата!";
		}
		private IEnumerator WavePreparationTimeIndication(float preparationTime)
		{
			var time = preparationTime;
			while (time > 0)
			{
				textLabel.text = $"Время до следующей волны: {time.ToString()} секунд";

				time -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
		}

		#endregion
	}
}

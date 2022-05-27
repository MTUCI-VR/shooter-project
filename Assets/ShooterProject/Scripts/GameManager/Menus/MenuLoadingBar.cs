using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShooterProject.Scripts.GameManager.Menus
{
	[System.Serializable]
	public struct MenuLoadingBar
	{
		public GameObject loadingBar;

		public Image _progress;

		public TextMeshProUGUI _progressText;
	}
}

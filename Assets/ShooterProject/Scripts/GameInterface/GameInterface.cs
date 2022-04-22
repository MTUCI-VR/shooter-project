using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Actors.Health;
using ShooterProject.Scripts.Weapons.Reloading;
using ShooterProject.Scripts.Inventory;
using ShooterProject.Scripts.WaveControllers;

namespace ShooterProject.Scripts.GameInterface
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class GameInterface : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private Health playerHealth;

		[SerializeField]
		private WaveController waveControler;

		[SerializeField]
		private XRDirectInteractor HandDirectInteractor;

		private int _attachedMagazineAmmoCount;

		private TextMeshProUGUI _gameInterfaceText;

		#endregion

		#region Life Cycle

		private void Awake()
		{
			_gameInterfaceText = GetComponent<TextMeshProUGUI>();
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
			playerHealth.OnChanged += Print;
			InventoryInfo.OnAmmoCountChanged += Print;
			InventoryInfo.OnHasKnifeChanged += Print;
			waveControler.OnTimeBetweenWavesChanged += Print;
			HandDirectInteractor.selectEntered.AddListener(OnDirectInteractorSelectEntered);
			HandDirectInteractor.selectExited.AddListener(OnDirectInteractorSelectExited);
		}

		private void RemoveListeners()
		{
			playerHealth.OnChanged -= Print;
			InventoryInfo.OnAmmoCountChanged -= Print;
			InventoryInfo.OnHasKnifeChanged -= Print;
			waveControler.OnTimeBetweenWavesChanged -= Print;
			HandDirectInteractor.selectEntered.RemoveListener(OnDirectInteractorSelectEntered);
			HandDirectInteractor.selectExited.RemoveListener(OnDirectInteractorSelectExited);
		}

		private void OnDirectInteractorSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			if (selectEnterEventArgs.interactableObject.transform.TryGetComponent<WeaponMagazineController>(out var weaponMagazine))
			{
				XRSocketInteractor weaponMagazineSocketInteractor = weaponMagazine.GetComponent<XRSocketInteractor>();

				weaponMagazineSocketInteractor.selectEntered.AddListener(OnSocketInteractorSelectEntered);
				weaponMagazineSocketInteractor.selectExited.AddListener(OnSocketInteractorSelectExited);
			}
		}

		private void OnDirectInteractorSelectExited(SelectExitEventArgs selectExitEventArgs)
		{
			if (selectExitEventArgs.interactableObject.transform.TryGetComponent<WeaponMagazineController>(out var weaponMagazine))
			{
				XRSocketInteractor weaponMagazineSocketInteractor = weaponMagazine.GetComponent<XRSocketInteractor>();

				weaponMagazineSocketInteractor.selectEntered.RemoveListener(OnSocketInteractorSelectEntered);
				weaponMagazineSocketInteractor.selectExited.RemoveListener(OnSocketInteractorSelectExited);
			}
		}

		private void OnSocketInteractorSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			AmmoMagazine attachedMagazine = selectEnterEventArgs.interactableObject.transform.GetComponent<AmmoMagazine>();

			_attachedMagazineAmmoCount = attachedMagazine.AmmoCount;

			attachedMagazine.OnAmmoCountChanged += OnAmmoCountChanged;

			Print();
		}

		private void OnSocketInteractorSelectExited(SelectExitEventArgs selectExitEventArgs)
		{
			_attachedMagazineAmmoCount = 0;

			Print();
		}

		private void OnAmmoCountChanged(int ammoCount)
		{
			_attachedMagazineAmmoCount = ammoCount;

			Print();
		}

		private void Print()
		{
			_gameInterfaceText.text = $"HP: {playerHealth.CurrentHealth}\nAmmo: {_attachedMagazineAmmoCount}/{InventoryInfo.AmmoCount}\nKnife: {InventoryInfo.HasKnife}\n00:{waveControler.TimeBetweenWavesInSeconds}";
		}

		#endregion
	}
}

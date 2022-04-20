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

        private TextMeshProUGUI _text;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
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
            HandDirectInteractor.selectEntered.AddListener(OnSelectEntered);
            InventoryInfo.OnAmmoMagazineCountChanged += Print;
            InventoryInfo.OnHasKnifeChanged += Print;
        }

        private void RemoveListeners()
        {
            playerHealth.OnChanged -= Print;
            InventoryInfo.OnAmmoMagazineCountChanged -= Print;
            InventoryInfo.OnHasKnifeChanged -= Print;
        }

        private void Print()
        {
            Debug.Log($"HP: {playerHealth.CurrentHealth}\nAmmo: {80}/{100}\nKnife: {InventoryInfo.HasKnife}\n00:{waveControler.TimeBetweenWaves}");
        }

        private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
        {
            if (selectEnterEventArgs.interactableObject.transform.TryGetComponent<WeaponMagazineController>(out var weaponMagazine))
            {
                // weaponMagazine.GetComponent<XRSocketInteractor>().
            }
        }

        #endregion
    }
}

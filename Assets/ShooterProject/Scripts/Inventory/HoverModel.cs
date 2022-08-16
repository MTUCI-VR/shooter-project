using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ShooterProject.Scripts.Inventory
{
	[RequireComponent(typeof(XRSocketInteractor))]
	public class HoverModel : MonoBehaviour
	{
		#region Fields

		[SerializeField]
		private GameObject hoverModel;

		[SerializeField]
		private Material enterMaterial;

		[SerializeField]
		private Material exitMaterial;
		
		[SerializeField]
		private Material packedMaterial;

		[SerializeField]
		private Vector3 modelScaleOnHover;

		[SerializeField]
		private Vector3 StayModelScale;

		private MeshRenderer _meshRenderer;

		private XRSocketInteractor _socketInteractor;

		#endregion

		#region Properties

		private bool CanHover => TryGetComponent<AmmoMagazineInventorySlot>(out var ammoMagazineSlot) ? ammoMagazineSlot.CanPutInInventory : !_socketInteractor.hasSelection;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_socketInteractor = GetComponent<XRSocketInteractor>();

			if(!hoverModel.TryGetComponent<MeshRenderer>(out _meshRenderer))
			{
#if UNITY_EDITOR
				Debug.LogError("HoverModel's meshRenderer not found");
#endif
			}
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
			_socketInteractor.hoverEntered.AddListener(OnHoverEntered);
			_socketInteractor.hoverExited.AddListener(OnHoverExited);
			_socketInteractor.selectEntered.AddListener(OnSelectEntered);
		}

		private void RemoveListeners()
		{
			_socketInteractor.hoverEntered.RemoveListener(OnHoverEntered);
			_socketInteractor.hoverExited.RemoveListener(OnHoverExited);
			_socketInteractor.selectEntered.RemoveListener(OnSelectEntered);
		}

		private void OnHoverEntered(HoverEnterEventArgs hoverEnterEventArgs)
		{
			if(!CanHover) return;

			_meshRenderer.material = enterMaterial;
			hoverModel.transform.localScale = modelScaleOnHover;
		
		}
		private void OnHoverExited(HoverExitEventArgs hoverExitEventArgs)
		{
			_meshRenderer.material = _socketInteractor.hasSelection ? packedMaterial : exitMaterial;
			hoverModel.transform.localScale = StayModelScale;
		}

		private void OnSelectEntered(SelectEnterEventArgs selectEnterEventArgs)
		{
			_meshRenderer.material = packedMaterial;
			hoverModel.transform.localScale = StayModelScale;
		}

		#endregion
	}
}


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

		private MeshRenderer _meshRenderer;

		private XRSocketInteractor _socketInteractor;

		private bool _hasMeshRenderer;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_socketInteractor = GetComponent<XRSocketInteractor>();
			_hasMeshRenderer = hoverModel.TryGetComponent<MeshRenderer>(out _meshRenderer);
		}
		private void OnEnable()
		{
			if (_hasMeshRenderer)
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
			_socketInteractor.hoverEntered.AddListener(OnChangeMaterialToEnter);
			_socketInteractor.hoverExited.AddListener(OnChangeMaterialToExit);
			_socketInteractor.selectEntered.AddListener(OnDisableModel);
			_socketInteractor.selectExited.AddListener(OnEnableModel);
		}

		private void RemoveListeners()
		{
			_socketInteractor.hoverEntered.RemoveListener(OnChangeMaterialToEnter);
			_socketInteractor.hoverExited.RemoveListener(OnChangeMaterialToExit);
			_socketInteractor.selectEntered.RemoveListener(OnDisableModel);
			_socketInteractor.selectExited.RemoveListener(OnEnableModel);
		}

		private void OnChangeMaterialToEnter(HoverEnterEventArgs hoverEnterEventArgs)
		{
			if (!_socketInteractor.hasSelection)
				_meshRenderer.material = enterMaterial;
		}
		private void OnChangeMaterialToExit(HoverExitEventArgs hoverExitEventArgs)
		{
			_meshRenderer.material = exitMaterial;
		}

		private void OnDisableModel(SelectEnterEventArgs selectEnterEventArgs)
		{
			hoverModel.SetActive(false);
		}
		private void OnEnableModel(SelectExitEventArgs selectExitEventArgs)
		{
			hoverModel.SetActive(true);
		}

		#endregion
	}
}


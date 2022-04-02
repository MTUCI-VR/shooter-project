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
		
		private bool _hasSocketInteractor;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			_hasSocketInteractor = TryGetComponent<XRSocketInteractor>(out _socketInteractor);
			_hasMeshRenderer = hoverModel.TryGetComponent<MeshRenderer>(out _meshRenderer);
		}
		private void OnEnable()
		{
			if(_hasMeshRenderer && _hasSocketInteractor)
			{
				_socketInteractor.hoverEntered.AddListener(ChangeHoverModelMaterial);
				_socketInteractor.hoverExited.AddListener(ChangeHoverModelMaterial);
				_socketInteractor.selectEntered.AddListener(HoverModelOff);
				_socketInteractor.selectExited.AddListener(HoverModelOn);
			}
		}

		private void OnDisable()
		{
			_socketInteractor.hoverEntered.RemoveAllListeners();
			_socketInteractor.hoverExited.RemoveAllListeners();
			_socketInteractor.selectEntered.RemoveAllListeners();
			_socketInteractor.selectExited.RemoveAllListeners();
		}

		#endregion

		#region Private Methods

		private void ChangeHoverModelMaterial(HoverEnterEventArgs arg0)
		{
			if (!_socketInteractor.hasSelection)
				_meshRenderer.material = enterMaterial;
		}
		private void ChangeHoverModelMaterial(HoverExitEventArgs arg0)
		{
			_meshRenderer.material = exitMaterial;
		}

		private void HoverModelOff(SelectEnterEventArgs arg0)
		{
			hoverModel.SetActive(false);
		}
		private void HoverModelOn(SelectExitEventArgs arg0)
		{
			hoverModel.SetActive(true);
		}

		#endregion
	}
}


using System;
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
		private MeshRenderer meshRenderer;
		private XRSocketInteractor socketInteractor;

		#endregion

		#region LifeCycle

		private void Awake()
		{
			TryGetComponent<XRSocketInteractor>(out socketInteractor);
			meshRenderer = GetComponentInChildren<MeshRenderer>();
		}
		private void Start()
		{

			socketInteractor.hoverEntered.AddListener(HoverModelOn);
			socketInteractor.hoverExited.AddListener(HoverModelOff);
			socketInteractor.selectEntered.AddListener(HoverModelOff);
			socketInteractor.selectExited.AddListener(HoverModelOff);
		}


		#endregion

		#region Private Methods

		private void HoverModelOn(HoverEnterEventArgs arg0)
		{
			if (!socketInteractor.hasSelection)
				meshRenderer.material = enterMaterial;
			// hoverModel.SetActive(true);
		}
		private void HoverModelOff(HoverExitEventArgs arg0)
		{
			meshRenderer.material = exitMaterial;
			// hoverModel.SetActive(false);
		}

		private void HoverModelOff(SelectEnterEventArgs arg0)
		{
			hoverModel.SetActive(false);
		}
		private void HoverModelOff(SelectExitEventArgs arg0)
		{
			hoverModel.SetActive(true);
		}

		#endregion
	}
}


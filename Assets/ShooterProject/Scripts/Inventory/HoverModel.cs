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
        private XRSocketInteractor socketInteractor;

        #endregion

        #region LifeCycle
        private void Awake()
        {
            TryGetComponent<XRSocketInteractor>(out socketInteractor);
        }
        private void FixedUpdate()
        {
            socketInteractor.hoverEntered.AddListener(HoverModelOn);
            socketInteractor.hoverExited.AddListener(HoverModelOff);
            socketInteractor.selectEntered.AddListener(HoverModelOff);
        }

        #endregion

        #region Private Methods
		private void HoverModelOn(HoverEnterEventArgs arg0)
		{
			hoverModel.SetActive(true);
		}
		private void HoverModelOff(HoverExitEventArgs arg0)
		{
			hoverModel.SetActive(false);
		}

		private void HoverModelOff(SelectEnterEventArgs arg0)
		{
			hoverModel.SetActive(false);
		}

        #endregion
    }
}


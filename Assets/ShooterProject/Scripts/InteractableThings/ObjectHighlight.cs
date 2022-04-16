using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Items;

namespace ShooterProject.Scripts.InteractableThings
{
    public class ObjectHighlight : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private XRDirectInteractor directInteractor;

        [SerializeField] 
        private LayerMask objectHighlightLayer;

        private int _objectHighlightLayerValue;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _objectHighlightLayerValue = (int)System.Math.Log(objectHighlightLayer,2);
        }

        private void OnEnable()
        {
            directInteractor.selectEntered.AddListener(OnObjectHighlightDisable);
            directInteractor.selectExited.AddListener(OnObjectHighlightEnable);
        }
        private void OnDisable()
        {
            directInteractor.selectEntered.RemoveListener(OnObjectHighlightDisable);
            directInteractor.selectExited.RemoveListener(OnObjectHighlightEnable);
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent<Item>(out Item item) && !collider.gameObject.GetComponent<XRGrabInteractable>().isSelected) 
            {
                ObjectHighlightEnable(collider.gameObject);                
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if(collider.TryGetComponent<Item>(out Item item))
            {
                ObjectHighlightDisable(collider.gameObject);
            }
        }

        private void OnObjectHighlightDisable(SelectEnterEventArgs selectEnterEventArgs)
        {
            ObjectHighlightDisable(selectEnterEventArgs.interactableObject.transform.gameObject);
        }
        
        private void OnObjectHighlightEnable(SelectExitEventArgs selectExitEventArgs)
        {
            ObjectHighlightEnable(selectExitEventArgs.interactableObject.transform.gameObject);
        }

        private void ObjectHighlightEnable(GameObject objectHighlight)
        {
            objectHighlight.layer = _objectHighlightLayerValue;
        }

        private void ObjectHighlightDisable(GameObject objectHighlight)
        {
            objectHighlight.layer = objectHighlight.GetComponent<Item>().initialLayerValue;
        }

        #endregion
    }
}

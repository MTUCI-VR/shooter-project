using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Items;

namespace ShooterProject.Scripts.InteractableThings
{
    [RequireComponent(typeof(SphereCollider))]
    public class ObjectHighlight : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private XRDirectInteractor directInteractor;

        [SerializeField] 
        private LayerMask grabLayer;
        
        [SerializeField] 
        private LayerMask defaultLayer;

        private SphereCollider _collider;

        #endregion

        #region Life Cycle

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();

            directInteractor.selectEntered.AddListener(OnObjectHighlightDisable);
            directInteractor.selectExited.AddListener(OnObjectHighlightEnable);
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent<Item>(out Item item))
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
            _collider.enabled = false;

            ObjectHighlightDisable(selectEnterEventArgs.interactableObject.transform.gameObject);
        }
        
        private void OnObjectHighlightEnable(SelectExitEventArgs selectExitEventArgs)
        {
            ObjectHighlightEnable(selectExitEventArgs.interactableObject.transform.gameObject);

            _collider.enabled = true;
        }

        private void ObjectHighlightEnable(GameObject objectHighlight)
        {
            objectHighlight.layer = (int)System.Math.Log(grabLayer,2);
        }

        private void ObjectHighlightDisable(GameObject objectHighlight)
        {
            objectHighlight.layer = (int)System.Math.Log(defaultLayer,2);
        }

        #endregion
    }
}

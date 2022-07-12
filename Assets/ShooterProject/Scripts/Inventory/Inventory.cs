using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using ShooterProject.Scripts.Weapons.Reloading;

[RequireComponent(typeof(Collider))]
public class Inventory : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Transform attachTransform;

    private int _itemCount;

    private List<GameObject> _items = new List<GameObject>();

    private XRSocketInteractor _socketInteractor;

    #endregion

    #region Life Cycle

    private void Awake()
    {
        _socketInteractor = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        _socketInteractor.hoverEntered.AddListener(OnHoverEnter);
        _socketInteractor.selectExited.AddListener(OnSelectExit);
    }
    private void OnDisable()
    {
        _socketInteractor.hoverEntered.RemoveListener(OnHoverEnter);
        _socketInteractor.selectExited.RemoveListener(OnSelectExit);
    }

    #endregion

    #region Private Methods

    private void OnHoverEnter(HoverEnterEventArgs hoverEnterEventArgs)
    {
        if (hoverEnterEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine) && !magazine.GetComponent<XRGrabInteractable>().isSelected) //////////////////////////////////////////////////////// item
        {
            if (_socketInteractor.hasSelection)
            {Debug.Log("there");
                magazine.gameObject.SetActive(false);

                magazine.transform.position = attachTransform.position;
                magazine.transform.rotation = attachTransform.rotation;
            }
            
            _items.Add(magazine.gameObject);
            // Debug.Log($"Enter {magazine.gameObject} | {_items.Count}");
        }
    }

    private void OnSelectExit(SelectExitEventArgs selectExitEventArgs)
    {
        if (selectExitEventArgs.interactableObject.transform.TryGetComponent<AmmoMagazine>(out AmmoMagazine magazine))
        {
            _items.Remove(magazine.gameObject);

            if (_items.Count > 0)
                _items[_items.Count - 1].SetActive(true);
        }
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShooterProject.Scripts.Inventory
{
    public class InventoryPositionUpdate : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Transform cameraPosition;

        [SerializeField]
        private Vector3 offset;

        #endregion

        #region LifeCycle
        
        private void FixedUpdate()
        {
            transform.position = cameraPosition.position + offset;
        }

        #endregion
    }
}


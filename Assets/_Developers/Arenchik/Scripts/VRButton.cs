using UnityEngine;

public class VRButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<ConfigurableJoint>(out var configurableJoint))
            Debug.Log("asdas");
    }
}

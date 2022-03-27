using UnityEngine;
using UnityEngine.UI;

public class GetInventoryInfoForCanvas : MonoBehaviour
{
    #region Fields
    private Text text;

    #endregion

    #region LifeCycle

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    private void Update()
    {
        text.text = $"Магазинов на поясе: {InventoryInfo.ammoCount}\nНож: {InventoryInfo.hasKnife}";
    }

    #endregion
}

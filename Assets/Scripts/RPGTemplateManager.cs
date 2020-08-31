using UnityEngine.UI;
using UnityEngine;

public class RPGTemplateManager : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private GameObject inventorySystemCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventorySystemCanvas.gameObject.activeSelf)
            {
                inventorySystemCanvas.gameObject.SetActive(false);
            }
            else
            {
                inventorySystemCanvas.gameObject.SetActive(true);
            }
        }
    }
}

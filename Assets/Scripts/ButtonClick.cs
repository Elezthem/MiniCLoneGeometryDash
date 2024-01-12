using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        // Deactivate the panel at the start
        if (panelSettings != null)
        {
            panelSettings.SetActive(false);
        }

        // Attach the button click event
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }

        // Attach the close button click event
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnCloseClick);
        }
    }

    private void OnClick()
    {
        // Toggle the state of the panel
        if (panelSettings != null)
        {
            panelSettings.SetActive(!panelSettings.activeSelf);
        }
    }

    private void OnCloseClick()
    {
        // Close the panel
        if (panelSettings != null)
        {
            panelSettings.SetActive(false);
        }
    }
}

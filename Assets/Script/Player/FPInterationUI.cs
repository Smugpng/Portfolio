using Player;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FPInterationUI : MonoBehaviour
{
    public static FPInterationUI instance;

    [Header("Components")]
    [SerializeField] private bool showUI;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Camera")]
    private Camera mainCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        var rotation = mainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }
    private void OnValidate()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }
    public void SetUp(Transform pos, string prompt)
    {
        canvasGroup.alpha = 1.0f;
        transform.position = pos.position;
        text.text = prompt;
    }
    public void HideUI()
    {
        canvasGroup.alpha = 0f;
        transform.position = new Vector3(0,0,0);
        text.text = null;
    }
}

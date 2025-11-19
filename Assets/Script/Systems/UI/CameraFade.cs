using UnityEngine;
using UnityEngine.UI;
[ExecuteInEditMode]
public class CameraFade : MonoBehaviour
{
    public static CameraFade instance;
    [Header("UI Components")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Fade Stats")]
    private bool isfade;
    private float fadeAmount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }
    private void Update()
    {
        if (isfade)
        {
            canvasGroup.alpha += fadeAmount;
            Debug.Log("FADIOG");
        }
        else
        {
            StopFade();
        }
    }


    public void FadeIn()
    {
        fadeAmount = -.05f;
        isfade = true;
        Debug.Log("Negative Fade");
    }
    public void FadeOut()
    {
        Debug.Log("Postitive Fade");
        fadeAmount = .05f;
        isfade = true;
    }
    public void StopFade()
    {
        isfade = false;
        fadeAmount = 0;
    }

}

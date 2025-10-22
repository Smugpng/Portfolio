using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialougeText : MonoBehaviour
{

    #region Instance
    public static DialougeText instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DisableUI();
    }
    #endregion



    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI currentText,currentName;
    [SerializeField] private CanvasGroup currentCanvasGroup;
    [SerializeField] private float textSpeed = 50f;

    
    public void ActivateText(string textToType, string npcName)
    {
        currentCanvasGroup.alpha = 1;
        currentName.text = npcName;  
        StartCoroutine(AnimateText(textToType));
    }
    IEnumerator AnimateText(string textToType)
    {
        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            t += Time.deltaTime * textSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            currentText.text = textToType.Substring(0, charIndex);

            yield return null;
        }

        currentText.text = textToType;
    }

    public void DisableUI()
    {
        currentName.text = null;
        currentText.text = null;
        currentCanvasGroup.alpha = 0;
    }
}

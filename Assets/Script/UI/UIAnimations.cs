using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIAnimations : MonoBehaviour
{
    [SerializeField] private GameObject currentObject;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private RectTransform currentTransform;
    [SerializeField] private Vector2 size;

    [SerializeField] private AudioClip buttonPress;

    public UnityEvent onClickEvent;

   
    public void SizeUP(GameObject UIOBJ)
    {
        Debug.Log("HEOP");
        currentTransform = UIOBJ.GetComponent<RectTransform>();
        startPos = currentTransform.localPosition;
        currentTransform.LeanMoveLocal(new Vector3(startPos.x, startPos.y + 40), .25f).setEaseInOutBack();

    }
    public void SizeDOWN(GameObject UIOBJ)
    {
        currentTransform.LeanMoveLocal(new Vector3(startPos.x, startPos.y), .25f).setEaseInOutBack();
        if (size != new Vector2(0,0)) currentTransform.localScale = size;
    }
  
    public void CLICK(GameObject UIOBJ)
    {
        LeanTween.scale(currentTransform, currentTransform.localScale*.9f,.25f).setEaseInOutBack().setOnComplete(Return);
        //StartCoroutine(Click(UIOBJ));
    }
    public void Return()
    {
        LeanTween.scale(currentTransform, currentTransform.localScale / .9f, .25f).setEaseInOutBack().setOnComplete(OnClickEvent);
    }
    private void OnClickEvent()
    {
        onClickEvent.Invoke();
    }
    public void Drag()
    {
        currentTransform.LeanMoveLocal(Input.mousePosition, .25f).setEaseInOutBack();
        //currentTransform.position = Input.mousePosition;
    }
    public void Drop()
    {
        //LeanTween.move(currentTransform,new Vector3(startPos.x, startPos.y), .25f).setEaseInOutBack();
    }
    private IEnumerator Click(GameObject UIOBJ)
    {
        SFXManager.instance.PlaySFX(buttonPress);
        currentTransform = UIOBJ.GetComponent<RectTransform>();
        size = currentTransform.localScale;
        float t = 0;
        while (t < 1)
        {
            yield return null;
            t += Time.deltaTime * 10;
            currentTransform.localScale = Vector3.Lerp(size, (size*.90f), t);
        }
        currentTransform.localScale = size;
    }

}

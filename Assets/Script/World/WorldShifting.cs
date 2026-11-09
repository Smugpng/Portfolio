using Player;
using UnityEngine;

[ExecuteInEditMode]
public class WorldShifting : MonoBehaviour
{
    [Header("World Materials")]
    [SerializeField] private Material lightSkyBox, darkSkyBox;

    [Header("Fade Variables")]
    [SerializeField] private float dreamFog,realFog;
    private float lastFade;
    private bool isDreaming;

    public static WorldShifting instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        ToRealWorld();
    }
    public void ToDreamWorld()
    {
        RenderSettings.skybox = darkSkyBox;
        FogFade(dreamFog);
    }
    public void ToRealWorld()
    {
        RenderSettings.skybox = lightSkyBox;
        FogFade(realFog);
    }
    private void FogFade(float fadeTo)
    {

        if (LeanTween.isTweening(this.gameObject))
        {
            LeanTween.cancel(this.gameObject);
            RenderSettings.fogDensity = lastFade;
        }
        LeanTween.value(gameObject, RenderSettings.fogDensity, fadeTo, 1f).setOnUpdate((float val) => { RenderSettings.fogDensity = val; });
        lastFade = fadeTo;
    }
    [ContextMenu("Swap Worlds")]
    public void SwapWords(bool question)
    {
        isDreaming = question;
        Debug.Log("Hey");
        if (isDreaming) ToDreamWorld();
        else ToRealWorld();
    }

}

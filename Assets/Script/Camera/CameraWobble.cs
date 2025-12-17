using Player;
using Unity.Cinemachine;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    private FPControler FPControler;
    [SerializeField] private CinemachineBasicMultiChannelPerlin noiseChannel;
    private float verticalNoise, horizontalNoise;

    [Header("Player States")]
    public Horizontal horizontalState;
    public enum Horizontal
    {
        Standing,
        Walking,
        Running
    };
    public Vertical verticalState;
    public enum Vertical
    {
        Grounded,
        Falling,
        Rising
    };
    private void Awake()
    {
        FPControler = GetComponentInParent<FPControler>();
    }
    private void Update()
    {
        VerticalSates();
        HorizontalSates();
        switch (verticalState)
        {
            case Vertical.Grounded:
                verticalNoise = 0.1f;
                break;
            case Vertical.Falling:
                verticalNoise = 5f;
                break;
            case Vertical.Rising:
                verticalNoise = 0.05f;
                break;
        }
        switch (horizontalState)
        {
            case Horizontal.Standing:
                horizontalNoise = 0.1f;
                break;
            case Horizontal.Walking:
                horizontalNoise = .5f;
                break;
            case Horizontal.Running:
                horizontalNoise = 2f;
                break;
        }
        noiseChannel.AmplitudeGain = Mathf.Lerp(noiseChannel.AmplitudeGain, (verticalNoise + horizontalNoise), Time.deltaTime*2);
        noiseChannel.FrequencyGain = Mathf.Lerp(noiseChannel.FrequencyGain, (verticalNoise + horizontalNoise)/1.5f, Time.deltaTime*2);
        

    }
    private void VerticalSates()
    {
        float vert = FPControler.verticalVelocity;
        if(vert == -3)
        {
            verticalState = Vertical.Grounded;
        }
        else if(vert > -3)
        {
            verticalState = Vertical.Rising;
        }
        else if (vert < -10) //Used only for long falls
        {
            verticalState = Vertical.Falling;
        }
    }
    private void HorizontalSates()
    {
        float hori = FPControler.currentSpeed;
        if (hori == 0)
        {
            horizontalState = Horizontal.Standing;
        }
        else if (hori > 0.1f && hori <= 5f)
        {
            horizontalState = Horizontal.Walking;
        }
        else if (hori > 5.1f)
        {
            horizontalState = Horizontal.Running;
        }
    }
}

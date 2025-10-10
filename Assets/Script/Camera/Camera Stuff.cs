using UnityEngine;
using Unity.Cinemachine;
public class CameraStuff : MonoBehaviour
{
    [SerializeField]  CinemachineImpulseSource source;

    public void ScreeShake()
    {
        source.GenerateImpulse();
    }
}

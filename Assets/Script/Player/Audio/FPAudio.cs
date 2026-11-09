using UnityEngine;

public class FPAudio : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void playclip(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}

using UnityEngine;

public class AIAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    private void Awake()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        InvokeRepeating("PlaySound", 5, 20);
    }
    private void PlaySound()
    {
        int randomSound = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[randomSound]);
    }
}

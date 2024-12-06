using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _soundSource;

    [SerializeField] private AudioClip clickClip;
    [SerializeField] private AudioClip peepClip;

    private void Awake()
    {
        _soundSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        if (clickClip != null)
        {
            _soundSource.PlayOneShot(clickClip);
        }
    }

    public void PlayPeepSound()
    {
        if (peepClip)
        {
            _soundSource.PlayOneShot(peepClip);
        }
    }
}


using UnityEngine;

public class SoundManager : MonoBehaviour
{//Basic sound manager, this is used by the Dialogue manager to play sound clips when typing
   public static SoundManager instance { get; private set; }

    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound (AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
}

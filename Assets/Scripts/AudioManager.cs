using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] _audioSources;

    // Start is called before the first frame update
    void Start()
    {
        _audioSources = GetComponentsInChildren<AudioSource>();
        
        foreach(AudioSource source in _audioSources)
        {
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

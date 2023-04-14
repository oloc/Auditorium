using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private float _winConditionTime;

    private AudioSource[] _audioSources;
    private float _victoryTimer;

    // Start is called before the first frame update
    private void Start()
    {
        _audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource source in _audioSources)
        {
            source.Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        float volumesSum = 0f;
        foreach (AudioSource source in _audioSources)
        {
            volumesSum += source.volume;
        }

        if (volumesSum == _audioSources.Length && _victoryTimer < _winConditionTime)
        {
            _victoryTimer += Time.deltaTime;
        }
        else
        {
            _victoryTimer = 0f;
        }

        if (_victoryTimer >= _winConditionTime)
        {
            Debug.Log("VICTORY !!!");
        }

    }
}

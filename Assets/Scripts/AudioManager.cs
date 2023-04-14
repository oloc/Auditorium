using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private float _winConditionTime;

    private AudioSource[] _audioSources;
    private float _winningStartTime;
    private bool _victoryIsNear;

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

        if (volumesSum == _audioSources.Length)
        {
            if (!_victoryIsNear)
            {
                _victoryIsNear = true;
                _winningStartTime = Time.time;
            }
            else
            {
                if (Time.time >= _winningStartTime + _winConditionTime)
                {
                    Debug.Log("VICTORY !!!");
                }
            }
        }
        else
        {
            _victoryIsNear = false;
        }
    }
}

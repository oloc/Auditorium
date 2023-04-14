using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private float _winConditionTime;

    private AudioSource[] _audioSources;
    private float _winningStartTime;
    private bool _victoryIsNear;
    private List<float> _winList;

    // Start is called before the first frame update
    private void Start()
    {
        _audioSources = GetComponentsInChildren<AudioSource>();

        foreach (AudioSource source in _audioSources)
        {
            source.Play();
        }

        _winList = Enumerable.Repeat(1f, _audioSources.Length).ToList();
    }

    // Update is called once per frame
    private void Update()
    {
        List<float> volumes = new List<float>();
        foreach (AudioSource source in _audioSources)
        {
            volumes.Add(source.volume);
        }

        if (Enumerable.SequenceEqual(volumes, _winList))
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

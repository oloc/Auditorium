using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private Renderer[] _volumeBarRenderers = null;

    [Header("Materials")]
    [SerializeField]
    private Material _volumeOnMaterial;
    [SerializeField]
    private Material _volumeOffMaterial;

    [Header("Audio Parameters")]
    [Range(0, 1)]
    [SerializeField]
    private float _audioVolumeSensitivity;
    [Range(0, 1)]
    [SerializeField]
    private float _audioVolumeDecrease;
    [SerializeField]
    private float _delayBeforeVolumeDecrease;

    private float _lastParticuleCollisionTime;

    void Awake()
    {
        foreach (Renderer volumeBar in _volumeBarRenderers)
        {
            volumeBar.material = _volumeOffMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float volume = _audioSource.volume;
        if (Time.time > _lastParticuleCollisionTime + _delayBeforeVolumeDecrease)
        {
            if (volume > 0)
                _audioSource.volume -= _delayBeforeVolumeDecrease * Time.deltaTime;
        }

        for (int i = 0; i < _volumeBarRenderers.Length; i++)
        {
            float minVolume = (i + 1) * 0.2f;
            if (volume >= minVolume)
            {
                _volumeBarRenderers[i].material = _volumeOnMaterial;
            }
            else
            {
                _volumeBarRenderers[i].material = _volumeOffMaterial;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Particle"))
        {
            if (_audioSource.volume < 1)
                _audioSource.volume += _audioVolumeSensitivity;

            _lastParticuleCollisionTime = Time.time;
        }
    }

}

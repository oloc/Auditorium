using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ParticleController : MonoBehaviour
{
    [SerializeField]
    private float _speedMini;
    [SerializeField]
    private float _timeToFade;

    private MeshRenderer _meshRenderer;
    private Rigidbody2D _rigidBody;
    private bool _isFlaggedToDestroy;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (_rigidBody.velocity.sqrMagnitude <= _speedMini && !_isFlaggedToDestroy)
        {
            _isFlaggedToDestroy = true;
            StartCoroutine(IE_FadeOut(_timeToFade));
        }
    }

    private IEnumerator IE_FadeOut(float timeToFade)
    {
        float timer = 0f;
        Material material = _meshRenderer.material;
        while (timer < timeToFade)
        {
            float percent = timer / timeToFade;
            Color matColor = material.color;
            matColor.a = Mathf.Lerp(1f, 0f, percent);
            material.color = matColor;

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Color finalColor = material.color;
        finalColor.a = 0f;
        material.color = finalColor;

        Destroy(gameObject);
    }

    private object WaitForEndOfFrame()
    {
        throw new NotImplementedException();
    }
}

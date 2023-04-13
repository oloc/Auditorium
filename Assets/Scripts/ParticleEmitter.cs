using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [Header("Particle")]
    [SerializeField]
    private GameObject _particulePrefab;
    [SerializeField]
    private Vector2 _mouvementDirection;
    [SerializeField]
    [Range(0.02f, 0.08f)]
    private float _speed;

    [Header("Emitter")]
    [Range(0.1f, 1f)]
    [SerializeField]
    private float _radius;
    [Range(0.01f, 0.2f)]
    [SerializeField]
    private float _delayBetweenParticles;

    private float _nextParticuleTime;

    private void Awake()
    {
        _nextParticuleTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > _nextParticuleTime)
        {
            GenerateParticule();
            _nextParticuleTime = Time.time + _delayBetweenParticles;

        }
    }

    private void GenerateParticule()
    {
        Vector2 randomPosition = Random.insideUnitCircle * _radius + (Vector2)transform.position;
        GameObject newParticule = Instantiate(_particulePrefab, randomPosition, Quaternion.identity);
        Rigidbody2D particuleRigidBody = newParticule.GetComponent<Rigidbody2D>();
        particuleRigidBody.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

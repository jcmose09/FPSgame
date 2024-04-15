using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour 
{
	public float speed = 3.0f;  // Wandering forward speed
	public float obstacleRange = 2.0f;
    public float avoidanceRange = 5.0f; 
    public float fireballDelay = 1.0f; 
    public float Force = 70.0f;
    public Vector3 Torque = new Vector3(100, 0, 0);


    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;
    private bool _alive;

    void Start()
    {
        _alive = true;
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (_alive)
        {
            yield return new WaitForSeconds(fireballDelay);
            FireFireball();
        }
    }

    void Update()
    {
        if (!_alive) return;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.75f, transform.forward, out hit, obstacleRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                if (_fireball == null)
                {
                    FireFireball();
                }
            }
            else if (hit.distance < avoidanceRange)
            {
                AvoidObstacle(hit);
            }
        }

        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void AvoidObstacle(RaycastHit hit)
    {
        Vector3 targetDir = transform.position + transform.right * Random.Range(-1f, 1f);
        transform.rotation = Quaternion.LookRotation(targetDir - transform.position);
    }

    void FireFireball()
    {
        _fireball = Instantiate(fireballPrefab, transform.position + transform.forward * 1.5f, transform.rotation);
        Rigidbody rb = _fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * Force;
            rb.AddTorque(Torque);
        }
    }
    public void SetAlive(bool alive)
    {
        _alive = alive;
        if (!_alive && _fireball != null)
        {
            Destroy(_fireball);
        }
    }
}

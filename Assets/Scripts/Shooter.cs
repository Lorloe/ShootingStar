using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Basic Variables")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float firingRate = 0.2f;
    
    [Header("Enemy Bot Variables")]
    [SerializeField] private bool useRobot;
    [SerializeField] private float minimumFiringRate = 0.1f;
    [SerializeField] private float fireRateVariance = 0f;

    [HideInInspector] public bool isFiring;

    AudioManager audioManager;
    private Coroutine firingCoroutine;

    void Start()
    {
        audioManager = AudioManager.Instance;
        if (useRobot)
            isFiring = true;
    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
            firingCoroutine = StartCoroutine(FireContinuously()); // Instantiate the projectile and repeated
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    // Using for Enemy Bot to fire continuously with random firing rate
    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();

            projectile.transform.rotation = transform.rotation;

            // projectileRb.linearVelocityY = projectileSpeed * Time.deltaTime;
            projectileRb.linearVelocity = transform.up * projectileSpeed;

            Destroy(projectile, projectileLifetime);

            float waitTime = Random.Range(firingRate - fireRateVariance, firingRate + fireRateVariance);
            waitTime = Mathf.Clamp(waitTime, minimumFiringRate, float.MaxValue);

            audioManager.PlayShootingSFX();

            // yield return new WaitForSeconds(firingRate);
            yield return new WaitForSeconds(waitTime);
        }
    }
}

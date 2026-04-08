using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints = 100;
    [SerializeField] private ParticleSystem hitParticleEffect;

    [SerializeField] private bool isPlayer; // To differentiate between player and enemy for score keeping
    [SerializeField] private int scoreValue = 10; // Score value for destroying an enemy

    [SerializeField] private bool applyCameraShake;

    private CameraShake cameraShake;
    private AudioManager audioManager;
    private ScoreKeeper scoreKeeper;
    private LevelManager levelManager;

    public int GetHealth()
    {
        return healthPoints;
    }


    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = FindObjectOfType<AudioManager>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if hit a damage dealer

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());   // Reduce health
            damageDealer.Hit();

            HitParticleEffect();    // Show hit particle effect
            
            if (applyCameraShake)
            {
                cameraShake.Play(); // Apply camera shake if enabled
            }

            audioManager.PlayDamageExplosionSFX();   // Play damage explosion sound
        }
    }

    private void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        if (isPlayer)
        {
            levelManager.LoadGameOverScene();
        }
        else if (!isPlayer)
        {
            scoreKeeper.AddScore(scoreValue); // Add score for destroying an enemy
        }
        Destroy(gameObject);
        // Debug.Log("Object destroyed");
    }

    private void HitParticleEffect()
    {
        if (hitParticleEffect != null)
        {
            ParticleSystem particles = Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
            Destroy(particles, particles.main.duration + particles.main.startLifetime.constantMax);
        }
    }
}

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
    private bool isDestroyed;

    public int GetHealth()
    {
        return healthPoints;
    }


    void Start()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            cameraShake = mainCamera.GetComponent<CameraShake>();
        }

        audioManager = AudioManager.Instance;
        scoreKeeper = ScoreKeeper.Instance;

        if (levelManager == null)
        {
            levelManager = FindFirstObjectByType<LevelManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed)
        {
            return;
        }

        if (HandleShipCollision(other.gameObject))
        {
            return;
        }

        // Check if hit a damage dealer

        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            damageDealer.Hit();
            PlayHitFeedback();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        HandleShipCollision(other.gameObject);
    }

    public void TakeDamage(int damage)
    {
        if (isDestroyed)
        {
            return;
        }

        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        if (isDestroyed)
        {
            return;
        }

        isDestroyed = true;

        if (isPlayer)
        {
            if (levelManager != null)
            {
                levelManager.LoadGameOverScene();
            }
            else
            {
                Debug.LogError("LevelManager not found when trying to load GameOverScene.");
            }
        }
        else if (!isPlayer && scoreKeeper != null)
        {
            scoreKeeper.AddScore(scoreValue); // Add score for destroying an enemy
        }

        Destroy(gameObject);
    }

    private bool HandleShipCollision(GameObject otherObject)
    {
        if (isDestroyed || !isPlayer || !otherObject.CompareTag("Enemy"))
        {
            return false;
        }

        Health otherHealth = otherObject.GetComponent<Health>();
        if (otherHealth == null)
        {
            return false;
        }

        otherHealth.TakeDamage(otherHealth.GetHealth());
        PlayHitFeedback();
        TakeDamage(healthPoints);
        return true;
    }

    private void PlayHitFeedback()
    {
        HitParticleEffect();

        if (applyCameraShake && cameraShake != null)
        {
            cameraShake.Play();
        }

        if (audioManager != null)
        {
            audioManager.PlayDamageExplosionSFX();
        }
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

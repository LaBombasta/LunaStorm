using UnityEngine;

public class SpiralEffect : MonoBehaviour
{
    public ParticleSystem particleSystemPrefab; // Reference to the Particle System prefab
    public float spiralSpeed = 5f; // Speed of the spiral rotation
    public float spiralRadius = 2f; // Radius of the spiral

    private ParticleSystem particleSystemInstance;

    void Start()
    {
        // Instantiate the Particle System prefab
        particleSystemInstance = Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        // Get the particles from the Particle System
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystemInstance.main.maxParticles];
        int numParticlesAlive = particleSystemInstance.GetParticles(particles);

        // Update the position of each particle to create a spiral motion
        for (int i = 0; i < numParticlesAlive; i++)
        {
            particles[i].position = CalculateSpiralPosition(particles[i].startLifetime - particles[i].remainingLifetime);
        }

        // Set the updated particles back to the Particle System
        particleSystemInstance.SetParticles(particles, numParticlesAlive);
    }

    Vector3 CalculateSpiralPosition(float elapsedTime)
    {
        // Calculate the position of a point on the spiral based on elapsed time
        float angle = elapsedTime * spiralSpeed;
        float x = Mathf.Cos(angle) * spiralRadius * angle;
        float y = Mathf.Sin(angle) * spiralRadius * angle;
        return new Vector3(x, y, 0f);
    }
}

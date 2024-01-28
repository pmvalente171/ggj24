using UnityEngine;

namespace DefaultNamespace
{
    public static class ParticleManager
    {
        // Load particle from resources
        public static void PlayParticle(string particleName, Vector3 position)
        {
            var particle = Resources.Load<ParticleSystem>("Particles/" + particleName);
            var particleInstance = Object.Instantiate(particle, position, Quaternion.identity);
            particleInstance.Play();
        }
    }
}
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem[] AllParticles;
    // Start is called before the first frame update
    void Start()
    {
        AllParticles = GetComponentsInChildren<ParticleSystem>();
    }
    public void Play()
    {
        foreach(ParticleSystem ps in AllParticles)
        {
            ps.Stop();
            ps.Play();  
        }
    }

}

using UnityEngine;

public class LanderVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem RightThrusterParticleSystem;
    [SerializeField] private ParticleSystem MiddleThrusterParticleSystem;
    [SerializeField] private GameObject LanderObjectVfx;

    private Lander lander;
    private void Awake()
    {
        lander = GetComponent<Lander>();
        lander.OnUpForce += Lander_OnUpForce;
        lander.OnLeftForce += Lander_OnLeftForce;
        lander.OnRightForce += Lander_OnRightForce;
        lander.OnBeforeForce += Lander_OnBeforeForce;

        SetEnabledThrustleParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrustleParticleSystem(RightThrusterParticleSystem, false);
        SetEnabledThrustleParticleSystem(MiddleThrusterParticleSystem, false);
    }
    private void Start()
    {
        lander.OnLanded += Lander_OnLanded;
    }

    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        switch (e.landingType)
        {
            case Lander.LandingType.TooFastLanding:
            case Lander.LandingType.TooSteepAngle:
            case Lander.LandingType.WrongLandingArea:
            // Instantiate
            Instantiate(LanderObjectVfx, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            break;
        }
    }

    private void Lander_OnBeforeForce(object sender, System.EventArgs e)
    {
        SetEnabledThrustleParticleSystem(leftThrusterParticleSystem, false);
        SetEnabledThrustleParticleSystem(RightThrusterParticleSystem, false);
        SetEnabledThrustleParticleSystem(MiddleThrusterParticleSystem, false);
    }

    private void Lander_OnUpForce(object sender, System.EventArgs e)
    {
        SetEnabledThrustleParticleSystem(leftThrusterParticleSystem, true);
        SetEnabledThrustleParticleSystem(RightThrusterParticleSystem, true);
        SetEnabledThrustleParticleSystem(MiddleThrusterParticleSystem, true);
    }

    private void Lander_OnLeftForce(object sender, System.EventArgs e)
    {
       
        SetEnabledThrustleParticleSystem(RightThrusterParticleSystem, true);
        
    }

    private void Lander_OnRightForce(object sender, System.EventArgs e)
    {
        SetEnabledThrustleParticleSystem(leftThrusterParticleSystem, true);
        
    }

    private void SetEnabledThrustleParticleSystem(ParticleSystem particleSystem, bool enabled)
    {
        ParticleSystem.EmissionModule emissionModule = particleSystem.emission;
        emissionModule.enabled = enabled;
    }
}

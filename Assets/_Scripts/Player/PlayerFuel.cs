using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour
{
    // Reference Game Manager
    public GameManager m_GameManager;

    public ParticleSystem LowFuelParticles;

    public Image m_FuelBar;

    // Create variables needed to display and count the fuel
    private float m_Fuel = 36;
    public bool reduceFuel = false;

    void Start()
    {
        LowFuelParticles.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        var ParticleEmissions = LowFuelParticles.emission;

        if (m_Fuel <= 30)
        {
            LowFuelParticles.Play();
            ParticleEmissions.rateOverTime = -m_Fuel/2 + 15; // Cool Linear Function
        }
    }

    public IEnumerator DecreaseFuel()
    {
        while (true)
        {
            m_Fuel -= 3;
            //Debug.LogWarning("Fuel: " + m_Fuel);
            m_FuelBar.fillAmount = m_Fuel / 100;
            yield return new WaitForSeconds(2.5f);
        }
    }

    public IEnumerator FallToDeath()
    {
        yield return new WaitForSeconds(1);
    }
}

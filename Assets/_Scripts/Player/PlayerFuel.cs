using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour
{
    // Reference the Game Manager
    private AudioManager m_AudioManager;

    // Get the particles
    public GameObject explosion;
    public ParticleSystem LowFuelParticles;

    // Get the UI element
    public Image m_FuelBar;

    // Create variables needed to display and count the fuel
    private float m_Fuel = 102;
    public bool reduceFuel = false;

    private void Awake()
    {
        m_AudioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void Start()
    {
        LowFuelParticles.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        var ParticleEmissions = LowFuelParticles.emission;

        // Create particle effects for when the fuel bar gets low
        if (m_Fuel <= 30)
        {
            LowFuelParticles.Play();
            ParticleEmissions.rateOverTime = -m_Fuel + 30;

            if (m_Fuel <= 0)
            {
                // Explode the player once the fuel drops too low
                Instantiate(explosion, transform.position, transform.rotation);
                m_AudioManager.ExplosionAudio.Play();
                gameObject.SetActive(false);
            }
        }
        else
        {
            LowFuelParticles.Pause();
            LowFuelParticles.Clear();
        }
    }

    // Decrease the fuel by x amount every y seconds
    public IEnumerator DecreaseFuel()
    {
        while (reduceFuel == true)
        {
            m_Fuel -= 3;
            m_FuelBar.fillAmount = m_Fuel / 100;
            yield return new WaitForSeconds(1.25f);
        }
    }

    public void IncreaseFuel(float amount)
    {
        if (m_Fuel + amount <= 100)
        {
            m_Fuel += amount;
        }
        else
        {
            m_Fuel = 100;
        }
        m_FuelBar.fillAmount = m_Fuel / 100;
    }
}

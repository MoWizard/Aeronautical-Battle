using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour
{
    // Reference Game Manager
    public GameManager m_GameManager;

    public Image m_FuelBar;

    private float m_Fuel = 103;

    public bool reduceFuel = false;

    public IEnumerator DecreaseFuel()
    {
        while (true)
        {
            m_Fuel -= 3;
            Debug.LogWarning("Fuel: " + m_Fuel);
            m_FuelBar.fillAmount = m_Fuel / 100;
            yield return new WaitForSeconds(2.5f);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}

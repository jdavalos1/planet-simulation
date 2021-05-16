using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;
    public float maxEnergy;
    public float depletionRatio;
    public float cellIncrease;
    public float movementEnergyDecay;
    public float jumpEnergyDecay;
    public float boostEnergyDecay;
    public GameObject gameOverUI;
    public GameObject[] ingameUIs;

    private float energy;

    void Start()
    {
        energy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = energy;
    }
    private void LateUpdate()
    {
        energy -= depletionRatio;
        energyBar.value = energy;

        DecreaseOnMovement();

        if (energyBar.value <= 0)
        {
            EndGame();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("Item"))
        {
            energy += cellIncrease;
            energyBar.value = energy;
            Destroy(hit.gameObject);
        }
    }

    void DecreaseOnMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if(x != 0 || y != 0)
        {
            energy -= movementEnergyDecay * Time.deltaTime;
            energyBar.value = energy;
        }
    }
    public void DecreaseOnJump()
    {
        energy -= jumpEnergyDecay;
        energyBar.value = energy;
    }
    
    public void DecreaseOnBoost()
    {
        energy -= boostEnergyDecay * Time.deltaTime;
        energyBar.value = energy;
    }

    void EndGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        gameOverUI.SetActive(true);
        Array.ForEach(ingameUIs, go => go.SetActive(false));
        Time.timeScale = 0f;
    }
}
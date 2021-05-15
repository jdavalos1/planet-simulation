using System.Collections;
using System.Collections.Generic;
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

    private float energy;

    void Start()
    {
        energy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = energy;
    }
    private void Update()
    {
        energy -= depletionRatio;
        energyBar.value = energy;

        if (energyBar.value <= 0) Debug.Log("Out of energy");
        DecreaseOnMovement();
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
            energy -= movementEnergyDecay;
            energyBar.value = energy;
        }
    }
    void DecreaseOnJump()
    {
        if (Input.GetKeyDown("Jump"))
        {

        }
    }
}

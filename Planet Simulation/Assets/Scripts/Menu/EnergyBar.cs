using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private void LateUpdate()
    {
        energy -= depletionRatio;
        energyBar.value = energy;

        DecreaseOnMovement();

        if (energyBar.value <= 0)
        {
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
            energy -= movementEnergyDecay;
            energyBar.value = energy;
        }
    }
    public void DecreaseOnJump()
    {
        energy -= jumpEnergyDecay;
        energyBar.value = energy;
    }
}
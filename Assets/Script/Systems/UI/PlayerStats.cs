using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Image staminaBar;
    private float maxStamina,currentStamina,staminaFill;
    void Start()
    {
        maxStamina = FPControler.instance.climbMaxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        currentStamina = FPControler.instance.climbTimer/maxStamina;
        staminaFill = Mathf.Lerp(staminaFill, currentStamina, Time.deltaTime * 10);
        staminaBar.fillAmount = staminaFill;
    }
}

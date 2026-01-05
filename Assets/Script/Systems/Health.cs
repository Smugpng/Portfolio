using UnityEngine;

public interface IHealth
{
    [Header("Charater Stats")]
    public float MaxHealth { get; }
    public float CurrHealth { get;}


    //Functions are not set inside this Interface incase I want diffrent things to happen to diffrent gameObjects using said interface;
    public void Heal(float health);
    public void DMGTaken(float health);
    public void Death();
}

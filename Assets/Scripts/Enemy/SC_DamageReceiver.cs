using UnityEngine;

public class SC_DamageReceiver : MonoBehaviour, IEntity
{
    //This script will keep track of player HP
    public float playerHP = 100;
    public int playerPoints = 0;
    public SC_CharacterController playerController;
    public SC_WeaponManager weaponManager;

    private bool onPause = false;

    public void ApplyDamage(float points)
    {
        playerHP -= points;

        if (playerHP <= 0)
        {
            //Player is dead
            playerController.canMove = false;
            playerHP = 0;
        }
    }

    public void ApplyHeal(float points)
    {
        playerHP += points;

        if (playerHP > 100)
        {
            playerHP = 100;
        }
    }

    public void ApplyPoints(int points)
    {
        playerPoints += points;
    }


    public bool IsOnPause()
    {
        return onPause;
    }

    public void SetOnPause(bool mode)
    {
        onPause = mode;
    }

    public void SetBullets()
    {
        weaponManager.SetBullets();
    }
}
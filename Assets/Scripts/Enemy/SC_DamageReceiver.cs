using UnityEngine;

public class SC_DamageReceiver : MonoBehaviour, IEntity
{
    //This script will keep track of player HP
    public float playerHP = 100;
    public int playerPoints = 0;
    public SC_CharacterController playerController;
    public SC_WeaponManager weaponManager;

    private bool onPause = false;
    private bool isSlowMotionUnlocked = false;
    private int hpTotalDeafault = 100;

    public static SC_DamageReceiver Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public int GetHpDefault()
    {
        return hpTotalDeafault;
    }

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

        if (playerHP > hpTotalDeafault)
        {
            playerHP = hpTotalDeafault;
        }
    }

    public void RestoreHealth()
    {
        playerHP = hpTotalDeafault;
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

    public void IncreaseBullets()
    {
        weaponManager.IncreaseBullets();
    }

    public void IncreaseHP()
    {
        hpTotalDeafault += 25;
    }

    public void UnlockSecondaryWeapon()
    {
        weaponManager.UnlockSecondaryWeapon();
    }

    public void UnlockSlowMotion()
    {
        isSlowMotionUnlocked = true;
    }

    public bool checkSlowMotion()
    {
        return isSlowMotionUnlocked;
    }
}
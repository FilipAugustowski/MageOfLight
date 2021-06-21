using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaTracker : MonoBehaviour
{
    /* Access UI */
    public float mana = 100;

    public static ManaTracker Instance;

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        UIManager.Instance.SetupMana(mana);
    }

    public void DeductMana(float _manaCost)
    {
        mana -= _manaCost;

        if (mana < 0) mana = 0;

        //int truncatedMana = (int)mana;

        SendManaToUI();

    }

    public void ManaRegen(float _regenAmount)
    {
        if(mana < 100)
        {
            mana += _regenAmount;
        }
        else
        {
            mana = 100;
        }

        SendManaToUI();

    }

    private void SetMana()
    {
        int truncatedMana = (int)mana;

    }

    public float GetCurrentMana()
    {
        return mana;
    }

    public void ResetMana()
    {
        mana = 100;
        SetMana();
    }

    private void SendManaToUI()
    {
        UIManager.Instance.UpdateMana(mana);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public int experience;
    public int coin;
    public LeftCubeLevel leftcube;

    //SetUp singular
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void GainExperience(int exp)
    {
        experience += exp;
    }

    public void UseExperience(int exp)
    {
        experience -= exp;
    }

    public void GainCoin(int amo)
    {
        coin += amo;
    }

    public void UseCoin(int amo)
    {
        coin -= amo;
    }

}

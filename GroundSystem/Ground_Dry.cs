using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Dry : Ground 
{
    public Seed weeds1;
    public float weeds1Posbility;

    public Seed weeds2;
    public float weeds2Posbility;

    public Seed tree;
    public float treePosbility;

    public int daysToGrass;
    public int curDay;
    public float grassPosNormalDay;
    public float grassPosRainDay;
    

    public override void Update_byday()
    {
        if (isOccupied)
        {
            //If there's something on ground
            if (placedType == PlacedType.SEED)
            {   //if this thing is a seed, then update it's status
                this.transform.GetComponentInChildren<SeedState>().UpdateStates(groundType);
            }
        }
        else
        {
            //If there ain't no nothing on the ground, plant tree, weeds and mushroom with there own possibility, which can be set in inspector
            float temp = Random.Range(0f, 101f);
            if (temp < treePosbility)
            {
                Debug.Log("tree");
                PlantSeed(tree);
            }
            else if (temp < treePosbility + weeds1Posbility)
            {
                Debug.Log("weed");

                PlantSeed(weeds1);
            }
            else if (temp < treePosbility + weeds1Posbility + weeds2Posbility)
            {
                Debug.Log("weed2");

                PlantSeed(weeds2);
            }
        }

        if (!isOccupied)
        {
            //If still haven't planted anything on it, it's been 10 days, then it should start 
            if (curDay >= 10)
            {
                float rantemp = Random.Range(0f, 101f);
                if (GameManager.instance.weather == Weather.RAIN)
                {
                    if (rantemp <= grassPosRainDay)
                        GroundManager.instance.SwapGround(transform.gameObject, GroundType.GRASS);
                    return;
                }
                else
                {
                    if (rantemp <= grassPosNormalDay)
                        GroundManager.instance.SwapGround(transform.gameObject, GroundType.GRASS);
                    return;
                }
            }
        }

        if(curDay < 10)
        {
            curDay++;
        }
    }
}

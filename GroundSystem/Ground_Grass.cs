using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ground_Grass : Ground
{
    public Seed weeds1;
    public float weeds1Posbility;

    public Seed weeds2;
    public float weeds2Posbility;

    public Seed tree;
    public float treePosbility;


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
            float temp = Random.Range(0f, 101f);
            if(temp < treePosbility)
            {
                PlantSeed(tree);
            } else if(temp < treePosbility + weeds1Posbility)
            {
                PlantSeed(weeds1);
            }
            else if(temp < treePosbility + weeds1Posbility + weeds2Posbility)
            {
                PlantSeed(weeds2);
            }
        }
    }

    


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Ready : Ground
{
    public override void Update_byday()
    {
        if (!isOccupied)
        {   //if there's nothing on that ground, switch this ground to dry ground, because it should be dry today
            GroundManager.instance.SwapGround(this.gameObject, GroundType.DRY);
        } else if (GameManager.instance.weather == Weather.RAIN)
        {
            //if today is rainny day, ready ground should switch to wet ground, because, it's wet.
            GameObject newGround = GroundManager.instance.SwapGround(this.gameObject, GroundType.WET);
            //Then update the seed's state.

            newGround.transform.GetComponentInChildren<SeedState>().UpdateStates(groundType);
        }
        else
        {
            //If seed is planted, but not rainy day, we still need to update seedstate.
            transform.GetComponentInChildren<SeedState>().UpdateStates(groundType);
        }
    }
}

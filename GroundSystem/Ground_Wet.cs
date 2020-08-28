using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Wet : Ground
{
    public override void Update_byday()
    {
        if (isOccupied)
        {
            if(placedType == PlacedType.SEED)
            {
                transform.GetComponentInChildren<SeedState>().UpdateStates(groundType);
            }
        }
        //Wet ground should switch back to Ready ground if there's no rain.
        if(GameManager.instance.weather != Weather.RAIN)
        {
            GroundManager.instance.SwapGround(this.gameObject, GroundType.READY);
        }
    }
}

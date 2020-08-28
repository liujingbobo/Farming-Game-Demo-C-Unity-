using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public int groundid;
    public bool isOccupied;
    public PlacedType placedType;
    public GroundType groundType;

public virtual void Update_byday() { }

    public bool PlantSeed(Seed seed)
    {
        if (isOccupied)
        {
            return false;
        } else if((groundType & seed.plantableGround) == groundType)
        {
            GameObject temp = Instantiate(seed.thisPrefab, transform.position + ((seed.offset == null) ? Vector3.zero : seed.offset), Quaternion.identity, transform);
            transform.GetComponent<Ground>().isOccupied = true;
            transform.GetComponent<Ground>().placedType = PlacedType.SEED;
            return true;
        } else
        {
            return false;
        }
    }

    //private void OnMouseEnter()
    //{
    //    this.GetComponent<Outline>().enabled = true;
    //}

    //private void OnMouseExit()
    //{
    //    this.GetComponent<Outline>().enabled = false;
    //}
}


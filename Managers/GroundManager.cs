using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public static GroundManager instance;
    public GameObject grounds;
    [Space]
    public GameObject dry_ground_prefab;
    public GameObject wet_ground_prefab;
    public GameObject grass_ground_prefab;
    public GameObject ready_ground_prefab;

    public List<Transform> groundList;
    public int row;
    public int column;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < grounds.transform.childCount; i++)
        {
            groundList.Add(grounds.transform.GetChild(i));
        }

        groundList = groundList.OrderByDescending(temp => temp.transform.position.z).ThenByDescending(temp => transform.position.x).ToList();

        for (int i = 0; i < groundList.Count; i++)
        {
            groundList[i].GetComponent<Ground>().groundid = i;
        }
    }

    public void Update_ByDay()
    {
        int temp = grounds.transform.childCount;

        for (int i = 0; i < temp; i++)
        {
            grounds.transform.GetChild(i).GetComponent<Ground>().Update_byday();
        }
    }

    public void InstantiateItem(Plant plant, int amount)
    {
            
    }


    //TODO
    public void PlaceFurniture(GameObject ground)
    {

    }

    public GameObject SwapGround(GameObject thisground, GroundType ground_type)
    {
        GameObject newGround = Instantiate(GetGround(ground_type), thisground.transform.position, Quaternion.identity, thisground.transform.parent);
        SwapGroundInfo(thisground, newGround);
        Destroy(thisground.gameObject);
        return newGround;
    }

    public GameObject GetGround(GroundType ground_type)
    {
        switch (ground_type)
        {
            case GroundType.DRY:
                return dry_ground_prefab;
            case GroundType.GRASS:
                return grass_ground_prefab;
            case GroundType.READY:
                return ready_ground_prefab;
            case GroundType.WET:
                return wet_ground_prefab;
            default:
                return null;
        }
    }

    public void SwapGroundInfo(GameObject oldGround, GameObject newGround)
    {
        newGround.GetComponent<Ground>().isOccupied = oldGround.GetComponent<Ground>().isOccupied;
        newGround.GetComponent<Ground>().placedType = oldGround.GetComponent<Ground>().placedType;
        newGround.GetComponent<Ground>().groundid = oldGround.GetComponent<Ground>().groundid;
        groundList[newGround.GetComponent<Ground>().groundid] = newGround.transform;
        if (oldGround.transform.childCount == 1)
        {
            oldGround.transform.GetChild(0).transform.SetParent(newGround.transform);
        }
    }

    public void SelectSquareGround(List<Transform> selectedList, PositionType pos, int width, int length, int thisground)
    {
        int pivit = 0;
        int widthafter = 0;
        int lengthafter = 0;

        switch (pos)
        {   //thisground 101, width 7, width/2 = 3, 
            case PositionType.DOWN:
                //1 - 2 = -1
                if ((thisground % 100 - width / 2)<0)
                {
                    //5 - (2 - 1) = 4;
                    //7 - (3 - 1) = 5;
                    widthafter = width - (width / 2 - thisground % 100);
                    pivit = thisground - thisground % 100; // 101 -> 101 - 1 = 100
                }
                else if((thisground % 100 + width / 2) > 99)
                {
                    //99
                    //98, width 5 
                    // 5 - 2 + 99 -98 = 4
                    // 7 - 3 + 99 - 98 = 5
                    widthafter = width - width / 2 + 99 - thisground % 100;
                    pivit = thisground - width / 2;
                }
                else
                {
                    pivit = thisground - width / 2;
                    widthafter = width;
                }

                if ((thisground / 100 + length - 1) > 99)
                {
                    lengthafter = 99 - thisground / 100 + 1;
                }
                else
                {
                    lengthafter = length;
                }

                for (int i = 0; i < lengthafter; i++)
                {
                    for (int m = 0; m < widthafter; m++)
                    {
                        selectedList.Add(groundList[pivit + m + i * 100]);
                    }
                }
                //break;
                break;
            case PositionType.UP:
                if ((thisground % 100 - width / 2) < 0)
                {
                    //5 - (2 - 1) = 4;
                    //7 - (3 - 1) = 5;
                    widthafter = width - (width / 2 - thisground % 100);
                    pivit = thisground - thisground % 100; // 101 -> 101 - 1 = 100
                }
                else if ((thisground % 100 + width / 2) > 99)
                {
                    //99
                    //98, width 5 
                    // 5 - 2 + 99 -98 = 4
                    // 7 - 3 + 99 - 98 = 5
                    widthafter = width - width / 2 + 99 - thisground % 100;
                    pivit = thisground - width / 2;
                }
                else
                {
                    pivit = thisground - width / 2;
                    widthafter = width;
                }

                if ((thisground / 100 - length + 1) < 0)
                {
                    lengthafter = thisground / 100 + 1;
                }
                else
                {
                    lengthafter = length;
                }

                for (int i = 0; i < lengthafter; i++)
                {
                    for (int m = 0; m < widthafter; m++)
                    {
                        selectedList.Add(groundList[pivit + m - i * 100]);
                    }
                }
                break;
            case PositionType.LEFT:
                if ((thisground / 100 - width / 2) < 0)
                {
                    widthafter = width / 2 + 1 + thisground / 100;
                    pivit = thisground - 100 * (thisground/100); // 101 -> 101 - 1 = 100
                }
                else if ((thisground / 100 + width / 2) > 99)
                {
                    // 7 - 3 + 99 - 98
                    widthafter = width - width / 2 + 99 - thisground / 100;
                    pivit = thisground - 100 * (width / 2);
                }
                else
                {
                    pivit = thisground - 100 * (width / 2);
                    widthafter = width;
                }

                if ((thisground % 100 - length + 1) < 0)
                {
                    lengthafter = thisground % 100 + 1;
                }
                else
                {
                    lengthafter = length;
                }

                for (int i = 0; i < widthafter; i++)
                {
                    for(int m = 0; m < lengthafter; m++)
                    {
                        selectedList.Add(groundList[pivit - m + i * 100]);
                    }
                }
                break;
            case PositionType.RIGHT:
                if ((thisground / 100 - width / 2) < 0)
                {
                    widthafter = width / 2 + 1 + thisground / 100;
                    pivit = thisground - 100 * (thisground / 100); // 101 -> 101 - 1 = 100
                }
                else if ((thisground / 100 + width / 2) > 99)
                {
                    // 7 - 3 + 99 - 98
                    widthafter = width - width / 2 + 99 - thisground / 100;
                    pivit = thisground - 100 * (width / 2);
                }
                else
                {
                    pivit = thisground - 100 * (width / 2);
                    widthafter = width;
                }

                if ((thisground % 100 + length - 1) > 99)
                {
                    lengthafter = 99 - thisground % 100 + 1;
                }
                else
                {
                    lengthafter = length;
                }

                for (int i = 0; i < widthafter; i++)
                {
                    for (int m = 0; m < lengthafter; m++)
                    {
                        selectedList.Add(groundList[pivit + m + i * 100]);
                    }
                }
                break;
        }


    }
}

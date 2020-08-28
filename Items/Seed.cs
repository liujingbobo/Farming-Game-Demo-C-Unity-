using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Seed", menuName = "Items/Seed")]
public class Seed : ScriptableObject
{
    public int id;
    public GameObject thisPrefab;
    public Vector3 offset;
    public int bearableday;
    public GroundType preferGround;
    public GroundType plantableGround;
    public int totalStates; // Starts from 0, the last state is death state. For example, if totalstates is 4, then it has 5 states in total. [0,1,2,3,4], [4]is death state, [3] is final state. Final state is 
    public Plant plant;
    [Space]
    public bool regrowable;
    public int regrowState;
    public int regrowTimes;
    public Item thisItem;
    public int harvestAmount;
    public GameObject plantOnWorld;
    public GameObject seedOnWorld;
}

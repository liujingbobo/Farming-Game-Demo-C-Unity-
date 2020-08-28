using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeedState : MonoBehaviour
{
    public bool isDead;
    public int regrowTimes;
    public int curState;
    public int daysTillDie;
    public bool readyForHarvest;

    public Seed seed;
    public bool isPlaying;

    
    public void UpdateStates(GroundType ground_type)
    {
        if (!isDead)
        {
            if(daysTillDie == 1 && (seed.preferGround & ground_type) != ground_type)
            {
                //If on dislike ground and was one day till die, it should die
                transform.GetChild(curState).gameObject.SetActive(false);
                transform.GetChild(seed.totalStates).gameObject.SetActive(true);
                isDead = true;
                curState = seed.totalStates;
            } //if on prefer ground and still not the last state yet
            else if((seed.preferGround & ground_type) == ground_type && curState < seed.totalStates -1)
            {

                daysTillDie = seed.bearableday;
                Grow();
                if (curState == seed.totalStates - 1)
                    readyForHarvest = true;
            } else if(curState == seed.totalStates - 1)
            {   //If this seed is ready for harvest, then do nothing even not on prefer ground
                readyForHarvest = true;
                return;
            }
            else
            {   //If seed is not ready for harvest and not on prefer ground, daystilldie should decrease by one.
                daysTillDie--;
            }
        }
    }

    public void Grow()
    {
        //    if(curState != seed.totalStates - 1)
        //    {
        transform.GetChild(curState).gameObject.SetActive(false);
            curState++;
            transform.GetChild(curState).gameObject.SetActive(true);
        //}
    }

    public void Harvest()
    {
        if (!isPlaying)
        {
            DOTween.Init();
            if (!isDead)
            {
                if (seed.regrowable)
                {
                    if (regrowTimes < seed.regrowTimes)
                    {
                        transform.DOShakePosition(1, 1, 1, 0, false, true);
                        StartCoroutine(CreateItemOnWorld(false));
                        //Shake it
                        //Drop item 
                        //Set state to regrowstate
                    }
                    else
                    {
                        //Scale = 0
                        //Drop Item
                        //Destroyself
                        //SetGround to not occupied
                    }
                }
                else
                {
                    //StartCoroutine(DestroySelf());
                    //Scale = 0
                    //Drop Item
                    //Destroyself
                    //SetGround to not occupied
                }
            }
            else
            {
                StartCoroutine(CreateItemOnWorld(true));
                //Scale = 0
                //Drop Seed
                //Destroyself
                //SetGround to not occupied
            }
            //TODO
        }

    }

    IEnumerator CreateItemOnWorld(bool isSeed)
    {
        isPlaying = true;
        GameObject harvestItem;
        if (isSeed)
            harvestItem = Instantiate(seed.seedOnWorld, transform.position, Quaternion.identity);
        else
            harvestItem = Instantiate(seed.plantOnWorld, transform.position, Quaternion.identity);
        Vector3 oldscale = harvestItem.transform.localScale;
        harvestItem.transform.localScale = new Vector3(0, 0, 0);
        Tween myTween = harvestItem.transform.DOScale(oldscale, 0.2f);

        yield return myTween.WaitForCompletion();

        yield return StartCoroutine(harvestItem.GetComponent<ItemOnWorld>().OnHarvest(transform.position));
        isPlaying = false;
    }


    //IEnumerator DestroySelf(IEnumerator newEnumerator)
    //{
    //    Tween myTween = transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    //    yield return myTween.WaitForCompletion();
    //    //TODO
    //    Destroy(this.gameObject);
    //}
}

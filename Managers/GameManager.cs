using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalDay = 0;
    public int date;
    public int year;
    public Season season;
    public Weather weather;
    [Space]
    public Canvas canvas;
    public bool awakeSetting;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            awakeSetting = false;
            Transform[] allCanvas = canvas.GetComponentsInChildren<Transform>();
            for (int i = 1; i < canvas.transform.childCount; i++)
            {
                allCanvas[i].gameObject.SetActive(false);
            }
        }
    }

    public void OpenSetting()
    {
        Time.timeScale = 0;
    }

    public void Update_ByDay()
    {
        Update_Date();
        Update_Scene();
    }

    public void Update_Date()
    {
        totalDay++;
        year = 2020 + (int)Mathf.Floor(totalDay / 90f);
        int temp = totalDay % 118;
        if (temp <= 30)
        {
            date = temp;
            season = Season.SPRING;
        }
        else if (temp <= 58)
        {
            date = temp - 30;
            season = Season.SUMMER;

        }
        else if (temp <= 88)
        {
            date = temp - 58;
            season = Season.AUTUMN;
        }
        else
        {
            date = temp - 88;
            season = Season.WINTER;
        }
    }

    public void Update_Scene()
    {
        float temp = Random.Range(0, 100);
        switch (season)
        {
            case Season.SPRING:
                if(temp <= 70)
                {
                    weather = Weather.NORMAL;
                }
                else
                {
                    weather = Weather.RAIN;
                }
                break;
            case Season.SUMMER:
                if (temp <= 90)
                {
                    weather = Weather.NORMAL;
                }
                else
                {
                    weather = Weather.RAIN;
                }
                break;
            case Season.WINTER:
                if (temp <= 60)
                {
                    weather = Weather.NORMAL;
                }
                else
                {
                    weather = Weather.SNOW;
                }
                break;
            case Season.AUTUMN:
                if (temp <= 90)
                {
                    weather = Weather.NORMAL;
                }
                else
                {
                    weather = Weather.RAIN;
                }
                break;
            default:
                break;
        }

    }
}

public enum Manager
{
    INVENTORY,
    FISH,
    MEAL,
    GROUND
}

public enum PlacedType
{
    SEED,
    FURNITURE
}

public enum GroundType
{
    DRY = 1,
    WET = 2,
    GRASS = 4,
    READY = 8,
    DRY_GRASS = 5,
    READY_WET = 10
}

public enum Weather
{
    RAIN,
    SNOW,
    NORMAL
}

public enum Season
{
    WINTER,
    SPRING,
    SUMMER,
    AUTUMN
}

public enum ItemType
{
    NONE,
    SEED,
    FURNITURE,
    AXE,
    SWORD,
    HOE,
    ROD,
    WATERINGCAN,
    SHOVEL
}

public enum PositionType
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}



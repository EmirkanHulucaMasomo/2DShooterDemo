using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> SpawnLoc;
    [SerializeField]
    private GameObject ZombiePrefab;
    

    public List<GameObject> spawnedZombieList;

    public int LevelCounter;
    public int PointCounter;
    public static int EnemyCounter;
    public static int spawnedCounter;
    public int CoinCounter;
    public int HighScore;

   

    private bool funcCheck1;
    public bool funcCheck2;
    public bool buffTaken;
    private State state;

    public int enemyc;
    public int spawnc;
    private enum State
    {
        Wait,
        StartOfTheLevel,
        EndOfTheLevel,
        InLevel,
        BuffPicking,
    }

    //public SavebleInfo savebleInfo;

    void Start()
    {
        Time.timeScale = 1f;
        EnemyCounter= 0;
        spawnedCounter= 0;
        //savebleInfo = new SavebleInfo(PointCounter, PointCounter, CoinCounter);
        //savebleInfo = FileHandler.ReadFromJson<SavebleInfo>("SaveableInfo.json");
        
        LevelCounter = 1;
        state = State.Wait;

    }


    void Update()
    {
        enemyc = EnemyCounter;
        spawnc= spawnedCounter;
        Debug.Log(state);
        if (Input.GetKeyDown(KeyCode.H))
        {
            buffTaken= true;
            
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            
        }
        //Debug.Log(state);
        
        switch (state)
        {
            case State.Wait:
                StartCoroutine(EventDelayer(1f, () => {
                    if (LevelCounter == 1 || LevelCounter == 3 || LevelCounter == 5 || LevelCounter == 7)
                    {
                        state = State.StartOfTheLevel;
                    }
                    else
                    {
                        state = State.StartOfTheLevel;
                    }

                }));
                break;
            case State.StartOfTheLevel:
                if (funcCheck1 == false)
                {
                    SpawnEnemy();
                    funcCheck1 = true;
                }

                state = State.InLevel;
                break;
            case State.InLevel:
                if (EnemyCounter == 0)
                {
                    if(funcCheck2== false)
                    {
                        LevelCounter++;
                        funcCheck2= true;
                    }
                    
                    state = State.EndOfTheLevel;
                }

                break;
            case State.EndOfTheLevel:
                funcCheck1 = false;
                funcCheck2 = false;
                buffTaken = false;
                //savebleInfo.Score = PointCounter;
                //savebleInfo.Coins += PointCounter;
                //FileHandler.SaveToJson<SavebleInfo>(savebleInfo, "SaveableInfo.json");
                state = State.Wait;
                break;
            
        }

    }

    private void SpawnEnemy()
    {
        //spawnedZombieList = new List<GameObject>();
        switch (LevelCounter)
        {
            case 1:
                //2
                for (int x = 0; x < 2; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case 2:
                //5
                for (int x = 0; x < 5; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case 3:
                //8
                for (int x = 0; x < 8; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case 4:
                //11
                for (int x = 0; x < 11; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case 5:
                //15
                for (int x = 0; x < 15; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case 6:
                //20
                for (int x = 0; x < 20; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case 7:
                //25
                for (int x = 0; x < 25; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case 8:
                //30
                for (int x = 0; x < 30; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
            case >= 9:
                for (int x = 0; x < 40; x++)
                {
                    GameObject spawnedSoldier = Instantiate(ZombiePrefab, SpawnLoc[0]);
                    spawnedZombieList.Add(spawnedSoldier);
                    spawnedCounter++;
                }
                break;
        }
        EnemyCounter = spawnedCounter;
    }

    

    

    IEnumerator EventDelayer(float delay, Action onActionComplete)
    {
        yield return new WaitForSeconds(delay);
        onActionComplete();
    }
}

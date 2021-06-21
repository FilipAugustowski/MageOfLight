using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    protected EC_EnemyManager[] orderedMonsters;
    //made LightManger public to test Illumination
    protected LightManager lightManager;

    protected NarrativeManager narrativeManager;

    protected EndPoint endPoint;

    protected PC_UIManager uiManager;

    public int myLvl;

    /* Objective Logic */
    protected int currObjective;

    public static LvlManager Instance;

    protected bool playerInCombat;

    public PC_PlayerManager playerManager;

    public int minNumberOfMobsToKill = 5;

    public GameObject player;

    public Vector3 respawn;
    public Vector3 newRot;

    protected TransformationControl transformationControl;

    public int currentCheck = -1;
    int mobA_count = 0;
    int mobB_count = 0;
    int mobC_count = 0;
    int mobE_count = 0;
    int backupMobsKilled = 0;
    public EnemySpawning RespawnerA;
    public EnemySpawning RespawnerB;
    public EnemySpawning RespawnerC;
    public EnemySpawning RespawnerE;

    public EC_EnemyManager[] enemyList;
    public Vector3[] spawnLocations;
    public bool[] mobLifes;
    public int numEnemies;

    int mobsKilled;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        currObjective = 0;

        playerInCombat = false;
        playerManager = FindObjectOfType<PC_PlayerManager>();
        uiManager = FindObjectOfType<PC_UIManager>();
        transformationControl = FindObjectOfType<TransformationControl>();
        player = GameObject.FindGameObjectWithTag("FullPlayer");
        respawn = player.transform.position;
        FindMonsters();
        //SetupMobs();
        Invoke("SetupMobs", 0.1f);

        //UpdateNumbers();
        //SetUpSpawners();

    }

    protected void SetupLvlManager(int _myLvl)
    {
        lightManager = LightManager.Instance;
        narrativeManager = NarrativeManager.Instance;
        endPoint = EndPoint.Instance;
        myLvl = _myLvl;
    }

    protected virtual void Start()
    {
        mobsKilled = 0;
        SetupLvlManager(0);
        SetNextObjective();
    }

    public virtual void SignalMonsterDeath(int _monsterID, bool bossMob = false)
    {
        bool allMonstersKilled;
        mobsKilled++;

        if(mobsKilled == 1 && myLvl == 1)
        {
            AudioManager.Instance.Play("Level_1_RegrowthMechanic", true);
        }

        if(mobsKilled >= minNumberOfMobsToKill)
        {
            allMonstersKilled = true;
            if (LvlManager.Instance.myLvl == 1 && mobsKilled == minNumberOfMobsToKill) AudioManager.Instance.Play("Level_1_PortalOpens", true);
            else if(LvlManager.Instance.myLvl == 2 && mobsKilled == minNumberOfMobsToKill) AudioManager.Instance.Play("Level_2_PortalOpens", true);
            else if (LvlManager.Instance.myLvl == 3 && mobsKilled == minNumberOfMobsToKill) AudioManager.Instance.Play("Level_3_PortalOpens", true);

        }
        else
        {
            allMonstersKilled = false;  
        }


        if(bossMob)
        {
            endPoint.ActivateExit();
            uiManager.ClearObjective();
        }
        else if (!allMonstersKilled)
        {
            SetNextObjective();
        }
        else
        {
            endPoint.ActivateExit();
            uiManager.ClearObjective();
        }
    }

    protected void SetNextObjective()
    {
        Transform currObjectiveTransformToTrack;
        foreach (EC_EnemyManager mob in orderedMonsters)
        {
            if (currObjective == mob.mobID)
            {
                currObjectiveTransformToTrack = mob.transform;
                uiManager.SignalNewObjectiveToTrack(currObjectiveTransformToTrack, playerManager.transform);
                currObjective++;
                break;
            }
        }

    }

    public void SignalNarrativeMonsterEncounter(int _narrativeID)
    {
        narrativeManager.RecieveNarrative(_narrativeID, NarrativeManager.NarrativeTriggers.MonsterEncounteredPlayer);

    }



    public void PauseGameForNarrative() /* Should make seperate function for pausing */
    {
        playerManager.gamePaused = true;

        foreach(EC_EnemyManager enemyManager in orderedMonsters)
        {
            enemyManager.gamePaused = true;
        }

        Time.timeScale = 0;
        uiManager.HideNonNarrativeUI();

        uiManager.SignalGamePaused();
    }

    public void ResumeGame() /* Called from narrative rather than UI since the UI has access to narrative, it just makes more sense to only pause the game here rather than two seperate scripts */
    {


        playerManager.gamePaused = false;


        uiManager.ReenableNonNarrativeUI();
        uiManager.EnableMouseForGamePlay();
        Time.timeScale = 1;

        Invoke(nameof(HandleMobAnimators), .1f);
    }

    private void HandleMobAnimators()
    {
        foreach (EC_EnemyManager enemyManager in orderedMonsters)
        {
            enemyManager.gamePaused = false;
        }
    }

    public void FindMonsters()
    {
        /* have to find monsters in correct order */
        orderedMonsters = GameObject.FindObjectsOfType<EC_EnemyManager>();


    }
    /* This only works right now for the first time the player gets into combat,
     * this can work later on if each monster increments a counter when approaching and decrements it when it dies */
    public virtual void NotifyInCombat() 
    {
        playerInCombat = true;
    }
    public void NewCheckpoint(Vector3 pos, Vector3 rot, int priority)
    {
        if(priority > currentCheck)
        {
            currentCheck = priority;
            respawn = pos;
            newRot = rot;
            UpdateMobs();
        }
        //UpdateNumbers();
        //UpdateMobs();
    }
    void UpdateNumbers()
    {
        int a = 0; //0
        int b = 0; //1
        
        int e = 0; //2
        int c = 0;
        int backup = 0;
        EC_EnemyManager[] enemies = Object.FindObjectsOfType<EC_EnemyManager>();
        foreach(EC_EnemyManager enemy in enemies)
        {
            if (!enemy.isDead)
            {
                if(enemy.monsterType == 0)
                {
                    a++;
                }
                else if(enemy.monsterType == 1)
                {
                    b++;
                }
                else if(enemy.monsterType == 2)
                {
                    e++;
                }
                else if(enemy.monsterType == 3)
                {
                    c++;
                }
            }
            else
            {
                backup++;
            }
        }
        mobA_count = a;
        mobB_count = b;
        mobC_count = c;
        mobE_count = e;
        backupMobsKilled = backup;
    }
    
    
    void SetUpSpawners()
    {
        EnemySpawning[] spawners = Object.FindObjectsOfType<EnemySpawning>();
        foreach(EnemySpawning spawner in spawners)
        {
            if(spawner.enemyType == 0)
            {
                RespawnerA = spawner;
            }
            else if(spawner.enemyType == 1)
            {
                RespawnerB = spawner;
            }
            else if(spawner.enemyType == 2)
            {
                RespawnerE = spawner;
            }
            else if(spawner.enemyType == 3)
            {
                RespawnerC = spawner;
            }
            else
            {
                Debug.Log("If this gets printed, something is messed up");
            }
        }
    }
    void SetupMobs()
    {
        enemyList = GameObject.FindObjectsOfType<EC_EnemyManager>();
        numEnemies = enemyList.Length;
        spawnLocations = new Vector3[numEnemies];
        mobLifes = new bool[numEnemies];
        Debug.Log(numEnemies);
        int index = 0;
        for(int i = 0; i < numEnemies; i++)
        {

            spawnLocations[i] = enemyList[i].gameObject.transform.position;
            mobLifes[i] = true;

        }
        
    }
    void UpdateMobs()
    {
        for(int i = 0; i < enemyList.Length; ++i)
        {
            if (enemyList[i].isDead)
            {
                Debug.Log("dead mob");
                mobLifes[i] = false;
            }
            else
            {
                Debug.Log("Alive");
            }
            
        }
    }
    public void RespawnMobs()
    {
        int newnum = 0;
        for(int i = 0; i < enemyList.Length; ++i)
        {
            if (mobLifes[i])
            {
                ResetEnemy(enemyList[i], spawnLocations[i]);
                
            }
            else
            {
                enemyList[i].gameObject.SetActive(false);
                newnum++;
            }
        }
        mobsKilled = newnum;
    }
    public void ResetEnemy(EC_EnemyManager enemy, Vector3 pos)
    {
        enemy.ProperReset();
        enemy.transform.position = pos;
        enemy.gameObject.GetComponent<Collider>().enabled = true;
        Collider[] colliders = enemy.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        enemy.gameObject.GetComponent<PC_CharacterColliderBlocker>().enabled = true;
        enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;


    }
    public void Respawn()
        
    {
        Debug.Log("helo");
        /*
        
        //kill all monsters
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        /*
        foreach(GameObject enemy in enemyList)
        {
            Destroy(enemy);
        }
        
        if(mobA_count > 0)
        {
            RespawnerA.SpawnEnemies(mobA_count);
        }
        if (mobB_count > 0)
        {
            RespawnerB.SpawnEnemies(mobB_count);

        }
        if (mobC_count > 0)
        {
            RespawnerC.SpawnEnemies(mobC_count);

        }
        if (mobE_count > 0)
        {
            RespawnerE.SpawnEnemies(mobE_count);

        }
        */
        /*
        player.GetComponent<PC_EC_Vitals>().ResetHealth();
        player.GetComponent<PC_PlayerManager>().isDead = false;
        player.GetComponent<PC_UIManager>().CameraFadeOut();
        player.GetComponentInChildren<PC_AnimatorController>().StopCurrentAnimation();
        //reset animation too
        player.transform.position = respawn;
        
        mobsKilled = backupMobsKilled;
        */
        
    }

    public void DestroyAllMobs()
    {
        foreach(EC_EnemyManager mob in orderedMonsters)
        {
            mob.gameObject.SetActive(false);
        }
    }
}

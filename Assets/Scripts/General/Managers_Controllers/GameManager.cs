using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    private static GameManager _i;
    public static GameManager i { get { return _i; } }
    [SerializeField] private Transform sysMessagePoint;
    [SerializeField] private Transform playerSpawnPoint, bossSpawnPoint;
    [SerializeField] private Transform pooledObjectLocation;
    [SerializeField] private UIController _uiController;
    private BonusStatController _bonusStatController;
    private GameObject playerGO, bossGO;
    private bool isPaused;


    #endregion
    
    #region Initialize
    private void Awake() 
    {
        _i = this;  
        SetupObjectPools();  
        Initialize();
    }

    private void Initialize() 
    {
        _bonusStatController = new BonusStatController(0,0,0);
        SpawnPlayerObject();
    }

    private void SpawnPlayerObject()
    {
        playerGO = Instantiate(GameAssets.i.pfPlayerObject, playerSpawnPoint);
        playerGO.transform.parent = null;
        playerGO.GetComponent<IHandler>().Initialize();

        SpawnBoss();
    }

    private void SpawnBoss()
    {
        bossGO = Instantiate(GameAssets.i.pfBossObject, bossSpawnPoint);
        bossGO.transform.parent = null;
        bossGO.GetComponent<IHandler>().Initialize();
        _uiController.Initialze();
    }

    public void SetupObjectPools()
    {
        ObjectPooler.SetupPool(GameAssets.i.pfProjectile.GetComponent<Projectile>(), 30, "Sprinkle");
    }
    #endregion

    public void PauseGame(){if(isPaused) return; else isPaused = true;}
    public void UnPauseGame(){if(isPaused) isPaused = false; else return;}
    
    public Transform GetSysMessagePoint(){ return sysMessagePoint;}
    public GameObject GetPlayerGO() { return playerGO; }
    public GameObject GetBossGO() { return bossGO;}
    public bool GetIsPaused() { return isPaused; }
    public Transform GetPoolLocation(){return pooledObjectLocation;}
    public BonusStatController GetBonusStats() { return _bonusStatController; }

}

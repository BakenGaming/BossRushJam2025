using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    private static GameManager _i;
    public static GameManager i { get { return _i; } }
    [SerializeField] private Transform sysMessagePoint, textPopupLocation;
    [SerializeField] private Transform playerSpawnPoint, bossSpawnPoint;
    [SerializeField] private Transform pooledObjectLocation;
    [SerializeField] private UIController _uiController;
    [SerializeField] private SugarManager _sugarManager;
    private WeaponSystem _weaponSystem;
    private BonusStatController _bonusStatController;
    private GameObject playerGO, bossGO;
    private bool isPaused;


    #endregion
    
    #region Initialize
    private void Awake() 
    {
        _i = this;  
        Initialize();
    }

    private void Initialize() 
    {
        _weaponSystem = GetComponent<WeaponSystem>();
        _weaponSystem.Initialize();

        _sugarManager = GetComponent<SugarManager>();
        _sugarManager.Initialize();

        _bonusStatController = new BonusStatController(0,0,0);
        SetupObjectPools();  
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
        _weaponSystem.EquipFirstWeapon();
    }

    public void SetupObjectPools()
    {
        foreach (GameObject _weapon in _weaponSystem.GetWeapons())
            ObjectPooler.SetupPool(_weapon.GetComponent<Weapon>().GetWeaponStats().projectileGO.GetComponent<Projectile>(),
                20, _weapon.GetComponent<Weapon>().GetWeaponStats().projectileName);

        ObjectPooler.SetupPool(GameAssets.i.pfLootObject.GetComponent<LootObject>(), 20, "Sugar Cube");
        ObjectPooler.SetupPool(GameAssets.i.pfBerryBomb.GetComponent<Projectile>(), 3, "Berry Bomb");
        ObjectPooler.SetupPool(GameAssets.i.pfBerry.GetComponent<Projectile>(), 3, "Berry");
    }
    #endregion

    public void PauseGame(){if(isPaused) return; else isPaused = true;}
    public void UnPauseGame(){if(isPaused) isPaused = false; else return;}
    
    public Transform GetSysMessagePoint(){ return sysMessagePoint;}
    public Transform GetTextPopUpLocation(){return textPopupLocation;}
    public GameObject GetPlayerGO() { return playerGO; }
    public GameObject GetBossGO() { return bossGO;}
    public bool GetIsPaused() { return isPaused; }
    public Transform GetPoolLocation(){return pooledObjectLocation;}
    public BonusStatController GetBonusStats() { return _bonusStatController; }

}

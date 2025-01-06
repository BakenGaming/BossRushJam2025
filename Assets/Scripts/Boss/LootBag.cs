using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{   
    public void Initialize()
    {
        EnemyHandler.onDamageReceived += SpawnLoot;
    }
    private void OnDisable() 
    {
        EnemyHandler.onDamageReceived -= SpawnLoot;
    }
    private void SpawnLoot()
    {
        LootObject newLoot = ObjectPooler.DequeueObject<LootObject>("Sugar Cube");
        newLoot.transform.position = transform.position;
        newLoot.gameObject.SetActive(true);
        newLoot.Initialize();
    }
}

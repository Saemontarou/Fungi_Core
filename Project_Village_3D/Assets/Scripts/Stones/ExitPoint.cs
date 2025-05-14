using System;
using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public GameObject enemyBossOne;
    //public GameObject enemyBossTwo;
    
    public GameObject miniMap;
    
    public GameObject deathZone;
    public DeathZoneDamage zoneDamage;

    private void OnEnable()
    {
        ActionManager.BossBecomesVisible += BossesVisible;
        ActionManager.MapBecomesInvisible += MapInvisible;
    }

    public void BossesVisible()
    {
        enemyBossOne.SetActive(true);
        //enemyBossTwo.SetActive(true);
        deathZone.SetActive(true);
        zoneDamage.DamageZone();
    }

    public void MapInvisible()
    {
        miniMap.SetActive(false);
    }

    private void OnDisable()
    {
        ActionManager.BossBecomesVisible -= BossesVisible;
        ActionManager.MapBecomesInvisible -= MapInvisible;
    }
}

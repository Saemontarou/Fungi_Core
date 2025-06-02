using UnityEngine;

public class ExitPoint : MonoBehaviour
{
    public GameObject enemyBoss;
    public GameObject miniMap;
    public GameObject sandTornado;
    public GameObject deathZone;
    public DeathZone zoneDamage;

    private void OnEnable()
    {
        ActionManager.BossBecomesVisible += BossVisible;
        ActionManager.TornadoBecomesVisible += TornadoVisible;
        ActionManager.MapBecomesInvisible += MapInvisible;
    }

    private void BossVisible()
    {
        enemyBoss.SetActive(true);
        deathZone.SetActive(true);
        zoneDamage.DamageZone();
    }

    private void TornadoVisible()
    {
        sandTornado.SetActive(true);
    }

    private void MapInvisible()
    {
        miniMap.SetActive(false);
    }

    private void OnDisable()
    {
        ActionManager.BossBecomesVisible -= BossVisible;
        ActionManager.TornadoBecomesVisible -= TornadoVisible;
        ActionManager.MapBecomesInvisible -= MapInvisible;
    }
}
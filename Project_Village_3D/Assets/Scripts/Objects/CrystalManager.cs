using System;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    public int value;
    
    [Header("Кристаллы в Canvas")]
    public GameObject canvasCrystalRed;
    public GameObject canvasCrystalGreen;
    public GameObject canvasCrystalBlue;
    public GameObject canvasCrystalOrange;
    public GameObject canvasCrystalPurple;
    public GameObject canvasEmptyGems;
    
    [Header("Кейсы в Canvas")]
    public GameObject caseSphereRight;
    public GameObject caseSphereLeft;
    public GameObject canvasSoulKey;
    
    [Header("Кристаллы на локации")]
    public GameObject redShard;
    public GameObject greenShard;
    public GameObject blueShard;
    public GameObject orangeShard;
    public GameObject purpleShard;
    
    [Header("Кристаллы на карте")]
    public GameObject mapRedShard;
    public GameObject mapGreenShard;
    public GameObject mapBlueShard;
    public GameObject mapOrangeShard;
    public GameObject mapPurpleShard;
    
    [Header("Кристаллы на алтаре")]
    public GameObject altarGems;
    public GameObject keyFence;
    
    [Header("Кресты на карте")]
    public GameObject crossRedShard;
    public GameObject crossGreenShard;
    public GameObject crossBlueShard;
    public GameObject crossOrangeShard;
    public GameObject crossPurpleShard;

    public GameObject golemFirst;
    public GameObject golemSecond;

    [SerializeField] private GameObject crystalScore;
    private CrystalScore _crystalScore;
    
    public AudioSource takeCrystal;
    
    private void OnEnable()
    {
        ActionManager.RedShardGrab += TakeGemRed;
        ActionManager.GreenShardGrab += TakeGemGreen;
        ActionManager.BlueShardGrab += TakeGemBlue;
        ActionManager.OrangeShardGrab += TakeGemOrange;
        ActionManager.PurpleShardGrab += TakeGemPurple;
        ActionManager.PutAllCrystals += PutAllGems;
        ActionManager.TakeStoneKey += KeyHide;
        ActionManager.KeyCanvasHide += KeyInsert;
    }

    private void Start()
    {
        _crystalScore = crystalScore.GetComponent<CrystalScore>();
    }

    private void TakeGemRed()
    {
        CrystalScore.Crystals += value;
        _crystalScore.numberCrystal.text = CrystalScore.Crystals.ToString();
        canvasCrystalRed.SetActive(true);
        redShard.SetActive(false);
        mapRedShard.SetActive(false);
        crossRedShard.SetActive(true);
        takeCrystal.Play();
    }
    
    private void TakeGemGreen()
    {
        CrystalScore.Crystals += value;
        _crystalScore.numberCrystal.text = CrystalScore.Crystals.ToString();
        canvasCrystalGreen.SetActive(true);
        greenShard.SetActive(false);
        mapGreenShard.SetActive(false);
        crossGreenShard.SetActive(true);
        takeCrystal.Play();
    }
    
    private void TakeGemBlue()
    {
        CrystalScore.Crystals += value;
        _crystalScore.numberCrystal.text = CrystalScore.Crystals.ToString();
        canvasCrystalBlue.SetActive(true);
        blueShard.SetActive(false);
        mapBlueShard.SetActive(false);
        crossBlueShard.SetActive(true);
        takeCrystal.Play();
    }
    
    private void TakeGemOrange()
    {
        CrystalScore.Crystals += value;
        _crystalScore.numberCrystal.text = CrystalScore.Crystals.ToString();
        canvasCrystalOrange.SetActive(true);
        orangeShard.SetActive(false);
        mapOrangeShard.SetActive(false);
        crossOrangeShard.SetActive(true);
        takeCrystal.Play();
    }
    
    private void TakeGemPurple()
    {
        CrystalScore.Crystals += value;
        _crystalScore.numberCrystal.text = CrystalScore.Crystals.ToString();
        canvasCrystalPurple.SetActive(true);
        purpleShard.SetActive(false);
        mapPurpleShard.SetActive(false);
        crossPurpleShard.SetActive(true);
        takeCrystal.Play();
        
        golemFirst.SetActive(true);
        golemSecond.SetActive(true);
    }
    
    private void PutAllGems()
    {
        altarGems.SetActive(true);
        canvasEmptyGems.SetActive(false);
        Destroy(caseSphereRight);
    }
    
    private void KeyHide()
    {
        keyFence.SetActive(false);
        canvasSoulKey.SetActive(true);
    }

    private void KeyInsert()
    {
        canvasSoulKey.SetActive(false);
        Destroy(caseSphereLeft);
    }

    private void OnDisable()
    {
        ActionManager.RedShardGrab -= TakeGemRed;
        ActionManager.GreenShardGrab -= TakeGemGreen;
        ActionManager.BlueShardGrab -= TakeGemBlue;
        ActionManager.OrangeShardGrab -= TakeGemOrange;
        ActionManager.PurpleShardGrab -= TakeGemPurple;
        ActionManager.PutAllCrystals -= PutAllGems;
        ActionManager.TakeStoneKey -= KeyHide;
        ActionManager.KeyCanvasHide -= KeyInsert;
    }
}
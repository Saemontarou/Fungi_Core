using System;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static event Action PressStoneButton;
    public static event Action OpenGate;
    
    public static event Action PutAllCrystals;
    public static event Action BossBecomesVisible;
    public static event Action TornadoBecomesVisible;
    public static event Action MapBecomesInvisible;
    public static event Action AltarHideAnimation;
    public static event Action TurretRiseAnimation;
    
    public static event Action CloseGate;
    
    public static event Action AltarRiseAnimation;
    public static event Action TurretHideAnimation;
    
    public static event Action TakeStoneKey;
    public static event Action ExitKeyHoleRise;
    
    public static event Action ExitKeyHoleHide;
    public static event Action ExitFenceHide;
    public static event Action KeyCanvasHide;

    public static event Action RedShardGrab;
    public static event Action GreenShardGrab;
    public static event Action BlueShardGrab;
    public static event Action OrangeShardGrab;
    public static event Action PurpleShardGrab;

    public static event Action TakeShards;
    public static event Action TakeStones;
    
    public static event Action SmallStones;
    
    public static void TakeCrystalShard()
    {
        TakeShards?.Invoke();
    }
    
    public static void TakeThrowStones()
    {
        TakeStones?.Invoke();
    }

    public static void TakeSmallStones()
    {
        SmallStones?.Invoke();
    }

    public static void OpenMainGate()
    {
        PressStoneButton?.Invoke();
        OpenGate?.Invoke();
    }

    public static void CloseMainGate()
    {
        CloseGate?.Invoke();
    }

    public static void PutGemsOnStone()
    {
        PutAllCrystals?.Invoke();
        BossBecomesVisible?.Invoke();
        TornadoBecomesVisible?.Invoke();
        MapBecomesInvisible?.Invoke();
        AltarHideAnimation?.Invoke();
        TurretRiseAnimation?.Invoke();
    }

    public static void CreepBossDeath()
    {
        AltarRiseAnimation?.Invoke();
        TurretHideAnimation?.Invoke();
    }

    public static void TakeKeyOnStone()
    {
        TakeStoneKey?.Invoke();
        ExitKeyHoleRise?.Invoke();
    }

    public static void InsertKeyOnHole()
    {
        ExitKeyHoleHide?.Invoke();
        ExitFenceHide?.Invoke();
        KeyCanvasHide?.Invoke();
    }

    public static void RedCrystalShardGrab()
    {
        RedShardGrab?.Invoke();
    }

    public static void GreenCrystalShardGrab()
    {
        GreenShardGrab?.Invoke();
    }

    public static void BlueCrystalShardGrab()
    {
        BlueShardGrab?.Invoke();
    }

    public static void OrangeCrystalShardGrab()
    {
        OrangeShardGrab?.Invoke();
    }

    public static void PurpleCrystalShardGrab()
    {
        PurpleShardGrab?.Invoke();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum UIEvent
    {
        Click,
        Pressed,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag,
    }

    public enum Scene
    {
        Unknown,
        DevScene,
        GameScene,
    }

    public enum Sound
    {
        Bgm,
        Effect,
    }

    public enum ObjectType
    {
        Player, //플레이어
        Monster, //몬스터
        Projectile, //투사체
        Env //환경채집물
    }

    public enum SkillType
    {
        None,
        Melee,
        Projectile,
        Etc,
    }

    public enum StageType
    {
        Normal,
        Boss,
    }

    public enum CreatureState
    {
        Idle,
        Moving,
        Skill,
        Dead
    }

    public const int GOBLIN_ID = 1;
    public const int SNAKE_ID = 2;
    public const int BOSS_ID = 3;

    public const string PLAYER_DATA_ID = "Player.prefab";
    public const string JOYSTICK_DATA_ID = "UI_Joystick.prefab";
    public const string MAP_DATA_ID = "Map.prefab";
    public const string EXP_GEM_PREFAB = "EXPGem.prefab";

    public const int EGO_SWORD_ID = 10;
}

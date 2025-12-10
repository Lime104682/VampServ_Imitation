using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class GameScene : MonoBehaviour
{
    SpawningPool _spawningPool;

    void Start()
    {
        Managers.Resource.LoadAllAsync<Object>("FirstLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if (count == totalCount)
            {
                StartLoaded();
            }
        });
    }

    Define.StageType _stageType;
    public Define.StageType StageType
    {
        get { return _stageType; }
        set
        {
            _stageType = value;

            if (_spawningPool != null)
            {
                switch (value)
                {
                    case Define.StageType.Normal:
                        _spawningPool.Stopped = false;
                        break;
                    case Define.StageType.Boss:
                        _spawningPool.Stopped = true;
                        break;
                }
            }
        }
    }

    void StartLoaded()
    {
        Managers.Data.Init();

        Managers.UI.ShowSceneUI<UI_GameScene>();

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        GameObject MonsterHouse = new GameObject() { name = "Monsters" };

        PlayerController player = Managers.Object.Spawn<PlayerController>(Vector3.zero);
        var joystick = Managers.Resource.Instantiate(Define.JOYSTICK_DATA_ID);
        var map = Managers.Resource.Instantiate(Define.MAP_DATA_ID);

        ////이거 주석 해제하고 실행하면 Map도 생성안되고 오류도 겁나 뜸
        //GameObject sprit = Managers.Resource.Instantiate("EXPGem_01");

        for (int i = 0; i < 10; i++)
        {
            Vector3 randPos = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            MonsterController monsters = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));

            monsters.transform.parent = MonsterHouse.transform;
        }

        #region 이름&위치 명명
        joystick.name = "@UI_Joystick";
        GameObject canvas = GameObject.Find("Canvas");
        joystick.transform.parent = canvas.transform;
        map.name = "@Map";

        Camera.main.GetComponent<CameraController>()._target = player.gameObject;
        player.gameObject.GetComponent<PlayerController>()._joystick = joystick.gameObject.GetComponent<FloatingJoystick>();
        #endregion

        #region xml파일확인용
        //foreach (var playerData in Managers.Data.PlayerDic.Values)
        //{
        //    Debug.Log($"Lvl : {playerData.level}, Hp{playerData.maxHp}");
        //}

        //foreach (var skillData in Managers.Data.SkillDic.Values)
        //{
        //    Debug.Log($"templateID : {skillData.templateID}, skillType : {skillData.skillType}, damage : {skillData.damage}");
        //}
        #endregion

        Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
        Managers.Game.OnKillCountChanged += HandleOnKillCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;
    }

    int _collectedGemCount = 0;
    int _remainingTotalGemCount = 10;

    public void HandleOnGemCountChanged(int gemCount)
    {
        _collectedGemCount++;

        if (_collectedGemCount == _remainingTotalGemCount)
        {
            Managers.UI.ShowPopup<UI_SkillSelectPopup>();
            _collectedGemCount = 0;
            _remainingTotalGemCount *= 2;
        }

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)_collectedGemCount / _remainingTotalGemCount);
    }

    public void HandleOnKillCountChanged(int killCount)
    {
        Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);

        if (killCount == 5)
        {
            // BOSS
            //StageType = Define.StageType.Boss;

            //Managers.Object.DespawnAllMonsters();

            //Vector2 spawnPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 5, 10);

            //Managers.Object.Spawn<MonsterController>(spawnPos, Define.BOSS_ID);
        }
    }

    private void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
    }
}

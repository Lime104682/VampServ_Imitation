using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/{totalCount}");

            if(count == totalCount)
            {
               

                var player = Managers.Resource.Instantiate("Player.prefab");
                player.name = "Player";

                GameObject monsters = new GameObject() { name = "Monsters" };

                var snake = Managers.Resource.Instantiate("Snake_01.prefab");
                snake.transform.parent = monsters.transform;

                var joystick = Managers.Resource.Instantiate("Floating Joystick.prefab");
                joystick.name = "@UI_Joystick";

                GameObject canvas = GameObject.Find("Canvas");
                joystick.transform.parent = canvas.transform;

                var map = Managers.Resource.Instantiate("Map.prefab");
                map.name = "@Map"; 
                
                Camera.main.GetComponent<CameraController>()._target = player.gameObject;
                player.gameObject.GetComponent<PlayerController>()._joystick 
                = joystick.gameObject.GetComponent<FloatingJoystick>();
            }
        });
    }

    #region 복사해온것
    //void Start()
    //{

    //    Managers.Resource.LoadAllAsync<GameObject>("Prefabs", (key, count, totalCount) =>
    //    {
    //        Debug.Log($"{key} {count}/{totalCount}");

    //        if (count == totalCount)
    //        {
    //            Managers.Resource.LoadAllAsync<TextAsset>("Data", (key3, count3, totalCount3) =>
    //            {
    //                if (count3 == totalCount3)
    //                {
    //                    StartLoaded();
    //                }
    //            });
    //        }
    //    });
    //}


    //SpawningPool _spawningPool;

    //void StartLoaded()
    //{
    //    _spawningPool = gameObject.AddComponent<SpawningPool>();

    //    var player = Managers.Object.Spawn<PlayerController>();

    //    for (int i = 0; i < 10; i++)
    //    {
    //        MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
    //        mc.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
    //    }

    //    var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
    //    joystick.name = "@UI_Joystick";

    //    var map = Managers.Resource.Instantiate("Map.prefab");
    //    map.name = "@Map";

    //    Camera.main.GetComponent<CameraController>()._target = player.gameObject;

    //    Data Test
    //    Managers.Data.Init();

    //    foreach (var playerData in Managers.Data.PlayerDic.Values)
    //    {
    //        Debug.Log($"Lvl : {playerData.level}, Hp{playerData.maxHp}");
    //    }

    //}
    #endregion

}

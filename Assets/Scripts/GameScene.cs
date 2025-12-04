using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    void StartLoaded()
    {
        _spawningPool = gameObject.AddComponent<SpawningPool>();

        GameObject MonsterHouse = new GameObject() { name = "Monsters" };

        PlayerController player = Managers.Object.Spawn<PlayerController>(Vector3.zero);

        //이거 주석 해제하고 실행하면 Map도 생성안되고 오류도 겁나 뜸 
        //GameObject sprit = Managers.Resource.Instantiate("EXPGem_01");
        //sprit.name = "@EXP_Gem";

        for (int i = 0; i < 10; i++)
        {
            Vector3 randPos = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            MonsterController monsters = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));


            monsters.transform.parent = MonsterHouse.transform;
        }

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

}

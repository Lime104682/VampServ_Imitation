using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;

public class ObjectManager
{
    public PlayerController Player { get; private set; }

    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();
    public HashSet<GemController> Gems { get; } = new HashSet<GemController>();

    public T Spawn<T>(Vector3  position, int templateID = 0) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            var go = Managers.Resource.Instantiate(Define.PLAYER_DATA_ID);
            go.name = "Player";
            go.transform.position = position;

            PlayerController pc = go.GetComponent<PlayerController>();
            Player = pc;
            pc.Init();

            return pc as T;
        }
        else if (type == typeof(MonsterController))
        {
            string name = (templateID == 0 ? "Goblin_01" : "Snake_01");
            var go = Managers.Resource.Instantiate(name + ".prefab", pooling: true);
            go.transform.position = position;

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
             
            Monsters.Add(mc);
            mc.Init();

            return mc as T;
        }
        else if(type == typeof(GemController))
        {
            GameObject go = Managers.Resource.Instantiate(Define.EXP_GEM_PREFAB, pooling: true);
            go.transform.position = position;

            GemController gc = go.GetOrAddComponent<GemController>();

            Gems.Add(gc);
            gc.Init();

            string key = UnityEngine.Random.Range(0, 2) == 0 ? "EXPGem_01.sprite" : "EXPGem_02.sprite";
            Sprite sprite = Managers.Resource.Load<Sprite>(key);
            gc.GetComponent<SpriteRenderer>().sprite = sprite;
            if (sprite != null )
            {
                Debug.Log("name :" + sprite);
            }
            else if (sprite == null)
            {
                Debug.Log("sprtie null" );
                Debug.Log("key :" + key);

                Debug.Log("name :" + sprite);

            }

            return gc as T; 
        }

        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            // ?
        }
        else if (type == typeof(MonsterController))
        {
            Monsters.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(GemController))
        {
            Gems.Remove(obj as GemController);
            Managers.Resource.Destroy (obj.gameObject);
        }
    }
}

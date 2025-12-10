using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using Object = UnityEngine.Object;

public class ResourceManager 
{
    
	Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

	public T Load<T>(string key) where T : Object
	{
		if (_resources.TryGetValue(key, out Object resource))
			return resource as T;

		return null;
	}

	public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
	{
		GameObject prefab = Load<GameObject>($"{key}");
		if (prefab == null)
		{
			Debug.Log($"Failed to load prefab : {key}");
			return null;
		}

        // Pooling
        if (pooling)
		{
            return Managers.Pool.Pop(prefab);
        }

        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
	}

	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		if (Managers.Pool.Push(go))
			return;

		Object.Destroy(go);
	}

	#region 어드레서블
	public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
	{
		// 캐시 확인.
		if (_resources.TryGetValue(key, out Object resource))
		{
			callback?.Invoke(resource as T);
			return;
		}

		string loadKey = key;

		if (key.Contains(".sprite"))
		{
			loadKey = $"{key}[{key.Replace(".sprite", "")}]";

			var asyncOperation = Addressables.LoadAssetAsync<Sprite>(loadKey);
			asyncOperation.Completed += (op) =>
			{
				_resources.Add(key, op.Result);
				callback?.Invoke(op.Result as T);
			};
		}
		else
        {
            // 리소스 비동기 로딩 시작.
            var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
            asyncOperation.Completed += (op) =>
            {
                _resources.Add(key, op.Result);
                callback?.Invoke(op.Result);
            };
        }
    }

    
	//Addreassables의 특정 label에 속한 에셋들 비동기로 가져오기
    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
	{
        //key에 해당하는 에셋들의 경로(IResourceLocation)를 가져온다. 메모리에 에셋을 로드한 것이 아닌 에셋들 경로만 가져온 것.
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T)); 
        opHandle.Completed += (op) =>
		{
			int loadCount = 0;
			int totalCount = op.Result.Count;

			foreach (var result in op.Result)
			{
				LoadAsync<T>(result.PrimaryKey, (obj) =>
				{
					loadCount++;
					callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
				});
			}
		};
	}
	#endregion
}

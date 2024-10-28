using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LoadingScreen : MonoBehaviour
{
    protected static LoadingScreen _instance;
    public static LoadingScreen Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<LoadingScreen>();
                if (obj != null)
                {
                    _instance = obj;
                }
                else
                {
                    Debug.Log("LoadingScreen가 없습니다.");
                }
            }
            return _instance;
        }

        private set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("LoadingScreen");
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress >= 0.9f)
            {
                // 3초 동안 지연
                float delay = 3f;
                float timer = 0f;
                while (timer < delay)
                {
                    timer += Time.unscaledDeltaTime;
                    yield return null;
                }

                op.allowSceneActivation = true;
                yield break;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

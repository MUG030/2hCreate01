using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;  // UnityEngineのRandomを使用することを明示的に指定
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private bool inputDisabled = false;
    private float inputStartTime = 0; // 初期値を設定
    [SerializeField] private new GameObject gameObject;

    async void Start()
    {
        gameObject.SetActive(false);
        // ランダムな時間を生成（5秒から20秒の間）
        float randomTime = Random.Range(5f, 20f);
        Debug.Log(randomTime);
        await DisableInputWithDelay(randomTime);
    }

    void Update()
    {
        if (!inputDisabled)
        {
            return;
        }

        float judgeTime = Time.time - inputStartTime;

        Debug.Log(judgeTime);

        if (Input.GetKeyDown(KeyCode.Return) && judgeTime <= 1.0f)
        {
            Debug.Log("俺の勝ち");
            SceneManager.LoadScene("ClearScene");
            inputDisabled = false;
        }
        else if (judgeTime > 1.0f)
        {
            Debug.Log("お前の負け");
            SceneManager.LoadScene("GameOverScene");
            inputDisabled = false;
        }
    }

    IEnumerator DisableInputWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        inputStartTime = Time.time;
        gameObject.SetActive(true);
        inputDisabled = true;
    }
}

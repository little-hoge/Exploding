using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENUM;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject EnemyPrefab;

    private void Update()
    {

        if (PlayerController.Instance.GetPlayerState() == ePlayerState.GAMEOVER)
        {
            // 敵生成を止める
            StopAllCoroutines();
        }


    }

    void Start()
    {
        // 開始時2秒毎敵生成(30秒後、1分後、3分後追加)
        StartCoroutine(EnemyCreate(0));
        StartCoroutine(EnemyCreate(30));
        StartCoroutine(EnemyCreate(60));
        StartCoroutine(EnemyCreate(180));
     
    }


    // 敵生成処理
    IEnumerator EnemyCreate(int WaitSec)
    {
        // 一定時間経過後経過後敵生成
        yield return new WaitForSeconds(WaitSec);
        while (true)
        {
            // ランダム位置に生成
            Instantiate(EnemyPrefab, new Vector3(Random.Range(-2.5f,2.5f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(2);
        }

    }
   


}

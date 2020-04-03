using GodTouches;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : SingletonMonoBehaviour<TouchManager>
{

    // シングルトン
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        // シーン切り替え後、オブジェクト引き継ぎ
        DontDestroyOnLoad(gameObject);
    }

    // 更新
    void Update()
    {
        // タッチ座標更新
        UpdatePosition();

    }

    // タッチ位置更新
    public void UpdatePosition()
    {
        // 押された時
        if (GodTouch.GetPhase() == GodPhase.Began)
        {
            // スクリーン座標をワールド座標に変換
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(pos);

            // タッチ位置更新
            transform.position = pos;
        }
    }


    // タッチ時オブジェクトを全取得
    public List<GameObject> getClickObjectAll()
    {
        List<GameObject> result = new List<GameObject>();

        // 押された時
        if (GodTouch.GetPhase() == GodPhase.Began)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // ワールド座標にあるオブジェクトをすべて取得
            foreach (RaycastHit2D hit in Physics2D.RaycastAll(worldPoint, Vector2.zero))
            {

                // オブジェクトがあった時
                if (hit)
                {
                    result.Add(hit.collider.gameObject);
                }
            }

        }
        return result;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Camera mainCamera;

    // 初期化
    void Start()
    {
        // カメラ情報取得
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

    }

    // 更新
    void Update()
    {
        transform.Translate(0, Define.SHOT_SPEED, 0);

        // 画面外
        if (transform.position.y > getScreenTopLeft().y + this.GetComponent<SpriteRenderer>().bounds.size.y)
        {
            Destroy(gameObject);
        }

    }

    private Vector3 getScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

}
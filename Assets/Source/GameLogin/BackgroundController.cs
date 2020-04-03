using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    // 初期化
    void Start()
    {

    }

    // 更新
    void Update()
    {

        //最初に戻す
        if (transform.position.y < -4.9f)
        {
            transform.position = new Vector3(0, 4.9f, 0);
        }
        else
        {

            // スクロール
            transform.Translate(0, -0.03f, 0);

        }

    }
}

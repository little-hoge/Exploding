using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ENUM;

public class EnemyController : MonoBehaviour
{
    // オブジェクト 
    private GameObject TextPrefab;
    Camera m_Cmera;

    // 下の壁にあたった回数
    int reflectCount;

    // 初期化
    void Start()
    {
        // プレハブ設定
        TextPrefab = (GameObject)Resources.Load("Prefab/TextPrefab");

        // カメラ情報取得
        m_Cmera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // コンポネント取得
        var Body = gameObject.GetComponent<Rigidbody2D>();

        // 初動左右ランダム設定
        var direction = new Vector2(1, 1).normalized;
        Body.velocity = direction * (int)Function.Random<eDIRECTION>();

        // 下の壁にあたった回数
        reflectCount = 0;
        
        // 色設定
        ParticleSystem.MainModule main = this.GetComponent<ParticleSystem>().main;
        main.startColor = Function.GetSelectRandomColor();
        
    }

    // 更新
    void Update()
    {
        switch (PlayerController.Instance.GetPlayerState())
        {
            case ePlayerState.GAMEOVER:
                GameOverMove();
                break;

            default:
                break;
        }

    }


    // ゲームオーバー時の移動
    void GameOverMove()
    {
        GameObject[] EnemyObj = GameObject.FindGameObjectsWithTag("Enemy");

        // 存在する敵の重力を0にする
        foreach (GameObject e in EnemyObj)
        {
            Rigidbody2D EnemyScript = e.GetComponent<Rigidbody2D>();
            EnemyScript.velocity = Vector3.zero;
        }

    }



    // 敵と当たった瞬間
    void OnTriggerEnter2D(Collider2D coll)
    {
        int point = Define.FIRST_POINT;

        // レイヤー情報取得
        string layerName = LayerMask.LayerToName(coll.gameObject.layer);

        // 下の壁に当たった時
        if (layerName == "BottomCollision")
        {
            reflectCount++;
            //print(reflectCount);
        }

        // 追加得点
        if (layerName == "Text_Add")
        {
            var TextGeneratorScript = coll.gameObject.GetComponent<TextGenerator>();
            point = int.Parse(TextGeneratorScript.textmesh.text) * 2;
        }

        // 弾または文字にあたった時、一定数下の壁にあたった時
        if (layerName == "Shot" || layerName == "Text_Add" || reflectCount >= Define.ENEMY_RAIFLECT_MAX)
        {
            // エフェクト再生
            this.GetComponent<ParticleSystem>().Play();

            // 今取得したスコア生成
            var TextObj = Instantiate(TextPrefab, transform.position, Quaternion.identity);
            var TextGeneratorScript = TextObj.gameObject.GetComponent<TextGenerator>();

            // 当たり判定追加
            var TextCircleCollider2D = TextObj.gameObject.AddComponent<CircleCollider2D>();
            TextCircleCollider2D.radius = Define.TEXT_RADIUS;
            TextCircleCollider2D.isTrigger = true;

            // 上限
            if (point >= 9999)
            {
                point = 9999;
                TextGeneratorScript.textmesh.text = 9999.ToString();
                TextGeneratorScript.textmesh.color = Color.red;
                SoundManager.Instance.PlaySeByName(Define.DEATH_MAX);

            }
            else
            {
                TextGeneratorScript.textmesh.text = (point).ToString();
                SoundManager.Instance.PlaySeByName(Define.DEATH);
            }


            // スコアを更新
            GameObject.Find("UIController").GetComponent<UIController>().AddScore(point);


            // 文字にあたった時
            if (layerName == "Text_Add")
            {
                // 当たった物の削除
                Destroy(coll.gameObject.GetComponent<CircleCollider2D>());
            }

            // 弾にあたった時
            if (layerName == "Shot")
            {
                // 当たった物の削除
                Destroy(coll.gameObject);
            }

            // 文字・弾・一定数下の壁にあたった時
            if (layerName == "Text_Add" || layerName == "Shot" || reflectCount >= Define.ENEMY_RAIFLECT_MAX)
            {
                // 当たり判定、重力、画像無効
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().simulated = false;
                GetComponent<SpriteRenderer>().enabled = false;

                // エフェクト処理後、敵オブジェクト削除
                Destroy(gameObject, GetComponent<ParticleSystem>().main.duration);
            }
        }

    }


}



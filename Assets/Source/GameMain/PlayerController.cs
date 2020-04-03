using GodTouches;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ENUM;

public class PlayerController : MonoBehaviour
{
    // オブジェクト 
    GameObject TextPrefab, ShotPrefab;
    TextGenerator[] TextScript;
    GameObject[] PlayerTextObj;
    SpriteRenderer PlayerSprite;

    // 
    public static PlayerController Instance;


    private ePlayerState PlayerState;
    float recovery_cnt, down_cnt;
    int hitcount;



    // プレイヤー状態を渡す
    public ePlayerState GetPlayerState()
    {
        return PlayerState;
    }

    void Awake()
    {
        // インスタンス設定
        Instance = this;

    }


    // 初期化
    void Start()
    {
        // プレイヤー画像情報
        PlayerSprite = GetComponent<SpriteRenderer>();

        // プレハブ設定
        TextPrefab = Resources.Load("Prefab/TextPrefab") as GameObject;
        ShotPrefab = Resources.Load("Prefab/ShotPrefab") as GameObject;


        // カウント文字生成
        // 0：ダウンカウント用 1：復帰カウント用
        Vector3 pos = transform.position;
        pos.y += 1;
        PlayerTextObj = new GameObject[] { Instantiate(TextPrefab, pos, Quaternion.identity),
           Instantiate(TextPrefab, transform.position, Quaternion.identity) };
        TextScript = new TextGenerator[] { PlayerTextObj[0].gameObject.GetComponent<TextGenerator>(),
            PlayerTextObj[1].gameObject.GetComponent<TextGenerator>() };


        // ダウンカウント文字設定
        TextScript[0].textmesh.text = " ";
        TextScript[0].textmesh.color = Color.red;
        TextScript[0].texttype = eTextType.RecoveryCount;

        // 復帰カウント文字設定
        TextScript[1].textmesh.text = " ";
        TextScript[1].texttype = eTextType.DownCount;

        // プレイヤー状態
        PlayerState = ePlayerState.NONE;
        hitcount = 0;

        // エフェクト色設定
        ParticleSystem.MainModule main = this.GetComponent<ParticleSystem>().main;
        main.startColor = Function.GetSelectRandomColor();

        // タッチ位置初期化
        TouchManager.Instance.transform.position = Vector3.zero;

        // BGM開始
        SoundManager.Instance.PlayBgmByName(Define.MAIN);

    }



    // 更新
    void Update()
    {
        switch (PlayerState)
        {
            case ePlayerState.NONE:
                Move();
                Shot();
                break;
            case ePlayerState.DOWN:
                Recovery();
                break;
            case ePlayerState.DEATH:
                GameOverJudge();
                break;
            default:
                break;
        }
    }


    // 移動
    void Move()
    {
        var PlayerPosX = transform.position.x;
        var TouchPos = TouchManager.Instance.transform.position;

        if (PlayerPosX >= TouchPos.x)
        {
            if (PlayerPosX - TouchPos.x >= Define.PLAYER_SPEED)
            {
                transform.Translate(-Define.PLAYER_SPEED, 0, 0);
            }
        }
        else
        {
            if (PlayerPosX - TouchPos.x <= Define.PLAYER_SPEED)
            {
                transform.Translate(Define.PLAYER_SPEED, 0, 0);
            }

        }


    }

    // 弾の発射
    void Shot()
    {
        // タッチ時オブジェクト全取得
        var HitObjList = TouchManager.Instance.getClickObjectAll();

        // プレイヤーにタッチした時
        if ((HitObjList.IndexOf(gameObject) + 1) != 0) {
            // 弾発射
            Instantiate(ShotPrefab, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySeByName(Define.SHOT);
        }
    }


    // 復帰
    void Recovery()
    {
        // カウント表示
        TextScript[0].textmesh.text = ((int)down_cnt).ToString();
        TextScript[1].textmesh.text = recovery_cnt.ToString();

        // 押された時
        if (GodTouch.GetPhase() == GodPhase.Began)
        {
            recovery_cnt--;
            // 復帰
            if (recovery_cnt <= 0)
            {
                PlayerSprite.enabled = true;
                PlayerState = ePlayerState.NONE;
                TextScript[0].textmesh.text = "";
                TextScript[1].textmesh.text = "";

                // 無敵状態コルーチン
                StartCoroutine("Invincible");

            }
        }


        down_cnt -= Time.deltaTime;

        // 復帰不可
        if (down_cnt <= 0)
        {
            // エフェクト再生
            this.GetComponent<ParticleSystem>().Play();

            // 今取得したスコア生成表示
            var DeathTextObj = Instantiate(TextPrefab, transform.position, Quaternion.identity);
            var PlayerDeathTextScript = DeathTextObj.gameObject.GetComponent<TextGenerator>();
            PlayerDeathTextScript.textmesh.text = 10.ToString();

            // 得点文字に当たり判定追加
            var TextCircleCollider2D = DeathTextObj.gameObject.AddComponent<CircleCollider2D>();
            TextCircleCollider2D.radius = Define.TEXT_RADIUS;
            TextCircleCollider2D.isTrigger = true;

            // 状態更新
            PlayerState = ePlayerState.DEATH;

            // 復帰・ダウンカウント文字削除
            Destroy(PlayerTextObj[0]);
            Destroy(PlayerTextObj[1]);

        }

    }



    // ゲームオーバー判定
    void GameOverJudge()
    {
        var TextTagNum = GameObject.FindGameObjectsWithTag("Text");

        // 消え残しがなくなった時
        if (TextTagNum.Length == 0)
        {
            // BGMを止めてゲームオーバーへ
            SoundManager.Instance.StopBgm();
            PlayerState = ePlayerState.GAMEOVER;
        }
    }


    // プレイヤーと当たった時
    void OnTriggerEnter2D(Collider2D coll)
    {

        // レイヤー情報取得
        string layerName = LayerMask.LayerToName(coll.gameObject.layer);

        // 敵、プレイヤーが生きてる時
        if (layerName == "Enemy" && PlayerState == ePlayerState.NONE)
        {
            float player_pos_x;
            Vector3 text_pos;
            hitcount++;

            // プレイヤー情報設定
            PlayerSprite.enabled = false;
            player_pos_x = transform.position.x;
            PlayerState = ePlayerState.DOWN;
            recovery_cnt = Define.PLAYER_WEIGHT * hitcount;
            down_cnt = Define.PLAYER_DOWNCOUNT;

            // ダウンカウント再設定
            TextScript[0].textmesh.color = Color.red;
            TextScript[0].texttype = eTextType.RecoveryCount;
            text_pos = TextScript[0].transform.position;
            text_pos.x = player_pos_x;
            TextScript[0].transform.position = text_pos;

            // 復帰カウント再設定
            TextScript[1].texttype = eTextType.DownCount;
            text_pos = TextScript[1].transform.position;
            text_pos.x = player_pos_x;
            TextScript[1].transform.position = text_pos;


        }
    }

    // 一定時間無敵コルーチン
    IEnumerator Invincible()
    {
        // 当たり判定を消し、半透明にする
        gameObject.layer = LayerMask.NameToLayer("Player_Invincible");
        PlayerSprite.material.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(Define.PLAYER_INVINCIBLECOUNT);

        // 通常に戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
        PlayerSprite.material.color = new Color(1, 1, 1, 1);
    }




}




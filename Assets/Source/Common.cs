using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Common : MonoBehaviour
{

}


// PC画面サイズ設定
public class GameInitial
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(Define.SCREEN_X, Define.SCREEN_Y, false, Define.FPS);

    }

}


public static class Define
{
    // 定数
    public const string GAME_LOGIN = ("GameLogin");
    public const string GAME_MAIN = ("GameMain");
    public const string GAME_RANKING = ("GameRanking");
    public const string PLAYER = ("GamePlayer");

    // 音楽データ
    public const string DEATH = ("death");
    public const string DEATH_MAX = ("death_max");
    public const string SHOT = ("shot");
    public const string MAIN = ("main");

    // 画面設定
    public const int SCREEN_X = (480), SCREEN_Y = (864), FPS = (30);

    // 敵
    public const short ENEMY_RAIFLECT_MAX = (5);

    // プレイヤー
    public const float PLAYER_SPEED = (0.05f);
    public const float PLAYER_INVINCIBLECOUNT = (3f);
    public const int PLAYER_DOWNCOUNT = (11);
    public const int PLAYER_WEIGHT = (10);

    // 弾
    public const float SHOT_SPEED = (0.05F);

    // テキスト
    public const float TEXT_DELETECOUNT = (1.2f);
    public const float TEXT_RADIUS = (2.5f);
    public const int FIRST_POINT = (2);


    // NCMB
    public const string DATASTORENAME = ("Ranking");// ランキングクラス名
    public const int DRAWLIST_MAX = (20);// 最大いくつまでランキングデータを取得するか

}

public static class Function
{

    // 乱数
    private static readonly System.Random mRandom = new System.Random();

    // 指定された列挙型の値をランダムに返す
    public static T Random<T>()
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .OrderBy(c => mRandom.Next())
            .FirstOrDefault();
    }

    // 指定された列挙型の値の数返す
    public static int GetLength<T>()
    {
        return Enum.GetValues(typeof(T)).Length;
    }

    // ランダムに特定色を返す
    public static Color GetSelectRandomColor()
    {
        Color[] ColorArray = { Color.yellow, Color.red,Color.green,Color.white, Color.cyan, Color.blue , Color.magenta};

        var RandomIndex = mRandom.Next(ColorArray.Length);

        return ColorArray[RandomIndex];

    }


}


// 共有データ
public class Data
{
    public readonly static Data Instance = new Data();

    // 変数
    public int P1Score, P2Score;    // スコア
    public string P2Name;           // 相手の名前
    public bool GameJudge;          // 決着判定
    public bool Disconnect;          // 切断判定
    public int NowQuestionNumber;   // 出題番号



}

namespace ENUM
{
    public enum ePlayerState
    {
        DEATH = 0,
        NONE = 1,
        DOWN = 2,
        GAMEOVER = 3,
    };

    public enum eTextType
    {
        AddScore = 0,
        RecoveryCount = 1,
        DownCount = 2,
    };

    public enum eDIRECTION
    {
        RIGHT = 2,
        LEFT = -2,
    };

    
}

using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Text.RegularExpressions;
using ENUM;
using NCMB;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private Text Score;
    private Text HighScore;
    private Text GameOver;
    private Text TextTime;
    private GameObject Button_Retry, Button_Ranking, Button_Logout;
    int nowScore;
    bool IsCallSave;

    // 計測時間
    public TimeDate time;

    public struct TimeDate
    {
        public int min, sec;  //  分, 秒, 
        public float msec;    // ミリ秒
    };


    // 初期化
    void Start()
    {

        // スクリプト及びオブジェクト取得
        Score = GameObject.Find("Text_Score").GetComponent<Text>();
        HighScore = GameObject.Find("Text_HighScore").GetComponent<Text>();
        GameOver = GameObject.Find("Text_GameOver").GetComponent<Text>();
        TextTime = GameObject.Find("Text_Time").GetComponent<Text>();
        Button_Ranking = GameObject.Find("Button_Ranking");
        Button_Retry = GameObject.Find("Button_Retry");
        Button_Logout = GameObject.Find("Button_Logout");

        // 非表示
        Button_Ranking.SetActive(false);
        Button_Retry.SetActive(false);
        Button_Logout.SetActive(false);

        // 
        IsCallSave = false;
        nowScore = 0;
        Score.text = "Score: 0";
        GameOver.text = "";
        TextTime.text = "Time:" + QuickRanking.Instance.mRankingData.time;//string.Format("Time: {0:00}:{1:00}.{2:00}", time.min, time.sec, (time.msec * 0.1));


    }


    // 更新
    void Update()
    {
        switch (PlayerController.Instance.GetPlayerState())
        {
            case ePlayerState.NONE:
            case ePlayerState.DEATH:
            case ePlayerState.DOWN:
                CalcTime();
                break;
            case ePlayerState.GAMEOVER:
                DataSave();
                DrawGameOver();

                break;
            default:
                break;
        }

        HighScore.text = "HighScore：" + QuickRanking.Instance.mRankingData.score.ToString();
        Score.text = "Score：" + nowScore.ToString();
        TextTime.text = "Time:" + QuickRanking.Instance.mRankingData.time;//string.Format("Time：{0:00}:{1:00}.{2:00}", time.min, time.sec, (time.msec * 0.1));
    }


    // スコア加算処理
    public void AddScore(int addscore)
    {
        print(addscore);
        nowScore += addscore;

    }

    // 時間計算
    void CalcTime()
    {


        // 最大値制御
        if (time.min < 59 || time.sec < 59 || time.msec < 900)
        {
            // ミリ秒換算
            time.msec += (int)(UnityEngine.Time.deltaTime * 1000.0f);
        }

        // 時間計算
        if (time.sec >= 60)
        {
            time.sec = 0;
            time.min++;
        }
        if (time.msec >= 1000)
        {
            time.msec -= 1000;
            time.sec++;
         
        }

        // 時間データ保存 
        QuickRanking.Instance.mRankingData.time = string.Format("{0:00}:{1:00}.{2:00}", time.min, time.sec, (time.msec * 0.1));

    }

    // データ保存処理
    void DataSave()
    {

        // 1回ScoreがHIGHSCOREを上回った時
        if (IsCallSave && (nowScore > QuickRanking.Instance.mRankingData.score))
        {
            // データ更新
            HighScore.text = "HighScore: " + nowScore.ToString();
            QuickRanking.Instance.mRankingData.score = nowScore;
            QuickRanking.Instance.mRankingData.time = string.Format("{0:00}:{1:00}.{2:00}", time.min, time.sec, (time.msec * 0.1));
            QuickRanking.Instance.UserDataUpdate();

        }
        IsCallSave = true;
    }

    // ゲームオーバー画面表示
    void DrawGameOver()
    {
        // Button表示
        Button_Ranking.SetActive(true);
        Button_Retry.SetActive(true);
        Button_Logout.SetActive(true);
        GameOver.text = "GameOver";

    }

    // ランキング
    public void OnClick_Ranking()
    {

        SceneManager.LoadScene(Define.GAME_RANKING);

    }

    // リトライ
    public void OnClick_SinglePlay()
    {
        SceneManager.LoadScene(Define.GAME_MAIN);


    }

    // ログアウト
    public void OnClick_Logout()
    {

        NCMBUser.LogOutAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogWarning("ログアウトに失敗: " + e.ErrorMessage);
            }
            else
            {
                Debug.Log("ログアウトに成功");
                SceneManager.LoadScene(Define.GAME_LOGIN);
            }
        });

        // ランキング用オブジェクト破棄(ユーザー情報破棄)
        var RankingObj = GameObject.FindGameObjectWithTag("QuickRanking");
        if (RankingObj != null)
        {
            Destroy(RankingObj);
        }

    }

}
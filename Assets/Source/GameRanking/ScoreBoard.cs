using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum RANKING_TEXT_TYPE
{
    NAME,
    SCORE,
}

public class ScoreBoard : MonoBehaviour
{
    private Text userName;
    private Text score;
    private Text playerCount;


    private void Start()
    {
        // テキストオブジェクト設定
        userName = GameObject.Find("Text_UserName").GetComponent<Text>();
        score = GameObject.Find("Text_Score").GetComponent<Text>();
        playerCount = GameObject.Find("Text_PlayerCount").GetComponent<Text>();


        // ランキングデータ読み直し　 
        QuickRanking.Instance.DBRankingLoad();
;
    }
    private void Update()
    {
        // 表示情報取得
        userName.text = QuickRanking.Instance.GetRankingText(RANKING_TEXT_TYPE.NAME);
        score.text = QuickRanking.Instance.GetRankingText(RANKING_TEXT_TYPE.SCORE);
        playerCount.text = "プレイヤー数: " + QuickRanking.Instance.GetPlayerCount();

    }

    // 更新を押した時
    public void OnClick_Update()
    {
        QuickRanking.Instance.DBRankingLoad();
    }

    // リトライを押した時
    public void OnClick_Buck()
    {
        SceneManager.LoadScene(Define.GAME_MAIN);
    }

    // 計を押した時
    public void OnClick_GametotalSort()
    {
        // ゲーム回数降順
        QuickRanking.Instance.GametotalSort();
    }

    // 名前を押した時
    public void OnClick_GameTimeSort()
    {
        // 時間ショ昇順
        QuickRanking.Instance.GameTimeSort();
    }

}
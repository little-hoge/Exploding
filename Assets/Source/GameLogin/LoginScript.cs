using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class LoginScript : MonoBehaviour
{
    private void Start()
    {

    }

    // 新規登録
    public void OnClick_Signin()
    {
        //　インスタンス生成
        NCMBUser user = new NCMBUser();

        // ユーザー名・パスワードを設定
        user.UserName = GameObject.Find("Text_UserName").GetComponent<Text>().text;
        user.Password = GameObject.Find("Text_Password").GetComponent<Text>().text;

        // ユーザーの新規登録処理
        user.SignUpAsync((NCMBException e) =>
        {
            if (e != null)
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = string.Format("ユーザーの新規登録失敗しました。\n{0}", e.ErrorMessage);
            }
            else
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "ユーザーの新規登録成功しました。";

            }
        });
    }

    // ログイン
    public void OnClick_Login()
    {
        // ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync(
            GameObject.Find("Text_UserName").GetComponent<Text>().text,
            GameObject.Find("Text_Password").GetComponent<Text>().text,
            (NCMBException e) =>
        {
            if (e != null)
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = string.Format("ログインに失敗しました。\n{0}", e.ErrorMessage);
            }
            else
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "ログインに成功しました。";

                // ランキング用オブジェクト生成
                GameObject QuickRankingObj = new GameObject("QuickRanking");
                QuickRankingObj.tag= "QuickRanking";
                QuickRankingObj.AddComponent<QuickRanking>();

                // ランキング用オブジェクト引き継ぎ
                DontDestroyOnLoad(QuickRankingObj);

                // メインに遷移
                SceneManager.LoadScene(Define.GAME_MAIN);

            }
        });
    }
}



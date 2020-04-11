# 目次

<!-- TOC -->

- [目次](#%E7%9B%AE%E6%AC%A1)
- [操作方法](#%E6%93%8D%E4%BD%9C%E6%96%B9%E6%B3%95)
  - [操作方法](#%E6%93%8D%E4%BD%9C%E6%96%B9%E6%B3%95-1)
- [デモ](#%E3%83%87%E3%83%A2)
- [開発環境](#%E9%96%8B%E7%99%BA%E7%92%B0%E5%A2%83)
- [使用アセット](#%E4%BD%BF%E7%94%A8%E3%82%A2%E3%82%BB%E3%83%83%E3%83%88)
  - [ユーザーデータ管理(必須)](#%E3%83%A6%E3%83%BC%E3%82%B6%E3%83%BC%E3%83%87%E3%83%BC%E3%82%BF%E7%AE%A1%E7%90%86%E5%BF%85%E9%A0%88)
  - [Unityエディタ上でタッチの動作確認(必須)](#unity%E3%82%A8%E3%83%87%E3%82%A3%E3%82%BF%E4%B8%8A%E3%81%A7%E3%82%BF%E3%83%83%E3%83%81%E3%81%AE%E5%8B%95%E4%BD%9C%E7%A2%BA%E8%AA%8D%E5%BF%85%E9%A0%88)
  - [バージョン管理(任意)](#%E3%83%90%E3%83%BC%E3%82%B8%E3%83%A7%E3%83%B3%E7%AE%A1%E7%90%86%E4%BB%BB%E6%84%8F)
  - [WebGL画面サイズ自動調整(任意)](#webgl%E7%94%BB%E9%9D%A2%E3%82%B5%E3%82%A4%E3%82%BA%E8%87%AA%E5%8B%95%E8%AA%BF%E6%95%B4%E4%BB%BB%E6%84%8F)
  - [WebGL日本語入力(任意)](#webgl%E6%97%A5%E6%9C%AC%E8%AA%9E%E5%85%A5%E5%8A%9B%E4%BB%BB%E6%84%8F)
- [参考リンク](#%E5%8F%82%E8%80%83%E3%83%AA%E3%83%B3%E3%82%AF)
  - [環境](#%E7%92%B0%E5%A2%83)
  - [実装](#%E5%AE%9F%E8%A3%85)
  - [素材](#%E7%B4%A0%E6%9D%90)
- [機能](#%E6%A9%9F%E8%83%BD)
  - [時限処理](#%E6%99%82%E9%99%90%E5%87%A6%E7%90%86)
  - [コルーチン](#%E3%82%B3%E3%83%AB%E3%83%BC%E3%83%81%E3%83%B3)
  - [リソースからプレハブ読み込み](#%E3%83%AA%E3%82%BD%E3%83%BC%E3%82%B9%E3%81%8B%E3%82%89%E3%83%97%E3%83%AC%E3%83%8F%E3%83%96%E8%AA%AD%E3%81%BF%E8%BE%BC%E3%81%BF)
  - [オブジェクト作成後、コンポーネント追加](#%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E4%BD%9C%E6%88%90%E5%BE%8C%E3%82%B3%E3%83%B3%E3%83%9D%E3%83%BC%E3%83%8D%E3%83%B3%E3%83%88%E8%BF%BD%E5%8A%A0)
- [メモ](#%E3%83%A1%E3%83%A2)
  - [注意](#%E6%B3%A8%E6%84%8F)
  - [課題](#%E8%AA%B2%E9%A1%8C)
  - [未実装](#%E6%9C%AA%E5%AE%9F%E8%A3%85)
- [流用方法](#%E6%B5%81%E7%94%A8%E6%96%B9%E6%B3%95)
  - [必要ファイル](#%E5%BF%85%E8%A6%81%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB)
  - [手順](#%E6%89%8B%E9%A0%86)

<!-- /TOC -->

# 操作方法
#### 操作方法  
- 通常時
  - 画面タッチ/クリック：左右移動
  - 自機タッチ/クリック：弾発射

- ダウン時
  - 画面タッチ/クリック：復帰カウント減少

# デモ
[爆裂ハンド](https://little-hoge.github.io/Exploding/)  
[![main](https://user-images.githubusercontent.com/3638785/79036062-4c960b00-7bff-11ea-8378-8555aa1de6b2.gif)](https://little-hoge.github.io/Exploding/)

# 開発環境
- Windows10 64bit
- unity2019.2.0f1  unity日本語化(https://www.sejuku.net/blog/56333)
- Visual C# 2019
- jdk1.8.0_25
- android-ndk-r13b

# 使用アセット
#### ユーザーデータ管理(必須)
- NCMB(https://github.com/NIFCLOUD-mbaas/ncmb_unity/releases) \
※登録(https://console.mbaas.nifcloud.com/)

#### Unityエディタ上でタッチの動作確認(必須)
- GodTouch(https://github.com/okamura0510/GodTouch) \
※スマホに対応する場合便利

#### バージョン管理(任意)
- Github for Unity(https://miyagame.net/github-for-unity/) \
※登録(https://github.com/) \
※使い方(https://qiita.com/toRisouP/items/97c4cddcb735acde2f03)  

#### WebGL画面サイズ自動調整(任意)
- WebGL responsive template(https://github.com/miguel12345/UnityWebglResponsiveTemplate) \
※WebGLに対応する場合便利  

#### WebGL日本語入力(任意)
- WebGLInput(https://github.com/kou-yeung/WebGLInput) \
※WebGLに対応する場合便利  

# 参考リンク
#### 環境
- UnityからAndroid実機ビルド手順(2017.08版)   
https://qiita.com/relzx/items/7f8e7817c9edd11c5023

- 【超初心者】Unityで使えるフォントを増やす方法 Google Font編  
https://yagigame.hatenablog.com/entry/2018/10/25/212020  
※**WebGL**に対応する場合、一部フォントを表示できない為、**必須**

#### 実装
- 【Unity入門】60分で作るシューティングゲーム　第１回  
http://nn-hokuson.hatenablog.com/entry/2016/07/04/213231  

- 【Unity】レイヤー設定でオブジェクト同士の衝突判定を無くす方法  
https://gomafrontier.com/unity/1189

- Unity ParticleSystemで打ち上げ花火(Unity5.5対応)  
https://qiita.com/ELIXIR/items/f28c98196540e31d4fc8

#### 素材
- Google Fonts  
https://fonts.google.com/

- OtoLogic  
https://otologic.jp/free/se/quiz01.html

- SoundManagerのC#スクリプト  
https://00m.in/Lp0Up

# 機能
#### 時限処理
```C#
using UnityEngine;

// 2秒後に実行
Invoke("関数名", 2)

// 1秒後に実行、更に2秒ごとに実行
InvokeRepeating("関数名", 1,2);

// ～秒ごとの実行を止める
CancelInvoke()
```  

#### コルーチン
```C#
using UnityEngine;

// コルーチン定義
IEnumerator 関数名()
{
    // 当たり判定を消し、半透明にする
    gameObject.layer = LayerMask.NameToLayer("Player_Invincible");
    PlayerSprite.material.color = new Color(1, 1, 1, 0.5f);
    yield return new WaitForSeconds(秒数);

    // 通常に戻す
    gameObject.layer = LayerMask.NameToLayer("Player");
    PlayerSprite.material.color = new Color(1, 1, 1, 1);
}


// コルーチン呼び出し
StartCoroutine("関数名");

// コルーチン停止
StopAllCoroutines();

```  
#### リソースからプレハブ読み込み
```C#
// ランキング用プレハブ読み込み
GameObject obj = (GameObject)Resources.Load("Prefab/プレハブファイル名");
// インスタンスを生成
Instantiate(obj, Vector3.zero, m_GameObject.identity);
```

#### オブジェクト作成後、コンポーネント追加
```C#
// ゲームオブジェクト生成
GameObject obj = new GameObject("オブジェクト名");
obj.AddComponent<コンポーネント名>();

// ランキング用オブジェクト引き継ぎ
DontDestroyOnLoad(obj);
```

# メモ
#### 注意

#### 課題
 - おそらく敵破壊時オブジェクト破棄ではなく、非アクティブにし再生生成時に
    非アクティブオブジェクトがある場合再設定で使い回す方が良さげ。  

#### 未実装


# 流用方法
#### 必要ファイル  
- NCMB(https://github.com/NIFCLOUD-mbaas/ncmb_unity/releases)  
- GodTouch(https://github.com/okamura0510/GodTouch)  

#### 手順  
[参考](https://github.com/little-hoge/EarlyPush#%E6%89%8B%E9%A0%86)  

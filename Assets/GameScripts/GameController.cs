using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ゲーム全体を制御する/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>ゲーム中か判定するフラグ </summary>
    public bool m_isInGame = false;
    /// <summary>ボール/// </summary>
    BallController m_ball;
    /// <summary>ボールの初期位置/// </summary>
    Vector2 m_initialPositionOfBall;
    /// <summary>メッセージを表示するtext/// </summary>
    Text m_console;
    // 敵オブジェクトを保存する配列宣言
    private GameObject[] enemyObjects;

    // Get component TargetBallGenerator
    GameObject tbg;
    public bool flag = false;
	public bool destroy_flag = false;
	public bool bgm_gameOver_flag = false;
	public bool bgm_gameClear_flag = false;

    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {      


        if (m_isInGame) //ゲーム中の場合
        {
            // Enemyというtagが付いているobjectのdataを配列"enemyObjects"に入れる
            enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

            //dataの入った要素数が0に等しくなった時(Enemyというtagがついているobjectが全滅したとき)
            if (enemyObjects.Length == 0)
            {
                // ゲームクリアメソッドを呼び出す
                ClearGame();
            }
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Return)) //ゲーム中でない場合
            {
                
                m_isInGame = true;
                GetComponent<AudioSource>().Play();  
                StartGame();
			}
        }


    }

    void InitializeGame()
    {
		// OG:PlayerBallを取得
		GameObject ballObject = GameObject.Find("PlayerBall");
        // ballの初期位置を取得
        m_initialPositionOfBall = ballObject.transform.position;
        // ballについてるBallController.scのcomponentを取得
        m_ball = ballObject.GetComponent<BallController>();
		// GO:ConsoleTextを取得
        GameObject consoleObject = GameObject.Find("ConsoleText");
        // consoleObjectの"Text"Componentを取得
        m_console = consoleObject.GetComponent<Text>();
        m_console.text = "Hit Enter To Start";
		//BgmControllerを取得
		GameObject bgm = GameObject.Find("BGM");
		BgmController bgm_c = bgm.GetComponent<BgmController>();





    }

    void StartGame()
    {

        destroy_flag = false;
        flag = true;
		// ballの座標に初期位置の座標を代入する
        m_ball.transform.position = m_initialPositionOfBall;
        m_ball.Reset();
        m_console.text = "";
        tbg = GameObject.Find("EOGenerator");
		// TargetBallGeneratorからResetBall()を呼び出す
        TargetBallGenerator d2 = tbg.GetComponent<TargetBallGenerator>();
        d2.ResetBall();
		//BgmControllerを取得
        GameObject bgm = GameObject.Find("BGM");
        BgmController bgm_c = bgm.GetComponent<BgmController>();
        //bgm Start
		bgm_c.Reset();
    }

    public void EndGame()
    {
        m_isInGame = false;
		destroy_flag = true;
        flag = false;
		bgm_gameOver_flag = true;
        m_console.text = "GameOver...\r\nHit Enter To Restart";

		//BgmControllerを取得
        GameObject bgm = GameObject.Find("BGM");
        BgmController bgm_c = bgm.GetComponent<BgmController>();
		bgm_c.GameOver();

    }

    void ClearGame()
    {
        m_isInGame = false;
		bgm_gameClear_flag = true;
        m_console.text = "Congratulation!!! Game Clear!!!\r\nHit Enter To Restart";

		//BgmControllerを取得
        GameObject bgm = GameObject.Find("BGM");
        BgmController bgm_c = bgm.GetComponent<BgmController>();
		bgm_c.GameClear();


    }
}
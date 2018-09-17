using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
	public int count = 10;
	//public int SettingCount;
	private Text CText;
	GameObject gc;
	GameObject bc;
	Rigidbody2D bc_rb2d;
    
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//GameControllerを取得
        gc = GameObject.Find("GameController");
        GameController d1 = gc.GetComponent<GameController>();
        //BallControllerを取得
		bc = GameObject.Find("PlayerBall");
        // PlayerBallのRigidbody2Dを取得
		bc_rb2d = bc.GetComponent<Rigidbody2D>();
        //CountTextを取得
        CText = GetComponentInChildren<Text>();
                      
        // ゲーム中の場合
		if (d1.m_isInGame)
		{
			//残りcountを表示する
            CText.text = "Last\n\r" + count.ToString("00");
			//カウントが1以上且つballが止まっているならば(何故か機能してくれない為BallController.cs(78)で実装)
/*			if (count > 0 && bc_rb2d.IsSleeping())
			{
				//マウスドラッグでボールを弾いた後count--実行
				if (Input.GetMouseButtonUp(0))
				{
					count--;
				}
			}
*/
			//countが0且つPlayerballが止まったらGameControllerClass EndGame Methodを呼び出す
			if(count == 0 && bc_rb2d.IsSleeping())
			{
				d1.EndGame();
				count = 10;
			}
		}
		else
		{
			count = 10;
        }
	}

}

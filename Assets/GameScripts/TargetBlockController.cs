using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBlockController : MonoBehaviour
{
	int life = 2;
	GameObject gc;
    
    private void Start()
    {

    }
    
    private void Update()
    {
		// GameController.csをscript内で使用可能にする
        gc = GameObject.Find("GameController");
        GameController d1 = gc.GetComponent<GameController>();

        // GameOverの場合残ったobjectを消去する
		if(d1.destroy_flag)
		{
			Destroy(this.gameObject);
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            life--;
        }

        if (life < 1)
        { 
            Destroy(this.gameObject,1.0f);

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }
}
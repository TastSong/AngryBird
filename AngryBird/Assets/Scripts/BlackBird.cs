using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    public List<Pig> blacks = new List<Pig>();//存储碰撞盒周围的物体

    private bool isBoom = false; //判断炸弹是否爆炸
    /// <summary>
    /// 进入触发区域
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blacks.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
    /// 离开触发区域
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blacks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }
    public override void ShowSkill()
    {
        base.ShowSkill();
        if (blacks.Count > 0 && blacks != null)
        {
            for (int i = 0; i < blacks.Count; i++)
            {
                blacks[i].Dead();
            }
        }

        OnClear();
        isBoom = true;
    }
    /// <summary>
    /// 爆炸之后就不能再动了等系列效果
    /// </summary>
    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.ClearTrails();

    }

    protected override void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
        if(!isBoom)
        {
            Instantiate(boom, transform.position, Quaternion.identity);
        }
    }
}

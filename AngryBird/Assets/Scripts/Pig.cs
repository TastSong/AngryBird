using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 8;//用于检测猪被碰撞后的死亡受伤判断
    public float minSpeed = 4;
    private SpriteRenderer render;//猪的照片显示
    public Sprite hurt;//存储受伤照片

    public GameObject boom;//存储爆炸效果
    public GameObject score;//分数显示

    public bool isPig = false; //判断组件是否为猪，因为这个组件要和木头够用

    public AudioClip hurtClip;//猪受伤的声音
    public AudioClip dead;//猪死亡的声音
    public AudioClip birdCollision;//猪被小鸟碰撞后的声音
    
    //获取照片显示组件
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    //猪的死亡受伤判断
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //播放小鸟碰到猪的声音
        if(collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollision);
            //当小鸟碰到Player时受伤
            collision.transform.GetComponent<Bird>().Hurt();
        }
        //print(collision.relativeVelocity.magnitude);
        if(collision.relativeVelocity.magnitude > maxSpeed)
        {//死亡
            Dead();
        }
        if(collision .relativeVelocity.magnitude < maxSpeed && collision.relativeVelocity.magnitude > minSpeed)
        {//受伤
            render.sprite = hurt;
            //受伤的声音
            AudioPlay(hurtClip);
        }
        
    }

    //猪死亡之后的效果
    public void Dead()
    {
        if(isPig)
        {//移除死亡的猪
            GameManager._instance.pig.Remove(this);
        }
       //销毁猪组件
        Destroy(gameObject);
        //爆炸
        Instantiate(boom, transform.position, Quaternion.identity);
        //分数
        GameObject go = Instantiate(score, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        Destroy(go, 1.5f);
        //猪死亡声音
        AudioPlay(dead);
    }
    /// <summary>
    /// 静态音乐片段播放方法
    /// </summary>
    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}

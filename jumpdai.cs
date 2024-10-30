using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpdai : MonoBehaviour
{

    public  Animator A;
    public GameObject efect;//葉っぱのエフェクト

    [Header("回数消滅")]
    public bool s = false;
    public int k;
    [Header("移動")]
    public bool m = false;
    float sp = 5;
    public float x;
    public bool migi = false;
    float r;
    float shoatt;
    Rigidbody2D r2;

    //非表示　葉っぱ追加
    float t = 0; //復活時間
    int kk; //踏んだ回数
    bool humare; //踏まれた


    public void Start()
    {
        if(m)
        {
            shoatt = transform.position.x;
            r2 = this.GetComponent<Rigidbody2D>();
            r = transform.position.x + x;
            
            
            if (migi)
            {
                transform.position = new Vector3(r, transform.position.y, transform.position.z);
            }
        }
    }

    public void FixedUpdate()
    {
  
        if (m && !humare)
        {
            if(migi)
            {
                sp = (r - transform.position.x) * 3;
                if (x >= 0)
                {
                    if (transform.position.x >= r)
                    {
                        sp = 0;
                    }
                }
                else
                {
                    if (transform.position.x <= r)
                    {
                        sp = 0;
                    }
                }
                r2.velocity = new Vector2(sp, 0);
            }
            else
            {
                sp = (shoatt - transform.position.x) * 3;
                if (x >= 0)
                {
                    if (transform.position.x <= shoatt)
                    {
                        sp = 0;
                    }
                }
                else
                {
                    if (transform.position.x >= shoatt)
                    {
                        sp = 0;
                    }
                }
                r2.velocity = new Vector2(sp, 0);
            }

        }

        if (s)
        {
            if(k - 1 <= kk)
            {
                A.SetBool("kowaresou", true);
            }
        }
    }

    public void AA()
    {
        humare = true;
        if(s)
        kk++;
        migi = !migi;
        if (k <= kk && s)
        {
            A.SetBool("kowareta", true);
        }
        else
        {
            
            A.SetBool("bon", true);
        }
    }

    public  void BA()
    {
        humare = false;
        A.SetBool("bon", false);
    }
    public void hukkatu()
    {
        humare = false;
        kk = 0;
        A.SetBool("kowareta", false);
        A.SetBool("kowaresou", false);
    }
    public void happa()
    {
        float a = Random.Range(-0.5f, 0.6f);
        Instantiate(efect, new Vector3(transform.position.x + a,transform.position.y - 0.8f,transform.position.z), transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class shot_Base : MonoBehaviour
{
    //�J�X�^�}�C�Y���R

    public enum elemental //����
    {
        normal,
        stan,
    }
    public enum who //�G�@��l���@���֌W
    { 
        larua,
        enemy,
        boss,
        mukankei
    }
    [Header("����")]
    public elemental e;
    [Header("�N?")]
    public who w;

    [SpaceAttribute(5)]
    [Header("�R���|�[�l���g")]
    public Rigidbody2D R2;
    public SpriteRenderer SR;
    public Animator A;

    [SpaceAttribute(5)]
    [Header("�X�^�C��")]

    [TooltipAttribute("���ɐi��")]
    public bool straight_line;
    [TooltipAttribute("�c�ɐi��")]
    public bool vertical;
    [TooltipAttribute("�~�ړ�")]
    public bool circle;
    [TooltipAttribute("�W�O�U�O")]
    public bool zigzag;
    [TooltipAttribute("��ʊO����")]
    public bool shometu;

    [SpaceAttribute(5)]
    [Header("���\")]
    public  int damage;//�_���[�W
    public bool kantu;//�ђʁ@�L����
    public bool kantu2;//�ђʁ@��
    public bool kantu3;//�ђʁ@�V���b�g
    [Header("�ӂ�ނ�")]
    public bool huri = false;

    [SpaceAttribute(5)]
    [Header("�c���̐��l")]

    public float x; //��
    public float y; //�c

    public GameObject[] GG = new GameObject[2]; //�G�t�F�N�g
    public Renderer R;

    [Header("�Ǘ��Ō����擾")]
    public bool yes = false;

    [Header("�V���b�N�E�F�[�u")]
    public bool shock = false;
    public float rf;
    public float iti;

    BGM_Base b;

    private void Awake()
    {
        b = GameObject.Find("BGM").GetComponent<BGM_Base>();
        if(!R)
        R = GetComponentInChildren<SpriteRenderer>();
    if(yes)
        {
            SR.flipX = GetComponentInParent<SpriteRenderer>().flipX;
            transform.parent = null;
        }

    
    }
    void OnDrawGizmos()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Vector2(transform.localPosition.x + iti, transform.localPosition.y), Vector3.down * rf);
    }
    public void Start()
    {
        if (shock)
        {
            var aa = 1 << 3;
            var bb = 1 << 13;
            var cc = aa + bb;
            RaycastHit2D r2 = Physics2D.Raycast(new Vector2(transform.localPosition.x + iti, transform.localPosition.y), Vector2.down * rf, rf, cc); ;
            if(r2.collider == null)
            {
                Destroy(gameObject);
            }
        }
    }

    public virtual void FixedUpdate()
    {

        if(straight_line | vertical)
        {
            if (SR.flipX)
            {
                xy(x, y);
            }
            else
            {
                xy(-x, y);
            }
        }
        if(!R.isVisible && shometu)
        {
            Destroy(gameObject);
        }

    }

    public void xy(float xx,float yy)
    {
        if(!straight_line)
        {
            xx = R2.velocity.x;
        }
        if (!vertical)
        {
            yy = R2.velocity.y;
        }
        R2.velocity = new Vector2(xx, yy);
    }
    public virtual void OnTriggerEnter2D(Collider2D C2)
    {
        if (w == who.larua)
        {
            if (C2.tag == "Enemy")
            {
                GameObject G = C2.gameObject;
                Action_Base E = G.GetComponent<Action_Base>();

                if (E && E.life > 0)
                {
                    if (!E.m)
                    {
                        efect();
                        damages(E);
                    }
                    else
                    {
                        efect(1);

                    }
                    if (!kantu)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }

    }
    public  virtual void OnTriggerStay2D(Collider2D C2)
    {
        if(!kantu2)
            if (C2.tag == "Block")
            {

                if (C2.gameObject.layer == 11 && R2.velocity.y >= 0 || C2.gameObject.layer == 13 && R2.velocity.y >= 0)
                {
                }
                else
                {
                    efect();
                    Destroy(this.gameObject);
                }
            }
    }

    public void damages(Action_Base E)
    {
        E.damageE(damage);
        if(huri)
        E.mukii(transform.position,E.transform.position);
        if (E.life <= damage)
            E.mukii(transform.position, E.transform.position);
        if (e == elemental.stan)
        {

            E.stan_start(3);
        }
    }

    //�G�t�F�N�g
    public void efect(int a = 0)
    {
        if (a == 0)
        {
            b.se(24);
            Instantiate(GG[0], transform.position, transform.rotation);
        }
        else
        {
            b.se(25);
            if (GG[1])
            {
                Instantiate(GG[1], transform.position, transform.rotation);
            }
        }
    }
 }
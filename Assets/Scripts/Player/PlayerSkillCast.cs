using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCast : MonoBehaviour
{
    [Header("Mana Settings")]
    public float totalMana=100f;
    public float manaRegen;
    public Image manaBar;
    [Header("Cooldown Icons")] public Image[] CooldownImages;
    [Header("ManaCooldown Icons")] public Image[] ManaCooldownImages;
    [Header("Cooldown Times")] 
    public float CooldownTime1;
    public float CooldownTime2;
    public float CooldownTime3;
    public float CooldownTime4;
    public float CooldownTime5;
    public float CooldownTime6;
    [Header("Mana Amounts")] 
    public float Skill1ManaAmount = 20f;
    public float Skill2ManaAmount = 20f;
    public float Skill3ManaAmount = 20f;
    public float Skill4ManaAmount = 20f;
    public float Skill5ManaAmount = 20f;
    public float Skill6ManaAmount = 20f;

    private bool faded = false;
    private int[] fadeImages = new int[] {0, 0, 0, 0, 0, 0};
    public List<float> CooldownTimesList = new List<float>();
    public List<float> ManaAmountList = new List<float>();

    private Animator _animator;
    private bool _canAttack = false;

    private PlayerOnClick _playerOnClick;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerOnClick = GetComponent<PlayerOnClick>();
        manaBar = GameObject.Find("ManaOrb").GetComponent<Image>();
    }

    void Start()
    {
        AddList();
    }
    void AddList()
    {
        CooldownTimesList.Add(CooldownTime1);
        CooldownTimesList.Add(CooldownTime2);
        CooldownTimesList.Add(CooldownTime3);
        CooldownTimesList.Add(CooldownTime4);
        CooldownTimesList.Add(CooldownTime5);
        CooldownTimesList.Add(CooldownTime6);
        //
        ManaAmountList.Add(Skill1ManaAmount);
        ManaAmountList.Add(Skill2ManaAmount);
        ManaAmountList.Add(Skill3ManaAmount);
        ManaAmountList.Add(Skill4ManaAmount);
        ManaAmountList.Add(Skill5ManaAmount);
        ManaAmountList.Add(Skill6ManaAmount);
    }
    void Update()
    {
        if (!_animator.IsInTransition(0) && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            _canAttack = true;
        }
        else
        {
            _canAttack = false;
        }

        if (_animator.IsInTransition(0)&&_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            TurnThePlayer();
        }
        if (totalMana<100f)
        {
            totalMana += Time.deltaTime * manaRegen;
            manaBar.fillAmount = totalMana/100f;
        }
        CheckMana();
        CheckToFade();
        CheckInput();
    }

    private void TurnThePlayer()
    {
        Vector3 targetPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.y);
        }
        transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(targetPosition-transform.position),_playerOnClick.turnSpeed*Time.deltaTime);
    }

    void CheckInput()
    {
        if (_animator.GetInteger("Attack")==0)
        {
            _playerOnClick.FinishedMovement = false;
            if (!_animator.IsInTransition(0) && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                _playerOnClick.FinishedMovement = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)&& totalMana >= Skill1ManaAmount)
        {
            _playerOnClick.TargetPosition = transform.position;
            if (_playerOnClick.FinishedMovement==true && fadeImages[0]!= 1 && _canAttack)
            {
                totalMana -= Skill1ManaAmount;
                fadeImages[0] = 1;
                _animator.SetInteger("Attack",1);
            }
        }else if (Input.GetKeyDown(KeyCode.Alpha2)&& totalMana >= Skill2ManaAmount)
        {
            _playerOnClick.TargetPosition = transform.position;
            if (_playerOnClick.FinishedMovement==true && fadeImages[1]!= 1 && _canAttack)
            {
                totalMana -= Skill2ManaAmount;
                fadeImages[1] = 1;
                _animator.SetInteger("Attack",2);
            } 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)&& totalMana >= Skill3ManaAmount)
        {
            _playerOnClick.TargetPosition = transform.position;
            if (_playerOnClick.FinishedMovement==true && fadeImages[2]!= 1 && _canAttack)
            {
                totalMana -= Skill3ManaAmount;
                fadeImages[2] = 1;
                _animator.SetInteger("Attack",3);
            } 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)&& totalMana >= Skill4ManaAmount)
        {
            _playerOnClick.TargetPosition = transform.position;
            if (_playerOnClick.FinishedMovement==true && fadeImages[3]!= 1 && _canAttack)
            {
                totalMana -= Skill4ManaAmount;
                fadeImages[3] = 1;
                _animator.SetInteger("Attack",4);
            } 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5)&& totalMana >= Skill5ManaAmount)
        {
            _playerOnClick.TargetPosition = transform.position;
            if (_playerOnClick.FinishedMovement==true && fadeImages[4]!= 1 && _canAttack)
            {
                totalMana -= Skill5ManaAmount;
                fadeImages[4] = 1;
                _animator.SetInteger("Attack",5);
            } 
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6)&& totalMana >= Skill6ManaAmount)
        {
            _playerOnClick.TargetPosition = transform.position;
            if (_playerOnClick.FinishedMovement==true && fadeImages[5]!= 1 && _canAttack)
            {
                totalMana -= Skill6ManaAmount;
                fadeImages[5] = 1;
                _animator.SetInteger("Attack",6);
            } 
        }
        else
        {
            _animator.SetInteger("Attack",0);
        }
    }
    void CheckToFade()
    {
        for (int i = 0; i < CooldownImages.Length; i++)
        {
            if (fadeImages[i]==1)
            {
                if (FadeAndWait(CooldownImages[i],CooldownTimesList[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }
    }

    void CheckMana()
    {
        for (int i = 0; i < ManaCooldownImages.Length; i++)
        {
            if (totalMana<ManaAmountList[i])
            {
                ManaCooldownImages[i].gameObject.SetActive(true);
            }
            else
            {                
                ManaCooldownImages[i].gameObject.SetActive(false);
            }
        }
    }
    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        faded = false;
        if (fadeImage == null)
        {
            return faded;
        }

        if (!fadeImage.gameObject.activeInHierarchy)
        {
           fadeImage.gameObject.SetActive(true);
           fadeImage.fillAmount = 1f;
        }
        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if (fadeImage.fillAmount <= 0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }
}
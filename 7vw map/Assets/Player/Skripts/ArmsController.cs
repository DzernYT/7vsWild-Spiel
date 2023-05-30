using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ArmsController : MonoBehaviour
{
    [Header("Animation Rigging")]
    [SerializeField] private Rig changeWeaponRig;
    [SerializeField] private TwoBoneIKConstraint rightHandIK;
    public float changeWeaponDuration = 0.3f;

    [Header("Weapon")]
    [SerializeField] private GameObject WeaponContainer;
    public bool isWeaponAttached = true;

    private bool isWeaponChanging;

    Animator armsAnimator;
    PlayerMovment playerMovment;
    PlayerController playerController;

    

    void Start()
    {
        armsAnimator = GetComponent<Animator>();

        playerMovment = FindObjectOfType<PlayerMovment>();
        playerMovment.OnPlayerSprintingUpdate += HandleSprintingAnimation;
        playerMovment.OnPlayerWalkingUpdate += HandleWalkingAnimation;
        playerMovment.OnPlayerIdleUpdate += HandleIdleAnimation;

        playerController = FindObjectOfType<PlayerController>();   
        playerController.OnWeaponChangeUpdate += HandlePlayerWeaponChange;
        playerController.OnAttackUpdate += HandlePlayerAttack;
        playerController.OnGrabingUpdate += HandlePlayerGrabing;


        WeaponManager();
    }

    
    void Update()
    {
        if (isWeaponChanging)
        {
            // Set Rig Weight to 1
            changeWeaponRig.weight += Time.deltaTime / changeWeaponDuration;

            if (changeWeaponRig.weight == 1)
            {
                isWeaponChanging = false;
            }
        }
        // If Rig Weight is 0, time to Change Weapon 




        // Set Rig Weight to 0
        else
            changeWeaponRig.weight -= Time.deltaTime / changeWeaponDuration;
    }


    public void WeaponManager()
    {
        if (isWeaponAttached)
        {
            rightHandIK.weight = 1;
            WeaponContainer.SetActive(true);
        }
        else
        {
            rightHandIK.weight = 0;
            WeaponContainer.SetActive(false);
        }
    }

    //Checks if Player is Idle
    public void HandleIdleAnimation()
    {
        armsAnimator.SetFloat("speed", 0f, 0.1f, Time.deltaTime);
    }

    //Checks if Player is Walking
    public void HandleWalkingAnimation()
    {
        armsAnimator.SetFloat("speed", 0.5f, 0.2f, Time.deltaTime);
    }

    //Checks if Player is sprinting
    public void HandleSprintingAnimation()
    {
        armsAnimator.SetFloat("speed", 1f, 0.2f, Time.deltaTime);
    }

    //Checks if Player change Weapon
    public void HandlePlayerWeaponChange(bool changeWeapon)
    {
        if (changeWeapon)
            isWeaponChanging = true; 
    }

    //Checks if Player is Attacking 
    public void HandlePlayerAttack()
    {
        if(isWeaponAttached && rightHandIK.weight == 1)
        {
            armsAnimator.SetTrigger("isAttackWithWeapon");
        }
        else if(!isWeaponAttached && rightHandIK.weight == 0)
        {
            armsAnimator.SetTrigger("isAttackWithHand");
        }
    }

    //Checks if Player is Grabing 
    public void HandlePlayerGrabing(bool isGrabing)
    {
        if (isGrabing)
            armsAnimator.SetTrigger("isGrabing");
    }
}

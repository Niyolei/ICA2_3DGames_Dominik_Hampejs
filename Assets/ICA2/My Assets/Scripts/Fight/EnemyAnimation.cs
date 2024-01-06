using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    private int fightHash = Animator.StringToHash("Fight");
    
    public void SetFight(bool fight)
    {
        animator.SetBool(fightHash, fight);
    }
}

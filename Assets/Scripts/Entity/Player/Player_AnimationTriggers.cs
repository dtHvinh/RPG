using System.Collections;
using UnityEngine;

public class Player_AnimationTriggers : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void CurrentStateTrigger()
    {
        player.CallAnimationTrigger();
    }

    private void Test()
    {
        Debug.Log("This animation start");
    }
}

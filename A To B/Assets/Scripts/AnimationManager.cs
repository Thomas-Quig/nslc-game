using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Animation anim;

    public AnimationClip run;
    public AnimationClip sprint;
    public AnimationClip land;
    public AnimationClip idle;
    public AnimationClip strafeLeft;
    public AnimationClip strafeRight;
    public AnimationClip staticAir;
    public AnimationClip forwardAir;
    public AnimationClip runBackwards;

    public AnimationClip headBob;

    public Rigidbody player;

    float direction;


    // Update is called once per frame
    void Update () {
        direction = transform.InverseTransformDirection(player.velocity).z;

        if (player.GetComponent<RigidbodyFPSWalker>().canJump) {
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    playAnim(sprint.name);
                }
                else
                {
                    playAnim(run.name);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playAnim(runBackwards.name);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playAnim(strafeLeft.name);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playAnim(strafeRight.name);
            }
            else
            {
                playAnim(idle.name);
            }
        }
        else
        {
            if (direction >= 1.0f)
            {
                playAnim(forwardAir.name);
            }
            else
            {
                playAnim(staticAir.name);
            }
        }
    }

    public void playAnim(string animName) {
        anim.CrossFade(animName, 0.3f);
    }

    public void stopAnim() {
        anim.Stop();
    }
}

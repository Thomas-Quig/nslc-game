using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    public Animation anim;

    public AnimationClip run;
    public AnimationClip jump;
    public AnimationClip idle;
    public AnimationClip strafeLeft;
    public AnimationClip strafeRight;
    public AnimationClip midair;
    public AnimationClip runBackwards;

    public AnimationClip headBob;

    public Rigidbody player;
	
	// Update is called once per frame
	void Update () {

        if (player.GetComponent<RigidbodyFPSWalker>().grounded) {
            if (Input.GetKey(KeyCode.W))
            {
                playAnim(run.name);
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
        } else {
            if (!anim.IsPlaying(jump.name)){
                playAnim(midair.name);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBook : MonoBehaviour {

	private Animator animator;
	private AnimatorStateInfo state;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		state = animator.GetCurrentAnimatorStateInfo(0);
	}
	
	// Update is called once per frame
	void Update () {
		state = animator.GetCurrentAnimatorStateInfo(0);
		// Debug.Log("stat : " + state.fullPathHash + "   hash :" + Animator.StringToHash("Base Layer.0 Upper Left"));
		if (state.fullPathHash == Animator.StringToHash("Base Layer.0 Upper Left"))
		{
			Destroy(gameObject);
		}
	}
}

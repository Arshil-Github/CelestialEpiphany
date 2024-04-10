using System;
using Unity;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] private AnimationClip effectAnimation;
    [SerializeField] private AudioClip effectAudio;

    private Animator animator;
    private Action actionToCall = () => { };
    private AudioSource source;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(effectAnimation.name);

        source = GetComponent<AudioSource>();
        source.clip = effectAudio;
        source.Play();
    }
    public void Setup(Action toCall)
    {
        actionToCall = toCall;
    }
    private void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && !source.isPlaying)
        {
            actionToCall();
            Destroy(gameObject);
        }
    }
}
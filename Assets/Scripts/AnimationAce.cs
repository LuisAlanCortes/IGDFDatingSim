using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using UnityEngine;


public class AnimationAce : MonoBehaviour
{
    // Start is called before the first frame updat
    public Vector2 moveDistance = new Vector2(0,0);
    public float moveDuration = 1f;
    public int loopCount = 3;

    void Start()
    {
    
    }

    public void ShuffleOne() // Moves the card to the middle.
    {
        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetLeftPosition = initialPosition - moveDistance;
        Vector2 targetRightPosition = initialPosition + moveDistance;

        // Sets up leftTween. Handles moving too the left.
        Tweener leftTween = transform.DOMove(targetLeftPosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener rightTween = transform.DOMove(targetRightPosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        sequence.Append(leftTween);
        sequence.Append(rightTween);
        sequence.Append(initialTween);
        sequence.SetLoops(3);
        
    }

    public void ShuffleTwo() // Sets Ace to far left.
    {
        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetLeftPosition = initialPosition - moveDistance;
        Vector2 targetRightPosition = initialPosition + moveDistance;

        // Sets up leftTween. Handles moving too the left.
        Tweener leftTween = transform.DOMove(targetLeftPosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener rightTween = transform.DOMove(targetRightPosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        sequence.Append(initialTween);
        sequence.Append(rightTween);
        sequence.Append(leftTween);
        sequence.SetLoops(3);

    }

    public void ShuffleThree() // Sets Ace to far right.
    {
        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetLeftPosition = initialPosition - moveDistance;
        Vector2 targetRightPosition = initialPosition + moveDistance;

        // Sets up leftTween. Handles moving too the left.
        Tweener leftTween = transform.DOMove(targetLeftPosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener rightTween = transform.DOMove(targetRightPosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        sequence.Append(leftTween);
        sequence.Append(initialTween);
        sequence.Append(rightTween);

    
        sequence.SetLoops(3);
        sequence.SetAutoKill(false);

    }

    
}


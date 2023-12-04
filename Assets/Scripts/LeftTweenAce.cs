using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class LeftTweenAce : MonoBehaviour
{
    // Start is called before the first frame updat
    public Vector2 moveDistance = new Vector2(0,0);
    public Vector2 moveDistanceOffset = new Vector2(0,0);
    public float moveDuration = 1f;
    public int loopCount = 3;
    

    void Start()
    {
        
    }

    public void ShuffleOne() // Sets ACE to the far right.
    {
        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetMiddlePosition = initialPosition + moveDistance;
        Vector2 targetRightPosition = initialPosition + moveDistanceOffset;

        // Sets up leftTween. Handles moving too the left.
        Tweener middleTween = transform.DOMove(targetMiddlePosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener rightTween = transform.DOMove(targetRightPosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        sequence.SetAutoKill(false);
        sequence.Append(middleTween);
        sequence.Append(initialTween);
        sequence.Append(rightTween);
        sequence.SetLoops(3);
         // Game objects stays where they are.. yeesh.
        
    }

    public void ShuffleTwo() // Sets ACE(1) to the Middle.
    {

        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetMiddlePosition = initialPosition + moveDistance;
        Vector2 targetRightPosition = initialPosition + moveDistanceOffset;

        // Sets up leftTween. Handles moving too the left.
        Tweener middleTween = transform.DOMove(targetMiddlePosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener rightTween = transform.DOMove(targetRightPosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rightTween);
        sequence.Append(initialTween);
        sequence.Append(middleTween);
        
        
        sequence.SetLoops(3);
        sequence.SetAutoKill(false);
         // Game objects stays where they are.. yeesh.

    }

    public void ShuffleThree() // Sets back too left.
    {
        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetMiddlePosition = initialPosition + moveDistance;
        Vector2 targetRightPosition = initialPosition + moveDistanceOffset;

        // Sets up leftTween. Handles moving too the left.
        Tweener middleTween = transform.DOMove(targetMiddlePosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener rightTween = transform.DOMove(targetRightPosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rightTween);
        sequence.Append(middleTween);
        sequence.Append(initialTween);
        
        sequence.SetLoops(3);
        sequence.SetAutoKill(false);
         // Game objects stays where they are.. yeesh.

    }

    
}

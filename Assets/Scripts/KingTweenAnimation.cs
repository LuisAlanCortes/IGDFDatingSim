using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KingTweenAnimation : MonoBehaviour
{
    public Vector2 moveDistance = new Vector2(0,0);
    public Vector2 moveDistanceOffset = new Vector2(0,0);
    public float moveDuration = 1f;
    public int loopCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ShuffleOne() // Sets King to the far Left
    {
        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetLeftPosition = initialPosition - moveDistance;
        Vector2 targetMiddlePosition = initialPosition - moveDistanceOffset;

        // Sets up leftTween. Handles moving too the left.
        Tweener leftTween = transform.DOMove(targetLeftPosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener middleTween = transform.DOMove(targetMiddlePosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();

        sequence.Append(initialTween);
        sequence.Append(middleTween);
        sequence.Append(leftTween);
        sequence.SetLoops(3);
        sequence.SetAutoKill(false);
         // Game objects stays where they are.. yeesh.
    }

    public void ShuffleTwo() // Sets the king to the right.
    {
        // Moves between these three positions. move Distance is how far too move.
        Vector2 initialPosition  = transform.position;
        Vector2 targetLeftPosition = initialPosition - moveDistance;
        Vector2 targetMiddlePosition = initialPosition - moveDistanceOffset;

        // Sets up leftTween. Handles moving too the left.
        Tweener leftTween = transform.DOMove(targetLeftPosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener middleTween = transform.DOMove(targetMiddlePosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);

        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(middleTween);
        sequence.Append(leftTween);
        sequence.Append(initialTween);
        sequence.SetLoops(3);
        sequence.SetAutoKill(false);
         // Game objects stays where they are.. yeesh.
    }

    public void ShuffleThree() // Sets king too the middle.
    {
        // Moves between these three positions. move Distance is how far too move.
       Vector2 initialPosition  = transform.position;
        Vector2 targetLeftPosition = initialPosition - moveDistance;
        Vector2 targetMiddlePosition = initialPosition - moveDistanceOffset;

        // Sets up leftTween. Handles moving too the left.
        Tweener leftTween = transform.DOMove(targetLeftPosition, 1f);
        // Sets up the rightTween.. Handles moving to the Right.
        Tweener middleTween = transform.DOMove(targetMiddlePosition, 1f);

        Tweener initialTween = transform.DOMove(initialPosition, 1f);
        // Chains sequence. sequence.Append is basically saying "move left, then move right, etc."
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(leftTween);
        sequence.Append(initialTween);
        sequence.Append(middleTween);
        sequence.SetLoops(3);
        sequence.SetAutoKill(false);
         // Game objects stays where they are.. yeesh.
    }
}

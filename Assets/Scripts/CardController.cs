using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public KingTweenAnimation kingShuffle;
    public LeftTweenAce leftAce;
    public AnimationAce middleAce;


    public SpriteRenderer[] cardSprites;
    public Sprite[] cardDown;

    private Sprite[] originalSprites;
    private int randomNumber;
    // Start is called before the first frame update

    void Start()
    {
        originalSprites = new Sprite[cardSprites.Length];

        for (int i = 0; i < cardSprites.Length; i++)
        {
            originalSprites[i] = cardSprites[i].sprite;
        }
    }

    public void changeCardSprites()
    {
        if (cardSprites.Length != cardDown.Length)
        {
            Debug.Log("Renderers and Other sprites' arays are not the same.");
            return;
        }

        for (int i = 0; i < cardSprites.Length; i++)
        {
            cardSprites[i].sprite = cardDown[i];
        }
    }

    public void randomizeShuffles()
    {
        randomNumber = Mathf.FloorToInt(Random.Range(1,4));
        print(randomNumber);

        switch(randomNumber)
        {
            case 1:
            kingShuffle.ShuffleOne();
            middleAce.ShuffleOne();
            leftAce.ShuffleOne();
                break;

            case 2:
             kingShuffle.ShuffleTwo();
            middleAce.ShuffleTwo();
            leftAce.ShuffleTwo();
                break;

            case 3:
            kingShuffle.ShuffleThree();
            middleAce.ShuffleThree();
            leftAce.ShuffleThree();
                break;

            default:
            Debug.Log("Oh no: " + randomNumber);
                break;
        }
    }

    public void RevertToOriginalSprite()
    {
        for (int i = 0; i < cardSprites.Length && i <originalSprites.Length; i++)
        {
            cardSprites[i].sprite = originalSprites[i];
        }
        
    }
}

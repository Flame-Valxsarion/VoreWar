using UnityEngine;

namespace Assets.Scripts.Entities.Animations
{
    class VenomBite : AnimationBase
    {


        public void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingOrder = 20000;
            frames = new Frame[]
            {
            new Frame(State.GameManager.SpriteDictionary.SevilleBite[0], transform.position, .10f),
            new Frame(State.GameManager.SpriteDictionary.SevilleBite[1], transform.position, .08f),
            new Frame(State.GameManager.SpriteDictionary.SevilleBite[2], transform.position, .08f),
            new Frame(State.GameManager.SpriteDictionary.SevilleBite[3], transform.position, .15f),
            };
        }
    }
}

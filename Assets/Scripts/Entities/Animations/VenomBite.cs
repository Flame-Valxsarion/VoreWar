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
            new Frame(SpriteDictionary.SevilleBite[0], transform.position, .40f),
            new Frame(SpriteDictionary.SevilleBite[1], transform.position, .08f),
            new Frame(SpriteDictionary.SevilleBite[2], transform.position, .08f),
            new Frame(SpriteDictionary.SevilleBite[3], transform.position, .15f),
            };
        }
    }
}

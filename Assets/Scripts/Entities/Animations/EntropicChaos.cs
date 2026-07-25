using UnityEngine;

namespace Assets.Scripts.Entities.Animations
{
    class EntropicChaos : AnimationBase
    {


        public void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingOrder = 20000;
            frames = new Frame[]
            {
            new Frame(SpriteDictionary.Tatltuae[131], transform.position + new Vector3(0, 0.55f), .04f),
            new Frame(SpriteDictionary.Tatltuae[133], transform.position + new Vector3(0, 0.55f), .04f),
            new Frame(SpriteDictionary.Tatltuae[135], transform.position + new Vector3(0, 0.55f), .04f),
            new Frame(SpriteDictionary.Tatltuae[137], transform.position + new Vector3(0, 0.55f), .04f),
            new Frame(SpriteDictionary.Tatltuae[139], transform.position + new Vector3(0, 0.55f), .04f),
            new Frame(SpriteDictionary.Tatltuae[141], transform.position + new Vector3(0, 0.55f), .04f),
            new Frame(SpriteDictionary.Tatltuae[143], transform.position + new Vector3(0, 0.55f), .04f),
            new Frame(SpriteDictionary.Tatltuae[130], transform.position + new Vector3(0, 0.55f), .07f),
            new Frame(SpriteDictionary.Tatltuae[132], transform.position + new Vector3(0, 0.55f), .07f),
            new Frame(SpriteDictionary.Tatltuae[134], transform.position + new Vector3(0, 0.55f), .07f),
            new Frame(SpriteDictionary.Tatltuae[136], transform.position + new Vector3(0, 0.55f), .07f),
            new Frame(SpriteDictionary.Tatltuae[138], transform.position + new Vector3(0, 0.55f), .07f),
            new Frame(SpriteDictionary.Tatltuae[140], transform.position + new Vector3(0, 0.55f), .07f),
            new Frame(SpriteDictionary.Tatltuae[142], transform.position + new Vector3(0, 0.55f), .1f),
            };
        }
    }
}

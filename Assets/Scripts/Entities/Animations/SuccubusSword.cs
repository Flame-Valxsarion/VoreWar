using UnityEngine;

namespace Assets.Scripts.Entities.Animations
{
    class SuccubusSword : AnimationBase
    {


        public void Start()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingOrder = 20000;
            frames = new Frame[]
            {
            new Frame(SpriteDictionary.Succubi[10], transform.position + new Vector3(0, 1), .03f),
            new Frame(SpriteDictionary.Succubi[11], transform.position + new Vector3(0, 1), .03f),
            new Frame(SpriteDictionary.Succubi[12], transform.position + new Vector3(0, 1), .03f),
            new Frame(SpriteDictionary.Succubi[13], transform.position + new Vector3(0, 1), .03f),
            new Frame(SpriteDictionary.Succubi[14], transform.position + new Vector3(0, 1), .03f),
            new Frame(SpriteDictionary.Succubi[15], transform.position + new Vector3(0, 1), .05f),
            new Frame(SpriteDictionary.Succubi[16], transform.position + new Vector3(0, 1), .05f),
            new Frame(SpriteDictionary.Succubi[17], transform.position + new Vector3(0, 1), .05f),
            new Frame(SpriteDictionary.Succubi[18], transform.position + new Vector3(0, 1), .05f),
            new Frame(SpriteDictionary.Succubi[19], transform.position + new Vector3(0, 1), .3f),
            new Frame(SpriteDictionary.Succubi[19], transform.position + new Vector3(0, 1), .01f),
            new Frame(SpriteDictionary.Succubi[19], transform.position, .1f),
            };
        }
    }
}

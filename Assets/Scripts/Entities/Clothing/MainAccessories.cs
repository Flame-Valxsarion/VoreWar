class AsuraMask : ClothingAccessory
{
    public AsuraMask()
    {
        clothing1 = new SpriteExtraInfo(19, null, null);
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.Asura[40];
        switch (actor.Unit.Race)
        {
            case Race.Imps:
            case Race.Goblins:
                clothing1.YOffset = -22 * .625f;
                break;
            case Race.Taurus:
                clothing1.YOffset = 12 * .625f;
                break;
            default:
                clothing1.YOffset = 0;
                break;
        }
        base.Configure(sprite, actor);
    }
}

class SantaHat : ClothingAccessory
{
    public SantaHat()
    {
        clothing1 = new SpriteExtraInfo(28, null, null);
        ReqWinterHoliday = true;
    }

    public override void Configure(CompleteSprite sprite, Actor_Unit actor)
    {
        clothing1.GetSprite = (s) => State.GameManager.SpriteDictionary.SantaHat;
        switch (actor.Unit.Race)
        {
            case Race.Imps:
            case Race.Goblins:
                clothing1.YOffset = -22 * .625f;
                break;
            case Race.Taurus:
                clothing1.YOffset = 12 * .625f;
                break;
            case Race.Frogs:
                clothing1.YOffset = 9 * .625f;
                break;
            case Race.Bees:
                clothing1.YOffset = 26 * .625f;
                break;
            case Race.Merfolk:
                clothing1.YOffset = 14 * .625f;
                break;
            case Race.Kangaroos:
                clothing1.YOffset = -3 * .625f;
                break;
            case Race.Sergal:
                clothing1.YOffset = 26 * .625f;
                break;
            case Race.Bears:
                clothing1.YOffset = 12 * .625f;
                break;
            default:
                clothing1.YOffset = 0;
                break;
        }
        base.Configure(sprite, actor);
    }
}
static class MainAccessories
{
    internal static AsuraMask AsuraMask = new AsuraMask();
    internal static SantaHat SantaHat = new SantaHat();

}


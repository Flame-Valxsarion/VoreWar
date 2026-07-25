using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Taraluxia : BlankSlate
{
    bool facingFront = true;
    bool SelAlive = false;
    bool SelDigesting = false;
    bool SelGurgled = false;
    internal DisplayState displayState = DisplayState.Normal;

    internal enum DisplayState
    {
        Normal,
        TailStrike,
    }

    RaceFrameList TailStrikeEffects = new RaceFrameList(new int[6] { 0, 1, 2, 3, 4, 5 }, new float[6] { .1f, .1f, .1f, .07f, .07f, .07f });
    RaceFrameList TaraSwallowFront = new RaceFrameList(new int[10] { 6, 99, 1, 100, 101, 102, 2, 103, 104, 1 }, new float[10] { .25f, .25f, .25f, .1f, .1f, .1f, .1f, .1f, .1f, .60f });
    RaceFrameList TaraOralVore = new RaceFrameList(new int[3] { 0, 3, 4 }, new float[3] { .05f, .05f, 1.34f });
    RaceFrameList TaraOralVoreGlow = new RaceFrameList(new int[3] { 0, 3, 5 }, new float[3] { .05f, .05f, 1.34f });
    RaceFrameList TaraLickChops = new RaceFrameList(new int[7] { 100, 101, 102, 2, 103, 104, 1 }, new float[7] { .1f, .1f, .1f, .1f, .1f, .1f, 1f });
    RaceFrameList TaraDigestSelFront = new RaceFrameList(new int[7] { 72, 71, 70, 69, 68, 67, 66 }, new float[7] { 1f, .95f, .9f, .85f, .8f, .75f, .7f, });
    RaceFrameList TaraDigestSelBack = new RaceFrameList(new int[7] { 98, 97, 96, 95, 94, 93, 92 }, new float[7] { 1f, .95f, .9f, .85f, .8f, .75f, .7f, });


    readonly Sprite[] Sprites = SpriteDictionary.Taraluxia;
    readonly Sprite[] Sprites2 = SpriteDictionary.TaraTailStrikes;


    public Taraluxia()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
        GentleAnimation = true;
        Body = new SpriteExtraInfo(4, BodySprite, WhiteColored); // Foreground Wings and Hind Legs
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored); // Background Wings and Hind Legs
        BodyAccent2 = new SpriteExtraInfo(7, BodyAccentSprite2, WhiteColored); // Tail
        BodyAccent3 = new SpriteExtraInfo(8, BodyAccentSprite3, WhiteColored); // TailStrike Effects
        BodyAccent4 = new SpriteExtraInfo(3, BodyAccentSprite4, WhiteColored); // Sel Belly Animation
        BodyAccent5 = new SpriteExtraInfo(-2, BodyAccentSprite5, WhiteColored); // Glowing Maw
        BodyAccent6 = new SpriteExtraInfo(6, BodyAccentSprite6, WhiteColored); // Front Arm
        Head = new SpriteExtraInfo(5, HeadSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, WhiteColored);

        HeadTypes = 0;
        BodySizes = 3;
        TailTypes = 2;
        BodyAccentTypes5 = 2;
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Taraluxia";
        unit.TailType = (0);
        unit.HeadType = (0);
    }

    internal void SetUpAnimations(Actor_Unit actor)
    {
        actor.AnimationController.frameLists = new AnimationController.FrameList[]
        {
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
            new AnimationController.FrameList(0, 0, false),
        };
    }

    internal override void RunFirst(Actor_Unit actor)
    {
        if (actor.Unit.TailType == 0)
            facingFront = true;
        else facingFront = false;

        if (actor.AnimationController.frameLists == null || actor.AnimationController.frameLists.Count() == 0) SetUpAnimations(actor);

        if (actor.AnimationController?.frameLists[0].currentlyActive ?? false)
            displayState = DisplayState.TailStrike;
        else displayState = DisplayState.Normal;
        base.RunFirst(actor);
    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
        if (facingFront)
        {
            AddOffset(Head, 20, 20 * 1f);
            AddOffset(Belly, 20, 20 * 1f);
            AddOffset(Body, 20, 20 * 1f);
            AddOffset(BodyAccent, 20, 20 * 1f);
            AddOffset(BodyAccent2, 20, 20 * 1f);
            AddOffset(BodyAccent3, 20, 20 * 1f);
            AddOffset(BodyAccent4, 20, 20 * 1f);
            AddOffset(BodyAccent6, 20, 20 * 1f);
        }
        else
        {
            AddOffset(Head, 0, 20 * 1f);
            AddOffset(Belly, 0, 20 * 1f);
            AddOffset(Body, 0, 20 * 1f);
            AddOffset(BodyAccent, 0, 20 * 1f);
            AddOffset(BodyAccent2, 0, 20 * 1f);
            AddOffset(BodyAccent3, 0, 20 * 1f);
            AddOffset(BodyAccent4, 0, 20 * 1f);
            AddOffset(BodyAccent6, 0, 20 * 1f);
        }
    }

    protected override Sprite BodyAccentSprite2(Actor_Unit actor) // Tail
    {
        if (facingFront)
        {
            int bellySize = actor.GetStomachSize(20, 3.0f);
            if (bellySize > 12)
            {
                switch (displayState)
                {
                    case DisplayState.Normal: return Sprites[35];
                    case DisplayState.TailStrike: return Sprites[37];
                    default: return null;
                }
            }
            else
            {
                switch (displayState)
                {
                    case DisplayState.Normal: return Sprites[34];
                    case DisplayState.TailStrike: return Sprites[36];
                    default: return null;
                }
            }
        }
        else
        {
            int bellySize = actor.GetStomachSize(20, 3.0f);
            if (bellySize > 12)
            {
                switch (displayState)
                {
                    case DisplayState.Normal: return Sprites[39];
                    case DisplayState.TailStrike: return Sprites[41];
                    default: return null;
                }
            }
            else
            {
                switch (displayState)
                {
                    case DisplayState.Normal: return Sprites[38];
                    case DisplayState.TailStrike: return Sprites[40];
                    default: return null;
                }
            }
        }
    }

    protected override Sprite BodyAccentSprite3(Actor_Unit actor) // TailStrike Effects
    {
        switch (displayState)
        {
            case DisplayState.TailStrike:
            {
                if (facingFront)
                {
                    int bellySize = actor.GetStomachSize(20, 3.0f);
                    if (bellySize > 12)
                    {
                        if (actor.AnimationController.frameLists[0].currentlyActive)
                        {
                            if (actor.AnimationController.frameLists[0].currentTime >= TailStrikeEffects.times[actor.AnimationController.frameLists[0].currentFrame])
                            {
                                actor.AnimationController.frameLists[0].currentFrame++;
                                actor.AnimationController.frameLists[0].currentTime = 0f;

                                if (actor.AnimationController.frameLists[0].currentFrame >= TailStrikeEffects.frames.Length)
                                {
                                    actor.AnimationController.frameLists[0].currentFrame = 0;
                                    actor.AnimationController.frameLists[0].currentTime = 0f;
                                    actor.AnimationController.frameLists[0].currentlyActive = false;
                                }
                            }
                        }
                        else
                            return null;
                        return Sprites2[6 + TailStrikeEffects.frames[actor.AnimationController.frameLists[0].currentFrame]];
                    }
                    else
                    {
                        if (actor.AnimationController.frameLists[0].currentlyActive)
                        {
                            if (actor.AnimationController.frameLists[0].currentTime >= TailStrikeEffects.times[actor.AnimationController.frameLists[0].currentFrame])
                            {
                                actor.AnimationController.frameLists[0].currentFrame++;
                                actor.AnimationController.frameLists[0].currentTime = 0f;

                                if (actor.AnimationController.frameLists[0].currentFrame >= TailStrikeEffects.frames.Length)
                                {
                                    actor.AnimationController.frameLists[0].currentFrame = 0;
                                    actor.AnimationController.frameLists[0].currentTime = 0f;
                                    actor.AnimationController.frameLists[0].currentlyActive = false;
                                }
                            }
                        }
                        else
                            return null;
                        return Sprites2[0 + TailStrikeEffects.frames[actor.AnimationController.frameLists[0].currentFrame]];
                    }
                }
                else
                {
                    int bellySize = actor.GetStomachSize(20, 3.0f);
                    if (bellySize > 12)
                    {
                        if (actor.AnimationController.frameLists[0].currentlyActive)
                        {
                            if (actor.AnimationController.frameLists[0].currentTime >= TailStrikeEffects.times[actor.AnimationController.frameLists[0].currentFrame])
                            {
                                actor.AnimationController.frameLists[0].currentFrame++;
                                actor.AnimationController.frameLists[0].currentTime = 0f;

                                if (actor.AnimationController.frameLists[0].currentFrame >= TailStrikeEffects.frames.Length)
                                {
                                    actor.AnimationController.frameLists[0].currentFrame = 0;
                                    actor.AnimationController.frameLists[0].currentTime = 0f;
                                    actor.AnimationController.frameLists[0].currentlyActive = false;
                                }
                            }
                        }
                        else
                            return null;
                        return Sprites2[18 + TailStrikeEffects.frames[actor.AnimationController.frameLists[0].currentFrame]];
                    }
                    else
                    {
                        if (actor.AnimationController.frameLists[0].currentlyActive)
                        {
                            if (actor.AnimationController.frameLists[0].currentTime >= TailStrikeEffects.times[actor.AnimationController.frameLists[0].currentFrame])
                            {
                                actor.AnimationController.frameLists[0].currentFrame++;
                                actor.AnimationController.frameLists[0].currentTime = 0f;

                                if (actor.AnimationController.frameLists[0].currentFrame >= TailStrikeEffects.frames.Length)
                                {
                                    actor.AnimationController.frameLists[0].currentFrame = 0;
                                    actor.AnimationController.frameLists[0].currentTime = 0f;
                                    actor.AnimationController.frameLists[0].currentlyActive = false;
                                }
                            }
                        }
                        else
                            return null;
                        return Sprites2[12 + TailStrikeEffects.frames[actor.AnimationController.frameLists[0].currentFrame]];
                    }
                }
            } 
            case DisplayState.Normal: 
                return null; 
            default: 
                return null;
        }
    }

    protected override Sprite BodyAccentSprite5(Actor_Unit actor) // Glowing Maw
    {
        //Just a placeholder for the customization to appear in the menus
        return null;
    }

    protected override Sprite BodyAccentSprite6(Actor_Unit actor) // Front Arm
    {
        if (facingFront)
        {
            BodyAccent6.layer = 5;
            if (actor.IsAttacking) return Sprites[31];
            return Sprites [30];
        }
        else 
        {
            BodyAccent6.layer = 1;
            if (actor.IsAttacking) return Sprites[33];
            return Sprites [32];
        }
    }

    protected override Sprite HeadSprite(Actor_Unit actor)
    {
        if (facingFront)
        {
            if (SelDigesting == true) return Sprites[1]; // Watching her fade away into dragon fat~
            if (SelGurgled == true)
            {
                actor.AnimationController.frameLists[4].currentlyActive = true;
                if (actor.AnimationController.frameLists[4].currentlyActive)
                {
                    if (actor.AnimationController.frameLists[4].currentTime >= TaraLickChops.times[actor.AnimationController.frameLists[4].currentFrame])
                    {
                        actor.AnimationController.frameLists[4].currentFrame++;
                        actor.AnimationController.frameLists[4].currentTime = 0f;

                        if (actor.AnimationController.frameLists[4].currentFrame >= TaraLickChops.frames.Length)
                        {
                            actor.AnimationController.frameLists[4].currentFrame = 0;
                            actor.AnimationController.frameLists[4].currentTime = 0f;
                            actor.AnimationController.frameLists[4].currentlyActive = false;
                            SelGurgled = false;
                            if (Config.BurpOnDigest && State.Rand.NextDouble() < Config.BurpFraction)
                            {
                                actor.SetBurpMode();
                                State.GameManager.SoundManager.PlayBurp(actor);
                            }
                        }
                    }
                }
                else
                    return null;
                return Sprites[TaraLickChops.frames[actor.AnimationController.frameLists[4].currentFrame]];
            }
            if (actor.IsAttacking) return Sprites[3];
            if (actor.IsOralVoring)
            {
                if (actor.Unit.BodyAccentType5 == 1) //Glowing Maw ON
                {
                    actor.AnimationController.frameLists[3].currentlyActive = true;
                    if (actor.AnimationController.frameLists[3].currentlyActive)
                    {
                        if (actor.AnimationController.frameLists[3].currentTime >= TaraOralVoreGlow.times[actor.AnimationController.frameLists[3].currentFrame])
                        {
                            actor.AnimationController.frameLists[3].currentFrame++;
                            actor.AnimationController.frameLists[3].currentTime = 0f;

                            if (actor.AnimationController.frameLists[3].currentFrame >= TaraOralVoreGlow.frames.Length)
                            {
                                actor.AnimationController.frameLists[3].currentFrame = 0;
                                actor.AnimationController.frameLists[3].currentTime = 0f;
                                actor.AnimationController.frameLists[3].currentlyActive = false;
                            }
                        }
                    }
                    else
                        return null;
                    return Sprites[TaraOralVoreGlow.frames[actor.AnimationController.frameLists[3].currentFrame]];
                }
                else //Glowing Maw OFF
                {
                    actor.AnimationController.frameLists[2].currentlyActive = true;
                    if (actor.AnimationController.frameLists[2].currentlyActive)
                    {
                        if (actor.AnimationController.frameLists[2].currentTime >= TaraOralVore.times[actor.AnimationController.frameLists[2].currentFrame])
                        {
                            actor.AnimationController.frameLists[2].currentFrame++;
                            actor.AnimationController.frameLists[2].currentTime = 0f;

                            if (actor.AnimationController.frameLists[2].currentFrame >= TaraOralVore.frames.Length)
                            {
                                actor.AnimationController.frameLists[2].currentFrame = 0;
                                actor.AnimationController.frameLists[2].currentTime = 0f;
                                actor.AnimationController.frameLists[2].currentlyActive = false;
                            }
                        }
                    }
                    else
                        return null;
                    return Sprites[TaraOralVore.frames[actor.AnimationController.frameLists[2].currentFrame]];
                }
            }

            if (actor.HasJustVored) //Swallow Animation
            {
                actor.AnimationController.frameLists[1].currentlyActive = true;
                if (actor.AnimationController.frameLists[1].currentlyActive)
                    {
                        if (actor.AnimationController.frameLists[1].currentTime >= TaraSwallowFront.times[actor.AnimationController.frameLists[1].currentFrame])
                        {
                            actor.AnimationController.frameLists[1].currentFrame++;
                            actor.AnimationController.frameLists[1].currentTime = 0f;

                            if (actor.AnimationController.frameLists[1].currentFrame >= TaraSwallowFront.frames.Length)
                            {
                                actor.AnimationController.frameLists[1].currentFrame = 0;
                                actor.AnimationController.frameLists[1].currentTime = 0f;
                                actor.AnimationController.frameLists[1].currentlyActive = false;
                            }
                        }
                    }
                    else
                        return null;
                    return Sprites[TaraSwallowFront.frames[actor.AnimationController.frameLists[1].currentFrame]];
            }
            //Front View Idle Pose
            return Sprites[0];
        }
        else // Back View
        {
            if (SelDigesting == true) return Sprites[8]; // Melt into me, Sel~
            if (SelGurgled == true) // All mine...
            {
                if (Config.BurpOnDigest && State.Rand.NextDouble() < Config.BurpFraction)
                {
                    actor.SetBurpMode();
                    State.GameManager.SoundManager.PlayBurp(actor);
                    SelGurgled = false;
                }
                return Sprites[9]; // Placeholder until animation is finished, if that happens.
            }
            if (actor.IsAttacking) return Sprites[10];
            if (actor.IsOralVoring)
            {
                if (actor.Unit.BodyAccentType5 == 1) return Sprites[12]; // Glowing Maw ON
                else return Sprites[11]; // Glowing Maw OFF
            }

            if (actor.HasJustVored) return Sprites[13]; //Swallow
            else return Sprites[7]; // Back View Idle Pose
        }
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Foreground Wings and Hind Legs
    {
        int bellySize = actor.GetStomachSize(20, 3.0f);
        if (bellySize > 12)
        {
            if (facingFront)
            {
                return Sprites[17 + actor.Unit.BodySize];
            }
            else return Sprites[23 + actor.Unit.BodySize];
        }
        else
        {
            if (facingFront)
            {
                return Sprites[14 + actor.Unit.BodySize];
            }
            else return Sprites[20 + actor.Unit.BodySize];
        }
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Background Wings and Hind Legs
    {
        if (facingFront)
        {
            int bellySize = actor.GetStomachSize(20, 3.0f);
            if (bellySize > 12) return Sprites[27];
            else return Sprites[26];
        }
        else return Sprites[28];
    }

    protected override Sprite BodyAccentSprite4(Actor_Unit actor) // Selicia Digest Animation
    {
        if (facingFront) //Front view
        {
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false)// Oh no, death!
            {
                if (SelAlive == true)
                {
                    SelDigesting = true;
                    actor.AnimationController.frameLists[5].currentlyActive = true;
                    if (actor.AnimationController.frameLists[5].currentlyActive)
                        {
                            if (actor.AnimationController.frameLists[5].currentTime >= TaraDigestSelFront.times[actor.AnimationController.frameLists[5].currentFrame])
                            {
                                actor.AnimationController.frameLists[5].currentFrame++;
                                actor.AnimationController.frameLists[5].currentTime = 0f;

                                if (actor.AnimationController.frameLists[5].currentFrame >= TaraDigestSelFront.frames.Length)
                                {
                                    actor.AnimationController.frameLists[5].currentFrame = 0;
                                    actor.AnimationController.frameLists[5].currentTime = 0f;
                                    actor.AnimationController.frameLists[5].currentlyActive = false;
                                    SelDigesting = false;
                                    SelGurgled = true;
                                    SelAlive = false;
                                }
                            }
                        }
                        else
                            return null;
                        return Sprites[TaraDigestSelFront.frames[actor.AnimationController.frameLists[5].currentFrame]];
                }
                else return null;
            }
            else return null;
        }
        else // Back view
        {
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false)// Oh no, death!
            {
                if (SelAlive == true)
                {
                    SelDigesting = true;
                    actor.AnimationController.frameLists[6].currentlyActive = true;
                    if (actor.AnimationController.frameLists[6].currentlyActive)
                        {
                            if (actor.AnimationController.frameLists[6].currentTime >= TaraDigestSelBack.times[actor.AnimationController.frameLists[6].currentFrame])
                            {
                                actor.AnimationController.frameLists[6].currentFrame++;
                                actor.AnimationController.frameLists[6].currentTime = 0f;

                                if (actor.AnimationController.frameLists[6].currentFrame >= TaraDigestSelBack.frames.Length)
                                {
                                    actor.AnimationController.frameLists[6].currentFrame = 0;
                                    actor.AnimationController.frameLists[6].currentTime = 0f;
                                    actor.AnimationController.frameLists[6].currentlyActive = false;
                                    SelDigesting = false;
                                    SelGurgled = true;
                                    SelAlive = false;
                                }
                            }
                        }
                        else
                            return null;
                        return Sprites[TaraDigestSelBack.frames[actor.AnimationController.frameLists[6].currentFrame]];
                }
                else return null;
            }
            else return null;
        }
    }

    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly) // O R B
    {
        if (facingFront) // Front View
        {
            int bellySize = actor.GetStomachSize(20, 3.0f);
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false) //Healthy derg
            {
                SelAlive = true;
                return Sprites[72];
            }
            if (bellySize > 19) 
            {
                bellySize = 19;
                return Sprites[66];
            }
            else return Sprites[47 + bellySize];
        }
        else // Back View
        {
            int bellySize = actor.GetStomachSize(20, 3.0f);
            if (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false) //Healthy derg
            {
                SelAlive = true;
                return Sprites[98];
            }
            if (bellySize > 19) 
            {
                bellySize = 19;
                return Sprites[92];
            }
            else return Sprites[73 + bellySize];
        }
    }

}
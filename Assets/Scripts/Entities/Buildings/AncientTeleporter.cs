using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AncientTeleporter
{

    static internal List<AncientTeleporter> availibleTeleporters;

    [OdinSerialize]
    internal Vec2i Position;

    static int TurnRefreshed;

    public AncientTeleporter(Vec2i position)
    {
        Position = position;
    }

}


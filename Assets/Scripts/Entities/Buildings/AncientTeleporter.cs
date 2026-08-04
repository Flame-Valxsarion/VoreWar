using OdinSerializer;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AncientTeleporter
{

    static internal List<AncientTeleporter> availableTeleporters;

    [OdinSerialize]
    internal Vec2i Position;

    static int TurnRefreshed;

    public AncientTeleporter(Vec2i position)
    {
        Position = position;
    }

}


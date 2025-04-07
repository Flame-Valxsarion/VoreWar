using Boo.Lang;
using System;
using System.Collections.Generic;
using System.Linq;

public static class AIBuildingUtilityFunctions
{
    public static class WorkCampUtility 
    { 
        public static int GetRequiredGold(Dictionary<ConstructionResourceType, int> incoming)
        {
            int cost = 0;
            foreach (var item in incoming)
            {
                cost += Config.BuildConfig.WorkCampItemPrice.ResourceCountFromType(item.Key) * item.Value;
            }
            return cost;
        }
        public static bool PurchaseResource(ConstructionResourceType resource, WorkCamp camp)
        {
            if (camp.inStockItems.ResourceCountFromType(resource) > 0)
            {
                camp.inStockItems.SpendResource(resource, 1);
                return true;
            }
            return false;
        }
    }
}

using BlueMile.Coc.Data;
using BlueMile.Coc.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlueMile.Coc.Mobile.Services
{
    public static class RequirementValidationService
    {
        public static async Task<string> GetRequiredItems(Guid boatId)
        {
            ISqlDataService dataService = new SqlDataService();
            var boat = await dataService.GetBoatById(boatId).ConfigureAwait(false);
            var requiredItems = await GetRequiredCategoryList(boat.CategoryId).ConfigureAwait(false);
            var boatItems = await dataService.GetItemsByBoatId(boat.Id).ConfigureAwait(false);
            
            return $"You need the following required items for {boat.Name}\n{await ValidateItems(requiredItems, boatItems).ConfigureAwait(false)}";
        }

        private static Task<string> ValidateItems(List<ItemRquirementModel> requiredItems, List<RequiredItemModel> boatItems)
        {
            TaskCompletionSource<string> completionSource = new TaskCompletionSource<string>();
            for (int i = 0; i < requiredItems.Count; i++)
            {
                var AmountHas = boatItems.Where(x => x.ItemTypeId == requiredItems[i].ItemType).ToList();
                requiredItems[i].RequirementFullfilled = requiredItems[i].Quantity <= AmountHas.Count;
            }

            var itemsResult = String.Empty;

            foreach (var item in requiredItems)
            {
                itemsResult += $"{GetDescription(item.ItemType)} x {item.Quantity}: " + (item.RequirementFullfilled ? "Yes" : "No") + "\n";
            }
            completionSource.SetResult(itemsResult);

            return completionSource.Task;
        }

        public static Task<List<ItemRquirementModel>> GetRequiredCategoryList(CategoryStaticEntity category)
        {
            TaskCompletionSource<List<ItemRquirementModel>> completionSource = new TaskCompletionSource<List<ItemRquirementModel>>();
            var requirements = new List<ItemRquirementModel>();
            switch (category)
            {
                case (CategoryStaticEntity.C):
                    requirements = BuildCategoryCList();
                    break;
                case (CategoryStaticEntity.D):
                    requirements = BuildCategoryDList();
                    break;
                case (CategoryStaticEntity.E):
                    requirements = BuildCategoryEList();
                    break;
                case (CategoryStaticEntity.Z):
                    requirements = BuildCategoryZList();
                    break;
            }

            completionSource.SetResult(requirements);

            return completionSource.Task;
        }

        private static List<ItemRquirementModel> BuildCategoryCList()
        {
            var items = new List<ItemRquirementModel>();
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.MagneticCompass,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SmokeFlare,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.TowRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.Anchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.AnchorRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.IdSheet,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FirstAidKit,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FogHorn,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.RadarReflector,
                Quantity = 1
            });

            return items;
        }

        private static List<ItemRquirementModel> BuildCategoryDList()
        {
            var items = new List<ItemRquirementModel>();
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.MagneticCompass,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SmokeFlare,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.TowRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.Anchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.AnchorRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.IdSheet,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FirstAidKit,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FogHorn,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.RadarReflector,
                Quantity = 1
            });

            return items;
        }
        
        private static List<ItemRquirementModel> BuildCategoryEList()
        {
            var items = new List<ItemRquirementModel>();
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.MagneticCompass,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SmokeFlare,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.TowRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.Anchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.AnchorRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.IdSheet,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FirstAidKit,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FogHorn,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.RadarReflector,
                Quantity = 1
            });

            return items;
        }

        private static List<ItemRquirementModel> BuildCategoryZList()
        {
            var items = new List<ItemRquirementModel>();
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.MagneticCompass,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SmokeFlare,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.TowRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.Anchor,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.AnchorRope,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.IdSheet,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FirstAidKit,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.FogHorn,
                Quantity = 1
            });
            items.Add(new ItemRquirementModel()
            {
                ItemType = ItemTypeStaticEntity.RadarReflector,
                Quantity = 1
            });

            return items;
        }

        public static string GetDescription(ItemTypeStaticEntity x)
        {
            Type type = x.GetType();
            MemberInfo[] memInfo = type.GetMember(x.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return x.ToString();
        }
    }
}

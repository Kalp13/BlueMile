using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlueMile.Certification.Mobile.Services.InternalServices
{
    public static class RequirementValidationService
    {
        public static async Task<string> GetRequiredItems(Guid boatId)
        {
            IDataService dataService = new DataService();
            var boat = await dataService.FindBoatBySystemIdAsync(boatId).ConfigureAwait(false);
            var requiredItems = await GetRequiredCategoryList((BoatCategoryEnum)boat.BoatCategoryId).ConfigureAwait(false);
            var boatItems = await dataService.FindItemsByBoatIdAsync(boat.Id).ConfigureAwait(false);

            return $"You need the following required items for {boat.Name}\n{await ValidateItems(requiredItems, boatItems).ConfigureAwait(false)}";
        }

        private static Task<string> ValidateItems(List<RequiredItemModel> requiredItems, List<ItemMobileModel> boatItems)
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
                itemsResult += $"{GetDescription((ItemTypeEnum)item.ItemType)} x {item.Quantity}: " + (item.RequirementFullfilled ? "Yes" : "No") + "\n";
            }
            completionSource.SetResult(itemsResult);

            return completionSource.Task;
        }

        public static Task<List<RequiredItemModel>> GetRequiredCategoryList(BoatCategoryEnum category)
        {
            TaskCompletionSource<List<RequiredItemModel>> completionSource = new TaskCompletionSource<List<RequiredItemModel>>();
            var requirements = new List<RequiredItemModel>();
            switch (category)
            {
                case (BoatCategoryEnum.C):
                    requirements = BuildCategoryCList();
                    break;
                case (BoatCategoryEnum.D):
                    requirements = BuildCategoryDList();
                    break;
                case (BoatCategoryEnum.E):
                    requirements = BuildCategoryEList();
                    break;
                case (BoatCategoryEnum.Z):
                    requirements = BuildCategoryZList();
                    break;
            }

            completionSource.SetResult(requirements);

            return completionSource.Task;
        }

        private static List<RequiredItemModel> BuildCategoryCList()
        {
            var items = new List<RequiredItemModel>();
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.MagneticCompass,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SmokeFlare,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.TowRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.Anchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.AnchorRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.IdSheet,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FirstAidKit,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FogHorn,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.RadarReflector,
                Quantity = 1
            });

            return items;
        }

        private static List<RequiredItemModel> BuildCategoryDList()
        {
            var items = new List<RequiredItemModel>();
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.MagneticCompass,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SmokeFlare,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.TowRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.Anchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.AnchorRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.IdSheet,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FirstAidKit,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FogHorn,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.RadarReflector,
                Quantity = 1
            });

            return items;
        }

        private static List<RequiredItemModel> BuildCategoryEList()
        {
            var items = new List<RequiredItemModel>();
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.MagneticCompass,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SmokeFlare,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.TowRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.Anchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.AnchorRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.IdSheet,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FirstAidKit,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FogHorn,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.RadarReflector,
                Quantity = 1
            });

            return items;
        }

        private static List<RequiredItemModel> BuildCategoryZList()
        {
            var items = new List<RequiredItemModel>();
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.MagneticCompass,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.HandHeldFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.ParachuteFlare,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SmokeFlare,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.FireExtinguisher,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.DrogueAnchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.TowRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.Anchor,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.AnchorRope,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)ItemTypeEnum.SpaceBlanket,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)(long)ItemTypeEnum.IdSheet,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)(long)ItemTypeEnum.FirstAidKit,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)(long)ItemTypeEnum.OarOrPaddle,
                Quantity = 2
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)(long)ItemTypeEnum.FogHorn,
                Quantity = 1
            });
            items.Add(new RequiredItemModel()
            {
                ItemType = (long)(long)ItemTypeEnum.RadarReflector,
                Quantity = 1
            });

            return items;
        }

        public static string GetDescription(ItemTypeEnum x)
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

using BlueMile.Certification.Mobile.Data.Static;
using BlueMile.Certification.Mobile.Models;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace BlueMile.Certification.Mobile.Converters
{
    public class ItemToColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var item = (ItemMobileModel)value;

                if (item.ItemTypeId == (int)ItemTypeEnum.Anchor ||
                    item.ItemTypeId == (int)ItemTypeEnum.AnchorRope ||
                    item.ItemTypeId == (int)ItemTypeEnum.CapsizeBottleWith2mLaneyard ||
                    item.ItemTypeId == (int)ItemTypeEnum.CodeFlag ||
                    item.ItemTypeId == (int)ItemTypeEnum.DrogueAnchor ||
                    item.ItemTypeId == (int)ItemTypeEnum.FittedGrabline ||
                    item.ItemTypeId == (int)ItemTypeEnum.FogHorn ||
                    item.ItemTypeId == (int)ItemTypeEnum.LifeJacket ||
                    item.ItemTypeId == (int)ItemTypeEnum.MagneticCompass ||
                    item.ItemTypeId == (int)ItemTypeEnum.OarOrPaddle ||
                    item.ItemTypeId == (int)ItemTypeEnum.RadarReflector ||
                    item.ItemTypeId == (int)ItemTypeEnum.SpaceBlanket ||
                    item.ItemTypeId == (int)ItemTypeEnum.TowRope ||
                    item.ItemTypeId == (int)ItemTypeEnum.VhfRadio ||
                    item.ItemTypeId == (int)ItemTypeEnum.WaterproofTorch)
                {
                    return Color.White;
                }
                else
                {
                    var underSixM = DateTime.Compare(DateTime.Today.AddMonths(6), item.ExpiryDate);
                    var betweenSixAndTwelve = DateTime.Compare(DateTime.Today.AddMonths(12), item.ExpiryDate);
                    if (underSixM >= 0)
                    {
                        return Color.FromHex("FF4D4D");
                    }
                    else if (underSixM < 0 && betweenSixAndTwelve >= 0)
                    {
                        return Color.FromHex("FFBB4D");
                    }
                    else
                    {
                        return Color.White;
                    }
                }
            }
            return Color.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

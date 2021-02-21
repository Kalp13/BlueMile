using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlueMile.Certification.Data.Static
{
    public enum ItemTypeEnum
    {
        [Description("Hand Held Flare")]
        HandHeldFlare = 1,

        [Description("Life Jacket")]
        LifeJacket,

        [Description("Floating Smoke Flare")]
        SmokeFlare,

        [Description("Anchor With Chain")]
        Anchor,

        [Description("100m Anchor Rope")]
        AnchorRope,

        [Description("Drogue Anchor")]
        DrogueAnchor,

        [Description("Capsize bottle c/w 2m lanyard")]
        CapsizeBottleWith2mLaneyard,

        [Description("Parachute Flare")]
        ParachuteFlare,

        [Description("Waterproof Torch")]
        WaterproofTorch,

        [Description("Space Blanket")]
        SpaceBlanket,

        [Description("Orange/Yellow ID Sheet")]
        IdSheet,

        [Description("Hand-held Spotlight")]
        HandheldSpotlight,

        [Description("First Aid Kit")]
        FirstAidKit,

        [Description("Radar Reflector")]
        RadarReflector,

        [Description("VHF Radio")]
        VhfRadio,

        [Description("Steering Magnetic Compass")]
        MagneticCompass,

        [Description("Fire Extinguisher")]
        FireExtinguisher,

        [Description("OarOrPaddle")]
        OarOrPaddle,

        [Description("Fitted Grab-line")]
        FittedGrabline,

        [Description("Code Flag")]
        CodeFlag,

        [Description("Fog Horn")]
        FogHorn,

        [Description("Tow Rope")]
        TowRope
    }
}

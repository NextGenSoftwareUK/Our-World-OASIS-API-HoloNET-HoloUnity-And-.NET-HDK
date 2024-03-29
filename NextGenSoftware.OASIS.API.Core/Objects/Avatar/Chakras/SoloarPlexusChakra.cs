﻿using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Helpers;

namespace NextGenSoftware.OASIS.API.Core.Objects
{
    public class SoloarPlexusChakra : Chakra
    {
        public SoloarPlexusChakra()
        {
            Type = new EnumValue<ChakraType>(ChakraType.SolarPlexus);
        }
    }
}
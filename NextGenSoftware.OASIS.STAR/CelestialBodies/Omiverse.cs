﻿using System.Collections.Generic;
using NextGenSoftware.OASIS.API.Core.Interfaces.STAR;

namespace NextGenSoftware.OASIS.STAR.CelestialBodies
{
    public class Omiverse : IOmiverse
    {
        public IGreatGrandSuperStar GreatSuperStar { get; set; }
        public List<IUniverse> Universes { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using NextGenSoftware.OASIS.API.Core.Holons;
using NextGenSoftware.OASIS.API.Core.Interfaces;

namespace NextGenSoftware.OASIS.API.Core.Helpers
{
    public static class Mapper
    {
        public static IEnumerable<Holon> Convert(IEnumerable<IHolon> sourceHolons)
        {
            if (sourceHolons != null)
            {
                List<Holon> targetList = new List<Holon>();

                foreach (IHolon sourceHolon in sourceHolons)
                    targetList.Add((Holon)sourceHolon);

                return targetList;
            }
            else
                return null;
        }

        public static IEnumerable<IHolon> Convert(IEnumerable<Holon> sourceHolons)
        {
            if (sourceHolons != null)
            {
                List<IHolon> targetList = new List<IHolon>();

                foreach (IHolon sourceHolon in sourceHolons)
                    targetList.Add(sourceHolon);

                return targetList;
            }
            else
                return null;
        }

        public static IHolon MapBaseHolonProperties(IHolon sourceHolon, IHolon targetHolon, bool mapCelestialProperties = true)
        {
            if (sourceHolon != null && targetHolon != null)
            {
                targetHolon.Id = sourceHolon.Id;
                targetHolon.ProviderUniqueStorageKey = sourceHolon.ProviderUniqueStorageKey;
                targetHolon.Name = sourceHolon.Name;
                targetHolon.Description = sourceHolon.Description;
                targetHolon.HolonType = sourceHolon.HolonType;

                if (mapCelestialProperties)
                {
                    targetHolon.ParentGreatGrandSuperStar = sourceHolon.ParentGreatGrandSuperStar;
                    targetHolon.ParentGreatGrandSuperStarId = sourceHolon.ParentGreatGrandSuperStarId;
                    targetHolon.ParentGrandSuperStar = sourceHolon.ParentGrandSuperStar;
                    targetHolon.ParentGrandSuperStarId = sourceHolon.ParentGrandSuperStarId;
                    targetHolon.ParentSuperStar = sourceHolon.ParentSuperStar;
                    targetHolon.ParentSuperStarId = sourceHolon.ParentSuperStarId;
                    targetHolon.ParentStar = sourceHolon.ParentStar;
                    targetHolon.ParentStarId = sourceHolon.ParentStarId;
                    targetHolon.ParentPlanet = sourceHolon.ParentPlanet;
                    targetHolon.ParentPlanetId = sourceHolon.ParentPlanetId;
                    targetHolon.ParentMoon = sourceHolon.ParentMoon;
                    targetHolon.ParentMoonId = sourceHolon.ParentMoonId;
                    targetHolon.ParentCelestialSpaceId = sourceHolon.ParentCelestialSpaceId;
                    targetHolon.ParentCelestialBodyId = sourceHolon.ParentCelestialBodyId;
                    targetHolon.ParentZome = sourceHolon.ParentZome;
                    targetHolon.ParentZomeId = sourceHolon.ParentZomeId;
                    targetHolon.ParentHolon = sourceHolon.ParentHolon;
                    targetHolon.ParentHolonId = sourceHolon.ParentHolonId;
                    targetHolon.ParentOmniverse = sourceHolon.ParentOmniverse;
                    targetHolon.ParentOmniverseId = sourceHolon.ParentOmniverseId;
                    targetHolon.ParentMultiverse = sourceHolon.ParentMultiverse;
                    targetHolon.ParentMultiverseId = sourceHolon.ParentMultiverseId;
                    targetHolon.ParentDimension = sourceHolon.ParentDimension;
                    targetHolon.ParentDimensionId = sourceHolon.ParentDimensionId;
                    targetHolon.ParentUniverse = sourceHolon.ParentUniverse;
                    targetHolon.ParentUniverseId = sourceHolon.ParentUniverseId;
                    targetHolon.ParentGalaxyCluster = sourceHolon.ParentGalaxyCluster;
                    targetHolon.ParentGalaxyClusterId = sourceHolon.ParentGalaxyClusterId;
                    targetHolon.ParentGalaxy = sourceHolon.ParentGalaxy;
                    targetHolon.ParentGalaxyId = sourceHolon.ParentGalaxyId;
                    targetHolon.ParentSolarSystem = sourceHolon.ParentSolarSystem;
                    targetHolon.ParentSolarSystemId = sourceHolon.ParentSolarSystemId;
                }

                targetHolon.Children = sourceHolon.Children;
                targetHolon.Nodes = sourceHolon.Nodes;
                //targetHolon.CelestialBodyCore.Id = sourceHolon.Id; //TODO: Dont think need targetHolon now?
                //targetHolon.CelestialBodyCore.ProviderUniqueStorageKey = sourceHolon.ProviderUniqueStorageKey; //TODO: Dont think need targetHolon now?
                targetHolon.CreatedByAvatar = sourceHolon.CreatedByAvatar;
                targetHolon.CreatedByAvatarId = sourceHolon.CreatedByAvatarId;
                targetHolon.CreatedDate = sourceHolon.CreatedDate;
                targetHolon.ModifiedByAvatar = sourceHolon.ModifiedByAvatar;
                targetHolon.ModifiedByAvatarId = sourceHolon.ModifiedByAvatarId;
                targetHolon.ModifiedDate = sourceHolon.ModifiedDate;
                targetHolon.DeletedByAvatar = sourceHolon.DeletedByAvatar;
                targetHolon.DeletedByAvatarId = sourceHolon.DeletedByAvatarId;
                targetHolon.DeletedDate = sourceHolon.DeletedDate;
                targetHolon.Version = sourceHolon.Version;
                targetHolon.IsActive = sourceHolon.IsActive;
                targetHolon.IsChanged = sourceHolon.IsChanged;
                targetHolon.IsNewHolon = sourceHolon.IsNewHolon;
                targetHolon.MetaData = sourceHolon.MetaData;
                targetHolon.ProviderMetaData = sourceHolon.ProviderMetaData;
                targetHolon.Original = sourceHolon.Original;
            }

            return targetHolon;
        }

        public static IEnumerable<IHolon> MapBaseHolonProperties(IEnumerable<IHolon> sourceHolons, IEnumerable<IHolon> targetHolons)
        {
            if (sourceHolons != null && targetHolons != null)
            {
                List<IHolon> sourceList = sourceHolons.ToList();
                List<IHolon> targetList = targetHolons.ToList();

                for (int i = 0; i < sourceHolons.Count(); i++)
                    targetList[i] = (MapBaseHolonProperties(sourceList[i], targetList[i]));

                return targetList;
            }
            else
                return null;
        }

        public static IEnumerable<IHolon> MapBaseHolonProperties(IEnumerable<IHolon> sourceHolons)
        {
            return MapBaseHolonProperties(sourceHolons, new List<IHolon>());
        }

        //TODO: Need to get working properly.
        public static IEnumerable<IHolon> CastCollection(IEnumerable<IHolon> sourceHolons) //where T1 : IHolon//, T2 : IHolon
        {
            if (sourceHolons != null)
            {
                List<IHolon> sourceList = sourceHolons.ToList();
                List<IHolon> targetList = new List<IHolon>();

                for (int i = 0; i < sourceHolons.Count(); i++)
                    targetList[i] = sourceList[i];

                return targetList;
            }
            else
                return null;
        }

        public static IHolon MapParentCelestialBodyProperties(IHolon sourceCelestialBody, IHolon targetCelestialBody, bool onlyMapIfTargetIsNull = true)
        {
            if (sourceCelestialBody != null && targetCelestialBody != null)
            {
                if (targetCelestialBody.ParentGreatGrandSuperStar == null || (targetCelestialBody.ParentGreatGrandSuperStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGreatGrandSuperStar = sourceCelestialBody.ParentGreatGrandSuperStar;

                if (targetCelestialBody.ParentGreatGrandSuperStarId == Guid.Empty || (targetCelestialBody.ParentGreatGrandSuperStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGreatGrandSuperStarId = sourceCelestialBody.ParentGreatGrandSuperStarId;

                if (targetCelestialBody.ParentGrandSuperStar == null || (targetCelestialBody.ParentGrandSuperStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGrandSuperStar = sourceCelestialBody.ParentGrandSuperStar;

                if (targetCelestialBody.ParentGrandSuperStarId == Guid.Empty || (targetCelestialBody.ParentGrandSuperStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGrandSuperStarId = sourceCelestialBody.ParentGrandSuperStarId;

                if (targetCelestialBody.ParentSuperStar == null || (targetCelestialBody.ParentSuperStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSuperStar = sourceCelestialBody.ParentSuperStar;

                if (targetCelestialBody.ParentSuperStarId == Guid.Empty || (targetCelestialBody.ParentSuperStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSuperStarId = sourceCelestialBody.ParentSuperStarId;

                if (targetCelestialBody.ParentStar == null || (targetCelestialBody.ParentStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentStar = sourceCelestialBody.ParentStar;

                if (targetCelestialBody.ParentStarId == Guid.Empty || (targetCelestialBody.ParentStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentStarId = sourceCelestialBody.ParentStarId;

                if (targetCelestialBody.ParentPlanet == null || (targetCelestialBody.ParentPlanet != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentPlanet = sourceCelestialBody.ParentPlanet;

                if (targetCelestialBody.ParentPlanetId == Guid.Empty || (targetCelestialBody.ParentPlanetId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentPlanetId = sourceCelestialBody.ParentPlanetId;

                if (targetCelestialBody.ParentMoon == null || (targetCelestialBody.ParentMoon != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMoon = sourceCelestialBody.ParentMoon;

                if (targetCelestialBody.ParentMoonId == Guid.Empty || (targetCelestialBody.ParentMoonId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMoonId = sourceCelestialBody.ParentMoonId;

                if (targetCelestialBody.ParentCelestialSpace == null || (targetCelestialBody.ParentCelestialSpace != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialSpace = sourceCelestialBody.ParentCelestialSpace;

                if (targetCelestialBody.ParentCelestialSpaceId == Guid.Empty || (targetCelestialBody.ParentCelestialSpaceId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialSpaceId = sourceCelestialBody.ParentCelestialSpaceId;

                if (targetCelestialBody.ParentCelestialBody == null || (targetCelestialBody.ParentCelestialBody != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialBody = sourceCelestialBody.ParentCelestialBody;

                if (targetCelestialBody.ParentCelestialBodyId == Guid.Empty || (targetCelestialBody.ParentCelestialBodyId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialBodyId = sourceCelestialBody.ParentCelestialBodyId;

                if (targetCelestialBody.ParentZome == null || (targetCelestialBody.ParentZome != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentZome = sourceCelestialBody.ParentZome;

                if (targetCelestialBody.ParentZomeId == Guid.Empty || (targetCelestialBody.ParentZomeId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentZomeId = sourceCelestialBody.ParentZomeId;

                if (targetCelestialBody.ParentHolon == null || (targetCelestialBody.ParentHolon != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentHolon = sourceCelestialBody.ParentHolon;

                if (targetCelestialBody.ParentHolonId == Guid.Empty || (targetCelestialBody.ParentHolonId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentHolonId = sourceCelestialBody.ParentHolonId;

                if (targetCelestialBody.ParentOmniverse == null || (targetCelestialBody.ParentOmniverse != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentOmniverse = sourceCelestialBody.ParentOmniverse;

                if (targetCelestialBody.ParentOmniverseId == Guid.Empty || (targetCelestialBody.ParentOmniverseId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentOmniverseId = sourceCelestialBody.ParentOmniverseId;

                if (targetCelestialBody.ParentMultiverse == null || (targetCelestialBody.ParentMultiverse != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMultiverse = sourceCelestialBody.ParentMultiverse;

                if (targetCelestialBody.ParentMultiverseId == Guid.Empty || (targetCelestialBody.ParentMultiverseId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMultiverseId = sourceCelestialBody.ParentMultiverseId;

                if (targetCelestialBody.ParentDimension == null || (targetCelestialBody.ParentDimension != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentDimension = sourceCelestialBody.ParentDimension;

                if (targetCelestialBody.ParentDimensionId == Guid.Empty || (targetCelestialBody.ParentDimensionId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentDimensionId = sourceCelestialBody.ParentDimensionId;

                if (targetCelestialBody.ParentUniverse == null || (targetCelestialBody.ParentUniverse != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentUniverse = sourceCelestialBody.ParentUniverse;

                if (targetCelestialBody.ParentUniverseId == Guid.Empty || (targetCelestialBody.ParentUniverseId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentUniverseId = sourceCelestialBody.ParentUniverseId;

                if (targetCelestialBody.ParentGalaxyCluster == null || (targetCelestialBody.ParentGalaxyCluster != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxyCluster = sourceCelestialBody.ParentGalaxyCluster;

                if (targetCelestialBody.ParentGalaxyClusterId == Guid.Empty || (targetCelestialBody.ParentGalaxyClusterId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxyClusterId = sourceCelestialBody.ParentGalaxyClusterId;

                if (targetCelestialBody.ParentGalaxy == null || (targetCelestialBody.ParentGalaxy != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxy = sourceCelestialBody.ParentGalaxy;

                if (targetCelestialBody.ParentGalaxyId == Guid.Empty || (targetCelestialBody.ParentGalaxyId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxyId = sourceCelestialBody.ParentGalaxyId;

                if (targetCelestialBody.ParentSolarSystem == null || (targetCelestialBody.ParentSolarSystem != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSolarSystem = sourceCelestialBody.ParentSolarSystem;

                if (targetCelestialBody.ParentSolarSystemId == Guid.Empty || (targetCelestialBody.ParentSolarSystemId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSolarSystemId = sourceCelestialBody.ParentSolarSystemId;
            }

            return targetCelestialBody;
        }
    }

    public static class Mapper<T1, T2>
        where T1 : IHolon
        where T2 : IHolon, new()
    {

        public static T2 MapBaseHolonProperties(T1 sourceHolon)
        {
            return MapBaseHolonProperties(sourceHolon, new T2());
        }

        public static T2 MapBaseHolonProperties(T1 sourceHolon, T2 targetHolon, bool mapCelestialProperties = true)
        {
            if (sourceHolon != null && targetHolon != null)
            {
                targetHolon.Id = sourceHolon.Id;
                targetHolon.ProviderUniqueStorageKey = sourceHolon.ProviderUniqueStorageKey;
                targetHolon.Name = sourceHolon.Name;
                targetHolon.Description = sourceHolon.Description;
                targetHolon.HolonType = sourceHolon.HolonType;

                if (mapCelestialProperties)
                {
                    targetHolon.ParentGreatGrandSuperStar = sourceHolon.ParentGreatGrandSuperStar;
                    targetHolon.ParentGreatGrandSuperStarId = sourceHolon.ParentGreatGrandSuperStarId;
                    targetHolon.ParentGrandSuperStar = sourceHolon.ParentGrandSuperStar;
                    targetHolon.ParentGrandSuperStarId = sourceHolon.ParentGrandSuperStarId;
                    targetHolon.ParentSuperStar = sourceHolon.ParentSuperStar;
                    targetHolon.ParentSuperStarId = sourceHolon.ParentSuperStarId;
                    targetHolon.ParentStar = sourceHolon.ParentStar;
                    targetHolon.ParentStarId = sourceHolon.ParentStarId;
                    targetHolon.ParentPlanet = sourceHolon.ParentPlanet;
                    targetHolon.ParentPlanetId = sourceHolon.ParentPlanetId;
                    targetHolon.ParentMoon = sourceHolon.ParentMoon;
                    targetHolon.ParentMoonId = sourceHolon.ParentMoonId;
                    targetHolon.ParentCelestialSpaceId = sourceHolon.ParentCelestialSpaceId;
                    targetHolon.ParentCelestialBodyId = sourceHolon.ParentCelestialBodyId;
                    targetHolon.ParentZome = sourceHolon.ParentZome;
                    targetHolon.ParentZomeId = sourceHolon.ParentZomeId;
                    targetHolon.ParentHolon = sourceHolon.ParentHolon;
                    targetHolon.ParentHolonId = sourceHolon.ParentHolonId;
                    targetHolon.ParentOmniverse = sourceHolon.ParentOmniverse;
                    targetHolon.ParentOmniverseId = sourceHolon.ParentOmniverseId;
                    targetHolon.ParentMultiverse = sourceHolon.ParentMultiverse;
                    targetHolon.ParentMultiverseId = sourceHolon.ParentMultiverseId;
                    targetHolon.ParentDimension = sourceHolon.ParentDimension;
                    targetHolon.ParentDimensionId = sourceHolon.ParentDimensionId;
                    targetHolon.ParentUniverse = sourceHolon.ParentUniverse;
                    targetHolon.ParentUniverseId = sourceHolon.ParentUniverseId;
                    targetHolon.ParentGalaxyCluster = sourceHolon.ParentGalaxyCluster;
                    targetHolon.ParentGalaxyClusterId = sourceHolon.ParentGalaxyClusterId;
                    targetHolon.ParentGalaxy = sourceHolon.ParentGalaxy;
                    targetHolon.ParentGalaxyId = sourceHolon.ParentGalaxyId;
                    targetHolon.ParentSolarSystem = sourceHolon.ParentSolarSystem;
                    targetHolon.ParentSolarSystemId = sourceHolon.ParentSolarSystemId;
                }

                targetHolon.Children = sourceHolon.Children;
                targetHolon.Nodes = sourceHolon.Nodes;
                //targetHolon.CelestialBodyCore.Id = sourceHolon.Id; //TODO: Dont think need targetHolon now?
                //targetHolon.CelestialBodyCore.ProviderUniqueStorageKey = sourceHolon.ProviderUniqueStorageKey; //TODO: Dont think need targetHolon now?
                targetHolon.CreatedByAvatar = sourceHolon.CreatedByAvatar;
                targetHolon.CreatedByAvatarId = sourceHolon.CreatedByAvatarId;
                targetHolon.CreatedDate = sourceHolon.CreatedDate;
                targetHolon.ModifiedByAvatar = sourceHolon.ModifiedByAvatar;
                targetHolon.ModifiedByAvatarId = sourceHolon.ModifiedByAvatarId;
                targetHolon.ModifiedDate = sourceHolon.ModifiedDate;
                targetHolon.DeletedByAvatar = sourceHolon.DeletedByAvatar;
                targetHolon.DeletedByAvatarId = sourceHolon.DeletedByAvatarId;
                targetHolon.DeletedDate = sourceHolon.DeletedDate;
                targetHolon.Version = sourceHolon.Version;
                targetHolon.IsActive = sourceHolon.IsActive;
                targetHolon.IsChanged = sourceHolon.IsChanged;
                targetHolon.IsNewHolon = sourceHolon.IsNewHolon;
                targetHolon.MetaData = sourceHolon.MetaData;
                targetHolon.ProviderMetaData = sourceHolon.ProviderMetaData;
                targetHolon.Original = sourceHolon.Original;
            }

            return targetHolon;
        }
        public static IEnumerable<T2> MapBaseHolonProperties(IEnumerable<T1> sourceHolons)
        {
            if (sourceHolons != null)
            {
                List<T2> targetList = new List<T2>();

                foreach (T1 sourceHolon in sourceHolons)
                    targetList.Add(MapBaseHolonProperties(sourceHolon));

                return targetList;
            }
            else
                return null;
        }

        public static IEnumerable<IHolon> Convert(IEnumerable<T1> sourceHolons)
        {
            if (sourceHolons != null)
            {
                List<IHolon> targetList = new List<IHolon>();

                foreach (T1 sourceHolon in sourceHolons)
                    targetList.Add(sourceHolon);

                return targetList;
            }
            else
                return null;
        }

        public static T2 MapParentCelestialBodyProperties(T1 sourceHolon)
        {
            return MapParentCelestialBodyProperties(sourceHolon, new T2());
        }

        public static T2 MapParentCelestialBodyProperties(T1 sourceCelestialBody, T2 targetCelestialBody, bool onlyMapIfTargetIsNull = true)
        {
            if (sourceCelestialBody != null && targetCelestialBody != null)
            {
                if (targetCelestialBody.ParentGreatGrandSuperStar == null || (targetCelestialBody.ParentGreatGrandSuperStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGreatGrandSuperStar = sourceCelestialBody.ParentGreatGrandSuperStar;

                if (targetCelestialBody.ParentGreatGrandSuperStarId == Guid.Empty || (targetCelestialBody.ParentGreatGrandSuperStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGreatGrandSuperStarId = sourceCelestialBody.ParentGreatGrandSuperStarId;

                if (targetCelestialBody.ParentGrandSuperStar == null || (targetCelestialBody.ParentGrandSuperStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGrandSuperStar = sourceCelestialBody.ParentGrandSuperStar;

                if (targetCelestialBody.ParentGrandSuperStarId == Guid.Empty || (targetCelestialBody.ParentGrandSuperStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGrandSuperStarId = sourceCelestialBody.ParentGrandSuperStarId;

                if (targetCelestialBody.ParentSuperStar == null || (targetCelestialBody.ParentSuperStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSuperStar = sourceCelestialBody.ParentSuperStar;

                if (targetCelestialBody.ParentSuperStarId == Guid.Empty || (targetCelestialBody.ParentSuperStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSuperStarId = sourceCelestialBody.ParentSuperStarId;

                if (targetCelestialBody.ParentStar == null || (targetCelestialBody.ParentStar != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentStar = sourceCelestialBody.ParentStar;

                if (targetCelestialBody.ParentStarId == Guid.Empty || (targetCelestialBody.ParentStarId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentStarId = sourceCelestialBody.ParentStarId;

                if (targetCelestialBody.ParentPlanet == null || (targetCelestialBody.ParentPlanet != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentPlanet = sourceCelestialBody.ParentPlanet;

                if (targetCelestialBody.ParentPlanetId == Guid.Empty || (targetCelestialBody.ParentPlanetId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentPlanetId = sourceCelestialBody.ParentPlanetId;

                if (targetCelestialBody.ParentMoon == null || (targetCelestialBody.ParentMoon != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMoon = sourceCelestialBody.ParentMoon;

                if (targetCelestialBody.ParentMoonId == Guid.Empty || (targetCelestialBody.ParentMoonId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMoonId = sourceCelestialBody.ParentMoonId;

                if (targetCelestialBody.ParentCelestialSpace == null || (targetCelestialBody.ParentCelestialSpace != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialSpace = sourceCelestialBody.ParentCelestialSpace;

                if (targetCelestialBody.ParentCelestialSpaceId == Guid.Empty || (targetCelestialBody.ParentCelestialSpaceId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialSpaceId = sourceCelestialBody.ParentCelestialSpaceId;

                if (targetCelestialBody.ParentCelestialBody == null || (targetCelestialBody.ParentCelestialBody != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialBody = sourceCelestialBody.ParentCelestialBody;

                if (targetCelestialBody.ParentCelestialBodyId == Guid.Empty || (targetCelestialBody.ParentCelestialBodyId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentCelestialBodyId = sourceCelestialBody.ParentCelestialBodyId;

                if (targetCelestialBody.ParentZome == null || (targetCelestialBody.ParentZome != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentZome = sourceCelestialBody.ParentZome;

                if (targetCelestialBody.ParentZomeId == Guid.Empty || (targetCelestialBody.ParentZomeId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentZomeId = sourceCelestialBody.ParentZomeId;

                if (targetCelestialBody.ParentHolon == null || (targetCelestialBody.ParentHolon != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentHolon = sourceCelestialBody.ParentHolon;

                if (targetCelestialBody.ParentHolonId == Guid.Empty || (targetCelestialBody.ParentHolonId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentHolonId = sourceCelestialBody.ParentHolonId;

                if (targetCelestialBody.ParentOmniverse == null || (targetCelestialBody.ParentOmniverse != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentOmniverse = sourceCelestialBody.ParentOmniverse;

                if (targetCelestialBody.ParentOmniverseId == Guid.Empty || (targetCelestialBody.ParentOmniverseId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentOmniverseId = sourceCelestialBody.ParentOmniverseId;

                if (targetCelestialBody.ParentMultiverse == null || (targetCelestialBody.ParentMultiverse != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMultiverse = sourceCelestialBody.ParentMultiverse;

                if (targetCelestialBody.ParentMultiverseId == Guid.Empty || (targetCelestialBody.ParentMultiverseId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentMultiverseId = sourceCelestialBody.ParentMultiverseId;

                if (targetCelestialBody.ParentDimension == null || (targetCelestialBody.ParentDimension != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentDimension = sourceCelestialBody.ParentDimension;

                if (targetCelestialBody.ParentDimensionId == Guid.Empty || (targetCelestialBody.ParentDimensionId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentDimensionId = sourceCelestialBody.ParentDimensionId;

                if (targetCelestialBody.ParentUniverse == null || (targetCelestialBody.ParentUniverse != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentUniverse = sourceCelestialBody.ParentUniverse;

                if (targetCelestialBody.ParentUniverseId == Guid.Empty || (targetCelestialBody.ParentUniverseId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentUniverseId = sourceCelestialBody.ParentUniverseId;

                if (targetCelestialBody.ParentGalaxyCluster == null || (targetCelestialBody.ParentGalaxyCluster != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxyCluster = sourceCelestialBody.ParentGalaxyCluster;

                if (targetCelestialBody.ParentGalaxyClusterId == Guid.Empty || (targetCelestialBody.ParentGalaxyClusterId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxyClusterId = sourceCelestialBody.ParentGalaxyClusterId;

                if (targetCelestialBody.ParentGalaxy == null || (targetCelestialBody.ParentGalaxy != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxy = sourceCelestialBody.ParentGalaxy;

                if (targetCelestialBody.ParentGalaxyId == Guid.Empty || (targetCelestialBody.ParentGalaxyId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentGalaxyId = sourceCelestialBody.ParentGalaxyId;

                if (targetCelestialBody.ParentSolarSystem == null || (targetCelestialBody.ParentSolarSystem != null && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSolarSystem = sourceCelestialBody.ParentSolarSystem;

                if (targetCelestialBody.ParentSolarSystemId == Guid.Empty || (targetCelestialBody.ParentSolarSystemId != Guid.Empty && !onlyMapIfTargetIsNull))
                    targetCelestialBody.ParentSolarSystemId = sourceCelestialBody.ParentSolarSystemId;
            }

            return targetCelestialBody;
        }
        public static IEnumerable<T2> MapParentCelestialBodyProperties(IEnumerable<T1> sourceCelestialBodies)
        {
            if (sourceCelestialBodies != null)
            {
                List<T2> targetList = new List<T2>();

                foreach (T1 sourceCelestialBody in sourceCelestialBodies)
                    targetList.Add(MapParentCelestialBodyProperties(sourceCelestialBody));

                return targetList;
            }
            else
                return null;
        }
    }
}
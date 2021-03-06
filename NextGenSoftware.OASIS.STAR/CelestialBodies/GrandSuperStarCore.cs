﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Holons;
using NextGenSoftware.OASIS.API.Core.Helpers;
using NextGenSoftware.OASIS.API.Core.Interfaces;
using NextGenSoftware.OASIS.API.Core.Interfaces.STAR;
using NextGenSoftware.OASIS.STAR.CelestialBodies;
using NextGenSoftware.OASIS.STAR.CelestialSpace;

namespace NextGenSoftware.OASIS.STAR
{
    // At the centre of each Multiverse (creates dimensions, universes, GalaxyClusters, SolarSystems (outside of a Galaxy), Stars (outside of a Galaxy) & Planets (outside of a Galaxy)). Prime Creator
    public class GrandSuperStarCore : CelestialBodyCore, IGrandSuperStarCore
    {
        public IGrandSuperStar GrandSuperStar { get; set; }

        public GrandSuperStarCore(IGrandSuperStar grandSuperStar) : base()
        {
            GrandSuperStar = grandSuperStar;
        }

        public GrandSuperStarCore(IGrandSuperStar grandSuperStar, Dictionary<ProviderType, string> providerKey) : base(providerKey)
        {
            GrandSuperStar = grandSuperStar;
        }

        public GrandSuperStarCore(IGrandSuperStar grandSuperStar, Guid id) : base(id)
        {
            GrandSuperStar = grandSuperStar;
        }

        public async Task<OASISResult<IUniverse>> AddUniverseAsync(IUniverse universe)
        {
            return OASISResultHolonToHolonHelper<IHolon, IUniverse>.CopyResult(
                await AddHolonToCollectionAsync(GrandSuperStar, universe, (List<IHolon>)Mapper<IUniverse, Holon>.MapBaseHolonProperties(
                    GrandSuperStar.ParentMultiverse.Universes)), new OASISResult<IUniverse>());
        }

        public OASISResult<IUniverse> AddUniverse(IUniverse universe)
        {
            return AddUniverseAsync(universe).Result;
        }

        public async Task<OASISResult<IDimension>> AddDimensionToUniverseAsync(IUniverse universe, IDimension dimension)
        {
            return OASISResultHolonToHolonHelper<IHolon, IDimension>.CopyResult(
                await AddHolonToCollectionAsync(universe, dimension, (List<IHolon>)Mapper<IDimension, Holon>.MapBaseHolonProperties(
                    universe.Dimensions)), new OASISResult<IDimension>());
        }

        public OASISResult<IDimension> AddDimensionToUniverse(IUniverse universe, IDimension dimension)
        {
            return AddDimensionToUniverseAsync(universe, dimension).Result;
        }

        public async Task<OASISResult<IGalaxyCluster>> AddGalaxyClusterToDimensionAsync(IDimension dimension, IGalaxyCluster galaxyCluster)
        {
            return OASISResultHolonToHolonHelper<IHolon, IGalaxyCluster>.CopyResult(
               await AddHolonToCollectionAsync(dimension, galaxyCluster, (List<IHolon>)Mapper<IGalaxyCluster, Holon>.MapBaseHolonProperties(
                   dimension.GalaxyClusters)), new OASISResult<IGalaxyCluster>());
        }

        public OASISResult<IGalaxyCluster> AddGalaxyClusterToDimension(IDimension dimension, IGalaxyCluster galaxyCluster)
        {
            return AddGalaxyClusterToDimensionAsync(dimension, galaxyCluster).Result;
        }

        public async Task<OASISResult<ISolarSystem>> AddSolarSystemToDimensionAsync(IDimension dimension, ISolarSystem solarSystem)
        {
            return OASISResultHolonToHolonHelper<IHolon, ISolarSystem>.CopyResult(
                await AddHolonToCollectionAsync(dimension, solarSystem, (List<IHolon>)Mapper<ISolarSystem, Holon>.MapBaseHolonProperties(
                    dimension.SoloarSystems)), new OASISResult<ISolarSystem>());
        }

        public OASISResult<ISolarSystem> AddSolarSystemToDimension(IDimension dimension, ISolarSystem solarSystem)
        {
            return AddSolarSystemToDimensionAsync(dimension, solarSystem).Result;
        }

        public async Task<OASISResult<IStar>> AddStarToDimensionAsync(IDimension dimension, IStar star)
        {
            return OASISResultHolonToHolonHelper<IHolon, IStar>.CopyResult(
                await AddHolonToCollectionAsync(dimension, star, (List<IHolon>)Mapper<IStar, Holon>.MapBaseHolonProperties(
                    dimension.Stars)), new OASISResult<IStar>());
        }

        public OASISResult<IStar> AddStarToDimension(IDimension dimension, IStar star)
        {
            return AddStarToDimensionAsync(dimension, star).Result;
        }

        public async Task<OASISResult<IPlanet>> AddPlanetToDimensionAsync(IDimension dimension, IPlanet planet)
        {
            return OASISResultHolonToHolonHelper<IHolon, IPlanet>.CopyResult(
                await AddHolonToCollectionAsync(dimension, planet, (List<IHolon>)Mapper<IPlanet, Holon>.MapBaseHolonProperties(
                    dimension.Planets)), new OASISResult<IPlanet>());
        }

        public OASISResult<IPlanet> AddPlanetToDimension(IDimension dimension, IPlanet planet)
        {
            return AddPlanetToDimensionAsync(dimension, planet).Result;
        }

        public async Task<OASISResult<IEnumerable<IUniverse>>> GetAllUniversesForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IUniverse>> result = new OASISResult<IEnumerable<IUniverse>>();
            OASISResult<IEnumerable<IHolon>> holonResult = await GetHolonsAsync(GrandSuperStar.ParentMultiverse.Universes, HolonType.Universe, refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IHolon>, IEnumerable<IUniverse>>.CopyResult(holonResult, ref result);
            result.Result = Mapper<IHolon, Universe>.MapBaseHolonProperties(holonResult.Result);
            return result;
        }

        public OASISResult<IEnumerable<IUniverse>> GetAllUniversesForMultiverse(bool refresh = true)
        {
            return GetAllUniversesForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IDimension>>> GetAllDimensionsForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IDimension>> result = new OASISResult<IEnumerable<IDimension>>();
            OASISResult<IEnumerable<IUniverse>> universesResult = await GetAllUniversesForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IUniverse>, IEnumerable<IDimension>>.CopyResult(universesResult, ref result);

            if (!universesResult.IsError)
            {
                List<IDimension> dimensions = new List<IDimension>();

                foreach (IUniverse universe in universesResult.Result)
                    dimensions.AddRange(universe.Dimensions);

                result.Result = dimensions;
            }

            return result;
        }

        public OASISResult<IEnumerable<IDimension>> GetAllDimensionsForMultiverse(bool refresh = true)
        {
            return GetAllDimensionsForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IGalaxyCluster>>> GetAllGalaxyClustersForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IGalaxyCluster>> result = new OASISResult<IEnumerable<IGalaxyCluster>>();
            OASISResult<IEnumerable<IDimension>> dimensionsResult = await GetAllDimensionsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IDimension>, IEnumerable<IGalaxyCluster>>.CopyResult(dimensionsResult, ref result);

            if (!dimensionsResult.IsError)
            {
                List<IGalaxyCluster> clusters = new List<IGalaxyCluster>();

                foreach (IDimension dimension in dimensionsResult.Result)
                    clusters.AddRange(dimension.GalaxyClusters);

                result.Result = clusters;
            }

            return result;
        }

        public OASISResult<IEnumerable<IGalaxyCluster>> GetAllGalaxyClustersForMultiverse(bool refresh = true)
        {
            return GetAllGalaxyClustersForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IGalaxy>>> GetAllGalaxiesForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IGalaxy>> result = new OASISResult<IEnumerable<IGalaxy>>();
            OASISResult<IEnumerable<IGalaxyCluster>> galaxyClustersResult = await GetAllGalaxyClustersForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IGalaxyCluster>, IEnumerable<IGalaxy>>.CopyResult(galaxyClustersResult, ref result);

            if (!galaxyClustersResult.IsError)
            {
                List<IGalaxy> galaxies = new List<IGalaxy>();

                foreach (IGalaxyCluster cluster in galaxyClustersResult.Result)
                    galaxies.AddRange(cluster.Galaxies);

                result.Result = galaxies;
            }

            return result;
        }

        public OASISResult<IEnumerable<IGalaxy>> GetAllGalaxiesForMultiverse(bool refresh = true)
        {
            return GetAllGalaxiesForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<ISuperStar>>> GetAllSuperStarsForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<ISuperStar>> result = new OASISResult<IEnumerable<ISuperStar>>();
            OASISResult<IEnumerable<IGalaxy>> galaxiesResult = await GetAllGalaxiesForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IGalaxy>, IEnumerable<ISuperStar>>.CopyResult(galaxiesResult, ref result);

            if (!galaxiesResult.IsError)
            {
                List<ISuperStar> superstars = new List<ISuperStar>();

                foreach (IGalaxy galaxy in galaxiesResult.Result)
                    superstars.Add(galaxy.SuperStar);

                result.Result = superstars;
            }

            return result;
        }

        public OASISResult<IEnumerable<ISuperStar>> GetAllSuperStarsForMultiverse(bool refresh = true)
        {
            return GetAllSuperStarsForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<ISolarSystem>>> GetAllSolarSystemsOutSideOfGalaxiesForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<ISolarSystem>> result = new OASISResult<IEnumerable<ISolarSystem>>();
            OASISResult<IEnumerable<IGalaxyCluster>> galaxyClustersResult = await GetAllGalaxyClustersForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IGalaxyCluster>, IEnumerable<ISolarSystem>>.CopyResult(galaxyClustersResult, ref result);

            if (!galaxyClustersResult.IsError)
            {
                List<ISolarSystem> solarSystems = new List<ISolarSystem>();

                foreach (IGalaxyCluster cluster in galaxyClustersResult.Result)
                    solarSystems.AddRange(cluster.SoloarSystems);

                result.Result = solarSystems;
            }

            return result;
        }

        public OASISResult<IEnumerable<ISolarSystem>> GetAllSolarSystemsOutSideOfGalaxiesForMultiverse(bool refresh = true)
        {
            return GetAllSolarSystemsOutSideOfGalaxiesForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<ISolarSystem>>> GetAllSolarSystemsOutSideOfGalaxyClustersForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<ISolarSystem>> result = new OASISResult<IEnumerable<ISolarSystem>>();
            OASISResult<IEnumerable<IDimension>> dimensionsResult = await GetAllDimensionsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IDimension>, IEnumerable<ISolarSystem>>.CopyResult(dimensionsResult, ref result);

            if (!dimensionsResult.IsError)
            {
                List<ISolarSystem> solarSystems = new List<ISolarSystem>();

                foreach (IDimension dimension in dimensionsResult.Result)
                    solarSystems.AddRange(dimension.SoloarSystems);

                result.Result = solarSystems;
            }

            return result;
        }

        public OASISResult<IEnumerable<ISolarSystem>> GetAllSolarSystemsOutSideOfGalaxyClustersForMultiverse(bool refresh = true)
        {
            return GetAllSolarSystemsOutSideOfGalaxyClustersForMultiverseAsync(refresh).Result;
        }


        public async Task<OASISResult<IEnumerable<ISolarSystem>>> GetAllSolarSystemsForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<ISolarSystem>> result = new OASISResult<IEnumerable<ISolarSystem>>();
            OASISResult<IEnumerable<IGalaxy>> galaxiesResult = await GetAllGalaxiesForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IGalaxy>, IEnumerable<ISolarSystem>>.CopyResult(galaxiesResult, ref result);
            List<ISolarSystem> solarSystems = new List<ISolarSystem>();

            if (!galaxiesResult.IsError)
            {
                foreach (IGalaxy galaxy in galaxiesResult.Result)
                    solarSystems.AddRange(galaxy.SolarSystems);

                result.Result = solarSystems;
            }

            OASISResult<IEnumerable<ISolarSystem>> solarSystemsOutsideResult = await GetAllSolarSystemsOutSideOfGalaxyClustersForMultiverseAsync(refresh);

            if (!solarSystemsOutsideResult.IsError)
                solarSystems.AddRange(solarSystemsOutsideResult.Result);

            solarSystemsOutsideResult = await GetAllSolarSystemsOutSideOfGalaxiesForMultiverseAsync(refresh);

            if (!solarSystemsOutsideResult.IsError)
                solarSystems.AddRange(solarSystemsOutsideResult.Result);

            result.Result = solarSystems;
            return result;
        }

        public OASISResult<IEnumerable<ISolarSystem>> GetAllSolarSystemsForMultiverse(bool refresh = true)
        {
            return GetAllSolarSystemsForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IStar>>> GetAllStarsOutSideOfGalaxiesForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IStar>> result = new OASISResult<IEnumerable<IStar>>();
            OASISResult<IEnumerable<IGalaxyCluster>> galaxyClustersResult = await GetAllGalaxyClustersForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IGalaxyCluster>, IEnumerable<IStar>>.CopyResult(galaxyClustersResult, ref result);

            if (!galaxyClustersResult.IsError)
            {
                List<IStar> stars = new List<IStar>();

                foreach (IGalaxyCluster cluster in galaxyClustersResult.Result)
                    stars.AddRange(cluster.Stars);

                result.Result = stars;
            }

            return result;
        }

        public OASISResult<IEnumerable<IStar>> GetAllStarsOutSideOfGalaxiesForMultiverse(bool refresh = true)
        {
            return GetAllStarsOutSideOfGalaxiesForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IStar>>> GetAllStarsOutSideOfGalaxyClustersForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IStar>> result = new OASISResult<IEnumerable<IStar>>();
            OASISResult<IEnumerable<IDimension>> dimensionsResult = await GetAllDimensionsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IDimension>, IEnumerable<IStar>>.CopyResult(dimensionsResult, ref result);

            if (!dimensionsResult.IsError)
            {
                List<IStar> stars = new List<IStar>();

                foreach (IDimension dimension in dimensionsResult.Result)
                    stars.AddRange(dimension.Stars);

                result.Result = stars;
            }

            return result;
        }

        public OASISResult<IEnumerable<IStar>> GetAllStarsOutSideOfGalaxyClustersForMultiverse(bool refresh = true)
        {
            return GetAllStarsOutSideOfGalaxyClustersForMultiverseAsync(refresh).Result;
        }


        public async Task<OASISResult<IEnumerable<IStar>>> GetAllStarsForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IStar>> result = new OASISResult<IEnumerable<IStar>>();
            OASISResult<IEnumerable<ISuperStar>> superStarsResult = await GetAllSuperStarsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<ISuperStar>, IEnumerable<IStar>>.CopyResult(superStarsResult, ref result);
            List<IStar> stars = new List<IStar>();

            if (!superStarsResult.IsError)
            {
                foreach (ISuperStar superStar in superStarsResult.Result)
                {
                    OASISResult<IEnumerable<IStar>> starsResult = await ((ISuperStarCore)superStar.CelestialBodyCore).GetAllStarsForGalaxyAsync(refresh);

                    if (!starsResult.IsError)
                        stars.AddRange(starsResult.Result);
                }
            }

            OASISResult<IEnumerable<IStar>> starsOutsideResult = await GetAllStarsOutSideOfGalaxyClustersForMultiverseAsync(refresh);

            if (!starsOutsideResult.IsError)
                stars.AddRange(starsOutsideResult.Result);

            starsOutsideResult = await GetAllStarsOutSideOfGalaxiesForMultiverseAsync(refresh);

            if (!starsOutsideResult.IsError)
                stars.AddRange(starsOutsideResult.Result);

            result.Result = stars;
            return result;
        }

        public OASISResult<IEnumerable<IStar>> GetAllStarsForMultiverse(bool refresh = true)
        {
            return GetAllStarsForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IPlanet>>> GetAllPlanetsOutSideOfGalaxiesForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IPlanet>> result = new OASISResult<IEnumerable<IPlanet>>();
            OASISResult<IEnumerable<IGalaxyCluster>> galaxyClustersResult = await GetAllGalaxyClustersForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IGalaxyCluster>, IEnumerable<IPlanet>>.CopyResult(galaxyClustersResult, ref result);

            if (!galaxyClustersResult.IsError)
            {
                List<IPlanet> planets = new List<IPlanet>();

                foreach (IGalaxyCluster cluster in galaxyClustersResult.Result)
                    planets.AddRange(cluster.Planets);

                result.Result = planets;
            }

            return result;
        }

        public OASISResult<IEnumerable<IPlanet>> GetAllPlanetsOutSideOfGalaxiesForMultiverse(bool refresh = true)
        {
            return GetAllPlanetsOutSideOfGalaxiesForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IPlanet>>> GetAllPlanetsOutSideOfGalaxyClustersForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IPlanet>> result = new OASISResult<IEnumerable<IPlanet>>();
            OASISResult<IEnumerable<IDimension>> dimensionsResult = await GetAllDimensionsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IDimension>, IEnumerable<IPlanet>>.CopyResult(dimensionsResult, ref result);

            if (!dimensionsResult.IsError)
            {
                List<IPlanet> planets = new List<IPlanet>();

                foreach (IDimension dimension in dimensionsResult.Result)
                    planets.AddRange(dimension.Planets);

                result.Result = planets;
            }

            return result;
        }

        public OASISResult<IEnumerable<IPlanet>> GetAllPlanetsOutSideOfGalaxyClustersForMultiverse(bool refresh = true)
        {
            return GetAllPlanetsOutSideOfGalaxyClustersForMultiverseAsync(refresh).Result;
        }

        public async Task<OASISResult<IEnumerable<IPlanet>>> GetAllPlanetsForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IPlanet>> result = new OASISResult<IEnumerable<IPlanet>>();
            OASISResult<IEnumerable<ISuperStar>> superStarsResult = await GetAllSuperStarsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<ISuperStar>, IEnumerable<IPlanet>>.CopyResult(superStarsResult, ref result);
            List<IPlanet> planets = new List<IPlanet>();

            if (!superStarsResult.IsError)
            {
                foreach (ISuperStar superStar in superStarsResult.Result)
                {
                    OASISResult<IEnumerable<IPlanet>> planetsResult = await ((ISuperStarCore)superStar.CelestialBodyCore).GetAllPlanetsForGalaxyAsync(refresh);

                    if (!planetsResult.IsError)
                        planets.AddRange(planetsResult.Result);
                }
            }

            OASISResult<IEnumerable<IPlanet>> planetsOutsideResult = await GetAllPlanetsOutSideOfGalaxyClustersForMultiverseAsync(refresh);

            if (!planetsOutsideResult.IsError)
                planets.AddRange(planetsOutsideResult.Result);

            planetsOutsideResult = await GetAllPlanetsOutSideOfGalaxiesForMultiverseAsync(refresh);

            if (!planetsOutsideResult.IsError)
                planets.AddRange(planetsOutsideResult.Result);

            result.Result = planets;
            return result;
        }

        public OASISResult<IEnumerable<IPlanet>> GetAllPlanetsForMultiverse(bool refresh = true)
        {
            return GetAllPlanetsForMultiverseAsync(refresh).Result;
        }


        /*
        public async Task<OASISResult<IEnumerable<IPlanet>>> GetAllPlanetsForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IPlanet>> result = new OASISResult<IEnumerable<IPlanet>>();
            OASISResult<IEnumerable<IStar>> starsResult = await GetAllStarsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IStar>, IEnumerable<IPlanet>>.CopyResult(starsResult, ref result);

            if (!starsResult.IsError)
            {
                List<IPlanet> planets = new List<IPlanet>();

                foreach (IStar star in starsResult.Result)
                {
                    OASISResult<IEnumerable<IPlanet>> planetsResult = await ((IStarCore)star.CelestialBodyCore).GetAllPlanetsForSolarSystemAsync(refresh);

                    if (!planetsResult.IsError)
                        planets.AddRange(planetsResult.Result);
                }

                result.Result = planets;
            }

            return result;
        }

        public OASISResult<IEnumerable<IPlanet>> GetAllPlanetsForMultiverse(bool refresh = true)
        {
            return GetAllPlanetsForMultiverseAsync(refresh).Result;
        }*/

        public async Task<OASISResult<IEnumerable<IMoon>>> GetAllMoonsForMultiverseAsync(bool refresh = true)
        {
            OASISResult<IEnumerable<IMoon>> result = new OASISResult<IEnumerable<IMoon>>();
            OASISResult<IEnumerable<IPlanet>> planetsResult = await GetAllPlanetsForMultiverseAsync(refresh);
            OASISResultCollectionToCollectionHelper<IEnumerable<IPlanet>, IEnumerable<IMoon>>.CopyResult(planetsResult, ref result);

            if (!planetsResult.IsError)
            {
                List<IMoon> moons = new List<IMoon>();

                foreach (IPlanet planet in planetsResult.Result)
                {
                    OASISResult<IEnumerable<IMoon>> moonsResult = await ((IPlanetCore)planet.CelestialBodyCore).GetMoonsAsync(refresh);

                    if (!moonsResult.IsError)
                        moons.AddRange(moonsResult.Result);
                }

                result.Result = moons;
            }

            return result;
        }

        public OASISResult<IEnumerable<IMoon>> GetAllMoonsForMultiverse(bool refresh = true)
        {
            return GetAllMoonsForMultiverseAsync(refresh).Result;
        }
    }
}
﻿using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using NextGenSoftware.OASIS.Common;
using NextGenSoftware.OASIS.API.DNA;
using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Helpers;
using NextGenSoftware.OASIS.API.Core.Managers;
using NextGenSoftware.OASIS.API.Core.Interfaces;
using NextGenSoftware.OASIS.API.Core.Interfaces.STAR;
using NextGenSoftware.OASIS.API.ONode.Core.Holons;
using NextGenSoftware.OASIS.API.ONode.Core.Interfaces.Holons;

using NextGenSoftware.OASIS.API.ONode.Core.Interfaces;

namespace NextGenSoftware.OASIS.API.ONode.Core.Managers
{
    public class OAPPManager : OASISManager
    {
       //private static OAPPManager _instance = null;

        public OAPPManager(IOASISStorageProvider OASISStorageProvider, Guid avatarId, OASISDNA OASISDNA = null) : base(OASISStorageProvider, avatarId, OASISDNA)
        {

        }

        public OAPPManager(Guid avatarId, OASISDNA OASISDNA = null) : base(avatarId, OASISDNA)
        {

        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public async Task<OASISResult<IEnumerable<IOAPP>>> ListOAPPsCreatedByAvatarAsync(Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IEnumerable<IOAPP>> result = new OASISResult<IEnumerable<IOAPP>>();
            string errorMessage = "Error occured in OAPPManager.ListOAPPsCreatedByAvatarAsync, Reason:";

            try
            {
                OASISResult<IEnumerable<OAPP>> oapps = await Data.LoadHolonsForParentAsync<OAPP>(avatarId, HolonType.OAPP, true, true, 0, true, false, 0, HolonType.All, 0, providerType);

                if (oapps != null && oapps.Result != null && !oapps.IsError)
                {
                    result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IEnumerable<OAPP>, IEnumerable<IOAPP>>(oapps);
                    result.Result = [.. oapps.Result];
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Data.LoadHolonsForParentAsync, reason: {oapps.Message}");
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public OASISResult<IEnumerable<IOAPP>> ListOAPPsCreatedByAvatar(Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IEnumerable<IOAPP>> result = new OASISResult<IEnumerable<IOAPP>>();
            string errorMessage = "Error occured in OAPPManager.ListOAPPsCreatedByAvatar, Reason:";

            try
            {
                OASISResult<IEnumerable<OAPP>> oapps = Data.LoadHolonsForParent<OAPP>(avatarId, HolonType.OAPP, true, true, 0, true, false, 0, HolonType.All, 0, providerType);

                if (oapps != null && oapps.Result != null && !oapps.IsError)
                {
                    result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IEnumerable<OAPP>, IEnumerable<IOAPP>>(oapps);
                    result.Result = [.. oapps.Result];
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Data.LoadHolonsForParent, reason: {oapps.Message}");
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public async Task<OASISResult<IEnumerable<IOAPP>>> ListAllOAPPsAsync(ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IEnumerable<IOAPP>> result = new OASISResult<IEnumerable<IOAPP>>();
            string errorMessage = "Error occured in OAPPManager.ListAllOAPPsAsync, Reason:";

            try
            {
                //result = DecodeNFTMetaData(await Data.LoadAllHolonsAsync(HolonType.NFT, true, true, 0, true, false, HolonType.All, 0, providerType), result, errorMessage);
                //OASISResult<IEnumerable<IHolon>> holons = await Data.LoadAllHolonsAsync(HolonType.OAPP, true, true, 0, true, false, HolonType.All, 0, providerType);
                OASISResult<IEnumerable<OAPP>> oapps = await Data.LoadAllHolonsAsync<OAPP>(HolonType.OAPP, true, true, 0, true, false, HolonType.All, 0, providerType);

                if (oapps != null && oapps.Result != null && !oapps.IsError)
                {
                    result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IEnumerable<OAPP>, IEnumerable<IOAPP>>(oapps);
                    result.Result = [.. oapps.Result];
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Data.LoadAllHolonsAsync, reason: {oapps.Message}");

                //if (holons != null && holons.Result != null && !holons.IsError)
                //{
                //    List<IOAPP> oapps = new List<IOAPP>();

                //    foreach (IHolon holon in holons.Result) 
                //    {
                //        IOAPP OAPP = Mapper<IHolon, OAPP>.MapBaseHolonProperties(holon);
                //        OAPP.GenesisType = holon.MetaData[]
                //    }
                //}
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public OASISResult<IEnumerable<IOAPP>> ListAllOAPPs(ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IEnumerable<IOAPP>> result = new OASISResult<IEnumerable<IOAPP>>();
            string errorMessage = "Error occured in OAPPManager.ListAllOAPPs, Reason:";

            try
            {
                OASISResult<IEnumerable<OAPP>> oapps = Data.LoadAllHolons<OAPP>(HolonType.OAPP, true, true, 0, true, false, HolonType.All, 0, providerType);

                if (oapps != null && oapps.Result != null && !oapps.IsError)
                {
                    result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IEnumerable<OAPP>, IEnumerable<IOAPP>>(oapps);
                    result.Result = [.. oapps.Result];

                    
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Data.LoadAllHolons, reason: {oapps.Message}");
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public async Task<OASISResult<IOAPP>> LoadOAPPAsync(Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadOAPPAsync, Reason:";

            try
            {
                OASISResult<OAPP> oapp = await Data.LoadHolonAsync<OAPP>(OAPPId, true, true, 0, true, false, HolonType.All, 0, providerType);

                if (oapp != null && oapp.Result != null && !oapp.IsError)
                {
                    //OAPPDNA oappDNA = JsonSerializer.Deserialize<OAPPDNA>(oapp.Result.MetaData["OAPPDNAJSON"].ToString());
                    //oapp.Result.OAPPDNA = oappDNA;

                    result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<OAPP, IOAPP>(oapp);

                    //result.Result.OAPPDNA = JsonSerializer.Deserialize<OAPPDNA>(result.Result.MetaData["OAPPDNAJSON"].ToString());
                    result.Result = oapp.Result;
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Data.LoadHolonAsync, reason: {oapp.Message}");
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public OASISResult<IOAPP> LoadOAPP(Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadOAPP, Reason:";

            try
            {
                OASISResult<OAPP> oapp = Data.LoadHolon<OAPP>(OAPPId, true, true, 0, true, false, HolonType.All, 0, providerType);

                if (oapp != null && oapp.Result != null && !oapp.IsError)
                {
                    //OAPPDNA oappDNA = JsonSerializer.Deserialize<OAPPDNA>(oapp.Result.MetaData["OAPPDNAJSON"].ToString());
                    //oapp.Result.OAPPDNA = oappDNA;

                    result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<OAPP, IOAPP>(oapp);
                    result.Result = oapp.Result;
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Data.LoadHolon, reason: {oapp.Message}");
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        public async Task<OASISResult<IOAPP>> LoadOAPPAsync(string fullPathToOAPP, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadOAPPAsync, Reason:";

            try
            {
                OASISResult<IOAPPDNA> readOAPPDNAResult = await ReadOAPPDNAAsync(fullPathToOAPP);

                if (readOAPPDNAResult != null && readOAPPDNAResult.Result != null && !readOAPPDNAResult.IsError)
                    result = await LoadOAPPAsync(readOAPPDNAResult.Result.OAPPId, providerType);
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Reading the OAPPDNA with ReadOAPPDNAAsync. Reason: {readOAPPDNAResult.Message}");
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        public OASISResult<IOAPP> LoadOAPP(string fullPathToOAPP, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadOAPP, Reason:";

            try
            {
                OASISResult<IOAPPDNA> readOAPPDNAResult = ReadOAPPDNA(fullPathToOAPP);

                if (readOAPPDNAResult != null && readOAPPDNAResult.Result != null && !readOAPPDNAResult.IsError)
                    result = LoadOAPP(readOAPPDNAResult.Result.OAPPId, providerType);
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Reading the OAPPDNA with ReadOAPPDNA. Reason: {readOAPPDNAResult.Message}");
            }
            catch (Exception e)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Unknown error occured: {e.Message}", e);
            }

            return result;
        }

        public async Task<OASISResult<IOAPPDNA>> CreateOAPPAsync(string OAPPName, string OAPPDescription, OAPPType OAPPType, GenesisType genesisType, Guid avatarId, string fullPathToOAPP, ICelestialBody celestialBody = null, IEnumerable<IZome> zomes = null, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            string errorMessage = "Error occured in OAPPManager.CreateOAPPAsync, Reason:";

            try
            {
                //TODO: Phase this out ASAP!
                OAPP OAPP = new OAPP()
                {
                    Id = Guid.NewGuid(),
                    Name = OAPPName,
                    Description = OAPPDescription,
                    //CelestialBody = celestialBody, //The CelestialBody that represents the OAPP (if any).
                    //CelestialBodyId = celestialBody != null ? celestialBody.Id : Guid.Empty,
                    //OAPPType = OAPPType,
                    //GenesisType = genesisType,
                };

                //if (celestialBody != null)
                //{
                //    foreach (IZome zome in celestialBody.CelestialBodyCore.Zomes)
                //        OAPP.Children.Add(zome);
                //}

                //if (zomes != null)
                //{
                //    foreach (IZome zome in zomes)
                //        OAPP.Children.Add(zome);
                //}

                OASISResult<IAvatar> avatarResult = await AvatarManager.Instance.LoadAvatarAsync(avatarId, false, true, providerType);

                if (avatarResult != null && avatarResult.Result != null && !avatarResult.IsError)
                {
                    OAPPDNA OAPPDNA = new OAPPDNA()
                    {
                        OAPPId = OAPP.Id,
                        OAPPName = OAPPName,
                        Description = OAPPDescription,
                        //CelestialBody = celestialBody, //TODO: Temp
                        CelestialBodyId = celestialBody != null ? celestialBody.Id : Guid.Empty,
                        CelestialBodyName = celestialBody != null ? celestialBody.Name : "",
                        CelestialBodyType = celestialBody != null ? celestialBody.HolonType : HolonType.None,
                        //Zomes = zomes, //Can be either zomes of CelestialBody but not both (zomes are contained in CelestialBody) but if no CelestialBody is generated then this prop is used instead.
                        OAPPType = OAPPType,
                        GenesisType = genesisType,
                        CreatedByAvatarId = avatarId,
                        CreatedByAvatarUsername = avatarResult.Result.Username,
                        CreatedOn = DateTime.Now,
                        Version = "1.0.0"
                    };

                    await WriteOAPPDNAAsync(OAPPDNA, fullPathToOAPP);

                    OAPP.MetaData["OAPPDNAJSON"] = JsonSerializer.Serialize(OAPPDNA); //Store the OAPPDNA in the db so it can be verified later against the file OASISDNA when publishing, installing etc to make sure its not been tampered with.
                    //OAPP.OAPPDNA = JsonSerializer.Serialize(OAPPDNA); //OAPPDNA;
                    OASISResult<OAPP> saveHolonResult = await Data.SaveHolonAsync<OAPP>(OAPP, avatarId, true, true, 0, true, false, providerType);

                    if (saveHolonResult != null && saveHolonResult.Result != null && !saveHolonResult.IsError)
                    {
                        if (celestialBody != null)
                        {
                            //celestialBody.MetaData["OAPPID"] = saveHolonResult.Result.Id; //Store a link to the OAPP on the CelestialBody.
                            //celestialBody.MetaData["OAPP"] = JsonSerializer.Serialize(saveHolonResult.Result); //TODO: Don't think we need to store the whole OAPP here (especially since it extends Holon so has a LOT of props) and is duplicated when the OAPP can be easily loaded from the ID.

                            OASISResult<ICelestialBody> celestialBodyResult = await celestialBody.SaveAsync(true, true, 0, true, false, providerType);

                            if (celestialBodyResult != null && celestialBodyResult.Result != null && !celestialBodyResult.IsError)
                            {
                                result.Result = OAPPDNA;
                                result.Message = $"Successfully created the OAPP on the {Enum.GetName(typeof(ProviderType), providerType)} provider by AvatarId {avatarId} for OAPPType {Enum.GetName(typeof(OAPPType), OAPPType)} and GenesisType {Enum.GetName(typeof(GenesisType), genesisType)}.";
                            }
                            else
                                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving OAPP to the {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {saveHolonResult.Message}");
                        }
                        else
                            result.Message = $"Successfully created the OAPP on the {Enum.GetName(typeof(ProviderType), providerType)} provider by AvatarId {avatarId} for OAPPType {Enum.GetName(typeof(OAPPType), OAPPType)} and GenesisType {Enum.GetName(typeof(GenesisType), genesisType)}.";
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving OAPP to the {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {saveHolonResult.Message}");
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadAvatarAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {avatarResult.Message}");
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving OAPP to the {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {ex}");
            }

            return result;
        }
        public OASISResult<IOAPPDNA> CreateOAPP(string OAPPName, string OAPPDescription, OAPPType OAPPType, GenesisType genesisType, Guid avatarId, string fullPathToOAPP, ICelestialBody celestialBody = null, IEnumerable<IZome> zomes = null, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            string errorMessage = "Error occured in OAPPManager.CreateOAPP, Reason:";

            try
            {
                OAPP OAPP = new OAPP()
                {
                    Id = Guid.NewGuid(),
                    Name = OAPPName,
                    Description = OAPPDescription,
                    //CelestialBody = celestialBody, //The CelestialBody that represents the OAPP (if any).
                    //CelestialBodyId = celestialBody != null ? celestialBody.Id : Guid.Empty,
                    //OAPPType = OAPPType,
                    //GenesisType = genesisType,
                };

                if (zomes != null)
                {
                    foreach (IZome zome in zomes)
                        OAPP.Children.Add(zome);
                }

                OASISResult<IAvatar> avatarResult = AvatarManager.Instance.LoadAvatar(avatarId, false, true, providerType);

                if (avatarResult != null && avatarResult.Result != null && !avatarResult.IsError)
                {
                    OAPPDNA OAPPDNA = new OAPPDNA()
                    {
                        OAPPId = OAPP.Id,
                        OAPPName = OAPPName,
                        Description = OAPPDescription,
                        //CelestialBody = celestialBody, //TODO: Temp
                        CelestialBodyId = celestialBody != null ? celestialBody.Id : Guid.Empty,
                        CelestialBodyName = celestialBody != null ? celestialBody.Name : "",
                        CelestialBodyType = celestialBody != null ? celestialBody.HolonType : HolonType.None,
                        //Zomes = zomes, //Can be either zomes of CelestialBody but not both (zomes are contained in CelestialBody) but if no CelestialBody is generated then this prop is used instead.
                        OAPPType = OAPPType,
                        GenesisType = genesisType,
                        CreatedByAvatarId = avatarId,
                        CreatedByAvatarUsername = avatarResult.Result.Username,
                        CreatedOn = DateTime.Now,
                        Version = "1.0.0"
                    };

                    WriteOAPPDNA(OAPPDNA, fullPathToOAPP);

                    OAPP.MetaData["OAPPDNAJSON"] = JsonSerializer.Serialize(OAPPDNA); //Store the OAPPDNA in the db so it can be verified later against the file OASISDNA when publishing, installing etc to make sure its not been tampered with.
                    //OAPP.OAPPDNA = OAPPDNA;
                    OASISResult<IHolon> saveHolonResult = Data.SaveHolon(OAPP, avatarId, true, true, 0, true, false, providerType);

                    if (saveHolonResult != null && saveHolonResult.Result != null && !saveHolonResult.IsError)
                    {
                        if (celestialBody != null)
                        {
                            //celestialBody.MetaData["OAPPID"] = saveHolonResult.Result.Id;
                            //celestialBody.MetaData["OAPP"] = JsonSerializer.Serialize(saveHolonResult.Result); //TODO: Don't think we need to store the whole OAPP here (especially since it extends Holon so has a LOT of props) and is duplicated when the OAPP can be easily loaded from the ID.

                            OASISResult<ICelestialBody> celestialBodyResult = celestialBody.Save(true, true, 0, true, false, providerType);

                            if (celestialBodyResult != null && celestialBodyResult.Result != null && !celestialBodyResult.IsError)
                            {
                                result.Result = OAPPDNA;
                                result.Message = $"Successfully created the OAPP on the {Enum.GetName(typeof(ProviderType), providerType)} provider by AvatarId {avatarId} for OAPPType {Enum.GetName(typeof(OAPPType), OAPPType)} and GenesisType {Enum.GetName(typeof(GenesisType), genesisType)}.";
                            }
                            else
                                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving OAPP to the {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {saveHolonResult.Message}");
                        }
                        else
                            result.Message = $"Successfully created the OAPP on the {Enum.GetName(typeof(ProviderType), providerType)} provider by AvatarId {avatarId} for OAPPType {Enum.GetName(typeof(OAPPType), OAPPType)} and GenesisType {Enum.GetName(typeof(GenesisType), genesisType)}.";
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving OAPP to the {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {saveHolonResult.Message}");
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadAvatar on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {avatarResult.Message}");
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving OAPP to the {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {ex}");
            }

            return result;
        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public async Task<OASISResult<IOAPP>> SaveOAPPAsync(IOAPP OAPP, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IHolon> saveResult = await OAPP.SaveAsync(true, true, 0, true, false, providerType);

            if (saveResult != null && !saveResult.IsError && saveResult.Result != null)
            {
                result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IHolon, IOAPP>(saveResult);
                result.Result = (IOAPP)saveResult.Result;
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.SaveOAPPAsync saving the OAPP. Reason: {saveResult.Message}");

            return result;
        }

        //TODO: Think will return IOAPPDNA instead of IOAPP (get from metadata)
        public OASISResult<IOAPP> SaveOAPP(IOAPP OAPP, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IHolon> saveResult = OAPP.Save(true, true, 0, true, false, providerType);

            if (saveResult != null && !saveResult.IsError && saveResult.Result != null)
            {
                result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IHolon, IOAPP>(saveResult);
                result.Result = (IOAPP)saveResult.Result;
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.SaveOAPP saving the OAPP. Reason: {saveResult.Message}");

            return result;
        }

        public async Task<OASISResult<IOAPPDNA>> PublishOAPPAsync(string fullPathToOAPP, string launchTarget, Guid avatarId, bool dotnetPublish = true, string fullPathToPublishTo = "", bool registerOnSTARNET = true, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            string errorMessage = "Error occured in OAPPManager.PublishOAPPAsync. Reason: ";

            try
            {
                OASISResult<IOAPPDNA> readOAPPDNAResult = await ReadOAPPDNAAsync(fullPathToOAPP);

                if (readOAPPDNAResult != null && !readOAPPDNAResult.IsError && readOAPPDNAResult.Result != null)
                {
                    OASISResult<IAvatar> loadAvatarResult = await AvatarManager.Instance.LoadAvatarAsync(avatarId, false, true, providerType);

                    if (loadAvatarResult != null && loadAvatarResult.Result != null && !loadAvatarResult.IsError)
                    {
                        string publishedOAPPFileName = string.Concat(readOAPPDNAResult.Result.OAPPName, ".oapp");
                        string tempPath = Path.Combine(Path.GetTempPath(), publishedOAPPFileName);

                        if (dotnetPublish)
                        {
                            //TODO: Finish implementing this.
                            //Process.Start("dotnet publish -c Release -r <RID> --self-contained");
                            //Process.Start("dotnet publish -c Release -r win-x64 --self-contained");
                            //string command = 

                            string dotnetPublishPath = Path.Combine(fullPathToOAPP, "dotnetPublished");
                            Process.Start($"dotnet publish PROJECT {fullPathToOAPP} -c Release --self-contained -output {dotnetPublishPath}");
                            fullPathToOAPP = dotnetPublishPath;

                            //"bin\\Release\\net8.0\\";
                        }

                        if (string.IsNullOrEmpty(fullPathToPublishTo))
                            fullPathToPublishTo = Path.Combine(fullPathToOAPP, "Published", publishedOAPPFileName);

                        readOAPPDNAResult.Result.PublishedOn = DateTime.Now;
                        readOAPPDNAResult.Result.PublishedByAvatarId = avatarId;
                        readOAPPDNAResult.Result.PublishedByAvatarUsername = loadAvatarResult.Result.Username;
                        readOAPPDNAResult.Result.LaunchTarget = launchTarget;
                        readOAPPDNAResult.Result.PublishedPath = fullPathToPublishTo;
                        readOAPPDNAResult.Result.PublishedOnSTARNET = registerOnSTARNET;

                        WriteOAPPDNA(readOAPPDNAResult.Result, fullPathToOAPP);

                        if (File.Exists(tempPath))
                            File.Delete(tempPath);

                        ZipFile.CreateFromDirectory(fullPathToOAPP, tempPath);

                        Directory.CreateDirectory(Path.Combine(fullPathToOAPP, "Published"));
                        File.Move(tempPath, readOAPPDNAResult.Result.PublishedPath);

                        if (File.Exists(tempPath))
                            File.Delete(tempPath);

                        OASISResult<IOAPP> loadOAPPResult = await LoadOAPPAsync(readOAPPDNAResult.Result.OAPPId);

                        if (loadOAPPResult != null && loadOAPPResult.Result != null && !loadOAPPResult.IsError)
                        {
                            if (registerOnSTARNET)
                                loadOAPPResult.Result.PublishedOAPP = File.ReadAllBytes(readOAPPDNAResult.Result.PublishedPath);

                            loadOAPPResult.Result.OAPPDNA = readOAPPDNAResult.Result;
                            OASISResult<IOAPP> saveOAPPResult = await SaveOAPPAsync(loadOAPPResult.Result, providerType);

                            if (saveOAPPResult != null && !saveOAPPResult.IsError && saveOAPPResult.Result != null)
                            {
                                result.Result = readOAPPDNAResult.Result;
                                result.IsSaved = true;
                            }
                            else
                                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling SaveOAPPAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {saveOAPPResult.Message}");
                        }
                        else
                            OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadOAPPAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {loadOAPPResult.Message}");
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadAvatarAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {loadAvatarResult.Message}");
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling ReadOAPPDNAAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {readOAPPDNAResult.Message}");
            }
            catch (Exception ex) 
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} {ex}");
            }

            return result;
        }

        //TODO: Come back to this, was going to call this for publishing and installing to make sure the DNA hadn't been changed on the disk, but maybe we want to allow this? Not sure, needs more thought...
        //private async OASISResult<bool> IsOAPPDNAValidAsync(IOAPPDNA OAPPDNA)
        //{
        //    OASISResult<IOAPP> OAPPResult = await LoadOAPPAsync(OAPPDNA.OAPPId);

        //    if (OAPPResult != null && OAPPResult.Result != null && !OAPPResult.IsError)
        //    {
        //        IOAPPDNA originalDNA =  JsonSerializer.Deserialize<IOAPPDNA>(OAPPResult.Result.MetaData["OAPPDNA"].ToString());

        //        if (originalDNA != null)
        //        {
        //            if (originalDNA.GenesisType != OAPPDNA.GenesisType ||
        //                originalDNA.OAPPType != OAPPDNA.OAPPType ||
        //                originalDNA.CelestialBodyType != OAPPDNA.CelestialBodyType ||
        //                originalDNA.CelestialBodyId != OAPPDNA.CelestialBodyId ||
        //                originalDNA.CelestialBodyName != OAPPDNA.CelestialBodyName ||
        //                originalDNA.CreatedByAvatarId != OAPPDNA.CreatedByAvatarId ||
        //                originalDNA.CreatedByAvatarUsername != OAPPDNA.CreatedByAvatarUsername ||
        //                originalDNA.CreatedOn != OAPPDNA.CreatedOn ||
        //                originalDNA.Description != OAPPDNA.Description ||
        //                originalDNA.IsActive != OAPPDNA.IsActive ||
        //                originalDNA.LaunchTarget != OAPPDNA.LaunchTarget ||
        //                originalDNA. != OAPPDNA.LaunchTarget ||

        //        }
        //    }
        //}

        public OASISResult<IOAPPDNA> PublishOAPP(string fullPathToOAPP, string launchTarget, Guid avatarId, string fullPathToPublishTo = "", bool registerOnSTARNET = true, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            string errorMessage = "Error occured in OAPPManager.PublishOAPP. Reason: ";

            try
            {
                OASISResult<IOAPPDNA> readOAPPDNAResult = ReadOAPPDNA(fullPathToOAPP);

                if (readOAPPDNAResult != null && !readOAPPDNAResult.IsError && readOAPPDNAResult.Result != null)
                {
                    OASISResult<IAvatar> loadAvatarResult = AvatarManager.Instance.LoadAvatar(avatarId, false, true, providerType);

                    if (loadAvatarResult != null && loadAvatarResult.Result != null && !loadAvatarResult.IsError)
                    {
                        string publishedOAPPFileName = string.Concat(readOAPPDNAResult.Result.OAPPName, ".oapp");
                        string tempPath = Path.Combine(Path.GetTempPath(), publishedOAPPFileName);

                        if (string.IsNullOrEmpty(fullPathToPublishTo))
                            fullPathToPublishTo = Path.Combine(fullPathToOAPP, "Published", publishedOAPPFileName);

                        readOAPPDNAResult.Result.PublishedOn = DateTime.Now;
                        readOAPPDNAResult.Result.PublishedByAvatarId = avatarId;
                        readOAPPDNAResult.Result.PublishedByAvatarUsername = loadAvatarResult.Result.Username;
                        readOAPPDNAResult.Result.LaunchTarget = launchTarget;
                        readOAPPDNAResult.Result.PublishedPath = fullPathToPublishTo;
                        readOAPPDNAResult.Result.PublishedOnSTARNET = registerOnSTARNET;

                        WriteOAPPDNA(readOAPPDNAResult.Result, fullPathToOAPP);

                        if (File.Exists(tempPath))
                            File.Delete(tempPath);

                        ZipFile.CreateFromDirectory(fullPathToOAPP, tempPath);

                        Directory.CreateDirectory(Path.Combine(fullPathToOAPP, "Published"));
                        File.Move(tempPath, readOAPPDNAResult.Result.PublishedPath);

                        if (File.Exists(tempPath))
                            File.Delete(tempPath);

                        OASISResult<IOAPP> loadOAPPResult = LoadOAPP(readOAPPDNAResult.Result.OAPPId);

                        if (loadOAPPResult != null && loadOAPPResult.Result != null && !loadOAPPResult.IsError)
                        {
                            if (registerOnSTARNET)
                                loadOAPPResult.Result.PublishedOAPP = File.ReadAllBytes(readOAPPDNAResult.Result.PublishedPath);

                            loadOAPPResult.Result.OAPPDNA = readOAPPDNAResult.Result;
                            OASISResult<IOAPP> saveOAPPResult = SaveOAPP(loadOAPPResult.Result, providerType);

                            if (saveOAPPResult != null && !saveOAPPResult.IsError && saveOAPPResult.Result != null)
                            {
                                result.Result = readOAPPDNAResult.Result;
                                result.IsSaved = true;
                            }
                            else
                                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling SaveOAPPAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {saveOAPPResult.Message}");
                        }
                        else
                            OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadOAPPAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {loadOAPPResult.Message}");
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadAvatarAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {loadAvatarResult.Message}");
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling ReadOAPPDNAAsync on {Enum.GetName(typeof(ProviderType), providerType)} provider. Reason: {readOAPPDNAResult.Message}");
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} {ex}");
            }

            return result;
        }

        public async Task<OASISResult<IOAPPDNA>> UnPublishOAPPAsync(IOAPPDNA OAPPDNA, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>(OAPPDNA);
            OASISResult<IOAPP> oappResult = await LoadOAPPAsync(OAPPDNA.OAPPId, providerType);
            string errorMessage = "Error occured in UnPublishOAPPAsync. Reason: ";

            if (oappResult != null && oappResult.Result != null && !oappResult.IsError)
            {
                oappResult.Result.OAPPDNA.PublishedOn = DateTime.MinValue;
                oappResult.Result.OAPPDNA.PublishedByAvatarId = Guid.Empty;
                oappResult.Result.OAPPDNA.PublishedByAvatarUsername = "";

                oappResult = await SaveOAPPAsync(oappResult.Result, providerType);

                if (oappResult != null && oappResult.Result != null && !oappResult.IsError)
                {
                    OAPPDNA.PublishedOn = DateTime.MinValue;
                    OAPPDNA.PublishedByAvatarId = Guid.Empty;
                    result.Message = "OAPP Unpublised";
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving the OAPP with the SaveOAPPAsync method, reason: {oappResult.Message}");
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured loading the OAPP with the LoadOAPPAsync method, reason: {oappResult.Message}");

            return result;
        }

        public OASISResult<IOAPPDNA> UnPublishOAPP(IOAPPDNA OAPPDNA, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>(OAPPDNA);
            OASISResult<IOAPP> oappResult = LoadOAPP(OAPPDNA.OAPPId, providerType);
            string errorMessage = "Error occured in UnPublishOAPP. Reason: ";

            if (oappResult != null && oappResult.Result != null && !oappResult.IsError)
            {
                oappResult.Result.OAPPDNA.PublishedOn = DateTime.MinValue;
                oappResult.Result.OAPPDNA.PublishedByAvatarId = Guid.Empty;
                oappResult.Result.OAPPDNA.PublishedByAvatarUsername = "";

                oappResult = SaveOAPP(oappResult.Result, providerType);

                if (oappResult != null && oappResult.Result != null && !oappResult.IsError)
                {
                    OAPPDNA.PublishedOn = DateTime.MinValue;
                    OAPPDNA.PublishedByAvatarId = Guid.Empty;
                    result.Message = "OAPP Unpublised";
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving the OAPP with the SaveOAPP method, reason: {oappResult.Message}");
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured loading the OAPP with the LoadOAPP method, reason: {oappResult.Message}");

            return result;
        }

        public async Task<OASISResult<IOAPPDNA>> UnPublishOAPPAsync(IOAPP OAPP, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            string errorMessage = "Error occured in UnPublishOAPPAsync. Reason: ";

            OAPP.OAPPDNA.PublishedOn = DateTime.MinValue;
            OAPP.OAPPDNA.PublishedByAvatarId = Guid.Empty;
            OAPP.OAPPDNA.PublishedByAvatarUsername = "";

            OASISResult<IOAPP> oappResult = await SaveOAPPAsync(OAPP, providerType);

            if (oappResult != null && oappResult.Result != null && !oappResult.IsError)
            {
                result.Result = OAPP.OAPPDNA; //ConvertOAPPToOAPPDNA(OAPP);
                result.Message = "OAPP Unpublised";
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving the OAPP with the SaveOAPPAsync method, reason: {oappResult.Message}");
            
            return result;
        }

        public OASISResult<IOAPPDNA> UnPublishOAPP(IOAPP OAPP, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            string errorMessage = "Error occured in UnPublishOAPP. Reason: ";

            OAPP.OAPPDNA.PublishedOn = DateTime.MinValue;
            OAPP.OAPPDNA.PublishedByAvatarId = Guid.Empty;
            OAPP.OAPPDNA.PublishedByAvatarUsername = "";

            OASISResult<IOAPP> oappResult = SaveOAPP(OAPP, providerType);

            if (oappResult != null && oappResult.Result != null && !oappResult.IsError)
            {
                result.Result = OAPP.OAPPDNA; //ConvertOAPPToOAPPDNA(OAPP);
                result.Message = "OAPP Unpublised";
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured saving the OAPP with the SaveOAPP method, reason: {oappResult.Message}");

            return result;
        }

        public async Task<OASISResult<IOAPPDNA>> UnPublishOAPPAsync(Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            OASISResult<IOAPP> loadResult = await LoadOAPPAsync(OAPPId, providerType);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
                result = await UnPublishOAPPAsync(loadResult.Result, providerType);
            else
                OASISErrorHandling.HandleError(ref result, $"Error occured in UnPublishOAPPAsync loading the OAPP with the LoadOAPPAsync method, reason: {loadResult.Message}");

            return result;
        }

        public OASISResult<IOAPPDNA> UnPublishOAPP(Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            OASISResult<IOAPP> loadResult = LoadOAPP(OAPPId, providerType);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
                result = UnPublishOAPP(loadResult.Result, providerType);
            else
                OASISErrorHandling.HandleError(ref result, $"Error occured in UnPublishOAPP loading the OAPP with the LoadOAPP method, reason: {loadResult.Message}");

            return result;
        }

        public async Task<OASISResult<IInstalledOAPP>> InstallOAPPAsync(Guid avatarId, string fullPathToPublishedOAPPFile, string fullInstallPath, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.InstallOAPPAsync. Reason: ";

            try
            {
                ZipFile.ExtractToDirectory(fullPathToPublishedOAPPFile, fullInstallPath, Encoding.Default, true);
                OASISResult<IOAPPDNA> OAPPDNAResult = await ReadOAPPDNAAsync(fullInstallPath);

                if (OAPPDNAResult != null && OAPPDNAResult.Result != null && !OAPPDNAResult.IsError)
                {
                    OASISResult<IAvatar> avatarResult = await AvatarManager.Instance.LoadAvatarAsync(avatarId, false, true, providerType);

                    if (avatarResult != null && !avatarResult.IsError && avatarResult.Result != null)
                    {
                        InstalledOAPP installedOAPP = new InstalledOAPP()
                        {
                            OAPPId = OAPPDNAResult.Result.OAPPId,
                            OAPPDNA = OAPPDNAResult.Result,
                            InstalledBy = avatarId,
                            InstalledByAvatarUsername = avatarResult.Result.Username,
                            InstalledOn = DateTime.Now,
                            InstalledPath = fullInstallPath
                        };

                        OASISResult<IHolon> saveResult = await installedOAPP.SaveAsync();

                        if (saveResult != null && saveResult.Result != null && !saveResult.IsError)
                        {
                            result.Message = "OAPP Installed";
                            result.Result = installedOAPP;
                        }
                        else
                            OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling SaveAsync method. Reason: {saveResult.Message}");
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadAvatarAsync method. Reason: {avatarResult.Message}");
                }
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} {ex}");
            }

            return result;
        }

        public OASISResult<IInstalledOAPP> InstallOAPP(Guid avatarId, string fullPathToPublishedOAPPFile, string fullInstallPath, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.InstallOAPP. Reason: ";

            try
            {
                ZipFile.ExtractToDirectory(fullPathToPublishedOAPPFile, fullInstallPath, Encoding.Default, true);
                OASISResult<IOAPPDNA> OAPPDNAResult = ReadOAPPDNA(fullInstallPath);

                if (OAPPDNAResult != null && OAPPDNAResult.Result != null && !OAPPDNAResult.IsError)
                {
                    OASISResult<IAvatar> avatarResult = AvatarManager.Instance.LoadAvatar(avatarId, false, true, providerType);

                    if (avatarResult != null && !avatarResult.IsError && avatarResult.Result != null)
                    {
                        InstalledOAPP installedOAPP = new InstalledOAPP()
                        {
                            OAPPId = OAPPDNAResult.Result.OAPPId,
                            OAPPDNA = OAPPDNAResult.Result,
                            InstalledBy = avatarId,
                            InstalledByAvatarUsername = avatarResult.Result.Username,
                            InstalledOn = DateTime.Now,
                            InstalledPath = fullInstallPath
                        };

                        OASISResult<IHolon> saveResult = installedOAPP.Save();

                        if (saveResult != null && saveResult.Result != null && !saveResult.IsError)
                        {
                            result.Message = "OAPP Installed";
                            result.Result = installedOAPP;
                        }
                        else
                            OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Save method. Reason: {saveResult.Message}");
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadAvatar method. Reason: {avatarResult.Message}");
                }
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} {ex}");
            }

            return result;
        }

        public async Task<OASISResult<IInstalledOAPP>> InstallOAPPAsync(Guid avatarId, IOAPP OAPP, string fullInstallPath, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.InstallOAPPAsync. Reason: ";

            try
            {
                string OAPPPath = Path.Combine("temp", OAPP.Name, ".oapp");
                await File.WriteAllBytesAsync(OAPPPath, OAPP.PublishedOAPP);
                result = await InstallOAPPAsync(avatarId, OAPPPath, fullInstallPath, providerType);
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} {ex}");
            }

            return result;
        }

        public OASISResult<IInstalledOAPP> InstallOAPP(Guid avatarId, IOAPP OAPP, string fullInstallPath, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.InstallOAPP. Reason: ";

            try
            {
                string OAPPPath = Path.Combine("temp", OAPP.Name, ".oapp");
                File.WriteAllBytes(OAPPPath, OAPP.PublishedOAPP);
                result = InstallOAPP(avatarId, OAPPPath, fullInstallPath, providerType);
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} {ex}");
            }

            return result;
        }

        public async Task<OASISResult<IInstalledOAPP>> InstallOAPPAsync(Guid avatarId, Guid OAPPId, string fullInstallPath, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            OASISResult<IOAPP> OAPPResult = await LoadOAPPAsync(OAPPId, providerType);

            if (OAPPResult != null && !OAPPResult.IsError && OAPPResult.Result != null)
                result = await InstallOAPPAsync(avatarId, OAPPResult.Result, fullInstallPath, providerType);
            else
                OASISErrorHandling.HandleError(ref result, $"Error occured in OAPPManager.InstallOAPPAsync loading the OAPP with the LoadOAPPAsync method, reason: {result.Message}");

            return result;
        }

        public OASISResult<IInstalledOAPP> InstallOAPP(Guid avatarId, Guid OAPPId, string fullInstallPath, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            OASISResult<IOAPP> OAPPResult = LoadOAPP(OAPPId, providerType);

            if (OAPPResult != null && !OAPPResult.IsError && OAPPResult.Result != null)
                result = InstallOAPP(avatarId, OAPPResult.Result, fullInstallPath, providerType);
            else
                OASISErrorHandling.HandleError(ref result, $"Error occured in OAPPManager.InstallOAPP loading the OAPP with the LoadOAPP method, reason: {result.Message}");

            return result;
        }

        public async Task<OASISResult<IOAPPDNA>> UnInstallOAPPAsync(IOAPPDNA OAPP, Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            return await UnInstallOAPPAsync(OAPP.OAPPId, avatarId, providerType);
        }

        public OASISResult<IOAPPDNA> UnInstallOAPP(IOAPPDNA OAPP, Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            return UnInstallOAPP(OAPP.OAPPId, avatarId, providerType);
        }

        public async Task<OASISResult<IOAPPDNA>> UnInstallOAPPAsync(Guid OAPPId, Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            OASISResult<IEnumerable<InstalledOAPP>> intalledOAPPResult = await Data.LoadHolonsForParentAsync<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            string errorMessage = "Error occured in OAPPManager.UnInstallOAPPAsync. Reason: ";

            if (intalledOAPPResult != null && !intalledOAPPResult.IsError && intalledOAPPResult.Result != null)
            {
                InstalledOAPP installedOAPP = intalledOAPPResult.Result.FirstOrDefault(x => x.OAPPDNA.OAPPId == OAPPId);

                if (installedOAPP != null)
                {
                    OASISResult<IHolon> holonResult = await installedOAPP.DeleteAsync(false, providerType);

                    if (holonResult != null && !holonResult.IsError && holonResult.Result != null)
                    {
                        result.Message = "OAPP Uninstalled";
                        result.Result = installedOAPP.OAPPDNA;
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling DeleteAsync. Reason: {holonResult.Message}");
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} No installed OAPP was found for the Id {OAPPId}.");
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParentAsync. Reason: {intalledOAPPResult.Message}");
            
            return result;
        }

        public OASISResult<IOAPPDNA> UnInstallOAPP(Guid OAPPId, Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();
            OASISResult<IEnumerable<InstalledOAPP>> intalledOAPPResult = Data.LoadHolonsForParent<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            string errorMessage = "Error occured in OAPPManager.UnInstallOAPP. Reason: ";

            if (intalledOAPPResult != null && !intalledOAPPResult.IsError && intalledOAPPResult.Result != null)
            {
                InstalledOAPP installedOAPP = intalledOAPPResult.Result.FirstOrDefault(x => x.OAPPDNA.OAPPId == OAPPId);

                if (installedOAPP != null)
                {
                    OASISResult<IHolon> holonResult = installedOAPP.Delete(false, providerType);

                    if (holonResult != null && !holonResult.IsError && holonResult.Result != null)
                    {
                        result.Message = "OAPP Uninstalled";
                        result.Result = installedOAPP.OAPPDNA;
                    }
                    else
                        OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling Delete. Reason: {holonResult.Message}");
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} No installed OAPP was found for the Id {OAPPId}.");
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParent. Reason: {intalledOAPPResult.Message}");

            return result;
        }

        public async Task<OASISResult<IEnumerable<IInstalledOAPP>>> ListInstalledOAPPsAsync(Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IEnumerable<IInstalledOAPP>> result = new OASISResult<IEnumerable<IInstalledOAPP>>();
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = await Data.LoadHolonsForParentAsync<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            string errorMessage = "Error occured in OAPPManager.ListInstalledOAPPsAsync. Reason: ";

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
            {
                result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IEnumerable<InstalledOAPP>, IEnumerable<IInstalledOAPP>>(installedOAPPsResult);
                result.Result = Mapper.Convert<InstalledOAPP, IInstalledOAPP>(installedOAPPsResult.Result);
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParentAsync. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public OASISResult<IEnumerable<IInstalledOAPP>> ListInstalledOAPPs(Guid avatarId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IEnumerable<IInstalledOAPP>> result = new OASISResult<IEnumerable<IInstalledOAPP>>();
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = Data.LoadHolonsForParent<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            string errorMessage = "Error occured in OAPPManager.ListInstalledOAPPs. Reason: ";

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
            {
                result = OASISResultHelper.CopyOASISResultOnlyWithNoInnerResult<IEnumerable<InstalledOAPP>, IEnumerable<IInstalledOAPP>>(installedOAPPsResult);
                result.Result = Mapper.Convert<InstalledOAPP, IInstalledOAPP>(installedOAPPsResult.Result);
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParent. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public async Task<OASISResult<bool>> IsOAPPInstalledAsync(Guid avatarId, Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<bool> result = new OASISResult<bool>();
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = await Data.LoadHolonsForParentAsync<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            string errorMessage = "Error occured in OAPPManager.IsOAPPInstalledAsync. Reason: ";

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.Any(x => x.OAPPDNA.OAPPId == OAPPId);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParentAsync. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public OASISResult<bool> IsOAPPInstalled(Guid avatarId, Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<bool> result = new OASISResult<bool>();
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = Data.LoadHolonsForParent<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            string errorMessage = "Error occured in OAPPManager.IsOAPPInstalled. Reason: ";

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.Any(x => x.OAPPDNA.OAPPId == OAPPId);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParent. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public async Task<OASISResult<bool>> IsOAPPInstalledAsync(Guid avatarId, string OAPPName, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<bool> result = new OASISResult<bool>();
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = await Data.LoadHolonsForParentAsync<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            string errorMessage = "Error occured in OAPPManager.IsOAPPInstalledAsync. Reason: ";

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.Any(x => x.OAPPDNA.OAPPName == OAPPName);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParentAsync. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public OASISResult<bool> IsOAPPInstalled(Guid avatarId, string OAPPName, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<bool> result = new OASISResult<bool>();
            string errorMessage = "Error occured in OAPPManager.IsOAPPInstalled. Reason: ";
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = Data.LoadHolonsForParent<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);
            
            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.Any(x => x.OAPPDNA.OAPPName == OAPPName);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParent. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public async Task<OASISResult<IInstalledOAPP>> LoadInstalledOAPPAsync(Guid avatarId, Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadInstalledOAPPAsync. Reason: ";
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = await Data.LoadHolonsForParentAsync<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.FirstOrDefault(x => x.OAPPDNA.OAPPId == OAPPId);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParentAsync. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public OASISResult<IInstalledOAPP> LoadInstalledOAPP(Guid avatarId, Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadInstalledOAPP. Reason: ";
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = Data.LoadHolonsForParent<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.FirstOrDefault(x => x.OAPPDNA.OAPPId == OAPPId);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParent. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public async Task<OASISResult<IInstalledOAPP>> LoadInstalledOAPPAsync(Guid avatarId, string OAPPName, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadInstalledOAPPAsync. Reason: ";
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = await Data.LoadHolonsForParentAsync<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.FirstOrDefault(x => x.OAPPDNA.OAPPName == OAPPName);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParentAsync. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public OASISResult<IInstalledOAPP> LoadInstalledOAPP(Guid avatarId, string OAPPName, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            string errorMessage = "Error occured in OAPPManager.LoadInstalledOAPP. Reason: ";
            OASISResult<IEnumerable<InstalledOAPP>> installedOAPPsResult = Data.LoadHolonsForParent<InstalledOAPP>(avatarId, HolonType.InstalledOAPP, false, false, 0, true, false, 0, HolonType.All, 0, providerType);

            if (installedOAPPsResult != null && !installedOAPPsResult.IsError && installedOAPPsResult.Result != null)
                result.Result = installedOAPPsResult.Result.FirstOrDefault(x => x.OAPPDNA.OAPPName == OAPPName);
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error occured calling LoadHolonsForParent. Reason: {installedOAPPsResult.Message}");

            return result;
        }

        public async Task<OASISResult<IInstalledOAPP>> LaunchOAPPAsync(Guid avatarId, Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            result = await LoadInstalledOAPPAsync(avatarId, OAPPId);

            if (result != null && !result.IsError && result.Result != null)
            {
                //Process.Start("explorer.exe", Path.Combine(result.Result.InstalledPath, result.Result.OAPPDNA.LaunchTarget));
                Process.Start("dotnet.exe", Path.Combine(result.Result.InstalledPath, result.Result.OAPPDNA.LaunchTarget));
            }
            else
                OASISErrorHandling.HandleError(ref result, $"Error occured in OAPPManager.LaunchOAPPAsync loading the OAPP with the LoadInstalledOAPPAsync method, reason: {result.Message}");

            return result;
        }

        public OASISResult<IInstalledOAPP> LaunchOAPP(Guid avatarId, Guid OAPPId, ProviderType providerType = ProviderType.Default)
        {
            OASISResult<IInstalledOAPP> result = new OASISResult<IInstalledOAPP>();
            result = LoadInstalledOAPP(avatarId, OAPPId);

            if (result != null && !result.IsError && result.Result != null)
            {
                //Process.Start("explorer.exe", Path.Combine(result.Result.InstalledPath, result.Result.OAPPDNA.LaunchTarget));
                Process.Start("dotnet.exe", Path.Combine(result.Result.InstalledPath, result.Result.OAPPDNA.LaunchTarget));
            }
            else
                OASISErrorHandling.HandleError(ref result, $"Error occured in OAPPManager.LaunchOAPP loading the OAPP with the LoadInstalledOAPP method, reason: {result.Message}");

            return result;
        }


        //private IOAPPDNA ConvertOAPPToOAPPDNA(IOAPP OAPP)
        //{
        //    OAPPDNA OAPPDNA = new OAPPDNA()
        //    {
        //        CelestialBodyId = OAPP.CelestialBodyId,
        //        //CelestialBody = OAPP.CelestialBody,
        //        CelestialBodyName = OAPP.CelestialBody != null ? OAPP.CelestialBody.Name : "",
        //        CelestialBodyType = OAPP.CelestialBody != null ? OAPP.CelestialBody.HolonType : HolonType.None,
        //        CreatedByAvatarId = OAPP.CreatedByAvatarId,
        //        CreatedByAvatarUsername = OAPP.CreatedByAvatarUsername,
        //        CreatedOn = OAPP.CreatedDate,
        //        Description = OAPP.Description,
        //        GenesisType = OAPP.GenesisType,
        //        OAPPId = OAPP.Id,
        //        OAPPName = OAPP.Name,
        //        OAPPType = OAPP.OAPPType,
        //        PublishedByAvatarId = OAPP.PublishedByAvatarId,
        //        PublishedByAvatarUsername = OAPP.PublishedByAvatarUsername,
        //        PublishedOn = OAPP.PublishedOn,
        //        PublishedOnSTARNET = OAPP.PublishedOAPP != null,
        //        Version = OAPP.Version.ToString()
        //    };

        //    List<IZome> zomes = new List<IZome>();
        //    foreach (IHolon holon in OAPP.Children)
        //        zomes.Add((IZome)holon);

        //   //OAPPDNA.Zomes = zomes;
        //    return OAPPDNA;
        //}

        public async Task<OASISResult<bool>> WriteOAPPDNAAsync(IOAPPDNA OAPPDNA, string fullPathToOAPP)
        {
            OASISResult<bool> result = new OASISResult<bool>();

            try
            {
                JsonSerializerOptions options = new()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                await File.WriteAllTextAsync(Path.Combine(fullPathToOAPP, "OAPPDNA.json"), JsonSerializer.Serialize((OAPPDNA)OAPPDNA, options));
                result.Result = true;
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"An error occured writing the OAPPDNA in WriteOAPPDNAAsync: Reason: {ex.Message}");
            }

            return result;
        }

        public OASISResult<bool> WriteOAPPDNA(IOAPPDNA OAPPDNA, string fullPathToOAPP)
        {
            OASISResult<bool> result = new OASISResult<bool>();

            try
            {
                File.WriteAllText(Path.Combine(fullPathToOAPP, "OAPPDNA.json"), JsonSerializer.Serialize((OAPPDNA)OAPPDNA));
                result.Result = true;
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"An error occured writing the OAPPDNA in WriteOAPPDNA: Reason: {ex.Message}");
            }

            return result;
        }

        public async Task<OASISResult<IOAPPDNA>> ReadOAPPDNAAsync(string fullPathToOAPP)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();

            try
            {
                result.Result = JsonSerializer.Deserialize<OAPPDNA>(await File.ReadAllTextAsync(Path.Combine(fullPathToOAPP, "OAPPDNA.json")));
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"An error occured reading the OAPPDNA in ReadOAPPDNAAsync: Reason: {ex.Message}");
            }

            return result;
        }

        public OASISResult<IOAPPDNA> ReadOAPPDNA(string fullPathToOAPP)
        {
            OASISResult<IOAPPDNA> result = new OASISResult<IOAPPDNA>();

            try
            {
                result.Result = JsonSerializer.Deserialize<OAPPDNA>(File.ReadAllText(Path.Combine(fullPathToOAPP, "OAPPDNA.json")));
            }
            catch (Exception ex)
            {
                OASISErrorHandling.HandleError(ref result, $"An error occured reading the OAPPDNA in ReadOAPPDNA: Reason: {ex.Message}");
            }

            return result;
        }

        public async Task<OASISResult<IOAPP>> AddMissionToOAPPAsync(Guid OAPPId, IMission mission)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IOAPP> loadResult = await LoadOAPPAsync(OAPPId);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                loadResult.Result.Missions.Add(mission);
                result = await SaveOAPPAsync(loadResult.Result);
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.AddMissionToOAPPAsync calling LoadOAPPAsync. Reason: {loadResult.Message}");

            return result;
        }

        public OASISResult<IOAPP> AddMissionToOAPP(Guid OAPPId, IMission mission)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IOAPP> loadResult = LoadOAPP(OAPPId);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                loadResult.Result.Missions.Add(mission);
                result = SaveOAPP(loadResult.Result);
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.AddMissionToOAPP calling LoadOAPP. Reason: {loadResult.Message}");

            return result;
        }

        public async Task<OASISResult<IOAPP>> RemoveMissionFromOAPPAsync(Guid OAPPId, IMission mission)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IOAPP> loadResult = await LoadOAPPAsync(OAPPId);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                loadResult.Result.Missions.Remove(mission);
                result = await SaveOAPPAsync(loadResult.Result);
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.RemoveMissionFromOAPPAsync calling LoadOAPPAsync. Reason: {loadResult.Message}");

            return result;
        }

        public OASISResult<IOAPP> RemoveMissionFromOAPP(Guid OAPPId, IMission mission)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IOAPP> loadResult = LoadOAPP(OAPPId);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                loadResult.Result.Missions.Remove(mission);
                result = SaveOAPP(loadResult.Result);
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.RemoveMissionFromOAPP calling LoadOAPP. Reason: {loadResult.Message}");

            return result;
        }

        public async Task<OASISResult<IOAPP>> RemoveMissionFromOAPPAsync(Guid OAPPId, Guid missionId)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IOAPP> loadResult = await LoadOAPPAsync(OAPPId);
            string errorMessage = "An error occured in OAPPManager.RemoveMissionFromOAPPAsync. Reason:";

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                IMission mission = loadResult.Result.Missions.FirstOrDefault(x => x.Id == missionId);

                if (mission != null)
                {
                    loadResult.Result.Missions.Remove(mission);
                    result = await SaveOAPPAsync(loadResult.Result);
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error calling LoadOAPPAsync. Reason: {loadResult.Message}");
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} No mission could be found for the missionId {missionId}");

            return result;
        }

        public OASISResult<IOAPP> RemoveMissionFromOAPP(Guid OAPPId, Guid missionId)
        {
            OASISResult<IOAPP> result = new OASISResult<IOAPP>();
            OASISResult<IOAPP> loadResult = LoadOAPP(OAPPId);
            string errorMessage = "An error occured in OAPPManager.RemoveMissionFromOAPP. Reason:";

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                IMission mission = loadResult.Result.Missions.FirstOrDefault(x => x.Id == missionId);

                if (mission != null)
                {
                    loadResult.Result.Missions.Remove(mission);
                    result = SaveOAPP(loadResult.Result);
                }
                else
                    OASISErrorHandling.HandleError(ref result, $"{errorMessage} Error calling LoadOAPP. Reason: {loadResult.Message}");
            }
            else
                OASISErrorHandling.HandleError(ref result, $"{errorMessage} No mission could be found for the missionId {missionId}");

            return result;
        }

        public async Task<OASISResult<IList<IMission>>> GetOAPPMissionsAsync(Guid OAPPId)
        {
            OASISResult<IList<IMission>> result = new OASISResult<IList<IMission>>();
            OASISResult<IOAPP> loadResult = await LoadOAPPAsync(OAPPId);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                result.Result = loadResult.Result.Missions;
                result.IsLoaded = true;
                result.Message = "Missions Loaded Successfully";
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.GetOAPPMissions calling LoadOAPPAsync. Reason: {loadResult.Message}");

            return result;
        }

        public OASISResult<IList<IMission>> GetOAPPMissions(Guid OAPPId)
        {
            OASISResult<IList<IMission>> result = new OASISResult<IList<IMission>>();
            OASISResult<IOAPP> loadResult = LoadOAPP(OAPPId);

            if (loadResult != null && loadResult.Result != null && !loadResult.IsError)
            {
                result.Result = loadResult.Result.Missions;
                result.IsLoaded = true;
                result.Message = "Missions Loaded Successfully";
            }
            else
                OASISErrorHandling.HandleError(ref result, $"An error occured in OAPPManager.GetOAPPMissions calling LoadOAPP. Reason: {loadResult.Message}");

            return result;
        }
    }
}
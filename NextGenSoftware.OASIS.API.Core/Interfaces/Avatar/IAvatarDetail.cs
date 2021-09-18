﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Helpers;
using NextGenSoftware.OASIS.API.Core.Interfaces.STAR;
using NextGenSoftware.OASIS.API.Core.Objects;

namespace NextGenSoftware.OASIS.API.Core.Interfaces
{
    public interface IAvatarDetail
    {
        List<Achievement> Achievements { get; set; }
        string Address { get; set; }
        AvatarAttributes Attributes { get; set; }
        AvatarAura Aura { get; set; }
        EnumValue<AvatarType> AvatarType { get; set; }
        AvatarChakras Chakras { get; set; }
        string Country { get; set; }
        string County { get; set; }
        EnumValue<OASISType> CreatedOASISType { get; set; }
        Dictionary<DimensionLevel, Guid> DimensionLevelIds { get; set; }
        Dictionary<DimensionLevel, IHolon> DimensionLevels { get; set; }
        DateTime DOB { get; set; }
        string Email { get; set; }
        ConsoleColor FavouriteColour { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        List<GeneKey> GeneKeys { get; set; }
        List<AvatarGift> Gifts { get; set; }
        List<HeartRateEntry> HeartRateData { get; set; }
        HumanDesign HumanDesign { get; set; }
        string Image2D { get; set; }
        List<InventoryItem> Inventory { get; set; }
        int Karma { get; set; }
        List<KarmaAkashicRecord> KarmaAkashicRecords { get; set; }
        string Landline { get; set; }
        string LastName { get; set; }
        int Level { get; }
        string Mobile { get; set; }
        string Model3D { get; set; }
        string Name { get; }
        IOmiverse Omiverse { get; set; }
        string Password { get; set; }
        string Postcode { get; set; }
        Dictionary<ProviderType, string> ProviderPrivateKey { get; set; }
        Dictionary<ProviderType, string> ProviderPublicKey { get; set; }
        Dictionary<ProviderType, string> ProviderUsername { get; set; }
        Dictionary<ProviderType, string> ProviderWalletAddress { get; set; }
        AvatarSkills Skills { get; set; }
        List<Spell> Spells { get; set; }
        ConsoleColor STARCLIColour { get; set; }
        AvatarStats Stats { get; set; }
        AvatarSuperPowers SuperPowers { get; set; }
        string Title { get; set; }
        string Town { get; set; }
        string UmaJson { get; set; }
        string Username { get; set; }
        int XP { get; set; }

        bool HasHolonChanged(bool checkChildren = true);
        KarmaAkashicRecord KarmaEarnt(KarmaTypePositive karmaType, KarmaSourceType karmaSourceType, string karamSourceTitle, string karmaSourceDesc, string webLink = null, bool autoSave = true, int karmaOverride = 0);
        Task<KarmaAkashicRecord> KarmaEarntAsync(KarmaTypePositive karmaType, KarmaSourceType karmaSourceType, string karamSourceTitle, string karmaSourceDesc, string webLink = null, bool autoSave = true, int karmaOverride = 0);
        KarmaAkashicRecord KarmaLost(KarmaTypeNegative karmaType, KarmaSourceType karmaSourceType, string karamSourceTitle, string karmaSourceDesc, string webLink = null, bool autoSave = true, int karmaOverride = 0);
        Task<KarmaAkashicRecord> KarmaLostAsync(KarmaTypeNegative karmaType, KarmaSourceType karmaSourceType, string karamSourceTitle, string karmaSourceDesc, string webLink = null, bool autoSave = true, int karmaOverride = 0);
        IAvatarDetail Save();
        Task<IAvatarDetail> SaveAsync();
    }
}
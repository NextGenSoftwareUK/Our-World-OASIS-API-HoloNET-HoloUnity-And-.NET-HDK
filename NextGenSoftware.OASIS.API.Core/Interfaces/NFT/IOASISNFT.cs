﻿using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Helpers;
using NextGenSoftware.Utilities;
using System;
using System.Collections.Generic;

namespace NextGenSoftware.OASIS.API.Core.Interfaces.NFT
{
    public interface IOASISNFT
    {
        Guid Id { get; set; }
        Guid MintedByAvatarId { get; set; }
        DateTime MintedOn { get; set; }
        string MintedByAddress { get; set; }
        string Hash { get; set; }
        string URL { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        // Guid OffChainProviderHolonId { get; set; }
        decimal Price { get; set; }
        decimal Discount { get; set; }
        public byte[] Image { get; set; }
        public string ImageUrl { get; set; }
        byte[] Thumbnail { get; set; }
        string ThumbnailUrl { get; set; }
        //public string Token { get; set; } //TODO: Should be dervied from the OnChainProvider so may not need this?
        public string MemoText { get; set; }
        Dictionary<string, object> MetaData { get; set; }
        EnumValue<ProviderType> OffChainProvider { get; set; }
        EnumValue<ProviderType> OnChainProvider { get; set; }
    }
}
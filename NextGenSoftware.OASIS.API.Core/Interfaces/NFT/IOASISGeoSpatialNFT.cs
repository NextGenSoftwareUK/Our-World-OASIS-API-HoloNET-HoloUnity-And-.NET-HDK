﻿namespace NextGenSoftware.OASIS.API.Core.Interfaces.NFT
{
    public interface IOASISGeoSpatialNFT
    {
        long Lat { get; set; }
        long Long { get; set; }
        bool AllowOtherPlayersToAlsoCollect { get; set; }
        bool PermSpawn { get; set; }
        int GlobalSpawnQuantity { get; set; }
        int PlayerSpawnQuantity { get; set; }
    }
}
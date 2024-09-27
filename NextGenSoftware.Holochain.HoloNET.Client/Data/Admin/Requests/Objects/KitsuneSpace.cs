﻿namespace NextGenSoftware.Holochain.HoloNET.Client.Data.Admin.Requests.Objects
{
    public class KitsuneSpace
    {
        public byte[] Data { get; set; }

        public KitsuneSpace(byte[] data)
        {
            Data = data;
        }

        public byte[] GetData()
        {
            return Data;
        }
    }
}

//https://docs.rs/kitsune_p2p/0.2.1/kitsune_p2p/struct.KitsuneSpace.html
//pub struct KitsuneSpace(pub Vec<u8, Global>);
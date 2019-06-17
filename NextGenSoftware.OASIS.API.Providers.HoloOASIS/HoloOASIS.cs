//using NextGenSoftware.Holochain.HoloNET.Client;
//using NextGenSoftware.OASIS.API.Core;
//using System;
//using System.Collections.Generic;

//namespace NextGenSoftware.OASIS.API.HoloOASIS
//{
//    public class HoloOASIS : HoloNETClient, IOASISNET, IOASISSTORAGE 
//    {
//        public HoloOASIS(string holochainURI) : base(holochainURI)
//        {

//        }

// #region IOASISSTORAGE Implementation

//        public IProfile GetProfile(Guid id)
//        {
//            throw new NotImplementedException();
//        }

//        public bool SaveProfile(IProfile profile)
//        {
//            throw new NotImplementedException();
//        }

//        public bool AddKarmaToProfile(IProfile profile, int karma)
//        {
//            throw new NotImplementedException();
//        }

//        public bool RemoveKarmaFromProfile(IProfile profile, int karma)
//        {
//            throw new NotImplementedException();
//        }

//        #endregion

//        #region IOASISNET Implementation

//        public List<Holon> GetHolonsNearMe(HolonType type)
//        {
//            throw new NotImplementedException();
//        }

//        public List<Player> GetPlayersNearMe()
//        {
//            throw new NotImplementedException();
//        }

//        #endregion
//    }
//}

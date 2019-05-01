using System;
using System.Collections.Generic;
using System.Text;

namespace NextGenSoftware.OASIS.API.Core
{
    // This interface is responsbile for persisting data/state to storage, this could be a local DB or other local 
    // storage or through a distributed/decentralised provider such as IPFS or Holochain (these two implementations 
    // will be implemented soon (IPFSOASIS & HoloOASIS).
    public interface IOASISSTORAGE
    {
        IProfile GetProfile(Guid Id);
        bool SaveProfile(IProfile);
        bool AddKarmaToProfile(IProfile, int karma);
        bool RemoveKarmaFromProfile(IProfile, int karma);

        //TODO: Lots more to come! ;-)
    }
}

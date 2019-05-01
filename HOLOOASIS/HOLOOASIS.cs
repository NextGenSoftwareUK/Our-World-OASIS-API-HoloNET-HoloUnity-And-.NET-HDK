using NextGenSoftware.OASIS.API.Core;
using System;

namespace NextGenSoftware.OASIS.API.HoloOASIS
{
    public class HoloOASIS : HoloNET.HoloNET, IOASISNET, IOASISSTORAGE
    {
        public IProfile GetProfile(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool SaveProfile(IProfile )
        {
            throw new NotImplementedException();
        }
    }
}

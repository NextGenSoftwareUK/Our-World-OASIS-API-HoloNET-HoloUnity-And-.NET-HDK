using System;
using System.Collections.Generic;
using System.Text;

namespace NextGenSoftware.OASIS.API.Core
{
    public class ProfileManager
    {
        public IOASISSTORAGE OASISStorageProvider { get; set; }

        public ProfileManager(IOASISSTORAGE OASISStorageProvider)
        {
            this.OASISStorageProvider = OASISStorageProvider;
        }

        public ProfileManager()
        {
            //this.OASISStorageProvider = 
        }
    }
}

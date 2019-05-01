using System;

namespace NextGenSoftware.OASIS.API.Core
{
    public interface IProfile
    {
        string Title { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DOB { get; set; }
        string Address { get; set; }
        int Karma { get; }
        int Level { get; }

        bool AddKarma(int karmaToAdd);
        bool SubstractKarma(int karmaToRemove);
        bool Save();
    }
}

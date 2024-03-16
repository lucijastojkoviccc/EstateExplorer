namespace EstateExplorer.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string AdministrativniRadnik = "AdministrativniRadnik";
            public const string Kupac = "Kupac";
            public const string Investitor = "Investitor";
            public const string User = "User";
        }

        public static class Policies
        {
            public const string RequireAdministrator = "RequireAdministrator";
            public const string RequireAdministrativniRadnik = "RequireAdministrativniRadnik";
            public const string RequireKupac = "RequireKupac";
            public const string RequireInvestitor = "RequireInvestitor";
        }
    }
}

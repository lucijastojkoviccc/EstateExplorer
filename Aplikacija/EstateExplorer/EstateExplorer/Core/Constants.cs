namespace EstateExplorer.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string AdministrativniRadnik = "AdministrativniRadnik";
            public const string Kupac = "Kupac";
            public const string Investitor = "Investitor";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireAdministrativniRadnik = "RequireAdministrativniRadnik";
            public const string RequireKupac = "RequireKupac";
            public const string RequireInvestitor = "RequireInvestitor";
        }
    }
}

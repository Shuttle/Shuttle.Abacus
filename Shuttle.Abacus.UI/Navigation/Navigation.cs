namespace Abacus.UI
{
    public static class Navigation
    {
        public static class Keys
        {
            public static readonly string File = "File";
            public static readonly string Help = "Help";
        }

        public static class SharedKeys
        {
            public static readonly string List = "List";
            public static readonly string New = "New";
        }

        public static class FileKeys
        {
            public static readonly string Import = "FileImport";
            public static readonly string Export = "FileExport";
            public static readonly string Exit = "Exit";

            public static class ImportKeys
            {
                public static readonly string ImportSchedules = "ImportSchedules";
                public static readonly string ImportSecureComplexes = "ImportSecureComplexes";
                public static readonly string ImportAutoDealersClassification = "ImportAutoDealersClassification";
                public static readonly string ImportCarHireAreas = "ImportCarHireAreas";
                public static readonly string ImportCarGroup = "ImportCarGroup";
                public static readonly string ImportVESACompliancyList = "ImportVESACompliancyList";
                public static readonly string ImportArgumentDetails = "ImportArgumentDetails";
                public static readonly string ImportTransUnionData = "ImportTransUnionData";
                public static readonly string ImportProSourceData = "ImportProSourceData";
            }
        }

        public static class AdministrationKeys
        {
            public static readonly string Methods = "Methods";
            public static readonly string UsersAdd = "UsersAdd";
            public static readonly string UsersEdit = "UsersEdit";
            public static readonly string UsersDelete = "UsersDelete";
        }

        public static class HelpKeys
        {
            public static readonly string Manual = "Manual";
        }
    }
}

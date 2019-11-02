namespace BatiFren.Common.MyExtensionClasses
{
    public static class ExtensionClass
    {
        public static string TrimSlash(this string url)  //For Url /Index etc.
        {
            string a = "/";
            string url2 = a + url;
            return url2;
        }
    }
}

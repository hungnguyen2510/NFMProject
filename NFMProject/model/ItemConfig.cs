using System.Collections.Generic;

namespace NFM
{
    public class ItemConfig
    {

        public string token;
        public string pathFolderWatching;
    }

    public class Account
    {
        public List<ItemConfig> items { get; set; }
    }

    public class RootObject
    {
        public Account Account { get; set; }
    }
}

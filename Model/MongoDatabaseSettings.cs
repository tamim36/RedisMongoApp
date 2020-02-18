using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}

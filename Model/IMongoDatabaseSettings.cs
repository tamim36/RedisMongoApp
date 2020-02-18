using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IMongoDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace lume.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();



    }
}

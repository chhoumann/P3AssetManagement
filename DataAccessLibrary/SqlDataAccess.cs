using AssetManagement.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetManagement.DataAccessLibrary
{
    public sealed partial class SqlDataAccess
    {

        public IAssetDataAccess AssetDataAccess { get; }

        public SqlDataAccess(AssetContext db)
        {
            AssetDataAccess = new IAssetDataAccess(db);
        }
    }
}

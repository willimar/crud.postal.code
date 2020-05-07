using Microsoft.AspNetCore.Connections;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace postal.code.api.Context
{
    public class MongoClientFactory: MongoClient
    {
        public MongoClientFactory():base(connectionString: ConnectionFactory())
        {

        }

        private static string ConnectionFactory()
        {
            string connectionString;

            if (string.IsNullOrEmpty(Program.DataBaseUser))
            {
                connectionString = $"mongodb://{Program.DataBaseHost}:{Program.DataBasePort}/?readPreference=primary&appname=postal.code.api&ssl=false";
            }
            else
            {
                connectionString = $"mongodb+srv://{Program.DataBaseUser}:{Program.DataBasePws}@{Program.DataBaseHost}/{Program.DataBaseName}?retryWrites=true&w=majority";
                //connectionString = $"mongodb://{Program.DataBaseUser}:{Program.DataBasePws}@{Program.DataBaseHost}:{Program.DataBasePort}/?authSource={Program.DataBaseAuth}&readPreference=primary&appname=postal.code.api&ssl=false";
            }

            return connectionString;
        }
    }
}

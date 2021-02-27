using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iPassport.Infra.ExternalServices.StorageExternalServices
{
    public class StorageConfigurations
    {
        public string BucketName { get; set; }
        public string awsAccesskey { get; set; }
        public string awsSecret { get; set; }
    }
}

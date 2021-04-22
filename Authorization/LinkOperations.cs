using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkAggregator.Authorization
{
    public class Constants
    {
        public static readonly string DeleteOperationName = "Delete";
    }

    public static class LinkOperations
    {
        public static OperationAuthorizationRequirement Delete =
            new OperationAuthorizationRequirement() { Name = Constants.DeleteOperationName };
    }
}

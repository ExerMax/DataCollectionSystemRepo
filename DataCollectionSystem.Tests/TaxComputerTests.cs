using DataCollectionSystem.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DbAccess.Models;

namespace DataCollectionSystem.Tests
{
    public class TaxComputerTests
    {
        [Fact]
        public void IndexComputeResult()
        {
            TaxComputer tc = new TaxComputer(null);

            double nullRes = tc.Compute(null);

            Assert.Equal(nullRes, 0);
        }
    }
}

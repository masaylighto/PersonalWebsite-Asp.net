using Core.DataKit.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Extensions;

 static class ResultExtensions
{
    public static void FailTestIfError<DataType>(this Result<DataType> result)
    {
        if (result.ContainError())
        {
            Assert.Fail(result.GetErrorMessage());
        }
    }
}

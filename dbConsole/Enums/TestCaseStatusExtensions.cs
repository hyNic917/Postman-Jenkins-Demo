using System;
using System.Collections.Generic;
using System.Text;

namespace dbConsole.Enums
{
    public static class TestCaseStatusExtensions
    {
        public static string DisplayString(this TestCaseStatus status)
        {
            return status switch
            {
                TestCaseStatus.InProcess => "IN_PROCESS",
                TestCaseStatus.SuccessWithoutValidations => "SUCCESS_WO_VALIDATIONS",
                TestCaseStatus.ErrorInProgress => "ERROR_IN_PROGRESS",
                TestCaseStatus.SuccessWithValidations => "SUCCESS_W_VALIDATIONS",
                TestCaseStatus.Failure => "FAILURE",
                TestCaseStatus.CompleteWithErrors => "COMPLETE_WITH_ERRORS",
                _ => "UNINITIALIZED",
            };
        }
    }
}

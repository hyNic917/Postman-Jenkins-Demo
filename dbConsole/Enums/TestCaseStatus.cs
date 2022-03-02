using System;
using System.Collections.Generic;
using System.Text;

namespace dbConsole.Enums
{
    public enum TestCaseStatus
    {
        Uninitialized,
        InProcess,
        SuccessWithValidations,
        SuccessWithoutValidations,
        CompleteWithErrors,
        Failure,
        ErrorInProgress
    }
}

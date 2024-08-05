using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.DTO
{
    public enum MoovingType
    {
        Revenues = 1,
        Expenses = 2
    }

    public enum TableCode
    {
        UserTypes = 1,
        Cities = 2,
        Areas = 3,
        UrgencyDebt = 4,
        Status = 5,
        Users = 8,
        PayOption = 9,
    }

    public enum AnsOption
    {
        Yes = 1,
        No = 2,
        OtherOption = 3
    }

    public enum userTypeDTO {
        systemAdministrator = 1,
        lender = 2,
        user = 3,
        lendersManager = 4,
        userUnderLender = 5,
        presenceUser = 6,
    }

    public enum ActionOptions
    {
        update = 1,
        delete = 2,
        IsNotActive = 3,
    }
    
        
    
}

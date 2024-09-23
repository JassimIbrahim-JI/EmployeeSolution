namespace WASM.ApplicationStates
{
    public class AllSectionState
    {
        //Scope Action
        public Action? Action { get; set; }

        // General Department
        public bool ShowGeneralDepartment { get; set; }
        public void GeneralDepartmentClicked() 
        {
            ResetAllState();
            ShowGeneralDepartment = true;
            Action?.Invoke();
        }

        //Department
        public bool ShowDepartment { get; set; }
        public void DepartmentClicked()
        {
            ResetAllState();
            ShowDepartment = true;
            Action?.Invoke();
        }

        //Branch
        public bool ShowBranch { get; set; }
        public void BranchClicked()
        {
            ResetAllState();
            ShowBranch = true;
            Action?.Invoke();
        }

        //Country
        public bool ShowCountry { get; set; }
        public void CountryClicked()
        {
            ResetAllState();
            ShowCountry = true;
            Action?.Invoke();
        }


        //City
        public bool ShowCity { get; set; }
        public void CityClicked()
        {
            ResetAllState();
            ShowCity = true;
            Action?.Invoke();
        }

        //Town
        public bool ShowTown { get; set; }
        public void TownClicked()
        {
            ResetAllState();
            ShowTown = true;
            Action?.Invoke();
        }

        //Employee
        public bool ShowEmployee { get; set; }
        public void EmployeeClicked()
        {
            ResetAllState();
            ShowEmployee = true;
            Action?.Invoke();
        }

        //User
        public bool ShowUser { get; set; }
        public void UserClicked()
        {
            ResetAllState();
            ShowUser = true;
            Action?.Invoke();
        }
        public bool ShowHealth { get; set; }
        public void HealthClicked() 
        {
            ResetAllState();
            ShowHealth = true;
            Action?.Invoke();
        }
        public bool ShowOvertimeType { get; set; }
        public void OvertimeTypeClicked() 
        {
            ResetAllState();
            ShowOvertimeType = true;
            Action?.Invoke();
        }
        public bool ShowOvertime { get; set; }
        public void OvertimeClicked()
        {
            ResetAllState();
            ShowOvertime = true;
            Action?.Invoke();
        }
        public bool ShowVaction { get; set; }
        public void VactionClicked()
        {
            ResetAllState();
            ShowVaction = true;
            Action?.Invoke();
        }
        public bool ShowSanction { get; set; }
        public void SanctionClicked()
        {
            ResetAllState();
            ShowSanction = true;
            Action?.Invoke();
        }
        public bool ShowSanctionType { get; set; }
        public void SanctionTypeClicked()
        {
            ResetAllState();
            ShowSanctionType = true;
            Action?.Invoke();
        }
        public bool ShowVactionType { get; set; }
        public void VactionTypeClicked()
        {
            ResetAllState();
            ShowVactionType = true;
            Action?.Invoke();
        }

        public bool ShowUserProfile { get; set; }
        public void ProfileClicked()
        {
            ResetAllState();
            ShowUserProfile = true;
            Action?.Invoke();
        }

        public void ResetAllState() 
        {
            ShowGeneralDepartment = false;
            ShowDepartment = false;
            ShowBranch  = false;
            ShowCountry = false;
            ShowCity = false;
            ShowTown = false;
            ShowEmployee = false;
            ShowUser = false;
            ShowHealth = false;
            ShowOvertimeType = false;
            ShowOvertime = false;
            ShowVaction = false;
            ShowSanction = false;
            ShowVactionType = false;
            ShowSanctionType = false;
            ShowUserProfile = false;
         }



    }
}

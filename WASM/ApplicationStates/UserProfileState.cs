using BaseLibrary.Class.Entities;

namespace WASM.ApplicationStates
{
    public class UserProfileState
    {
        public Action? Action {  get; set; }
        public UserProfile userProfile { get; set; } = new();


        public void ProfileUpdated() => Action!.Invoke();
    }
}

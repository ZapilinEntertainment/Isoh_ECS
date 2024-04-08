namespace ZE.IsohECS
{
    public interface ISessionObject 
    {
        public void OnSessionStart();
        public void OnSessionEnd();
        public void OnPause();
        public void OnUnpause();
    }
}

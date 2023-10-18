using PROG301_Week6.Models;

namespace PROG301_Week6.Interfaces
{
    
    public interface IEngine
    {

        Engine Engine { get; set;}

        void StartEngine();
        void StopEngine();

        string getEngineStartedString();

    }

}
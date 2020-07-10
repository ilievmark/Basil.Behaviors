using System.Diagnostics;

namespace BehaviorsSample.Models
{
    public class InjectedModel
    {
        private InjectedModel m_model;
        
        public InjectedModel Model { get; }
        
        public InjectedModel() {}

        public InjectedModel(InjectedModel model)
        {
            Model = model;
            m_model = model;
        }

        public void MethodToRize()
        {
            Debug.WriteLine("Hello from there!");
        }
    }
}
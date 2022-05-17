using Rhino;
using System.ComponentModel;
using System.Windows.Input;

namespace Umi.UrbanLCA.Panel
{
    public class PanelViewModel : INotifyPropertyChanged
    {
        private double totalSelectedBuildingOpEnergy;
        private double totalSelectedBuildingEmEnergy;

        public PanelViewModel()
        {
            //RunExampleCommand = new RelayCommand(RunExampleRhinoCommand);

            PropertyChanged += (s, e) => { };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //public ICommand RunExampleCommand { get; }

        public double TotalSelectedBuildingOpEnergy
        {
            get => totalSelectedBuildingOpEnergy;
            set
            {
                totalSelectedBuildingOpEnergy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalSelectedBuildingOpEnergy)));
            }
        }

        public double TotalSelectedBuildingEmEnergy
        {
            get => totalSelectedBuildingEmEnergy;
            set
            {
                totalSelectedBuildingEmEnergy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalSelectedBuildingEmEnergy)));
            }
        }

        //private void RunExampleRhinoCommand()
        //{
        //    RhinoApp.RunScript("UmiExampleModuleCommand", echo: true);
        //}
    }
}

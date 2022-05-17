using Rhino;
using System.ComponentModel;
using System.Windows.Input;

namespace Umi.UrbanLCA.Panel
{
    public class PanelViewModel : INotifyPropertyChanged
    {
        private double totalSelectedBuildingOpEnergy;
        private double totalSelectedBuildingEmEnergy;
        private double totalSelectedBuildingEnergy;

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

        //For total energy
        public double TotalSelectedBuildingEnergy
        {
            get => totalSelectedBuildingEnergy;
            set
            {
                totalSelectedBuildingEnergy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalSelectedBuildingEnergy)));
            }
        }

        //private void RunExampleRhinoCommand()
        //{
        //    RhinoApp.RunScript("UmiExampleModuleCommand", echo: true);
        //}
    }
}

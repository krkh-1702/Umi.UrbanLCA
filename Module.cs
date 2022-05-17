using Rhino;
using Rhino.DocObjects;
using Rhino.PlugIns;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Controls;
using Umi.RhinoServices;
using Umi.RhinoServices.Buildings;
using Umi.RhinoServices.Context;
using Umi.RhinoServices.UmiEvents;

using Umi.UrbanLCA.Panel;
using Umi.UrbanLCA.Properties;

namespace Umi.UrbanLCA
{
    public class Module : UmiModule
    {
        private readonly PanelViewModel panelViewModel;
        private readonly Dictionary<Guid, int> selectedBuildingOccupancy;

        //adding new dict
        private readonly Dictionary<Guid, double> selectedOpEnergy;
        private readonly Dictionary<Guid, double> selectedEmEnergy;
        //private readonly Dictionary<Guid, int> selectedTotalEnergy;

        public Module()
        {
            panelViewModel = new();
            selectedBuildingOccupancy = new();

            //energy components for Urban LCA
            selectedOpEnergy = new();
            selectedEmEnergy = new();
      

            ModuleControl = new PanelControl { DataContext = panelViewModel };
        }

        protected override UserControl ModuleControl { get; }

        protected override Tuple<Bitmap, ImageFormat> TabHeaderIcon => Tuple.Create(Resources.PanelIcon, ImageFormat.Png);

        //Chaneging the name
        protected override string TabHeaderToolTip => "UrbanLCA";

        protected override Color? Falsecolor(IUmiBuilding building)
        {
            return Color.Black;
        }

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            UmiEventSource.Instance.ProjectOpened += OnProjectOpened;
            UmiEventSource.Instance.ProjectClosed += OnProjectClosed;

            return base.OnLoad(ref errorMessage);
        }

        private void AddBuildingsToSelection(IEnumerable<RhinoObject> selectedRhinoObjects)
        {
            if (UmiContext.Current == null)
            {
                return;
            }

            var allResults = UmiContext.Current.GetObjects().ToDictionary(building => Guid.Parse(building.Id));


            foreach (var umiBuilding in UmiContext.Current.Buildings.ForObjects(selectedRhinoObjects))
            {

                //selectedBuildingOccupancy[umiBuilding.Id] = umiBuilding.Occupancy ?? 0;

                //retrieve UMI results objects from other modules by calling the UmiContext.GetObjects method?
                //Operator '??' cannot be applied to operands of type 'string' and 'int'
                //EnergySimulator is for new simulations 
                //selectedOpEnergy[umiBuilding.Id] = allResults[umiBuilding.Id].Data["SDL/Total Operational Energy"].Data.Sum();
                //selectedEmEnergy[umiBuilding.Id] = allResults[umiBuilding.Id].Data["SDL/Building Embodied CO2"].Data[1];

                if (allResults.TryGetValue(umiBuilding.Id, out var umiObject))
                {
                    selectedOpEnergy[umiBuilding.Id] = umiObject.Data["SDL/Total Operational Energy"].Data.Sum();
                    selectedEmEnergy[umiBuilding.Id] = umiObject.Data["SDL/Building Embodied CO2"].Data[1];
                }
            }
        }

        private void OnDeselectAllObjects(object sender, RhinoDeselectAllObjectsEventArgs e)
        {
            selectedBuildingOccupancy.Clear();

            panelViewModel.TotalSelectedBuildingOpEnergy = 0;

            //rechanging the values to 0
            selectedOpEnergy.Clear();
            selectedEmEnergy.Clear();
            

            
        }

        private void OnSelectionChanged(object sender, RhinoObjectSelectionEventArgs e)
        {
            if (e.Selected)
            {
                AddBuildingsToSelection(e.RhinoObjects);
            }
            else
            {
                RemoveBuildingsFromSelection(e.RhinoObjects);
            }

            panelViewModel.TotalSelectedBuildingOpEnergy = selectedOpEnergy.Values.Sum();
            panelViewModel.TotalSelectedBuildingEmEnergy = selectedEmEnergy.Values.Sum();
        }

        private void RemoveBuildingsFromSelection(IEnumerable<RhinoObject> deselectedRhinoObjects)
        {
            foreach (var rhinoObject in deselectedRhinoObjects)
            {
                selectedBuildingOccupancy.Remove(rhinoObject.Id);
            }
        }

        private void OnProjectOpened(object sender, UmiContext newProjectContext)
        {
            RhinoDoc.DeselectAllObjects += OnDeselectAllObjects;
            RhinoDoc.DeselectObjects += OnSelectionChanged;
            RhinoDoc.SelectObjects += OnSelectionChanged;

            selectedBuildingOccupancy.Clear();

            AddBuildingsToSelection(RhinoDoc.ActiveDoc.Objects.GetSelectedObjects(includeLights: false, includeGrips: false));
        }

        private void OnProjectClosed(object sender, object e)
        {
            RhinoDoc.SelectObjects -= OnSelectionChanged;
            RhinoDoc.DeselectObjects -= OnSelectionChanged;
            RhinoDoc.DeselectAllObjects -= OnDeselectAllObjects;
        }
    }
}

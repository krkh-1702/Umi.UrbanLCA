using Rhino;
using Rhino.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Umi.Core;
using Umi.RhinoServices;
using Umi.RhinoServices.Context;

namespace Umi.UrbanLCA.RhinoCommands
{
    [Guid("db25c9ad-b486-4d52-9c60-9debffe498dd")]
    public class UmiUrbanLCA : UmiCommand
    {
        public override string EnglishName => nameof(UmiUrbanLCA);

        public override Result Run(RhinoDoc doc, UmiContext context, RunMode mode)
        {
            //Create a variable that stores Operational Energy (year 1)
            //Create a variable that stores Embodied Energy (year 1)
            var databaseObjects = new List<IUmiObject>();

            foreach (var building in context.Buildings.All)
            {
                if (building.TemplateName == null)
                {
                    continue;
                }

                //EDIT THIS TO INCLUDE THE OPERATIONAL ENERGY VALUE FOR EACH BUILDING
                var opEnergy = building.GrossFloorArea;

                //EDIT THIS TO INCLUDE THE EMBODIED ENERGY VALUE FOR EACH BUILDING
                var emEnergy = building.FloorCount;

                RhinoApp.WriteLine($"{building.Name}: {opEnergy}, {emEnergy}");

                //UmiDataSeries 
                var databaseSeries = new UmiDataSeries();
                databaseSeries.Name = "life cycle assessment";
                databaseSeries.Units = "kgCO2e";
                //If there is no occupancy, set default value as 0
                databaseSeries.Data = new List<double>() { (opEnergy+emEnergy) ?? 0 };

                //ID - Rhino Id - convert to string for UMI to identify
                //For each building, we create a databaseSeries, add occupancy in persons and add it to databaseObjects list
                var databaseObject = UmiObject.Create(building.Name, building.Id.ToString(), databaseSeries);

                databaseObjects.Add(databaseObject);
                
            }
            //Storing objects to context
            context.StoreObjects(databaseObjects);

            return Result.Success;
        }
    }
}

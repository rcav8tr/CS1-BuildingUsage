﻿using ColossalFramework;
using UnityEngine;
using System;

namespace BuildingUsage
{
    /// <summary>
    /// a panel to display vehicle usage
    /// </summary>
    public class VehiclesUsagePanel : UsagePanel
    {
        /// <summary>
        /// Start is called once after the panel is created
        /// set up and populate the panel with UI components
        /// </summary>
        public override void Start()
        {
            // do base processing
            base.Start();

            try
            {
                // set the panel name
                name = GetType().ToString();

                // although IT Cluster (i.e. High Tech) office buildings produce goods, such goods are not delivered by vehicle (e.g. cargo truck), so OfficeBuildingAI is not included

                // create the usage groups, one for each vehicle usage type
                CreateUsageGroup<IndustrialBuildingAI, IndustrialExtractorAI, LivestockExtractorAI>(UsageType.VehiclesIndustrialTrucks);    // includes PloppableRICO
                CreateUsageGroup<MaintenanceDepotAI, SnowDumpAI                 >(UsageType.VehiclesMaintenanceTrucks);
                // WaterFacilityAI is in the base game, but VehiclesVacuumTrucks is introduced in NaturalDisastersDLC
                if (SteamHelper.IsDLCOwned(SteamHelper.DLC.NaturalDisastersDLC))
                {
                    CreateUsageGroup<WaterFacilityAI                            >(UsageType.VehiclesVacuumTrucks);
                }
                CreateUsageGroup<LandfillSiteAI, UltimateRecyclingPlantAI       >(UsageType.VehiclesGarbageTrucks);
                CreateUsageGroup<ExtractingFacilityAI, FishingHarborAI, FishFarmAI, ProcessingFacilityAI, UniqueFactoryAI, WarehouseAI>(UsageType.VehiclesIndustryVehicles);
                CreateUsageGroup<HospitalAI, MedicalCenterAI                    >(UsageType.VehiclesAmbulances);
                CreateUsageGroup<HelicopterDepotAI                              >(UsageType.VehiclesMedicalHelis);
                CreateUsageGroup<CemeteryAI                                     >(UsageType.VehiclesHearses);
                CreateUsageGroup<FireStationAI                                  >(UsageType.VehiclesFireEngines);
                CreateUsageGroup<HelicopterDepotAI                              >(UsageType.VehiclesFireHelis);
                CreateUsageGroup<DisasterResponseBuildingAI                     >(UsageType.VehiclesDisasterVehicles);
                CreateUsageGroup<ShelterAI                                      >(UsageType.VehiclesEvacuationBuses);
                CreateUsageGroup<PoliceStationAI                                >(UsageType.VehiclesPoliceCars);
                CreateUsageGroup<HelicopterDepotAI                              >(UsageType.VehiclesPoliceHelis);
                // PoliceStationAI is in the base game, but VehiclesPrisonVans is introduced in AfterDarkDLC
                if (SteamHelper.IsDLCOwned(SteamHelper.DLC.AfterDarkDLC))
                {
                    CreateUsageGroup<PoliceStationAI                            >(UsageType.VehiclesPrisonVans);
                }
                CreateUsageGroup<PostOfficeAI                                   >(UsageType.VehiclesPostVansTrucks);
                CreateUsageGroup<PrivateAirportAI                               >(UsageType.VehiclesPrivatePlanes);
                // there are many MonumentAI buildings, but only the ChirpX Launch Site should be included
                if (ChirpXLaunchSiteAvailable())
                {
                    CreateUsageGroup<MonumentAI                                 >(UsageType.VehiclesRockets);
                }
                CreateUsageGroup                                                 (UsageType.VehiclesTransportation);     // too many building AIs to list

                // add detail panels
                AddDetailPanel<VehiclesIndustryUsagePanel      >(UsageType.VehiclesIndustryVehicles, this);
                AddDetailPanel<VehiclesTransportationUsagePanel>(UsageType.VehiclesTransportation,   this);

                // associate each building AI type with its usage type(s) and usage count routine(s)
                // associate building AIs even if corresponding DLC is not installed (there will simply be no buildings with that AI)
                AssociateBuildingAI<IndustrialBuildingAI                 >(UsageType.VehiclesIndustrialTrucks,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI("PloppableRICO.GrowableIndustrialAI",  UsageType.VehiclesIndustrialTrucks,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI("PloppableRICO.PloppableIndustrialAI", UsageType.VehiclesIndustrialTrucks,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation(buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<IndustrialExtractorAI                >(UsageType.VehiclesIndustrialTrucks,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<LivestockExtractorAI                 >(UsageType.VehiclesIndustrialTrucks,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI("PloppableRICO.GrowableExtractorAI",   UsageType.VehiclesIndustrialTrucks,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation(buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI("PloppableRICO.PloppableExtractorAI",  UsageType.VehiclesIndustrialTrucks,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation(buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<MaintenanceDepotAI                   >(UsageType.VehiclesMaintenanceTrucks, (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesMaintenanceDepot                        (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<SnowDumpAI                           >(UsageType.VehiclesMaintenanceTrucks, (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesSnowDump                                (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<WaterFacilityAI                      >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesWaterFacility                           (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<LandfillSiteAI                       >(UsageType.VehiclesGarbageTrucks,     (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesLandfillSite<LandfillSiteAI>            (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<UltimateRecyclingPlantAI             >(UsageType.VehiclesGarbageTrucks,     (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesLandfillSite<UltimateRecyclingPlantAI>  (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<ExtractingFacilityAI                 >(UsageType.VehiclesIndustryVehicles,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesExtractingFacility                      (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<FishingHarborAI                      >(UsageType.VehiclesIndustryVehicles,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesFishingHarbor                           (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<FishFarmAI                           >(UsageType.VehiclesIndustryVehicles,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesFishFarm                                (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<ProcessingFacilityAI                 >(UsageType.VehiclesIndustryVehicles,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesProcessingFacility<ProcessingFacilityAI>(buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<UniqueFactoryAI                      >(UsageType.VehiclesIndustryVehicles,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesProcessingFacility<UniqueFactoryAI>     (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<WarehouseAI                          >(UsageType.VehiclesIndustryVehicles,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesWarehouse                               (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<HospitalAI                           >(UsageType.VehiclesAmbulances,        (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesHospital<HospitalAI>                    (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<MedicalCenterAI                      >(UsageType.VehiclesAmbulances,        (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesHospital<MedicalCenterAI>               (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<HelicopterDepotAI                    >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesHelicopterDepot                         (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<CemeteryAI                           >(UsageType.VehiclesHearses,           (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesCemetery                                (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<FireStationAI                        >(UsageType.VehiclesFireEngines,       (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesFireStation                             (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<DisasterResponseBuildingAI           >(UsageType.VehiclesDisasterVehicles,  (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesDisasterResponseBuilding                (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<ShelterAI                            >(UsageType.VehiclesEvacuationBuses,   (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesShelter                                 (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<PoliceStationAI                      >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesPoliceStation                           (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<PostOfficeAI                         >(UsageType.VehiclesPostVansTrucks,    (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesPostVansTrucks                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<PrivateAirportAI                     >(UsageType.VehiclesPrivatePlanes,     (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesPrivatePlanes                           (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<MonumentAI                           >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesChirpXLaunchSite                        (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<CargoStationAI                       >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<CargoHarborAI                        >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<DepotAI                              >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<CableCarStationAI                    >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<TransportStationAI                   >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<HarborAI                             >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));
                AssociateBuildingAI<TourBuildingAI                       >(UsageType.UseLogic1,                 (ushort buildingID, ref Building data, ref int used, ref int allowed) => GetUsageCountVehiclesTransportation                          (buildingID, ref data, ref used, ref allowed));

                // associate each vehicle AI type with its usage type
                // associate all vehicle AIs even if corresponding DLC is not installed (there will simply be no vehicles with that AI)
                AssociateVehicleAI<MaintenanceTruckAI       >(UsageType.VehiclesMaintenanceTrucks);
                AssociateVehicleAI<SnowTruckAI              >(UsageType.VehiclesMaintenanceTrucks);
                AssociateVehicleAI<ParkMaintenanceVehicleAI >(UsageType.VehiclesMaintenanceTrucks);
                AssociateVehicleAI<WaterTruckAI             >(UsageType.VehiclesVacuumTrucks);
                AssociateVehicleAI<GarbageTruckAI           >(UsageType.VehiclesGarbageTrucks);
                AssociateVehicleAI<CargoTruckAI             >(UsageType.UseLogic1);
                AssociateVehicleAI<FishingBoatAI            >(UsageType.VehiclesIndustryVehicles);
                AssociateVehicleAI<AmbulanceAI              >(UsageType.VehiclesAmbulances);
                AssociateVehicleAI<AmbulanceCopterAI        >(UsageType.VehiclesMedicalHelis);
                AssociateVehicleAI<HearseAI                 >(UsageType.VehiclesHearses);
                AssociateVehicleAI<FireTruckAI              >(UsageType.VehiclesFireEngines);
                AssociateVehicleAI<FireCopterAI             >(UsageType.VehiclesFireHelis);
                AssociateVehicleAI<DisasterResponseVehicleAI>(UsageType.VehiclesDisasterVehicles);
                AssociateVehicleAI<DisasterResponseCopterAI >(UsageType.VehiclesDisasterVehicles);
                AssociateVehicleAI<BusAI                    >(UsageType.UseLogic1);
                AssociateVehicleAI<PoliceCarAI              >(UsageType.UseLogic1);
                AssociateVehicleAI<PoliceCopterAI           >(UsageType.VehiclesPoliceHelis);
                AssociateVehicleAI<PostVanAI                >(UsageType.VehiclesPostVansTrucks);
                AssociateVehicleAI<PrivatePlaneAI           >(UsageType.VehiclesPrivatePlanes);
                AssociateVehicleAI<RocketAI                 >(UsageType.VehiclesRockets);
                AssociateVehicleAI<TrolleybusAI             >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<TramAI                   >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<MetroTrainAI             >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<PassengerFerryAI         >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<PassengerHelicopterAI    >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<PassengerBlimpAI         >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<CableCarAI               >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<TaxiAI                   >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<BalloonAI                >(UsageType.VehiclesTransportation);
                AssociateVehicleAI<PassengerTrainAI         >(UsageType.UseLogic1);
                AssociateVehicleAI<PassengerShipAI          >(UsageType.UseLogic1);
                AssociateVehicleAI<PassengerPlaneAI         >(UsageType.UseLogic1);
                AssociateVehicleAI<CargoTrainAI             >(UsageType.UseLogic1);
                AssociateVehicleAI<CargoShipAI              >(UsageType.UseLogic1);
                AssociateVehicleAI<CargoPlaneAI             >(UsageType.UseLogic1);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        /// <summary>
        /// get the usage type for a building when logic 1 is required
        /// </summary>
        protected override UsageType GetUsageType1ForBuilding(ushort buildingID, ref Building data)
        {
            // logic depends on building AI type
            Type buildingAIType = data.Info.m_buildingAI.GetType();
            if (buildingAIType == typeof(PoliceStationAI))
            {
                // logic adapted from PoliceStationAI.GetLocalizedStats
                if (data.Info.m_class.m_level >= ItemClass.Level.Level4)
                {
                    // building is prison
                    return UsageType.VehiclesPrisonVans;
                }
                else
                {
                    // building is police station
                    return UsageType.VehiclesPoliceCars;
                }
            }
            else if (buildingAIType == typeof(WaterFacilityAI))
            {
                // logic adapted from WaterFacilityAI.GetLocalizedStats
                WaterFacilityAI buildingAI = data.Info.m_buildingAI as WaterFacilityAI;
                if (buildingAI.m_sewageOutlet != 0 && buildingAI.m_sewageStorage != 0 && buildingAI.m_pumpingVehicles != 0)
                {
                    // building is Pumping Service
                    return UsageType.VehiclesVacuumTrucks;
                }
                else
                {
                    // other water building with no vehicles
                    return UsageType.None;
                }
            }
            else if (buildingAIType == typeof(HelicopterDepotAI))
            {
                // convert building service to usage type
                if (data.Info.m_class.m_service == ItemClass.Service.HealthCare)
                {
                    return UsageType.VehiclesMedicalHelis;
                }
                if (data.Info.m_class.m_service == ItemClass.Service.FireDepartment)
                {
                    return UsageType.VehiclesFireHelis;
                }
                if (data.Info.m_class.m_service == ItemClass.Service.PoliceDepartment)
                {
                    return UsageType.VehiclesPoliceHelis;
                }
            }
            else if (buildingAIType == typeof(MonumentAI))
            {
                if (ChirpXLaunchSiteAvailable())
                {
                    return UsageType.VehiclesRockets;
                }
                else
                {
                    return UsageType.None;
                }
            }
            else if (buildingAIType == typeof(CargoStationAI    ) ||
                     buildingAIType == typeof(CargoHarborAI     ) ||
                     buildingAIType == typeof(DepotAI           ) ||
                     buildingAIType == typeof(CableCarStationAI ) ||
                     buildingAIType == typeof(TransportStationAI) ||
                     buildingAIType == typeof(HarborAI          ) ||
                     buildingAIType == typeof(TourBuildingAI    ))
            {
                if (GetVehiclesTransportationUsageType(data.Info) == UsageType.None)
                {
                    return UsageType.None;
                }
                else
                {
                    return UsageType.VehiclesTransportation;
                }
            }

            // usage type not determined with above logic
            Debug.LogError($"Unhandled building AI type [{buildingAIType.ToString()}] when getting usage type with logic.");
            return UsageType.None;
        }

        /// <summary>
        /// get the usage type for a building when logic 2 is required
        /// </summary>
        protected override UsageType GetUsageType2ForBuilding(ushort buildingID, ref Building data)
        {
            // usage type not determined with above logic
            Type buildingAIType = data.Info.m_buildingAI.GetType();
            Debug.LogError($"Unhandled building AI type [{buildingAIType.ToString()}] when getting usage type with logic.");
            return UsageType.None;
        }

        /// <summary>
        /// get the usage type for a vehicle when logic is required
        /// </summary>
        protected override UsageType GetUsageTypeForVehicle(ushort vehicleID, ref Vehicle data)
        {
            // logic depends on vehicle AI type
            Type vehicleAIType = data.Info.m_vehicleAI.GetType();
            if (vehicleAIType == typeof(CargoTruckAI))
            {
                // get first vehicle (e.g. engine on a train)
                ushort firstVehicle = data.GetFirstVehicle(vehicleID);
                if (firstVehicle == 0)
                {
                    firstVehicle = vehicleID;
                }

                // get source building type
                Vehicle firstVehicleData = Singleton<VehicleManager>.instance.m_vehicles.m_buffer[firstVehicle];
                ushort buildingID = firstVehicleData.m_sourceBuilding;
                Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID];

                // usage type determined by looking at building AI type of the source building
                Type buildingAIType = buildingData.Info.m_buildingAI.GetType();
                if (buildingAIType == typeof(OutsideConnectionAI))
                {
                    // ignore vehicle from outside connection
                    return UsageType.None;
                }
                else if (buildingAIType == typeof(ExtractingFacilityAI  ) ||
                         buildingAIType == typeof(FishingHarborAI       ) ||
                         buildingAIType == typeof(ProcessingFacilityAI  ) ||
                         buildingAIType == typeof(UniqueFactoryAI       ) ||
                         buildingAIType == typeof(WarehouseAI           ))
                {
                    return UsageType.VehiclesIndustryVehicles;
                }
                else if (buildingAIType            == typeof(IndustrialBuildingAI         ) ||
                         buildingAIType.ToString() == "PloppableRICO.GrowableIndustrialAI"  ||
                         buildingAIType.ToString() == "PloppableRICO.PloppableIndustrialAI" ||
                         buildingAIType            == typeof(IndustrialExtractorAI        ) ||
                         buildingAIType            == typeof(LivestockExtractorAI         ) ||
                         buildingAIType.ToString() == "PloppableRICO.GrowableExtractorAI"   ||
                         buildingAIType.ToString() == "PloppableRICO.PloppableExtractorAI")
                {
                    return UsageType.VehiclesIndustrialTrucks;
                }
                else
                {
                    return UsageType.VehiclesTransportation;
                }
            }
            else if (vehicleAIType == typeof(PassengerTrainAI   ) ||
                     vehicleAIType == typeof(PassengerShipAI    ) ||
                     vehicleAIType == typeof(PassengerPlaneAI   ) || 
                     vehicleAIType == typeof(CargoTrainAI       ) ||
                     vehicleAIType == typeof(CargoShipAI        ) ||
                     vehicleAIType == typeof(CargoPlaneAI       ))
            {
                // get first vehicle (e.g. engine on a train)
                ushort firstVehicle = data.GetFirstVehicle(vehicleID);
                if (firstVehicle == 0)
                {
                    firstVehicle = vehicleID;
                }

                // get source building of first vehicle
                Vehicle firstVehicleData = Singleton<VehicleManager>.instance.m_vehicles.m_buffer[firstVehicle];
                ushort buildingID = firstVehicleData.m_sourceBuilding;
                Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID];
                Type buildingAIType = buildingData.Info.m_buildingAI.GetType();

                // ignore vehicle from outside connection
                if (buildingAIType == typeof(OutsideConnectionAI))
                {
                    return UsageType.None;
                }
                else
                {
                    return UsageType.VehiclesTransportation;
                }
            }
            else if (vehicleAIType == typeof(PoliceCarAI))
            {
                // logic adapted from PoliceCarAI.GetBufferStatus
                if (data.Info.m_class.m_level >= ItemClass.Level.Level4)
                {
                    return UsageType.VehiclesPrisonVans;
                }
                else
                {
                    return UsageType.VehiclesPoliceCars;
                }
            }
            else if (vehicleAIType == typeof(BusAI))
            {
                // get source building type
                ushort buildingID = data.m_sourceBuilding;
                Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID];
                Type buildingAIType = buildingData.Info.m_buildingAI.GetType();

                // ignore vehicle from outside connection
                if (buildingAIType == typeof(OutsideConnectionAI))
                {
                    return UsageType.None;
                }

                // logic determined by looking at buses with ModTools
                if (data.Info.m_class.m_service == ItemClass.Service.Disaster)
                {
                    return UsageType.VehiclesEvacuationBuses;
                }
                else
                {
                    // Bus Depot, Biofuel Bus Depot, Intercity Bus Station, Intercity Bus Terminal, Sightseeing Bus Depot
                    return UsageType.VehiclesTransportation;
                }
            }

            Debug.LogError($"Unhandled vehicle AI type [{vehicleAIType.ToString()}] when getting usage type with logic.");
            return UsageType.None;
        }

    }
}

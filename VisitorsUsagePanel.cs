﻿using System;

namespace BuildingUsage
{
    /// <summary>
    /// a panel to display visitor usage
    /// </summary>
    public class VisitorsUsagePanel : UsagePanel
    {
        /// <summary>
        /// the bottom position of the bottom-most UI element on this panel and any detail panels
        /// </summary>
        public float BottomPosition { get { return GetUsageGroupBottomPosition(); } }

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

                // create the usage groups
                CreateUsageGroup<MarketAI                                                                                   >(UsageType.VisitorsFishMarket);
                CreateUsageGroup<HospitalAI, ChildcareAI, EldercareAI, MedicalCenterAI                                      >(UsageType.VisitorsMedicalPatients);
                CreateUsageGroup<SaunaAI                                                                                    >(UsageType.VisitorsMedicalVisitors);
                CreateUsageGroup<CemeteryAI                                                                                 >(UsageType.VisitorsDeceased);
                CreateUsageGroup<ShelterAI                                                                                  >(UsageType.VisitorsShelter);
                CreateUsageGroup<PoliceStationAI                                                                            >(UsageType.VisitorsCriminals);
                CreateUsageGroup<SchoolAI, LibraryAI, CampusBuildingAI, UniqueFacultyAI, MuseumAI, VarsitySportsArenaAI     >(UsageType.VisitorsEducation);
                CreateUsageGroup<AirportAuxBuildingAI, AirportEntranceAI                                                    >(UsageType.VisitorsAirportArea);
                CreateUsageGroup<ParkAI, EdenProjectAI, ParkBuildingAI, IceCreamStandAI, TourBuildingAI, ChirperTourAI,
                                 HotelAI                                                                                    >(UsageType.VisitorsParksPlazas);
                CreateUsageGroup<MonumentAI, AirlineHeadquartersAI, AnimalMonumentAI, PrivateAirportAI, ChirpwickCastleAI,
                                 FestivalAreaAI, StockExchangeAI, InternationalTradeBuildingAI, LibraryAI, SpaceElevatorAI,
                                 CountdownAI, ParkAI                                                                        >(UsageType.VisitorsUnique);

                // add detail panels
                AddDetailPanel<VisitorsEducationUsagePanel  >(UsageType.VisitorsEducation  );
                AddDetailPanel<VisitorsParksPlazasUsagePanel>(UsageType.VisitorsParksPlazas);
                AddDetailPanel<VisitorsUniqueUsagePanel     >(UsageType.VisitorsUnique     );

                // associate each building AI type with its usage type(s) and usage count routine(s)
                // associate building AIs even if corresponding DLC is not installed (there will simply be no buildings with that AI)
                AssociateBuildingAI<MarketAI                        >(UsageType.VisitorsFishMarket,         GetUsageCountVisitorsMarket                                 );
                AssociateBuildingAI<HospitalAI                      >(UsageType.VisitorsMedicalPatients,    GetUsageCountVisitorsHospital<HospitalAI>                   );
                AssociateBuildingAI<MedicalCenterAI                 >(UsageType.VisitorsMedicalPatients,    GetUsageCountVisitorsHospital<MedicalCenterAI>              );
                AssociateBuildingAI<ChildcareAI                     >(UsageType.VisitorsMedicalPatients,    GetUsageCountVisitorsChildcare                              );
                AssociateBuildingAI<EldercareAI                     >(UsageType.VisitorsMedicalPatients,    GetUsageCountVisitorsEldercare                              );
                AssociateBuildingAI<SaunaAI                         >(UsageType.VisitorsMedicalVisitors,    GetUsageCountVisitorsSauna                                  );
                AssociateBuildingAI<CemeteryAI                      >(UsageType.VisitorsDeceased,           GetUsageCountVisitorsCemetery                               );
                AssociateBuildingAI<ShelterAI                       >(UsageType.VisitorsShelter,            GetUsageCountVisitorsShelter                                );
                AssociateBuildingAI<PoliceStationAI                 >(UsageType.VisitorsCriminals,          GetUsageCountVisitorsPoliceStation                          );
                AssociateBuildingAI<SchoolAI                        >(UsageType.VisitorsEducation,          GetUsageCountVisitorsSchool<SchoolAI>                       );
                AssociateBuildingAI<LibraryAI                       >(UsageType.UseLogic1,                  GetUsageCountVisitorsLibrary                                );
                AssociateBuildingAI<CampusBuildingAI                >(UsageType.VisitorsEducation,          GetUsageCountVisitorsSchool<CampusBuildingAI>               );
                AssociateBuildingAI<UniqueFacultyAI                 >(UsageType.VisitorsEducation,          GetUsageCountVisitorsSchool<UniqueFacultyAI>                );
                AssociateBuildingAI<MuseumAI                        >(UsageType.VisitorsEducation,          GetUsageCountVisitorsMonument<MuseumAI>                     );
                AssociateBuildingAI<VarsitySportsArenaAI            >(UsageType.VisitorsEducation,          GetUsageCountVisitorsVarsitySportsArena                     );
                AssociateBuildingAI<AirportAuxBuildingAI            >(UsageType.VisitorsAirportArea,        GetUsageCountVisitorsAirportArea<AirportAuxBuildingAI>      );
                AssociateBuildingAI<AirportEntranceAI               >(UsageType.VisitorsAirportArea,        GetUsageCountVisitorsAirportArea<AirportEntranceAI>         );
                AssociateBuildingAI<ParkAI                          >(UsageType.UseLogic1,                  GetUsageCountVisitorsPark<ParkAI>                           );
                AssociateBuildingAI<EdenProjectAI                   >(UsageType.VisitorsParksPlazas,        GetUsageCountVisitorsPark<EdenProjectAI>                    );
                AssociateBuildingAI<ParkBuildingAI                  >(UsageType.VisitorsParksPlazas,        GetUsageCountVisitorsParkBuilding<ParkBuildingAI>           );
                AssociateBuildingAI<IceCreamStandAI                 >(UsageType.VisitorsParksPlazas,        GetUsageCountVisitorsParkBuilding<IceCreamStandAI>          );
                AssociateBuildingAI<TourBuildingAI                  >(UsageType.VisitorsParksPlazas,        GetUsageCountVisitorsTourBuilding<TourBuildingAI>           );
                AssociateBuildingAI<ChirperTourAI                   >(UsageType.VisitorsParksPlazas,        GetUsageCountVisitorsTourBuilding<ChirperTourAI>            );
                AssociateBuildingAI<HotelAI                         >(UsageType.VisitorsParksPlazas,        GetUsageCountVisitorsHotel                                  );
                AssociateBuildingAI<MonumentAI                      >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<MonumentAI>                   );
                AssociateBuildingAI<AirlineHeadquartersAI           >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<AirlineHeadquartersAI>        );
                AssociateBuildingAI<AnimalMonumentAI                >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<AnimalMonumentAI>             );
                AssociateBuildingAI<PrivateAirportAI                >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<PrivateAirportAI>             );
                AssociateBuildingAI<ChirpwickCastleAI               >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<ChirpwickCastleAI>            );
                AssociateBuildingAI<CountdownAI                     >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<CountdownAI>                  );
                AssociateBuildingAI<FestivalAreaAI                  >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<FestivalAreaAI>               );
                AssociateBuildingAI<StockExchangeAI                 >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<StockExchangeAI>              );
                AssociateBuildingAI<InternationalTradeBuildingAI    >(UsageType.VisitorsUnique,             GetUsageCountVisitorsMonument<InternationalTradeBuildingAI> );
                AssociateBuildingAI<SpaceElevatorAI                 >(UsageType.UseLogic1,                  GetUsageCountVisitorsPlazaTransference                      );
            }
            catch (Exception ex)
            {
                LogUtil.LogException(ex);
            }
        }

        /// <summary>
        /// get the usage type for a building when logic 1 is required
        /// </summary>
        protected override UsageType GetUsageType1ForBuilding(ushort buildingID, ref Building data)
        {
            // logic depends on building AI type
            Type buildingAIType = data.Info.m_buildingAI.GetType();
            if (buildingAIType == typeof(LibraryAI))
            {
                // The Creator's Library from Treasure Hunt is Unique
                if (data.Info.m_isTreasure)
                {
                    return UsageType.VisitorsUnique;
                }
                else
                {
                    return UsageType.VisitorsEducation;
                }
            }
            if (buildingAIType == typeof(ParkAI))
            {
                // Plaza of the Future from Treasure Hunt is Unique
                if (data.Info.m_isTreasure)
                {
                    return UsageType.VisitorsUnique;
                }
                else
                {
                    return UsageType.VisitorsParksPlazas;
                }
            }
            if (buildingAIType == typeof(SpaceElevatorAI))
            {
                // Plaza of Transference from Treasure Hunt is Unique
                if (data.Info.m_isTreasure)
                {
                    return UsageType.VisitorsUnique;
                }
                else
                {
                    return UsageType.None;
                }
            }

            // usage type not determined with above logic
            LogUtil.LogError($"Unhandled building AI type [{buildingAIType}] when getting usage type with logic.");
            return UsageType.None;
        }

        /// <summary>
        /// get the usage type for a building when logic 2 is required
        /// </summary>
        protected override UsageType GetUsageType2ForBuilding(ushort buildingID, ref Building data)
        {
            // usage type not determined with above logic
            Type buildingAIType = data.Info.m_buildingAI.GetType();
            LogUtil.LogError($"Unhandled building AI type [{buildingAIType}] when getting usage type with logic.");
            return UsageType.None;
        }

        /// <summary>
        /// get the usage type for a vehicle when logic is required
        /// </summary>
        protected override UsageType GetUsageTypeForVehicle(ushort vehicleID, ref Vehicle data)
        {
            Type vehicleAIType = data.Info.m_vehicleAI.GetType();
            LogUtil.LogError($"Unhandled vehicle AI type [{vehicleAIType}] when getting usage type with logic.");
            return UsageType.None;
        }

    }
}

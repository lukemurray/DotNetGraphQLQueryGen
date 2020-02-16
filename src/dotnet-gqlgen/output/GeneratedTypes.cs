
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNetGqlClient;

/// <summary>
/// Generated interfaces for making GraphQL API calls with a typed interface.
///
/// Generated on 31/10/19 9:43:34 am from ../../../../../xysense/xynger/src/Web/XyAdmin/src/app/queryTypes/schema.graphql with arguments -n Generated -c GraphQLClient -m Date=DateTime?,Date!=DateTime,ID=Guid?,ID!=Guid
/// </summary>

namespace Generated
{

    public interface RootQuery
    {
        /// <summary>
        /// Return a Audit by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("audit")]
        Audit Audit();
        /// <summary>
        /// Return a Audit by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("audit")]
        TReturn Audit<TReturn>(Guid id, Expression<Func<Audit, TReturn>> selection);
        /// <summary>
        /// Return a list of audits filterable by table name andor customer ID
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("audits")]
        List<Audit> Audits();
        /// <summary>
        /// Return a list of audits filterable by table name andor customer ID
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("audits")]
        List<TReturn> Audits<TReturn>(string tableName, Guid? customerId, Expression<Func<Audit, TReturn>> selection);
        /// <summary>
        /// Return a CameraModule by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("cameraModule")]
        CameraModule CameraModule();
        /// <summary>
        /// Return a CameraModule by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("cameraModule")]
        TReturn CameraModule<TReturn>(Guid id, Expression<Func<CameraModule, TReturn>> selection);
        /// <summary>
        /// Return a list of active camera modules optionally limited andor filtered by serialNumber
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("cameraModules")]
        List<CameraModule> CameraModules();
        /// <summary>
        /// Return a list of active camera modules optionally limited andor filtered by serialNumber
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("cameraModules")]
        List<TReturn> CameraModules<TReturn>(string search, int? limit, Expression<Func<CameraModule, TReturn>> selection);
        /// <summary>
        /// Return a Customer by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// Return a Customer by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Guid id, Expression<Func<Customer, TReturn>> selection);
        /// <summary>
        /// Return a list of active Customers
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customers")]
        List<Customer> Customers();
        /// <summary>
        /// Return a list of active Customers
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customers")]
        List<TReturn> Customers<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("dateRangeQueryingEnabled")]
        bool DateRangeQueryingEnabled { get; }
        /// <summary>
        /// Return a DetexyBoard by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("detexyBoard")]
        DetexyBoard DetexyBoard();
        /// <summary>
        /// Return a DetexyBoard by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("detexyBoard")]
        TReturn DetexyBoard<TReturn>(Guid id, Expression<Func<DetexyBoard, TReturn>> selection);
        /// <summary>
        /// Return a list of active detexy boards optionally limited andor filtered by serialNumber
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("detexyBoards")]
        List<DetexyBoard> DetexyBoards();
        /// <summary>
        /// Return a list of active detexy boards optionally limited andor filtered by serialNumber
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("detexyBoards")]
        List<TReturn> DetexyBoards<TReturn>(string like, int? limit, Expression<Func<DetexyBoard, TReturn>> selection);
        /// <summary>
        /// Return a Floor by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floor")]
        Floor Floor();
        /// <summary>
        /// Return a Floor by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floor")]
        TReturn Floor<TReturn>(Guid id, Expression<Func<Floor, TReturn>> selection);
        /// <summary>
        /// Return a FloorPlan by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floorPlan")]
        FloorPlan FloorPlan();
        /// <summary>
        /// Return a FloorPlan by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floorPlan")]
        TReturn FloorPlan<TReturn>(Guid id, Expression<Func<FloorPlan, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floorPlans")]
        List<FloorPlan> FloorPlans();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floorPlans")]
        List<TReturn> FloorPlans<TReturn>(Expression<Func<FloorPlan, TReturn>> selection);
        /// <summary>
        /// Return a list of active floors optionally filtered by a EQL expression
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floors")]
        List<Floor> Floors();
        /// <summary>
        /// Return a list of active floors optionally filtered by a EQL expression
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floors")]
        List<TReturn> Floors<TReturn>(string filter, Expression<Func<Floor, TReturn>> selection);
        /// <summary>
        /// Return a FloorSpace by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floorSpace")]
        FloorSpace FloorSpace();
        /// <summary>
        /// Return a FloorSpace by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floorSpace")]
        TReturn FloorSpace<TReturn>(Guid id, Expression<Func<FloorSpace, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floorSpaces")]
        List<FloorSpace> FloorSpaces();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floorSpaces")]
        List<TReturn> FloorSpaces<TReturn>(Expression<Func<FloorSpace, TReturn>> selection);
        /// <summary>
        /// Return stats about number of hardware modules and installs
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("hardwareStats")]
        HardwareStats HardwareStats();
        /// <summary>
        /// Return stats about number of hardware modules and installs
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("hardwareStats")]
        TReturn HardwareStats<TReturn>(Expression<Func<HardwareStats, TReturn>> selection);
        /// <summary>
        /// Returns a LenCalibration object by ID or a match on the name argument
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("lensCalibration")]
        LensCalibration LensCalibration();
        /// <summary>
        /// Returns a LenCalibration object by ID or a match on the name argument
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("lensCalibration")]
        TReturn LensCalibration<TReturn>(string name, Guid? id, Expression<Func<LensCalibration, TReturn>> selection);
        /// <summary>
        /// Return a list of active lens calibrations, optionally filtered by a search on their name and limited by x results
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("lensCalibrations")]
        List<LensCalibration> LensCalibrations();
        /// <summary>
        /// Return a list of active lens calibrations, optionally filtered by a search on their name and limited by x results
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("lensCalibrations")]
        List<TReturn> LensCalibrations<TReturn>(string search, int? limit, Expression<Func<LensCalibration, TReturn>> selection);
        /// <summary>
        /// Return a Location by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("location")]
        Location Location();
        /// <summary>
        /// Return a Location by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("location")]
        TReturn Location<TReturn>(Guid id, Expression<Func<Location, TReturn>> selection);
        /// <summary>
        /// Return a list of active locations optionally filtered by name
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("locations")]
        List<Location> Locations();
        /// <summary>
        /// Return a list of active locations optionally filtered by name
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("locations")]
        List<TReturn> Locations<TReturn>(string nameLike, Expression<Func<Location, TReturn>> selection);
        /// <summary>
        /// Return a Quote by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("quote")]
        Quote Quote();
        /// <summary>
        /// Return a Quote by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("quote")]
        TReturn Quote<TReturn>(Guid id, Expression<Func<Quote, TReturn>> selection);
        /// <summary>
        /// Return a list of active Quotes Optionally filtered by a search string (searches Customer name and Quote name)
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("quotes")]
        List<Quote> Quotes();
        /// <summary>
        /// Return a list of active Quotes Optionally filtered by a search string (searches Customer name and Quote name)
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("quotes")]
        List<TReturn> Quotes<TReturn>(string search, Expression<Func<Quote, TReturn>> selection);
        /// <summary>
        /// Return a QuoteSensor by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("quoteSensor")]
        QuoteSensor QuoteSensor();
        /// <summary>
        /// Return a QuoteSensor by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("quoteSensor")]
        TReturn QuoteSensor<TReturn>(Guid id, Expression<Func<QuoteSensor, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("quoteSensors")]
        List<QuoteSensor> QuoteSensors();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("quoteSensors")]
        List<TReturn> QuoteSensors<TReturn>(Expression<Func<QuoteSensor, TReturn>> selection);
        /// <summary>
        /// Return a Sensor by its ID or its serial number
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensor")]
        Sensor Sensor();
        /// <summary>
        /// Return a Sensor by its ID or its serial number
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensor")]
        TReturn Sensor<TReturn>(Guid? id, string serial, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// Get a sensor by its Detexy board serial number
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensorByBoardSerial")]
        Sensor SensorByBoardSerial();
        /// <summary>
        /// Get a sensor by its Detexy board serial number
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensorByBoardSerial")]
        TReturn SensorByBoardSerial<TReturn>(string serial, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// Get a sensor by its serial number
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensorBySerial")]
        Sensor SensorBySerial();
        /// <summary>
        /// Get a sensor by its serial number
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensorBySerial")]
        TReturn SensorBySerial<TReturn>(string serial, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// Return a SensorInstallation by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensorInstallation")]
        SensorInstallation SensorInstallation();
        /// <summary>
        /// Return a SensorInstallation by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensorInstallation")]
        TReturn SensorInstallation<TReturn>(Guid id, Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// Return the active Sensor Installations Optionally filtered by status Or filtered by a search on sensor serial number, SIM, board serial number, camera serial number or lens calibration name
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensorInstallations")]
        List<SensorInstallation> SensorInstallations();
        /// <summary>
        /// Return the active Sensor Installations Optionally filtered by status Or filtered by a search on sensor serial number, SIM, board serial number, camera serial number or lens calibration name
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensorInstallations")]
        List<TReturn> SensorInstallations<TReturn>(string status, string search, Guid? floorId, Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// Return a list of active Sensors
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensors")]
        List<Sensor> Sensors();
        /// <summary>
        /// Return a list of active Sensors
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensors")]
        List<TReturn> Sensors<TReturn>(Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// Return a SpaceGroup by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceGroup")]
        SpaceGroup SpaceGroup();
        /// <summary>
        /// Return a SpaceGroup by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceGroup")]
        TReturn SpaceGroup<TReturn>(Guid id, Expression<Func<SpaceGroup, TReturn>> selection);
        /// <summary>
        /// Return a list of active SpaceGroups
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceGroups")]
        List<SpaceGroup> SpaceGroups();
        /// <summary>
        /// Return a list of active SpaceGroups
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceGroups")]
        List<TReturn> SpaceGroups<TReturn>(Expression<Func<SpaceGroup, TReturn>> selection);
        /// <summary>
        /// Return active space groups filtered by floorId
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceGroupsByFloor")]
        List<SpaceGroup> SpaceGroupsByFloor();
        /// <summary>
        /// Return active space groups filtered by floorId
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceGroupsByFloor")]
        List<TReturn> SpaceGroupsByFloor<TReturn>(Guid floorId, Expression<Func<SpaceGroup, TReturn>> selection);
        /// <summary>
        /// Return active spaces filtered by floorId
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spacesByFloor")]
        List<FloorSpace> SpacesByFloor();
        /// <summary>
        /// Return active spaces filtered by floorId
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spacesByFloor")]
        List<TReturn> SpacesByFloor<TReturn>(Guid floorId, Expression<Func<FloorSpace, TReturn>> selection);
        /// <summary>
        /// Return a SpaceType by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceType")]
        SpaceType SpaceType();
        /// <summary>
        /// Return a SpaceType by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceType")]
        TReturn SpaceType<TReturn>(Guid id, Expression<Func<SpaceType, TReturn>> selection);
        /// <summary>
        /// Return a list of active space types optionally filtered by name
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceTypes")]
        List<SpaceType> SpaceTypes();
        /// <summary>
        /// Return a list of active space types optionally filtered by name
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceTypes")]
        List<TReturn> SpaceTypes<TReturn>(string nameLike, Expression<Func<SpaceType, TReturn>> selection);
        /// <summary>
        /// Return a list of uninstalled sensors optionally filtered by serial number, SIM, board serial number, camera serial number or lens calibration name and limited in number returned
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("uninstalledSensors")]
        List<Sensor> UninstalledSensors();
        /// <summary>
        /// Return a list of uninstalled sensors optionally filtered by serial number, SIM, board serial number, camera serial number or lens calibration name and limited in number returned
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("uninstalledSensors")]
        List<TReturn> UninstalledSensors<TReturn>(string search, int? limit, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// Return a list of active, unused (not assigned to a sensor) camera modules optionally limited andor filtered by serialNumber
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("unusedCameraModules")]
        List<CameraModule> UnusedCameraModules();
        /// <summary>
        /// Return a list of active, unused (not assigned to a sensor) camera modules optionally limited andor filtered by serialNumber
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("unusedCameraModules")]
        List<TReturn> UnusedCameraModules<TReturn>(string like, int? limit, Expression<Func<CameraModule, TReturn>> selection);
        /// <summary>
        /// Return a list of active, unused (not assigned to a sensor) detexy boards optionally limited andor filtered by serialNumber
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("unusedDetexyBoards")]
        List<DetexyBoard> UnusedDetexyBoards();
        /// <summary>
        /// Return a list of active, unused (not assigned to a sensor) detexy boards optionally limited andor filtered by serialNumber
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("unusedDetexyBoards")]
        List<TReturn> UnusedDetexyBoards<TReturn>(string like, int? limit, Expression<Func<DetexyBoard, TReturn>> selection);
        /// <summary>
        /// Return a camera module filtered by serialNumber, case insensitive that is associated with a sensor but does not have an active install
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("usedCameraModules")]
        List<CameraModule> UsedCameraModules();
        /// <summary>
        /// Return a camera module filtered by serialNumber, case insensitive that is associated with a sensor but does not have an active install
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("usedCameraModules")]
        List<TReturn> UsedCameraModules<TReturn>(string serialNumber, Expression<Func<CameraModule, TReturn>> selection);
        /// <summary>
        /// Return a detexy board module filtered by serialNumber, case insensitive that is associated with a sensor but does not have an active install
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("usedDetexyBoards")]
        List<DetexyBoard> UsedDetexyBoards();
        /// <summary>
        /// Return a detexy board module filtered by serialNumber, case insensitive that is associated with a sensor but does not have an active install
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("usedDetexyBoards")]
        List<TReturn> UsedDetexyBoards<TReturn>(string serialNumber, Expression<Func<DetexyBoard, TReturn>> selection);
    }
    public interface SubscriptionType
    {
        [GqlFieldName("name")]
        string Name { get; }
    }
    public interface Customer
    {
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("logoUrl")]
        string LogoUrl { get; }
        /// <summary>
        /// Return a list of floors for the customer
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floors")]
        List<Floor> Floors();
        /// <summary>
        /// Return a list of floors for the customer
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floors")]
        List<TReturn> Floors<TReturn>(Expression<Func<Floor, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("locations")]
        List<Location> Locations();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("locations")]
        List<TReturn> Locations<TReturn>(Expression<Func<Location, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("quotes")]
        List<Quote> Quotes();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("quotes")]
        List<TReturn> Quotes<TReturn>(Expression<Func<Quote, TReturn>> selection);
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
        /// <summary>
        /// Return count of sensors that are active for the customer
        /// </summary>
        [GqlFieldName("totalActiveSensors")]
        int TotalActiveSensors { get; }
    }
    public interface Floor
    {
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("sortSequence")]
        int SortSequence { get; }
        /// <summary>
        /// Current floorplan
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floorPlan")]
        FloorPlan FloorPlan();
        /// <summary>
        /// Current floorplan
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floorPlan")]
        TReturn FloorPlan<TReturn>(Expression<Func<FloorPlan, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("currentFloorPlan")]
        FloorPlan CurrentFloorPlan();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("currentFloorPlan")]
        TReturn CurrentFloorPlan<TReturn>(Expression<Func<FloorPlan, TReturn>> selection);
        [GqlFieldName("timeblockWindowInMilliseconds")]
        int TimeblockWindowInMilliseconds { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("location")]
        Location Location();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("location")]
        TReturn Location<TReturn>(Expression<Func<Location, TReturn>> selection);
        [GqlFieldName("locationId")]
        Guid? LocationId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("installedSensors")]
        List<SensorInstallation> InstalledSensors();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("installedSensors")]
        List<TReturn> InstalledSensors<TReturn>(Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("quoteSensors")]
        List<QuoteSensor> QuoteSensors();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("quoteSensors")]
        List<TReturn> QuoteSensors<TReturn>(Expression<Func<QuoteSensor, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("quote")]
        Quote Quote();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("quote")]
        TReturn Quote<TReturn>(Expression<Func<Quote, TReturn>> selection);
        [GqlFieldName("quoteId")]
        Guid? QuoteId { get; }
        /// <summary>
        /// Return a list of active (as of now) spaces mapped on the floor
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaces")]
        List<FloorSpace> Spaces();
        /// <summary>
        /// Return a list of active (as of now) spaces mapped on the floor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaces")]
        List<TReturn> Spaces<TReturn>(Expression<Func<FloorSpace, TReturn>> selection);
        [GqlFieldName("lastCompletedAnalyticsTimeblock")]
        DateTime? LastCompletedAnalyticsTimeblock { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("startDate")]
        DateTime StartDate { get; }
        [GqlFieldName("endDate")]
        DateTime EndDate { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        /// <summary>
        /// Read only floorplan bytes field
        /// </summary>
        [GqlFieldName("floorplanBytes")]
        string FloorplanBytes { get; }
        /// <summary>
        /// Current floorplan scale
        /// </summary>
        [GqlFieldName("scale")]
        double Scale { get; }
        /// <summary>
        /// Total number of sensors on the floor
        /// </summary>
        [GqlFieldName("totalSensors")]
        int TotalSensors { get; }
        /// <summary>
        /// Return a list of active (as of now) sensor installations mapped on the floor
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("currentInstalledSensors")]
        List<SensorInstallation> CurrentInstalledSensors();
        /// <summary>
        /// Return a list of active (as of now) sensor installations mapped on the floor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("currentInstalledSensors")]
        List<TReturn> CurrentInstalledSensors<TReturn>(Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// All active space groups on this floor
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceGroups")]
        List<SpaceGroup> SpaceGroups();
        /// <summary>
        /// All active space groups on this floor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceGroups")]
        List<TReturn> SpaceGroups<TReturn>(Expression<Func<SpaceGroup, TReturn>> selection);
        /// <summary>
        /// Return only sensors that are active for tracking on the floor
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("activeSensors")]
        List<SensorInstallation> ActiveSensors();
        /// <summary>
        /// Return only sensors that are active for tracking on the floor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("activeSensors")]
        List<TReturn> ActiveSensors<TReturn>(Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// Return count of sensors that are active for tracking on the floor
        /// </summary>
        [GqlFieldName("totalActiveSensors")]
        int TotalActiveSensors { get; }
        /// <summary>
        /// All key dates impacting the floor for space groups
        /// </summary>
        [GqlFieldName("spaceGroupTimeline")]
        List<DateTime> SpaceGroupTimeline { get; }
        /// <summary>
        /// All key dates impacting the floor for space groups
        /// </summary>
        [GqlFieldName("floorSpaceTimeline")]
        List<DateTime> FloorSpaceTimeline { get; }
        /// <summary>
        /// All key dates impacting the floor plan
        /// </summary>
        [GqlFieldName("floorPlanTimeline")]
        List<DateTime> FloorPlanTimeline { get; }
    }
    public interface FloorPlan
    {
        [GqlFieldName("floorId")]
        Guid FloorId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floor")]
        Floor Floor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floor")]
        TReturn Floor<TReturn>(Expression<Func<Floor, TReturn>> selection);
        [GqlFieldName("scale")]
        double Scale { get; }
        [GqlFieldName("floorplanBytes")]
        string FloorplanBytes { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("startDate")]
        DateTime StartDate { get; }
        [GqlFieldName("endDate")]
        DateTime EndDate { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
    }
    public interface Location
    {
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("address")]
        string Address { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        [GqlFieldName("latitude")]
        string Latitude { get; }
        [GqlFieldName("longitude")]
        string Longitude { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
        /// <summary>
        /// Time zone information for current location
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("timezone")]
        TimeZone Timezone();
        /// <summary>
        /// Time zone information for current location
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("timezone")]
        TReturn Timezone<TReturn>(Expression<Func<TimeZone, TReturn>> selection);
    }
    public interface SensorInstallation
    {
        [GqlFieldName("sensorId")]
        Guid SensorId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensor")]
        Sensor Sensor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensor")]
        TReturn Sensor<TReturn>(Expression<Func<Sensor, TReturn>> selection);
        [GqlFieldName("floorId")]
        Guid FloorId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floor")]
        Floor Floor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floor")]
        TReturn Floor<TReturn>(Expression<Func<Floor, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("floorLocation")]
        Point FloorLocation { get; }
        [GqlFieldName("installedHeightInCm")]
        double InstalledHeightInCm { get; }
        [GqlFieldName("orientationAngleInRadians")]
        double OrientationAngleInRadians { get; }
        [GqlFieldName("status")]
        int? Status { get; }
        [GqlFieldName("sensorCoverageType")]
        string SensorCoverageType { get; }
        [GqlFieldName("geofence")]
        List<Point> Geofence { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("startDate")]
        DateTime StartDate { get; }
        [GqlFieldName("endDate")]
        DateTime EndDate { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
    }
    public interface Sensor
    {
        /// <summary>
        /// Return the history of installation locations for this sensor
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("installs")]
        List<SensorInstallation> Installs();
        /// <summary>
        /// Return the history of installation locations for this sensor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("installs")]
        List<TReturn> Installs<TReturn>(Expression<Func<SensorInstallation, TReturn>> selection);
        [GqlFieldName("serialNumber")]
        string SerialNumber { get; }
        [GqlFieldName("cameraModuleId")]
        Guid? CameraModuleId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("cameraModule")]
        CameraModule CameraModule();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("cameraModule")]
        TReturn CameraModule<TReturn>(Expression<Func<CameraModule, TReturn>> selection);
        [GqlFieldName("detexyBoardId")]
        Guid? DetexyBoardId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("detexyBoard")]
        DetexyBoard DetexyBoard();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("detexyBoard")]
        TReturn DetexyBoard<TReturn>(Expression<Func<DetexyBoard, TReturn>> selection);
        [GqlFieldName("activeResolutionWidth")]
        int ActiveResolutionWidth { get; }
        [GqlFieldName("activeResolutionHeight")]
        int ActiveResolutionHeight { get; }
        [GqlFieldName("simSerialNumber")]
        string SimSerialNumber { get; }
        [GqlFieldName("imeiNumber")]
        string ImeiNumber { get; }
        /// <summary>
        /// Return the comment history for this sensor
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("comments")]
        List<SensorComment> Comments();
        /// <summary>
        /// Return the comment history for this sensor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("comments")]
        List<TReturn> Comments<TReturn>(Expression<Func<SensorComment, TReturn>> selection);
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
    }
    public interface CameraModule
    {
        [GqlFieldName("serialNumber")]
        string SerialNumber { get; }
        [GqlFieldName("lensCenterPoint")]
        Point LensCenterPoint { get; }
        [GqlFieldName("lensCalibrationId")]
        Guid LensCalibrationId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("lensCalibration")]
        LensCalibration LensCalibration();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("lensCalibration")]
        TReturn LensCalibration<TReturn>(Expression<Func<LensCalibration, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensor")]
        Sensor Sensor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensor")]
        TReturn Sensor<TReturn>(Expression<Func<Sensor, TReturn>> selection);
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
    }
    public interface LensCalibration
    {
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("antiDistortionCoefficients")]
        List<double> AntiDistortionCoefficients { get; }
        [GqlFieldName("focalLengthX")]
        double FocalLengthX { get; }
        [GqlFieldName("focalLengthY")]
        double FocalLengthY { get; }
        [GqlFieldName("calibrationResolutionWidth")]
        int CalibrationResolutionWidth { get; }
        [GqlFieldName("calibrationResolutionHeight")]
        int CalibrationResolutionHeight { get; }
        [GqlFieldName("maxRadiusInFisheyeCalibrationPixels")]
        double MaxRadiusInFisheyeCalibrationPixels { get; }
        [GqlFieldName("detexyUsableRadiusInCalibrationPixels")]
        int DetexyUsableRadiusInCalibrationPixels { get; }
        [GqlFieldName("isDefault")]
        bool IsDefault { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
    }
    public interface DetexyBoard
    {
        [GqlFieldName("serialNumber")]
        string SerialNumber { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensor")]
        Sensor Sensor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensor")]
        TReturn Sensor<TReturn>(Expression<Func<Sensor, TReturn>> selection);
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
    }
    public interface SensorComment
    {
        [GqlFieldName("sensorId")]
        Guid SensorId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("sensor")]
        Sensor Sensor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("sensor")]
        TReturn Sensor<TReturn>(Expression<Func<Sensor, TReturn>> selection);
        [GqlFieldName("text")]
        string Text { get; }
        [GqlFieldName("userId")]
        string UserId { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
        /// <summary>
        /// Name of the user that made the comment
        /// </summary>
        [GqlFieldName("name")]
        string Name { get; }
    }
    public interface QuoteSensor
    {
        [GqlFieldName("floorId")]
        Guid FloorId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floor")]
        Floor Floor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floor")]
        TReturn Floor<TReturn>(Expression<Func<Floor, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("floorLocation")]
        Point FloorLocation { get; }
        [GqlFieldName("sensorCoverageType")]
        string SensorCoverageType { get; }
        [GqlFieldName("notes")]
        string Notes { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
    }
    public interface Quote
    {
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        [GqlFieldName("name")]
        string Name { get; }
        /// <summary>
        /// Return the floors associated with the quote
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floors")]
        List<Floor> Floors();
        /// <summary>
        /// Return the floors associated with the quote
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floors")]
        List<TReturn> Floors<TReturn>(Expression<Func<Floor, TReturn>> selection);
        [GqlFieldName("areaPerSensor")]
        double AreaPerSensor { get; }
        [GqlFieldName("notes")]
        string Notes { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
    }
    public interface FloorSpace
    {
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("capacity")]
        int Capacity { get; }
        [GqlFieldName("floorId")]
        Guid FloorId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floor")]
        Floor Floor();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floor")]
        TReturn Floor<TReturn>(Expression<Func<Floor, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceType")]
        SpaceType SpaceType();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceType")]
        TReturn SpaceType<TReturn>(Expression<Func<SpaceType, TReturn>> selection);
        [GqlFieldName("spaceTypeId")]
        Guid SpaceTypeId { get; }
        [GqlFieldName("shape")]
        List<Point> Shape { get; }
        [GqlFieldName("shapeJson")]
        string ShapeJson { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("startDate")]
        DateTime StartDate { get; }
        [GqlFieldName("endDate")]
        DateTime EndDate { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        /// <summary>
        /// All space groups this space is assigned to
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceGroups")]
        List<SpaceGroup> SpaceGroups();
        /// <summary>
        /// All space groups this space is assigned to
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceGroups")]
        List<TReturn> SpaceGroups<TReturn>(Expression<Func<SpaceGroup, TReturn>> selection);
    }
    public interface SpaceGroupFloorSpace
    {
        [GqlFieldName("floorSpaceId")]
        Guid FloorSpaceId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("floorSpace")]
        FloorSpace FloorSpace();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("floorSpace")]
        TReturn FloorSpace<TReturn>(Expression<Func<FloorSpace, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("spaceGroupId")]
        Guid SpaceGroupId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaceGroup")]
        SpaceGroup SpaceGroup();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaceGroup")]
        TReturn SpaceGroup<TReturn>(Expression<Func<SpaceGroup, TReturn>> selection);
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("startDate")]
        DateTime StartDate { get; }
        [GqlFieldName("endDate")]
        DateTime EndDate { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
    }
    public interface SpaceGroup
    {
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("ratio")]
        double Ratio { get; }
        [GqlFieldName("description")]
        string Description { get; }
        [GqlFieldName("color")]
        string Color { get; }
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("type")]
        int? Type { get; }
        /// <summary>
        /// Total number of spaces assigned to this group
        /// </summary>
        [GqlFieldName("totalSpaces")]
        int TotalSpaces { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("startDate")]
        DateTime StartDate { get; }
        [GqlFieldName("endDate")]
        DateTime EndDate { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        /// <summary>
        /// All spaces within this group
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("spaces")]
        List<FloorSpace> Spaces();
        /// <summary>
        /// All spaces within this group
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("spaces")]
        List<TReturn> Spaces<TReturn>(Expression<Func<FloorSpace, TReturn>> selection);
    }
    public interface SpaceType
    {
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("parentType")]
        SpaceType ParentType();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("parentType")]
        TReturn ParentType<TReturn>(Expression<Func<SpaceType, TReturn>> selection);
        [GqlFieldName("parentTypeId")]
        Guid? ParentTypeId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("childTypes")]
        List<SpaceType> ChildTypes();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("childTypes")]
        List<TReturn> ChildTypes<TReturn>(Expression<Func<SpaceType, TReturn>> selection);
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("customer")]
        Customer Customer();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("customer")]
        TReturn Customer<TReturn>(Expression<Func<Customer, TReturn>> selection);
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("description")]
        string Description { get; }
        [GqlFieldName("color")]
        string Color { get; }
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("created")]
        DateTime Created { get; }
        [GqlFieldName("updated")]
        DateTime Updated { get; }
        [GqlFieldName("deleted")]
        DateTime Deleted { get; }
    }
    public interface Audit
    {
        [GqlFieldName("id")]
        Guid Id { get; }
        [GqlFieldName("customerId")]
        Guid CustomerId { get; }
        [GqlFieldName("userId")]
        string UserId { get; }
        [GqlFieldName("timestamp")]
        DateTime Timestamp { get; }
        [GqlFieldName("tableName")]
        string TableName { get; }
        [GqlFieldName("action")]
        int? Action { get; }
        [GqlFieldName("keyValues")]
        string KeyValues { get; }
        [GqlFieldName("oldValues")]
        string OldValues { get; }
        [GqlFieldName("newValues")]
        string NewValues { get; }
    }
    public interface HardwareStats
    {
        [GqlFieldName("totalSensors")]
        int TotalSensors { get; }
        [GqlFieldName("totalUninstalledSensors")]
        int TotalUninstalledSensors { get; }
        [GqlFieldName("totalNonActiveInstallations")]
        int TotalNonActiveInstallations { get; }
        [GqlFieldName("totalActiveInstallations")]
        int TotalActiveInstallations { get; }
        [GqlFieldName("totalDetexyBoards")]
        int TotalDetexyBoards { get; }
        [GqlFieldName("totalCameraModules")]
        int TotalCameraModules { get; }
        [GqlFieldName("totalLensCalibrations")]
        int TotalLensCalibrations { get; }
    }
    public interface TimeZone
    {
        [GqlFieldName("countryCode")]
        string CountryCode { get; }
        [GqlFieldName("countryName")]
        string CountryName { get; }
        [GqlFieldName("zoneName")]
        string ZoneName { get; }
        [GqlFieldName("abbreviation")]
        string Abbreviation { get; }
        [GqlFieldName("gmtOffset")]
        int GmtOffset { get; }
        [GqlFieldName("zoneStart")]
        int ZoneStart { get; }
        [GqlFieldName("zoneEnd")]
        int ZoneEnd { get; }
        [GqlFieldName("nextAbbreviation")]
        string NextAbbreviation { get; }
        [GqlFieldName("timestamp")]
        int Timestamp { get; }
        [GqlFieldName("formatted")]
        string Formatted { get; }
    }
    public interface Mutation
    {
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addFloor")]
        TReturn AddFloor<TReturn>(DateTime? currentDate, Guid? locationId, Guid? floorId, string base64Bytes, Guid? customerId, double? scale, Guid? quoteId, string name, int? sortSequence, Expression<Func<Floor, TReturn>> selection);
        /// <summary>
        /// Add a new floor plan to a floor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addNewFloorPlan")]
        TReturn AddNewFloorPlan<TReturn>(DateTime startDate, Guid floorId, string base64Bytes, double scale, Expression<Func<FloorPlan, TReturn>> selection);
        /// <summary>
        /// Update the details of the current active floor plan (or the active floor plan at currentDate)
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateFloor")]
        TReturn UpdateFloor<TReturn>(DateTime? currentDate, Guid? locationId, Guid? floorId, string base64Bytes, Guid? customerId, double? scale, Guid? quoteId, string name, int? sortSequence, Expression<Func<Floor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("setAllShapesToFloorStartDate")]
        TReturn SetAllShapesToFloorStartDate<TReturn>(Guid id, Expression<Func<Floor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addSpaceToFloor")]
        TReturn AddSpaceToFloor<TReturn>(Guid floorId, string name, Guid spaceTypeId, List<Point> shape, DateTime? currentDate, Expression<Func<FloorSpace, TReturn>> selection);
        /// <summary>
        /// Update a mapped space area on a floor Any updates to a shape change the shape for the whole time it has existed (startDate) If you want to make end of life this shape Delete it and add a new shape
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateSpaceOnFloor")]
        TReturn UpdateSpaceOnFloor<TReturn>(Guid id, string name, Guid? spaceTypeId, int? capacity, List<Point> shape, DateTime? updateFrom, Expression<Func<FloorSpace, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("removeSpaceOnFloor")]
        TReturn RemoveSpaceOnFloor<TReturn>(DateTime endDate, Guid id, Expression<Func<FloorSpace, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addOrUpdateSpaceGroup")]
        TReturn AddOrUpdateSpaceGroup<TReturn>(Guid? id, Guid floorId, string name, string color, double ratio, bool isNew, Expression<Func<SpaceGroup, TReturn>> selection);
        /// <summary>
        /// Purge a floor space from the database permanently
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("purgeFloorSpace")]
        TReturn PurgeFloorSpace<TReturn>(Guid id, Expression<Func<FloorSpace, TReturn>> selection);
        /// <summary>
        /// Assign spaces to a space group (eg neighbourhood
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addSpacesToGroup")]
        TReturn AddSpacesToGroup<TReturn>(Guid spaceGroupId, Guid floorId, List<Guid> spaceIds, DateTime currentDate, int? daysDuration, Expression<Func<SpaceGroup, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("removeSpaceGroup")]
        TReturn RemoveSpaceGroup<TReturn>(Guid spaceGroupId, DateTime dateEnd, Expression<Func<SpaceGroup, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("installSensor")]
        TReturn InstallSensor<TReturn>(string serialNumber, Guid floorId, double x, double y, double installedHeightInCm, int sensorCoverageType, Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateSensorInstallation")]
        TReturn UpdateSensorInstallation<TReturn>(Guid? sensorId, Point position, double? installedHeightInCm, double? orientationAngleInRadians, string sensorCoverageType, List<Point> geofence, bool removeGeofence, bool activate, bool deactivate, Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("deleteSensorInstallation")]
        TReturn DeleteSensorInstallation<TReturn>(Guid id, Expression<Func<SensorInstallation, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addLocation")]
        TReturn AddLocation<TReturn>(Guid? customerId, string address, string name, Expression<Func<Location, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addCustomer")]
        TReturn AddCustomer<TReturn>(string name, Expression<Func<Customer, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addQuote")]
        TReturn AddQuote<TReturn>(Guid? quoteId, double? area, Guid customerId, string name, string notes, Expression<Func<Quote, TReturn>> selection);
        /// <summary>
        /// Update the details of a Quote
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateQuote")]
        TReturn UpdateQuote<TReturn>(Guid? quoteId, double? area, Guid customerId, string name, string notes, Expression<Func<Quote, TReturn>> selection);
        /// <summary>
        /// Add a sensor to the quote
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addSensorToQuote")]
        TReturn AddSensorToQuote<TReturn>(Point location, Guid floorId, int? sensorCoverageType, Expression<Func<QuoteSensor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateQuoteSensor")]
        TReturn UpdateQuoteSensor<TReturn>(Guid id, Point position, Expression<Func<QuoteSensor, TReturn>> selection);
        /// <summary>
        /// Remove a sensor from a Quote Removes the SensorInstallation and Sensor objects
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("deleteQuoteSensor")]
        TReturn DeleteQuoteSensor<TReturn>(Guid id, Expression<Func<QuoteSensor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addSensor")]
        TReturn AddSensor<TReturn>(string serialNumber, string cameraModuleSerialNumber, string detexyBoardSerialNumber, bool createBoard, bool createCamera, string simSerialNumber, string imeiNumber, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateSensor")]
        TReturn UpdateSensor<TReturn>(string serialNumber, string cameraModuleSerialNumber, string detexyBoardSerialNumber, bool createBoard, bool createCamera, string simSerialNumber, string imeiNumber, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("deleteSensor")]
        TReturn DeleteSensor<TReturn>(Guid id, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addLensCalibration")]
        TReturn AddLensCalibration<TReturn>(CalibrationArgs calibration, Expression<Func<LensCalibration, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateLensCalibration")]
        TReturn UpdateLensCalibration<TReturn>(CalibrationArgs calibration, Expression<Func<LensCalibration, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addSensorComment")]
        TReturn AddSensorComment<TReturn>(Guid? id, string serial, string text, Expression<Func<Sensor, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addCameraModule")]
        TReturn AddCameraModule<TReturn>(CameraModuleArgs camera, Expression<Func<CameraModule, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateCameraModule")]
        TReturn UpdateCameraModule<TReturn>(CameraModuleArgs camera, Expression<Func<CameraModule, TReturn>> selection);
        /// <summary>
        /// Update the last date of processed analytics for a floor
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("updateFloorProcessed")]
        TReturn UpdateFloorProcessed<TReturn>(Guid floorId, DateTime lastProcessedAnalytics, Expression<Func<Floor, TReturn>> selection);
    }
    public class Point
    {
        [GqlFieldName("x")]
        public double X { get; set; }
        [GqlFieldName("y")]
        public double Y { get; set; }
    }
    public class CalibrationArgs
    {
        [GqlFieldName("id")]
        public Guid? Id { get; set; }
        [GqlFieldName("name")]
        public string Name { get; set; }
        [GqlFieldName("antiDistortionCoefficients")]
        public List<double> AntiDistortionCoefficients { get; set; }
        [GqlFieldName("focalLengthX")]
        public double FocalLengthX { get; set; }
        [GqlFieldName("focalLengthY")]
        public double FocalLengthY { get; set; }
        [GqlFieldName("calibrationResolution")]
        public Size CalibrationResolution { get; set; }
        [GqlFieldName("maxRadiusInFisheyeCalibrationPixels")]
        public int MaxRadiusInFisheyeCalibrationPixels { get; set; }
        [GqlFieldName("isDefault")]
        public bool IsDefault { get; set; }
        [GqlFieldName("detexyUsableRadiusInCalibrationPixels")]
        public int DetexyUsableRadiusInCalibrationPixels { get; set; }
    }
    public class CameraModuleArgs
    {
        [GqlFieldName("id")]
        public Guid? Id { get; set; }
        [GqlFieldName("serialNumber")]
        public string SerialNumber { get; set; }
        [GqlFieldName("lensCenter")]
        public Point LensCenter { get; set; }
        [GqlFieldName("lensCalibrationId")]
        public Guid LensCalibrationId { get; set; }
    }
    public class Size
    {
        [GqlFieldName("width")]
        public int Width { get; set; }
        [GqlFieldName("height")]
        public int Height { get; set; }
    }

}
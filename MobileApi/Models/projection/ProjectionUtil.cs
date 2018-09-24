using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeoAPI.Geometries;
using NetTopologySuite.IO;

namespace MobileApi.Models.projection
{
    public class ProjectionUtil
    {

        #region Public static constants

        public static readonly Int32 GCS_WGS84_SRID = 4326;
        public static readonly String GCS_WGS84_WKT = "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.01745329251994328,AUTHORITY[\"EPSG\",\"9122\"]],AUTHORITY[\"EPSG\",\"4326\"]]";

        public static readonly Int32 GOOGLE_MERCATOR_SRID = 900913;
        public static readonly String GOOGLE_MERCATOR_PROJECTION_WKT = "PROJCS[\"Google Mercator\",GEOGCS[\"WGS 84\",DATUM[\"World Geodetic System 1984\",SPHEROID[\"WGS 84\",6378137.0,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0.0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.017453292519943295],AXIS[\"Geodetic latitude\",NORTH],AXIS[\"Geodetic longitude\",EAST],AUTHORITY[\"EPSG\",\"4326\"]],PROJECTION[\"Mercator_1SP\"],PARAMETER[\"semi_minor\",6378137.0],PARAMETER[\"latitude_of_origin\",0.0],PARAMETER[\"central_meridian\",0.0],PARAMETER[\"scale_factor\",1.0],PARAMETER[\"false_easting\",0.0],PARAMETER[\"false_northing\",0.0],UNIT[\"m\",1.0],AXIS[\"Easting\",EAST],AXIS[\"Northing\",NORTH],AUTHORITY[\"EPSG\",\"900913\"]]";

        public static readonly Int32 SINUSOIDAL_SRID = 96842;
        public static readonly String SINUSOIDAL_WKT = "PROJCS[\"Sinusoidal\",GEOGCS[\"GCS_Undefined\",DATUM[\"Undefined\",SPHEROID[\"User_Defined_Spheroid\",6371007.181,0.0]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]],PROJECTION[\"Sinusoidal\"],PARAMETER[\"False_Easting\",0.0],PARAMETER[\"False_Northing\",0.0],PARAMETER[\"Central_Meridian\",0.0],UNIT[\"Meter\",1.0]]";

        public static readonly Int32 NORTH_AMERICAN_ALBERS_EQUAL_AREA_SRID = 9102008;
        public static readonly String NORTH_AMERICAN_ALBERS_EQUAL_AREA_WKT = "PROJCS[\"North_America_Albers_Equal_Area_Conic\",GEOGCS[\"GCS_North_American_1983\",DATUM[\"North_American_Datum_1983\",SPHEROID[\"GRS_1980\",6378137,298.257222101]],PRIMEM[\"Greenwich\",0],UNIT[\"Degree\",0.017453292519943295]],PROJECTION[\"Albers_Conic_Equal_Area\"],PARAMETER[\"False_Easting\",0],PARAMETER[\"False_Northing\",0],PARAMETER[\"longitude_of_center\",-96],PARAMETER[\"Standard_Parallel_1\",20],PARAMETER[\"Standard_Parallel_2\",60],PARAMETER[\"latitude_of_center\",40],UNIT[\"Meter\",1],AUTHORITY[\"EPSG\",\"102008\"]]";

        #endregion

        #region Public static methods

        public static IGeometry ConvertGCSWGS84ToGoogleMercator(IGeometry geometry)
        {
            ICoordinateFilter filter = new GCSWGS84ToGoogleMercatorCoordinateFilter();
            geometry.Apply(filter);

            return geometry;
        }

        public static void ConvertToGCSWGS84(IGeometry geometry)
        {
            ICoordinateFilter filter = new GCSWGS84CoordinateFilter();
            (filter as GCSWGS84CoordinateFilter).Projection = getWellKnownTextProjection(geometry.SRID);
            geometry.Apply(filter);
            geometry.SRID = ProjectionUtil.GCS_WGS84_SRID;
        }

        public static void ConvertToGCSWGS84(IGeometry geometry, String wktProjection)
        {
            ICoordinateFilter filter = new GCSWGS84CoordinateFilter();
            (filter as GCSWGS84CoordinateFilter).Projection = wktProjection;
            geometry.Apply(filter);
            geometry.SRID = ProjectionUtil.GCS_WGS84_SRID;
        }

        public static IGeometry ConvertGCSWGS84ToNorthAmericanAlbersEqualArea(IGeometry geometry)
        {
            ICoordinateFilter filter = new GCSWGS84ToNAAlbersEqualAreaCoordinateFilter();
            geometry.Apply(filter);
            geometry.SRID = NORTH_AMERICAN_ALBERS_EQUAL_AREA_SRID;

            return geometry;
        }
        #endregion

        // TODO: Hit the spatial_ref_sys table to get this information.
        private static String getWellKnownTextProjection(Int32 srid)
        {
            switch (srid)
            {
                case 4326:
                    return GCS_WGS84_WKT;

                case 900913:
                    return GOOGLE_MERCATOR_PROJECTION_WKT;

                case 96842:
                    return SINUSOIDAL_WKT;

                default:
                    throw new NotSupportedException(
                       String.Format(
                          "The {0} SRID is not supported in ProjectionUtil",
                          srid));
            }
        }
    }
}
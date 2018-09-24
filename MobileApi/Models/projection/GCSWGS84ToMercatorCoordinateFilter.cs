#region

using System.Collections.Generic;


using ProjNet.Converters.WellKnownText;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
//using SharpMap.Converters.WellKnownText;
//using SharpMap.CoordinateSystems;
//using SharpMap.CoordinateSystems.Transformations;

#endregion
namespace MobileApi.Models.projection
{
   public class GCSWGS84ToGoogleMercatorCoordinateFilter : GeoAPI.Geometries.ICoordinateFilter
   {
      #region Private constants

      private const string SRID_4236 = "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.01745329251994328,AUTHORITY[\"EPSG\",\"9122\"]],AUTHORITY[\"EPSG\",\"4326\"]]";

      #endregion

      #region ICoordinateFilter Members

      public void Filter( GeoAPI.Geometries.Coordinate coord )
      {
            ProjNet.CoordinateSystems.ICoordinateSystem mercator = getMercatorProjection();
            ProjNet.CoordinateSystems.IGeographicCoordinateSystem latlon =
            (ProjNet.CoordinateSystems.IGeographicCoordinateSystem)CoordinateSystemWktReader.Parse( SRID_4236 );
            ProjNet.CoordinateSystems.Transformations.ICoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            ProjNet.CoordinateSystems.Transformations.ICoordinateTransformation transformation =
            ctfac.CreateFromCoordinateSystems( latlon, mercator );

         double[] newCoords = transformation.MathTransform.Transform( new double[]{ coord.X, coord.Y } );
         coord.X = newCoords[0];
         coord.Y = newCoords[1];
      }

      #endregion

      #region Private methods

      private ProjNet.CoordinateSystems.ICoordinateSystem getMercatorProjection()
      {
         CoordinateSystemFactory factory = new CoordinateSystemFactory();

            ProjNet.CoordinateSystems.IGeographicCoordinateSystem wgs84 = factory.CreateGeographicCoordinateSystem( "WGS 84",
             AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
             new ProjNet.CoordinateSystems.AxisInfo( "north", ProjNet.CoordinateSystems.AxisOrientationEnum.North ), new
             AxisInfo( "east", AxisOrientationEnum.East ) );

         List<ProjectionParameter> parameters = new List<ProjectionParameter>
            {
                new ProjectionParameter("semi_major", 6378137), 
                new ProjectionParameter("semi_minor", 6378137), 
                new ProjectionParameter("latitude_of_origin", 0.0),
                new ProjectionParameter("central_meridian", 0.0),
                new ProjectionParameter("scale_factor", 1.0),
                new ProjectionParameter("false_easting", 0.0),
                new ProjectionParameter("false_northing", 0.0)
            };

         IProjection projection = factory.CreateProjection( "Mercator", "mercator_1sp", parameters );
         IProjectedCoordinateSystem mercator = factory.CreateProjectedCoordinateSystem( "Mercator",
             wgs84, projection, LinearUnit.Metre,
             new AxisInfo( "East", AxisOrientationEnum.East ),
             new AxisInfo( "North", AxisOrientationEnum.North ) );

         return mercator;
      }

      #endregion
   }
}

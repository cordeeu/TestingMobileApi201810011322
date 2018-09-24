#region

using ProjNet.Converters.WellKnownText;
using ProjNet.CoordinateSystems.Transformations;
//using SharpMap.Converters.WellKnownText;
//using SharpMap.CoordinateSystems.Transformations;

#endregion

namespace MobileApi.Models.projection
{
   public class GCSWGS84ToNAAlbersEqualAreaCoordinateFilter : GeoAPI.Geometries.ICoordinateFilter
   {
      #region ICoordinateFilter Members

      public void Filter(GeoAPI.Geometries.Coordinate coord )
      {
            ProjNet.CoordinateSystems.ICoordinateSystem equalArea =
             (ProjNet.CoordinateSystems.ICoordinateSystem)CoordinateSystemWktReader.Parse(ProjectionUtil.NORTH_AMERICAN_ALBERS_EQUAL_AREA_WKT);
            ProjNet.CoordinateSystems.IGeographicCoordinateSystem latlon =
             (ProjNet.CoordinateSystems.IGeographicCoordinateSystem)CoordinateSystemWktReader.Parse(ProjectionUtil.GCS_WGS84_WKT);
          ICoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
          ICoordinateTransformation transformation =
             ctfac.CreateFromCoordinateSystems(latlon, equalArea);

         double[] newCoords = transformation.MathTransform.Transform( new double[]{ coord.X, coord.Y } );
         coord.X = newCoords[0];
         coord.Y = newCoords[1];
      }

      #endregion
   }
}

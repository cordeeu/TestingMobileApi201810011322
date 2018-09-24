#region

using System;
using ProjNet.Converters.WellKnownText;
using ProjNet.CoordinateSystems.Transformations;
//using SharpMap.Converters.WellKnownText;
//using SharpMap.CoordinateSystems.Transformations;

#endregion

namespace MobileApi.Models.projection
{
   public class GCSWGS84CoordinateFilter : GeoAPI.Geometries.ICoordinateFilter
   {
      #region Class member variables

      private const String SRID_4236 = "GEOGCS[\"WGS 84\",DATUM[\"WGS_1984\",SPHEROID[\"WGS 84\",6378137,298.257223563,AUTHORITY[\"EPSG\",\"7030\"]],AUTHORITY[\"EPSG\",\"6326\"]],PRIMEM[\"Greenwich\",0,AUTHORITY[\"EPSG\",\"8901\"]],UNIT[\"degree\",0.01745329251994328,AUTHORITY[\"EPSG\",\"9122\"]],AUTHORITY[\"EPSG\",\"4326\"]]";
      private String projectionWkt;

      #endregion

      #region Public properties

      /// <summary>
      /// The projection definition in well-known text format.
      /// </summary>
      public String Projection
      {
         get { return projectionWkt; }
         set { projectionWkt = value; }
      }

      #endregion

      #region ICoordinateFilter Members

      public void Filter(GeoAPI.Geometries.Coordinate coord )
      {
         // TODO: Handle NotSupportedException for projections so
         // that we can offer the user a solution.
         ProjNet.CoordinateSystems.ICoordinateSystem fromCoordinateSystem = 
            (ProjNet.CoordinateSystems.ICoordinateSystem)CoordinateSystemWktReader.Parse( projectionWkt );

            ProjNet.CoordinateSystems.IGeographicCoordinateSystem toLatLon =
            (ProjNet.CoordinateSystems.IGeographicCoordinateSystem)CoordinateSystemWktReader.Parse( SRID_4236 );
            ProjNet.CoordinateSystems.Transformations.ICoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();

            ProjNet.CoordinateSystems.Transformations.ICoordinateTransformation transformation =
            ctfac.CreateFromCoordinateSystems(fromCoordinateSystem, toLatLon);

         //ICoordinateTransformation transformation =
         //   ctfac.CreateFromCoordinateSystems()

         double[] newCoords = transformation.MathTransform.Transform( new double[]{ coord.X, coord.Y } );
         coord.X = newCoords[0];
         coord.Y = newCoords[1];
      }

      #endregion
   }
}

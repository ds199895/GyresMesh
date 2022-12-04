using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.IO
{
    public class IConfig
    {
        public static double tolerance = 0.001D;
        public static double parameterTolerance = 0.001D;
        public static double angleTolerance = 0.0031415926535897933D;
        public static int segmentResolution = 10;
        public static int isoparmResolution = 1;
        public static int tessellationResolution = 5;
        public static int trimSegmentResolution = 4;
        public static int trimApproximationResolution = 20;
        public static IColor objectColor = new IColor(102, 102, 102);
        public static IColor strokeColor = new IColor(102, 102, 102);
        public static float strokeWeight = 1.0F;
        public static IColor ambientColor = null;
        public static IColor emissiveColor = null;
        public static IColor specularColor = new IColor(255, 255, 255);
        public static float shininess = 0.5F;
        public static int transparentModeAlpha = 102;
        public static IColor bgColor1 = new IColor(255, 255, 255);
        public static IColor bgColor2 = new IColor(230, 230, 230);
        public static IColor bgColor3 = new IColor(77, 128, 179);
        public static IColor bgColor4 = new IColor(77, 128, 179);
        public static float pointSize = 5.0F;
        public static float arrowSize = 2.0F;
        public static float arrowWidthRatio = 0.5F;
        public static bool useLight = true;
        public static bool lightWireframe = false;
        public static bool transparentWireframe = false;
        public static bool cullVertexBehindViewInP3D = true;
        public static bool cullVertexBehindViewInGL = false;
        public static bool depthSort = true;
        public static bool disableDepthTest = false;
        public static int maxObjectNumberForDepthSort = 3000;
        public static bool smoothGraphicP3D = true;
        public static bool drawOrderForward = false;
        public static bool deleteGraphicObjectsAfterDraw = false;
        public static bool clearBG = true;
        public static int meshCircleResolution = 24;
        public static bool updateMeshNormal = false;
        public static bool removeDuplicatesAtMeshCreation = true;
        public static int curveCacheResolution = 10;
        public static int surfaceCacheResolution = 4;
        public static int cacheRecursionMaxDepth = 15;
        public static double defaultFieldIntensity = 10.0D;
        public static bool defaultConstantFieldIntensity = true;
        public static double updateRate = 0.03D;
        public static bool enablePreinteract = true;
        public static bool enablePostinteract = true;
        public static bool enablePreupdate = true;
        public static bool enablePostupdate = true;
        public static bool loopPreinteract = true;
        public static bool loopPostinteract = true;
        public static bool loopPreupdate = true;
        public static bool loopPostupdate = false;
        public static bool syncDrawAndDynamics = false;
        public static bool clearParticleForceInPostupdate = false;
        public static bool insertBouncePointInTrajectory = true;
        public static bool checkAdjacentWalls = true;
        public static double mouseRotationSpeed = 0.3D;
        public static double mousePerspectivePanResolution = 200.0D;
        public static double mouseAxonometricPanSpeed = 1.0D;
        public static double mousePerspectiveZoomResolution = 200.0D;
        public static double mouseAxonometricZoomSpeed = 0.75D;
        public static double mouseWheelZoomSpeed = 40.0D;
        public static bool autoFocusAtStart = true;
        public static double keyRotationSpeed = 3.0D;
        public static double keyPerspectivePanSpeed = 0.5D;
        public static double keyAxonometricPanSpeed = 10.0D;
        public static double keyPerspectiveZoomSpeed = 1.0D;
        public static double keyZoomSpeed = 5.0D;
        public static double nearViewRatio = 0.001D;
        public static double farViewRatio = 1000.0D;
        public static double viewDistanceRatio = 10.0D;
        public static double viewDistance = 500.0D;
        public static double axonometricRatio = 1.0D;
        public static double perspectiveRatio = 0.5D;
        public static bool checkDuplicatedControlPointOnEdge = true;
        public static bool checkDuplicatedControlPoint = true;
        public static bool checkValidControlPoint = true;
        public static double defaultAIExportScale = 1.0E-5D;
        public static double defaultAIExportPixelScale = 0.1D;
        public static bool read3dmUserData = false;

        public IConfig()
        {
        }

    }
}

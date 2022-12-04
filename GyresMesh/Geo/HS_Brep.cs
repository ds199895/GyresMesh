//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hsy.Geo
//{
//    public class HS_Brep:HS_Geometry
//    {
//        public HS_Surface[] surfaces;
//        public bool solid;

//        public HS_Brep(HS_Surface[] var2)
//        {
//            this.solid = false;
//            this
//        }
//        public void initBrep()
//        {
//            this.parameter = null;
//            if (this.graphics == null)
//            {
//                this.initGraphic(var1);
//            }

//            if (IConfig.checkDuplicatedControlPoint)
//            {
//                this.checkDuplicatedPoints();
//            }

//        }


//        public void checkDuplicatedPoints()
//        {
//            bool var1 = false;

//            for (int var2 = 0; var2 < this.surfaces.Length; ++var2)
//            {
//                for (int var3 = 0; var3 < this.surfaces[var2].ucpNum(); ++var3)
//                {
//                    for (int var4 = 0; var4 < this.surfaces[var2].vcpNum(); ++var4)
//                    {
//                        HS_Vector var5 = this.surfaces[var2].cp(var3, var4);

//                        for (int var6 = var2 + 1; var6 < this.surfaces.Length; ++var6)
//                        {
//                            for (int var7 = 0; var7 < this.surfaces[var6].ucpNum(); ++var7)
//                            {
//                                for (int var8 = 0; var8 < this.surfaces[var6].vcpNum(); ++var8)
//                                {
//                                    if (this.surfaces[var6].cp(var7, var8) == var5)
//                                    {
//                                        this.surfaces[var6].controlPoints[var7][var8] = var5.dup();
//                                        var1 = true;
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }

//            if (var1)
//            {
//                this.updateGraphic();
//            }

//        }

//        public void initBrepWithSurfaceGeo( HS_Surface[] var2)
//        {
//            if (var2 == null)
//            {
//                IOut.err("input surface is null");
//            }
//            else
//            {
//                this.surfaces = var2;
//                this.initBrep(var1);
//            }
//        }
//    }
//}

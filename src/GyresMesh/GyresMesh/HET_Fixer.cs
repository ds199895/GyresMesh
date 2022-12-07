using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.GyresMesh
{
    public class GET_Fixer
    {
		public static void deleteTwoEdgeFace( GE_Mesh mesh,  GE_Face f)
		{
			if (mesh.Contains(f))
			{
				 GE_Halfedge he = f.GetHalfedge();
				 GE_Halfedge hen = he.GetNextInFace();
				if (he == hen.GetNextInFace())
				{
					 GE_Halfedge hePair = he.Pair();
					 GE_Halfedge henPair = hen.Pair();
					mesh.remove(f);
					mesh.remove(he);
					mesh.SetHalfedge(he.GetStart(),he.GetNextInVertex());
					mesh.remove(hen);
					mesh.SetHalfedge(hen.GetStart(), hen.GetNextInVertex());
					mesh.SetPair(hePair, henPair);
				}
			}
		}
	}
}

using Hsy.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Core
{
    public class HS_HashCode
    {
		/**
	 *
	 *
	 * @param x
	 * @param y
	 * @return
	 */
		public unsafe static int calculateHashCode( double x,  double y)
		{
			int result = 17;

			 long a = *(long*)&x;
			result += 31 * result + (int)(a ^ a >> 32);
			 long b = *(long*)&y;
			result += 31 * result + (int)(b ^ b >> 32);
			return result;
		}

		/**
		 *
		 *
		 * @param x
		 * @param y
		 * @param z
		 * @return
		 */
		public unsafe static int calculateHashCode( double x,  double y,  double z)
		{
			int result = 17;
			 long a = *(long*)&x;
			result += 31 * result + (int)(a ^ a >> 32);
			 long b = *(long*)&y;
			result += 31 * result + (int)(b ^ b >> 32);
			 long c = *(long*)&z;
			result += 31 * result + (int)(c ^ c >> 32);
			return result;
		}
		/**
 *
 *
 * @param x
 * @param y
 * @param z
 * @return
 */
		public unsafe static int calculateHashCode(HS_Coord p)
		{
			double x = p.xd;
			double y = p.yd;
			double z = p.zd;
			int result = 7;
			long a = *(long*)&x;
			result += 71 * result + (int)(a ^ a >> 32);
			long b = *(long*)&y;
			result += 71 * result + (int)(b ^ b >> 32);
			long c = *(long*)&z;
			result += 71 * result + (int)(c ^ c >> 32);
			return result;
		}

		/**
*
*
* @param x
* @param y
* @param z
* @return
*/
		public unsafe static int calculateHashCode(HS_Coord p,int id)
		{
			double x = p.xd;
			double y = p.yd;
			double z = p.zd;
			double w = id;
            unchecked
            {
				int result = (int)2166136261;
				long a = *(long*)&x;
				result += 16777619 * result + (int)(a ^ a >> 32);
				long b = *(long*)&y;
				result += 16777619 * result + (int)(b ^ b >> 32);
				long c = *(long*)&z;
				result += 16777619 * result + (int)(c ^ c >> 32);
				long d = *(long*)&w;
				result += 16777619 * result + (int)(d ^ d >> 32);
				return result;
			}

		}

		/**
		 *
		 *
		 * @param x
		 * @param y
		 * @param z
		 * @param w
		 * @return
		 */
		public unsafe static int calculateHashCode( double x,  double y,  double z,  double w)
		{
			int result = 17;
			 long a = *(long*)&x;
			result += 31 * result + (int)(a ^ a >> 32);
			 long b = *(long*)&y;
			result += 31 * result + (int)(b ^ b >> 32);
			 long c = *(long*)&z;
			result += 31 * result + (int)(c ^ c >> 32);
			 long d = *(long*)&w;
			result += 31 * result + (int)(d ^ d >> 32);
			return result;
		}
	}
}

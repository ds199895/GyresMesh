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
			x = x + 10000;
			y = y + 10000;
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
			x = x + 10000;
			y = y + 10000;
			z = z + 10000;
			int result = 31;
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
		public unsafe static int calculateHashCode(HS_Coord p)
		{
			double x = p.xd+10000;
			double y = p.yd+10000;
			double z = p.zd+10000;
      int result = 31;
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
			double x = p.xd + 10000;
			double y = p.yd + 10000;
			double z = p.zd + 10000;
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
			x = x + 10000;
			y = y + 10000;
			z = z + 10000;
			w = w + 10000;
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

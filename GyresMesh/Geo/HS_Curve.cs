using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hsy.Geo
{
    public interface HS_Curve
    {
		/**
	 *
	 *
	 * @param u
	 * @return
	 */
		public HS_Point getPointOnCurve(double u);

		/**
		 *
		 *
		 * @param u
		 * @return
		 */
		public HS_Vector getDirectionOnCurve(double u);

		/**
		 *
		 *
		 * @param u
		 * @return
		 */
		public HS_Vector getDerivative(double u);

		/**
		 *
		 *
		 * @return
		 */
		public double getLowerU();

		/**
		 *
		 *
		 * @return
		 */
		public double getUpperU();
	}
}
